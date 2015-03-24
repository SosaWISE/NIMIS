using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountSalesInformation
	{
		long AccountID { get; }
		string PaymentTypeId { get; }
		short? BillingDay { get; }
		string CurrentMonitoringStation { get; }
		string PanelTypeId { get; }
		string PanelItemId { get; }
		bool? IsTakeOver { get; }
		bool? IsOwner { get; }
		string CellPackageItemId { get; }
		string CellularTypeId { get; }
		string CellularTypeName { get; }
		string CellServicePackage { get; }
		string CellularVendor { get; set; }
		decimal? SetupFee { get; }
		decimal? SetupFee1StMonth { get; }
		decimal? MMR { get; }
		bool? Over3Months { get; }
		short? ContractLength { get; }
		int? ContractId { get; }
		int? ContractTemplateId { get; }
		string Email { get; }
		bool? IsMoni { get; }


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

		//bool IsActive { get; set; }
		//bool IsDeleted { get; set; }
		//DateTime ModifiedOn { get; set; }
		//string ModifiedBy { get; set; }
		//DateTime CreatedOn { get; set; }
		//string CreatedBy { get; set; }
	}
}