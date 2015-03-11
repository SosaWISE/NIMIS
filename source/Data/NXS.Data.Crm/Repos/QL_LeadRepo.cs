using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm.Repos
{
	public class QL_LeadRepo
	{
		CrmDb _db;
		public QL_LeadRepo(CrmDb db)
		{
			_db = db;
		}

		private Sequel MasterFileLeadSql(long cmfid, string top = null)
		{
			var L = _db.QL_Leads;
			var ML = _db.QL_CustomerMasterLeads;

			var qry = Sequel.Create().Select();
			if (top != null)
			{
				qry.Top("1");
			}
			qry.Columns(
				L.Star
			).From(L).WithNoLock()
			.InnerJoin(ML).WithNoLock()
			.On(L.LeadID, Comparison.Equals, ML.LeadId, literalText: true)
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid);
			return qry;
		}

		public async Task<QL_Lead> MasterFileLeadAsync(long cmfid, string customerTypeId)
		{
			var ML = _db.QL_CustomerMasterLeads;
			var qry = MasterFileLeadSql(cmfid, "1");
			if (string.Compare(customerTypeId, "PRI", System.StringComparison.OrdinalIgnoreCase) == 0)
				qry.And(ML.CustomerTypeId, Comparison.In, new string[] { customerTypeId, "LEAD" });
			else
				qry.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			qry.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return (await _db.QueryAsync<QL_Lead>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		public async Task<IEnumerable<QL_Lead>> MasterFileLeadsAsync(long cmfid)
		{
			var ML = _db.QL_CustomerMasterLeads;
			var qry = MasterFileLeadSql(cmfid)
				.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return (await _db.QueryAsync<QL_Lead>(qry.Sql, qry.Params).ConfigureAwait(false));
		}
	}
}
