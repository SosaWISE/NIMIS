using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountClientDetailsView
	{
		[DataMember]
		long CustomerAccountID { get; set; }

		[DataMember]
		long CustomerId { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string CustomerTypeId { get; set; }

		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		int DealerId { get; set; }

		[DataMember]
		long AddressId { get; set; }

		[DataMember]
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
		string CountryId { get; set; }

		[DataMember]
		long LeadId { get; set; }

		[DataMember]
		string LocalizationId { get; set; }

		[DataMember]
		string Prefix { get; set; }

		[DataMember]
		string FirstName { get; set; }

		[DataMember]
		string MiddleName { get; set; }

		[DataMember]
		string LastName { get; set; }

		[DataMember]
		string Postfix { get; set; }

		[DataMember]
		string Gender { get; set; }

		[DataMember]
		string PhoneHome { get; set; }

		[DataMember]
		string PhoneWork { get; set; }

		[DataMember]
		string PhoneMobile { get; set; }

		[DataMember]
		string Email { get; set; }

		[DataMember]
		DateTime? DOB { get; set; }

		[DataMember]
		string SSN { get; set; }

		[DataMember]
		string Username { get; set; }

		[DataMember]
		string Password { get; set; }

		[DataMember]
		bool CustomerIsActive { get; set; }

		[DataMember]
		long? IndustryAccountId { get; set; }

		[DataMember]
		string SystemTypeId { get; set; }

		[DataMember]
		string CellularTypeId { get; set; }

		[DataMember]
		string PanelTypeId { get; set; }

		[DataMember]
		string SimProductBarcodeId { get; set; }

		//[DataMember]
		//string GpsWatchProductBarcodeId { get; set; }

		//[DataMember]
		//string GpsWatchPhoneNumber { get; set; }

		//[DataMember]
		//string GpsWatchUnitID { get; set; }

		[DataMember]
		bool MsAccountIsActive { get; set; }

		[DataMember]
		string IndustryNumber { get; set; }

		[DataMember]
		string Designator { get; set; }

		[DataMember]
		string SubscriberNumber { get; set; }
		 
	}
}