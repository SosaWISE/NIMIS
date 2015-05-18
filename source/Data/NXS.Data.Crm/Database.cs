using NXS.Data;
using System;

namespace NXS.Data.Crm
{
	public partial class DBase : Database<DBase>
	{
		public readonly Sprocs Sprocs;
		public DBase()
		{
			Sprocs = new Sprocs(this);
		}

		public AE_ContractTable AE_Contracts { get; set; }
		public AE_ContractTemplateTable AE_ContractTemplates { get; set; }
		public AE_CustomerAccountTable AE_CustomerAccounts { get; set; }
		public AE_CustomerTable AE_Customers { get; set; }
		public AE_InvoiceItemTable AE_InvoiceItems { get; set; }
		public AE_InvoiceTable AE_Invoices { get; set; }
		public AE_InvoiceTypeTable AE_InvoiceTypes { get; set; }
		public AE_ItemTable AE_Items { get; set; }
		public AE_PaymentMethodTable AE_PaymentMethods { get; set; }
		public IE_AuditTable IE_Audits { get; set; }
		public IE_LocationTypeTable IE_LocationTypes { get; set; }
		public IE_PackingSlipItemTable IE_PackingSlipItems { get; set; }
		public IE_PackingSlipTable IE_PackingSlips { get; set; }
		public IE_ProductBarcodeBundleTable IE_ProductBarcodeBundles { get; set; }
		public IE_ProductBarcodeTable IE_ProductBarcodes { get; set; }
		public IE_ProductBarcodeTrackingTable IE_ProductBarcodeTrackings { get; set; }
		public IE_ProductBarcodeTrackingTypeTable IE_ProductBarcodeTrackingTypes { get; set; }
		public IE_PurchaseOrderItemTable IE_PurchaseOrderItems { get; set; }
		public IE_PurchaseOrderTable IE_PurchaseOrders { get; set; }
		public IE_ReceivedTable IE_Receiveds { get; set; }
		public IE_ReturnToManufacturerItemTable IE_ReturnToManufacturerItems { get; set; }
		public IE_ReturnToManufacturerTable IE_ReturnToManufacturers { get; set; }
		public IE_StockingLevelTable IE_StockingLevels { get; set; }
		public IE_VendorTable IE_Vendors { get; set; }
		public IE_WarehouseSiteTable IE_WarehouseSites { get; set; }
		public MC_AccountCancelReasonTable MC_AccountCancelReasons { get; set; }
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
		public MS_EquipmentTable MS_Equipments { get; set; }
		public QL_AddressTable QL_Addresses { get; set; }
		public QL_CustomerMasterLeadTable QL_CustomerMasterLeads { get; set; }
		public QL_LeadAddressTable QL_LeadAddresses { get; set; }
		public QL_LeadTable QL_Leads { get; set; }
		public TS_TeamTable TS_Teams { get; set; }
		public IE_ProductBarcodeLocationViewTable IE_ProductBarcodeLocationViews { get; set; }

		public partial class AE_ContractTable : Table<AE_Contract, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public AE_ContractTable(DBase db) : base(db, "AeC", "[WISE_CRM].[dbo].[AE_Contracts]", "ContractID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_ContractTemplateTable(DBase db) : base(db, "AeCT", "[WISE_CRM].[dbo].[AE_ContractTemplates]", "ContractTemplateID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_CustomerAccountTable(DBase db) : base(db, "AeCA", "[WISE_CRM].[dbo].[AE_CustomerAccounts]", "CustomerAccountID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_CustomerTable(DBase db) : base(db, "AeCu", "[WISE_CRM].[dbo].[AE_Customers]", "CustomerID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_InvoiceItemTable(DBase db) : base(db, "AeII", "[WISE_CRM].[dbo].[AE_InvoiceItems]", "InvoiceItemID", "bigint", true) { }
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
			public string IsCustomerPaying { get { return _alias + "[IsCustomerPaying]"; } }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_InvoiceTable(DBase db) : base(db, "AeI", "[WISE_CRM].[dbo].[AE_Invoices]", "InvoiceID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_InvoiceTypeTable(DBase db) : base(db, "AeIT", "[WISE_CRM].[dbo].[AE_InvoiceTypes]", "InvoiceTypeID", "varchar", false) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_ItemTable(DBase db) : base(db, "AeIt", "[WISE_CRM].[dbo].[AE_Items]", "ItemID", "varchar", false) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public AE_PaymentMethodTable(DBase db) : base(db, "AePM", "[WISE_CRM].[dbo].[AE_PaymentMethods]", "ID", "int", true) { }
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
		public partial class IE_AuditTable : Table<IE_Audit, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_AuditTable(DBase db) : base(db, "IeA", "[WISE_CRM].[dbo].[IE_Audits]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string LocationId { get { return _alias + "[LocationId]"; } }
			public string LocationTypeId { get { return _alias + "[LocationTypeId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class IE_LocationTypeTable : Table<IE_LocationType, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_LocationTypeTable(DBase db) : base(db, "IeLT", "[WISE_CRM].[dbo].[IE_LocationTypes]", "LocationTypeID", "varchar", false) { }
			public string LocationTypeID { get { return _alias + "[LocationTypeID]"; } }
			public string LocationTypeName { get { return _alias + "[LocationTypeName]"; } }
			public string TableName { get { return _alias + "[TableName]"; } }
			public string FieldID { get { return _alias + "[FieldID]"; } }
			public string FieldName { get { return _alias + "[FieldName]"; } }
			public string Comment { get { return _alias + "[Comment]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_PackingSlipItemTable : Table<IE_PackingSlipItem, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_PackingSlipItemTable(DBase db) : base(db, "IePSI", "[WISE_CRM].[dbo].[IE_PackingSlipItems]", "PackingSlipItemID", "bigint", true) { }
			public string PackingSlipItemID { get { return _alias + "[PackingSlipItemID]"; } }
			public string PackingSlipId { get { return _alias + "[PackingSlipId]"; } }
			public string ProductSkwId { get { return _alias + "[ProductSkwId]"; } }
			public string ItemId { get { return _alias + "[ItemId]"; } }
			public string Quantity { get { return _alias + "[Quantity]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_PackingSlipTable : Table<IE_PackingSlip, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_PackingSlipTable(DBase db) : base(db, "IePS", "[WISE_CRM].[dbo].[IE_PackingSlips]", "PackingSlipID", "int", true) { }
			public string PackingSlipID { get { return _alias + "[PackingSlipID]"; } }
			public string PurchaseOrderId { get { return _alias + "[PurchaseOrderId]"; } }
			public string ArrivalDate { get { return _alias + "[ArrivalDate]"; } }
			public string CloseDate { get { return _alias + "[CloseDate]"; } }
			public string PackingSlipNumber { get { return _alias + "[PackingSlipNumber]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_ProductBarcodeBundleTable : Table<IE_ProductBarcodeBundle, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ProductBarcodeBundleTable(DBase db) : base(db, "IePBB", "[WISE_CRM].[dbo].[IE_ProductBarcodeBundles]", "ProductBarcodeBundleID", "nvarchar", false) { }
			public string ProductBarcodeBundleID { get { return _alias + "[ProductBarcodeBundleID]"; } }
			public string ProductBarcodeId { get { return _alias + "[ProductBarcodeId]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_ProductBarcodeTable : Table<IE_ProductBarcode, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ProductBarcodeTable(DBase db) : base(db, "IePB", "[WISE_CRM].[dbo].[IE_ProductBarcodes]", "ProductBarcodeID", "nvarchar", false) { }
			public string ProductBarcodeID { get { return _alias + "[ProductBarcodeID]"; } }
			public string PurchaseOrderItemId { get { return _alias + "[PurchaseOrderItemId]"; } }
			public string PackingSlipItemId { get { return _alias + "[PackingSlipItemId]"; } }
			public string LastProductBarcodeTrackingId { get { return _alias + "[LastProductBarcodeTrackingId]"; } }
			public string ProductBarcodeBundleId { get { return _alias + "[ProductBarcodeBundleId]"; } }
			public string SimGUID { get { return _alias + "[SimGUID]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_ProductBarcodeTrackingTable : Table<IE_ProductBarcodeTracking, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ProductBarcodeTrackingTable(DBase db) : base(db, "IePBT", "[WISE_CRM].[dbo].[IE_ProductBarcodeTracking]", "ProductBarcodeTrackingID", "bigint", true) { }
			public string ProductBarcodeTrackingID { get { return _alias + "[ProductBarcodeTrackingID]"; } }
			public string ProductBarcodeTrackingTypeId { get { return _alias + "[ProductBarcodeTrackingTypeId]"; } }
			public string ProductBarcodeId { get { return _alias + "[ProductBarcodeId]"; } }
			public string LocationTypeId { get { return _alias + "[LocationTypeId]"; } }
			public string LocationId { get { return _alias + "[LocationId]"; } }
			public string AuditId { get { return _alias + "[AuditId]"; } }
			public string Comment { get { return _alias + "[Comment]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_ProductBarcodeTrackingTypeTable : Table<IE_ProductBarcodeTrackingType, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ProductBarcodeTrackingTypeTable(DBase db) : base(db, "IePBTT", "[WISE_CRM].[dbo].[IE_ProductBarcodeTrackingTypes]", "ProductBarcodeTrackingTypeID", "varchar", false) { }
			public string ProductBarcodeTrackingTypeID { get { return _alias + "[ProductBarcodeTrackingTypeID]"; } }
			public string ProductBarcodeTrackingTypeName { get { return _alias + "[ProductBarcodeTrackingTypeName]"; } }
			public string Comment { get { return _alias + "[Comment]"; } }
			public string IsInventoryMove { get { return _alias + "[IsInventoryMove]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_PurchaseOrderItemTable : Table<IE_PurchaseOrderItem, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_PurchaseOrderItemTable(DBase db) : base(db, "IePOI", "[WISE_CRM].[dbo].[IE_PurchaseOrderItems]", "PurchaseOrderItemID", "bigint", true) { }
			public string PurchaseOrderItemID { get { return _alias + "[PurchaseOrderItemID]"; } }
			public string PurchaseOrderId { get { return _alias + "[PurchaseOrderId]"; } }
			public string ItemId { get { return _alias + "[ItemId]"; } }
			public string WarehouseSiteId { get { return _alias + "[WarehouseSiteId]"; } }
			public string Quantity { get { return _alias + "[Quantity]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_PurchaseOrderTable : Table<IE_PurchaseOrder, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_PurchaseOrderTable(DBase db) : base(db, "IePO", "[WISE_CRM].[dbo].[IE_PurchaseOrders]", "PurchaseOrderID", "int", true) { }
			public string PurchaseOrderID { get { return _alias + "[PurchaseOrderID]"; } }
			public string VendorId { get { return _alias + "[VendorId]"; } }
			public string GPPONumber { get { return _alias + "[GPPONumber]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_ReceivedTable : Table<IE_Received, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ReceivedTable(DBase db) : base(db, "IeR", "[WISE_CRM].[dbo].[IE_Received]", "WarehouseID", "varchar", false) { }
			public string WarehouseID { get { return _alias + "[WarehouseID]"; } }
		}
		public partial class IE_ReturnToManufacturerItemTable : Table<IE_ReturnToManufacturerItem, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ReturnToManufacturerItemTable(DBase db) : base(db, "IeRTMI", "[WISE_CRM].[dbo].[IE_ReturnToManufacturerItems]", "SerialNumberID", "varchar", false) { }
			public string SerialNumberID { get { return _alias + "[SerialNumberID]"; } }
			public string RtmaNumberID { get { return _alias + "[RtmaNumberID]"; } }
			public string Description { get { return _alias + "[Description]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_ReturnToManufacturerTable : Table<IE_ReturnToManufacturer, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_ReturnToManufacturerTable(DBase db) : base(db, "IeRTM", "[WISE_CRM].[dbo].[IE_ReturnToManufacturers]", "RtmaNumberID", "nvarchar", false) { }
			public string RtmaNumberID { get { return _alias + "[RtmaNumberID]"; } }
			public string ManufacturerId { get { return _alias + "[ManufacturerId]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_StockingLevelTable : Table<IE_StockingLevel, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_StockingLevelTable(DBase db) : base(db, "IeSL", "[WISE_CRM].[dbo].[IE_StockingLevels]", "StockingLevelID", "int", true) { }
			public string StockingLevelID { get { return _alias + "[StockingLevelID]"; } }
			public string LocationTypeId { get { return _alias + "[LocationTypeId]"; } }
			public string LocationId { get { return _alias + "[LocationId]"; } }
			public string ItemId { get { return _alias + "[ItemId]"; } }
			public string ReorderLevel { get { return _alias + "[ReorderLevel]"; } }
			public string StockingLevel { get { return _alias + "[StockingLevel]"; } }
			public string OrderQuantity { get { return _alias + "[OrderQuantity]"; } }
			public string Comment { get { return _alias + "[Comment]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class IE_VendorTable : Table<IE_Vendor, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_VendorTable(DBase db) : base(db, "IeV", "[WISE_CRM].[dbo].[IE_Vendors]", "VendorID", "varchar", false) { }
			public string VendorID { get { return _alias + "[VendorID]"; } }
			public string VendorName { get { return _alias + "[VendorName]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class IE_WarehouseSiteTable : Table<IE_WarehouseSite, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public IE_WarehouseSiteTable(DBase db) : base(db, "IeWS", "[WISE_CRM].[dbo].[IE_WarehouseSites]", "WarehouseSiteID", "varchar", false) { }
			public string WarehouseSiteID { get { return _alias + "[WarehouseSiteID]"; } }
			public string WarehouseSiteName { get { return _alias + "[WarehouseSiteName]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
			public string DEX_ROW_ID { get { return _alias + "[DEX_ROW_ID]"; } }
		}
		public partial class MC_AccountCancelReasonTable : Table<MC_AccountCancelReason, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public MC_AccountCancelReasonTable(DBase db) : base(db, "McACR", "[WISE_CRM].[dbo].[MC_AccountCancelReasons]", "AccountCancelReasonID", "varchar", false) { }
			public string AccountCancelReasonID { get { return _alias + "[AccountCancelReasonID]"; } }
			public string AccountCancelReason { get { return _alias + "[AccountCancelReason]"; } }
		}
		public partial class MC_AccountTable : Table<MC_Account, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public MC_AccountTable(DBase db) : base(db, "McA", "[WISE_CRM].[dbo].[MC_Accounts]", "AccountID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MC_AddressTable(DBase db) : base(db, "McAd", "[WISE_CRM].[dbo].[MC_Addresses]", "AddressID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MC_FriendsAndFamilyTypeTable(DBase db) : base(db, "McFAFT", "[WISE_CRM].[dbo].[MC_FriendsAndFamilyTypes]", "FriendsAndFamilyTypeID", "varchar", false) { }
			public string FriendsAndFamilyTypeID { get { return _alias + "[FriendsAndFamilyTypeID]"; } }
			public string FriendsAndFamilyType { get { return _alias + "[FriendsAndFamilyType]"; } }
		}
		public partial class MS_AccountHoldCatg1Table : Table<MS_AccountHoldCatg1, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountHoldCatg1Table(DBase db) : base(db, "MsAHC", "[WISE_CRM].[dbo].[MS_AccountHoldCatg1]", "Catg1ID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountHoldCatg2Table(DBase db) : base(db, "MsAcHoCa", "[WISE_CRM].[dbo].[MS_AccountHoldCatg2]", "Catg2ID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountHoldTable(DBase db) : base(db, "MsAH", "[WISE_CRM].[dbo].[MS_AccountHolds]", "AccountHoldID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountPackageItemTable(DBase db) : base(db, "MsAPI", "[WISE_CRM].[dbo].[MS_AccountPackageItems]", "AccountPackageItemID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountPackageItemTypeTable(DBase db) : base(db, "MsAPIT", "[WISE_CRM].[dbo].[MS_AccountPackageItemTypes]", "AccountPackageItemTypeID", "varchar", false) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountPackageTable(DBase db) : base(db, "MsAP", "[WISE_CRM].[dbo].[MS_AccountPackages]", "AccountPackageID", "int", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountTable(DBase db) : base(db, "MsA", "[WISE_CRM].[dbo].[MS_Accounts]", "AccountID", "bigint", false) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public MS_AccountSalesInformationTable(DBase db) : base(db, "MsASI", "[WISE_CRM].[dbo].[MS_AccountSalesInformations]", "AccountID", "bigint", false) { }
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
			public string AccountPayoutTypeId { get { return _alias + "[AccountPayoutTypeId]"; } }
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
		public partial class MS_EquipmentTable : Table<MS_Equipment, string>
		{
			public DBase Db { get { return (DBase)_database; } }
			public MS_EquipmentTable(DBase db) : base(db, "MsE", "[WISE_CRM].[dbo].[MS_Equipments]", "EquipmentID", "varchar", false) { }
			public string EquipmentID { get { return _alias + "[EquipmentID]"; } }
			public string EquipmentMonitoredTypeId { get { return _alias + "[EquipmentMonitoredTypeId]"; } }
			public string EquipmentTypeId { get { return _alias + "[EquipmentTypeId]"; } }
			public string AccountZoneTypeId { get { return _alias + "[AccountZoneTypeId]"; } }
			public string AccountEventId { get { return _alias + "[AccountEventId]"; } }
			public string EquipmentPanelTypeId { get { return _alias + "[EquipmentPanelTypeId]"; } }
			public string GPItemNmbr { get { return _alias + "[GPItemNmbr]"; } }
			public string ItemDescription { get { return _alias + "[ItemDescription]"; } }
			public string ShortName { get { return _alias + "[ShortName]"; } }
			public string GenDescription { get { return _alias + "[GenDescription]"; } }
			public string FullName { get { return _alias + "[FullName]"; } }
			public string ShowInInventory { get { return _alias + "[ShowInInventory]"; } }
			public string Points { get { return _alias + "[Points]"; } }
			public string ActualPoints { get { return _alias + "[ActualPoints]"; } }
			public string RetailPrice { get { return _alias + "[RetailPrice]"; } }
			public string RepBonusUpgrade { get { return _alias + "[RepBonusUpgrade]"; } }
			public string TechMinutes { get { return _alias + "[TechMinutes]"; } }
			public string IsCellUnit { get { return _alias + "[IsCellUnit]"; } }
			public string AuditDay { get { return _alias + "[AuditDay]"; } }
			public string EmployeeCost { get { return _alias + "[EmployeeCost]"; } }
			public string DefaultTechStockLevel { get { return _alias + "[DefaultTechStockLevel]"; } }
			public string IsHighlighted { get { return _alias + "[IsHighlighted]"; } }
			public string IsWireless { get { return _alias + "[IsWireless]"; } }
			public string IsGeneric { get { return _alias + "[IsGeneric]"; } }
			public string IsExisting { get { return _alias + "[IsExisting]"; } }
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
			public DBase Db { get { return (DBase)_database; } }
			public QL_AddressTable(DBase db) : base(db, "QlA", "[WISE_CRM].[dbo].[QL_Address]", "AddressID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public QL_CustomerMasterLeadTable(DBase db) : base(db, "QlCML", "[WISE_CRM].[dbo].[QL_CustomerMasterLeads]", "CustomerMasterLeadID", "uniqueidentifier", false) { }
			public string CustomerMasterLeadID { get { return _alias + "[CustomerMasterLeadID]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
		}
		public partial class QL_LeadAddressTable : Table<QL_LeadAddress, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public QL_LeadAddressTable(DBase db) : base(db, "QlLA", "[WISE_CRM].[dbo].[QL_LeadAddress]", "LeadAddressID", "bigint", true) { }
			public string LeadAddressID { get { return _alias + "[LeadAddressID]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}
		public partial class QL_LeadTable : Table<QL_Lead, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public QL_LeadTable(DBase db) : base(db, "QlL", "[WISE_CRM].[dbo].[QL_Leads]", "LeadID", "bigint", true) { }
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
			public DBase Db { get { return (DBase)_database; } }
			public TS_TeamTable(DBase db) : base(db, "TsT", "[WISE_CRM].[dbo].[TS_Teams]", "TeamId", "int", false) { }
			public string TeamId { get { return _alias + "[TeamId]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string Version { get { return _alias + "[Version]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
		}
		public partial class IE_ProductBarcodeLocationViewTable : Table<IE_ProductBarcodeLocationView, string>
		{
			public IE_ProductBarcodeLocationViewTable(DBase db) : base(db, "IePBL", "[WISE_CRM].[dbo].[vwIE_ProductBarcodeLocation]", "ProductBarcodeId", "nvarchar", false) { }
			public string ProductBarcodeId { get { return _alias + "[ProductBarcodeId]"; } }
			public string ItemSKU { get { return _alias + "[ItemSKU]"; } }
			public string ItemDesc { get { return _alias + "[ItemDesc]"; } }
			public string LocationID { get { return _alias + "[LocationID]"; } }
		}

	}
}
