using AR = SOS.Data.SosCrm.AE_PaymentFullView;
using ARCollection = SOS.Data.SosCrm.AE_PaymentFullViewCollection;
using ARController = SOS.Data.SosCrm.AE_PaymentFullViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class AE_PaymentFullViewControllerExtensions
	{
		public static AR GetByInvoiceIDAndPaymentID(this ARController oCntlr, long lInvoiceID, long lPaymentID)
		{
			/** INit. */
			var oQry = AR.CreateQuery()
				.WHERE(AR.Columns.InvoiceID, lInvoiceID)
				.WHERE(AR.Columns.PaymentID, lPaymentID);
			AR oResult = oCntlr.LoadSingle(oQry);

			/** Return result. */
			return oResult;
		}
	}
}
