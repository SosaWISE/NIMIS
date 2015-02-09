using SubSonic;
using AR = SOS.Data.SosCrm.UI_ApplicationVersion;
using ARCollection = SOS.Data.SosCrm.UI_ApplicationVersionCollection;
using ARController = SOS.Data.SosCrm.UI_ApplicationVersionController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class UI_ApplicationControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetApplicationVersions(this ARController oCntrl, int nApplicationID)
		{
			Query oQry = AR.Query()
				.WHERE(AR.Columns.ApplicationId, Comparison.Equals, nApplicationID);

			return oCntrl.LoadCollection(oQry);
		}

	}
}
