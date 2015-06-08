using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Sales
{
	using AR = SL_Area;
	using ARCollection = IEnumerable<SL_Area>;
	using ARTable = DBase.SL_AreaTable;
	public static class SL_AreaTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			if (!snapshot.HasChange()) return;
			var item = snapshot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		}

		public static void Swap<T>(ref T a, ref T b)
		{
			T tmp = a;
			a = b;
			b = a;
		}
		public static async Task<List<SL_Area>> InBoundsAsync(this ARTable tbl, string repCompanyID, int teamId, decimal minlat, decimal maxlat, decimal minlng, decimal maxlng)
		{
			using (var db = DBase.Connect())
			{
				if (maxlat < minlat)
					Swap(ref maxlat, ref minlat);
				if (maxlng < minlng)
					Swap(ref maxlng, ref minlng);

				var A = tbl;
				var U = db.HrDb.RU_Users;

				var sql = Sequel.NewSelect(A.Star).From(A)
				.LeftOuterJoin(U).On(U.GPEmployeeId, Comparison.Equals, A.RepCompanyID, literalText: true)
				.Where(A.MaxLatitude, Comparison.GreaterOrEquals, minlat).And(A.MinLatitude, Comparison.LessOrEquals, maxlat)
					.And(A.MaxLongitude, Comparison.GreaterOrEquals, minlng).And(A.MinLongitude, Comparison.LessOrEquals, maxlng)
					.And((s) => s.Compare((object)repCompanyID, Comparison.Is, null).Or(A.RepCompanyID, Comparison.Equals, repCompanyID))
					.And((s) => s.Compare((object)teamId, Comparison.Equals, 0).Or(A.TeamId, Comparison.Equals, teamId));
				return (await db.QueryAsync<SL_Area>(sql.Sql, sql.Params).ConfigureAwait(false)).ToList();
			}
		}
	}
}
