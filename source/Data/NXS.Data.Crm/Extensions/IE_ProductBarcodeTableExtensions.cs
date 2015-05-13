using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = IE_ProductBarcode;
	using ARCollection = IEnumerable<IE_ProductBarcode>;
	using ARTable = DBase.IE_ProductBarcodeTable;
	public static class IE_ProductBarcodeTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			if (!snapshot.HasChange()) return;
			var item = snapshot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		}

		public static Task<IEnumerable<IE_ProductBarcodeLast>> ProductBarcodeLastByLocationIdAsync(this ARTable tbl, string locationId)
		{
			var PBT = tbl.Db.IE_ProductBarcodeTrackings;
			var sql = ProductBarcodeLastSql(tbl)
				.Where(PBT.LocationId, Comparison.Equals, locationId);
			return tbl.Db.QueryAsync<IE_ProductBarcodeLast>(sql.Sql, sql.Params);
		}
		public static async Task<IE_ProductBarcodeLast> ProductBarcodeLastByIdAsync(this ARTable tbl, string productBarcodeId)
		{
			var sql = ProductBarcodeLastSql(tbl, "1")
				.Where(tbl.ProductBarcodeID, Comparison.Equals, productBarcodeId);
			return (await tbl.Db.QueryAsync<IE_ProductBarcodeLast>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		private static Sequel ProductBarcodeLastSql(ARTable tbl, string top = null)
		{
			var PBT = tbl.Db.IE_ProductBarcodeTrackings;
			var POI = tbl.Db.IE_PurchaseOrderItems;
			var AeI = tbl.Db.AE_Items;

			return Sequel.NewSelect().Top(top).Columns(
				tbl.ProductBarcodeID
				, PBT.ProductBarcodeTrackingTypeId
				, PBT.LocationTypeId
				, PBT.LocationId
				, PBT.AuditId
				, AeI.ItemSKU
				, AeI.ItemDesc
			).From(tbl)
			.InnerJoin(PBT)
				.On(tbl.LastProductBarcodeTrackingId, Comparison.Equals, PBT.ProductBarcodeTrackingID, literalText: true)
				//.And(tbl.ProductBarcodeID, Comparison.Equals, PBT.ProductBarcodeId, literalText: true)
			.InnerJoin(POI)
				.On(tbl.PurchaseOrderItemId, Comparison.Equals, POI.PurchaseOrderItemID, literalText: true)
			.InnerJoin(AeI)
				.On(AeI.ItemID, Comparison.Equals, POI.ItemId, literalText: true);
		}
	}
}

