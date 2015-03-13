using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm.Repos
{
	using AR = QL_Lead;
	using ARCollection = IEnumerable<QL_Lead>;
	using DbTable = CrmDb.QL_LeadTable;
	public static class QL_LeadTableExtensions
	{
		private static Sequel MasterFileLeadSql(this DbTable tbl, long cmfid, string top = null)
		{
			var L = tbl.Db.QL_Leads;
			var ML = tbl.Db.QL_CustomerMasterLeads;

			var qry = Sequel.Create().Select();
			if (top != null)
				qry.Top(top);
			qry.Columns(
				L.Star
			).From(L).WithNoLock()
			.InnerJoin(ML).WithNoLock()
			.On(L.LeadID, Comparison.Equals, ML.LeadId, literalText: true)
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid);
			return qry;
		}

		public static async Task<AR> MasterFileLeadAsync(this DbTable tbl, long cmfid, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var qry = tbl.MasterFileLeadSql(cmfid, "1");
			if (string.Compare(customerTypeId, "PRI", System.StringComparison.OrdinalIgnoreCase) == 0)
				qry.And(ML.CustomerTypeId, Comparison.In, new string[] { customerTypeId, "LEAD" });
			else
				qry.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			qry.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		public static Task<ARCollection> MasterFileLeadsAsync(this DbTable tbl, long cmfid)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var qry = tbl.MasterFileLeadSql(cmfid)
				.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
		}
	}
}
