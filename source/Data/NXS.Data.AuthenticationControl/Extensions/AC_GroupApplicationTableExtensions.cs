﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_GroupApplication;
	using ARCollection = IEnumerable<AC_GroupApplication>;
	using ARTable = DBase.AC_GroupApplicationTable;
	public static class AC_GroupApplicationTableExtensions
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

		public static ARCollection AllActive(this ARTable tbl)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl)
				.WhereActiveAndNotDeleted();
			return tbl.Db.Query<AR>(sql.Sql, sql.Params);
		}

		public static Task<ARCollection> ByGroupNameWithUpdateLockFullAsync(this ARTable tbl, string groupName)
		{
			// load and lock row so we're the exclusive editors/readers
			var sql = tbl.SelectFull(with: "UPDLOCK,ROWLOCK")
				.Where(tbl.GroupName, Comparison.Equals, groupName);
			return tbl.LoadManyFull(sql);
		}

		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
			).From(tbl).With(with);
		}
		private static Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
