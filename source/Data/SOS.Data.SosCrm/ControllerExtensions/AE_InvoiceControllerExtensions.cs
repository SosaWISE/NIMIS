using System;
using AR = SOS.Data.SosCrm.AE_Invoice;
using ARCollection = SOS.Data.SosCrm.AE_InvoiceCollection;
using ARController = SOS.Data.SosCrm.AE_InvoiceController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_InvoiceControllerExtensions
	{
		public static AR CreateInvoiceHeader(this ARController oCntlr, long? lAccountId, string sInvoiceTypeID
			, int? nTaxScheduleId, int? nPaymentTermId, decimal? dSalesAmount, decimal? dOrigianlTranAmount
			, decimal? dCurrentTranAmount, decimal? dCostAmount, decimal? dTaxAmount, long? lContractID
			, DateTime? dtDocDate, DateTime? dtPostedDate, DateTime? dtDueDate, DateTime? dtGlPostDate)
		{
			/** Initialize. */
			AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceCreateHeader(lAccountId
				, sInvoiceTypeID
				, nTaxScheduleId
				, nPaymentTermId
				, dSalesAmount
				, dOrigianlTranAmount
				, dCurrentTranAmount
				, dCostAmount
				, dTaxAmount
				, lContractID
				, dtDocDate
				, dtPostedDate
				, dtDueDate
				, dtGlPostDate));

			/** Return result. */
			return oResult;
		}

		public static AR CalculatePrices(this ARController oCntlr, AR oInvoice, string sStateID, string sPostalCode)
		{
			/** Validate: Make sure that this invoice does not have the price set already.  If so throw exception. */
			if (oInvoice.OriginalTransactionAmount > 0) throw new Exception(
				string.Format("Sorry but the invoice pass with Invoice ID '{0}' has its prices already been calculated.", oInvoice.InvoiceID));

			/** Initilize. */
			AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceCalculatePrices(oInvoice.InvoiceID, sStateID, sPostalCode, false));

			/** Return result. */
			return oResult;
		}

		public static AR CreateInvoiceMin(this ARController cntlr, long accountId, string invoiceTypeId, string gpEmployeeId)
		{
			return
				cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceCreateMinimal(accountId, invoiceTypeId, gpEmployeeId));
		}

	}
}
