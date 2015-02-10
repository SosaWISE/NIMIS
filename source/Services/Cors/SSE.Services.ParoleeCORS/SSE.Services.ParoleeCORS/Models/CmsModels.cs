using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SOS.Services.Interfaces.Models;

namespace SSE.Services.ParoleeCORS.Models
{
	public static partial class CmsModels
	{
		#region McAddressType

		public interface IMcAddressType
		{
			string McAddressTypeID { get; set; }
			string McAddressTypeName { get; set; }
		}

		public class McAddressType : IMcAddressType
		{
			#region Implementation of IMcAddressType

			[DataMember]
			public string McAddressTypeID { get; set; }
			[DataMember]
			public string McAddressTypeName { get; set; }

			#endregion Implementation of IMcAddressType
		}

		#endregion McAddressType

		#region McAddressStatus

		public interface IMcAddressStatus
		{
			string McAddressStatusID { get; set; }
			string McAddressStatusName { get; set; }
		}

		public class McAddressStatus : IMcAddressStatus
		{
			#region Implementation of IMcAddressStatus

			[DataMember]
			public string McAddressStatusID { get; set; }
			[DataMember]
			public string McAddressStatusName { get; set; }

			#endregion Implementation of IMcAddressStatus
		}

		#endregion McAddressStatus

		#region McAddressValidationVendors

		public interface IMcAddressValidationVendors
		{
			string ValidationVendorID { get; set; }
			string ValidationVendorName { get; set; }
		}

		public class McAddressValidationVendors : IMcAddressValidationVendors
		{
			#region Implementation of IMcAddressValidationVendors

			[DataMember]
			public string ValidationVendorID { get; set; }
			[DataMember]
			public string ValidationVendorName { get; set; }

			#endregion Implementation of IMcAddressValidationVendors
		}

		#endregion McAddressValidationVendors

		#region McAddress

		public interface IMcAddress
		{
			long AddressID { get; }
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
			float Latitude { get; set; }
			float Longitude { get; set; }
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
			#endregion .ctor

			#region Implementation of IMcAddress

			[DataMember]
			public long AddressID { get; private set; }
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
			public float Latitude { get; set; }
			[DataMember]
			public float Longitude { get; set; }
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

		#region McPoliticalTimeZone
		public interface IMcPoliticalTimeZone
		{
			int TimeZoneID { get; set; }
			string TimeZoneName { get; set; }
			string TimeZoneAB { get; set; }
			string CentralTime { get; set; }
			int HourDifference { get; set; }
			bool IsActive { get; set; }
			bool IsDeleted { get; set; }
		}

		public class McPoliticalTimeZone : IMcPoliticalTimeZone
		{
			#region Implementation of IMcPoliticalTimeZone

			public int TimeZoneID { get; set; }
			public string TimeZoneName { get; set; }
			public string TimeZoneAB { get; set; }
			public string CentralTime { get; set; }
			public int HourDifference { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }

			#endregion Implementation of IMcPoliticalTimeZone
		}

		#endregion McPoliticalTimeZone

		#region QlLeadBasicView

		public interface IQlLeadBasicInfo
		{
			long LeadID { get; set; }
			long AddressId { get; set; }
			string CustomerTypeId { get; set; }
			long CustomerMasterFileId { get; set; }
			int DealerId { get; set; }
			string LocalizationId { get; set; }
			int TeamLocationId { get; set; }
			int SeasonId { get; set; }
			string SalesRepId { get; set; }
			string Salutation { get; set; }
			string FirstName { get; set; }
			string MiddleName { get; set; }
			string LastName { get; set; }
			string Suffix { get; set; }
			string SSN { get; set; }
			DateTime? DOB { get; set; }
			string DL { get; set; }
			string DLStateID { get; set; }
			string Email { get; set; }
			string PhoneHome { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			string PremisePhone { get; set; }
			string StreetAddress { get; set; }
			string City { get; set; }
			string StateId { get; set; }
			string Postal { get; set; }
			bool IsActive { get; set; }
		}
		public class QlLeadBasicView : IQlLeadBasicInfo
		{
			#region .ctor
			#endregion .ctor

			#region Implementation of IQlLeadBasicInfo

			public long LeadID { get; set; }
			public long AddressId { get; set; }
			public string CustomerTypeId { get; set; }
			public long CustomerMasterFileId { get; set; }
			public int DealerId { get; set; }
			public string LocalizationId { get; set; }
			public int TeamLocationId { get; set; }
			public int SeasonId { get; set; }
			public string SalesRepId { get; set; }
			public string Salutation { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string LastName { get; set; }
			public string Suffix { get; set; }
			public string SSN { get; set; }
			public DateTime? DOB { get; set; }
			public string DL { get; set; }
			public string DLStateID { get; set; }
			public string Email { get; set; }
			public string PhoneHome { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneMobile { get; set; }
			public string PremisePhone { get; set; }
			public string StreetAddress { get; set; }
			public string City { get; set; }
			public string StateId { get; set; }
			public string Postal { get; set; }
			public bool IsActive { get; set; }

			#endregion Implementation of IQlLeadBasicInfo
		}

		#endregion QlLeadBasicView

		#region QlLeadSearchResultView

		public interface IQlLeadSearchResultView
		{
			long CustomerMasterFileId { get; set; }
			long LeadId { get; set; }
			int DealerId { get; set; }
			string LocalizationId { get; set; }
			int DispositionId { get; set; }
			string Disposition { get; set; }
			int SourceId { get; set; }
			string Source { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			string PhoneHome { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			DateTime? DOB { get; set; }
			string SalesRepId { get; set; }
			string SSN { get; set; }
			string DL { get; set; }
			string DLStateID { get; set; }
			string Email { get; set; }
			bool? IsCustomer { get; set; }
			int? RowNum { get; set; }
		}

		public class QlLeadSearchResultView : IQlLeadSearchResultView
		{
			#region .ctor

			#endregion .ctor

			#region Implementation of IQlLeadSearchResultView

			public long CustomerMasterFileId { get; set; }
			public long LeadId { get; set; }
			public int DealerId { get; set; }
			public string LocalizationId { get; set; }
			public string Disposition { get; set; }
			public int DispositionId { get; set; }
			public string Source { get; set; }
			public int SourceId { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string PhoneHome { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneMobile { get; set; }
			public DateTime? DOB { get; set; }
			public string SalesRepId { get; set; }
			public string SSN { get; set; }
			public string DL { get; set; }
			public string DLStateID { get; set; }
			public string Email { get; set; }
			public bool? IsCustomer { get; set; }
			public int? RowNum { get; set; }

			#endregion
		}

		#endregion QlLeadSearchResultView

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

		#region QlLeadFullData

		public interface IQlLeadFullData
		{
			QlAddress Address { get; set; }
			AeCustomerType CustomerType { get; set; }
			List<QlLeadProductOffer> ProductSkwIdList { get; set; }
			long LeadID { get; set; }
			string CustomerTypeId { get; set; }
			long CustomerMasterFileId { get; set; }
			int DealerId { get; set; }
			AeDealer Dealer { get; set; }
			string LocalizationId { get; set; }
			McLocalization Localization { get; set; }
			int TeamLocationId { get; set; }
			int LeadSourceId { get; set; }
			string LeadSource { get; set; }
			int LeadDispositionId { get; set; }
			string LeadDisposition { get; set; }
			DateTime? LeadDispositionDateChange { get; set; }
			int SeasonId { get; set; }
			string SalesRepId { get; set; }
			RuModels.IRuUser SalesRep { get; set; }
			string Salutation { get; set; }
			string FirstName { get; set; }
			string MiddleName { get; set; }
			string LastName { get; set; }
			string Suffix { get; set; }
			string Gender { get; set; }
			string SSN { get; set; }
			DateTime? DOB { get; set; }
			string DL { get; set; }
			string DLStateID { get; set; }
			string Email { get; set; }
			string PhoneHome { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			bool IsActive { get; set; }
			bool IsDeleted { get; set; }
			DateTime CreatedOn { get; set; }
			string CreatedBy { get; set; }
		}

		public class QlLeadFullData : IQlLeadFullData
		{
			#region Implementation of IQlLeadFullData

			public QlAddress Address { get; set; }
			public AeCustomerType CustomerType { get; set; }
			public List<QlLeadProductOffer> ProductSkwIdList { get; set; }
			public long LeadID { get; set; }
			public string CustomerTypeId { get; set; }
			public long CustomerMasterFileId { get; set; }
			public int DealerId { get; set; }
			public AeDealer Dealer { get; set; }
			public string LocalizationId { get; set; }
			public McLocalization Localization { get; set; }
			public int TeamLocationId { get; set; }
			public int LeadSourceId { get; set; }
			public string LeadSource { get; set; }
			public int LeadDispositionId { get; set; }
			public string LeadDisposition { get; set; }
			public DateTime? LeadDispositionDateChange { get; set; }
			public int SeasonId { get; set; }
			public string SalesRepId { get; set; }
			public RuModels.IRuUser SalesRep { get; set; }
			public string Salutation { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string LastName { get; set; }
			public string Suffix { get; set; }
			public string Gender { get; set; }
			public string SSN { get; set; }
			public DateTime? DOB { get; set; }
			public string DL { get; set; }
			public string DLStateID { get; set; }
			public string Email { get; set; }
			public string PhoneHome { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneMobile { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }
			public DateTime CreatedOn { get; set; }
			public string CreatedBy { get; set; }

			#endregion Implementation of IQlLeadFullData
		}

		#endregion QlLeadFullData

		#region QlLeadProductOffer

		public interface IQlLeadProductOffer
		{
			long LeadProductOfferedId { get; set; }
			long LeadId { get; set; }
			string ProductSkwId { get; set; }
			string ProductName { get; set; }
			string ShortName { get; set; }
			string ProductTypeName { get; set; }
			string ProductImageName { get; set; }
			string SalesRepId { get; set; }
			string SalesRepFullName { get; set; }
			DateTime OfferDate { get; set; }
		}

		public class QlLeadProductOffer : IQlLeadProductOffer
		{

			#region Implementation of IQlLeadProductOffer

			public long LeadProductOfferedId { get; set; }
			public long LeadId { get; set; }
			public string ProductSkwId { get; set; }
			public string ProductName { get; set; }
			public string ShortName { get; set; }
			public string ProductTypeName { get; set; }
			public string ProductImageName { get; set; }
			public string SalesRepId { get; set; }
			public string SalesRepFullName { get; set; }
			public DateTime OfferDate { get; set; }

			#endregion Implementation of IQlLeadProductOffer
		}

		#endregion QlLeadProductOffer

		#region AeCustomerType
		public interface IAeCustomerType
		{
			string CustomerTypeID { get; set; }
			string CustomerType { get; set; }
		}

		public class AeCustomerType : IAeCustomerType
		{
			#region Implementation of IAeCustomerType

			public string CustomerTypeID { get; set; }
			public string CustomerType { get; set; }

			#endregion Implementation of IAeCustomerType
		}

		#endregion AeCustomerType

		#region AeCustomer

		public class AeCustomer : IAeCustomer
		{
			public long CustomerID { get; set; }
			public string CustomerTypeId { get; set; }
			public long CustomerMasterFileId { get; set; }
			public int DealerId { get; set; }
			public long AddressId { get; set; }
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
			public bool IsActive { get; set; }
			public DateTime ModifiedOn { get; set; }
			public string ModifiedBy { get; set; }
			public DateTime CreatedOn { get; set; }
			public string CreatedBy { get; set; }
			public string StateId { get; set; }
			public string CountryId { get; set; }
			public string TimezoneId { get; set; }
			public string StreetAddress { get; set; }
			public string StreetAddress2 { get; set; }
			public string County { get; set; }
			public string City { get; set; }
			public string PostalCode { get; set; }
			public string PlusFour { get; set; }
			public string Phone { get; set; }
		}

		public interface IAeCustomer
		{
			long CustomerID { get; set; }
			string CustomerTypeId { get; set; }
			long CustomerMasterFileId { get; set; }
			int DealerId { get; set; }
			long AddressId { get; set; }
			long LeadId { get; set; }
			string LocalizationId { get; set; }
			string Prefix { get; set; }
			string FirstName { get; set; }
			string MiddleName { get; set; }
			string LastName { get; set; }
			string Postfix { get; set; }
			string Gender { get; set; }
			string PhoneHome { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			string Email { get; set; }
			DateTime? DOB { get; set; }
			string SSN { get; set; }
			string Username { get; set; }
			string Password { get; set; }
			bool IsActive { get; set; }
			DateTime ModifiedOn { get; set; }
			string ModifiedBy { get; set; }
			DateTime CreatedOn { get; set; }
			string CreatedBy { get; set; }
		}

		#endregion AeCustomer

		#region AeDealer
		public interface IAeDealer
		{
			int DealerID { get; set; }
			string DealerName { get; set; }
			string ContactFirstName { get; set; }
			string ContactLastName { get; set; }
			string ContactEmail { get; set; }
			string PhoneWork { get; set; }
			string PhoneMobile { get; set; }
			string PhoneFax { get; set; }
			string Address { get; set; }
			string Address2 { get; set; }
			string City { get; set; }
			string StateAB { get; set; }
			string PostalCode { get; set; }
			string PlusFour { get; set; }
			string Username { get; set; }
			string Password { get; set; }
			bool IsActive { get; set; }
			bool IsDeleted { get; set; }
			DateTime ModifiedOn { get; set; }
			string ModifiedBy { get; set; }
			DateTime CreatedOn { get; set; }
			string CreatedBy { get; set; }
			// ReSharper disable InconsistentNaming
			DateTime DEX_ROW_TS { get; set; }
			// ReSharper restore InconsistentNaming
		}

		public class AeDealer : IAeDealer
		{
			#region Implementation of IAeDealer

			public int DealerID { get; set; }
			public string DealerName { get; set; }
			public string ContactFirstName { get; set; }
			public string ContactLastName { get; set; }
			public string ContactEmail { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneMobile { get; set; }
			public string PhoneFax { get; set; }
			public string Address { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string StateAB { get; set; }
			public string PostalCode { get; set; }
			public string PlusFour { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }
			public DateTime ModifiedOn { get; set; }
			public string ModifiedBy { get; set; }
			public DateTime CreatedOn { get; set; }
			public string CreatedBy { get; set; }
			public DateTime DEX_ROW_TS { get; set; }

			#endregion Implementation of IAeDealer
		}

		#endregion AeDealer

		#region McLocalization

		public interface IMcLocalization
		{
			string LocalizationID { get; set; }
			int MSLocalId { get; set; }
			string LocalizationName { get; set; }
			bool IsActive { get; set; }
			bool IsDeleted { get; set; }
		}

		public class McLocalization : IMcLocalization
		{
			#region Implementation of IMcLocalization

			public string LocalizationID { get; set; }
			public int MSLocalId { get; set; }
			public string LocalizationName { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }

			#endregion Implementation of IMcLocalization
		}

		#endregion McLocalization

		#region McAccountNotesFullInfoView

		public interface IMcAccountNotesFullInfoView
		{
			long NoteID { get; set; }
			string NoteTypeID { get; set; }
			string NoteType { get; set; }
			long CustomerMasterFileId { get; set; }
			long? CustomerId { get; set; }
			long? LeadId { get; set; }
			int NoteCategory1ID { get; set; }
			string Category1 { get; set; }
			string Desc1 { get; set; }
			int NoteCategory2ID { get; set; }
			string Category2 { get; set; }
			string Desc2 { get; set; }
			string Note { get; set; }
			string CreatedBy { get; set; }
			DateTime CreatedOn { get; set; }

		}

		public class McAccountNotesFullInfoView : IMcAccountNotesFullInfoView
		{
			#region Implementation of IMcAccountNotesFullInfoView

			public long NoteID { get; set; }
			public string NoteTypeID { get; set; }
			public string NoteType { get; set; }
			public long CustomerMasterFileId { get; set; }
			public long? CustomerId { get; set; }
			public long? LeadId { get; set; }
			public int NoteCategory1ID { get; set; }
			public string Category1 { get; set; }
			public string Desc1 { get; set; }
			public int NoteCategory2ID { get; set; }
			public string Category2 { get; set; }
			public string Desc2 { get; set; }
			public string Note { get; set; }
			public string CreatedBy { get; set; }
			public DateTime CreatedOn { get; set; }

			#endregion Implementation of IMcAccountNotesFullInfoView
		}

		#endregion McAccountNotesFullInfoView

		#region McAccountNotes

		public interface IMcAccountNote
		{
			long NoteID { get; set; }
			string NoteTypeId { get; set; }
			long CustomerMasterFileId { get; set; }
			long? CustomerId { get; set; }
			long? LeadId { get; set; }
			int NoteCategory1Id { get; set; }
			int? NoteCategory2Id { get; set; }
			string Note { get; set; }
			string CreatedBy { get; set; }
			DateTime CreatedOn { get; set; }
		}

		public class McAccountNote : IMcAccountNote
		{
			#region Implementation of IMcAccountNote

			public long NoteID { get; set; }
			public string NoteTypeId { get; set; }
			public long CustomerMasterFileId { get; set; }
			public long? CustomerId { get; set; }
			public long? LeadId { get; set; }
			public int NoteCategory1Id { get; set; }
			public int? NoteCategory2Id { get; set; }
			public string Note { get; set; }
			public string CreatedBy { get; set; }
			public DateTime CreatedOn { get; set; }

			#endregion Implementation of IMcAccountNote
		}

		#endregion McAccountNotes

		#region MsAccountClientsView

		public interface IMsAccountClientsView
		{
			[DataMember]
			long CustomerMasterFileId { get; set; }

			[DataMember]
			long CustomerID { get; set; }

			[DataMember]
			long AccountId { get; set; }

			[DataMember]
			string UnitID { get; set; }

			[DataMember]
			string AccountName { get; set; }

			[DataMember]
			long? EventID { get; set; }

			[DataMember]
			DateTime? EventDate { get; set; }

			[DataMember]
			string LastLatt { get; set; }

			[DataMember]
			string LastLong { get; set; }

			[DataMember]
			string UIName { get; set; }

			[DataMember]
			string Username { get; set; }

			[DataMember]
			string Password { get; set; }

			[DataMember]
			string CustomerTypeId { get; set; }

			[DataMember]
			string SystemTypeId { get; set; }

			[DataMember]
			string PanelTypeId { get; set; }

			[DataMember]
			string InvItemId { get; set; }

			[DataMember]
			long? IndustryAccountId { get; set; }

			[DataMember]
			string IndustryNumber { get; set; }

			[DataMember]
			string Designator { get; set; }

			[DataMember]
			string SubscriberNumber { get; set; }

		}

		public class MsAccountClientsView : IMsAccountClientsView
		{
			#region Properties

			public long CustomerMasterFileId { get; set; }
			public long CustomerID { get; set; }
			public long AccountId { get; set; }
			public string AccountName { get; set; }
			public long? EventID { get; set; }
			public DateTime? EventDate { get; set; }
			public string LastLatt { get; set; }
			public string LastLong { get; set; }
			public string UIName { get; set; }
			public string UnitID { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
			public string CustomerTypeId { get; set; }
			public string SystemTypeId { get; set; }
			public string PanelTypeId { get; set; }
			public string InvItemId { get; set; }
			public long? IndustryAccountId { get; set; }
			public string IndustryNumber { get; set; }
			public string Designator { get; set; }
			public string SubscriberNumber { get; set; }

			#endregion Properties

		}

		#endregion MsAccountClientsView

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
}