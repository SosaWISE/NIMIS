using System;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{

	#region Model

	public class FnsGsAccountGeoFencePolygons : IFnsGsAccountGeoFencePolygons
	{
		public long GeoFencePolygonID { get; set; }
		public long GeoFenceId { get; set; }
		public int Sequence { get; set; }
		public double Lattitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}

	#endregion Model

}
