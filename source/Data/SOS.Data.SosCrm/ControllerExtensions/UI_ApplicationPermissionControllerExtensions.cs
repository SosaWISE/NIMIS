using SubSonic;
using AR = SOS.Data.SosCrm.UI_ApplicationPermission;
using ARCollection = SOS.Data.SosCrm.UI_ApplicationPermissionCollection;
using ARController = SOS.Data.SosCrm.UI_ApplicationPermissionController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_ApplicationPermissionControllerExtensions
	{
		public static ARCollection GetPermissionsForApplication (this ARController oCntrl, int nApplicationId)
		{
			// Locals
			var oQry = AR.Query()
				.WHERE(AR.Columns.ApplicationId, Comparison.Equals, nApplicationId);

			return oCntrl.LoadCollection(oQry);
		}
	}
}
