using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Connext
{
	public interface IFnsCxAddress
	{
		[DataMember]
		long AddressID { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		string AddressTypeId { get; set; }
		[DataMember]
		string AddressValidationStateId { get; set; }
		[DataMember]
		string CarrierRoute { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		int? CongressionalDistric { get; set; }
		[DataMember]
		string CountryId { get; set; }
		[DataMember]
		string County { get; set; }
		[DataMember]
		string CountyCode { get; set; }
		[DataMember]
		string DeliveryPoint { get; set; }
		[DataMember]
		bool DPV { get; set; }
		[DataMember]
		string DPVFootnote { get; set; }
		[DataMember]
		string DPVResponse { get; set; }
		[DataMember]
		string Extension { get; set; }
		[DataMember]
		string ExtensionNumber { get; set; }
		[DataMember]
		double Latitude { get; set; }
		[DataMember]
		double Longitude { get; set; }
		[DataMember]
		string Phone { get; set; }
		[DataMember]
		string PlusFour { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string PostalCodeFull { get; set; }
		[DataMember]
		string PostDirectional { get; set; }
		[DataMember]
		string PreDirectional { get; set; }
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string StreetAddress { get; set; }
		[DataMember]
		string StreetAddress2 { get; set; }
		[DataMember]
		string StreetName { get; set; }
		[DataMember]
		string StreetNumber { get; set; }
		[DataMember]
		string StreetType { get; set; }
		[DataMember]
		int TeamLocationId { get; set; }
		[DataMember]
		int TimeZoneId { get; set; }
		[DataMember]
		string Urbanization { get; set; }
		[DataMember]
		string UrbanizationCode { get; set; }
		[DataMember]
		string ValidationVendorId { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }

	}
}