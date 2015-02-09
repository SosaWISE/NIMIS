using AR = SOS.Data.SosCrm.AE_CustomerSWINGInterimView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGInterimViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGInterimViewController;
                                     

namespace SOS.Data.SosCrm.ControllerExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class AE_CustomerSWINGFromInterimViewControllerExtensions
    {
        public static AR CustomerSWINGInterim(this ARController cntlr, long interimAccountID, bool swingEquipment)
        {
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerSWINGFromInterim(interimAccountID, swingEquipment));
        }
    }
}