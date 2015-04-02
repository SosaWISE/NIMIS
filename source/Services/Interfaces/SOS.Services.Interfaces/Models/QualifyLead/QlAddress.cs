using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.QualifyLead
{
	public class QlAddress : IQlAddress
	{
		#region Properties
		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public int TimeZoneId { get; set; }
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
		public string Phone { get; set; }
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
		#endregion Properties
	}

	public interface IQlAddress
	{
		#region Properties
		[DataMember]
		long AddressID { get; set; }
		[DataMember]
		int DealerId { get; set; }
		[DataMember]
		int TimeZoneId { get; set; }
		[DataMember]
		//string TimeZone { get; set; }
		string StreetAddress { get; set; }
		[DataMember]
		string StreetAddress2 { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string PlusFour { get; set; }
		[DataMember]
		string County { get; set; }
		[DataMember]
		string PreDirectional { get; set; }
		[DataMember]
		string PostDirectional { get; set; }
		[DataMember]
		string StreetType { get; set; }
		[DataMember]
		string Extension { get; set; }
		[DataMember]
		string ExtensionNumber { get; set; }
		[DataMember]
		string CarrierRoute { get; set; }
		[DataMember]
		string DPVResponse { get; set; }
		[DataMember]
		string Phone { get; set; }
		[DataMember]
		string StreetNumber { get; set; }
		[DataMember]
		string StreetName { get; set; }
		[DataMember]
		double Latitude { get; set; }
		[DataMember]
		double Longitude { get; set; }
		[DataMember]
		bool DPV { get; set; }
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		int TeamLocationId { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
		[DataMember]
		string CreatedBy { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }
		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		string ValidationVendorId { get; set; }
		[DataMember]
		string AddressValidationStateId { get; set; }
		[DataMember]
		string CountryId { get; set; }
		[DataMember]
		string AddressTypeId { get; set; }
		[DataMember]
		string CountyCode { get; set; }
		[DataMember]
		string Urbanization { get; set; }
		[DataMember]
		string UrbanizationCode { get; set; }
		[DataMember]
		string PostalCodeFull { get; set; }
		[DataMember]
		string DeliveryPoint { get; set; }
		[DataMember]
		string CrossStreet { get; set; }
		[DataMember]
		int? CongressionalDistric { get; set; }
		[DataMember]
		string DPVFootnote { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }

		#endregion Properties
	}
}
