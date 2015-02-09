using SubSonic;
using AR = SOS.Data.HumanResource.RU_Termination;
using ARCollection = SOS.Data.HumanResource.RU_TerminationCollection;
using ARController = SOS.Data.HumanResource.RU_TerminationController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TerminationControllerExtensions
	{
		public static ARCollection GetAllForRecruit(this ARController controller, int recruitID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.RecruitID, recruitID)
				.ORDER_BY(AR.Columns.CreatedByDate, "DESC");

			return	controller.LoadCollection(qry);
		}
	}
}