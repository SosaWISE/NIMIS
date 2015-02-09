using AR = SOS.Data.SosCrm.Models.ActionPermission;
using ARCollection = System.Collections.Generic.IList<SOS.Data.SosCrm.Models.ActionPermission>;

namespace SOS.Data.SosCrm.Controllers
{
	public class ActionPermissionController : BaseModelController<AR>
	{
		public ARCollection GetCurrentApplicationPermissions(int nApplicationID)
		{
			return LoadCollectionByProcedure(SosCrmDataStoredProcedureManager.UI_ApplicationGetCurrentApplicationPermissions(nApplicationID)
			);
		}
	}
}
