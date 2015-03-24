using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = MC_Address;
	using ARCollection = IEnumerable<MC_Address>;
	using ARTable = CrmDb.MC_AddressTable;
	public static class MC_AddressTableExtensions
	{
		public static async Task<AR> ByQlAddressIdAsync(this ARTable tbl, long qlAddressId)
		{
			var qry = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.QlAddressId, Comparison.Equals, qlAddressId);
			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).SingleOrDefault();
		}

		public static async Task<AR> OneByTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		{
			var qry = tbl.ByCustomerAccountTypeSql(accountId, customerTypeId, "1");
			return (await tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params).ConfigureAwait(false)).FirstOrDefault();
		}
		private static Sequel ByCustomerAccountTypeSql(this ARTable tbl, long accountId, string customerTypeId, string top = null)
		{
			var CA = tbl.Db.AE_CustomerAccounts;

			var qry = Sequel.NewSelect().Top(top)
			.Columns(tbl.Star).From(tbl)
			.InnerJoin(CA)
				.On(tbl.AddressID, Comparison.Equals, CA.AddressId, literalText: true)
			.Where(CA.AccountId, Comparison.Equals, accountId)
			.And(CA.CustomerTypeId, Comparison.Equals, customerTypeId);
			return qry;
		}
	}
}
