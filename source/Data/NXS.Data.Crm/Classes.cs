using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class AE_CustomerAccount // AE_CustomerAccounts
	{
		public long CustomerAccountID { get; set; }
		public long LeadId { get; set; }
		public long AccountId { get; set; }
		public long CustomerId { get; set; }
		public string CustomerTypeId { get; set; }
		public long AddressId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class AE_Customer // AE_Customers
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
		public string BusinessName { get; set; }
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
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class MC_Address // MC_Addresses
	{
		public long AddressID { get; set; }
		public long? QlAddressId { get; set; }
		public int DealerId { get; set; }
		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public int TimeZoneId { get; set; }
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
		public string CrossStreet { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int? CongressionalDistric { get; set; }
		public bool DPV { get; set; }
		public string DPVResponse { get; set; }
		public string DPVFootNote { get; set; }
		public string CarrierRoute { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class MS_AccountSalesInformation // MS_AccountSalesInformations
	{
		public long AccountID { get; set; }
		public string PaymentTypeId { get; set; }
		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
		public long? AccountFundingStatusId { get; set; }
		public short BillingDay { get; set; }
		public string Email { get; set; }
		public bool IsMoni { get; set; }
		public bool IsTakeOver { get; set; }
		public bool IsOwner { get; set; }
		public DateTime? InstallDate { get; set; }
		public DateTime? SubmittedToCSDate { get; set; }
		public string CsConfirmationNumber { get; set; }
		public string CsTwoWayConfNumber { get; set; }
		public DateTime? SubmittedToGPDate { get; set; }
		public DateTime? ContractSignedDate { get; set; }
		public DateTime? CancelDate { get; set; }
		public string AMA { get; set; }
		public string NOC { get; set; }
		public string SOP { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public string ApproverID { get; set; }
		public DateTime? NOCDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
		public long DEX_ROW_ID { get; set; }
	}
	public partial class QL_Address // QL_Address
	{
		public long AddressID { get; set; }
		public int DealerId { get; set; }
		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public int TimeZoneId { get; set; }
		public string AddressTypeId { get; set; }
		public int SeasonId { get; set; }
		public int TeamLocationId { get; set; }
		public string SalesRepId { get; set; }
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
		public string PostalCodeFull { get; set; }
		public string Phone { get; set; }
		public string DeliveryPoint { get; set; }
		public string CrossStreet { get; set; }
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
	}
	public partial class QL_CustomerMasterLead // QL_CustomerMasterLeads
	{
		public Guid CustomerMasterLeadID { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long LeadId { get; set; }
		public string CustomerTypeId { get; set; }
	}
	public partial class QL_LeadAddress // QL_LeadAddress
	{
		public long LeadAddressID { get; set; }
		public long LeadId { get; set; }
		public long AddressId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class QL_Lead // QL_Leads
	{
		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public DateTime? LeadDispositionDateChange { get; set; }
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
		public int? InsideSalesId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class TS_Team // TS_Teams
	{
		public int TeamId { get; set; }
		public long AddressId { get; set; }
		public int Version { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
	}
}
