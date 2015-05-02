using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = MS_AccountPackageItem;
	using ARCollection = IEnumerable<MS_AccountPackageItem>;
	using ARTable = DBase.MS_AccountPackageItemTable;
	public static class MS_AccountPackageItemTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			//item.ModifiedOn =
			item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy =
			item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		//{
		//	var item = snapShot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		//}

		public static Task<ARCollection> ByPackageIdAsync(this ARTable tbl, long packageId)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.AccountPackageId, Comparison.Equals, packageId);
			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
	}
}
