namespace SOS.Services.Interfaces.Models.CmsModels
{
	public class McAddressView : IMcAddressView
	{
		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public string CountryId { get; set; }
		public string CountryName { get; set; }
		public int TimeZoneId { get; set; }
		public string TimeZoneAB { get; set; }
		public string TimeZoneName { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string StateAB { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string County { get; set; }
		public string Phone { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string CrossStreet { get; set; }
	}

	public interface IMcAddressView
	{
		#region Properties
		long AddressID { get; set; }
		int DealerId { get; set; }
		string CountryId { get; set; }
		string CountryName { get; set; }
		int TimeZoneId { get; set; }
		string TimeZoneAB { get; set; }
		string TimeZoneName { get; set; }
		string StreetAddress { get; set; }
		string StreetAddress2 { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string StateAB { get; set; }
		string PostalCode { get; set; }
		string PlusFour { get; set; }
		string County { get; set; }
		string Phone { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
		string CrossStreet { get; set; }

		#endregion Properties
	}
}
