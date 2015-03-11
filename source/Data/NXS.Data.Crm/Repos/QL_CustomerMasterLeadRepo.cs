using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm.Repos
{
	public class QL_CustomerMasterLeadRepo
	{
		CrmDb _db;
		public QL_CustomerMasterLeadRepo(CrmDb db)
		{
			_db = db;
		}

		private async Task<bool> CustomerMasterLeadExists(long cmfid, string customerTypeId)
		{
			var ML = _db.QL_CustomerMasterLeads;

			var qry = Sequel.Create()
			.Select().Top("1")
			.Columns(
				ML.Star
			).From(ML).WithNoLock()
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid)
				.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			return (await _db.QueryAsync(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault() != null;
		}

		public async Task<bool> AddCustomerMasterLeadAsync(long cmfid, string customerTypeId, long leadID)
		{
			if (await CustomerMasterLeadExists(cmfid, customerTypeId).ConfigureAwait(false))
				return false;

			var item = new QL_CustomerMasterLead()
			{
				CustomerMasterLeadID = System.Guid.NewGuid(),
				CustomerMasterFileId = cmfid,
				CustomerTypeId = customerTypeId,
				LeadId = leadID,
			};
			await _db.QL_CustomerMasterLeads.InsertNoIdAsync(item).ConfigureAwait(false);
			return true;
		}
	}
}
