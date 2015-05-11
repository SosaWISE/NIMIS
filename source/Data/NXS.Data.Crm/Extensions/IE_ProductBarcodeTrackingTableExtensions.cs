using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = IE_ProductBarcodeTracking;
	using ARCollection = IEnumerable<IE_ProductBarcodeTracking>;
	using ARTable = DBase.IE_ProductBarcodeTrackingTable;
	public static class IE_ProductBarcodeTrackingTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		//{
		//	if (!snapShot.HasChange()) return;
		//	var item = snapShot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		//}

		public static async Task<IEnumerable<ProductBarcodeLocation>> ProductBarcodeLocationByLocationIdAsync(this ARTable tbl, string locationId)
		{
			var sql = ProductBarcodeLocationSql(tbl)
				.Where(tbl.LocationID, Comparison.Equals, locationId);
			return (await tbl.Db.QueryAsync<ProductBarcodeLocation>(sql.Sql, sql.Params).ConfigureAwait(false));
		}
		public static async Task<ProductBarcodeLocation> ProductBarcodeLocationByIdAsync(this ARTable tbl, string productBarcodeId)
		{
			var sql = ProductBarcodeLocationSql(tbl, "1")
				.Where(tbl.ProductBarcodeId, Comparison.Equals, productBarcodeId);
			return (await tbl.Db.QueryAsync<ProductBarcodeLocation>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		private static Sequel ProductBarcodeLocationSql(ARTable tbl, string top = null)
		{
			var PB = tbl.Db.IE_ProductBarcodes;
			var POI = tbl.Db.IE_PurchaseOrderItems;
			var AeI = tbl.Db.AE_Items;

			return Sequel.NewSelect().Top(top).Columns(
				tbl.ProductBarcodeId
				, tbl.LocationID
				, AeI.ItemSKU
				, AeI.ItemDesc
			).From(tbl)
			.InnerJoin(PB)
				.On(tbl.ProductBarcodeTrackingID, Comparison.Equals, PB.LastProductBarcodeTrackingId, literalText: true)
				.And(tbl.ProductBarcodeId, Comparison.Equals, PB.ProductBarcodeID, literalText: true)
			.InnerJoin(POI)
				.On(PB.PurchaseOrderItemId, Comparison.Equals, POI.PurchaseOrderItemID, literalText: true)
			.InnerJoin(AeI)
				.On(AeI.ItemID, Comparison.Equals, POI.ItemId, literalText: true);
		}
	}
}

