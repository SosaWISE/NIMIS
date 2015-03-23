using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_Customer;
	using ARCollection = IEnumerable<AE_Customer>;
	using ARTable = CrmDb.AE_CustomerTable;
	public static class AE_CustomerTableExtensions
	{
		//public static Task<ARCollection> ManyByTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		//{
		//	var qry = tbl.ByCustomerAccountSql(accountId, customerTypeId)
		//	return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
		//}
		public static async Task<AR> OneByTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		{
			var qry = tbl.ByCustomerAccountSql(accountId, customerTypeId, "1");
			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}
		private static Sequel ByCustomerAccountSql(this ARTable tbl, long accountId, string customerTypeId, string top = null)
		{
			var CA = tbl.Db.AE_CustomerAccounts;

			var qry = Sequel.NewSelect().Top(top)
			.Columns(tbl.Star).From(tbl)
			.InnerJoin(CA)
				.On(tbl.CustomerID, Comparison.Equals, CA.CustomerId, literalText: true)
			.Where(CA.AccountId, Comparison.Equals, accountId)
			.And(CA.CustomerTypeId, Comparison.Equals, customerTypeId);
			return qry;
		}

		public static async Task<AR> OneByLeadIdAsync(this ARTable tbl, long leadId)
		{
			var qry = Sequel.NewSelect(tbl.Star).From(tbl)
			.Where(tbl.LeadId, Comparison.Equals, leadId);
			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}
	}
}
