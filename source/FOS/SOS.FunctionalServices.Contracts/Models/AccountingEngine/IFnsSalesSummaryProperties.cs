using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsSalesSummaryProperties
	{
		[DataMember]
		long InvoiceID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		short? BillingDay { get; set; }

		[DataMember]
		string CurrentMonitoringStation { get; set; }

		[DataMember]
		decimal ActivationFee { get; set; }

		[DataMember]
		string ActivationFeeItemId { get; set; }

		[DataMember]
		decimal ActivationFeeActual { get; set; }

		[DataMember]
		decimal MonthlyMonitoringRate { get; set; }

		[DataMember]
		string MonthlyMonitoringRateItemId { get; set; }

		[DataMember]
		decimal MonthlyMonitoringRateActual { get; set; }

		[DataMember]
		string AlarmComPackage { get; set; }

		[DataMember]
		string PanelTypeId { get; set; }

		[DataMember]
		string CellTypeId { get; set; }

		[DataMember]
		string CellPackageItemId { get; set; }

		[DataMember]
		string CellServicePackage { get; set; }

		[DataMember]
		bool Over3Months { get; set; }

		[DataMember]
		string Email { get; set; }

		[DataMember]
		int DealerId { get; set; }

		[DataMember]
		string SalesmanID { get; set; }

		[DataMember]
		string TechnicianID { get; set; }

		[DataMember]
		string PaymentTypeId { get; set; }

		[DataMember]
		bool IsTakeover { get; set; }

		[DataMember]
		bool IsOwner { get; set; }

		[DataMember]
		bool IsMoni { get; set; }

		[DataMember]
		int ContractTemplateId { get; set; }

		[DataMember]
		int? ContractId { get; set; }

		[DataMember]
		int? ContractLength { get; set; }
	}
}