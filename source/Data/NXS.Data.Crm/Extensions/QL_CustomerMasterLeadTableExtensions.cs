using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm.Repos
{
	using AR = QL_CustomerMasterLead;
	using ARCollection = IEnumerable<QL_CustomerMasterLead>;
	using DbTable = CrmDb.QL_CustomerMasterLeadTable;
	public static class QL_CustomerMasterLeadTableExtensions
	{
		private static async Task<bool> CustomerMasterLeadExists(this DbTable tbl, long cmfid, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;

			var qry = Sequel.Create()
			.Select().Top("1")
			.Columns(
				ML.Star
			).From(ML).WithNoLock()
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid)
				.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			return (await tbl.Db.QueryAsync(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault() != null;
		}

		public static async Task<bool> AddCustomerMasterLeadAsync(this DbTable tbl, long cmfid, string customerTypeId, long leadID)
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
			await tbl.Db.QL_CustomerMasterLeads.InsertNoIdAsync(item).ConfigureAwait(false);
			return true;
		}
	}
}
