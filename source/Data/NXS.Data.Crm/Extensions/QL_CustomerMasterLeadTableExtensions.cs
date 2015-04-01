using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	using AR = QL_CustomerMasterLead;
	using ARCollection = IEnumerable<QL_CustomerMasterLead>;
	using ARTable = CrmDb.QL_CustomerMasterLeadTable;
	public static class QL_CustomerMasterLeadTableExtensions
	{
		private static async Task<bool> CustomerMasterLeadExists(this ARTable tbl, long cmfid, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;

			var qry = Sequel.NewSelect().Top("1")
			.Columns(
				ML.Star
			).From(ML)
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid)
				.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			return (await tbl.Db.QueryAsync(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault() != null;
		}

		public static async Task<bool> AddCustomerMasterLeadAsync(this ARTable tbl, long cmfid, string customerTypeId, long leadID)
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
			await tbl.Db.QL_CustomerMasterLeads.InsertAsync(item, hasIdentity: false).ConfigureAwait(false);
			return true;
		}
	}
}
