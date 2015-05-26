using NXS.Data;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NXS.Data.Sales;
using NXS.DataServices.Sales.Models;

namespace NXS.DataServices.Sales
{
	public class ContactsService
	{
		//string _gpEmployeeId;
		//public ContactsService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		public async Task<Result<List<SalesContactCategory>>> CategoriesAsync(int userId)
		{
			using (var db = DBase.Connect())
			{
				var sql = @"SELECT id, name, filename 
			 		FROM salesContactCategories
			 		WHERE (userId=0 OR userId=@userId)
			 		AND status='A'
			 		AND id NOT IN (
			 			SELECT categoryId FROM salesContactCategoriesBlacklist
			 			WHERE userId=@userId
			 		)
			 		ORDER BY sequence, name";
				var items = (await db.QueryAsync<SalesContactCategory>(sql, new { userId }).ConfigureAwait(false)).ToList();
				return new Result<List<SalesContactCategory>>(value: items);
			}
		}
		public async Task<Result<SalesContactCategory>> SaveCategoryAsync(CategoryInput inputItem)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.SalesContactCategories;

				//$_name = db_escape($name);
				//// check if the image exists
				//$filename = preg_replace("/[^a-z0-9\.\-_]/i", '', $filename);
				//if (!file_exists(IMG_PATH . '/map/markers/categories/' . $filename))
				//	throw Exception("Bad filename used for new category marker: $filename");

				SalesContactCategory item;
				if (inputItem.id > 0)
				{
					item = await tbl.ByIdAsync(inputItem.id).ConfigureAwait(false);
					if (item == null)
						return null;
					var snapshot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
				}
				else
				{
					item = new SalesContactCategory();
					inputItem.ToDb(item);
					await tbl.InsertAsync(item).ConfigureAwait(false);
				}

				//id, name, filename
				return new Result<SalesContactCategory>(value: item);
			}
		}

		public async Task<Result<bool>> DeleteCategoryAsync(int id, int userId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.SalesContactCategories;
				var item = await tbl.ByIdAsync(id).ConfigureAwait(false);
				if (item == null)
					throw new Exception("Invalid category identifier");

				if (item.userId == 0)
				{
					// this generic category can't be deleted because it's used by all users.  Instead, we can add it to this user's blacklist
					var blackItem = new SalesContactCategoriesBlacklist();
					blackItem.categoryId = id;
					blackItem.userId = userId;
					return new Result<bool>(value: true);
				}
				else if (item.userId != userId)
				{
					throw new Exception("You don't have permission to delete that category.");
				}

				await tbl.UpdateAsync(id, new { status = "X" });
				return new Result<bool>(value: true);
			}
		}

		public async Task<Result<SalesContact>> SaveContactAndInfoAsync(SalesContact contact, SalesContactNote note, SalesContactAddress address, SalesContactFollowup followup)
		{
			using (var db = DBase.Connect())
			{
				contact = await SaveContactAsync(db, contact).ConfigureAwait(false);
				if (note != null)
					await SaveContactNoteAsync(db, contact, note).ConfigureAwait(false);
				if (address != null)
					await SaveContactAddressAsync(db, contact, address).ConfigureAwait(false);
				if (followup != null)
					await SaveContactFollowupAsync(db, contact, followup).ConfigureAwait(false);
				return new Result<SalesContact>(value: contact);
			}
		}

		private async Task<SalesContact> SaveContactAsync(DBase db, SalesContact inputItem)
		{
			var tbl = db.SalesContacts;
			SalesContact item;
			if (inputItem.id <= 0)
			{
				// create
				item = new SalesContact();
				ToDb(inputItem, item);
				item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
			}
			else
			{
				// update
				item = await tbl.ByIdAsync(inputItem.id).ConfigureAwait(false);
				if (item == null)
					throw new Exception("Invalid contact id");
				var snapshot = Snapshotter.Start(item);
				ToDb(inputItem, item);
				await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
			}
			return item;
		}
		private void ToDb(SalesContact inputItem, SalesContact item)
		{
			item.contactTimestamp = DateTime.UtcNow;
			item.latitude = inputItem.latitude;
			item.longitude = inputItem.longitude;
		}

		private async Task<SalesContactNote> SaveContactNoteAsync(DBase db, SalesContact contact, SalesContactNote inputItem)
		{
			var tbl = db.SalesContactNotes;
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.contactId, Comparison.Equals, contact.id);
			var item = (await db.QueryAsync<SalesContactNote>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SalesContactNote();
				ToDb(inputItem, item);
				item.contactId = contact.id;
				item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				ToDb(inputItem, item);
				await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
			}
			return item;
		}
		private void ToDb(SalesContactNote inputItem, SalesContactNote item)
		{
			//item.contactId = contact.id;
			item.noteTimestamp = DateTime.UtcNow;
			item.salesRepId = inputItem.salesRepId;
			item.salesRepLatitude = inputItem.salesRepLatitude;
			item.salesRepLongitude = inputItem.salesRepLongitude;
			item.firstName = db_null_or_string(inputItem.firstName);
			item.lastName = db_null_or_string(inputItem.lastName);
			item.categoryId = inputItem.categoryId;
			item.systemId = inputItem.systemId;
			item.note = db_null_or_string(inputItem.note);
		}

		private async Task<SalesContactAddress> SaveContactAddressAsync(DBase db, SalesContact contact, SalesContactAddress inputItem)
		{
			var tbl = db.SalesContactAddresses;
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.salesContactId, Comparison.Equals, contact.id);
			var item = (await db.QueryAsync<SalesContactAddress>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SalesContactAddress();
				ToDb(inputItem, item);
				item.salesContactId = contact.id;
				item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				ToDb(inputItem, item);
				await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
			}
			return item;
		}
		private void ToDb(SalesContactAddress inputItem, SalesContactAddress item)
		{
			//item.salesContactId = contact.id;
			item.address = db_null_or_string(inputItem.address);
			item.address2 = db_null_or_string(inputItem.address2);
			item.city = db_null_or_string(inputItem.city);
			item.state = db_null_or_string(inputItem.state);
			item.zip = db_null_or_string(inputItem.zip);
		}

		private async Task<SalesContactFollowup> SaveContactFollowupAsync(DBase db, SalesContact contact, SalesContactFollowup inputItem)
		{
			var tbl = db.SalesContactFollowups; var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.contactId, Comparison.Equals, contact.id);
			var item = (await db.QueryAsync<SalesContactFollowup>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SalesContactFollowup();
				ToDb(inputItem, item);
				item.contactId = contact.id;
				item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				ToDb(inputItem, item);
				await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
			}
			return item;
		}
		private void ToDb(SalesContactFollowup inputItem, SalesContactFollowup item)
		{
			//item.contactId = contact.id;
			item.salesRepId = inputItem.salesRepId;
			item.status = db_null_or_string(inputItem.status);
		}

		public static string db_null_or_string(string txt)
		{
			if (string.IsNullOrEmpty(txt))
				return null;
			else
				return txt;
		}

		public async Task<Result<List<dynamic>>> ContactsInAreaAsync(int salesRepId, int officeId, double minlat, double minlng, double maxlat, double maxlng)
		{
			using (var db = DBase.Connect())
			{
				if (minlat > maxlat)
				{
					var tmp = minlat;
					minlat = maxlat;
					maxlat = tmp;
				}
				if (minlng > maxlng)
				{
					var tmp = minlng;
					minlng = maxlng;
					maxlng = tmp;
				}

				//// validate permissions
				//if (!Permission::user_has_permission(array(Permission::BASIC, Permission::OFFICE_STATS, Permission::COMPANY_STATS), $_SESSION['userid'], $salesRepId, $officeId))
				//	throw new Exception("You ain't got permission");

				var sql = @"SELECT
							c.id
							, c.latitude
							, c.longitude
							--
							, cn.salesRepId
							, cn.salesRepFirstName
							, cn.salesRepLastName
							, cn.noteTimestamp
							, cn.firstName
							, cn.lastName
							, cn.categoryId
							, cn.systemId
							, cn.note
							--
							, ca.address
							, ca.address2
							, ca.city
							, ca.state
							, ca.zip
							--
							, cf.followupTimestamp as followup
						FROM salesContacts AS c
						INNER JOIN (
							SELECT
								cn.*
								, u.firstName AS salesRepFirstName
								, u.lastName AS salesRepLastName
							FROM salesContactNotes AS cn
							INNER JOIN (
								SELECT
									contactId
									, MAX(noteTimestamp) AS latest
								FROM salesContactNotes
								GROUP BY contactId
							) AS u_cn
							ON
								cn.contactId=u_cn.contactId
								AND cn.noteTimestamp=u_cn.latest
							INNER JOIN users AS u
							ON
								cn.salesRepId=u.id";
				{
					if (officeId != 0 || salesRepId != 0)
					{
						sql += @" INNER JOIN salesReps AS sr
						ON
							cn.salesRepId=sr.userId";
						if (officeId != 0)
							sql += " AND sr.officeId=@officeId";
						if (salesRepId != 0)
							sql += " AND sr.userId=@salesRepId";
					}
				}

				sql += @") AS cn
						ON
							c.id=cn.contactId
						LEFT JOIN salesContactAddresses AS ca
						ON
							c.id=ca.salesContactId
						LEFT JOIN salesContactFollowups AS cf
						ON
							c.id=cf.contactId
						WHERE
							latitude BETWEEN @minlat AND @maxlat
							AND longitude BETWEEN @minlng AND @maxlng";

				var items = (await db.QueryAsync(sql, new { salesRepId, officeId, minlat, minlng, maxlat, maxlng }).ConfigureAwait(false)).ToList();
				return new Result<List<dynamic>>(value: items);
			}
		}

		public async Task<List<dynamic>> ContactsByHourAsync(int salesRepId, int officeId, DateTime startTimestamp, DateTime endTimestamp)
		{
			using (var db = DBase.Connect())
			{
				//// validate permissions
				//if (!Permission::user_has_permission(array(Permission::OFFICE_STATS, Permission::COMPANY_STATS), null, $salesRepId, $officeId))
				//	throw new Exception("You ain't got permission");

				string sql;
				var diff = endTimestamp - startTimestamp;
				if (diff.Days == 0)
				{
					sql = @"SELECT YEAR(cn.noteTimestamp) AS yr, MONTH(cn.noteTimestamp) AS mo, DAY(cn.noteTimestamp) AS dy, HOUR(cn.noteTimestamp) AS hr, cn.categoryId, cn.systemId, 
				 			COUNT(c.id) AS qty
				 			FROM salesContacts AS c
				 			INNER JOIN salesContactNotes AS cn ON c.id=cn.contactId
				 			INNER JOIN salesReps AS sr ON cn.salesRepId=sr.userId
				 			WHERE cn.noteTimestamp BETWEEN @startTimestamp AND @endTimestamp
				 			";
					if (salesRepId > 0)
						sql += " AND c.salesRepId=@salesRepId ";
					else if (officeId > 0)
						sql += " AND sr.officeId=@officeId ";
					sql += " GROUP BY yr, mo, dy, hr, categoryId, systemId";
				}
				else if (diff.Days <= 31)
				{
					sql = @"SELECT YEAR(cn.noteTimestamp) AS yr, MONTH(cn.noteTimestamp) AS mo, DAY(cn.noteTimestamp) AS dy, cn.categoryId, cn.systemId, 
				 			COUNT(c.id) AS qty
				 			FROM salesContacts AS c
				 			INNER JOIN salesContactNotes AS cn ON c.id=cn.contactId
				 			INNER JOIN salesReps AS sr ON cn.salesRepId=sr.userId
				 			WHERE cn.noteTimestamp BETWEEN @startTimestamp AND @endTimestamp
				 			";
					if (salesRepId > 0)
						sql += " AND c.salesRepId=@salesRepId ";
					else if (officeId > 0)
						sql += " AND sr.officeId=@officeId ";
					sql += " GROUP BY yr, mo, dy, categoryId, systemId";
				}
				else
				{
					sql = @"SELECT YEAR(cn.noteTimestamp) AS yr, MONTH(cn.noteTimestamp) AS mo, cn.categoryId, cn.systemId, 
				 			COUNT(c.id) AS qty
				 			FROM salesContacts AS c
				 			INNER JOIN salesContactNotes AS cn ON c.id=cn.contactId
				 			INNER JOIN salesReps AS sr ON cn.salesRepId=sr.userId
				 			WHERE cn.noteTimestamp BETWEEN @startTimestamp AND @endTimestamp
				 			";
					if (salesRepId > 0)
						sql += " AND c.salesRepId=@salesRepId ";
					else if (officeId > 0)
						sql += " AND sr.officeId=@officeId ";
					sql += " GROUP BY yr, mo, categoryId, systemId";
				}

				return (await db.QueryAsync(sql, new { salesRepId, officeId, startTimestamp, endTimestamp }).ConfigureAwait(false)).ToList();
			}
		}
	}
}
