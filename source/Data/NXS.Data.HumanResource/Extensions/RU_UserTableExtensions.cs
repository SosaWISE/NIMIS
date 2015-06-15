using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.HumanResource
{
	using AR = RU_User;
	using ARCollection = IEnumerable<RU_User>;
	using ARTable = DBase.RU_UserTable;
	public static class RU_UserTableExtensions
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

		public static Task<ARCollection> AllSalesUsersFullAsync(this ARTable tbl)
		{
			var RR = tbl.Db.RU_Recruits;
			var RT = tbl.Db.RU_UserTypes;

			var sql = tbl.SelectFull()
				.Where(tbl.IsActive, Comparison.Equals, true).And(tbl.IsDeleted, Comparison.Equals, false)
				.And(RR.IsActive, Comparison.Equals, true).And(RR.IsDeleted, Comparison.Equals, false)
				.And(RT.RoleLocationID, Comparison.Equals, (int)RU_RoleLocation.IDEnum.Sales);
			return tbl.LoadManyFull(sql);
		}

		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null)
		{
			var RR = tbl.Db.RU_Recruits;
			var RT = tbl.Db.RU_UserTypes;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				, RR.Star
			).From(tbl)
			.InnerJoin(RR).On(RR.UserId, Comparison.Equals, tbl.UserID, literalText: true)
			.InnerJoin(RT).On(RT.UserTypeID, Comparison.Equals, RR.UserTypeId, literalText: true);
		}
		private static async Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var RR = tbl.Db.RU_Recruits;
			var RT = tbl.Db.RU_UserTypes;

			var dict = new Dictionary<int, AR>();
			await tbl.Db.QueryAsync<AR, RU_Recruit, AR>(sql.Sql, param: sql.Params,
				splitOn: new string[] { RR.RecruitID },
				map: (item, recruit) =>
				{
					var key = item.UserID;
					if (dict.ContainsKey(key))
						item = dict[key];
					else
					{
						item.Recruits = new List<RU_Recruit>();
						dict.Add(key, item);
					}
					item.Recruits.Add(recruit);
					return item;
				}
			);
			return dict.Values;
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
