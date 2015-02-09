using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeInvoiceMsInstallInfo : IFnsAeInvoiceMsInstallInfo
	{
		#region .ctor

		public FnsAeInvoiceMsInstallInfo(AE_InvoiceMsInstallInfoView item)
		{
			InvoiceID = item.InvoiceID;
			AccountId = item.AccountId;
			ActivationFeeItemId = item.ActivationFeeItemId;
			ActivationFeeActual = item.ActivationFeeActual;
			ActivationFee = item.ActivationFee;
			MonthlyMonitoringRateItemId = item.MonthlyMonitoringRateItemId;
			MonthlyMonitoringRateActual = item.MonthlyMonitoringRateActual;
			MonthlyMonitoringRate = item.MonthlyMonitoringRate;
			AlarmComPackageId = item.AlarmComPackageId;
			Over3Months = item.Over3Months;
			CellularTypeId = item.CellularTypeId;
			PanelTypeId = PanelTypeId = item.PanelTypeId;
			ContractId = item.ContractId;
		}

		#endregion .ctor

		#region Properties
		public long InvoiceID { get; private set; }
		public long AccountId { get; private set; }
		public string ActivationFeeItemId { get; private set; }
		public decimal? ActivationFeeActual { get; private set; }
		public decimal? ActivationFee { get; private set; }
		public string MonthlyMonitoringRateItemId { get; private set; }
		public decimal? MonthlyMonitoringRateActual { get; private set; }
		public decimal? MonthlyMonitoringRate { get; private set; }
		public string AlarmComPackageId { get; private set; }
		public bool? Over3Months { get; private set; }
		public string CellularTypeId { get; private set; }
		public string PanelTypeId { get; private set; }
		public int? ContractId { get; private set; }
		#endregion Properties
	}
}
