using AR = SOS.Data.GpsTracking.GS_AccountGeoFenceCircle;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFenceCircleCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFenceCircleController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_AccountGeoFenceCircleControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR Save(this ARController oCntlr, long lGeoFenceId, long lAccountId, double dRadius, double dCenterLattitude, double dCenterLongitude, string sModifiedBy)
		{
			/** Inititalize. */
			AR oResult =
				oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFenceCircleSave(lGeoFenceId, lAccountId,
				                                                                                     dCenterLattitude,
				                                                                                     dCenterLongitude, dRadius,

																									 sModifiedBy));
			/** Return result. */
			return oResult;
		}
	}
}
