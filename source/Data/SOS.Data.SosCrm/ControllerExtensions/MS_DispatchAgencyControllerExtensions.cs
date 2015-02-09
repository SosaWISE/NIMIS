using AR = SOS.Data.SosCrm.MS_DispatchAgenciesView;
using ARCollection = SOS.Data.SosCrm.MS_DispatchAgenciesViewCollection;
using ARController = SOS.Data.SosCrm.MS_DispatchAgenciesViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_DispatchAgencyControllerExtensions
	{
		public static ARCollection GetDispatchAgencies(this ARController cntlr, string city, string state, string zip)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_DispatchAgencyGetByCityStateZips(city, state, zip));
		}

		public static ARCollection GetDispatchAgenciesByAgencyTypeId(this ARController cntlr, string city, string state,
			string zip, int dispatchAgencyTypeId)
		{
			return
				cntlr.LoadCollection(
					SosCrmDataStoredProcedureManager.MS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId(city, state, zip,
						dispatchAgencyTypeId));
		}
		public static ARCollection GetMonitronicsEntityAgencies(this ARController cntlr, string city, string state, string zip)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesGetByCityStateZips(city, state, zip));
		}

		public static ARCollection GetMonitronicsEntityAgenciesTypeId(this ARController cntlr, string city, string state,
			string zip, int dispatchAgencyTypeId)
		{
			return
				cntlr.LoadCollection(
					SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId(city, state, zip,
						dispatchAgencyTypeId));
		}
	}
}
