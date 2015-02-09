using AR = SOS.Data.SosCrm.AE_CustomerSWINGSystemDetailView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGSystemDetailViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGSystemDetailViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class AE_CustomerSWINGSystemDetailsViewControllerExtensions
    {
        public static AR GetCustomerSWINGSystemDetails(this ARController cntlr, long interimAccountID)
        {
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSWINGGetSystemDetail(interimAccountID));
        }
    }
}