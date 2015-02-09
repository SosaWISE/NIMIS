using AR = SOS.Data.SosCrm.AE_CustomerSWUNGInfoView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWUNGInfoViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWUNGInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class AE_CustomerSwungInfoViewControllerExtensions
    {
        public static AR CustomerSwungInfo(this ARController cntlr, long interimAccountID)
        {
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSwungInfo(interimAccountID));
        }
    }
}