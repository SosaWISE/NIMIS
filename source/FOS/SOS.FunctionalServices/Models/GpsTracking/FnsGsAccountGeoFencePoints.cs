using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	#region Model
	public class FnsGsAccountGeoFencePoints : IFnsGsAccountGeoFencePoints
	{
		public FnsGsAccountGeoFencePoints(GS_AccountGeoFencePointsView oResulItem)
		{
			GeoFenceID = oResulItem.GeoFenceID;
			PlaceName = oResulItem.PlaceName;
			PlaceDescription = oResulItem.PlaceDescription;
			Lattitude = oResulItem.PointLatitude ?? 0;
			Longitude = oResulItem.PointLongitude ?? 0;
			CreatedOn = oResulItem.CreatedOn;
			CreatedBy = oResulItem.CreatedBy;
		}

		public FnsGsAccountGeoFencePoints(LP_CommandMessageAVRMCsView oResulItem)
		{
			PlaceName = "Current Location";
			PlaceDescription = string.Format("Current Location as of {0}", DateTime.UtcNow);
// ReSharper disable SpecifyACultureInStringConversionExplicitly
			Lattitude = (double) Lib.LaipacAPI.Helper.GPSUnit.GetLatitudeFromLapacDevice(oResulItem.Latitude.ToString(), oResulItem.NSIndicator);
			Longitude = (double) Lib.LaipacAPI.Helper.GPSUnit.GetLongitudeFromLaipacDevice(oResulItem.Longitude.ToString(), oResulItem.EWIndicator);
// ReSharper restore SpecifyACultureInStringConversionExplicitly
			CreatedOn = oResulItem.CreatedOn;
		}

		public long GeoFenceID { get; set; }
		public string PlaceName { get; set; }
		public string PlaceDescription { get; set; }
		public double Lattitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	#endregion Model
}
