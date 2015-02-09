using System.Collections.Generic;
using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.UI_ApplicationMenuView;
using ARCollection = SOS.Data.SosCrm.UI_ApplicationMenuViewCollection;
using ARController = SOS.Data.SosCrm.UI_ApplicationMenuViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_ApplicationMenuViewControllerExtensions
	{
		public static ARCollection GetCurrentApplicationMenuAD(this ARController controller, int applicationID, string userNameAD, List<string> groupNamesAD)
		{
			return controller.LoadCollection(
				SosCrmDataStoredProcedureManager.UI_MenuItemsGetCurrentApplicationMenuAD(applicationID, userNameAD, StringUtility.Join(groupNamesAD, ","))
			);
		}
		public static ARCollection GetCurrentApplicationMenu(this ARController controller, int applicationID)
		{
			return GetCurrentApplicationMenu(controller, applicationID, false);
		}
		public static ARCollection GetCurrentApplicationMenu(this ARController controller, int applicationID, bool includeNotVisible)
		{
			return controller.LoadCollection(
				SosCrmDataStoredProcedureManager.UI_MenuItemsGetCurrentApplicationMenu(applicationID, includeNotVisible)
			);
		}
	}
}
