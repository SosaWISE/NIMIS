using System;
using SOS.Data.SosCrm;
using SSE.FOS.AddressVerification.Interfaces;

namespace SSE.FOS.AddressVerification.Models
{
	public class FosAddressVerified : IFosAddressVerified
	{
		#region .ctor
		public FosAddressVerified(QL_Address address)
		{
			AddressID = address.AddressID;
			DealerId = address.DealerId;
			AddressValidationStateId = address.AddressValidationStateId;
			StateId = address.StateId;
			CountryId = address.CountryId;
			TimeZoneId = address.TimeZoneId;
			TimeZone = address.TimeZone.TimeZoneName;
			AddressTypeId = address.AddressTypeId;
			StreetAddress = address.StreetAddress;
			StreetAddress2 = address.StreetAddress2;
			StreetNumber = address.StreetNumber;
			StreetName = address.StreetName;
			StreetType = address.StreetType;
			PreDirectional = address.PreDirectional;
			PostDirectional = address.PostDirectional;
			Extension = address.Extension;
			ExtensionNumber = address.ExtensionNumber;
			County = address.County;
			CountyCode = address.CountyCode;
			Urbanization = address.Urbanization;
			UrbanizationCode = address.UrbanizationCode;
			City = address.City;
			PostalCode = address.PostalCode;
			PlusFour = address.PlusFour;
			Phone = address.Phone;
			DeliveryPoint = address.DeliveryPoint;
			Lattitude = address.Latitude;
			Longitude = address.Longitude;
			CongressionalDistric = address.CongressionalDistric;
			DPV = address.DPV;
			DPVResponse = address.DPVResponse;
			DPVFootnote = address.DPVFootnote;
			CarrierRoute = address.CarrierRoute;
			IsActive = address.IsActive;
			IsDeleted = address.IsDeleted;
			CreatedBy = address.CreatedBy;
			CreatedOn = address.CreatedOn;

		}
		#endregion .ctor

		#region Properties
		public long AddressID { get; private set; }
		public int DealerId { get; private set; }
		public string AddressValidationStateId { get; private set; }
		public string StateId { get; private set; }
		public string CountryId { get; private set; }
		public int TimeZoneId { get; private set; }
		public string TimeZone { get; private set; }
		public string AddressTypeId { get; private set; }
		public string StreetAddress { get; private set; }
		public string StreetAddress2 { get; private set; }
		public string StreetNumber { get; private set; }
		public string StreetName { get; private set; }
		public string StreetType { get; private set; }
		public string PreDirectional { get; private set; }
		public string PostDirectional { get; private set; }
		public string Extension { get; private set; }
		public string ExtensionNumber { get; private set; }
		public string County { get; private set; }
		public string CountyCode { get; private set; }
		public string Urbanization { get; private set; }
		public string UrbanizationCode { get; private set; }
		public string City { get; private set; }
		public string PostalCode { get; private set; }
		public string PlusFour { get; private set; }
		public string Phone { get; private set; }
		public string DeliveryPoint { get; private set; }
		public double Lattitude { get; private set; }
		public double Longitude { get; private set; }
		public int? CongressionalDistric { get; private set; }
		public bool DPV { get; private set; }
		public string DPVResponse { get; private set; }
		public string DPVFootnote { get; private set; }
		public string CarrierRoute { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		#endregion Properties
	}
}
