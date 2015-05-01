using NXS.Data;
using System;

namespace NXS.Data.Crm
{
	public partial class AE_Contract // AE_Contracts
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return ContractID; } set { ContractID = value; } }
		public int ContractID { get; set; }
		public int ContractTemplateId { get; set; }
		public long? AccountId { get; set; }
		public string ContractName { get; set; }
		public short ContractLength { get; set; }
		public DateTime EffectiveDate { get; set; }
		public decimal MonthlyFee { get; set; }
		public string ShortDesc { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string ModifiedById { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedById { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class AE_ContractTemplate // AE_ContractTemplates
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return ContractTemplateID; } set { ContractTemplateID = value; } }
		public int ContractTemplateID { get; set; }
		public string ContractName { get; set; }
		public short ContractLength { get; set; }
		public decimal MonthlyFee { get; set; }
		public string ShortDesc { get; set; }
		public string Readable { get; set; }
		public short OrderNumber { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class AE_CustomerAccount // AE_CustomerAccounts
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return CustomerAccountID; } set { CustomerAccountID = value; } }
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
		[IgnorePropertyAttribute(true)] public long ID { get { return CustomerID; } set { CustomerID = value; } }
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
	public partial class AE_InvoiceItem // AE_InvoiceItems
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return InvoiceItemID; } set { InvoiceItemID = value; } }
		public long InvoiceItemID { get; set; }
		public long InvoiceId { get; set; }
		public string ItemId { get; set; }
		public string ProductBarcodeId { get; set; }
		public long? AccountEquipmentId { get; set; }
		public string TaxOptionId { get; set; }
		public short Qty { get; set; }
		public decimal Cost { get; set; }
		public decimal RetailPrice { get; set; }
		public decimal? PriceWithTax { get; set; }
		public decimal SystemPoints { get; set; }
		public string SalesmanId { get; set; }
		public string TechnicianId { get; set; }
		public bool? IsCustomerPaying { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class AE_Invoice // AE_Invoices
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return InvoiceID; } set { InvoiceID = value; } }
		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public string InvoiceTypeId { get; set; }
		public int? ContractId { get; set; }
		public int? TaxScheduleId { get; set; }
		public int? PaymentTermId { get; set; }
		public DateTime DocDate { get; set; }
		public DateTime? PostedDate { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? GLPostDate { get; set; }
		public decimal? CurrentTransactionAmount { get; set; }
		public decimal SalesAmount { get; set; }
		public decimal OriginalTransactionAmount { get; set; }
		public decimal CostAmount { get; set; }
		public decimal TaxAmount { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string ModifiedById { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedById { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class AE_InvoiceType // AE_InvoiceTypes
	{
		public static class MetaData
		{
			public const string ChangeofServiceID = "CHANGE_SERVICE";
			public const string EquipmentPullID = "EQM_PULL";
			public const string EquipmentSalesID = "EQM_SALE";
			public const string SetupandInstallationID = "INSTALL";
			public const string RefundID = "REFUND";
			public const string SaleID = "SALE";
			public const string ServiceCallID = "SERV_CALL";
			public const string ServiceCallWarrantyID = "SERV_CALL_FREE";
			public const string SiteSurveyID = "SITE_SURVEY";
			public const string UpgradeID = "UPGRAGE";
		}
		[IgnorePropertyAttribute(true)] public string ID { get { return InvoiceTypeID; } set { InvoiceTypeID = value; } }
		public string InvoiceTypeID { get; set; }
		public string InvoiceType { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string ModifiedById { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedById { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
		public int DEX_ROW_ID { get; set; }
	}
	public partial class AE_Item // AE_Items
	{
		[IgnorePropertyAttribute(true)] public string ID { get { return ItemID; } set { ItemID = value; } }
		public string ItemID { get; set; }
		public string ItemTypeId { get; set; }
		public string TaxOptionId { get; set; }
		public string AccountZoneTypeId { get; set; }
		public string VerticalId { get; set; }
		public string ModelNumber { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public decimal SystemPoints { get; set; }
		public bool IsCatalogItem { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
		public int DEX_ROW_ID { get; set; }
	}
	public partial class AE_PaymentMethod // AE_PaymentMethods
	{
		public int ID { get; set; }
		public string PaymentTypeId { get; set; }
		public int? CardTypeId { get; set; }
		public string CardNumber { get; set; }
		public string VerificationValue { get; set; }
		public int? ExpirationMonth { get; set; }
		public int? ExpirationYear { get; set; }
		public string NameOnCard { get; set; }
		public int? AccountTypeId { get; set; }
		public string AccountNumber { get; set; }
		public string RoutingNumber { get; set; }
		public string NameOnAccount { get; set; }
		public string CheckNumber { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class MC_AccountCancelReason // MC_AccountCancelReasons
	{
		[IgnorePropertyAttribute(true)] public string ID { get { return AccountCancelReasonID; } set { AccountCancelReasonID = value; } }
		public string AccountCancelReasonID { get; set; }
		public string AccountCancelReason { get; set; }
	}
	public partial class MC_Account // MC_Accounts
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AccountID; } set { AccountID = value; } }
		public long AccountID { get; set; }
		public long? CustomerMasterFileId { get; set; }
		public string AccountTypeId { get; set; }
		public int DealerId { get; set; }
		public long? ShipContactId { get; set; }
		public long? ShipAddressId { get; set; }
		public string DealerAccountId { get; set; }
		public bool ShipContactSameAsCustomer { get; set; }
		public bool ShipAddressSameAsCustomer { get; set; }
		public string AccountName { get; set; }
		public string AccountDesc { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class MC_Address // MC_Addresses
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AddressID; } set { AddressID = value; } }
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
	public partial class MC_FriendsAndFamilyType // MC_FriendsAndFamilyTypes
	{
		[IgnorePropertyAttribute(true)] public string ID { get { return FriendsAndFamilyTypeID; } set { FriendsAndFamilyTypeID = value; } }
		public string FriendsAndFamilyTypeID { get; set; }
		public string FriendsAndFamilyType { get; set; }
	}
	public partial class MS_AccountHoldCatg1 // MS_AccountHoldCatg1
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return Catg1ID; } set { Catg1ID = value; } }
		public int Catg1ID { get; set; }
		public string CatgName { get; set; }
		public string CatgDescription { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class MS_AccountHoldCatg2 // MS_AccountHoldCatg2
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return Catg2ID; } set { Catg2ID = value; } }
		public int Catg2ID { get; set; }
		public string CatgName { get; set; }
		public int Catg1Id { get; set; }
		public string RecruitFriendlyName { get; set; }
		public string CatgDescription { get; set; }
		public bool IsRepFrontEndHold { get; set; }
		public bool IsRepBackEndHold { get; set; }
		public bool IsTechFrontEndHold { get; set; }
		public bool IsTechBackEndHold { get; set; }
		public bool PreventsContractSale { get; set; }
		public bool IsAccountFlag { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class MS_AccountHold // MS_AccountHolds
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AccountHoldID; } set { AccountHoldID = value; } }
		public long AccountHoldID { get; set; }
		public long AccountId { get; set; }
		public int Catg2Id { get; set; }
		public string HoldDescription { get; set; }
		public string FixedNote { get; set; }
		public string FixedBy { get; set; }
		public DateTime? FixedOn { get; set; }
		public bool IsActive { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class MS_AccountPackageItem // MS_AccountPackageItems
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return AccountPackageItemID; } set { AccountPackageItemID = value; } }
		public int AccountPackageItemID { get; set; }
		public int AccountPackageId { get; set; }
		public string AccountPackageItemTypeId { get; set; }
		public string PackegeItemName { get; set; }
		public string ItemId { get; set; }
		public string ModelNumber { get; set; }
		public bool IsUpgrade { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
	}
	public partial class MS_AccountPackageItemType // MS_AccountPackageItemTypes
	{
		[IgnorePropertyAttribute(true)] public string ID { get { return AccountPackageItemTypeID; } set { AccountPackageItemTypeID = value; } }
		public string AccountPackageItemTypeID { get; set; }
		public string PackageItemTypeName { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int DEX_ROW_ID { get; set; }
	}
	public partial class MS_AccountPackage // MS_AccountPackages
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return AccountPackageID; } set { AccountPackageID = value; } }
		public int AccountPackageID { get; set; }
		public string AccountPackageName { get; set; }
		public string ShortName { get; set; }
		public string Description { get; set; }
		public decimal BasePoints { get; set; }
		public decimal BaseRMR { get; set; }
		public decimal MinRMR { get; set; }
		public decimal MaxRMR { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
	}
	public partial class MS_Account // MS_Accounts
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AccountID; } set { AccountID = value; } }
		public long AccountID { get; set; }
		public long? PremiseAddressId { get; set; }
		public long? IndustryAccountId { get; set; }
		public long? IndustryAccount2Id { get; set; }
		public string SiteTypeId { get; set; }
		public string SystemTypeId { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public short? DslSeizureId { get; set; }
		public string PanelItemId { get; set; }
		public string CellPackageItemId { get; set; }
		public int? ContractId { get; set; }
		public short? SignalFormatTypeId { get; set; }
		public string PanelCode { get; set; }
		public string PanelPhone { get; set; }
		public string PanelLocation { get; set; }
		public string TransformerLocation { get; set; }
		public bool? Privacy { get; set; }
		public string AccountPassword { get; set; }
		public string SimProductBarcodeId { get; set; }
		public string DispatchMessage { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
		public long DEX_ROW_ID { get; set; }
	}
	public partial class MS_AccountSalesInformation // MS_AccountSalesInformations
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AccountID; } set { AccountID = value; } }
		public long AccountID { get; set; }
		public string PaymentTypeId { get; set; }
		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public int? AccountPackageId { get; set; }
		public int? PaymentMethodId { get; set; }
		public int? InitialPaymentMethodId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
		public long? AccountFundingStatusId { get; set; }
		public string AccountPayoutTypeId { get; set; }
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
		public bool OptOutCorporate { get; set; }
		public bool OptOutAffiliate { get; set; }
		public bool? Waived1stmonth { get; set; }
		public decimal? RMRIncreasePoints { get; set; }
		public string AccountCreationTypeId { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
		public long DEX_ROW_ID { get; set; }
	}
	public partial class QL_Address // QL_Address
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return AddressID; } set { AddressID = value; } }
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
		[IgnorePropertyAttribute(true)] public Guid ID { get { return CustomerMasterLeadID; } set { CustomerMasterLeadID = value; } }
		public Guid CustomerMasterLeadID { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long LeadId { get; set; }
		public string CustomerTypeId { get; set; }
	}
	public partial class QL_LeadAddress // QL_LeadAddress
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return LeadAddressID; } set { LeadAddressID = value; } }
		public long LeadAddressID { get; set; }
		public long LeadId { get; set; }
		public long AddressId { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }
	}
	public partial class QL_Lead // QL_Leads
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return LeadID; } set { LeadID = value; } }
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
		[IgnorePropertyAttribute(true)] public int ID { get { return TeamId; } set { TeamId = value; } }
		public int TeamId { get; set; }
		public long AddressId { get; set; }
		public int Version { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
	}
}
