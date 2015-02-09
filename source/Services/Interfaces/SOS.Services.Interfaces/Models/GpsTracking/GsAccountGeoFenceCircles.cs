using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsAccountGeoFenceCircles
	{
		[DataMember]
		long GeoFenceID { get; set; }

		[DataMember]
		double CenterLattitude { get; set; }

		[DataMember]
		double CenterLongitude { get; set; }

		[DataMember]
		double Radius { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

	}

	#region Models

	public class GsAccountGeoFenceCircles : IGsAccountGeoFenceCircles
	{
		public long GeoFenceID { get; set; }
		public double CenterLattitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Radius { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}

	#endregion Models
}
