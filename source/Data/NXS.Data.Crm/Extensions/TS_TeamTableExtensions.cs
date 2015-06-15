using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NXS.Data.HumanResource;

namespace NXS.Data.Crm
{
	using AR = TS_Team;
	using ARCollection = IEnumerable<TS_Team>;
	using ARTable = DBase.TS_TeamTable;
	public static class TS_TeamExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			//item.ID = 
			await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		{
			var item = snapShot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		}

		public static Task<ARCollection> AllFullAsync(this ARTable tbl)
		{
			var sql = tbl.SelectFull();
			return tbl.LoadManyFull(sql);
		}
		public static Task<AR> ByIdFullAsync(this ARTable tbl, int id)
		{
			var sql = tbl.SelectFull()
				.Where(tbl.TeamId, Comparison.Equals, id);
			return tbl.LoadOneFull(sql);
		}
		public static Task<ARCollection> AllByRoleLocationIdFullAsync(this ARTable tbl, int roleLocationId)
		{
			var RUT = tbl.Db.HrDb.RU_Teams;

			var sql = tbl.SelectFull()
				.Where(RUT.RoleLocationId, Comparison.Equals, roleLocationId);
			return tbl.LoadManyFull(sql);
		}
		public static Task<ARCollection> AllSalesTeamsFullAsync(this ARTable tbl)
		{
			return tbl. AllByRoleLocationIdFullAsync((int)RU_RoleLocation.IDEnum.Sales);
		}


		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null)
		{
			var ADDR = tbl.Db.QL_Addresses;
			var RUT = tbl.Db.HrDb.RU_Teams;

			return (sql ?? Sequel.NewSelect()).Columns(
				RUT.TeamID
				, RUT.Description
				, tbl.Star
				, ADDR.Star
			).From(RUT)
			.LeftOuterJoin(tbl).On(RUT.TeamID, Comparison.Equals, tbl.TeamId, literalText: true)
			.LeftOuterJoin(ADDR).On(ADDR.AddressID, Comparison.Equals, tbl.AddressId, literalText: true);
		}
		private static Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var ADDR = tbl.Db.QL_Addresses;

			return tbl.Db.QueryAsync<RU_Team, AR, MC_Address, AR>(sql.Sql, param: sql.Params,
				splitOn: new string[] { tbl.TeamId, ADDR.AddressID },
				map: (team, item, address) =>
				{
					if (item == null)
					{
						item = new AR();
						item.TeamId = team.TeamID;
					}
					item.Address = address;
					item.Team = team;
					return item;
				}
			);
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
