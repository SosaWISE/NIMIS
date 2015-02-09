using System;

namespace SSE.FOS.AddressVerification.Interfaces
{
	public interface IFosAddressVerified
	{
		long AddressID { get; }
		int DealerId { get; }
		string AddressValidationStateId { get; }
		string StateId { get; }
		string CountryId { get; }
		int TimeZoneId { get; }
		string TimeZone { get; }
		string AddressTypeId { get; }
		string StreetAddress { get; }
		string StreetAddress2 { get; }
		string StreetNumber { get; }
		string StreetName { get; }
		string StreetType { get; }
		string PreDirectional { get; }
		string PostDirectional { get; }
		string Extension { get; }
		string ExtensionNumber { get; }
		string County { get; }
		string CountyCode { get; }
		string Urbanization { get; }
		string UrbanizationCode { get; }
		string City { get; }
		string PostalCode { get; }
		string PlusFour { get; }
		string Phone { get; }
		string DeliveryPoint { get; }
		double Lattitude { get; }
		double Longitude { get; }
		int? CongressionalDistric { get; }
		bool DPV { get; }
		string DPVResponse { get; }
		string DPVFootnote { get; }
		string CarrierRoute { get; }
		bool IsActive { get; }
		bool IsDeleted { get; }
		string CreatedBy { get; }
		DateTime CreatedOn { get; }
	}
}