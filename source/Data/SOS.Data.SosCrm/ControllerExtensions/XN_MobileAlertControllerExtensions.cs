using SubSonic;
using AR = SLS.Data.Cms.XN_MobileAlert;
using ARCollection = SLS.Data.Cms.XN_MobileAlertCollection;
using ARController = SLS.Data.Cms.XN_MobileAlertController;

namespace SLS.Data.Cms.ControllerExtensions
{
	public static class XN_MobileAlertControllerExtensions
	{
		public static ARCollection GetAlertsByEmployeeID(this ARController oCntlr, string szEmployeeID)
		{
			return oCntlr.LoadCollection(CmsDataStoredProcedureManager.XN_MobileAlertsGetByEmployeeID(szEmployeeID));
		}
	}
}
