using System.Data;
using AR = NXS.Data.Licensing.LM_Requirement;
using ARCollection = NXS.Data.Licensing.LM_RequirementCollection;
using ARController = NXS.Data.Licensing.LM_RequirementController;

namespace NXS.Data.Licensing.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LM_RequirementControllerExtensions
	{
		public static DataTable GetCustomerPermitLetters(this ARController oController)
		{
			// Locals
			var oResult = new DataTable();
			var oDataSet = LicensingDataStoredProcedureManager.LM_RequirementsGetCustomerPermitLetterData().GetDataSet();

			// Check that we have a result
			if (oDataSet != null
				&& oDataSet.Tables.Count > 0
				&& oDataSet.Tables[0].Rows.Count > 0)
				oResult = oDataSet.Tables[0];

			// Return result
			return oResult;
		}
	}
}
