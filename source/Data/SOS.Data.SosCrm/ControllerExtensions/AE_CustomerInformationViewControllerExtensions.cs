using AR = SOS.Data.SosCrm.AE_CustomerInformationView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerInformationViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerInformationViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_CustomerInformationViewControllerExtensions
	{
		public static AR GetByAccountId(this ARController cntlr, long accountId)
		{
			return
				cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerInformationViewMonitoredPartyByAccountId(accountId));
		}
	}
}
