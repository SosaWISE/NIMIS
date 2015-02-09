using AR = SOS.Data.HumanResource.Models.IDText;
using ARCollection = System.Collections.Generic.IList<SOS.Data.HumanResource.Models.IDText>;

namespace SOS.Data.HumanResource.Controllers
{
	public class IDTextController : BaseModelController<AR>
	{
		public ARCollection GetRecruitNames(int seasonID)
		{
			return LoadCollectionByProcedure(HumanResourceDataStoredProcedureManager.RU_RecruitsGetRecruitNames(seasonID)
			);
		}
	}
}
