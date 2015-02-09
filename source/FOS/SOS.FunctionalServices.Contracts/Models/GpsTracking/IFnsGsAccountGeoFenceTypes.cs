using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsAccountGeoFenceTypes
	{
		[DataMember]
		string GeoFenceTypeID { get; set; }

		[DataMember]
		string GeoFenceType { get; set; }
	}
}
