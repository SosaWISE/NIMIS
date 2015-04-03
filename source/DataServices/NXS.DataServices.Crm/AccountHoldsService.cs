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
	public class AccountHoldsService
	{
		string _gpEmployeeId;
		public AccountHoldsService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<HoldCatg1>>> Catg1s()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHoldCatg1s;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<HoldCatg1>>(value: items.ConvertAll(item => HoldCatg1.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<HoldCatg2>>> Catg2s()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHoldCatg2s;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<HoldCatg2>>(value: items.ConvertAll(item => HoldCatg2.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<Hold>>> Holds(long accountId)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;
				var items = await tbl.ByAccountIdAsync(accountId).ConfigureAwait(false);
				var result = new Result<List<Hold>>(value: items.ConvertAll(item => Hold.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<Hold>> NewHold(HoldNew holdNew)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;

				var item = new MS_AccountHold();
				holdNew.ToDb(item);
				item.IsActive = true;
				item.ModifiedOn = item.CreatedOn = DateTime.UtcNow;
				item.ModifiedBy = item.CreatedBy = _gpEmployeeId;
				item.AccountHoldID = await tbl.InsertAsync(item).ConfigureAwait(false);

				var result = new Result<Hold>(value: Hold.FromDb(item));
				return result;
			}
		}
		public async Task<Result<Hold>> FixHold(HoldFix holdFix)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;

				var item = await tbl.ByIdAsync(holdFix.AccountHoldID).ConfigureAwait(false);

				var snapShot = Snapshotter.Start(item);
				holdFix.ToDb(item);
				item.FixedBy = _gpEmployeeId;
				item.FixedOn = item.ModifiedOn = DateTime.UtcNow;
				item.ModifiedBy = _gpEmployeeId;
				await tbl.UpdateAsync(item.AccountHoldID, snapShot.Diff()).ConfigureAwait(false);

				var result = new Result<Hold>(value: Hold.FromDb(item));
				return result;
			}
		}
	}
}
