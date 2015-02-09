using AR = SOS.Data.SosCrm.MS_IndustryNumberByCallerIdView;
using ARCollection = SOS.Data.SosCrm.MS_IndustryNumberByCallerIdViewCollection;
using ARController = SOS.Data.SosCrm.MS_IndustryNumberByCallerIdViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MS_IndustryNumberByCallerIdViewControllerExtensions
	{
		public static ARCollection GetInfo(this ARController oCntlr, string szCallerId)
		{
			/** Initialize. */
			ARCollection oCol = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_IndustryAccountFindByCallerId(szCallerId));

			/** Return result. */
			return oCol;
		}
	}
}
