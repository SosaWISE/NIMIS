using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	#region Models

	public class FnsGsAccountGeoFenceCircles : IFnsGsAccountGeoFenceCircles
	{
		public FnsGsAccountGeoFenceCircles(GS_AccountGeoFenceCircle oGsCircle)
		{
			GeoFenceID = oGsCircle.GeoFenceID;
			CenterLattitude = oGsCircle.CenterLattitude;
			CenterLongitude = oGsCircle.CenterLongitude;
			Radius = oGsCircle.Radius;
			CreatedOn = oGsCircle.CreatedOn;
			CreatedBy = oGsCircle.CreatedBy;
		}

		public long GeoFenceID { get; set; }
		public double CenterLattitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Radius { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}

	#endregion Models
}
