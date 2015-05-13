using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class InventoryService
	{
		string _gpEmployeeId;
		public InventoryService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<MetadataType>>> LocationTypesAsync()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_LocationTypes;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<MetadataType>>> LocationsByLocationTypeIdAsync(string locationTypeId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_LocationTypes;
				var items = await tbl.LocationsByLocationTypeIdAsync(locationTypeId).ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<IeProductBarcodeLast>>> ProductBarcodesByLocationIdAsync(string locationId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_ProductBarcodes;
				var items = await tbl.ProductBarcodeLastByLocationIdAsync(locationId).ConfigureAwait(false);
				var result = new Result<List<IeProductBarcodeLast>>(value: items.ConvertAll(item => IeProductBarcodeLast.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<IeProductBarcodeLast>> ProductBarcodesByIdAsync(string productBarcodeId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_ProductBarcodes;
				var item = await tbl.ProductBarcodeLastByIdAsync(productBarcodeId).ConfigureAwait(false);
				var result = new Result<IeProductBarcodeLast>(value: IeProductBarcodeLast.FromDb(item, true));
				return result;
			}
		}

		public static DateTime CloseOn { get { return DateTime.Now.Subtract(TimeSpan.FromHours(24)); } }
		private IeAudit AuditFromDb(IE_Audit item)
		{
			var result = IeAudit.FromDb(item);
			result.IsClosed = item.CreatedOn < CloseOn;
			return result;
		}

		public async Task<Result<List<IeAudit>>> AuditsByLocationIdAsync(string locationId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_Audits;
				var items = await tbl.ByLocationIdAsync(locationId).ConfigureAwait(false);
				return new Result<List<IeAudit>>(value: items.ConvertAll(AuditFromDb));
			}
		}
		public async Task<Result<IeAudit>> SaveAuditAsync(IeAuditSave inputItem)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<IeAudit>();

				// load on hand parts
				var onHand = await db.IE_ProductBarcodes.ProductBarcodeLastByLocationIdAsync(inputItem.LocationId).ConfigureAwait(false);
				var onHandDict = new Dictionary<string, IE_ProductBarcodeLast>(StringComparer.OrdinalIgnoreCase);
				foreach (var item in onHand)
					onHandDict.Add(item.ProductBarcodeID, item);

				// check that all the barcodes are currently assigned to the location
				foreach (var barcode in inputItem.Barcodes)
					if (!onHandDict.ContainsKey(barcode))
						return result.Fail(-1, string.Format("Barcode '{0}' does not belong to location", barcode));

				var locationId = inputItem.LocationId;
				var locationTypeId = inputItem.LocationTypeId;
				await db.TransactionAsync(async () =>
				{
					// save audit
					IE_Audit audit;
					if (inputItem.ID <= 0)
					{
						// create audit
						audit = new IE_Audit();
						inputItem.ToDb(audit);
						audit.IsActive = true;
						audit.IsDeleted = false;
						await db.IE_Audits.InsertAsync(audit, _gpEmployeeId).ConfigureAwait(false);
					}
					else
					{
						// load audit
						audit = await db.IE_Audits.ByIdWithUpdateLockAsync(inputItem.ID).ConfigureAwait(false);
						if (audit == null)
							return result.Fail(-1, "Invalid audit ID");
						if (audit.CreatedOn < CloseOn)
							return result.Fail(-1, "Audit is closed");
						if (audit.LocationId != inputItem.LocationId)
							return result.Fail(-1, "Location cannot be changed");
						if (audit.LocationTypeId != inputItem.LocationTypeId)
							return result.Fail(-1, "LocationType cannot be changed");
						// check ModifiedOn
						if (VersionHelper.CheckModifiedOn(audit.ModifiedOn, inputItem.ModifiedOn, result, () => AuditFromDb(audit)).Failure)
							return result;
						// update audit
						var snapshot = Snapshotter.Start(audit);
						// ensure active and not deleted
						audit.IsActive = true;
						audit.IsDeleted = false;
						await db.IE_Audits.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
					}
					// set return value
					result.Value = AuditFromDb(audit);

					// save audit barcodes
					var trackingTypeId = IE_ProductBarcodeTrackingType.MetaData.AuditPerformedID;
					foreach (var barcode in inputItem.Barcodes)
					{
						IE_ProductBarcodeLast onHandItem;
						if (!onHandDict.TryGetValue(barcode, out onHandItem))
							continue; // ignore duplicates
						onHandDict.Remove(barcode); // remove from on hand parts
						// track barcode
						await TrackBarcodeAsync(db, _gpEmployeeId, onHandItem, trackingTypeId, locationTypeId, locationId, audit.ID).ConfigureAwait(false);
					}

					// mark barcodes as missing
					trackingTypeId = IE_ProductBarcodeTrackingType.MetaData.Audit_EquipmentMissingID;
					foreach (var barcode in onHandDict.Keys)
					{
						var onHandItem = onHandDict[barcode];
						await TrackBarcodeAsync(db, _gpEmployeeId, onHandItem, trackingTypeId, locationTypeId, locationId, audit.ID).ConfigureAwait(false);
					}

					return result;
				});

				return result;
			}
		}

		internal static async Task TrackBarcodeAsync(DBase db, string gpEmployeeId, IE_ProductBarcodeLast onHandItem, string productBarcodeTrackingTypeId, string locationTypeId, string locationId, int? auditId = null, string comment = null)
		{
			// don't update if the tracking isn't changing
			if (onHandItem.ProductBarcodeTrackingTypeId == productBarcodeTrackingTypeId &&
				onHandItem.LocationTypeId == locationTypeId &&
				onHandItem.AuditId == auditId &&
				onHandItem.LocationId == locationId)
			{
				return;
			}
			var barcode = onHandItem.ProductBarcodeID;

			// add tracking row
			var item = new IE_ProductBarcodeTracking();
			item.ProductBarcodeId = barcode;
			item.ProductBarcodeTrackingTypeId = productBarcodeTrackingTypeId;
			item.LocationTypeId = locationTypeId;
			item.LocationId = locationId;
			item.AuditId = auditId;
			item.Comment = comment;
			await db.IE_ProductBarcodeTrackings.InsertAsync(item, gpEmployeeId).ConfigureAwait(false);

			// update product barcode
			var snapshot = Snapshotter.Start(new IE_ProductBarcode() { ID = barcode });
			snapshot.Value.LastProductBarcodeTrackingId = item.ProductBarcodeTrackingID;
			await db.IE_ProductBarcodes.UpdateAsync(snapshot, gpEmployeeId).ConfigureAwait(false);
		}
	}
}
