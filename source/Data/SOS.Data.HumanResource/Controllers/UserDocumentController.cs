using SOS.Data.HumanResource.Models;
using ARCollection = System.Collections.Generic.IList<SOS.Data.HumanResource.Models.UserDocument>;

namespace SOS.Data.HumanResource.Controllers
{
	public class UserDocumentController : BaseModelController<UserDocument>
	{
		public ARCollection GetDocumentsByGPID(string companyID)
		{
			return LoadCollectionByProcedure(HumanResourceDataStoredProcedureManager.RU_UsersGetDocumentsByGPID(companyID)
			);
		}
	}
}
