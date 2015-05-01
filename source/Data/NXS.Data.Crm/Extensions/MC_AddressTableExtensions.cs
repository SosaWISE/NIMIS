using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = MC_Address;
	using ARCollection = IEnumerable<MC_Address>;
	using ARTable = DBase.MC_AddressTable;
	public static class MC_AddressTableExtensions
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

		public static async Task<AR> ByQlAddressIdAsync(this ARTable tbl, long qlAddressId)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.QlAddressId, Comparison.Equals, qlAddressId);
			return (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false)).SingleOrDefault();
		}

		public static async Task<AR> ByTypeAsync(this ARTable tbl, long accountId, string customerTypeId)
		{
			var sql = tbl.ByCustomerAccountTypeSql(accountId, customerTypeId, "1");
			return (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
		}
		private static Sequel ByCustomerAccountTypeSql(this ARTable tbl, long accountId, string customerTypeId, string top = null)
		{
			var CA = tbl.Db.AE_CustomerAccounts;

			var sql = Sequel.NewSelect().Top(top)
			.Columns(tbl.Star).From(tbl)
			.InnerJoin(CA)
				.On(tbl.AddressID, Comparison.Equals, CA.AddressId, literalText: true)
			.Where(CA.AccountId, Comparison.Equals, accountId)
			.And(CA.CustomerTypeId, Comparison.Equals, customerTypeId);
			return sql;
		}
	}
}
