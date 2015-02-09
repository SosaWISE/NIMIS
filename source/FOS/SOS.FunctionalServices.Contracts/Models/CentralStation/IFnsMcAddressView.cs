namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMcAddressView
	{
		#region Properties
		long AddressID { get; }
		int DealerId { get; }
		string CountryId { get; }
		string CountryName { get; }
		int TimeZoneId { get; }
		string TimeZoneAB { get; }
		string TimeZoneName { get; }
		string StreetAddress { get; }
		string StreetAddress2 { get; }
		string City { get; }
		string StateId { get; }
		string StateAB { get; }
		string PostalCode { get; }
		string PlusFour { get; }
		string County { get; }
		string Phone { get; }
		double Latitude { get; }
		double Longitude { get; }
		string CrossStreet { get; }

		#endregion Properties
	}
}