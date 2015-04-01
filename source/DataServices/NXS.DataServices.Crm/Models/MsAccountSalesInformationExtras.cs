using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	//@NOTE: data from MS_AccountSalesInformations that is not modified
	//		 by the sales info page/InvoiceRefresh
	public class MsAccountSalesInformationExtras
	{
		// public string PaymentTypeId { get; set; }
		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
		//public long AccountFundingStatusId { get; set; }
		// public short BillingDay { get; set; }
		// public string Email { get; set; }
		// public bool IsMoni { get; set; }
		// public bool IsTakeOver { get; set; }
		// public bool IsOwner { get; set; }
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
		// public bool IsActive { get; set; }
		// public bool IsDeleted { get; set; }
		// public DateTime ModifiedOn { get; set; }
		// public string ModifiedBy { get; set; }
		// public DateTime CreatedOn { get; set; }
		// public string CreatedBy { get; set; }


		//internal static MsAccountSalesInformationExtras FromDb(MS_AccountSalesInformation item, bool nullable = false)
		//{
		//	if (item == null)
		//	{
		//		if (nullable)
		//			return null;
		//		else
		//			throw new Exception("AccountSalesInformation is null");
		//	}
		//
		//	var result = new MsAccountSalesInformationExtras();
		//	//result.PaymentTypeId = item.PaymentTypeId;
		//	result.FriendsAndFamilyTypeId = item.FriendsAndFamilyTypeId;
		//	result.AccountSubmitId = item.AccountSubmitId;
		//	result.AccountCancelReasonId = item.AccountCancelReasonId;
		//	result.TechId = item.TechId;
		//	result.SalesRepId = item.SalesRepId;
		//	//result.AccountFundingStatusId = item.AccountFundingStatusId;
		//	//result.BillingDay = item.BillingDay;
		//	//result.Email = item.Email;
		//	//result.IsMoni = item.IsMoni;
		//	//result.IsTakeOver = item.IsTakeOver;
		//	//result.IsOwner = item.IsOwner;
		//	result.InstallDate = item.InstallDate;
		//	result.SubmittedToCSDate = item.SubmittedToCSDate;
		//	result.CsConfirmationNumber = item.CsConfirmationNumber;
		//	result.CsTwoWayConfNumber = item.CsTwoWayConfNumber;
		//	result.SubmittedToGPDate = item.SubmittedToGPDate;
		//	result.ContractSignedDate = item.ContractSignedDate;
		//	result.CancelDate = item.CancelDate;
		//	result.AMA = item.AMA;
		//	result.NOC = item.NOC;
		//	result.SOP = item.SOP;
		//	result.ApprovedDate = item.ApprovedDate;
		//	result.ApproverID = item.ApproverID;
		//	result.NOCDate = item.NOCDate;
		//	//result.IsActive = item.IsActive;
		//	//result.IsDeleted = item.IsDeleted;
		//	//result.ModifiedOn = item.ModifiedOn;
		//	//result.ModifiedBy = item.ModifiedBy;
		//	//result.CreatedOn = item.CreatedOn;
		//	//result.CreatedBy = item.CreatedBy;
		//	return result;
		//}

		internal void ToDb(MS_AccountSalesInformation item)
		{
			//item.PaymentTypeId = this.PaymentTypeId;
			item.FriendsAndFamilyTypeId = this.FriendsAndFamilyTypeId;
			item.AccountSubmitId = this.AccountSubmitId;
			item.AccountCancelReasonId = this.AccountCancelReasonId;
			item.TechId = this.TechId;
			item.SalesRepId = this.SalesRepId;
			//item.AccountFundingStatusId = this.AccountFundingStatusId;
			//item.BillingDay = this.BillingDay;
			//item.Email = this.Email;
			//item.IsMoni = this.IsMoni;
			//item.IsTakeOver = this.IsTakeOver;
			//item.IsOwner = this.IsOwner;
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
			//item.IsActive = this.IsActive;
			//item.IsDeleted = this.IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
