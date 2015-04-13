using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_InvoiceItem;
	using ARCollection = IEnumerable<AE_InvoiceItem>;
	using ARTable = CrmDb.AE_InvoiceItemTable;
	public static class AE_InvoiceItemTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		{
			var item = snapShot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		}

		public static Task<ARCollection> ByInvoiceIdAsync(this ARTable tbl, long invoiceId)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.InvoiceId, Comparison.Equals, invoiceId);
			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
	}
}
