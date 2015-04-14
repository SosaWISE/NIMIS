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
	public class AccountContractService
	{
		string _gpEmployeeId;
		public AccountContractService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<AeContract>> AccountContract(long accountId)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<AeContract>();

				var account = await db.MS_Accounts.ByIdAsync(accountId).ConfigureAwait(false);
				if (account == null)
					return result.Fail(-1, "Invalid Account ID");
				if (!account.ContractId.HasValue)
					return result;

				var tbl = db.AE_Contracts;
				var item = (await tbl.ByIdAsync(account.ContractId.Value).ConfigureAwait(false));
				result.Value = AeContract.FromDb(item);
				return result;
			}
		}

		public async Task<Result<AeContract>> SaveContract(long accountId, AeContract inputItem)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<AeContract>();
				AE_Contract item = null;

				await db.TransactionAsync(async () =>
				{
					var account = await db.MS_Accounts.ByIdAsync(accountId).ConfigureAwait(false);
					if (account == null)
					{
						result.Fail(-1, "Invalid Account ID");
						return false;
					}

					var template = await db.AE_ContractTemplates.ByIdAsync(inputItem.ContractTemplateId).ConfigureAwait(false);
					if (template == null)
					{
						result.Fail(-1, "Invalid Contract Template ID");
						return false;
					}

					var tbl = db.AE_Contracts;
					if (inputItem.ID <= 0)
					{
						// create new
						item = new AE_Contract();
						item.AccountId = account.ID;
						CopyFromTemplate(item, template);
						await tbl.InsertAsync(item, _gpEmployeeId);
					}
					else
					{
						item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
						if (item == null)
						{
							result.Fail(-1, "Invalid Contract ID");
							return false;
						}
						// check ModifiedOn matches input
						if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
						{
							result.Fail(-1, "Contract(" + inputItem.ID + "): " + result.Message);
							return false;
						}

						// update item
						var snapShot = Snapshotter.Start(item);
						item.AccountId = account.ID;
						CopyFromTemplate(item, template);
						await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					if (account.ContractId != item.ID)
					{
						var snapShot = Snapshotter.Start(account);
						account.ContractId = item.ID;
						await db.MS_Accounts.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				// return contract
				result.Value = AeContract.FromDb(item, true);
				return result;
			}
		}

		private static void CopyFromTemplate(AE_Contract item, AE_ContractTemplate template)
		{
			item.ContractTemplateId = template.ID;
			item.ContractName = template.ContractName;
			item.ContractLength = template.ContractLength;
			item.MonthlyFee = template.MonthlyFee;
			item.ShortDesc = template.ShortDesc;
			item.IsActive = true;
			item.IsDeleted = false;
		}
	}
}
