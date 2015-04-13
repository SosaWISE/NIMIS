using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = AE_Invoice;
	using ARCollection = IEnumerable<AE_Invoice>;
	using ARTable = CrmDb.AE_InvoiceTable;
	public static class AE_InvoiceTableExtensions
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

		//public static Task<ARCollection> ByAccountIdAsync(this ARTable tbl, long accountId)
		//{
		//	var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.AccountId, Comparison.Equals, accountId);
		//	return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		//}

		public static Task<AR> ByIdWithUpdateLockFullAsync(this ARTable tbl, long id)
		{
			// load all and lock row so we're the exclusive editors/readers
			// http://stackoverflow.com/a/4597023
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"), "UPDLOCK,ROWLOCK")
				.Where(tbl.InvoiceID, Comparison.Equals, id);
			return tbl.LoadOneFull(sql);
		}
		public static Task<AR> ByAccountIdAndTypeFullAsync(this ARTable tbl, long accountId, string invoiceTypeId)
		{
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"))
				.Where(tbl.AccountId, Comparison.Equals, accountId)
				.And(tbl.InvoiceTypeId, Comparison.Equals, invoiceTypeId);
			return tbl.LoadOneFull(sql);
		}
		public static async Task<AE_Invoice> CreateInvoice(this ARTable tbl, long accountId, string invoiceTypeId, string gpEmployeeId)
		{
			var item = new AE_Invoice();
			item.AccountId = accountId;
			item.InvoiceTypeId = invoiceTypeId;
			item.DocDate = DateTime.UtcNow.Date;
			await tbl.InsertAsync(item, gpEmployeeId).ConfigureAwait(false);
			//item.InvoiceItems = new List<AE_InvoiceItem>();
			return item;
		}

		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			var AeC = tbl.Db.AE_Contracts;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				, AeC.Star
			).From(tbl).With(with)
			.LeftOuterJoin(AeC).On(AeC.ContractID, Comparison.Equals, tbl.ContractId, literalText: true);
		}
		private static async Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var AeC = tbl.Db.AE_Contracts;

			var list = await tbl.Db.QueryAsync<AR, AE_Contract, AR>(sql.Sql, param: sql.Params,
				splitOn: new string[] { AeC.ContractID },
				map: (item, contract) =>
				{
					item.Contract = contract;
					return item;
				}
			);
			// load invoice items
			foreach (var item in list)
				item.InvoiceItems = await tbl.Db.AE_InvoiceItems.ByInvoiceIdAsync(item.ID).ConfigureAwait(false);
			// return invoices
			return list;
		}
		private static async Task<AR> LoadOneFull(this ARTable tbl, Sequel sql)
		{
			return (await tbl.LoadManyFull(sql).ConfigureAwait(false)).FirstOrDefault();
		}
		#endregion // full load
	}
}
