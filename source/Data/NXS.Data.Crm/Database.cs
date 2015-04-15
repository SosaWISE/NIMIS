using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class CrmDb : Database<CrmDb>
	{
		public AE_ContractTable AE_Contracts { get; set; }
		public AE_ContractTemplateTable AE_ContractTemplates { get; set; }
		public AE_CustomerAccountTable AE_CustomerAccounts { get; set; }
		public AE_CustomerTable AE_Customers { get; set; }
		public AE_InvoiceItemTable AE_InvoiceItems { get; set; }
		public AE_InvoiceTable AE_Invoices { get; set; }
		public AE_InvoiceTypeTable AE_InvoiceTypes { get; set; }
		public AE_ItemTable AE_Items { get; set; }
		public AE_PaymentMethodTable AE_PaymentMethods { get; set; }
		public MC_AccountTable MC_Accounts { get; set; }
		public MC_AddressTable MC_Addresses { get; set; }
		public MC_FriendsAndFamilyTypeTable MC_FriendsAndFamilyTypes { get; set; }
		public MS_AccountHoldCatg1Table MS_AccountHoldCatg1s { get; set; }
		public MS_AccountHoldCatg2Table MS_AccountHoldCatg2s { get; set; }
		public MS_AccountHoldTable MS_AccountHolds { get; set; }
		public MS_AccountPackageItemTable MS_AccountPackageItems { get; set; }
		public MS_AccountPackageItemTypeTable MS_AccountPackageItemTypes { get; set; }
		public MS_AccountPackageTable MS_AccountPackages { get; set; }
		public MS_AccountTable MS_Accounts { get; set; }
		public MS_AccountSalesInformationTable MS_AccountSalesInformations { get; set; }
		public QL_AddressTable QL_Addresses { get; set; }
		public QL_CustomerMasterLeadTable QL_CustomerMasterLeads { get; set; }
		public QL_LeadAddressTable QL_LeadAddresses { get; set; }
		public QL_LeadTable QL_Leads { get; set; }
		public TS_TeamTable TS_Teams { get; set; }

		public partial class AE_ContractTable : Table<AE_Contract, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_ContractTable(CrmDb db) : base(db, "AeC", "[dbo].[AE_Contracts]", "ContractID", "int", true) { }
			public string ContractID { get { return _alias + "[ContractID]"; } }
			public string ContractTemplateId { get { return _alias + "[ContractTemplateId]"; } }
			public string AccountId { get { return _alias + "[AccountId]"; } }
			public string ContractName { get { return _alias + "[ContractName]"; } }
			public string ContractLength { get { return _alias + "[ContractLength]"; } }
			public string EffectiveDate { get { return _alias + "[EffectiveDate]"; } }
			public string MonthlyFee { get { return _alias + "[MonthlyFee]"; } }
			public string ShortDesc { get { return _alias + "[ShortDesc]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedDate { get { return _alias + "[ModifiedDate]"; } }
			public string ModifiedById { get { return _alias + "[ModifiedById]"; } }
			public string CreatedDate { get { return _alias + "[CreatedDate]"; } }
			public string CreatedById { get { return _alias + "[CreatedById]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class AE_ContractTemplateTable : Table<AE_ContractTemplate, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_ContractTemplateTable(CrmDb db) : base(db, "AeCT", "[dbo].[AE_ContractTemplates]", "ContractTemplateID", "int", true) { }
			public string ContractTemplateID { get { return _alias + "[ContractTemplateID]"; } }
			public string ContractName { get { return _alias + "[ContractName]"; } }
			public string ContractLength { get { return _alias + "[ContractLength]"; } }
			public string MonthlyFee { get { return _alias + "[MonthlyFee]"; } }
			public string ShortDesc { get { return _alias + "[ShortDesc]"; } }
			public string Readable { get { return _alias + "[Readable]"; } }
			public string OrderNumber { get { return _alias + "[OrderNumber]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class AE_CustomerAccountTable : Table<AE_CustomerAccount, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_CustomerAccountTable(CrmDb db) : base(db, "AeCA", "[dbo].[AE_CustomerAccounts]", "CustomerAccountID", "bigint", true) { }
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
			public AE_CustomerTable(CrmDb db) : base(db, "AeCu", "[dbo].[AE_Customers]", "CustomerID", "bigint", true) { }
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
		public partial class AE_InvoiceItemTable : Table<AE_InvoiceItem, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_InvoiceItemTable(CrmDb db) : base(db, "AeII", "[dbo].[AE_InvoiceItems]", "InvoiceItemID", "bigint", true) { }
			public string InvoiceItemID { get { return _alias + "[InvoiceItemID]"; } }
			public string InvoiceId { get { return _alias + "[InvoiceId]"; } }
			public string ItemId { get { return _alias + "[ItemId]"; } }
			public string ProductBarcodeId { get { return _alias + "[ProductBarcodeId]"; } }
			public string AccountEquipmentId { get { return _alias + "[AccountEquipmentId]"; } }
			public string TaxOptionId { get { return _alias + "[TaxOptionId]"; } }
			public string Qty { get { return _alias + "[Qty]"; } }
			public string Cost { get { return _alias + "[Cost]"; } }
			public string RetailPrice { get { return _alias + "[RetailPrice]"; } }
			public string PriceWithTax { get { return _alias + "[PriceWithTax]"; } }
			public string SystemPoints { get { return _alias + "[SystemPoints]"; } }
			public string SalesmanId { get { return _alias + "[SalesmanId]"; } }
			public string TechnicianId { get { return _alias + "[TechnicianId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class AE_InvoiceTable : Table<AE_Invoice, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_InvoiceTable(CrmDb db) : base(db, "AeI", "[dbo].[AE_Invoices]", "InvoiceID", "bigint", true) { }
			public string InvoiceID { get { return _alias + "[InvoiceID]"; } }
			public string AccountId { get { return _alias + "[AccountId]"; } }
			public string InvoiceTypeId { get { return _alias + "[InvoiceTypeId]"; } }
			public string ContractId { get { return _alias + "[ContractId]"; } }
			public string TaxScheduleId { get { return _alias + "[TaxScheduleId]"; } }
			public string PaymentTermId { get { return _alias + "[PaymentTermId]"; } }
			public string DocDate { get { return _alias + "[DocDate]"; } }
			public string PostedDate { get { return _alias + "[PostedDate]"; } }
			public string DueDate { get { return _alias + "[DueDate]"; } }
			public string GLPostDate { get { return _alias + "[GLPostDate]"; } }
			public string CurrentTransactionAmount { get { return _alias + "[CurrentTransactionAmount]"; } }
			public string SalesAmount { get { return _alias + "[SalesAmount]"; } }
			public string OriginalTransactionAmount { get { return _alias + "[OriginalTransactionAmount]"; } }
			public string CostAmount { get { return _alias + "[CostAmount]"; } }
			public string TaxAmount { get { return _alias + "[TaxAmount]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedDate { get { return _alias + "[ModifiedDate]"; } }
			public string ModifiedById { get { return _alias + "[ModifiedById]"; } }
			public string CreatedDate { get { return _alias + "[CreatedDate]"; } }
			public string CreatedById { get { return _alias + "[CreatedById]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class AE_InvoiceTypeTable : Table<AE_InvoiceType, string>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_InvoiceTypeTable(CrmDb db) : base(db, "AeIT", "[dbo].[AE_InvoiceTypes]", "InvoiceTypeID", "varchar", false) { }
			public string InvoiceTypeID { get { return _alias + "[InvoiceTypeID]"; } }
			public string InvoiceType { get { return _alias + "[InvoiceType]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedDate { get { return _alias + "[ModifiedDate]"; } }
			public string ModifiedById { get { return _alias + "[ModifiedById]"; } }
			public string CreatedDate { get { return _alias + "[CreatedDate]"; } }
			public string CreatedById { get { return _alias + "[CreatedById]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class AE_ItemTable : Table<AE_Item, string>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_ItemTable(CrmDb db) : base(db, "AeIt", "[dbo].[AE_Items]", "ItemID", "varchar", false) { }
			public string ItemID { get { return _alias + "[ItemID]"; } }
			public string ItemTypeId { get { return _alias + "[ItemTypeId]"; } }
			public string TaxOptionId { get { return _alias + "[TaxOptionId]"; } }
			public string AccountZoneTypeId { get { return _alias + "[AccountZoneTypeId]"; } }
			public string VerticalId { get { return _alias + "[VerticalId]"; } }
			public string ModelNumber { get { return _alias + "[ModelNumber]"; } }
			public string ItemSKU { get { return _alias + "[ItemSKU]"; } }
			public string ItemDesc { get { return _alias + "[ItemDesc]"; } }
			public string Price { get { return _alias + "[Price]"; } }
			public string Cost { get { return _alias + "[Cost]"; } }
			public string SystemPoints { get { return _alias + "[SystemPoints]"; } }
			public string IsCatalogItem { get { return _alias + "[IsCatalogItem]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class AE_PaymentMethodTable : Table<AE_PaymentMethod, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public AE_PaymentMethodTable(CrmDb db) : base(db, "AePM", "[dbo].[AE_PaymentMethods]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string PaymentTypeId { get { return _alias + "[PaymentTypeId]"; } }
			public string CardTypeId { get { return _alias + "[CardTypeId]"; } }
			public string CardNumber { get { return _alias + "[CardNumber]"; } }
			public string VerificationValue { get { return _alias + "[VerificationValue]"; } }
			public string ExpirationMonth { get { return _alias + "[ExpirationMonth]"; } }
			public string ExpirationYear { get { return _alias + "[ExpirationYear]"; } }
			public string NameOnCard { get { return _alias + "[NameOnCard]"; } }
			public string AccountTypeId { get { return _alias + "[AccountTypeId]"; } }
			public string AccountNumber { get { return _alias + "[AccountNumber]"; } }
			public string RoutingNumber { get { return _alias + "[RoutingNumber]"; } }
			public string NameOnAccount { get { return _alias + "[NameOnAccount]"; } }
			public string CheckNumber { get { return _alias + "[CheckNumber]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class MC_AccountTable : Table<MC_Account, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MC_AccountTable(CrmDb db) : base(db, "McA", "[dbo].[MC_Accounts]", "AccountID", "bigint", true) { }
			public string AccountID { get { return _alias + "[AccountID]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string AccountTypeId { get { return _alias + "[AccountTypeId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string ShipContactId { get { return _alias + "[ShipContactId]"; } }
			public string ShipAddressId { get { return _alias + "[ShipAddressId]"; } }
			public string DealerAccountId { get { return _alias + "[DealerAccountId]"; } }
			public string ShipContactSameAsCustomer { get { return _alias + "[ShipContactSameAsCustomer]"; } }
			public string ShipAddressSameAsCustomer { get { return _alias + "[ShipAddressSameAsCustomer]"; } }
			public string AccountName { get { return _alias + "[AccountName]"; } }
			public string AccountDesc { get { return _alias + "[AccountDesc]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class MC_AddressTable : Table<MC_Address, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MC_AddressTable(CrmDb db) : base(db, "McAd", "[dbo].[MC_Addresses]", "AddressID", "bigint", true) { }
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
		public partial class MC_FriendsAndFamilyTypeTable : Table<MC_FriendsAndFamilyType, string>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MC_FriendsAndFamilyTypeTable(CrmDb db) : base(db, "McFAFT", "[dbo].[MC_FriendsAndFamilyTypes]", "FriendsAndFamilyTypeID", "varchar", false) { }
			public string FriendsAndFamilyTypeID { get { return _alias + "[FriendsAndFamilyTypeID]"; } }
			public string FriendsAndFamilyType { get { return _alias + "[FriendsAndFamilyType]"; } }
		}
		public partial class MS_AccountHoldCatg1Table : Table<MS_AccountHoldCatg1, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountHoldCatg1Table(CrmDb db) : base(db, "MsAHC", "[dbo].[MS_AccountHoldCatg1]", "Catg1ID", "int", true) { }
			public string Catg1ID { get { return _alias + "[Catg1ID]"; } }
			public string CatgName { get { return _alias + "[CatgName]"; } }
			public string CatgDescription { get { return _alias + "[CatgDescription]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class MS_AccountHoldCatg2Table : Table<MS_AccountHoldCatg2, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountHoldCatg2Table(CrmDb db) : base(db, "MsAcHoCa", "[dbo].[MS_AccountHoldCatg2]", "Catg2ID", "int", true) { }
			public string Catg2ID { get { return _alias + "[Catg2ID]"; } }
			public string CatgName { get { return _alias + "[CatgName]"; } }
			public string Catg1Id { get { return _alias + "[Catg1Id]"; } }
			public string RecruitFriendlyName { get { return _alias + "[RecruitFriendlyName]"; } }
			public string CatgDescription { get { return _alias + "[CatgDescription]"; } }
			public string IsRepFrontEndHold { get { return _alias + "[IsRepFrontEndHold]"; } }
			public string IsRepBackEndHold { get { return _alias + "[IsRepBackEndHold]"; } }
			public string IsTechFrontEndHold { get { return _alias + "[IsTechFrontEndHold]"; } }
			public string IsTechBackEndHold { get { return _alias + "[IsTechBackEndHold]"; } }
			public string PreventsContractSale { get { return _alias + "[PreventsContractSale]"; } }
			public string IsAccountFlag { get { return _alias + "[IsAccountFlag]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class MS_AccountHoldTable : Table<MS_AccountHold, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountHoldTable(CrmDb db) : base(db, "MsAH", "[dbo].[MS_AccountHolds]", "AccountHoldID", "bigint", true) { }
			public string AccountHoldID { get { return _alias + "[AccountHoldID]"; } }
			public string AccountId { get { return _alias + "[AccountId]"; } }
			public string Catg2Id { get { return _alias + "[Catg2Id]"; } }
			public string HoldDescription { get { return _alias + "[HoldDescription]"; } }
			public string FixedNote { get { return _alias + "[FixedNote]"; } }
			public string FixedBy { get { return _alias + "[FixedBy]"; } }
			public string FixedOn { get { return _alias + "[FixedOn]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class MS_AccountPackageItemTable : Table<MS_AccountPackageItem, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountPackageItemTable(CrmDb db) : base(db, "MsAPI", "[dbo].[MS_AccountPackageItems]", "AccountPackageItemID", "int", true) { }
			public string AccountPackageItemID { get { return _alias + "[AccountPackageItemID]"; } }
			public string AccountPackageId { get { return _alias + "[AccountPackageId]"; } }
			public string AccountPackageItemTypeId { get { return _alias + "[AccountPackageItemTypeId]"; } }
			public string PackegeItemName { get { return _alias + "[PackegeItemName]"; } }
			public string ItemId { get { return _alias + "[ItemId]"; } }
			public string ModelNumber { get { return _alias + "[ModelNumber]"; } }
			public string IsUpgrade { get { return _alias + "[IsUpgrade]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}
		public partial class MS_AccountPackageItemTypeTable : Table<MS_AccountPackageItemType, string>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountPackageItemTypeTable(CrmDb db) : base(db, "MsAPIT", "[dbo].[MS_AccountPackageItemTypes]", "AccountPackageItemTypeID", "varchar", false) { }
			public string AccountPackageItemTypeID { get { return _alias + "[AccountPackageItemTypeID]"; } }
			public string PackageItemTypeName { get { return _alias + "[PackageItemTypeName]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class MS_AccountPackageTable : Table<MS_AccountPackage, int>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountPackageTable(CrmDb db) : base(db, "MsAP", "[dbo].[MS_AccountPackages]", "AccountPackageID", "int", true) { }
			public string AccountPackageID { get { return _alias + "[AccountPackageID]"; } }
			public string AccountPackageName { get { return _alias + "[AccountPackageName]"; } }
			public string ShortName { get { return _alias + "[ShortName]"; } }
			public string Description { get { return _alias + "[Description]"; } }
			public string BasePoints { get { return _alias + "[BasePoints]"; } }
			public string BaseRMR { get { return _alias + "[BaseRMR]"; } }
			public string MinRMR { get { return _alias + "[MinRMR]"; } }
			public string MaxRMR { get { return _alias + "[MaxRMR]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}
		public partial class MS_AccountTable : Table<MS_Account, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountTable(CrmDb db) : base(db, "MsA", "[dbo].[MS_Accounts]", "AccountID", "bigint", false) { }
			public string AccountID { get { return _alias + "[AccountID]"; } }
			public string PremiseAddressId { get { return _alias + "[PremiseAddressId]"; } }
			public string IndustryAccountId { get { return _alias + "[IndustryAccountId]"; } }
			public string IndustryAccount2Id { get { return _alias + "[IndustryAccount2Id]"; } }
			public string SiteTypeId { get { return _alias + "[SiteTypeId]"; } }
			public string SystemTypeId { get { return _alias + "[SystemTypeId]"; } }
			public string CellularTypeId { get { return _alias + "[CellularTypeId]"; } }
			public string PanelTypeId { get { return _alias + "[PanelTypeId]"; } }
			public string DslSeizureId { get { return _alias + "[DslSeizureId]"; } }
			public string PanelItemId { get { return _alias + "[PanelItemId]"; } }
			public string CellPackageItemId { get { return _alias + "[CellPackageItemId]"; } }
			public string ContractId { get { return _alias + "[ContractId]"; } }
			public string SignalFormatTypeId { get { return _alias + "[SignalFormatTypeId]"; } }
			public string PanelCode { get { return _alias + "[PanelCode]"; } }
			public string PanelPhone { get { return _alias + "[PanelPhone]"; } }
			public string PanelLocation { get { return _alias + "[PanelLocation]"; } }
			public string TransformerLocation { get { return _alias + "[TransformerLocation]"; } }
			public string Privacy { get { return _alias + "[Privacy]"; } }
			public string AccountPassword { get { return _alias + "[AccountPassword]"; } }
			public string SimProductBarcodeId { get { return _alias + "[SimProductBarcodeId]"; } }
			public string DispatchMessage { get { return _alias + "[DispatchMessage]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class MS_AccountSalesInformationTable : Table<MS_AccountSalesInformation, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public MS_AccountSalesInformationTable(CrmDb db) : base(db, "MsASI", "[dbo].[MS_AccountSalesInformations]", "AccountID", "bigint", false) { }
			public string AccountID { get { return _alias + "[AccountID]"; } }
			public string PaymentTypeId { get { return _alias + "[PaymentTypeId]"; } }
			public string FriendsAndFamilyTypeId { get { return _alias + "[FriendsAndFamilyTypeId]"; } }
			public string AccountSubmitId { get { return _alias + "[AccountSubmitId]"; } }
			public string AccountCancelReasonId { get { return _alias + "[AccountCancelReasonId]"; } }
			public string AccountPackageId { get { return _alias + "[AccountPackageId]"; } }
			public string PaymentMethodId { get { return _alias + "[PaymentMethodId]"; } }
			public string InitialPaymentMethodId { get { return _alias + "[InitialPaymentMethodId]"; } }
			public string TechId { get { return _alias + "[TechId]"; } }
			public string SalesRepId { get { return _alias + "[SalesRepId]"; } }
			public string AccountFundingStatusId { get { return _alias + "[AccountFundingStatusId]"; } }
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
			public string ApprovedDate { get { return _alias + "[ApprovedDate]"; } }
			public string ApproverID { get { return _alias + "[ApproverID]"; } }
			public string NOCDate { get { return _alias + "[NOCDate]"; } }
			public string OptOutCorporate { get { return _alias + "[OptOutCorporate]"; } }
			public string OptOutAffiliate { get { return _alias + "[OptOutAffiliate]"; } }
			public string Waived1stmonth { get { return _alias + "[Waived1stmonth]"; } }
			public string RMRIncreasePoints { get { return _alias + "[RMRIncreasePoints]"; } }
			public string AccountCreationTypeId { get { return _alias + "[AccountCreationTypeId]"; } }
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
			public QL_AddressTable(CrmDb db) : base(db, "QlA", "[dbo].[QL_Address]", "AddressID", "bigint", true) { }
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
			public QL_CustomerMasterLeadTable(CrmDb db) : base(db, "QlCML", "[dbo].[QL_CustomerMasterLeads]", "CustomerMasterLeadID", "uniqueidentifier", false) { }
			public string CustomerMasterLeadID { get { return _alias + "[CustomerMasterLeadID]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
		}
		public partial class QL_LeadAddressTable : Table<QL_LeadAddress, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public QL_LeadAddressTable(CrmDb db) : base(db, "QlLA", "[dbo].[QL_LeadAddress]", "LeadAddressID", "bigint", true) { }
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
			public QL_LeadTable(CrmDb db) : base(db, "QlL", "[dbo].[QL_Leads]", "LeadID", "bigint", true) { }
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
			public TS_TeamTable(CrmDb db) : base(db, "TsT", "[dbo].[TS_Teams]", "TeamId", "int", false) { }
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
