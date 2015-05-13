using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = IE_Audit;
	using ARCollection = IEnumerable<IE_Audit>;
	using ARTable = DBase.IE_AuditTable;
	public static class IE_AuditTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			//if (!snapshot.HasChange()) return; // always update, even if nothing has changed
			var item = snapshot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		}

		public static async Task<AR> ByIdWithUpdateLockAsync(this ARTable tbl, int id)
		{
			var sql = Sequel.NewSelect().Top("1").Columns(tbl.Star).From(tbl)
				.Where(tbl.ID, Comparison.Equals, id);
			return (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		public static Task<IEnumerable<AR>> ByLocationIdAsync(this ARTable tbl, string locationId)
		{
			var sql = Sequel.NewSelect().Columns(tbl.Star).From(tbl)
				.WhereActiveAndNotDeleted()
				.And(tbl.LocationId, Comparison.Equals, locationId);
			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
	}
}

