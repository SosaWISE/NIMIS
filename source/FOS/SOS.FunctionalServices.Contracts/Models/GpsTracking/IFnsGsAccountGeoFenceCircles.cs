using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsAccountGeoFenceCircles
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
}
