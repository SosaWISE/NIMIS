using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_User;
	using ARCollection = IEnumerable<AC_User>;
	using ARTable = DBase.AC_UserTable;
	public static class AC_UserTableExtensions
	{
		//public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		//{
		//	item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = item.CreatedBy = gpEmployeeId;
		//	item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		//}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		//{
		//	var item = snapShot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		//}

		public static AR ByUsername(this ARTable tbl, string username)
		{
			// load and lock row so we're the exclusive editors/readers
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"))
				.Where(tbl.Username, Comparison.Equals, username)
				.And(tbl.IsActive, Comparison.Equals, true);
			return tbl.LoadOneFull(sql);
		}


		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
			).From(tbl).With(with);
		}
		private static ARCollection LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var list = tbl.Db.Query<AR>(sql.Sql, sql.Params);
			return list;
		}
		private static AR LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return tbl.LoadManyFull(sql).FirstOrDefault();
		}
		private static Task<ARCollection> LoadManyFullAsync(this ARTable tbl, Sequel sql)
		{
			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
		private static async Task<AR> LoadOneFullAsync(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFullAsync(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
