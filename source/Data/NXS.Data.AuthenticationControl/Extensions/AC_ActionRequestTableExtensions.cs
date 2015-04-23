using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_ActionRequest;
	using ARCollection = IEnumerable<AC_ActionRequest>;
	using ARTable = AuthControlDb.AC_ActionRequestTable;
	public static class AC_ActionRequestTableExtensions
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
		public static void Update(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		{
			var item = snapShot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			tbl.Update(item.ID, snapShot.Diff());
		}

		public static AR ByActionKey(this ARTable tbl, string actionKey)
		{
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"))
				.Where(tbl.ActionKey, Comparison.Equals, actionKey);
			return tbl.LoadOneFull(sql);
		}
		public static AR ByActionKeyWithUpdateLock(this ARTable tbl, string actionKey)
		{
			// load and lock row so we're the exclusive editors/readers
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"), "UPDLOCK,ROWLOCK")
				.Where(tbl.ActionKey, Comparison.Equals, actionKey);
			return tbl.LoadOneFull(sql);
		}

		public static Task<ARCollection> SearchByOnBehalfOfAsync(this ARTable tbl, int? max, string onBehalfOf, int requestReasonId, DateTime startRange, DateTime endRange)
		{
			var sql = tbl.SelectFull(Sequel.NewSelect().Top(max.HasValue ? max.ToString() : null))
				.Where(tbl.OnBehalfOf, Comparison.Equals, onBehalfOf)
				.And(tbl.RequestReasonId, Comparison.Equals, requestReasonId)
				//.And(tbl.UsedOn, Comparison.IsNot, null) // 
				// has to be approved (signed and not denied)
				.And(tbl.SignedOn, Comparison.IsNot, null).And(tbl.DeniedReasonId, Comparison.Is, null)
				// date range
				.And(tbl.CreatedOn, Comparison.GreaterOrEquals, startRange).And(tbl.CreatedOn, Comparison.LessOrEquals, endRange);
			return tbl.LoadManyFullAsync(sql);
		}


		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			var AcU = tbl.Db.AC_Users;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				, AcU.Username
			).From(tbl).With(with)
			.InnerJoin(AcU).On(AcU.UserID, Comparison.Equals, tbl.UserId, literalText: true);
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
