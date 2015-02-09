using SubSonic;
using AR = NXS.Data.Licensing.LM_License;
using ARCollection = NXS.Data.Licensing.LM_LicenseCollection;
using ARController = NXS.Data.Licensing.LM_LicenseController;

namespace NXS.Data.Licensing.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LM_LicenseControllerExtensions
	{
		public static AR GetExistingCustomerLicense(this ARController controller, int accountID, int requirementID)
		{
			Query qry = AR.Query()
							.WHERE(AR.Columns.AccountID, accountID)
							.AND(AR.Columns.RequirementID, requirementID);

			return controller.LoadSingle(qry);
		}
	}
}
