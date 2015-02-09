using AR = SOS.Data.GpsTracking.GS_AccountGeoFencePointsView;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFencePointsViewCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFencePointsViewController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_AccountGeoFencePointsViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR GeoPointCreateUpdate(this ARController oCntlr, long lGeoFenceID, long lAccountId, string sPlaceName, string sPlaceDescription, double sLattitude, double sLongitude, string sUserId)
		{
			/** Initialize. */
			long? id = lGeoFenceID == 0 ? (long?) null : lGeoFenceID;
			AR oItem = oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencePointSave(id, lAccountId, sPlaceName,
			                                                                          sPlaceDescription, sLattitude, sLongitude,
			                                                                          sUserId));

			/** Return result. */
			return oItem;
		}

		public static bool GeoPointDelete(this ARController oCntlr, long lGeoFenceID, long lAccountId, string sUserId)
		{
			/** Initialize. */
			oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencePointDelete(lGeoFenceID, lAccountId,
				                                                                                      sUserId));

			/** Return result. */
			return true;
		}

		public static AR GeoPointRead(this ARController oCntlr, long lGeoFenceID, long lAccountId)
		{
			/** Initialize. */
			AR oItem =
				oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencePointRead(lGeoFenceID, lAccountId));

			/** Return result. */
			return oItem;
		}
	}
}
