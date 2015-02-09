using AR = SOS.Data.SosCrm.MS_AccountMonitorInformationsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountMonitorInformationsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountMonitorInformationsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountMonitorInformationControllerExtensions
	{
		public static AR LoadByPrimaryKey(this ARController cntlr, long accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountMonitorInformationsByAccountID(accountId));
		}
	}
}
