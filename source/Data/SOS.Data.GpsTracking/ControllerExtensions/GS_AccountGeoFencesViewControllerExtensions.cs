using SubSonic;
using AR = SOS.Data.GpsTracking.GS_AccountGeoFencesView;
using ARCollection = SOS.Data.GpsTracking.GS_AccountGeoFencesViewCollection;
using ARController = SOS.Data.GpsTracking.GS_AccountGeoFencesViewController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_AccountGeoFencesViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetByAccountID(this ARController oCntlr, long lAccountID)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencesByAccountID(lAccountID));
		}

		public static ARCollection GetByCMFID(this ARController oCntlr, long lCMFID, long? customerId)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencesByCMFID(lCMFID, customerId));
		}

		public static AR LoadByPrimaryKey(this ARController oCntlr, long lGeoFenceID)
		{
			/** Initialize. */
			AR oResult = oCntlr.LoadSingle(ReadOnlyRecord<GS_AccountGeoFencesView>.Query().WHERE(GS_AccountGeoFencesView.Columns.GeoFenceID, lGeoFenceID));

			/** Return result. */
			return oResult;
		}

		public static AR BindLaipacFence(this ARController oCntlr, long commandMessageID, long accountId)
		{
			AR oResult = oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.GS_AccountGeoFencesBindLaipacFence(commandMessageID, accountId));

			return oResult;
		}
	}
}
