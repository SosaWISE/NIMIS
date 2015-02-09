using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	#region Model

	public class FnsGsAccountGeoFenceTypes : IFnsGsAccountGeoFenceTypes
	{
		public string GeoFenceTypeID { get; set; }
		public string GeoFenceType { get; set; }
	}

	#endregion Model
}
