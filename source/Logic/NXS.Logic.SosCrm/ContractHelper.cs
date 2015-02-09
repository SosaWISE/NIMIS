using System;
using SOS.Data.SosCrm;

namespace NXS.Logic.SosCrm
{
	public static class ContractHelper
	{
		private static readonly int DefaultContractLength = 39;

		private static MC_ContractRenewalType.ContractRenewalTypeEnum GetRenewalType(MS_Account account)
		{
			return MC_ContractRenewalType.ContractRenewalTypeEnum.Annual;
		}

		public static void CreateOrUpdateContract(int accountID, string username, string note)
		{
			MS_Account acct = InterimCrmContext.Instance.MS_Accounts.LoadByPrimaryKey(accountID);
			if (acct == null)
			{
				throw new InvalidOperationException("The account specified does not exist");
			}
			else if (acct.BillingInfo == null)
			{
				throw new InvalidOperationException("The account does not have billing info defined");
			}

			MC_Contract contract = InterimCrmContext.Instance.MC_Contracts.GetContractForAccount(accountID);
			MC_ContractTerm terms = new MC_ContractTerm();

			// Create the contract record if it doesn't exist
			if (contract == null)
			{
				contract = new MC_Contract();
				contract.AccountId = accountID;
				contract.ContractDate = acct.CreatedByDate;
				if (acct.BillingInfo.New_Terms != null)
				{
					contract.ExpirationDate = contract.ContractDate.AddMonths(acct.BillingInfo.New_Terms.Value);
				}
				else
				{
					contract.ExpirationDate = contract.ContractDate.AddMonths(DefaultContractLength);
				}
				contract.CreatedOn = DateTime.Now;
				contract.CreatedBy = username;
				contract.Save(username);

				terms.IsOriginalTerms = true;
			}

			// Create the terms
			terms.ContractId = contract.ContractID;
			terms.ContractRenewalTypeId = (int)GetRenewalType(acct);
			terms.RMR = acct.MonthlyFee ?? acct.BillingInfo.New_RMR ?? 0M;
			terms.ActivationFee = acct.ActivationFee ?? acct.BillingInfo.New_activationFee ?? 0M;
			terms.InitialTermLength = acct.BillingInfo.New_Terms ?? DefaultContractLength;
			terms.AddendumNote = note;
			terms.CreatedOn = DateTime.Now;
			terms.CreatedBy = username;
			terms.Save(username);
		}
	}
}