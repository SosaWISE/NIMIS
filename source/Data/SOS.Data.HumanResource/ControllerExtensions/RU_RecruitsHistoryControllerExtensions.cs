using SubSonic;
using AR = SOS.Data.HumanResource.RU_RecruitsHistory;
using ARCollection = SOS.Data.HumanResource.RU_RecruitsHistoryCollection;
using ARController = SOS.Data.HumanResource.RU_RecruitsHistoryController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_RecruitsHistoryControllerExtensions
	{
		public static ARCollection ByRecruit(this ARController controller, int recruitID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.RecruitID, Comparison.Equals, recruitID);

			return controller.LoadCollection(qry);
		}
	}
}
