using AR = SOS.Data.SosCrm.AE_InvoiceItemsView;
using ARCollection = SOS.Data.SosCrm.AE_InvoiceItemsViewCollection;
using ARController = SOS.Data.SosCrm.AE_InvoiceItemsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_InvoiceItemViewControllerExtensions
	{

		public static ARCollection ByInvoiceId(this ARController cntlr, long invoiceId)
		{
			return cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.InvoiceId, invoiceId));
		}

		public static AR Create(this ARController cntlr, long invoiceId, string itemId, short qty, string salesmanId, string technicianId, string gpEmployeeId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceItemCreate(invoiceId, itemId, qty, salesmanId, technicianId, gpEmployeeId));
		}

		public static AR Update(this ARController cntlr, long invoiceItemId, short qty, decimal price, decimal? systemPoints, string salesmanId, string technicianId, string gpEmployeeId)
        {
	        return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceItemUpdate(invoiceItemId, qty, price, systemPoints, salesmanId, technicianId, gpEmployeeId));
        }

		public static ARCollection RefreshMsAccountInstall(this ARController cntlr, long invoiceID, long accountId, string activationFeeItemId,
			decimal activationFeeActual, string monthlyMonitoringRateItemId, decimal monthlyMonitoringRateActual, string panelTypeId, string cellTypeId,
			bool over3Months, string alarmComPackageId, int dealerId, string salesmanID, string technicianID, string gpEmployeeID)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_InvoiceItemRefreshMsAccountInstall(invoiceID, accountId, activationFeeItemId, activationFeeActual,
							monthlyMonitoringRateItemId, monthlyMonitoringRateActual, panelTypeId, cellTypeId, over3Months, alarmComPackageId, dealerId, gpEmployeeID, salesmanID, technicianID));
		}

		public static ARCollection RefreshMsAccountInstall001(this ARController cntlr, long invoiceID, long accountId, short? billingDay, string currentMonitoringStation, string email, string activationFeeItemId,
			decimal activationFeeActual, string monthlyMonitoringRateItemId, decimal monthlyMonitoringRateActual, string panelTypeId, string cellTypeId, string cellPackageItemId,
			bool over3Months, string alarmComPackageId, string paymentTypeId, bool isTakeOver, bool isOwner, bool isMoni, int contractTemplateId,
					int? contractLength, int dealerId, string salesmanID, string technicianID, string gpEmployeeID)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_InvoiceItemRefreshMsAccountInstall001(invoiceID, accountId, billingDay, currentMonitoringStation, email, activationFeeItemId, activationFeeActual,
							monthlyMonitoringRateItemId, monthlyMonitoringRateActual, panelTypeId, cellTypeId, cellPackageItemId, over3Months, alarmComPackageId, paymentTypeId, isTakeOver, isOwner, isMoni, 
							contractTemplateId, contractLength, dealerId,
							gpEmployeeID, salesmanID, technicianID));
		}

		public static ARCollection AddByPartNumber(this ARController cntlr, long invoiceID, string itemSku,
			int qty, string salesmanID, string technicianID, string gpEmployeeId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_InvoiceItemAddByPartNumber(invoiceID, itemSku, qty, salesmanID, technicianID, gpEmployeeId));
		}

        public static ARCollection AddByBarcode(this ARController cntlr, long invoiceID, string barcodeId,
            string salesmanID, string technicianID, string gpEmployeeId)
        {
            return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_InvoiceItemAddByBarcode(invoiceID, barcodeId, salesmanID, technicianID, gpEmployeeId));
        }

	    public static ARCollection AddExistingEquipment(this ARController cntlr, long invoiceID, string itemSku,
	        int qty, string salesmanID, string technicianID, string gpEmployeeId)
	    {
            return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_InvoiceItemAddExistingEquipment(invoiceID, itemSku, qty, salesmanID, technicianID, gpEmployeeId));
	    }
	}
}