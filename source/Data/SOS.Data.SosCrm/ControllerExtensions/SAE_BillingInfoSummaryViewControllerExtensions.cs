using AR = SOS.Data.SosCrm.SAE_BillingInfoSummaryView;
using ARCollection = SOS.Data.SosCrm.SAE_BillingInfoSummaryViewCollection;
using ARController = SOS.Data.SosCrm.SAE_BillingInfoSummaryViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class SAE_BillingInfoSummaryViewControllerExtensions
	{
		public static ARCollection GetByCMFID(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.SAE_BillingInfoSummaryByCMFID(cmfid, null));
		}
		public static ARCollection GetByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.SAE_BillingInfoSummaryByCMFID(null, accountId));
		}
	}
}
