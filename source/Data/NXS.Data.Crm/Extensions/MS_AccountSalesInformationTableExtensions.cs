using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace NXS.Data.Crm
{
	using AR = MS_AccountSalesInformation;
	using ARTable = DBase.MS_AccountSalesInformationTable;
// ReSharper disable once InconsistentNaming
	public static class MS_AccountSalesInformationTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			//item.ID =
			await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			if (!snapshot.HasChange()) return;
			var item = snapshot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
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
