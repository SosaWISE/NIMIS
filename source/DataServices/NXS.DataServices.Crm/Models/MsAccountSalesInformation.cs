using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class MsAccountSalesInformation
	{
		public long ID { get; set; }
		public string PaymentTypeId { get; set; }
		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public int? AccountPackageId { get; set; }
		public int? PaymentMethodId { get; set; }
		public int? InitialPaymentMethodId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
		public long? AccountFundingStatusId { get; set; }
		public string AccountPayoutTypeId { get; set; }
		public short BillingDay { get; set; }
		public string Email { get; set; }
		public bool IsMoni { get; set; }
		public bool IsTakeOver { get; set; }
		public bool IsOwner { get; set; }
		public DateTime? InstallDate { get; set; }
		public DateTime? SubmittedToCSDate { get; set; }
		public string CsConfirmationNumber { get; set; }
		public string CsTwoWayConfNumber { get; set; }
		public DateTime? SubmittedToGPDate { get; set; }
		public DateTime? ContractSignedDate { get; set; }
		public DateTime? CancelDate { get; set; }
		public string AMA { get; set; }
		public string NOC { get; set; }
		public string SOP { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public string ApproverID { get; set; }
		public DateTime? NOCDate { get; set; }
		public bool OptOutCorporate { get; set; }
		public bool OptOutAffiliate { get; set; }
		public bool? Waived1stmonth { get; set; }
		public string RMRPaidInFullId { get; set; }
		public decimal? RMRIncreasePoints { get; set; }
		public string AccountCreationTypeId { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static MsAccountSalesInformation FromDb(MS_AccountSalesInformation item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("AccountSalesInformation is null");
			}

			var result = new MsAccountSalesInformation();
			result.ID = item.ID;
			result.PaymentTypeId = item.PaymentTypeId;
			result.FriendsAndFamilyTypeId = item.FriendsAndFamilyTypeId;
			result.AccountSubmitId = item.AccountSubmitId;
			result.AccountCancelReasonId = item.AccountCancelReasonId;
			result.AccountPackageId = item.AccountPackageId;
			result.PaymentMethodId = item.PaymentMethodId;
			result.InitialPaymentMethodId = item.InitialPaymentMethodId;
			result.TechId = item.TechId;
			result.SalesRepId = item.SalesRepId;
			result.AccountFundingStatusId = item.AccountFundingStatusId;
			result.AccountPayoutTypeId = item.AccountPayoutTypeId;
			result.BillingDay = item.BillingDay;
			result.Email = item.Email;
			result.IsMoni = item.IsMoni;
			result.IsTakeOver = item.IsTakeOver;
			result.IsOwner = item.IsOwner;
			result.InstallDate = item.InstallDate;
			result.SubmittedToCSDate = item.SubmittedToCSDate;
			result.CsConfirmationNumber = item.CsConfirmationNumber;
			result.CsTwoWayConfNumber = item.CsTwoWayConfNumber;
			result.SubmittedToGPDate = item.SubmittedToGPDate;
			result.ContractSignedDate = item.ContractSignedDate;
			result.CancelDate = item.CancelDate;
			result.AMA = item.AMA;
			result.NOC = item.NOC;
			result.SOP = item.SOP;
			result.ApprovedDate = item.ApprovedDate;
			result.ApproverID = item.ApproverID;
			result.NOCDate = item.NOCDate;
			result.OptOutCorporate = item.OptOutCorporate;
			result.OptOutAffiliate = item.OptOutAffiliate;
			result.Waived1stmonth = item.Waived1stmonth;
			result.RMRIncreasePoints = item.RMRIncreasePoints;
			result.RMRPaidInFullId = item.RMRPaidInFullId;
			result.AccountCreationTypeId = item.AccountCreationTypeId;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}

		internal void ToDb(MS_AccountSalesInformation item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");

			//item.ID = this.ID;
			item.PaymentTypeId = this.PaymentTypeId;
			item.FriendsAndFamilyTypeId = this.FriendsAndFamilyTypeId;
			item.AccountSubmitId = this.AccountSubmitId;
			item.AccountCancelReasonId = this.AccountCancelReasonId;
			item.AccountPackageId = this.AccountPackageId;
			//item.PaymentMethodId = this.PaymentMethodId; // cannot edit this directly
			//item.InitialPaymentMethodId = this.InitialPaymentMethodId; // cannot edit this directly
			item.TechId = this.TechId;
			item.SalesRepId = this.SalesRepId;
			item.AccountFundingStatusId = this.AccountFundingStatusId;
			item.AccountPayoutTypeId = this.AccountPayoutTypeId;
			item.BillingDay = this.BillingDay;
			item.Email = this.Email;
			item.IsMoni = this.IsMoni;
			item.IsTakeOver = this.IsTakeOver;
			item.IsOwner = this.IsOwner;
			item.InstallDate = this.InstallDate;
			item.SubmittedToCSDate = this.SubmittedToCSDate;
			item.CsConfirmationNumber = this.CsConfirmationNumber;
			item.CsTwoWayConfNumber = this.CsTwoWayConfNumber;
			item.SubmittedToGPDate = this.SubmittedToGPDate;
			item.ContractSignedDate = this.ContractSignedDate;
			item.CancelDate = this.CancelDate;
			item.AMA = this.AMA;
			item.NOC = this.NOC;
			item.SOP = this.SOP;
			item.ApprovedDate = this.ApprovedDate;
			item.ApproverID = this.ApproverID;
			item.NOCDate = this.NOCDate;
			item.OptOutCorporate = this.OptOutCorporate;
			item.OptOutAffiliate = this.OptOutAffiliate;
			item.Waived1stmonth = this.Waived1stmonth;
			item.RMRIncreasePoints = this.RMRIncreasePoints;
			item.RMRPaidInFullId = this.RMRPaidInFullId;
			item.AccountCreationTypeId = this.AccountCreationTypeId;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
