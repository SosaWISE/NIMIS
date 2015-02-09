using AR = SOS.Data.SosCrm.UI_Action;
using ARCollection = SOS.Data.SosCrm.UI_ActionCollection;
using ARController = SOS.Data.SosCrm.UI_ActionController;
using SubSonic;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_ActionsControllerExtensions
	{
		public static ARCollection AllActive(this ARController oCntrl)
		{
			// Locals
			var oQry = AR.Query()
				.WHERE(AR.Columns.IsActive, Comparison.Equals, true)
				.ORDER_BY(AR.Columns.ActionName);

			return oCntrl.LoadCollection(oQry);
		}
	}
}
