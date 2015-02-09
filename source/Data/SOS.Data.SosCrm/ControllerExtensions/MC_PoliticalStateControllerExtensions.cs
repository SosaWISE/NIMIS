using SubSonic;
using AR = SOS.Data.SosCrm.MC_PoliticalState;
using ARCollection = SOS.Data.SosCrm.MC_PoliticalStateCollection;
using ARController = SOS.Data.SosCrm.MC_PoliticalStateController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_PoliticalStateControllerExtensions
	{
		public static ARCollection GetAllInCountry(this ARController controller, string szCountryID = "USA")
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.CountryId, Comparison.Equals, szCountryID)
				.ORDER_BY(AR.Columns.StateName, "ASC");

			return controller.LoadCollection(qry);
		}

		public static AR GetByStateAB(this ARController ctlr, string stateAB)
		{
			return ctlr.LoadSingle(SosCrmDataStoredProcedureManager.MC_PoliticalStateGetByStateAB(stateAB));
		}
	}
}
