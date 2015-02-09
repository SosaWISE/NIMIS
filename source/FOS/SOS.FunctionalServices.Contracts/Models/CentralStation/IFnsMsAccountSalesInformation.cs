using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountSalesInformation
	{
		[DataMember]
		long AccountID { get; }

		[DataMember]
		string PaymentTypeId { get; }

		[DataMember]
		short? BillingDay { get; }

		[DataMember]
		string CurrentMonitoringStation { get; }

		[DataMember]
		string PanelTypeId { get; }

		[DataMember]
		string PanelItemId { get; }

		[DataMember]
		bool? IsTakeOver { get; }

		[DataMember]
		bool? IsOwner { get; }

		[DataMember]
		string CellPackageItemId { get; }

		[DataMember]
		string CellularTypeId { get; }

		[DataMember]
		string CellularTypeName { get; }

		[DataMember]
		string CellServicePackage { get; }

		[DataMember]
		string CellularVendor { get; set; }

		[DataMember]
		decimal? SetupFee { get; }

		[DataMember]
		decimal? SetupFee1StMonth { get; }

		[DataMember]
		decimal? MMR { get; }

		[DataMember]
		bool? Over3Months { get; }
		
		[DataMember]
		short? ContractLength { get; }

		[DataMember]
		int? ContractId { get; }

		[DataMember]
		int? ContractTemplateId { get; }

		[DataMember]
		string Email { get; }

		[DataMember]
		bool? IsMoni { get; }

	}
}