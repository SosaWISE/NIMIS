using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsAccountGeoFencePoints
	{
		[DataMember]
		long GeoFenceID { get; set; }

		[DataMember]
		string PlaceName { get; set; }

		[DataMember]
		string PlaceDescription { get; set; }

		[DataMember]
		double Lattitude { get; set; }

		[DataMember]
		double Longitude { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}
}
