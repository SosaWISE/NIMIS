using SubSonic;
using AR = SOS.Data.HumanResource.RU_TerminationCategory;
using ARCollection = SOS.Data.HumanResource.RU_TerminationCategoryCollection;
using ARController = SOS.Data.HumanResource.RU_TerminationCategoryController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TerminationCategoryControllerExtensions
	{
		public static ARCollection GetAllForTerminationType(this ARController controller, int terminationTypeID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.TerminationTypeID, terminationTypeID);

			return controller.LoadCollection(qry);
		}
	}
}
