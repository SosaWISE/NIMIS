using AR = SOS.Data.SosCrm.UI_ApplicationVersionsView;
using ARCollection = SOS.Data.SosCrm.UI_ApplicationVersionsViewCollection;
using ARController = SOS.Data.SosCrm.UI_ApplicationVersionsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_ApplicationVersionsViewControllerExtensions
	{

		public static ARCollection GetApplicationsByPermission(this ARController oCntlr, UI_PermissionType.PermissionTypeEnum eType, string szPrincipalName)
		{
			// Locals

			UI_ApplicationVersionsViewCollection oResult = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.UI_ApplicationsGetByPermission(szPrincipalName, (int)eType));
			return oResult;
		}

		public static AR GetApplicationVersionView(this ARController oCntlr, int nApplicationID)
		{
			// Locals
			var oQry = AR.Query().WHERE(AR.Columns.ApplicationID, nApplicationID);

			AR oResult = oCntlr.LoadSingle(oQry);

			if (oResult.IsLoaded)
				return oResult;

			// Default execution path
			return null;
		}

	}
}
