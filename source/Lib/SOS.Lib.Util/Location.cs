using System;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public enum DistanceUnits
	{
		Kilometers,
		Miles,
	}

	public class LocationList : List<Location>
	{
		/// <summary>
		/// Returns the maximum distance between all of the Locations
		/// </summary>
		/// <param name="units"></param>
		/// <returns></returns>
		public double MaxSpacingDistance(DistanceUnits units)
		{
			double max = 0;
			double d;
			int length = Count;
			for (int i = 0; i < length; i++)
			{
				//don't want to compare to yourself or compare the same twice
				//	when i=0 -> j=1, i=1 -> j=2, etc.
				for (int j = i + 1; j < length; j++)
				{
					d = this[i].DistanceFrom(this[j], units);
					if (d > max)
					{
						max = d;
					}
				}
			}
			return max;
		}

		/// <summary>
		/// Returns the shortest distance between all of the Locations
		/// </summary>
		/// <param name="location"> </param>
		/// <param name="units"></param>
		/// <returns></returns>
		public double ShortestDistanceFrom(Location location, DistanceUnits units)
		{
			if (Count > 0)
			{
				double min = double.MaxValue;
				double d;
				foreach (Location loc in this)
				{
					d = loc.DistanceFrom(location, units);
					if (d < min)
					{
						min = d;
					}
				}
				return min;
			}
			return 0;
		}
	}

	public class Location
	{
		public const double EarthRadiusMiles = 3963.189;
		public const double EarthRadiusKilometers = 6378.135;

		public const double RadianDegrees = (Math.PI/180d);

		/// <summary>
		/// Miles estimate for latitude
		/// Created using distance between 40, -110 and 41, -100, which is 69.17069692 miles
		/// 1/69.17069692=0.014456988935019103751427115157075
		/// </summary>
		public const double LAT_MILE = 0.0144569889;

		/// <summary>
		/// Miles estimate for longitude
		/// Created using distance between 40, -110 and 40, -101, which is 52.98755012 miles
		/// 1/52.98755012=0.018872357709222582944357496179331
		/// </summary>
		public const double LNG_MILE = 0.0188723578;

		public Location(double lat, double lng)
		{
			Latitude = lat;
			Longitude = lng;
		}

		public double Latitude { get; private set; }

		public double Longitude { get; private set; }

		public double DistanceFrom(Location dest, DistanceUnits units)
		{
			return DistanceFrom(dest, units, false);
		}

		public double DistanceFrom(Location dest, DistanceUnits units, bool bAsDrivingEstimate)
		{
			double distance = CalcHaversineDistance(Latitude, Longitude, dest.Latitude, dest.Longitude, units);

			if (bAsDrivingEstimate)
			{
				distance = GetDrivingEstimate(distance);
			}

			return distance;
		}

		public static double GetDrivingEstimate(double distance)
		{
			//this equation is assuming:
			//	that the closer together two points are the less direct a route will be or less like the haversine distance
			//	that the further apart two points are the more direct a rounte will be or more like the haversine distance
			//so it's just a linear equation that states
			//	at 1 mile the estimated driving distance is 1.5 times greater or 1.5 miles
			//	at 1000 miles the estimated driving distance is 1.15 times greater or 1150 miles

			//this is very primitive so feel free to improve

			// ((rise/run * x) + y-intercept)
			//at (0,1) y-intercept = 1.5
			//at (0,1000) y-intercept = 1.15
			//
			//1.15-1.5 = -.35
			//-0.35 (rise) / 1000 (run) = -0.00035
			double drivingEstimatePercent = (-0.00035*distance) + 1.5;

			//don't let the estimate drop below this point
			drivingEstimatePercent = Math.Max(1.15, drivingEstimatePercent);

			return distance*drivingEstimatePercent;
		}


		/* http://en.wikipedia.org/wiki/Earth_radius */
		/* 6,356.750 km to 6,378.135 km (≈3,949.901 — 3,963.189 mi) */
		/* chose longest - radius at equator */

		/// <summary>
		/// Calculates the shortest distance between two points on a sphere
		/// </summary>
		/// <param name="lat1">Latitude of first point</param>
		/// <param name="lng1">Longitude of first point</param>
		/// <param name="lat2">Latitude of second point</param>
		/// <param name="lng2">Longitude of second point</param>
		/// <param name="units"></param>
		/// <returns></returns>
		public static double CalcHaversineDistance(double lat1, double lng1, double lat2, double lng2, DistanceUnits units)
		{
			/*
				The Haversine formula according to Dr. Math.
				http://mathforum.org/library/drmath/view/51879.html
                
				dlng = lng2 - lng1
				dlat = lat2 - lat1
				a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlng/2))^2
				c = 2 * atan2(sqrt(a), sqrt(1-a)) 
				d = R * c
                
				Where
					* dlng is the change in longitude
					* dlat is the change in latitude
					* c is the great circle distance in Radians.
					* R is the radius of a spherical Earth.
					* The locations of the two points in spherical coordinates (longitude and latitude) are lng1,lat1 and lng2, lat2.
			*/
			double lat1InRad = lat1*RadianDegrees;
			double lng1InRad = lng1*RadianDegrees;

			double lat2InRad = lat2*RadianDegrees;
			double lng2InRad = lng2*RadianDegrees;

			double dlat = lat2InRad - lat1InRad;
			double dlng = lng2InRad - lng1InRad;

			// Intermediate result a.

			double a = Math.Pow(Math.Sin(dlat/2.0), 2.0)
			           + Math.Cos(lat1InRad)*Math.Cos(lat2InRad)
			           *Math.Pow(Math.Sin(dlng/2.0), 2.0);

			// Intermediate result c (great circle distance in Radians).

			double c = 2.0*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

			// Distance.
			double r;
			switch (units)
			{
				case DistanceUnits.Kilometers:
					r = EarthRadiusKilometers;
					break;
				case DistanceUnits.Miles:
					r = EarthRadiusMiles;
					break;
				default:
					throw new NotSupportedException();
			}

			return r*c;
		}

		public static double LatitudeMiles(int miles)
		{
			return LAT_MILE*miles;
		}

		public static double LongitudeMiles(int miles)
		{
			return LNG_MILE*miles;
		}
	}
}