using AR = SOS.Data.GpsTracking.GS_EventTypesView;
using ARCollection = SOS.Data.GpsTracking.GS_EventTypesViewCollection;
using ARController = SOS.Data.GpsTracking.GS_EventTypesViewController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
	public static class GS_EventTypeViewControllerExtensions
	{
		public static ARCollection ReadAll(this ARController oCntlr, string eventTypeID, string eventType)
		{
			/** Inititliaze. */
			var oResult = oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_EventTypeViewReadAll(eventTypeID, eventType));

			return oResult;
		}
	}
}