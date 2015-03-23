using System;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsVerifyAddress
	{
		long AddressID { get; set; }
		int DealerId { get; set; }
		int TimeZoneId { get; set; }
		//string TimeZone { get; set; }
		string StreetAddress { get; set; }
		string StreetAddress2 { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string PostalCode { get; set; }
		string PlusFour { get; set; }
		string County { get; set; }
		string PreDirectional { get; set; }
		string PostDirectional { get; set; }
		string StreetType { get; set; }
		string Extension { get; set; }
		string ExtensionNumber { get; set; }
		string CarrierRoute { get; set; }
		string DPVResponse { get; set; }
		string Phone { get; set; }
		string StreetNumber { get; set; }
		string StreetName { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
		bool DPV { get; set; }
		string SalesRepId { get; set; }
		int SeasonId { get; set; }
		int TeamLocationId { get; set; }
		bool IsActive { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }

		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }

		string ValidationVendorId { get; set; }
		string AddressValidationStateId { get; set; }
		string CountryId { get; set; }
		string AddressTypeId { get; set; }
		string CountyCode { get; set; }
		string Urbanization { get; set; }
		string UrbanizationCode { get; set; }
		string PostalCodeFull { get; set; }
		string DeliveryPoint { get; set; }
		string CrossStreet { get; set; }
		int? CongressionalDistric { get; set; }
		string DPVFootnote { get; set; }
		bool IsDeleted { get; set; }

		//string LeadId { get; set; }
		//string CustomerTypeId { get; set; }
		//string CustomerMasterFileId { get; set; }
		//string LocalizationId { get; set; }
		//string LeadSourceId { get; set; }
		//string LeadDispositionId { get; set; }
		//string LeadDispositionDateChange { get; set; }
		//string Salutation { get; set; }
		//string FirstName { get; set; }
		//string MiddleName { get; set; }
		//string LastName { get; set; }
		//string Suffix { get; set; }
		//string Gender { get; set; }
		//string SSN { get; set; }
		//string DOB { get; set; }
		//string Dl { get; set; }
		//string DlStateId { get; set; }
		//string Email { get; set; }
		//string PhoneHome { get; set; }
		//string PhoneWork { get; set; }
		//string PhoneMobile { get; set; }
	}
}
