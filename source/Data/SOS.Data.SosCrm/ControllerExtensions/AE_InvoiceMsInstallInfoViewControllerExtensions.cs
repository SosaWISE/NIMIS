using AR = SOS.Data.SosCrm.AE_InvoiceMsInstallInfoView;
using ARCollection = SOS.Data.SosCrm.AE_InvoiceMsInstallInfoViewCollection;
using ARController = SOS.Data.SosCrm.AE_InvoiceMsInstallInfoViewController;


namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_InvoiceMsInstallInfoViewControllerExtensions
	{
		public static AR GetByIDs(this ARController cntlr, long? invoiceID, long? accountId, string gpEmployeeId)
		{
			return
				cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_InvoiceMsInstallInfoViewGetByIDs(invoiceID, accountId,
					gpEmployeeId));
		}
	}
}
