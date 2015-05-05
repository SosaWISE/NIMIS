using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	using AR = QL_CustomerMasterLead;
	using ARCollection = IEnumerable<QL_CustomerMasterLead>;
	using ARTable = DBase.QL_CustomerMasterLeadTable;
	public static class QL_CustomerMasterLeadTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			//item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			//item.ID = 
			await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		{
			var item = snapShot.Value;
			//item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		}

		private static async Task<bool> CustomerMasterLeadExists(this ARTable tbl, long cmfid, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;

			var sql = Sequel.NewSelect().Top("1")
			.Columns(
				ML.Star
			).From(ML)
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid)
				.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			return (await tbl.Db.QueryAsync(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault() != null;
		}

		public static async Task<bool> AddCustomerMasterLeadAsync(this ARTable tbl, long cmfid, string customerTypeId, long leadID, string gpEmployeeId)
		{
			if (await tbl.CustomerMasterLeadExists(cmfid, customerTypeId).ConfigureAwait(false))
				return false;

			var item = new AR()
			{
				CustomerMasterLeadID = System.Guid.NewGuid(),
				CustomerMasterFileId = cmfid,
				CustomerTypeId = customerTypeId,
				LeadId = leadID,
			};
			await tbl.InsertAsync(item, gpEmployeeId).ConfigureAwait(false);
			return true;
		}
	}
}
