using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_CustomerAccount;
	using ARCollection = IEnumerable<AE_CustomerAccount>;
	using ARTable = DBase.AE_CustomerAccountTable;
	public static class AE_CustomerAccountTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		{
			var item = snapShot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		}

		public static Task<ARCollection> ByAccountIdAsync(this ARTable tbl, long accountId)
		{
			var sql = tbl.SelectFull()
				.Where(tbl.AccountId, Comparison.Equals, accountId);
			return tbl.LoadManyFull(sql);
		}
		public static Task<ARCollection> ByAccountIdAndTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		{
			var sql = tbl.SelectFull()
				.Where(tbl.AccountId, Comparison.Equals, accountId)
				.And(tbl.CustomerTypeId, Comparison.Equals, customerTypeId);
			return tbl.LoadManyFull(sql);
		}
		//public static async Task<AR> OneByAccountIdAndTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		//{
		//	return (await tbl.ManyByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).FirstOrDefault();
		//}
		//public static Task<ARCollection> ManyByCustomerIdAndTypeAsync(this ARTable tbl, long customerId, string customerTypeId)
		//{
		//	var sql = tbl.SelectFull()
		//		.Where(tbl.CustomerId, Comparison.Equals, customerId)
		//		.And(tbl.CustomerTypeId, Comparison.Equals, customerTypeId);
		//	return tbl.LoadManyFull(sql);
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
		//private static Task<ARCollection> LoadMany(this ARTable tbl, Sequel sql)
		//{
		//	return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		//}
		//private static async Task<AR> LoadOne(this ARTable tbl, Sequel sql)
		//{
		//	return (await tbl.LoadMany(sql).ConfigureAwait(false)).FirstOrDefault();
		//}

		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null)
		{
			var CUST = tbl.Db.AE_Customers;
			var McA = tbl.Db.MC_Addresses;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				, CUST.Star
				, McA.Star
			).From(tbl)
			.InnerJoin(CUST).On(CUST.CustomerID, Comparison.Equals, tbl.CustomerId, literalText: true)
			.InnerJoin(McA).On(McA.AddressID, Comparison.Equals, tbl.AddressId, literalText: true);
		}
		private static Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var CUST = tbl.Db.AE_Customers;
			var McA = tbl.Db.MC_Addresses;

			return tbl.Db.QueryAsync<AR, AE_Customer, MC_Address, AR>(sql.Sql, param: sql.Params,
				splitOn: new string[] { CUST.CustomerID, McA.AddressID },
				map: (item, customer, address) =>
				{
					item.Customer = customer;
					item.Address = address;
					return item;
				}
			);
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
