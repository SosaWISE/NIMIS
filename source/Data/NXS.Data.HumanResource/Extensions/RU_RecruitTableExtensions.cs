using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.HumanResource
{
	using AR = RU_Recruit;
	using ARCollection = IEnumerable<RU_Recruit>;
	using ARTable = DBase.RU_RecruitTable;
	public static class RU_RecruitTableExtensions
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

		public static async Task<IEnumerable<PossibleReportTo>> PossibleReportTos(this ARTable tbl, int seasonID, int userTypeID)
		{
			return await tbl.Db.Sprocs.RU_RecruitsGetPossibleReportTos<PossibleReportTo>(seasonID, userTypeID).ConfigureAwait(false);
		}
	}
}
