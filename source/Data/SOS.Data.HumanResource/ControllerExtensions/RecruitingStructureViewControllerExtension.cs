using AR = SOS.Data.HumanResource.RecruitingStructureView;
using ARCollection = SOS.Data.HumanResource.RecruitingStructureViewCollection;
using ARController = SOS.Data.HumanResource.RecruitingStructureViewController; 
namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RecruitingStructureViewControllerExtension
	{
		public static RecruitingStructureViewCollection LookupByRecruitIDSeasonID(this RecruitingStructureViewController controller, int recruitID, int seasonID)
		{

			var q = RecruitingStructureView.Query().AND(RecruitingStructureView.Columns.RecruitID, recruitID)
							.AND(RecruitingStructureView.Columns.SeasonID, seasonID);
			RecruitingStructureViewCollection recruitingStructureView = controller.LoadCollection(q);
			return recruitingStructureView;

		}
	}
}
