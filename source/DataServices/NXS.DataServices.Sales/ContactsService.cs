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

		#region icons
		static readonly string[] _icons = new string[]{
			"blue-arrow.png",
			"blue-blank.png",
			"blue-check.png",
			"blue-do-not-enter.png",
			"blue-flag.png",
			"blue-frown.png",
			"blue-question.png",
			"blue-single.png",
			"blue-smile.png",
			"blue-triangle.png",
			"blue-x.png",
			"gray-arrow.png",
			"gray-blank.png",
			"gray-check.png",
			"gray-do-not-enter.png",
			"gray-flag.png",
			"gray-frown.png",
			"gray-question.png",
			"gray-single.png",
			"gray-smile.png",
			"gray-triangle.png",
			"gray-x.png",
			"green-arrow.png",
			"green-blank.png",
			"green-check.png",
			"green-do-not-enter.png",
			"green-flag.png",
			"green-frown.png",
			"green-question.png",
			"green-single.png",
			"green-smile.png",
			"green-triangle.png",
			"green-x.png",
			"purple-check.png",
			"purple-frown.png",
			"purple-single.png",
			"yellow-arrow.png",
			"yellow-blank.png",
			"yellow-check.png",
			"yellow-do-not-enter.png",
			"yellow-flag.png",
			"yellow-frown.png",
			"yellow-question.png",
			"yellow-single.png",
			"yellow-smile.png",
			"yellow-triangle.png",
			"yellow-x.png",
		};
		#endregion // icons

		public string[] CategoryIcons()
		{
			return _icons;
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
			var result = new Result<SlContactCategory>();

			if (!_icons.Contains(inputItem.Filename))
				return result.Fail(-1, "Bad filename used for category marker: `" + inputItem.Filename + "`");

			using (var db = DBase.Connect(300))
			{
				var tbl = db.SL_ContactCategories;

				await db.TransactionAsync(async () =>
				{
					SL_ContactCategory item;
					if (inputItem.ID > 0)
					{
						item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
						if (item == null)
							return result.Fail(-1, "Invalid category id");

						if (string.IsNullOrEmpty(item.RepCompanyID))
						{
							// check if the built in category has already been overridden
							//@NOTE: if not null RealID effectively become the PK
							var realItem = await tbl.ByRealIDAsync(inputItem.ID, _gpEmployeeId).ConfigureAwait(false);
							if (realItem != null)
							{
								inputItem.ID = realItem.ID;
								item = realItem;
							}
						}

						if (string.IsNullOrEmpty(item.RepCompanyID))
						{
							// blacklist the built in category and then add the new
							await BlacklistCategoryAsync(db, item.ID);
							inputItem.ID = 0;
							item = await InsertCategoryAsync(db, inputItem, item.ID);
						}
						else if (item.RepCompanyID != _gpEmployeeId)
						{
							return result.Fail(-1, "You don't have permission to edit that category.");
						}
						else
						{
							var snapshot = Snapshotter.Start(item);
							inputItem.ToDb(item);
							item.RepCompanyID = _gpEmployeeId;
							await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
						}
					}
					else
					{
						item = await InsertCategoryAsync(db, inputItem);
					}

					//id, name, filename
					result.Value = SlContactCategory.FromDb(item);
					return result;
				});
				return result;
			}
		}
		private async Task<SL_ContactCategory> InsertCategoryAsync(DBase db, CategoryInput inputItem, int? realID = null)
		{
			var tbl = db.SL_ContactCategories;
			var item = new SL_ContactCategory();
			inputItem.ToDb(item);
			item.RepCompanyID = _gpEmployeeId;
			item.RealID = realID;
			await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			return item;
		}

		public async Task<Result<bool>> DeleteCategoryAsync(int id)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<bool>();
				await db.TransactionAsync(async () =>
				{
					var tbl = db.SL_ContactCategories;
					var item = await tbl.ByIdAsync(id).ConfigureAwait(false);
					if (item == null)
						return result.Fail(-1, "Invalid category id");

					if (string.IsNullOrEmpty(item.RepCompanyID))
					{
						await BlacklistCategoryAsync(db, id);
						result.Value = true;
						return result;
					}
					else if (item.RepCompanyID != _gpEmployeeId)
					{
						return result.Fail(-1, "You don't have permission to delete that category.");
					}

					var snapshot = Snapshotter.Start(item);
					item.IsDeleted = true;
					await tbl.UpdateAsync(snapshot, _gpEmployeeId);
					result.Value = true;
					return result;
				});
				return result;
			}
		}
		private async Task BlacklistCategoryAsync(DBase db, int categoryId)
		{
			var tbl = db.SL_ContactCategoriesBlacklists;
			// this generic category can't be deleted because it's used by all users.  Instead, we can add it to this user's blacklist
			var item = new SL_ContactCategoriesBlacklist();
			item.CategoryId = categoryId;
			item.RepCompanyID = _gpEmployeeId;
			await tbl.InsertAsync(item).ConfigureAwait(false);
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


		public async Task<Result<List<SlContactView>>> ContactsInAreaAsync(decimal minlat, decimal minlng, decimal maxlat, decimal maxlng)
		{
			using (var db = DBase.Connect())
			{
				//// validate permissions
				//if (!Permission::user_has_permission(array(Permission::BASIC, Permission::OFFICE_STATS, Permission::COMPANY_STATS), $_SESSION['userid'], $salesRepId, $officeId))
				//	throw new Exception("You ain't got permission");

				var items = await db.SL_Contacts.InAreaAsync(_gpEmployeeId, minlat, maxlat, minlng, maxlng).ConfigureAwait(false);
				return new Result<List<SlContactView>>(value: items.ConvertAll(a => SlContactView.FromDb(a)));
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
