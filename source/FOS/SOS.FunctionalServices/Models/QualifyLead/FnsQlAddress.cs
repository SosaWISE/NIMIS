using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;

namespace SOS.FunctionalServices.Models.QualifyLead
{
	public class FnsQlAddress : IFnsQlAddress
	{
		#region .ctor

		public FnsQlAddress(QL_Address address)
		{
			AddressID = address.AddressID;
			DealerId = address.DealerId;
			AddressTypeId = address.AddressTypeId;
			AddressValidationStateId = address.AddressValidationStateId;
			CarrierRoute = address.CarrierRoute;
			City = address.City;
			CongressionalDistric = address.CongressionalDistric;
			CountryId = address.CountryId;
			County = address.County;
			CountyCode = address.CountyCode;
			DeliveryPoint = address.DeliveryPoint;
			DPV = address.DPV;
			DPVFootnote = address.DPVFootnote;
			DPVResponse = address.DPVResponse;
			Extension = address.Extension;
			ExtensionNumber = address.ExtensionNumber;
			Latitude = address.Latitude;
			Longitude = address.Longitude;
			Phone = address.Phone;
			PlusFour = address.PlusFour;
			PostalCode = address.PostalCode;
			PostalCodeFull = address.PostalCodeFull;
			PostDirectional = address.PostDirectional;
			PreDirectional = address.PreDirectional;
			SalesRepId = address.SalesRepId;
			SeasonId = address.SeasonId;
			StateId = address.StateId;
			StreetAddress = address.StreetAddress;
			StreetAddress2 = address.StreetAddress2;
			StreetName = address.StreetName;
			StreetNumber = address.StreetNumber;
			StreetType = address.StreetType;
			TeamLocationId = address.TeamLocationId;
			TimeZoneId = address.TimeZoneId;
			Urbanization = address.Urbanization;
			UrbanizationCode = address.UrbanizationCode;
			ValidationVendorId = address.ValidationVendorId;
			IsActive = address.IsActive;
			IsDeleted = address.IsDeleted;
			CreatedBy = address.CreatedBy;
			CreatedOn = address.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		public long AddressID { get; private set; }
		public int DealerId { get; private set; }
		public string AddressTypeId { get; private set; }
		public string AddressValidationStateId { get; private set; }
		public string CarrierRoute { get; private set; }
		public string City { get; private set; }
		public int? CongressionalDistric { get; private set; }
		public string CountryId { get; private set; }
		public string County { get; private set; }
		public string CountyCode { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public string DeliveryPoint { get; private set; }
		public bool DPV { get; private set; }
		public string DPVFootnote { get; private set; }
		public string DPVResponse { get; private set; }
		public string Extension { get; private set; }
		public string ExtensionNumber { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public double Latitude { get; private set; }
		public double Longitude { get; private set; }
		public string Phone { get; private set; }
		public string PlusFour { get; private set; }
		public string PostalCode { get; private set; }
		public string PostalCodeFull { get; private set; }
		public string PostDirectional { get; private set; }
		public string PreDirectional { get; private set; }
		public string SalesRepId { get; private set; }
		public int SeasonId { get; private set; }
		public string StateId { get; private set; }
		public string StreetAddress { get; private set; }
		public string StreetAddress2 { get; private set; }
		public string StreetName { get; private set; }
		public string StreetNumber { get; private set; }
		public string StreetType { get; private set; }
		public int TeamLocationId { get; private set; }
		public int TimeZoneId { get; private set; }
		public string Urbanization { get; private set; }
		public string UrbanizationCode { get; private set; }
		public string ValidationVendorId { get; private set; }
		#endregion Properties
	}
}
