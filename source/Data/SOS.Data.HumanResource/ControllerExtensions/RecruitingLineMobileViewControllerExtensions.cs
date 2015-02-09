using AR = SOS.Data.HumanResource.RecruitingLineMobileView;
using ARCollection = SOS.Data.HumanResource.RecruitingLineMobileViewCollection;
using ARController = SOS.Data.HumanResource.RecruitingLineMobileViewController; 

namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RecruitingLineMobileViewControllerExtensions
	{
		public static AR GetByGpEmployeeIdAndSeasonId(this ARController oCntlr, string szGpEmployeeID, int nSeasonID)
		{
			// Locals
			return oCntlr.LoadSingle(AR.Query()
			                         	.WHERE(AR.Columns.GPEmployeeID, szGpEmployeeID)
			                         	.WHERE(AR.Columns.SeasonID, nSeasonID));
		}

		public static ARCollection GetByOfficeID(this ARController oCntlr, int nOfficeID)
		{
			return oCntlr.LoadCollection(AR.Query()
			                             	.WHERE(AR.Columns.TeamLocationID, nOfficeID));
		}
	}
}
