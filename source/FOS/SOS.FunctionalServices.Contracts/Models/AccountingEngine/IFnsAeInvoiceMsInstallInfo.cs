using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeInvoiceMsInstallInfo
	{
		#region Properties
		[DataMember]
		long InvoiceID { get; }

		[DataMember]
		long AccountId { get; }

		[DataMember]
		string ActivationFeeItemId { get; }

		[DataMember]
		decimal? ActivationFee { get; }

		[DataMember]
		decimal? ActivationFeeActual { get; }

		[DataMember]
		string MonthlyMonitoringRateItemId { get; }

		[DataMember]
		decimal? MonthlyMonitoringRateActual { get; }

		[DataMember]
		decimal? MonthlyMonitoringRate { get; }

		[DataMember]
		string AlarmComPackageId { get; }
		
		[DataMember]
		bool? Over3Months { get; }
	
		[DataMember]
		string CellularTypeId { get; }

		[DataMember]
		string PanelTypeId { get; }

		[DataMember]
		int? ContractId { get; }

		
		#endregion Properties
	}
}