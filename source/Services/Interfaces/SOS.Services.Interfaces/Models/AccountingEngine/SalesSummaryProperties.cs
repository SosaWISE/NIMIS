using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class SalesSummaryProperties : ISalesSummaryProperties
	{
		#region Properties
		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public short? BillingDay { get; set; }
		public string CurrentMonitoringStation { get; set; }
		public string ActivationFeeItemId { get; set; }
		public decimal ActivationFeeActual { get; set; }
		public string MonthlyMonitoringRateItemId { get; set; }
		public decimal MonthlyMonitoringRateActual { get; set; }
		public string PanelTypeId { get; set; }
		public string CellTypeId { get; set; }
		public string CellPackageItemId { get; set; }
		public string CellServicePackage { get; set; }
		public bool Over3Months { get; set; }
		public string AlarmComPackageId { get; set; }
		public string Email { get; set; }
		public int DealerId { get; set; }
		public string SalesmanID { get; set; }
		public string TechnicianID { get; set; }
		public string PaymentTypeId { get; set; }
		public bool IsTakeover { get; set; }
		public bool IsOwner { get; set; }
		public bool IsMoni { get; set; }
		public int ContractId { get; set; }
		public int ContractTemplateId { get; set; }
		public int? ContractLength { get; set; }

		#endregion Properties
	}

	public interface ISalesSummaryProperties
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
		string ActivationFeeItemId { get; set; }

		[DataMember]
		decimal ActivationFeeActual { get; set; }

		[DataMember]
		string MonthlyMonitoringRateItemId { get; set; }

		[DataMember]
		decimal MonthlyMonitoringRateActual { get; set; }

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
		string AlarmComPackageId { get; set; }

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
		int ContractId { get; set; }

		[DataMember]
		int ContractTemplateId { get; set; }

		[DataMember]
		int? ContractLength { get; set; }
	}
}