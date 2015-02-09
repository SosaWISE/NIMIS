using SubSonic;
using AR = SOS.Data.SosCrm.UI_MenusCurrentMenusView;
using ARCollection = SOS.Data.SosCrm.UI_MenusCurrentMenusViewCollection;
using ARController = SOS.Data.SosCrm.UI_MenusCurrentMenusViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_MenusCurrentMenusViewControllerExtensions
	{
		public static AR GetCurrentMenu(this ARController oCntrl, int nApplicationVersionID)
		{
			// Locals
			var oQry = AR.Query()
				.WHERE(AR.Columns.ApplicationVersionId, Comparison.Equals, nApplicationVersionID);

			return oCntrl.LoadSingle(oQry);
		}
	}
}
