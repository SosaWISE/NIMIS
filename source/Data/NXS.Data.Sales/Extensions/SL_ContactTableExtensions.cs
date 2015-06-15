using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Sales
{
	using AR = SL_Contact;
	using ARCollection = IEnumerable<SL_Contact>;
	using ARTable = DBase.SL_ContactTable;
	public static class SL_ContactTableExtensions
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
		public static async Task<List<SL_ContactView>> InBoundsAsync(this ARTable tbl, string repCompanyID/*, int teamId*/, decimal minlat, decimal maxlat, decimal minlng, decimal maxlng)
		{
			using (var db = DBase.Connect())
			{
				if (maxlat < minlat)
					Swap(ref maxlat, ref minlat);
				if (maxlng < minlng)
					Swap(ref maxlng, ref minlng);

				var C = tbl;
				var CN = tbl.Db.SL_ContactNotes;
				var CA = tbl.Db.SL_ContactAddresses;
				var CF = tbl.Db.SL_ContactFollowups;
				// HR tables
				var U = db.HrDb.RU_Users;
				//var R = db.HrDb.RU_Recruits;
				//var T = db.HrDb.RU_Teams;

				var sql = Sequel.NewSelect(
					C.Star
					, U.FullName.As("RepName")
					, CN.FirstName, CN.LastName, CN.CategoryId, CN.SystemId, CN.Note
					, CA.Address, CA.Address2, CA.City, CA.State, CA.Zip
					, CF.FollowupOn
				).From(C)
				.InnerJoin(U).On(U.GPEmployeeId, Comparison.Equals, C.RepCompanyID, literalText: true)
				.InnerJoin(CN).On(CN.ContactId, Comparison.Equals, C.ID, literalText: true)
				.LeftOuterJoin(CA).On(CA.ContactId, Comparison.Equals, C.ID, literalText: true)
				.LeftOuterJoin(CF).On(CF.ContactId, Comparison.Equals, C.ID, literalText: true);

				//if (true)
				//{
				//	sql.InnerJoin((s) =>
				//	{
				//		s.Select(
				//			U.GPEmployeeId
				//		).From(U)
				//		.InnerJoin(R).On(R.UserId, Comparison.Equals, U.UserID, literalText: true)
				//		.InnerJoin(T).On(R.TeamId, Comparison.Equals, T.TeamID, literalText: true)
				//		.Where(U.GPEmployeeId, Comparison.Equals, repCompanyID);
				//	}, U.Alias)
				//	.On(C.RepCompanyID, Comparison.Equals, U.GPEmployeeId, literalText: true);
				//}

				sql.Where(C.Latitude, Comparison.GreaterOrEquals, minlat)
				.And(C.Latitude, Comparison.LessOrEquals, maxlat)
				.And(C.Longitude, Comparison.GreaterOrEquals, minlng)
				.And(C.Longitude, Comparison.LessOrEquals, maxlng)
				.And((s) =>
				{
					s.Compare((object)repCompanyID, Comparison.Is, null).Or(C.RepCompanyID, Comparison.Equals, repCompanyID);
				});

				#region old
				// var sql = @"SELECT
				// 			c.id, c.latitude, c.longitude
				// 			, cn.salesRepId, cn.noteTimestamp, cn.firstName, cn.lastName, cn.categoryId, cn.systemId, cn.note
				// 			, ca.address, ca.address2, ca.city, ca.state, ca.zip
				// 			, cf.followupOn
				// 		FROM SL_Contacts AS c
				// 		INNER JOIN (
				// 			SELECT
				// 				cn.*
				// 				-- , u.firstName AS salesRepFirstName
				// 				-- , u.lastName AS salesRepLastName
				// 			FROM SL_ContactNotes AS cn
				// 			INNER JOIN (
				// 				SELECT
				// 					contactId
				// 					, MAX(noteTimestamp) AS latest
				// 				FROM SL_ContactNotes
				// 				GROUP BY contactId
				// 			) AS u_cn
				// 			ON
				// 				cn.contactId=u_cn.contactId
				// 				AND cn.noteTimestamp=u_cn.latest
				// 			INNER JOIN users AS u
				// 			ON
				// 				cn.salesRepId=u.id";
				// {
				// 	if (officeId != 0 || salesRepId != 0)
				// 	{
				// 		sql += @" INNER JOIN salesReps AS sr
				// 		ON
				// 			cn.salesRepId=sr.userId";
				// 		if (officeId != 0)
				// 			sql += " AND sr.officeId=@officeId";
				// 		if (salesRepId != 0)
				// 			sql += " AND sr.userId=@salesRepId";
				// 	}
				// }
				// sql += @") AS cn
				// 		ON
				// 			c.id=cn.contactId
				// 		LEFT JOIN SL_ContactAddresses AS ca
				// 		ON
				// 			c.id=ca.ContactId
				// 		LEFT JOIN SL_ContactFollowups AS cf
				// 		ON
				// 			c.id=cf.contactId
				// 		WHERE
				// 			latitude BETWEEN @minlat AND @maxlat
				// 			AND longitude BETWEEN @minlng AND @maxlng";
				//return (await db.QueryAsync(sql, new { salesRepId, officeId, minlat, minlng, maxlat, maxlng }).ConfigureAwait(false)).ToList();
				#endregion // old
				return (await db.QueryAsync<SL_ContactView>(sql.Sql, sql.Params).ConfigureAwait(false)).ToList();
			}
		}
	}
}
