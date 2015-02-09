using System;
using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Contracts
{
	public interface IGpsTrackingSerivces : IFunctionalService
	{
		IFnsResult<List<IFnsGsEventsView>> EventDeviceEventsGet(long lAccountID, DateTime? dStartDate, DateTime? dEndDate, int nPageSize, int nPageNumber = 1);

		IFnsResult<List<IFnsGsEventsView>> EventDeviceEventsMasterGet(long lCMFID, long? lCustomerId, DateTime? dStartDate,
		                                                              DateTime? dEndDate, int nPageSize, int nPageNumber = 1);

		IFnsResult<IFnsGsAccountGeoFencePoints> GeoPointCreateUpdate(long lGeoFenceID, long lAccountId, string sPlaceName, string sPlaceDescription, double sLattitude, double sLongitude, string sUserId);

		IFnsResult<bool> GeoPointDelete(long lGeoFenceID, long lAccountId, string sUserId);

		IFnsResult<IFnsGsAccountGeoFencePoints> GeoPointRead(long lGeoFenceID, long lAccountId);

		IFnsResult<List<IFnsGsAccountGeoFencePolygons>> GeoPolygonUpdate(long lGeoFenceId, long lAccountId, List<IFnsGsAccountGeoFencePolygons> listOfCoords, string sUserId);

		IFnsResult<List<IFnsGsAccountGeoFencePolygons>> GeoPolygonRead(long lGeoFenceId);

		IFnsResult<IFnsGsAccountGeoFenceCircles> GeoFenceCircleUpdate(long lGeoFenceId, double dRadius, double dCenterLattitude, double dCenterLongitude, string sModifiedBy);

		IFnsResult<List<IFnsGeoFencesView>> GeoFencesRead(long lAccountId);

		IFnsResult<List<IFnsGeoFencesView>> GeoFencesByCMFID(long lCMFID, long? lCustomerId);

		IFnsResult<IFnsGsAccountGeoFenceRectangles> GeoFenceRectangleCreate(long lAccountId, string sItemId,
		                                                                    string sReportMode, string geoFenceName,
		                                                                    string geoFenceDescription, double dMaxLattitude,
		                                                                    double dMaxLongitude, double dMinLattitude,
																			double dMinLongitude, short? zZoomLevel, string sModifiedBy);

		IFnsResult<IFnsGsAccountGeoFenceRectangles> GeoFenceRectangleUpdate(long lGeoFenceID, string sItemId,
		                                                                    string sReportMode, string geoFenceName,
		                                                                    string geoFenceDescription, double dMaxLattitude,
		                                                                    double dMinLongitude, double dMinLattitude,
		                                                                    double dMaxLongitude, short?  zZoomLevel, string sModifiedBy);

	    IFnsResult<bool> GeoFenceRectangleDelete(long lGeoFenceID, string sModifiedBy);

		IFnsResult<List<IFnsGsAccountGeoFenceRectangles>> GetLaipacS911GeoFences(long lAccountID, string sUsername);

		IFnsResult<IFnsGsAccountGeoFencePoints> GetLaipacS911CurrentLocation(long lAccountID, string sUsername);

		IFnsResult<List<IFnsGsDeviceEvents>> ReportEvents(long customerMasterFileId, long? deviceId, string eventTypeId, long? locationID, DateTime startDate, DateTime endDate, string username, int pageSize, int pageNumber);

		IFnsResult<List<IFnsGsEventTypeView>> EventTypeReadAll(string eventTypeID, string eventType);
	}
}