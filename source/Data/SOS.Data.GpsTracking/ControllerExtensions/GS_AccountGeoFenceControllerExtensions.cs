using AR = SOS.Data.GpsTracking.GS_AccountGeoFence;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFenceCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFenceController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
	public static class GS_AccountGeoFenceControllerExtensions
	{
		public static AR SavePolygoneFence(this ARController oCntlr, long? lGeoFenceId, long lAccountId, string sListOfCoords, string sUserId)
		{
			/** Initialize. */
			AR oResult = oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencePolygonSave(lGeoFenceId
				, GS_AccountGeoFenceType.MetaData.PolygonID
				, lAccountId
				, sListOfCoords
				, sUserId));

			/** Return result. */
			return oResult;
		}
	}
}
