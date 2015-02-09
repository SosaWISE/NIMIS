using AR = SOS.Data.SosCrm.MS_VendorAlarmComAccount;
using ARCollection = SOS.Data.SosCrm.MS_VendorAlarmComAccountCollection;
using ARController = SOS.Data.SosCrm.MS_VendorAlarmComAccountController;

namespace SOS.Data.SosCrm.ControllerExtensions {
    // ReSharper disable InconsistentNaming
    public static class MS_VendorAlarmComAccountControllerExtensions {
        // ReSharper restore InconsistentNaming
        public static AR GetByAccountId(this ARController cntlr, long accountID) {
            return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_VendorAlarmComAccountsGetByAccountId(accountID));
        }
    }
}
