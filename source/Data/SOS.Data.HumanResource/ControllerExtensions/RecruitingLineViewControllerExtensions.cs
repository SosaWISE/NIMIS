using SubSonic;
using AR = SOS.Data.HumanResource.RecruitingLineView;
using ARCollection = SOS.Data.HumanResource.RecruitingLineViewCollection;
using ARController = SOS.Data.HumanResource.RecruitingLineViewController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RecruitingLineViewControllerExtensions
	{
		public static AR LoadByRecruitID(this ARController controller, int recruitID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.RecruitID, recruitID);

			return controller.LoadSingle(qry);
		}
	}
}
