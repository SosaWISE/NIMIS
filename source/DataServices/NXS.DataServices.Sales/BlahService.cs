using NXS.Data;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NXS.Data.Sales;
using NXS.DataServices.Sales.Models;

namespace NXS.Data.Sales
{
	public partial class User
	{
		[IgnorePropertyAttribute(true)]
		public int officeId { get; set; }
		[IgnorePropertyAttribute(true)]
		public List<Perm> permissions { get; set; }
		[IgnorePropertyAttribute(true)]
		public bool hasPIN { get; set; }
	}
	public class Perm
	{
		public string permission { get; set; }
	}
}

namespace NXS.DataServices.Sales
{
	public class BlahService
	{
		string _gpEmployeeId;
		public BlahService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		// public async Task<User> UserSignInAsync(string username, string password)
		// {
		// 	using (var db = DBase.Connect())
		// 	{
		// 		// try with the password
		// 		var sql = @"SELECT users.id, firstName, lastName, email, GPID, sr.officeId,
		// 			CASE WHEN users.PIN IS NOT NULL THEN true ELSE false END AS hasPIN
		// 			FROM users
		// 			LEFT JOIN salesReps AS sr ON users.id=sr.userId
		// 			WHERE email=@username AND password=MD5(@password)
		// 			AND users.status='A'";
		// 		var user = (await db.QueryAsync<User>(sql, new { username, password }).ConfigureAwait(false)).FirstOrDefault();
		// 		if (user == null)
		// 			return null;
		// 
		// 		sql = @"SELECT permissionId AS permission FROM userPermissions WHERE userId=@id";
		// 		user.permissions = (await db.QueryAsync<string>(sql, new { user.id }).ConfigureAwait(false)).ConvertAll(p =>
		// 		{
		// 			return new Perm() { permission = p };
		// 		});
		// 		user.permissions.Add(new Perm() { permission = "BASIC" }); // everyone starts with the BASIC permission
		// 
		// 		user.hasPIN = !string.IsNullOrWhiteSpace(user.PIN);
		// 		return user;
		// 	}
		// }
		// public async Task<int> UpdatePasswordAsync(int userId, string password)
		// {
		// 	using (var db = DBase.Connect())
		// 	{
		// 		var sql = @"UPDATE users SET password=MD5(@password) WHERE id=@userId";
		// 		return await db.ExecuteAsync(sql, new { userId, password }).ConfigureAwait(false);
		// 	}
		// }
		// public async Task<int> UpdatePinAsync(int userId, string pin)
		// {
		// 	using (var db = DBase.Connect())
		// 	{
		// 		var sql = @"UPDATE users SET pin=MD5(@pin) WHERE id=@userId";
		// 		return await db.ExecuteAsync(sql, new { userId, pin }).ConfigureAwait(false);
		// 	}
		// }

		public async Task<Result<List<SlSystemType>>> SystemTypesAsync(int officeid)
		{
			using (var db = DBase.Connect())
			{
				var ST = db.SL_SystemTypes;
				var sql = Sequel.NewSelect(ST.Star).From(ST)
					.Where(ST.OfficeId, Comparison.Equals, 0).Or(ST.OfficeId, Comparison.Equals, officeid)
					.OrderBy(ST.Sequence, ST.CompanyName);
				var items = await db.QueryAsync<SL_SystemType>(sql.Sql, sql.Params).ConfigureAwait(false);
				return new Result<List<SlSystemType>>(value: items.ConvertAll(a => SlSystemType.FromDb(a)));
			}
		}
		// public async Task<List<Rep>> SalesRepsAsync(int officeid)
		// {
		// 	using (var db = DBase.Connect())
		// 	{
		// 		var sql = @"SELECT u.id, GPID, firstName, lastName, officeId, email 
		// 			FROM salesReps AS sr
		// 			INNER JOIN users AS u ON sr.userId=u.id
		// 			where sr.status='A'";
		// 		if (officeid != 0)
		// 			sql += " AND officeId=@officeid";
		// 		sql += " ORDER BY firstName, lastName ";
		// 		return (await db.QueryAsync<Rep>(sql, new { officeid }).ConfigureAwait(false)).ToList();
		// 	}
		// }
		// public async Task<List<SalesOffice>> SalesOfficesAsync()
		// {
		// 	using (var db = DBase.Connect())
		// 	{
		// 		var sql = @"SELECT id, officeCity, officeState, address FROM salesOffices where status='A'
		// 			ORDER BY officeCity, officeState";
		// 		return (await db.QueryAsync<SalesOffice>(sql).ConfigureAwait(false)).ToList();
		// 	}
		// }
		public async Task TrackLocationAsync(TrackingInput inputItem)
		{
			using (var db = DBase.Connect())
				await TrackLocationAsync(db, inputItem.Latitude, inputItem.Longitude, _gpEmployeeId);
		}
		internal static async Task TrackLocationAsync(DBase db, decimal latitude, decimal longitude, string gpEmployeeId)
		{
			var item = new SL_Tracking();
			item.Latitude = latitude;
			item.Longitude = longitude;
			item.RepCompanyID = gpEmployeeId;
			await db.SL_Trackings.InsertAsync(item, gpEmployeeId).ConfigureAwait(false);
		}
	}
}
