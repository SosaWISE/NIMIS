using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class AccountInvoicesService
	{
		string _gpEmployeeId;
		public AccountInvoicesService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		//public async Task<Result<List<Invoice>>> Invoices(long accountId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var tbl = db.AE_Invoices;
		//		var items = await tbl.ByAccountIdAsync(accountId).ConfigureAwait(false);
		//		var result = new Result<List<Invoice>>(value: items.ConvertAll(item => Invoice.FromDb(item)));
		//		return result;
		//	}
		//}

		public async Task<Result<List<AeItem>>> Items()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.AE_Items;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<AeItem>>(value: items.ConvertAll(item => AeItem.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<AeInvoice>> InstallInvoice(long accountId, bool canCreate)
		{
			using (var db = CrmDb.Connect())
			{
				var account = await db.MS_Accounts.EnsureByIdAsync(accountId, _gpEmployeeId).ConfigureAwait(false);

				var tbl = db.AE_Invoices;
				var invoiceTypeId = AE_InvoiceType.MetaData.SetupandInstallationID;
				var item = (await tbl.ByAccountIdAndTypeFullAsync(accountId, invoiceTypeId).ConfigureAwait(false));
				if (item == null && canCreate)
					item = await tbl.CreateInvoice(accountId, invoiceTypeId, _gpEmployeeId).ConfigureAwait(false);
				var result = new Result<AeInvoice>(value: AeInvoice.FromDb(item, true));
				return result;
			}
		}

		public async Task<Result<AeInvoice>> SaveInvoice(AeInvoice inputItem)
		{
			using (var db = CrmDb.Connect(360))
			{
				var result = new Result<AeInvoice>();
				AE_Invoice item = null;

				await db.TransactionAsync(async () =>
				{
					var tbl = db.AE_Invoices;

					// read and lock invoice(WITH(ROWLOCK,UPDLOCK)) and items(Full)
					item = await tbl.ByIdWithUpdateLockFullAsync(inputItem.ID).ConfigureAwait(false);
					if (item == null)
					{
						result.Fail(-1, "Invalid Invoice ID");
						return false;
					}
					// check ModifiedOn matches input
					if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
					{
						result.Fail(-1, result.Message);
						return false;
					}

					// update input items
					var invoiceItems = item.InvoiceItems.ToList();
					item.InvoiceItems = invoiceItems; // overwrite with list that we are modifying
					if (!await SaveInvoiceItemsAsync(db, result, item.ID, invoiceItems, inputItem.InvoiceItems))
						return false;

					//@TODO:// update invoice totals

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				//@TODO:// Return invoice & items
				result.Value = AeInvoice.FromDb(item, true);
				return result;
			}
		}
		private async Task<bool> SaveInvoiceItemsAsync<T>(CrmDb db, Result<T> result, long invoiceId, List<AE_InvoiceItem> items, List<AeInvoiceItem> inputItems)
		{
			var tbl = db.AE_InvoiceItems;

			// Loop items to save
			foreach (var inputItem in inputItems)
			{
				AE_InvoiceItem item;
				if (inputItem.ID <= 0)
				{
					// create new
					item = new AE_InvoiceItem();
					inputItem.ToDb(item);
					item.InvoiceId = invoiceId;
					await tbl.InsertAsync(item, _gpEmployeeId);
					// add to list
					items.Add(item);
				}
				else
				{
					// find item
					item = items.Where(a => a.ID == inputItem.ID).FirstOrDefault();
					if (item == null)
					{
						result.Fail(-1, "Invalid Invoice Item ID");
						return false;
					}
					// check ModifiedOn matches input
					if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
					{
						result.Fail(-1, result.Message);
						return false;
					}

					// update item
					var snapShot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					item.InvoiceId = invoiceId;
					await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
				}
			}

			return true;
		}
	}
}
