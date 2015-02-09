using AR = SOS.Data.GpsTracking.GS_AccountGeoFenceRectangle;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFenceRectangleCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFenceRectangleController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_AccountGeoFenceRectangleControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR Create(this ARController oCntlr, long lAccountID, string sReportModeId, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMinLongitude, double dMinLattitude, double dMaxLongitude, short? sZoomLevel, string sModifiedBy)
		{
			/** Inititalize. */
			AR oResult =
				oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFenceRectangleSave(0, lAccountID, sReportModeId,
					geoFenceName, geoFenceDescription,
					dMaxLattitude, dMinLongitude,
					dMinLattitude, dMaxLongitude,
					sZoomLevel,
					sModifiedBy));

			/** Return result. */
			return oResult;
		}

		public static AR Update(this ARController oCntlr, long? lGeoFenceID, long? lAccountID, string sReportModeId, string geoFenceName, string geoFenceDescription, double? dMaxLattitude, double? dMinLongitude, double? dMinLattitude, double? dMaxLongitude, short? sZoomLevel, string sModifiedBy)
		{
			/** Inititalize. */
			AR oResult =
				oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFenceRectangleSave(lGeoFenceID, lAccountID, sReportModeId,
					geoFenceName, geoFenceDescription, 
				    dMaxLattitude, dMinLongitude,
				    dMinLattitude, dMaxLongitude,
					sZoomLevel,
					sModifiedBy));

			/** Return result. */
			return oResult;
		}

	    public static AR Delete(this ARController oCntlr, long? lGeoFenceID, string sModifiedBy)
	    {
	        AR oResult =
	            oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFenceRectangleDelete(lGeoFenceID,
	                sModifiedBy));

            /** Return Result. */
	        return oResult;
	    }
	}
}
