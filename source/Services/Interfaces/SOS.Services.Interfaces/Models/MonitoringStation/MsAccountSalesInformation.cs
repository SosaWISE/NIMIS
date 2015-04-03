using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountSalesInformation : IMsAccountSalesInformation
	{
		#region Properties
		public long AccountID { get; set; }
		public string PaymentTypeId { get; set; }
		public short? BillingDay { get; set; }
		public string CurrentMonitoringStation { get; set; }
		public string PanelTypeId { get; set; }
		public string PanelItemId { get; set; }
		public bool? IsTakeOver { get; set; }
		public bool? IsOwner { get; set; }
		public string CellPackageItemId { get; set; }
		public string CellServicePackage { get; set; }
		public string CellularTypeId { get; set; }
		public string CellularTypeName { get; set; }
		public string CellularVendor { get; set; }
		public decimal? SetupFee { get; set; }
		public decimal? Setup1StMonth { get; set; }
		public decimal? MMR { get; set; }
		public bool? Over3Months { get; set; }
		public short? ContractLength { get; set; }
		public int? ContractId { get; set; }
		public int? ContractTemplateId { get; set; }
		public string Email { get; set; }
		public bool? IsMoni { get; set; }

		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
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
		#endregion Properties
	}

	public interface IMsAccountSalesInformation
	{
		long AccountID { get; set; }
		string PaymentTypeId { get; set; }
		short? BillingDay { get; set; }
		string CurrentMonitoringStation { get; set; }
		string PanelTypeId { get; set; }
		string PanelItemId { get; set; }
		bool? IsTakeOver { get; set; }
		bool? IsOwner { get; set; }
		string CellPackageItemId { get; set; }
		string CellularTypeId { get; set; }
		string CellularTypeName { get; set; }
		string CellularVendor { get; set; }
		string CellServicePackage { get; set; }
		decimal? SetupFee { get; set; }
		decimal? Setup1StMonth { get; set; }
		decimal? MMR { get; set; }
		bool? Over3Months { get; set; }
		short? ContractLength { get; set; }
		int? ContractId { get; set; }
		int? ContractTemplateId { get; set; }
		string Email { get; set; }
		bool? IsMoni { get; set; }

		string FriendsAndFamilyTypeId { get; set; }
		long? AccountSubmitId { get; set; }
		string AccountCancelReasonId { get; set; }
		string TechId { get; set; }
		string SalesRepId { get; set; }
		DateTime? InstallDate { get; set; }
		DateTime? SubmittedToCSDate { get; set; }
		string CsConfirmationNumber { get; set; }
		string CsTwoWayConfNumber { get; set; }
		DateTime? SubmittedToGPDate { get; set; }
		DateTime? ContractSignedDate { get; set; }
		DateTime? CancelDate { get; set; }
		string AMA { get; set; }
		string NOC { get; set; }
		string SOP { get; set; }
		DateTime? ApprovedDate { get; set; }
		string ApproverID { get; set; }
		DateTime? NOCDate { get; set; }
		bool OptOutCorporate { get; set; }
		bool OptOutAffiliate { get; set; }
	}
}
