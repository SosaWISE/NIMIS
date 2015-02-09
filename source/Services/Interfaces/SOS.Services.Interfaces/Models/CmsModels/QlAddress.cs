using System;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region QlAddress

	public interface IQlAddress
	{
		long AddressID { get; set; }
		int DealerId { get; set; }
		string ValidationVendorId { get; set; }
		string AddressValidationStateId { get; set; }
		string StateId { get; set; }
		string CountryId { get; set; }
		int TimeZoneId { get; set; }
		McPoliticalTimeZone TimeZone { get; set; }
		string AddressTypeId { get; set; }
		string StreetAddress { get; set; }
		string StreetAddress2 { get; set; }
		string StreetNumber { get; set; }
		string StreetName { get; set; }
		string StreetType { get; set; }
		string PreDirectional { get; set; }
		string PostDirectional { get; set; }
		string Extension { get; set; }
		string ExtensionNumber { get; set; }
		string County { get; set; }
		string CountyCode { get; set; }
		string Urbanization { get; set; }
		string UrbanizationCode { get; set; }
		string City { get; set; }
		string PostalCode { get; set; }
		string PlusFour { get; set; }
		string Phone { get; set; }
		string DeliveryPoint { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
		int? CongressionalDistric { get; set; }
		bool DPV { get; set; }
		string DPVResponse { get; set; }
		string DPVFootnote { get; set; }
		string CarrierRoute { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		string CreatedBy { get; set; }
		DateTime CreatedOn { get; set; }
	}

	public class QlAddress : IQlAddress
	{
		#region Implementation of IQlAddress

		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public int TimeZoneId { get; set; }
		public McPoliticalTimeZone TimeZone { get; set; }
		public string AddressTypeId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string StreetType { get; set; }
		public string PreDirectional { get; set; }
		public string PostDirectional { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public string County { get; set; }
		public string CountyCode { get; set; }
		public string Urbanization { get; set; }
		public string UrbanizationCode { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Phone { get; set; }
		public string DeliveryPoint { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int? CongressionalDistric { get; set; }
		public bool DPV { get; set; }
		public string DPVResponse { get; set; }
		public string DPVFootnote { get; set; }
		public string CarrierRoute { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion
	}

	#endregion QlAddress
}
