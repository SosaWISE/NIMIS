using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class AccountInvoicesService
	{
		string _gpEmployeeId;
		public AccountInvoicesService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		//public async Task<Result<List<Invoice>>> Invoices(long accountId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var tbl = db.AE_Invoices;
		//		var items = await tbl.ByAccountIdAsync(accountId).ConfigureAwait(false);
		//		var result = new Result<List<Invoice>>(value: items.ConvertAll(item => Invoice.FromDb(item)));
		//		return result;
		//	}
		//}

		public async Task<Result<List<AeItem>>> Items()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.AE_Items;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<AeItem>>(value: items.ConvertAll(item => AeItem.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<AeInvoice>> InstallInvoice(long accountId, bool canCreate)
		{
			using (var db = DBase.Connect())
			{
				var account = await db.MS_Accounts.EnsureByIdAsync(accountId, _gpEmployeeId).ConfigureAwait(false);

				var tbl = db.AE_Invoices;
				var invoiceTypeId = AE_InvoiceType.MetaData.SetupandInstallationID;
				var item = (await tbl.ByAccountIdAndTypeFullAsync(accountId, invoiceTypeId).ConfigureAwait(false));
				if (item == null && canCreate)
					item = await tbl.CreateInvoiceAsync(accountId, invoiceTypeId, _gpEmployeeId).ConfigureAwait(false);
				var result = new Result<AeInvoice>(value: AeInvoice.FromDb(item, true));
				return result;
			}
		}

		public async Task<Result<AeInvoice>> SaveInvoice(AeInvoice inputItem)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<AeInvoice>();
				AE_Invoice item = null;

				await db.TransactionAsync(async () =>
				{
					var tbl = db.AE_Invoices;

					// read and lock invoice(WITH(ROWLOCK,UPDLOCK)) and items(Full)
					item = await tbl.ByIdWithUpdateLockFullAsync(inputItem.ID).ConfigureAwait(false);
					if (item == null)
					{
						result.Fail(-1, "Invalid Invoice ID");
						return false;
					}
					// check ModifiedOn matches input
					if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
					{
						result.Fail(-1, "Invoice(" + inputItem.ID + "): " + result.Message);
						return false;
					}

					// update input items
					var invoiceItems = item.InvoiceItems.ToList();
					item.InvoiceItems = invoiceItems; // overwrite with list that we are modifying
					if (!await SaveInvoiceItemsAsync(db, result, item.ID, invoiceItems, inputItem.InvoiceItems))
						return false;

					// ensure the invoice is active and not deleted
					if (!item.IsActive || item.IsDeleted)
					{
						var snapShot = Snapshotter.Start(item);
						item.IsActive = true;
						item.IsDeleted = false;
						await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					//@REVIEW: get the correct state and zip
					// the current method in custAE_InvoiceItemRefreshMsAccountInstall001 will almost
					// always use the below hardcoded values since ShipAddressId is almost always null
					await tbl.InvoiceCalculatePrices(item.ID, "UT", "84097");

					//@HACK: CellPackageItemId must be inferred from invoice items...
					if (item.InvoiceTypeId == AE_InvoiceType.MetaData.SetupandInstallationID)
					{
						var acct = await db.MS_Accounts.ByIdAsync(item.AccountId).ConfigureAwait(false);
						string cellPackageItemId = null;
						foreach (var invItem in item.InvoiceItems)
						{
							if (!invItem.IsDeleted && invItem.ItemId.StartsWith("CELL_SRV"))
							{
								cellPackageItemId = invItem.ItemId;
								break;
							}
						}

						if (acct.CellPackageItemId != cellPackageItemId)
						{
							var snapShot = Snapshotter.Start(acct);
							acct.CellPackageItemId = cellPackageItemId;
							acct.CellularTypeId = (cellPackageItemId == null) ? null : "CELLPRI"; // ????????
							await db.MS_Accounts.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
						}
					}

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				// Return invoice & items
				result.Value = AeInvoice.FromDb(item, true);
				return result;
			}
		}
		private async Task<bool> SaveInvoiceItemsAsync<T>(DBase db, Result<T> result, long invoiceId, List<AE_InvoiceItem> items, List<AeInvoiceItem> inputItems)
		{
			var tbl = db.AE_InvoiceItems;

			// Loop items to save
			foreach (var inputItem in inputItems)
			{
				AE_InvoiceItem item;
				if (inputItem.ID <= 0)
				{
					// create new
					item = new AE_InvoiceItem();
					inputItem.ToDb(item);
					item.InvoiceId = invoiceId;
					await tbl.InsertAsync(item, _gpEmployeeId);
					// add to list
					items.Add(item);
				}
				else
				{
					// find item
					item = items.Where(a => a.ID == inputItem.ID).FirstOrDefault();
					if (item == null)
					{
						result.Fail(-1, "Invalid Invoice Item ID");
						return false;
					}
					// check ModifiedOn matches input
					if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
					{
						result.Fail(-1, "Invoice Item(" + inputItem.ID + "): " + result.Message);
						return false;
					}

					// update item
					var snapShot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					item.InvoiceId = invoiceId;
					await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
				}
			}

			return true;
		}
	}
}
