using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMcAddressView : IFnsMcAddressView
	{
		#region .ctor

		public FnsMcAddressView(MC_AddressesView view)
		{
			AddressID = view.AddressID;
			DealerId = view.DealerId;
			CountryId = view.CountryId;
			CountryName = view.CountryName;
			TimeZoneId = view.TimeZoneId;
			TimeZoneAB = view.TimeZoneAB;
			TimeZoneName = view.TimeZoneName;
			StreetAddress = view.StreetAddress;
			StreetAddress2 = view.StreetAddress2;
			City = view.City;
			StateId = view.StateId;
			StateAB = view.StateAB;
			PostalCode = view.PostalCode;
			PlusFour = view.PlusFour;
			County = view.County;
			Phone = view.Phone;
			Latitude = view.Latitude;
			Longitude = view.Longitude;
			CrossStreet = view.CrossStreet;
		}

		#endregion .ctor

		#region Properties

		public long AddressID { get; private set; }
		public int DealerId { get; private set; }
		public string CountryId { get; private set; }
		public string CountryName { get; private set; }
		public int TimeZoneId { get; private set; }
		public string TimeZoneAB { get; private set; }
		public string TimeZoneName { get; private set; }
		public string StreetAddress { get; private set; }
		public string StreetAddress2 { get; private set; }
		public string City { get; private set; }
		public string StateId { get; private set; }
		public string StateAB { get; private set; }
		public string PostalCode { get; private set; }
		public string PlusFour { get; private set; }
		public string County { get; private set; }
		public string Phone { get; private set; }
		public double Latitude { get; private set; }
		public double Longitude { get; private set; }
		public string CrossStreet { get; private set; }

		#endregion Properties
	}
}
