using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class AccountsService
	{
		string _gpEmployeeId;
		public AccountsService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<MsAccountSalesInformation>> AccountSalesInformation(long accountId)
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MS_AccountSalesInformations;
				var item = (await tbl.ByIdAsync(accountId).ConfigureAwait(false));
				return new Result<MsAccountSalesInformation>(value: MsAccountSalesInformation.FromDb(item, true));
			}
		}

		public async Task<Result<MsAccountSalesInformation>> SaveAccountSalesInformation(MsAccountSalesInformation inputItem)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<MsAccountSalesInformation>();
				var tbl = db.MS_AccountSalesInformations;

				var item = (await tbl.EnsureByIdAsync(inputItem.ID, _gpEmployeeId).ConfigureAwait(false));
				if (!item.IsNew())
				{
					var tmpMsg = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn);
					if (!string.IsNullOrEmpty((tmpMsg)))
					{
						result.Value = MsAccountSalesInformation.FromDb(item);
						return result.Fail((int)BaseErrorCodes.ErrorCodes.InvalidModifiedOn, tmpMsg);
					}
				}

				var snapShot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);

				result.Value = MsAccountSalesInformation.FromDb(item);
				return result;
			}
		}
	}
}
