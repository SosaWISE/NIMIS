using AR = SOS.Data.SosCrm.MS_AccountSiteGeneralDispatch;
using ARCollection = SOS.Data.SosCrm.MS_AccountSiteGeneralDispatchCollection;
using ARController = SOS.Data.SosCrm.MS_AccountSiteGeneralDispatchController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountSiteGeneralDispatchControllerExtensions
	{
		public static ARCollection GetByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_AccountSiteGeneralDispatchByAccountId(accountId));
		}
	}
}
