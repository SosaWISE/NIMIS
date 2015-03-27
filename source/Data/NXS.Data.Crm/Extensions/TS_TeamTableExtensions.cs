using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm.Repos
{
	using AR = TS_Team;
	using ARCollection = IEnumerable<TS_Team>;
	using ARTable = CrmDb.TS_TeamTable;
	public static class TS_TeamExtensions
	{
		public static Task<ARCollection> AllFullAsync(this ARTable tbl)
		{
			var qry = tbl.SelectFull();
			return tbl.LoadManyFull(qry);
		}
		public static Task<AR> ByIdFullAsync(this ARTable tbl, int id)
		{
			var qry = tbl.SelectFull()
				.Where(tbl.PkName, Comparison.Equals, id);
			return tbl.LoadOneFull(qry);
			//return (await tbl.LoadOneFull(qry).ConfigureAwait(false));
		}


		// full load
		private static Sequel SelectFull(this ARTable tbl)
		{
			var ADDR = tbl.Db.QL_Addresses;
			var RUT = tbl.Db.RU_Teams;

			return Sequel.NewSelect(
				RUT.TeamID
				, RUT.Description
				, tbl.Star
				, ADDR.Star
			).From(RUT)
			.LeftOuterJoin(tbl).On(RUT.TeamID, Comparison.Equals, tbl.TeamId, literalText: true)
			.LeftOuterJoin(ADDR).On(ADDR.AddressID, Comparison.Equals, tbl.AddressId, literalText: true);
		}
		private static Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel qry)
		{
			var ADDR = tbl.Db.QL_Addresses;

			return tbl.Db.QueryAsync<RU_Team, AR, MC_Address, AR>(qry.Sql, param: qry.Params,
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
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel qry)
		{
			return (await tbl.LoadManyFull(qry).ConfigureAwait(false)).FirstOrDefault();
		}
	}
}
