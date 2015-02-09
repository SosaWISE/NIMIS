using AR = SOS.Data.GpsTracking.GS_AccountGeoFencePolygon;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFencePolygonCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFencePolygonController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
	public static class GS_AccountGeoFencePolygonControllerExtensions
	{
		public static ARCollection GetByGeoFenceId(this ARController oCntlr, long lGeoFenceID)
		{
			/** Init query. */
			var oQry = AR.Query()
				.WHERE(AR.Columns.GeoFenceId, lGeoFenceID)
				.ORDER_BY(AR.Columns.Sequence);

			var oColResult = oCntlr.LoadCollection(oQry);

			/** Return result. */
			return oColResult;
		}
	}
}
