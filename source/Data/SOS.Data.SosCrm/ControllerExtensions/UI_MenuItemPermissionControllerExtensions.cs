using SubSonic;
using AR = SOS.Data.SosCrm.UI_MenuItemPermission;
using ARCollection = SOS.Data.SosCrm.UI_MenuItemPermissionCollection;
using ARController = SOS.Data.SosCrm.UI_MenuItemPermissionController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_MenuItemPermissionControllerExtensions
	{
		public static ARCollection GetByMenueItemID(this ARController oCntrl, int nMenuItemID)
		{
			// Locals
			var oQry = AR.Query()
				.WHERE(AR.Columns.MenuItemId, Comparison.Equals, nMenuItemID);

			return oCntrl.LoadCollection(oQry);
		}
	}
}
