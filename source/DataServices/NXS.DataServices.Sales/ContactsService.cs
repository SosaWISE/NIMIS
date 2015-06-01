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
		string _gpEmployeeId;
		public ContactsService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<SlContactCategory>>> CategoriesAsync()
		{
			using (var db = DBase.Connect())
			{
				var items = (await db.SL_ContactCategories.CategoriesByRepCompanyIDAsync(_gpEmployeeId).ConfigureAwait(false)).ToList();
				return new Result<List<SlContactCategory>>(value: items.ConvertAll(a => SlContactCategory.FromDb(a)));
			}
		}
		public async Task<Result<SlContactCategory>> SaveCategoryAsync(CategoryInput inputItem)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<SlContactCategory>();
				var tbl = db.SL_ContactCategories;

				//$_name = db_escape($name);
				//// check if the image exists
				//$filename = preg_replace("/[^a-z0-9\.\-_]/i", '', $filename);
				//if (!file_exists(IMG_PATH . '/map/markers/categories/' . $filename))
				//	throw Exception("Bad filename used for new category marker: $filename");

				inputItem.RepCompanyID = _gpEmployeeId;

				SL_ContactCategory item;
				if (inputItem.ID > 0)
				{
					item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
					if (item == null)
						return result.Fail(-1, "Invalid category id");
					var snapshot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
				}
				else
				{
					item = new SL_ContactCategory();
					inputItem.ToDb(item);
					await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
				}

				//id, name, filename
				result.Value = SlContactCategory.FromDb(item);
				return result;
			}
		}

		public async Task<Result<bool>> DeleteCategoryAsync(int id)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.SL_ContactCategories;
				var item = await tbl.ByIdAsync(id).ConfigureAwait(false);
				if (item == null)
					throw new Exception("Invalid category identifier");

				if (string.IsNullOrEmpty(item.RepCompanyID))
				{
					// this generic category can't be deleted because it's used by all users.  Instead, we can add it to this user's blacklist
					var blackItem = new SL_ContactCategoriesBlacklist();
					blackItem.CategoryId = id;
					blackItem.RepCompanyID = _gpEmployeeId;
					return new Result<bool>(value: true);
				}
				else if (item.RepCompanyID != _gpEmployeeId)
				{
					throw new Exception("You don't have permission to delete that category.");
				}

				var snapshot = Snapshotter.Start(item);
				item.IsDeleted = true;
				await tbl.UpdateAsync(snapshot, _gpEmployeeId);
				return new Result<bool>(value: true);
			}
		}

		public async Task<Result<SlContact>> SaveContactAndInfoAsync(ContactInput inputItem)
		{
			using (var db = DBase.Connect())
			{
				await BlahService.TrackLocationAsync(db, inputItem.SalesRepLatitude, inputItem.SalesRepLongitude, _gpEmployeeId).ConfigureAwait(false);

				var result = new Result<SlContact>();
				await db.TransactionAsync(async () =>
				{
					var contact = await SaveContactAsync(db, inputItem).ConfigureAwait(false);
					//@NOTE: the database schema allows for multiple Notes/Addresses/Followups per contact, but we are only allowing one of each.
					contact.Note = await SaveContactNoteAsync(db, contact, inputItem).ConfigureAwait(false);
					contact.Address = await SaveContactAddressAsync(db, contact, inputItem).ConfigureAwait(false);
					contact.Followup = await SaveContactFollowupAsync(db, contact, inputItem).ConfigureAwait(false);
					// set result value
					result.Value = SlContact.FromDb(contact);
					return true;
				});
				return result;
			}
		}

		private async Task<SL_Contact> SaveContactAsync(DBase db, ContactInput inputItem)
		{
			var tbl = db.SL_Contacts;
			SL_Contact item;
			if (inputItem.ID <= 0)
			{
				// create
				item = new SL_Contact();
				inputItem.ToDb(item);
				item.RepCompanyID = _gpEmployeeId;
				await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			}
			else
			{
				// update
				item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
				if (item == null)
					throw new Exception("Invalid contact id");
				var snapshot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
			}
			return item;
		}

		private async Task<SL_ContactNote> SaveContactNoteAsync(DBase db, SL_Contact contact, ContactInput inputItem)
		{
			var tbl = db.SL_ContactNotes;
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.ContactId, Comparison.Equals, contact.ID);
			var item = (await db.QueryAsync<SL_ContactNote>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SL_ContactNote();
				inputItem.ToDb(item);
				item.ContactId = contact.ID;
				await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
			}
			return item;
		}

		private async Task<SL_ContactAddress> SaveContactAddressAsync(DBase db, SL_Contact contact, ContactInput inputItem)
		{
			var tbl = db.SL_ContactAddresses;
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.ContactId, Comparison.Equals, contact.ID);
			var item = (await db.QueryAsync<SL_ContactAddress>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SL_ContactAddress();
				inputItem.ToDb(item);
				item.ContactId = contact.ID;
				await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
			}
			return item;
		}

		private async Task<SL_ContactFollowup> SaveContactFollowupAsync(DBase db, SL_Contact contact, ContactInput inputItem)
		{
			var tbl = db.SL_ContactFollowups;
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.ContactId, Comparison.Equals, contact.ID);
			var item = (await db.QueryAsync<SL_ContactFollowup>(sql.Sql, sql.Params).ConfigureAwait(false)).FirstOrDefault();
			if (item == null)
			{
				// create
				item = new SL_ContactFollowup();
				inputItem.ToDb(item);
				item.ContactId = contact.ID;
				await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			}
			else
			{
				// update address
				var snapshot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
			}
			return item;
		}


		public async Task<Result<List<dynamic>>> ContactsInAreaAsync(decimal minlat, decimal minlng, decimal maxlat, decimal maxlng)
		{
			using (var db = DBase.Connect())
			{
				//// validate permissions
				//if (!Permission::user_has_permission(array(Permission::BASIC, Permission::OFFICE_STATS, Permission::COMPANY_STATS), $_SESSION['userid'], $salesRepId, $officeId))
				//	throw new Exception("You ain't got permission");

				var items = await db.SL_Contacts.InAreaAsync(_gpEmployeeId, minlat, maxlat, minlng, maxlng).ConfigureAwait(false);
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
				 			FROM SL_Contacts AS c
				 			INNER JOIN SL_ContactNotes AS cn ON c.id=cn.contactId
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
				 			FROM SL_Contacts AS c
				 			INNER JOIN SL_ContactNotes AS cn ON c.id=cn.contactId
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
				 			FROM SL_Contacts AS c
				 			INNER JOIN SL_ContactNotes AS cn ON c.id=cn.contactId
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
