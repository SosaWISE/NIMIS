using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	#region MsAccountClientDetailsView

	public interface IMsAccountClientDetailsView
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

		[DataMember]
		string GpsWatchProductBarcodeId { get; set; }

		[DataMember]
		string GpsWatchPhoneNumber { get; set; }

		[DataMember]
		string GpsWatchUnitID { get; set; }

		[DataMember]
		bool MsAccountIsActive { get; set; }

		[DataMember]
		string IndustryNumber { get; set; }

		[DataMember]
		string Designator { get; set; }

		[DataMember]
		string SubscriberNumber { get; set; }

	}

	public class MsAccountClientDetailsView : IMsAccountClientDetailsView
	{
		public long CustomerAccountID { get; set; }
		public long CustomerId { get; set; }
		public long AccountId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public long AddressId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string County { get; set; }
		public string CountryId { get; set; }
		public long LeadId { get; set; }
		public string LocalizationId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool CustomerIsActive { get; set; }
		public long? IndustryAccountId { get; set; }
		public string SystemTypeId { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public string SimProductBarcodeId { get; set; }
		public string GpsWatchProductBarcodeId { get; set; }
		public string GpsWatchPhoneNumber { get; set; }
		public string GpsWatchUnitID { get; set; }
		public bool MsAccountIsActive { get; set; }
		public string IndustryNumber { get; set; }
		public string Designator { get; set; }
		public string SubscriberNumber { get; set; }
	}

	#endregion MsAccountClientDetailsView
}
