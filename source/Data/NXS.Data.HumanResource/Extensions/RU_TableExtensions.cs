using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.HumanResource
{
	using AR = RU_Recruit;
	using ARCollection = IEnumerable<RU_Recruit>;
	using ARTable = DBase.RU_RecruitTable;
	public static class RU_TableExtensions
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

		public static async Task<IEnumerable<PossibleReportTo>> PossibleReportTos(this ARTable tbl, int seasonID, int userTypeID)
		{
			return await tbl.Db.Sprocs.RU_RecruitsGetPossibleReportTos<PossibleReportTo>(seasonID, userTypeID).ConfigureAwait(false);
		}

		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			//var AeC = tbl.Db.AE_Contracts;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				//, AeC.Star
			).From(tbl).With(with);
			//.LeftOuterJoin(AeC).On(AeC.ContractID, Comparison.Equals, tbl.ContractId, literalText: true);
		}
		private static async Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			//var AeC = tbl.Db.AE_Contracts;

			//var list = await tbl.Db.QueryAsync<AR, AE_Contract, AR>(sql.Sql, param: sql.Params,
			//	splitOn: new string[] { AeC.ContractID },
			//	map: (item, contract) =>
			//	{
			//		item.Contract = contract;
			//		return item;
			//	}
			//);
			var list = (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false));
			return list;
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
