using AR = SOS.Data.HumanResource.RecruitUserView;
using ARCollection = SOS.Data.HumanResource.RecruitUserViewCollection;
using ARController = SOS.Data.HumanResource.RecruitUserViewController; 

namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RecruiteUserControllerExtensions
	{
		public static RecruitUserView LookupByGPEmployeeID(this ARController controller, string gpeEmployeeID)
		{
			var q = RecruitUserView.Query().AND(RecruitUserView.Columns.GPEmployeeID, gpeEmployeeID).AND(
				RecruitUserView.Columns.SeasonIsCurrent, true);
			var recruit = controller.LoadSingle(q);
			return recruit;
		}

		public static RecruitUserView LookupRecruitManagerByRecruitIDSeasonID(this RecruitUserViewController controller, int recruitID, int seasonID)
		{
			
			RecruitingStructureViewCollection recruitingStructureView = HumanResourceDataContext.Instance.RecruitingStructureViews.LookupByRecruitIDSeasonID(recruitID, seasonID);
			
			if (recruitingStructureView == null || recruitingStructureView.Count <= 0)
			{
				return null;
			}
			var qManager = RecruitUserView.Query().AND(RecruitUserView.Columns.RecruitID, recruitingStructureView[0].RecruitID);
			var managerRecuit = controller.LoadSingle(qManager);
			return managerRecuit;
		}

	}
}
