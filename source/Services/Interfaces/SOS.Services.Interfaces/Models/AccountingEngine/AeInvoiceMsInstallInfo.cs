using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeInvoiceMsInstallInfo : IAeInvoiceMsInstallInfo
	{
		#region Properties
		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public string ActivationFeeItemId { get; set; }
		public decimal? ActivationFee { get; set; }
		public decimal? ActivationFeeActual { get; set; }
		public string MonthlyMonitoringRateItemId { get; set; }
		public decimal? MonthlyMonitoringRateActual { get; set; }
		public decimal? MonthlyMonitoringRate { get; set; }
		public string AlarmComPackageId { get; set; }
		public bool? Over3Months { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public int? ContractTemplateId { get; set; }

		#endregion Properties
	}

	public interface IAeInvoiceMsInstallInfo
	{
		#region Properties

		[DataMember]
		long InvoiceID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string ActivationFeeItemId { get; set; }

		[DataMember]
		decimal? ActivationFeeActual { get; set; }

		[DataMember]
		decimal? ActivationFee { get; set; }

		[DataMember]
		string MonthlyMonitoringRateItemId { get; set; }

		[DataMember]
		decimal? MonthlyMonitoringRateActual { get; set; }

		[DataMember]
		decimal? MonthlyMonitoringRate { get; set; }

		[DataMember]
		string AlarmComPackageId { get; }

		[DataMember]
		bool? Over3Months { get; }

		[DataMember]
		string CellularTypeId { get; }

		[DataMember]
		string PanelTypeId { get; }

		[DataMember]
		int? ContractTemplateId { get; }

		#endregion Properties
	}
}
