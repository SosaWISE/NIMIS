using System;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Data.SosCrm;

namespace SOS.FunctionalServices.Models
{
	public class FnsVerifyAddress : IFnsVerifyAddress
	{
		public FnsVerifyAddress() { }

		public FnsVerifyAddress(QL_Address address)
		{
			AddressID = address.AddressID;
			DealerId = address.DealerId;
			TimeZoneId = address.TimeZoneId;
			TimeZone = address.TimeZone.TimeZoneName;
			Address = address.StreetAddress;
			Address2 = address.StreetAddress2;
			City = address.City;
			State = address.StateId;
			PostalCode = address.PostalCode;
			PlusFour = address.PlusFour;
			County = address.County;
			PreDirectional = address.PreDirectional;
			PostDirectional = address.PostDirectional;
			StreetType = address.StreetType;
			Extension = address.Extension;
			ExtensionNumber = address.ExtensionNumber;
			CarrierRoute = address.CarrierRoute;
			DPVResponse = address.DPVResponse;
			PhoneNumber = address.Phone;
			StreetNumber = address.StreetNumber;
			StreetName = address.StreetName;
			Latitude = address.Latitude;
			Longitude = address.Longitude;
			DPV = address.DPV;
			SalesRepId = address.SalesRepId;
			SeasonId = address.SeasonId;
			TeamLocationId = address.TeamLocationId;
			IsActive = address.IsActive;
			CreatedOn = address.CreatedOn;
			CreatedBy = address.CreatedBy;
			// fields here but not on QL_Address???
			//ModifiedOn = address.ModifiedOn;
			//ModifiedBy = address.ModifiedBy;

			ValidationVendorId = address.ValidationVendorId;
			AddressValidationStateId = address.AddressValidationStateId;
			CountryId = address.CountryId;
			AddressTypeId = address.AddressTypeId;
			CountyCode = address.CountyCode;
			Urbanization = address.Urbanization;
			UrbanizationCode = address.UrbanizationCode;
			PostalCodeFull = address.PostalCodeFull;
			DeliveryPoint = address.DeliveryPoint;
			CrossStreet = address.CrossStreet;
			CongressionalDistric = address.CongressionalDistric;
			DPVFootnote = address.DPVFootnote;
			IsDeleted = address.IsDeleted;
		}

		public void ToDb(QL_Address address)
		{
			if (address.AddressID != this.AddressID)
			{
				throw new Exception("AddressID's don't match");
			}

			address.DealerId = this.DealerId;
			address.TimeZoneId = this.TimeZoneId;
			address.StreetAddress = this.Address;
			address.StreetAddress2 = this.Address2;
			address.City = this.City;
			address.StateId = this.State;
			address.PostalCode = this.PostalCode;
			address.PlusFour = this.PlusFour;
			address.County = this.County;
			address.PreDirectional = this.PreDirectional;
			address.PostDirectional = this.PostDirectional;
			address.StreetType = this.StreetType;
			address.Extension = this.Extension;
			address.ExtensionNumber = this.ExtensionNumber;
			address.CarrierRoute = this.CarrierRoute;
			address.DPVResponse = this.DPVResponse;
			address.Phone = this.PhoneNumber;
			address.StreetNumber = this.StreetNumber;
			address.StreetName = this.StreetName;
			address.Latitude = this.Latitude;
			address.Longitude = this.Longitude;
			address.DPV = this.DPV;
			address.SalesRepId = this.SalesRepId;
			address.SeasonId = this.SeasonId;
			address.TeamLocationId = this.TeamLocationId;
			address.IsActive = this.IsActive;
			// don't copy over created
			//address.CreatedOn = this.CreatedOn;
			//address.CreatedBy = this.CreatedBy;
			// fields here but not on QL_Address???
			//address.ModifiedOn = this.ModifiedOn;
			//address.ModifiedBy = this.ModifiedBy;

			address.ValidationVendorId = this.ValidationVendorId;
			address.AddressValidationStateId = this.AddressValidationStateId;
			address.CountryId = this.CountryId;
			address.AddressTypeId = this.AddressTypeId;
			address.CountyCode = this.CountyCode;
			address.Urbanization = this.Urbanization;
			address.UrbanizationCode = this.UrbanizationCode;
			address.PostalCodeFull = this.PostalCodeFull;
			address.DeliveryPoint = this.DeliveryPoint;
			address.CrossStreet = this.CrossStreet;
			address.CongressionalDistric = this.CongressionalDistric;
			address.DPVFootnote = this.DPVFootnote;
			address.IsDeleted = this.IsDeleted;
		}


		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public int TimeZoneId { get; set; }
		public string TimeZone { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
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
		public bool DPV { get; set; }
		public string SalesRepId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }

		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string CountryId { get; set; }
		public string AddressTypeId { get; set; }
		public string CountyCode { get; set; }
		public string Urbanization { get; set; }
		public string UrbanizationCode { get; set; }
		public string PostalCodeFull { get; set; }
		public string DeliveryPoint { get; set; }
		public string CrossStreet { get; set; }
		public int? CongressionalDistric { get; set; }
		public string DPVFootnote { get; set; }
		public bool IsDeleted { get; set; }
	}
}
