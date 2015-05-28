using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_UserSession;
	using ARCollection = IEnumerable<AC_UserSession>;
	using ARTable = DBase.AC_UserSessionTable;
	public static class AC_UserSessionTableExtensions
	{
		//public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		//{
		//	item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = item.CreatedBy = gpEmployeeId;
		//	item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		//}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		//{
		//	if (!snapshot.HasChange()) return;
		//	var item = snapshot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		//}

		//public static AR ByUsername(this ARTable tbl, string username)
		//{
		//	// load and lock row so we're the exclusive editors/readers
		//	var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"))
		//		.Where(tbl.Username, Comparison.Equals, username)
		//		.And(tbl.IsActive, Comparison.Equals, true);
		//	return tbl.LoadOneFull(sql);
		//}

		public static AR BySessionKey(this ARTable tbl, string sessionKey)
		{
			var sql = tbl.BySessionKeySql(sessionKey);
			return tbl.Db.Query<AR>(sql.Sql, sql.Params).FirstOrDefault();
		}

		public static bool Touch(this ARTable tbl, string sessionKey)
		{
			////@TODO: do update without first loading
			//// e.g.: UPDATE SET LastAccessedOn=@0 FROM AC_UserSessions WHERE SessionKey=@1 AND LastAccessedOn<@2 AND Terminated=0
			//var now = System.DateTime.UtcNow;
			//var sql = tbl.BySessionKeySql(sessionKey)
			//	.And(tbl.LastAccessedOn, Comparison.LessThan, now)
			//	.And(tbl.Terminated, Comparison.Equals, false);
			//var sess = tbl.Db.Query<AR>(sql.Sql, sql.Params).FirstOrDefault();
			//if (sess == null)
			//	return;
			//sess.LastAccessedOn = now;
			//tbl.Update(sess.ID, new { sess.LastAccessedOn });

			var now = System.DateTime.UtcNow;
			//@TODO: use Sequel...
			return 0 < tbl.Db.Execute("UPDATE " + tbl.TableName + " SET LastAccessedOn=@now" +
				" WHERE SessionKey=@sessionKey AND LastAccessedOn<@now AND Terminated=0", new { now, sessionKey, });
		}
		public static bool Terminate(this ARTable tbl, string sessionKey)
		{
			//@TODO: use Sequel...
			return 0 < tbl.Db.Execute("UPDATE " + tbl.TableName + " SET Terminated=1 WHERE SessionKey=@sessionKey", new { sessionKey, });
		}

		public static Sequel BySessionKeySql(this ARTable tbl, string sessionKey)
		{
			return Sequel.NewSelect().Top("1")
				.Columns(tbl.Star)
				.From(tbl)
				.Where(tbl.SessionKey, Comparison.Equals, sessionKey);
		}


		//#region full load
		//private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		//{
		//	return (sql ?? Sequel.NewSelect()).Columns(
		//		tbl.Star
		//	).From(tbl).With(with);
		//}
		//private static ARCollection LoadManyFull(this ARTable tbl, Sequel sql)
		//{
		//	var list = tbl.Db.Query<AR>(sql.Sql, sql.Params);
		//	return list;
		//}
		//private static AR LoadOneFull(this ARTable tbl, Sequel sql)
		//{
		//	return tbl.LoadManyFull(sql).FirstOrDefault();
		//}
		//private static Task<ARCollection> LoadManyFullAsync(this ARTable tbl, Sequel sql)
		//{
		//	return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		//}
		//private static async Task<AR> LoadOneFullAsync(this ARTable tbl, Sequel sql)
		//{
		//	return (await tbl.LoadManyFullAsync(sql).ConfigureAwait(false)).FirstOrDefault();
		//}
		//#endregion // full load
	}
}
