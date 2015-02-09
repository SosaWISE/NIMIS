using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McAddress

	public interface IMcAddress
	{
		long AddressID { get; }
		int DealerId { get; set; }
		string ValidationVendorId { get; set; }
		string AddressStatusId { get; set; }
		string StateId { get; set; }
		string CountryId { get; set; }
		int TimeZoneId { get; set; }
		char AddressTypeId { get; set; }
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
		string DeliveryPoint { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
		int CongressionalDistric { get; set; }
		bool DPV { get; set; }
		string DPVResponse { get; set; }
		string DPVFootNote { get; set; }
		string CarrierRoute { get; set; }
	}

	public class McAddress : IMcAddress
	{
		#region .ctor
		public McAddress(long lAddressID)
		{
			AddressID = lAddressID;
		}

		public McAddress() { }

		#endregion .ctor

		#region Implementation of IMcAddress

		[DataMember]
		public long AddressID { get; private set; }
		[DataMember]
		public int DealerId { get; set; }
		[DataMember]
		public string ValidationVendorId { get; set; }
		[DataMember]
		public string AddressStatusId { get; set; }
		[DataMember]
		public string StateId { get; set; }
		[DataMember]
		public string CountryId { get; set; }
		[DataMember]
		public int TimeZoneId { get; set; }
		[DataMember]
		public char AddressTypeId { get; set; }
		[DataMember]
		public string StreetAddress { get; set; }
		[DataMember]
		public string StreetAddress2 { get; set; }
		[DataMember]
		public string StreetNumber { get; set; }
		[DataMember]
		public string StreetName { get; set; }
		[DataMember]
		public string StreetType { get; set; }
		[DataMember]
		public string PreDirectional { get; set; }
		[DataMember]
		public string PostDirectional { get; set; }
		[DataMember]
		public string Extension { get; set; }
		[DataMember]
		public string ExtensionNumber { get; set; }
		[DataMember]
		public string County { get; set; }
		[DataMember]
		public string CountyCode { get; set; }
		[DataMember]
		public string Urbanization { get; set; }
		[DataMember]
		public string UrbanizationCode { get; set; }
		[DataMember]
		public string City { get; set; }
		[DataMember]
		public string PostalCode { get; set; }
		[DataMember]
		public string PlusFour { get; set; }
		[DataMember]
		public string DeliveryPoint { get; set; }
		[DataMember]
		public double Latitude { get; set; }
		[DataMember]
		public double Longitude { get; set; }
		[DataMember]
		public int CongressionalDistric { get; set; }
		[DataMember]
		public bool DPV { get; set; }
		[DataMember]
		public string DPVResponse { get; set; }
		[DataMember]
		public string DPVFootNote { get; set; }
		[DataMember]
		public string CarrierRoute { get; set; }

		#endregion Implementation of IMcAddress
	}

	#endregion McAddress
}
