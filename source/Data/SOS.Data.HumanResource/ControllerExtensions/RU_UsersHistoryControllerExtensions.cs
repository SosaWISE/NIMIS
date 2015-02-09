using SubSonic;
using AR = SOS.Data.HumanResource.RU_UsersHistory;
using ARCollection = SOS.Data.HumanResource.RU_UsersHistoryCollection;
using ARController = SOS.Data.HumanResource.RU_UsersHistoryController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_UsersHistoryControllerExtensions
	{
		public static ARCollection ByUser(this ARController controller, int userID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.UserID, Comparison.Equals, userID);

			return controller.LoadCollection(qry);
		}
	}
}
