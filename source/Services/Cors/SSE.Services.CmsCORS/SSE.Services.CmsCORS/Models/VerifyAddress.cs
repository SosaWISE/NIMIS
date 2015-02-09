using System;
using SOS.Services.Interfaces.Models;

namespace SSE.Services.CmsCORS.Models
{
	public class VerifyAddress
	{
		#region .ctor
		public VerifyAddress() {}
		public VerifyAddress(SseAddress oItem)
		{
			AddressID = oItem.AddressId;
			DealerId = oItem.DealerId;
			TimeZoneId = oItem.TimeZoneId;
			TimeZone = oItem.TimeZone;
			StreetAddress = oItem.StreetAddress;
			StreetAddress2 = oItem.StreetAddress2;
			City = oItem.City;
			StateId = oItem.State;
			PostalCode = oItem.PostalCode;
			PlusFour = oItem.PlusFour;
			PhoneNumber = oItem.PhoneNumber;
			Latitude = oItem.Latitude;
			Longitude = oItem.Longitude;
			Validated = oItem.DPV;
		}

		#endregion .ctor

		#region Properties
		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public int TimeZoneId { get; set; }
		public string TimeZone { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string County { get; set; }
		public string PreDirectional { get; set; }
		public string PostDirectional { get; set; }
		public string StreetType { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public string CarrierRoute { get; set; }
		public string DPVResponse { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public bool Validated { get; set; }

		public string SalesRepId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
		public bool IsActive { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Properties
	}

}