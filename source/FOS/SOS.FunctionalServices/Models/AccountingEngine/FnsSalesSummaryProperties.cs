using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsSalesSummaryProperties : IFnsSalesSummaryProperties
	{
		#region .ctor

		public FnsSalesSummaryProperties(long invoiceID, decimal activationFee, decimal activationFeeActual,
			decimal monthlyMonitoringRate, decimal monthlyMonitoringRateActual, string alarmComPackage,
			bool over3Months)
		{
			InvoiceID = invoiceID;
			ActivationFee = activationFee;
			ActivationFeeActual = activationFeeActual;
			MonthlyMonitoringRate = monthlyMonitoringRate;
			MonthlyMonitoringRateActual = monthlyMonitoringRateActual;
			AlarmComPackage = alarmComPackage;
			Over3Months = over3Months;
		}

		public FnsSalesSummaryProperties(long invoiceID, long accountId, short? billingDay, string currentMonitoringStation, string activationFeeItemId, decimal activationFeeActual,
			string monthlyMonitoringRateItemId, decimal monthlyMonitoringRateActual, string alarmComPackage, string panelTypeId, string cellTypeId,
			string cellPackageItemId, string cellServicePackage, bool over3Months, string email, string paymentTypeId, bool isTakeOver, bool isOwner, bool isMoni, int contractTemplateId, int? contractId, int? contractLength)
		{
			InvoiceID = invoiceID;
			AccountId = accountId;
			BillingDay = billingDay;
			CurrentMonitoringStation = currentMonitoringStation;
			ActivationFeeItemId = activationFeeItemId;
			ActivationFeeActual = activationFeeActual;
			MonthlyMonitoringRateItemId = monthlyMonitoringRateItemId;
			MonthlyMonitoringRateActual = monthlyMonitoringRateActual;
			AlarmComPackage = alarmComPackage;
			PanelTypeId = panelTypeId;
			CellTypeId = cellTypeId;
			CellPackageItemId = cellPackageItemId;
			CellServicePackage = cellServicePackage;
			Over3Months = over3Months;
			Email = email;
			PaymentTypeId = paymentTypeId;
			IsTakeover = isTakeOver;
			IsOwner = isOwner;
			IsMoni = isMoni;
			ContractTemplateId = contractTemplateId;
			ContractId = contractId;
			ContractLength = contractLength;
		}

		#endregion .ctor

		#region Properties

		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public short? BillingDay { get; set; }
		public string CurrentMonitoringStation { get; set; }
		public decimal ActivationFee { get; set; }
		public string ActivationFeeItemId { get; set; }
		public decimal ActivationFeeActual { get; set; }
		public decimal MonthlyMonitoringRate { get; set; }
		public string MonthlyMonitoringRateItemId { get; set; }
		public decimal MonthlyMonitoringRateActual { get; set; }
		public string AlarmComPackage { get; set; }
		public string PanelTypeId { get; set; }
		public string CellTypeId { get; set; }
		public string CellPackageItemId { get; set; }
		public string CellServicePackage { get; set; }
		public bool Over3Months { get; set; }
		public string Email { get; set; }
		public int DealerId { get; set; }
		public string SalesmanID { get; set; }
		public string TechnicianID { get; set; }
		public string PaymentTypeId { get; set; }
		public bool IsTakeover { get; set; }
		public bool IsOwner { get; set; }
		public bool IsMoni { get; set; }
		public int ContractTemplateId { get; set; }
		public int? ContractId { get; set; }
		public int? ContractLength { get; set; }

		#endregion Properties
	}
}