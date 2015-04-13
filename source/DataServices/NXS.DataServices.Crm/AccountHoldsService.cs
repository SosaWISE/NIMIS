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

		public async Task<Result<List<MsHoldCatg1>>> Catg1s()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHoldCatg1s;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MsHoldCatg1>>(value: items.ConvertAll(item => MsHoldCatg1.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<MsHoldCatg2>>> Catg2s()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHoldCatg2s;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MsHoldCatg2>>(value: items.ConvertAll(item => MsHoldCatg2.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<List<MsHold>>> Holds(long accountId)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;
				var items = await tbl.ByAccountIdAsync(accountId).ConfigureAwait(false);
				var result = new Result<List<MsHold>>(value: items.ConvertAll(item => MsHold.FromDb(item)));
				return result;
			}
		}

		public async Task<Result<MsHold>> NewHold(MsHoldNew holdNew)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;

				var item = new MS_AccountHold();
				holdNew.ToDb(item);
				item.IsActive = true;
				await tbl.InsertAsync(item, _gpEmployeeId);

				var result = new Result<MsHold>(value: MsHold.FromDb(item));
				return result;
			}
		}
		public async Task<Result<MsHold>> FixHold(MsHoldFix holdFix)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountHolds;

				var item = await tbl.ByIdAsync(holdFix.AccountHoldID).ConfigureAwait(false);

				var snapShot = Snapshotter.Start(item);
				holdFix.ToDb(item);
				item.FixedBy = _gpEmployeeId;
				item.FixedOn = item.ModifiedOn = DateTime.UtcNow;
				await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);

				var result = new Result<MsHold>(value: MsHold.FromDb(item));
				return result;
			}
		}
	}
}
