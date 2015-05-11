using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = IE_LocationType;
	using ARCollection = IEnumerable<IE_LocationType>;
	using ARTable = DBase.IE_LocationTypeTable;
	public static class IE_LocationTypeTableExtensions
	{
		//public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		//{
		//	item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = item.CreatedBy = gpEmployeeId;
		//	item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		//}
		//public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapShot, string gpEmployeeId)
		//{
		//	var item = snapShot.Value;
		//	item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
		//	item.ModifiedBy = gpEmployeeId;
		//	await tbl.UpdateAsync(item.ID, snapShot.Diff()).ConfigureAwait(false);
		//}

		public static async Task<IEnumerable<Location>> LocationsByLocationTypeIdAsync(this ARTable thisTbl, string locationTypeId)
		{
			var locationType = await thisTbl.ByIdAsync(locationTypeId).ConfigureAwait(false);
			if (locationType == null)
				return Enumerable.Empty<Location>();
			//
			var tbl = new DBase.LocationTable(thisTbl.Db, locationType.TableName);
			var sql = Sequel.NewSelect(
				locationType.FieldID.As("ID", tbl.Alias)
				, locationType.FieldName.As("Name", tbl.Alias)
			).From(tbl)
			.Where(tbl.IsActive, Comparison.Equals, true)
			.And(tbl.IsDeleted, Comparison.Equals, false);
			return (await tbl.Db.QueryAsync<Location>(sql.Sql, sql.Params).ConfigureAwait(false));
		}
	}
}

