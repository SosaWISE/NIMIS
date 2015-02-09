using AR = SOS.Data.SosCrm.SAE_BillingHistoryView;
using ARCollection = SOS.Data.SosCrm.SAE_BillingHistoryViewCollection;
using ARController = SOS.Data.SosCrm.SAE_BillingHistoryViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class SAE_BillingHistoryViewControllerExtensions
    {
        public static ARCollection GetByCMFID(this ARController cntlr, long cmfid)
        {
            return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.SAE_BillingHistoryByCMFID(cmfid));
        }

    }
}
