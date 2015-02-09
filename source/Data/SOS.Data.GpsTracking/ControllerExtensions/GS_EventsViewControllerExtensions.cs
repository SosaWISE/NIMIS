using System;
using AR = SOS.Data.GpsTracking.GS_EventsView;
using ARCollection = SOS.Data.GpsTracking.GS_EventsViewCollection;
using ARController = SOS.Data.GpsTracking.GS_EventsViewController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class GS_EventsViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetDeviceEvents(this ARController oCntlr, long lAccountID, DateTime? dStartDate, DateTime? dEndDate, int nPageSize, int nPageNumber = 1)
		{
			/** Initialize. */
			ARCollection oResult =
				oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_EventsPagging(lAccountID, dStartDate, dEndDate,
				                                                                             nPageSize, nPageNumber));

			/** Return result. */
			return oResult;
		}

		public static ARCollection GetDeviceEventsMaster(this ARController oCntlr, long lCMFID, long? lCustomerId, DateTime? dStartDate,
		                                                 DateTime? dEndDate, int nPageSize, int nPageNumber = 1)
		{
			/** Initialize. */
			ARCollection oResult =
				oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_EventsPaggingMaster(lCMFID, lCustomerId, dStartDate, dEndDate,
				                                                                                   nPageSize, nPageNumber));

			/** Return result. */
			return oResult;
		}

		public static GS_EventsViewCollection EventsReport(this GS_EventsViewController oCntlr, long customerMasterFileID, long? accountID, string eventTypeID, long? geoFenceID, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
		{
			return
				oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.GS_EventsReporting(customerMasterFileID, accountID,
					eventTypeID, geoFenceID, startDate, endDate, pageSize, pageNumber));
		}
	}
}
