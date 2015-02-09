using SubSonic;
using AR = SOS.Data.SosCrm.UI_UserSettingsContainer;
using ARCollection = SOS.Data.SosCrm.UI_UserSettingsContainerCollection;
using ARController = SOS.Data.SosCrm.UI_UserSettingsContainerController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_UserSettingsContainerControllerExtensions
	{
		public static AR GetUserSettingsForApp(this ARController controller, int applicationID, string username)
		{
			Query qry = AR.Query()
							.WHERE(AR.Columns.ApplicationID, applicationID)
							.AND(AR.Columns.UserID, username);

			return controller.LoadSingle(qry);
		}
	}
}