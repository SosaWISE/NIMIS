using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace NXS.Data.Crm
{
	using AR = MS_AccountSetupCheckList;
	using ARCollection = IEnumerable<MS_AccountSetupCheckList>;
	using ARTable = DBase.MS_AccountSetupCheckListTable;

// ReSharper disable once InconsistentNaming
	public static class MS_AccountSetupCheckListTableExtensions
	{
		public static Task<AR> ByAccountIDAndColumnNameIfNotNull(this ARTable tbl, long accountId, string columnName)
		{
			var sql = tbl.SelectFull(Sequel.NewSelect().Top("1"))
				.Where(tbl.AccountID, Comparison.Equals, accountId)
				.And(columnName, Comparison.Is, (DateTime?)null);

			return tbl.LoadOneFull(sql);
		}
		#region full load
		private static Sequel SelectFull(this ARTable tbl, Sequel sql = null, string with = null)
		{
			//var AeC = tbl.Db.AE_Contracts;

			return (sql ?? Sequel.NewSelect()).Columns(
				tbl.Star
				//, AeC.Star
			).From(tbl).With(with);
			//.LeftOuterJoin(AeC).On(AeC.ContractID, Comparison.Equals, tbl.ContractId, literalText: true);
		}
		private static async Task<ARCollection> LoadManyFull(this ARTable tbl, Sequel sql)
		{
			var list = (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false));

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
