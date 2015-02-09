/**
 * This class will be used for computing the distance from zone to customer 
 * primarily used in Scheduling
 * 
 * ***************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;


namespace SSE.Services.CmsCORS.Helpers
{

    public static class CustomGeoCoordinate
    {
        public static double GetDistance(double lat1, double long1, double lat2, double long2)
        {
            try
            {
                var locA = new GeoCoordinate(lat1, long1);
                var locB = new GeoCoordinate(lat2, long2);
                double distance = locA.GetDistanceTo(locB) / 1609.344;
                return distance;
            }
            catch(Exception e) {
                return 0.0;
            }
        }
        
    }
}