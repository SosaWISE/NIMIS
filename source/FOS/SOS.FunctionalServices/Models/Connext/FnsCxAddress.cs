using System;
using NXS.Data.Connext;
using SOS.FunctionalServices.Contracts.Models.Connext;

namespace SOS.FunctionalServices.Models.Connext
{
	public class FnsCxAddress : IFnsCxAddress
	{
		#region .ctor

		public FnsCxAddress(CX_Address cxAddress)
		{
			AddressID = cxAddress.AddressID;
			DealerId = cxAddress.DealerId;
			AddressTypeId = cxAddress.AddressTypeId;
			AddressValidationStateId = cxAddress.AddressValidationStateId;
			CarrierRoute = cxAddress.CarrierRoute;
			City = cxAddress.City;
			CongressionalDistric = cxAddress.CongressionalDistric;
			CountryId = cxAddress.CountryId;
			County = cxAddress.County;
			CountyCode = cxAddress.CountyCode;
			DeliveryPoint = cxAddress.DeliveryPoint;
			DPV = cxAddress.DPV;
			DPVFootnote = cxAddress.DPVFootnote;
			DPVResponse = cxAddress.DPVResponse;
			Extension = cxAddress.Extension;
			ExtensionNumber = cxAddress.ExtensionNumber;
			Latitude = cxAddress.Latitude;
			Longitude = cxAddress.Longitude;
			Phone = cxAddress.Phone;
			PlusFour = cxAddress.PlusFour;
			PostalCode = cxAddress.PostalCode;
			PostalCodeFull = cxAddress.PostalCodeFull;
			PostDirectional = cxAddress.PostDirectional;
			PreDirectional = cxAddress.PreDirectional;
			SalesRepId = cxAddress.SalesRepId;
			SeasonId = cxAddress.SeasonId;
			StateId = cxAddress.StateId;
			StreetAddress = cxAddress.StreetAddress;
			StreetAddress2 = cxAddress.StreetAddress2;
			StreetName = cxAddress.StreetName;
			StreetNumber = cxAddress.StreetNumber;
			StreetType = cxAddress.StreetType;
			TeamLocationId = cxAddress.TeamLocationId;
			TimeZoneId = cxAddress.TimeZoneId;
			Urbanization = cxAddress.Urbanization;
			UrbanizationCode = cxAddress.UrbanizationCode;
			ValidationVendorId = cxAddress.ValidationVendorId;
			IsActive = cxAddress.IsActive;
			IsDeleted = cxAddress.IsDeleted;
			CreatedBy = cxAddress.CreatedBy;
			CreatedOn = cxAddress.CreatedOn;
		}

		public FnsCxAddress() { }

		#endregion .ctor

		#region Properties
		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public string AddressTypeId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string CarrierRoute { get; set; }
		public string City { get; set; }
		public int? CongressionalDistric { get; set; }
		public string CountryId { get; set; }
		public string County { get; set; }
		public string CountyCode { get; set; }
		public string DeliveryPoint { get; set; }
		public bool DPV { get; set; }
		public string DPVFootnote { get; set; }
		public string DPVResponse { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Phone { get; set; }
		public string PlusFour { get; set; }
		public string PostalCode { get; set; }
		public string PostalCodeFull { get; set; }
		public string PostDirectional { get; set; }
		public string PreDirectional { get; set; }
		public string SalesRepId { get; set; }
		public int SeasonId { get; set; }
		public string StateId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string StreetName { get; set; }
		public string StreetNumber { get; set; }
		public string StreetType { get; set; }
		public int TeamLocationId { get; set; }
		public int TimeZoneId { get; set; }
		public string Urbanization { get; set; }
		public string UrbanizationCode { get; set; }
		public string ValidationVendorId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		#endregion Properties
	}
}