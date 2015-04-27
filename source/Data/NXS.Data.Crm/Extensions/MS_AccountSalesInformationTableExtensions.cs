using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	using AR = MS_AccountSalesInformation;
	using ARTable = CrmDb.MS_AccountSalesInformationTable;
	public static class MS_AccountSalesInformationTableExtensions
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
			if (!snapShot.HasChange()) return;
			var item = snapShot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		}

		public static async Task<AR> EnsureByIdAsync(this ARTable tbl, long accountId, string gpEmployeeId)
		{
			var item = await tbl.ByIdAsync(accountId).ConfigureAwait(false);
			if (item == null)
			{
				item = new MS_AccountSalesInformation();
				item.ID = accountId;
				await tbl.InsertAsync(item, gpEmployeeId).ConfigureAwait(false);
			}
			return item;
		}
	}
}
