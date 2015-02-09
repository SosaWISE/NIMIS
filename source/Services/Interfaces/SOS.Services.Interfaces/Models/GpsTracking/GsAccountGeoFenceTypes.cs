using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsAccountGeoFenceTypes
	{
		[DataMember]
		string GeoFenceTypeID { get; set; }

		[DataMember]
		string GeoFenceType { get; set; }
	}

	#region Model

	public class GsAccountGeoFenceTypes : IGsAccountGeoFenceTypes
	{
		public string GeoFenceTypeID { get; set; }
		public string GeoFenceType { get; set; }
	}

	#endregion Model
}
