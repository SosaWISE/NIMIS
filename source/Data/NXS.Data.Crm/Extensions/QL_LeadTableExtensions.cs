using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	using AR = QL_Lead;
	using ARCollection = IEnumerable<QL_Lead>;
	using ARTable = DBase.QL_LeadTable;
	public static class QL_LeadTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			//item.ModifiedOn = 
			item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy = 
			item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		//{
		//	var item = snapShot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		//}

		private static Sequel MasterFileLeadSql(this ARTable tbl, long cmfid, string top = null)
		{
			var L = tbl.Db.QL_Leads;
			var ML = tbl.Db.QL_CustomerMasterLeads;

			var sql = Sequel.NewSelect().Top(top)
			.Columns(
				L.Star
			).From(L)
			.InnerJoin(ML)
			.On(L.LeadID, Comparison.Equals, ML.LeadId, literalText: true)
			.Where(ML.CustomerMasterFileId, Comparison.Equals, cmfid);
			return sql;
		}

		public static async Task<AR> MasterFileLeadAsync(this ARTable tbl, long cmfid, string customerTypeId)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var sql = tbl.MasterFileLeadSql(cmfid, "1");
			if (string.Compare(customerTypeId, "PRI", System.StringComparison.OrdinalIgnoreCase) == 0)
				sql.And(ML.CustomerTypeId, Comparison.In, new string[] { customerTypeId, "LEAD" });
			else
				sql.And(ML.CustomerTypeId, Comparison.Equals, customerTypeId);
			sql.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return (await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
		}

		public static Task<ARCollection> MasterFileLeadsAsync(this ARTable tbl, long cmfid)
		{
			var ML = tbl.Db.QL_CustomerMasterLeads;
			var sql = tbl.MasterFileLeadSql(cmfid)
				.OrderBy(ML.CustomerTypeId.ASC()); // PRI before LEAD

			return tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params);
		}
	}
}
