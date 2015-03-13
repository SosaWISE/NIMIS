using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_Customer;
	using ARCollection = IEnumerable<AE_Customer>;
	using DbTable = CrmDb.AE_CustomerTable;
	public static class AE_CustomerTableExtensions
	{
		private static Sequel CustomerAccountSql(this DbTable tbl, long accountId, string top = null)
		{
			var CUST = tbl.Db.AE_Customers;
			var CA = tbl.Db.AE_CustomerAccounts;

			var qry = Sequel.Create().Select();
			if (top != null)
				qry.Top(top);
			qry.Columns(
				CUST.Star
			).From(CUST).WithNoLock()
			.InnerJoin(CA).WithNoLock()
				.On(CUST.CustomerID, Comparison.Equals, CA.CustomerId, literalText: true)
			.Where(CA.AccountId, Comparison.Equals, accountId);
			return qry;
		}

		public static async Task<AR> CustomerByTypeAsync(this DbTable tbl, long accountId, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var qry = tbl.CustomerAccountSql(accountId, "1")
			.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);

			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}
		public static Task<ARCollection> CustomersByTypeAsync(this DbTable tbl, long accountId, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var qry = tbl.CustomerAccountSql(accountId)
			.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);

			return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
		}
	}
}
