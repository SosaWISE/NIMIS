using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Sales
{
	using AR = SL_ContactCategory;
	using ARCollection = IEnumerable<SL_ContactCategory>;
	using ARTable = DBase.SL_ContactCategoryTable;
	public static class SL_ContactCategoryTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			//item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			if (!snapshot.HasChange()) return;
			var item = snapshot.Value;
			//item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			//item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		}

		public static async Task<ARCollection> CategoriesByRepCompanyIDAsync(this ARTable tbl, string repCompanyID)
		{
			// var sql = @"SELECT * FROM SL_ContactCategories
			// 	WHERE (RepCompanyID IS NULL OR RepCompanyID=@companyID)
			// 	AND IsActive=1 AND IsDeleted=0
			// 	AND id NOT IN (
			// 		SELECT categoryId FROM SL_ContactCategoriesBlacklist
			// 		WHERE RepCompanyID=@companyID
			// 	)
			// 	ORDER BY sequence, name";
			// var companyID = _gpEmployeeId;
			// var items = (await db.QueryAsync<SalesContactCategory>(sql, new { companyID }).ConfigureAwait(false)).ToList();
			// return new Result<List<SalesContactCategory>>(value: items);

			var sql = Sequel.NewSelect(tbl.Star).From(tbl)
				.WhereActiveAndNotDeleted(tbl.Alias)
				.And(s =>
				{
					s.Compare(tbl.RepCompanyID, Comparison.Is, null).Or(tbl.RepCompanyID, Comparison.Equals, repCompanyID);
				})
				.And(tbl.ID, Comparison.NotIn, s =>
				{
					var BL = tbl.Db.SL_ContactCategoriesBlacklists;
					s.Select(BL.CategoryId).From(BL).Where(BL.RepCompanyID, Comparison.Equals, repCompanyID);
				})
				.OrderBy(tbl.Sequence, tbl.Name);
			return await tbl.Db.QueryAsync<AR>(sql.Sql, sql.Params).ConfigureAwait(false);
		}
	}
}
