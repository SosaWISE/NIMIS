using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Contracts
{
	public interface IAccountingEngineService : IFunctionalService
	{
		#region Search Customers
		IFnsResult<List<IFnsCustomerMasterFileGeneral>> CustomerGeneralSearch(int dealerId, string city, string stateId, string postalCode,
			string email, string firstName, string lastName, string phoneNumber, bool excludeLeads, int pageSize, int pageNumber,
			string gpEmployeeId);

		//IFnsResult<IFnsAeCustomerCardInfo> CustomerCardInfoGet(long cmfid, string gpEmployeeID);
		//IFnsResult<IFnsAeCustomerCardInfo> CustomerCardInfoGetByAccountId(long accountId, string gpEmployeeID);

		IFnsResult<object> Customer(long accountId, string customerTypeId);
		IFnsResult<object> CustomerAddress(long customerId, string customerAddressTypeId);
		#endregion Search Customers

		IFnsResult<List<IFnsAeInvoiceTemplate>> PointSystemsGet(int userId);
		IFnsResult<List<IFnsAeItem>> ActivationFeesGet(int userId);
		IFnsResult<List<IFnsMsAccountCellularType>> CellularTypesGet(int userId);
        IFnsResult<List<IFnsMsAccountServiceType>> ServiceTypesGet(int userId);
        IFnsResult<List<IFnsMsAccountPanelType>> PanelTypesGet(int userId);
		IFnsResult<List<IFnsMsVendorAlarmComPackage>> VendorAlarmComPackagesGet(int userId);
		IFnsResult<List<IFnsAeItem>> EquipmentByPointsGet(long inventoryTemplateId, int userID);
		IFnsResult<List<IFnsAeContractTemplate>> ContractLengthsGet(int invoiceTemplateId, int userId);
		IFnsResult<List<IFnsAeAging>> AgingGetByCMFID(long cmfid, string gpEmployeeID);

		#region Billing Summary Info

		IFnsResult<List<IFnsSaeBillingInfoSummary>> BillingInfoSummaryGetByCMFID(long cmfid, string gpEmployeeId);
		IFnsResult<List<IFnsSaeBillingInfoSummary>> BillingInfoSummaryGetByAccountID(long accountId, string gpEmployeeId);

		#endregion Billing Summary Info 

		#region Billing History

        //IFnsResult<List<IFnsSaeBillingHistory>> BillingHistoryGetByAccountID(long accountId, string gpEmployeeId);
		IFnsResult<List<IFnsSaeBillingHistory>> BillingHistoryGetByCMFID(long cmfid, string gpEmployeeId);

		#endregion Billing History

		#region Inventory Items

		IFnsResult<List<IFnsMsEquipmentsView>> FrequentlyInstalledEquipmentGet(string gpEmployeeId);

		#endregion Inventory Items

		#region Invoicing

		IFnsResult<IFnsAeInvoice> InvoiceGet(long invoiceId, string gpEmployeeId);

		IFnsResult<IFnsAeInvoiceHeader> InvoiceCreate(IFnsAeInvoiceHeader fnsHeader, string gpEmployeeID);

		IFnsResult<IFnsAeInvoiceItemView> InvoiceItemCreate(IFnsAeInvoiceItemView item, string gpEmployeeId);

		IFnsResult InvoiceItemDelete(long invoiceItemID, string gpEmployeeId);

		IFnsResult<IFnsAeInvoice> InvoiceAddByPartNumber(long invoiceID, string itemSku, int qty, string salesmanID, string technicianID, string gpEmployeeID);

		#endregion Invoicing

		IFnsResult<IFnsAeInvoice> RefreshMsAccountInstall(IFnsSalesSummaryProperties props, string gpEmployeeId);

		IFnsResult<IFnsAeInvoice> RefreshMsAccountInstall(long invoiceID, long accountId, string activationFeeItemId, decimal activationFeeActual,
			string monthlyMonitoringRateItemId, decimal monthlyMonitoringRateActual, string panelTypeId, string cellTypeId, bool over3Months,
			string alarmComPackageId, int dealerId, string salesmanID, string technicianID, string gpEmployeeID);

		IFnsResult<IFnsAeInvoiceMsInstallInfo> InvoiceMsIsntallsGetByAccountId(long accountId, string gpEmployeeID);

		IFnsResult<IFnsAeInvoiceMsInstallInfo> InvoiceMsIsntallsGetByInvoiceID(long invoiceID, string gpEmployeeID);

        IFnsResult<IFnsAeInvoice> InvoiceAddByBarcode(long invoiceID, string barcodeId, string salesmanID, string technicianID, string gpEmployeeID);

        IFnsResult<IFnsAeInvoiceItemView> UpdateEquipment(IFnsAeInvoiceItemView item, string gpEmployeeId);

        IFnsResult<IFnsAeInvoice> AddExistingEquipment(long invoiceID, string itemSku, int qty, string salesmanID, string technicianID, string gpEmployeeID);
	}
}