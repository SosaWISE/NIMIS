using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_CustomerAccount;
	using ARCollection = IEnumerable<AE_CustomerAccount>;
	using ARTable = CrmDb.AE_CustomerAccountTable;
	public static class AE_CustomerAccountTableExtensions
	{
		public static Task<ARCollection> ManyByAccountIdAsync(this ARTable tbl, long accountId)
		{
			var qry = tbl.SelectFull()
				.Where(tbl.AccountId, Comparison.Equals, accountId);
			return tbl.LoadManyFull(qry);
		}
		public static Task<ARCollection> ManyByAccountIdAndTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		{
			var qry = tbl.SelectFull()
				.Where(tbl.AccountId, Comparison.Equals, accountId)
				.And(tbl.CustomerTypeId, Comparison.Equals, customerTypeId);
			return tbl.LoadManyFull(qry);
		}
		//public static async Task<AR> OneByAccountIdAndTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		//{
		//	return (await tbl.ManyByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).FirstOrDefault();
		//}
		//public static Task<ARCollection> ManyByCustomerIdAndTypeAsync(this ARTable tbl, long customerId, string customerTypeId)
		//{
		//	var qry = tbl.SelectFull()
		//		.Where(tbl.CustomerId, Comparison.Equals, customerId)
		//		.And(tbl.CustomerTypeId, Comparison.Equals, customerTypeId);
		//	return tbl.LoadManyFull(qry);
		//}

		//
		// private funcs
		//

		//// default load
		//private static Sequel Select(this ARTable tbl)
		//{
		//	return Sequel.NewSelect(
		//		tbl.Star
		//	).From(tbl);
		//}
		//private static Task<ARCollection> LoadMany(this ARTable tbl, Sequel qry)
		//{
		//	return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
		//}
		//private static async Task<AR> LoadOne(this ARTable tbl, Sequel qry)
		//{
		//	return (await tbl.LoadMany(qry).ConfigureAwait(false)).FirstOrDefault();
		//}

		// full load
		private static Sequel SelectFull(this ARTable tbl)
		{
			var CUST = tbl.Db.AE_Customers;
			var McA = tbl.Db.MC_Addresses;

			return Sequel.NewSelect(
				tbl.Star
				, CUST.Star
				, McA.Star
			).From(tbl)
			.InnerJoin(CUST).On(CUST.CustomerID, Comparison.Equals, tbl.CustomerId, literalText: true)
			.LeftOuterJoin(McA).On(McA.AddressID, Comparison.Equals, tbl.AddressId, literalText: true); // AddressId is nullable
		}
		private static Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel qry)
		{
			var CUST = tbl.Db.AE_Customers;
			var McA = tbl.Db.MC_Addresses;

			//return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
			return tbl.Db.QueryAsync<AR, AE_Customer, MC_Address, AR>(qry.Sql, param: qry.Params,
				splitOn: new string[] { CUST.CustomerID, McA.AddressID },
				map: (item, customer, address) =>
				{
					item.Customer = customer;
					item.Address = address;
					return item;
				}
			);
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel qry)
		{
			return (await tbl.LoadManyFull(qry).ConfigureAwait(false)).FirstOrDefault();
		}
	}
}
