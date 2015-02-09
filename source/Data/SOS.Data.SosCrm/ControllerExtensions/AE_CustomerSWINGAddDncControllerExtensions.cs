using AR = SOS.Data.SosCrm.AE_CustomerSWINGAdd_DncView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGAdd_DncViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGAdd_DncViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class AE_CustomerSwingAddDncViewControllerExtensions
    {
        public static AR CustomerSwingAddDnc(this ARController cntlr, string phoneNumber)
        {
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSWINGAdd_Dnc(phoneNumber));
        }
    }
}