using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlAddress
	{
		[DataMember]
		long AddressID { get; }
		[DataMember]
		int DealerId { get; }
		[DataMember]
		string AddressTypeId { get; }
		[DataMember]
		string AddressValidationStateId { get; }
		[DataMember]
		string CarrierRoute { get; }
		[DataMember]
		string City { get; }
		[DataMember]
		int? CongressionalDistric { get; }
		[DataMember]
		string CountryId { get; }
		[DataMember]
		string County { get; }
		[DataMember]
		string CountyCode { get; }
		[DataMember]
		string DeliveryPoint { get; }
		[DataMember]
		bool DPV { get; }
		[DataMember]
		string DPVFootnote { get; }
		[DataMember]
		string DPVResponse { get; }
		[DataMember]
		string Extension { get; }
		[DataMember]
		string ExtensionNumber { get; }
		[DataMember]
		double Latitude { get; }
		[DataMember]
		double Longitude { get; }
		[DataMember]
		string Phone { get; }
		[DataMember]
		string PlusFour { get; }
		[DataMember]
		string PostalCode { get; }
		[DataMember]
		string PostalCodeFull { get; }
		[DataMember]
		string PostDirectional { get; }
		[DataMember]
		string PreDirectional { get; }
		[DataMember]
		string SalesRepId { get; }
		[DataMember]
		int SeasonId { get; }
		[DataMember]
		string StateId { get; }
		[DataMember]
		string StreetAddress { get; }
		[DataMember]
		string StreetAddress2 { get; }
		[DataMember]
		string StreetName { get; }
		[DataMember]
		string StreetNumber { get; }
		[DataMember]
		string StreetType { get; }
		[DataMember]
		int TeamLocationId { get; }
		[DataMember]
		int TimeZoneId { get; }
		[DataMember]
		string Urbanization { get; }
		[DataMember]
		string UrbanizationCode { get; }
		[DataMember]
		string ValidationVendorId { get; }
		[DataMember]
		bool IsActive { get; }
		[DataMember]
		bool IsDeleted { get; }
		[DataMember]
		string CreatedBy { get; }
		[DataMember]
		DateTime CreatedOn { get; }

	}
}