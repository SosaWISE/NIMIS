using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsAccountGeoFencePoints
	{
		[DataMember]
		long GeoFenceId { get; set; }

		[DataMember]
		string PlaceName { get; set; }

		[DataMember]
		string PaceDescription { get; set; }

		[DataMember]
		double Lattitude { get; set; }

		[DataMember]
		double Longitude { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}

	#region Model
	public class GsAccountGeoFencePoints : IGsAccountGeoFencePoints
	{

		public long GeoFenceId { get; set; }
		public string PlaceName { get; set; }
		public string PaceDescription { get; set; }
		public double Lattitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	#endregion Model
}
