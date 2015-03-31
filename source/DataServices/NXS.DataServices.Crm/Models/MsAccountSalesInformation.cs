//using NXS.Data.Crm;
//using System;
//
//namespace NXS.DataServices.Crm.Models
//{
//	public class MsAccountSalesInformation
//	{
//		public string PaymentTypeId { get; set; }
//		public string FriendsAndFamilyTypeId { get; set; }
//		public long? AccountSubmitId { get; set; }
//		public string AccountCancelReasonId { get; set; }
//		public string TechId { get; set; }
//		public string SalesRepId { get; set; }
//		public long? AccountFundingStatusId { get; set; }
//		public short BillingDay { get; set; }
//		public string Email { get; set; }
//		public bool IsMoni { get; set; }
//		public bool IsTakeOver { get; set; }
//		public bool IsOwner { get; set; }
//		public DateTime? InstallDate { get; set; }
//		public DateTime? SubmittedToCSDate { get; set; }
//		public string CsConfirmationNumber { get; set; }
//		public string CsTwoWayConfNumber { get; set; }
//		public DateTime? SubmittedToGPDate { get; set; }
//		public DateTime? ContractSignedDate { get; set; }
//		public DateTime? CancelDate { get; set; }
//		public string AMA { get; set; }
//		public string NOC { get; set; }
//		public string SOP { get; set; }
//		public DateTime? ApprovedDate { get; set; }
//		public string ApproverID { get; set; }
//		public DateTime? NOCDate { get; set; }
//		public bool IsActive { get; set; }
//		public bool IsDeleted { get; set; }
//		public DateTime ModifiedOn { get; set; }
//		public string ModifiedBy { get; set; }
//		public DateTime CreatedOn { get; set; }
//		public string CreatedBy { get; set; }
//
//		internal static MsAccountSalesInformation FromDb(MS_AccountSalesInformation item, bool nullable = false)
//		{
//			if (item == null)
//			{
//				if (nullable)
//					return null;
//				else
//					throw new Exception("AccountSalesInformation is null");
//			}
//
//			var result = new MsAccountSalesInformation();
//			result.PaymentTypeId = item.PaymentTypeId;
//			result.FriendsAndFamilyTypeId = item.FriendsAndFamilyTypeId;
//			result.AccountSubmitId = item.AccountSubmitId;
//			result.AccountCancelReasonId = item.AccountCancelReasonId;
//			result.TechId = item.TechId;
//			result.SalesRepId = item.SalesRepId;
//			result.AccountFundingStatusId = item.AccountFundingStatusId;
//			result.BillingDay = item.BillingDay;
//			result.Email = item.Email;
//			result.IsMoni = item.IsMoni;
//			result.IsTakeOver = item.IsTakeOver;
//			result.IsOwner = item.IsOwner;
//			result.InstallDate = item.InstallDate;
//			result.SubmittedToCSDate = item.SubmittedToCSDate;
//			result.CsConfirmationNumber = item.CsConfirmationNumber;
//			result.CsTwoWayConfNumber = item.CsTwoWayConfNumber;
//			result.SubmittedToGPDate = item.SubmittedToGPDate;
//			result.ContractSignedDate = item.ContractSignedDate;
//			result.CancelDate = item.CancelDate;
//			result.AMA = item.AMA;
//			result.NOC = item.NOC;
//			result.SOP = item.SOP;
//			result.ApprovedDate = item.ApprovedDate;
//			result.ApproverID = item.ApproverID;
//			result.NOCDate = item.NOCDate;
//			result.IsActive = item.IsActive;
//			result.IsDeleted = item.IsDeleted;
//			result.ModifiedOn = item.ModifiedOn;
//			result.ModifiedBy = item.ModifiedBy;
//			result.CreatedOn = item.CreatedOn;
//			result.CreatedBy = item.CreatedBy;
//			return result;
//		}
//	}
//}
