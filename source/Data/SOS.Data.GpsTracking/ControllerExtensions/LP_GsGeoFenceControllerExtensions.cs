using AR = SOS.Data.GpsTracking.LP_GsGeoFence;
using ARCollection = SOS.Data.GpsTracking.LP_GsGeoFenceCollection;
using ARController = SOS.Data.GpsTracking.LP_GsGeoFenceController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
	public static class LP_GsGeoFenceControllerExtensions
	{
		public static AR GetNewFence(this ARController oCntlr, long lAccountID, long? lUnitID, int numberOfFences = 5)
		{
			return
				oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_GsGeoFencesGetNextAvailable(lAccountID, lUnitID, numberOfFences));
		}

		public static AR GetByGsGeoFenceID(this ARController oCntlr, long lGsGeoFenceId)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_GsGeoFencesGetByGeoFenceId(lGsGeoFenceId));
		}
	}
}
