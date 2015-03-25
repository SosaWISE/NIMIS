using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class CrmDb : Database<CrmDb>
	{
		public AE_CustomerAccountTable AE_CustomerAccounts { get; set; }
		public AE_CustomerTable AE_Customers { get; set; }
		public MC_AddressTable MC_Addresses { get; set; }
		public MS_AccountSalesInformationTable MS_AccountSalesInformations { get; set; }
		public QL_AddressTable QL_Addresses { get; set; }
		public QL_CustomerMasterLeadTable QL_CustomerMasterLeads { get; set; }
		public QL_LeadAddressTable QL_LeadAddresses { get; set; }
		public QL_LeadTable QL_Leads { get; set; }
		public TS_TeamTable TS_Teams { get; set; }

		public partial class AE_CustomerAccountTable : Table<AE_CustomerAccount, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_CustomerAccountTable(CrmDb db) : base(db, "AeCA", "[dbo].[AE_CustomerAccounts]", "CustomerAccountID", "bigint") { }
			public string CustomerAccountID { get { return _alias + "[CustomerAccountID]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string AccountId { get { return _alias + "[AccountId]"; } }
			public string CustomerId { get { return _alias + "[CustomerId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class AE_CustomerTable : Table<AE_Customer, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_CustomerTable(CrmDb db) : base(db, "AeC", "[dbo].[AE_Customers]", "CustomerID", "bigint") { }
			public string CustomerID { get { return _alias + "[CustomerID]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string LocalizationId { get { return _alias + "[LocalizationId]"; } }
			public string Prefix { get { return _alias + "[Prefix]"; } }
			public string FirstName { get { return _alias + "[FirstName]"; } }
			public string MiddleName { get { return _alias + "[MiddleName]"; } }
			public string LastName { get { return _alias + "[LastName]"; } }
			public string Postfix { get { return _alias + "[Postfix]"; } }
			public string BusinessName { get { return _alias + "[BusinessName]"; } }
			public string Gender { get { return _alias + "[Gender]"; } }
			public string PhoneHome { get { return _alias + "[PhoneHome]"; } }
			public string PhoneWork { get { return _alias + "[PhoneWork]"; } }
			public string PhoneMobile { get { return _alias + "[PhoneMobile]"; } }
			public string Email { get { return _alias + "[Email]"; } }
			public string DOB { get { return _alias + "[DOB]"; } }
			public string SSN { get { return _alias + "[SSN]"; } }
			public string Username { get { return _alias + "[Username]"; } }
			public string Password { get { return _alias + "[Password]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class MC_AddressTable : Table<MC_Address, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MC_AddressTable(CrmDb db) : base(db, "McA", "[dbo].[MC_Addresses]", "AddressID", "bigint") { }
			public string AddressID { get { return _alias + "[AddressID]"; } }
			public string QlAddressId { get { return _alias + "[QlAddressId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string ValidationVendorId { get { return _alias + "[ValidationVendorId]"; } }
			public string AddressValidationStateId { get { return _alias + "[AddressValidationStateId]"; } }
			public string StateId { get { return _alias + "[StateId]"; } }
			public string CountryId { get { return _alias + "[CountryId]"; } }
			public string TimeZoneId { get { return _alias + "[TimeZoneId]"; } }
			public string AddressTypeId { get { return _alias + "[AddressTypeId]"; } }
			public string StreetAddress { get { return _alias + "[StreetAddress]"; } }
			public string StreetAddress2 { get { return _alias + "[StreetAddress2]"; } }
			public string StreetNumber { get { return _alias + "[StreetNumber]"; } }
			public string StreetName { get { return _alias + "[StreetName]"; } }
			public string StreetType { get { return _alias + "[StreetType]"; } }
			public string PreDirectional { get { return _alias + "[PreDirectional]"; } }
			public string PostDirectional { get { return _alias + "[PostDirectional]"; } }
			public string Extension { get { return _alias + "[Extension]"; } }
			public string ExtensionNumber { get { return _alias + "[ExtensionNumber]"; } }
			public string County { get { return _alias + "[County]"; } }
			public string CountyCode { get { return _alias + "[CountyCode]"; } }
			public string Urbanization { get { return _alias + "[Urbanization]"; } }
			public string UrbanizationCode { get { return _alias + "[UrbanizationCode]"; } }
			public string City { get { return _alias + "[City]"; } }
			public string PostalCode { get { return _alias + "[PostalCode]"; } }
			public string PlusFour { get { return _alias + "[PlusFour]"; } }
			public string Phone { get { return _alias + "[Phone]"; } }
			public string DeliveryPoint { get { return _alias + "[DeliveryPoint]"; } }
			public string CrossStreet { get { return _alias + "[CrossStreet]"; } }
			public string Latitude { get { return _alias + "[Latitude]"; } }
			public string Longitude { get { return _alias + "[Longitude]"; } }
			public string CongressionalDistric { get { return _alias + "[CongressionalDistric]"; } }
			public string DPV { get { return _alias + "[DPV]"; } }
			public string DPVResponse { get { return _alias + "[DPVResponse]"; } }
			public string DPVFootNote { get { return _alias + "[DPVFootNote]"; } }
			public string CarrierRoute { get { return _alias + "[CarrierRoute]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class MS_AccountSalesInformationTable : Table<MS_AccountSalesInformation, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountSalesInformationTable(CrmDb db) : base(db, "MsASI", "[dbo].[MS_AccountSalesInformations]", "AccountID", "bigint") { }
			public string AccountID { get { return _alias + "[AccountID]"; } }
			public string PaymentTypeId { get { return _alias + "[PaymentTypeId]"; } }
			public string FriendsAndFamilyTypeId { get { return _alias + "[FriendsAndFamilyTypeId]"; } }
			public string AccountSubmitId { get { return _alias + "[AccountSubmitId]"; } }
			public string AccountCancelReasonId { get { return _alias + "[AccountCancelReasonId]"; } }
			public string TechId { get { return _alias + "[TechId]"; } }
			public string SalesRepId { get { return _alias + "[SalesRepId]"; } }
			public string BillingDay { get { return _alias + "[BillingDay]"; } }
			public string Email { get { return _alias + "[Email]"; } }
			public string IsMoni { get { return _alias + "[IsMoni]"; } }
			public string IsTakeOver { get { return _alias + "[IsTakeOver]"; } }
			public string IsOwner { get { return _alias + "[IsOwner]"; } }
			public string InstallDate { get { return _alias + "[InstallDate]"; } }
			public string SubmittedToCSDate { get { return _alias + "[SubmittedToCSDate]"; } }
			public string CsConfirmationNumber { get { return _alias + "[CsConfirmationNumber]"; } }
			public string CsTwoWayConfNumber { get { return _alias + "[CsTwoWayConfNumber]"; } }
			public string SubmittedToGPDate { get { return _alias + "[SubmittedToGPDate]"; } }
			public string ContractSignedDate { get { return _alias + "[ContractSignedDate]"; } }
			public string CancelDate { get { return _alias + "[CancelDate]"; } }
			public string AMA { get { return _alias + "[AMA]"; } }
			public string NOC { get { return _alias + "[NOC]"; } }
			public string SOP { get { return _alias + "[SOP]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class QL_AddressTable : Table<QL_Address, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public QL_AddressTable(CrmDb db) : base(db, "QlA", "[dbo].[QL_Address]", "AddressID", "bigint") { }
			public string AddressID { get { return _alias + "[AddressID]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string ValidationVendorId { get { return _alias + "[ValidationVendorId]"; } }
			public string AddressValidationStateId { get { return _alias + "[AddressValidationStateId]"; } }
			public string StateId { get { return _alias + "[StateId]"; } }
			public string CountryId { get { return _alias + "[CountryId]"; } }
			public string TimeZoneId { get { return _alias + "[TimeZoneId]"; } }
			public string AddressTypeId { get { return _alias + "[AddressTypeId]"; } }
			public string SeasonId { get { return _alias + "[SeasonId]"; } }
			public string TeamLocationId { get { return _alias + "[TeamLocationId]"; } }
			public string SalesRepId { get { return _alias + "[SalesRepId]"; } }
			public string StreetAddress { get { return _alias + "[StreetAddress]"; } }
			public string StreetAddress2 { get { return _alias + "[StreetAddress2]"; } }
			public string StreetNumber { get { return _alias + "[StreetNumber]"; } }
			public string StreetName { get { return _alias + "[StreetName]"; } }
			public string StreetType { get { return _alias + "[StreetType]"; } }
			public string PreDirectional { get { return _alias + "[PreDirectional]"; } }
			public string PostDirectional { get { return _alias + "[PostDirectional]"; } }
			public string Extension { get { return _alias + "[Extension]"; } }
			public string ExtensionNumber { get { return _alias + "[ExtensionNumber]"; } }
			public string County { get { return _alias + "[County]"; } }
			public string CountyCode { get { return _alias + "[CountyCode]"; } }
			public string Urbanization { get { return _alias + "[Urbanization]"; } }
			public string UrbanizationCode { get { return _alias + "[UrbanizationCode]"; } }
			public string City { get { return _alias + "[City]"; } }
			public string PostalCode { get { return _alias + "[PostalCode]"; } }
			public string PlusFour { get { return _alias + "[PlusFour]"; } }
			public string PostalCodeFull { get { return _alias + "[PostalCodeFull]"; } }
			public string Phone { get { return _alias + "[Phone]"; } }
			public string DeliveryPoint { get { return _alias + "[DeliveryPoint]"; } }
			public string CrossStreet { get { return _alias + "[CrossStreet]"; } }
			public string Latitude { get { return _alias + "[Latitude]"; } }
			public string Longitude { get { return _alias + "[Longitude]"; } }
			public string CongressionalDistric { get { return _alias + "[CongressionalDistric]"; } }
			public string DPV { get { return _alias + "[DPV]"; } }
			public string DPVResponse { get { return _alias + "[DPVResponse]"; } }
			public string DPVFootnote { get { return _alias + "[DPVFootnote]"; } }
			public string CarrierRoute { get { return _alias + "[CarrierRoute]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}
		public partial class QL_CustomerMasterLeadTable : Table<QL_CustomerMasterLead, Guid>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public QL_CustomerMasterLeadTable(CrmDb db) : base(db, "QlCML", "[dbo].[QL_CustomerMasterLeads]", "CustomerMasterLeadID", "uniqueidentifier") { }
			public string CustomerMasterLeadID { get { return _alias + "[CustomerMasterLeadID]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
		}
		public partial class QL_LeadAddressTable : Table<QL_LeadAddress, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public QL_LeadAddressTable(CrmDb db) : base(db, "QlLA", "[dbo].[QL_LeadAddress]", "LeadAddressID", "bigint") { }
			public string LeadAddressID { get { return _alias + "[LeadAddressID]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class QL_LeadTable : Table<QL_Lead, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public QL_LeadTable(CrmDb db) : base(db, "QlL", "[dbo].[QL_Leads]", "LeadID", "bigint") { }
			public string LeadID { get { return _alias + "[LeadID]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string LocalizationId { get { return _alias + "[LocalizationId]"; } }
			public string TeamLocationId { get { return _alias + "[TeamLocationId]"; } }
			public string SeasonId { get { return _alias + "[SeasonId]"; } }
			public string SalesRepId { get { return _alias + "[SalesRepId]"; } }
			public string LeadSourceId { get { return _alias + "[LeadSourceId]"; } }
			public string LeadDispositionId { get { return _alias + "[LeadDispositionId]"; } }
			public string LeadDispositionDateChange { get { return _alias + "[LeadDispositionDateChange]"; } }
			public string Salutation { get { return _alias + "[Salutation]"; } }
			public string FirstName { get { return _alias + "[FirstName]"; } }
			public string MiddleName { get { return _alias + "[MiddleName]"; } }
			public string LastName { get { return _alias + "[LastName]"; } }
			public string Suffix { get { return _alias + "[Suffix]"; } }
			public string Gender { get { return _alias + "[Gender]"; } }
			public string SSN { get { return _alias + "[SSN]"; } }
			public string DOB { get { return _alias + "[DOB]"; } }
			public string DL { get { return _alias + "[DL]"; } }
			public string DLStateID { get { return _alias + "[DLStateID]"; } }
			public string Email { get { return _alias + "[Email]"; } }
			public string PhoneHome { get { return _alias + "[PhoneHome]"; } }
			public string PhoneWork { get { return _alias + "[PhoneWork]"; } }
			public string PhoneMobile { get { return _alias + "[PhoneMobile]"; } }
			public string InsideSalesId { get { return _alias + "[InsideSalesId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class TS_TeamTable : Table<TS_Team, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public TS_TeamTable(CrmDb db) : base(db, "TsT", "[dbo].[TS_Teams]", "TeamId", "int") { }
			public string TeamId { get { return _alias + "[TeamId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string Version { get { return _alias + "[Version]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
		}

	}
}
