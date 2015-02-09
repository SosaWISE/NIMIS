using AR = SOS.Data.HumanResource.Models.SeasonsMap;
using ARCollection = System.Collections.Generic.IList<SOS.Data.HumanResource.Models.SeasonsMap>;

namespace SOS.Data.HumanResource.Controllers
{
	public class SeasonsMapController : BaseModelController<AR>
	{
		public ARCollection GetRecruitSeasonsMaps(int nFromSeasonID, int nToSeasonID)
		{
			return LoadCollectionByProcedure(
				HumanResourceDataStoredProcedureManager.RU_RecruitsGetRecruitSeasonsMaps(nFromSeasonID, nToSeasonID)
			);
		}
		public ARCollection GetTeamSeasonsMaps(int nFromSeasonID, int nToSeasonID)
		{
			return LoadCollectionByProcedure(
				HumanResourceDataStoredProcedureManager.RU_TeamsGetTeamSeasonsMaps(nFromSeasonID, nToSeasonID)
			);
		}
		public ARCollection GetOfficeSeasonsMaps(int nFromSeasonID, int nToSeasonID)
		{
			return LoadCollectionByProcedure(
				HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetOfficeSeasonsMaps(nFromSeasonID, nToSeasonID)
			);
		}
	}
}
