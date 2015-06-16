using AR = SOS.Data.SosCrm.MS_AccountSetupCheckList;
using ARCollection = SOS.Data.SosCrm.MS_AccountSetupCheckListCollection;
using ARController = SOS.Data.SosCrm.MS_AccountSetupCheckListController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountSetupCheckListControllerExtensions
	{
		public static AR SetKeyValue(this ARController cntlr, long accountId, string key)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountSetupCheckListSetByKey(accountId, key));
		}
	}
}
