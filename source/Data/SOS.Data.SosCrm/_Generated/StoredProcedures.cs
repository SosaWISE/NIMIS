


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace SOS.Data.SosCrm {
	public partial class SosCrmDataStoredProcedureManager {
		public static StoredProcedure MC_DealerUserGetByDealerID(int? DealerID) {
			StoredProcedure sp = new StoredProcedure("cust_MC_DealerUserGetByDealerID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerID", DealerID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure AE_AgingStepByCMFID(long? CMFID) {
			StoredProcedure sp = new StoredProcedure("custAE_AgingStepByCMFID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_ContractTemplatesGetByInvoiceTemplateId(long? InvoiceTemplateId) {
			StoredProcedure sp = new StoredProcedure("custAE_ContractTemplatesGetByInvoiceTemplateId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceTemplateId", InvoiceTemplateId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerAddToGreatPlains(long? CMFID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerAddToGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerCreateByCustomerID(long? CustomerID,string CustomerTypeId,long? BillingAddressId) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerCreateByCustomerID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			sp.Command.AddParameter("@CustomerTypeId", CustomerTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@BillingAddressId", BillingAddressId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerGetByAccountID(long? AccountID,string CustomerTypeId) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGetByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@CustomerTypeId", CustomerTypeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_CustomerGPSClientDelete(long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGPSClientDelete" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerGPSClientRead(long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGPSClientRead" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerGpsClientSignup(int? DealerId,string SalesRepId,string LocalizationId,string FirstName,string LastName,string Gender,string PhoneHome,string PhoneWork,string PhoneMobile,string Email,string Username,string Password,int? LeadSourceId,int? LeadDispositionId) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGpsClientSignup" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			sp.Command.AddParameter("@LocalizationId", LocalizationId, DbType.AnsiString);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Gender", Gender, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.AnsiString);
			sp.Command.AddParameter("@PhoneWork", PhoneWork, DbType.AnsiString);
			sp.Command.AddParameter("@PhoneMobile", PhoneMobile, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.AnsiString);
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			sp.Command.AddParameter("@LeadSourceId", LeadSourceId, DbType.Int32);
			sp.Command.AddParameter("@LeadDispositionId", LeadDispositionId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure AE_CustomerGpsClientUpateLastLogin(long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGpsClientUpateLastLogin" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerGPSClientUpdate(string LocalizationId,string Prefix,string FirstName,string LastName,string Gender,string PhoneHome,string PhoneWork,string PhoneMobile,string Email,DateTime? DOB,string SSN,string Username,string Password,string StateId,string CountryId,int? TimeZoneId,string StreetAddress,string StreetAddress2,string County,string City,string PostalCode,string PlusFour,string Phone) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerGPSClientUpdate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@LocalizationId", LocalizationId, DbType.AnsiString);
			sp.Command.AddParameter("@Prefix", Prefix, DbType.String);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Gender", Gender, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.AnsiString);
			sp.Command.AddParameter("@PhoneWork", PhoneWork, DbType.AnsiString);
			sp.Command.AddParameter("@PhoneMobile", PhoneMobile, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.AnsiString);
			sp.Command.AddParameter("@DOB", DOB, DbType.DateTime);
			sp.Command.AddParameter("@SSN", SSN, DbType.AnsiString);
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@CountryId", CountryId, DbType.String);
			sp.Command.AddParameter("@TimeZoneId", TimeZoneId, DbType.Int32);
			sp.Command.AddParameter("@StreetAddress", StreetAddress, DbType.String);
			sp.Command.AddParameter("@StreetAddress2", StreetAddress2, DbType.String);
			sp.Command.AddParameter("@County", County, DbType.String);
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@PostalCode", PostalCode, DbType.AnsiString);
			sp.Command.AddParameter("@PlusFour", PlusFour, DbType.AnsiString);
			sp.Command.AddParameter("@Phone", Phone, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_CustomerInformationViewMonitoredPartyByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerInformationViewMonitoredPartyByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerInformationViewSearchByAccountID(int? DealerID,long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerInformationViewSearchByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerID", DealerID, DbType.Int32);
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerInformationViewSearchByCustomerID(int? DealerID,long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerInformationViewSearchByCustomerID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerID", DealerID, DbType.Int32);
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerMasterFileGeneralSearch(long? DealerId,string City,string StateId,string PostalCode,string Email,string FirstName,string LastName,string PhoneNumber,bool? ExcludeLeads,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerMasterFileGeneralSearch" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int64);
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@PostalCode", PostalCode, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.AnsiString);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@PhoneNumber", PhoneNumber, DbType.AnsiString);
			sp.Command.AddParameter("@ExcludeLeads", ExcludeLeads, DbType.Boolean);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure AE_CustomerMasterFileSearchCustomers(string FirstName,bool? FirstNameExact,string LastName,bool? LastNameExact,string PremisePhone,DateTime? DateOfBirth,string CSID,string Email,string Street,bool? StreetExact,string City,string ZipCode,string StateAB) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerMasterFileSearchCustomers" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@FirstNameExact", FirstNameExact, DbType.Boolean);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@LastNameExact", LastNameExact, DbType.Boolean);
			sp.Command.AddParameter("@PremisePhone", PremisePhone, DbType.AnsiString);
			sp.Command.AddParameter("@DateOfBirth", DateOfBirth, DbType.DateTime);
			sp.Command.AddParameter("@CSID", CSID, DbType.String);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@Street", Street, DbType.String);
			sp.Command.AddParameter("@StreetExact", StreetExact, DbType.Boolean);
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.String);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_CustomerMasterFileSearchCustomersByCompanyID(string CompanyID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerMasterFileSearchCustomersByCompanyID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CompanyID", CompanyID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGAdd_Dnc(string PhoneNumber) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGAdd_Dnc" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PhoneNumber", PhoneNumber, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGAE_Customer(long? CustomerIDOld,long? CustomerMasterFileID,long? LeadID,long? AddressID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGAE_Customer" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerIDOld", CustomerIDOld, DbType.Int64);
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@LeadID", LeadID, DbType.Int64);
			sp.Command.AddParameter("@AddressID", AddressID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGEquipment(long? InterimAccountID,long? CustomerMasterFileID,long? MsAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGEquipment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@MsAccountID", MsAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGFromInterim(long? InterimAccountID,bool? SwingEquipment) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGFromInterim" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@SwingEquipment", SwingEquipment, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGGetEmergencyContacts(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGGetEmergencyContacts" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGGetEquipments(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGGetEquipments" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGGetInfo(long? InterimAccountID,string CustomerType) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGGetInfo" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@CustomerType", CustomerType, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGGetPrimeseAddress(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGGetPrimeseAddress" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGGetSystemDetail(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGGetSystemDetail" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMC_Accounts(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMC_Accounts" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMC_Addresses(long? AddressID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMC_Addresses" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AddressID", AddressID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMC_CreditReport(long? InterimAccountID,long? LeadID,long? AddressID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMC_CreditReport" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@LeadID", LeadID, DbType.Int64);
			sp.Command.AddParameter("@AddressID", AddressID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMC_Lead(long? CustomerIDOld,long? CustomerMasterFileID,long? AddressID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMC_Lead" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerIDOld", CustomerIDOld, DbType.Int64);
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@AddressID", AddressID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMS_Account(long? InterimAccountID,long? AccountID,long? PremiseAddressId) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMS_Account" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@PremiseAddressId", PremiseAddressId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGMS_EmergencyContact(long? InterimAccountID,long? Customer1IDNew,long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGMS_EmergencyContact" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			sp.Command.AddParameter("@Customer1IDNew", Customer1IDNew, DbType.Int64);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSWINGQL_Address(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSWINGQL_Address" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerSwungInfo(long? InterimAccountID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerSwungInfo" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InterimAccountID", InterimAccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_CustomerUpdateInGreatPlains(long? CMFID) {
			StoredProcedure sp = new StoredProcedure("custAE_CustomerUpdateInGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID(long? CustomerMasterFileId,long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerMasterFileId", CustomerMasterFileId, DbType.Int64);
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure AE_InventoryItemsSycnWithMsAccountEquipmentInstalled(long? AccountID,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_InvoiceCalculatePrices(long? InvoiceID,string StateID,string PostalCode,bool? HideInvoiceHeader) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceCalculatePrices" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@StateID", StateID, DbType.AnsiString);
			sp.Command.AddParameter("@PostalCode", PostalCode, DbType.AnsiString);
			sp.Command.AddParameter("@HideInvoiceHeader", HideInvoiceHeader, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure AE_InvoiceCreateHeader(long? AccountId,string InvoiceTypeID,int? TaxScheduleId,int? PaymentTermId,decimal? SalesAmount,decimal? OriginalTransactionAmount,decimal? CurrentTransactionAmount,decimal? CostAmount,decimal? TaxAmount,long? ContractID,DateTime? DocDate,DateTime? PostedDate,DateTime? DueDate,DateTime? GLPostDate) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceCreateHeader" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@InvoiceTypeID", InvoiceTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@TaxScheduleId", TaxScheduleId, DbType.Int32);
			sp.Command.AddParameter("@PaymentTermId", PaymentTermId, DbType.Int32);
			sp.Command.AddParameter("@SalesAmount", SalesAmount, DbType.Currency);
			sp.Command.AddParameter("@OriginalTransactionAmount", OriginalTransactionAmount, DbType.Currency);
			sp.Command.AddParameter("@CurrentTransactionAmount", CurrentTransactionAmount, DbType.Currency);
			sp.Command.AddParameter("@CostAmount", CostAmount, DbType.Currency);
			sp.Command.AddParameter("@TaxAmount", TaxAmount, DbType.Currency);
			sp.Command.AddParameter("@ContractID", ContractID, DbType.Int64);
			sp.Command.AddParameter("@DocDate", DocDate, DbType.DateTime);
			sp.Command.AddParameter("@PostedDate", PostedDate, DbType.DateTime);
			sp.Command.AddParameter("@DueDate", DueDate, DbType.DateTime);
			sp.Command.AddParameter("@GLPostDate", GLPostDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure AE_InvoiceCreateMinimal(long? AccountId,string InvoiceTypeId,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceCreateMinimal" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@InvoiceTypeId", InvoiceTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemAddByBarcode(long? InvoiceID,string ProductBarcodeID,string SalesmanID,string TechnicianID,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemAddByBarcode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@ProductBarcodeID", ProductBarcodeID, DbType.String);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemAddByPartNumber(long? InvoiceID,string ItemSku,int? Qty,string SalesmanID,string TechnicianID,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemAddByPartNumber" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@ItemSku", ItemSku, DbType.String);
			sp.Command.AddParameter("@Qty", Qty, DbType.Int32);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemAddExistingEquipment(long? InvoiceID,string ItemSku,int? Qty,string SalesmanID,string TechnicianID,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemAddExistingEquipment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@ItemSku", ItemSku, DbType.String);
			sp.Command.AddParameter("@Qty", Qty, DbType.Int32);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemCreate(long? InvoiceId,string ItemId,short? Qty,string SalesmanId,string TechnicianId,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceId", InvoiceId, DbType.Int64);
			sp.Command.AddParameter("@ItemId", ItemId, DbType.AnsiString);
			sp.Command.AddParameter("@Qty", Qty, DbType.Int16);
			sp.Command.AddParameter("@SalesmanId", SalesmanId, DbType.String);
			sp.Command.AddParameter("@TechnicianId", TechnicianId, DbType.String);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemRefreshMsAccountInstall(long? InvoiceID,long? AccountId,string ActivationFeeItemId,decimal? ActivationFeeActual,string MMRItemId,decimal? MMRActual,string PanelTypeId,string CellularTypeId,bool? Over3Months,string AlarmComPackageId,int? DealerId,string GpEmployeeID,string SalesmanID,string TechnicianID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemRefreshMsAccountInstall" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@ActivationFeeItemId", ActivationFeeItemId, DbType.AnsiString);
			sp.Command.AddParameter("@ActivationFeeActual", ActivationFeeActual, DbType.Currency);
			sp.Command.AddParameter("@MMRItemId", MMRItemId, DbType.AnsiString);
			sp.Command.AddParameter("@MMRActual", MMRActual, DbType.Currency);
			sp.Command.AddParameter("@PanelTypeId", PanelTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@CellularTypeId", CellularTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@Over3Months", Over3Months, DbType.Boolean);
			sp.Command.AddParameter("@AlarmComPackageId", AlarmComPackageId, DbType.String);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemRefreshMsAccountInstall001(long? InvoiceID,long? AccountId,short? BillingDay,string CurrentMonitoringStation,string Email,string ActivationFeeItemId,decimal? ActivationFeeActual,string MMRItemId,decimal? MMRActual,string PanelTypeId,string CellularTypeId,string CellPackageItemId,bool? Over3Months,string AlarmComPackageId,string PaymentTypeId,bool? IsTakeOver,bool? IsOwner,bool? IsMoni,int? ContractTemplateId,int? ContractLength,int? DealerId,string GpEmployeeID,string SalesmanID,string TechnicianID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemRefreshMsAccountInstall001" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@BillingDay", BillingDay, DbType.Int16);
			sp.Command.AddParameter("@CurrentMonitoringStation", CurrentMonitoringStation, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@ActivationFeeItemId", ActivationFeeItemId, DbType.AnsiString);
			sp.Command.AddParameter("@ActivationFeeActual", ActivationFeeActual, DbType.Currency);
			sp.Command.AddParameter("@MMRItemId", MMRItemId, DbType.AnsiString);
			sp.Command.AddParameter("@MMRActual", MMRActual, DbType.Currency);
			sp.Command.AddParameter("@PanelTypeId", PanelTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@CellularTypeId", CellularTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@CellPackageItemId", CellPackageItemId, DbType.AnsiString);
			sp.Command.AddParameter("@Over3Months", Over3Months, DbType.Boolean);
			sp.Command.AddParameter("@AlarmComPackageId", AlarmComPackageId, DbType.String);
			sp.Command.AddParameter("@PaymentTypeId", PaymentTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@IsTakeOver", IsTakeOver, DbType.Boolean);
			sp.Command.AddParameter("@IsOwner", IsOwner, DbType.Boolean);
			sp.Command.AddParameter("@IsMoni", IsMoni, DbType.Boolean);
			sp.Command.AddParameter("@ContractTemplateId", ContractTemplateId, DbType.Int32);
			sp.Command.AddParameter("@ContractLength", ContractLength, DbType.Int32);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceItemUpdate(long? InvoiceItemID,int? Qty,decimal? Price,decimal? SystemPoints,string SalesmanID,string TechnicianID,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceItemUpdate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceItemID", InvoiceItemID, DbType.Int64);
			sp.Command.AddParameter("@Qty", Qty, DbType.Int32);
			sp.Command.AddParameter("@Price", Price, DbType.Currency);
			sp.Command.AddParameter("@SystemPoints", SystemPoints, DbType.Decimal);
			sp.Command.AddParameter("@SalesmanID", SalesmanID, DbType.String);
			sp.Command.AddParameter("@TechnicianID", TechnicianID, DbType.String);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceMsInstallInfoViewGetByIDs(long? InvoiceID,long? AccountId,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceMsInstallInfoViewGetByIDs" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure AE_InvoiceRefreshHeader(long? InvoiceID,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custAE_InvoiceRefreshHeader" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceID", InvoiceID, DbType.Int64);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_ItemActivationFeesGet() {
			StoredProcedure sp = new StoredProcedure("custAE_ItemActivationFeesGet" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure AE_ItemAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custAE_ItemAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure AE_ItemByBarcode(string BarcodeNumber) {
			StoredProcedure sp = new StoredProcedure("custAE_ItemByBarcode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@BarcodeNumber", BarcodeNumber, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure AE_ItemByInvoiceTemplateId(long? InvoiceTemplateId) {
			StoredProcedure sp = new StoredProcedure("custAE_ItemByInvoiceTemplateId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@InvoiceTemplateId", InvoiceTemplateId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure BE_BarcodesExistingBarcode(int? RecruitID,int? DocTypeID,string BarcodeNumber) {
			StoredProcedure sp = new StoredProcedure("custBE_BarcodesExistingBarcode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@DocTypeID", DocTypeID, DbType.Int32);
			sp.Command.AddParameter("@BarcodeNumber", BarcodeNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure BE_BarcodesGetLastBarcodesForRecruitID(int? RecruitID) {
			StoredProcedure sp = new StoredProcedure("custBE_BarcodesGetLastBarcodesForRecruitID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure BX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers(string BarcodeTypeID,long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@BarcodeTypeID", BarcodeTypeID, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure CA_AppointmentGetByUserIdAndDateRange(int? DealerUserId,DateTime? StartDate,DateTime? EndDate) {
			StoredProcedure sp = new StoredProcedure("custCA_AppointmentGetByUserIdAndDateRange" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerUserId", DealerUserId, DbType.Int32);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure DC_PhoneNumbersGetByPhoneNumber(string PhoneNumber) {
			StoredProcedure sp = new StoredProcedure("custDC_PhoneNumbersGetByPhoneNumber" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PhoneNumber", PhoneNumber, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure GS_AccountGetByLaipacUnitID(string UnitID) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGetByLaipacUnitID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@UnitID", UnitID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_LocationGetByLocationTypeID(string LocationTypeID) {
			StoredProcedure sp = new StoredProcedure("custIE_LocationGetByLocationTypeID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@LocationTypeID", LocationTypeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_PackingSlipCreate(string PackingSlipNumber,long? PurchaseOrderId,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custIE_PackingSlipCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PackingSlipNumber", PackingSlipNumber, DbType.AnsiString);
			sp.Command.AddParameter("@PurchaseOrderId", PurchaseOrderId, DbType.Int64);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_PackingSlipItemCreate(int? PackingSlipId,string ProductSkwId,string ItemId,int? Quantity,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custIE_PackingSlipItemCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PackingSlipId", PackingSlipId, DbType.Int32);
			sp.Command.AddParameter("@ProductSkwId", ProductSkwId, DbType.AnsiString);
			sp.Command.AddParameter("@ItemId", ItemId, DbType.AnsiString);
			sp.Command.AddParameter("@Quantity", Quantity, DbType.Int32);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_ProductBarcodeCreate(string ProductBarcodeID,long? ProductOrderItemId,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custIE_ProductBarcodeCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ProductBarcodeID", ProductBarcodeID, DbType.AnsiString);
			sp.Command.AddParameter("@ProductOrderItemId", ProductOrderItemId, DbType.Int64);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_ProductBarcodesReconcileLostEquipment(string Barcode,int? UserId,string EquipmentId,DateTime? LostDate) {
			StoredProcedure sp = new StoredProcedure("custIE_ProductBarcodesReconcileLostEquipment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Barcode", Barcode, DbType.String);
			sp.Command.AddParameter("@UserId", UserId, DbType.Int32);
			sp.Command.AddParameter("@EquipmentId", EquipmentId, DbType.AnsiString);
			sp.Command.AddParameter("@LostDate", LostDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure IE_ProductBarcodeTrackingCreate(string ProductBarcodeTrackingTypeId,string ProductBarcodeId,string LocationTypeID,string LocationID,string Comment,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custIE_ProductBarcodeTrackingCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ProductBarcodeTrackingTypeId", ProductBarcodeTrackingTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@ProductBarcodeId", ProductBarcodeId, DbType.String);
			sp.Command.AddParameter("@LocationTypeID", LocationTypeID, DbType.String);
			sp.Command.AddParameter("@LocationID", LocationID, DbType.String);
			sp.Command.AddParameter("@Comment", Comment, DbType.String);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_PurchaseOrdersAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custIE_PurchaseOrdersAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure IE_PurchaseOrdersGet(string GPPONumber,string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custIE_PurchaseOrdersGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@GPPONumber", GPPONumber, DbType.AnsiString);
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure IE_VendorsAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custIE_VendorsAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure IE_WarehouseSitesAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custIE_WarehouseSitesAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_AccountNoteCat1ByDepartmentId(string DepartmentId) {
			StoredProcedure sp = new StoredProcedure("custMC_AccountNoteCat1ByDepartmentId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DepartmentId", DepartmentId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MC_AccountNotesAllInfoViewGetByIds(long? CMFID,long? CustomerId,long? LeadId,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custMC_AccountNotesAllInfoViewGetByIds" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@CustomerId", CustomerId, DbType.Int64);
			sp.Command.AddParameter("@LeadId", LeadId, DbType.Int64);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure MC_AccountNotesGetByIds(long? CMFID,long? CustomerId,long? LeadId,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custMC_AccountNotesGetByIds" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@CustomerId", CustomerId, DbType.Int64);
			sp.Command.AddParameter("@LeadId", LeadId, DbType.Int64);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure MC_AccountUpdate(long? AccountID,string AccountName,string AccountDesc) {
			StoredProcedure sp = new StoredProcedure("custMC_AccountUpdate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@AccountName", AccountName, DbType.String);
			sp.Command.AddParameter("@AccountDesc", AccountDesc, DbType.String);
			return sp;
		}
		public static StoredProcedure MC_AddressesCreateFromAddressID(long? AddressId) {
			StoredProcedure sp = new StoredProcedure("custMC_AddressesCreateFromAddressID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AddressId", AddressId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MC_AddressGetPremiseByAccountId(long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custMC_AddressGetPremiseByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MC_DealerUserGetByUsernamePasswordAndDealerName(string Username,string Password,string DealerName) {
			StoredProcedure sp = new StoredProcedure("custMC_DealerUserGetByUsernamePasswordAndDealerName" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			sp.Command.AddParameter("@DealerName", DealerName, DbType.String);
			return sp;
		}
		public static StoredProcedure MC_DealerUsersAuthenticate(long? SessionId,long? DealerId,string Username,string Password) {
			StoredProcedure sp = new StoredProcedure("custMC_DealerUsersAuthenticate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@SessionId", SessionId, DbType.Int64);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int64);
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			return sp;
		}
		public static StoredProcedure MC_LeadGetNonSolicitingAndNewCities() {
			StoredProcedure sp = new StoredProcedure("custMC_LeadGetNonSolicitingAndNewCities" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_LeadGetNonSolicitingAndNewCounties() {
			StoredProcedure sp = new StoredProcedure("custMC_LeadGetNonSolicitingAndNewCounties" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_LeadGetNonSolicitingAndNewCountries() {
			StoredProcedure sp = new StoredProcedure("custMC_LeadGetNonSolicitingAndNewCountries" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_LeadGetNonSolicitingAndNewStates() {
			StoredProcedure sp = new StoredProcedure("custMC_LeadGetNonSolicitingAndNewStates" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_LeadGetNonSolicitingTownships() {
			StoredProcedure sp = new StoredProcedure("custMC_LeadGetNonSolicitingTownships" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MC_PoliticalCountryGetCreditsRanByCountryname(string CountryName) {
			StoredProcedure sp = new StoredProcedure("custMC_PoliticalCountryGetCreditsRanByCountryname" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			return sp;
		}
		public static StoredProcedure MC_PoliticalStateGetByStateAB(string StateAB) {
			StoredProcedure sp = new StoredProcedure("custMC_PoliticalStateGetByStateAB" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure MC_PoliticalStateGetCreditsRanByStateName(string StateName) {
			StoredProcedure sp = new StoredProcedure("custMC_PoliticalStateGetCreditsRanByStateName" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_AccountCellularTypesGet() {
			StoredProcedure sp = new StoredProcedure("custMS_AccountCellularTypesGet" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_AccountCreditsAndInstallsBySalesRepByDate(int? OfficeID,string SalesRepId,DateTime? begindate,DateTime? enddate,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountCreditsAndInstallsBySalesRepByDate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@OfficeID", OfficeID, DbType.Int32);
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			sp.Command.AddParameter("@begindate", begindate, DbType.DateTime);
			sp.Command.AddParameter("@enddate", enddate, DbType.DateTime);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountDispatchAgencyAssignmentSave(long? AccountId,int? DispatchAgencyOsId,string MonitoringStationOSId,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountDispatchAgencyAssignmentSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@DispatchAgencyOsId", DispatchAgencyOsId, DbType.Int32);
			sp.Command.AddParameter("@MonitoringStationOSId", MonitoringStationOSId, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId(int? DispatchAgencyId,long? AccountId,long? IndustryAccountId,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DispatchAgencyId", DispatchAgencyId, DbType.Int32);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@IndustryAccountId", IndustryAccountId, DbType.Int64);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEquipmentsAddEquipment(long? AccountId,string AccountEquipmentUpgradeTypeID,int? AccountEventId,long? AccountZoneAssignmentID,string AccountZoneTypeId,decimal? ActualPoints,string BarcodeId,string Comments,string EquipmentId,int? EquipmentLocationId,string SalesmanId,bool? IsExisting,bool? IsExistingWiring,bool? IsMainPanel,bool? IsServiceUpgrade,string ItemDesc,string ItemSKU,decimal? Points,decimal? Price,string Zone,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEquipmentsAddEquipment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@AccountEquipmentUpgradeTypeID", AccountEquipmentUpgradeTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@AccountEventId", AccountEventId, DbType.Int32);
			sp.Command.AddParameter("@AccountZoneAssignmentID", AccountZoneAssignmentID, DbType.Int64);
			sp.Command.AddParameter("@AccountZoneTypeId", AccountZoneTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@ActualPoints", ActualPoints, DbType.Decimal);
			sp.Command.AddParameter("@BarcodeId", BarcodeId, DbType.String);
			sp.Command.AddParameter("@Comments", Comments, DbType.String);
			sp.Command.AddParameter("@EquipmentId", EquipmentId, DbType.AnsiString);
			sp.Command.AddParameter("@EquipmentLocationId", EquipmentLocationId, DbType.Int32);
			sp.Command.AddParameter("@SalesmanId", SalesmanId, DbType.AnsiString);
			sp.Command.AddParameter("@IsExisting", IsExisting, DbType.Boolean);
			sp.Command.AddParameter("@IsExistingWiring", IsExistingWiring, DbType.Boolean);
			sp.Command.AddParameter("@IsMainPanel", IsMainPanel, DbType.Boolean);
			sp.Command.AddParameter("@IsServiceUpgrade", IsServiceUpgrade, DbType.Boolean);
			sp.Command.AddParameter("@ItemDesc", ItemDesc, DbType.String);
			sp.Command.AddParameter("@ItemSKU", ItemSKU, DbType.String);
			sp.Command.AddParameter("@Points", Points, DbType.Decimal);
			sp.Command.AddParameter("@Price", Price, DbType.Currency);
			sp.Command.AddParameter("@Zone", Zone, DbType.AnsiString);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEquipmentsViewAddExistingEquipment(long? AccountId,string EquipmentID,int? EquipmentLocationId,int? ZoneEventTypeId,string Zone,string Comments,bool? IsExisting,bool? IsExistingWiring,bool? IsMainPanel,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEquipmentsViewAddExistingEquipment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@EquipmentID", EquipmentID, DbType.AnsiString);
			sp.Command.AddParameter("@EquipmentLocationId", EquipmentLocationId, DbType.Int32);
			sp.Command.AddParameter("@ZoneEventTypeId", ZoneEventTypeId, DbType.Int32);
			sp.Command.AddParameter("@Zone", Zone, DbType.AnsiString);
			sp.Command.AddParameter("@Comments", Comments, DbType.AnsiString);
			sp.Command.AddParameter("@IsExisting", IsExisting, DbType.Boolean);
			sp.Command.AddParameter("@IsExistingWiring", IsExistingWiring, DbType.Boolean);
			sp.Command.AddParameter("@IsMainPanel", IsMainPanel, DbType.Boolean);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEquipmentsViewNextAssignment(long? AccountId,string ItemSKU,string GpEmployeeId,string CrmUserId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEquipmentsViewNextAssignment" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@ItemSKU", ItemSKU, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			sp.Command.AddParameter("@CrmUserId", CrmUserId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEquipmentsViewNextAssignmentByBarcode(long? AccountId,string BarcodeNumber,string GpEmployeeId,string CrmUserId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEquipmentsViewNextAssignmentByBarcode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@BarcodeNumber", BarcodeNumber, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			sp.Command.AddParameter("@CrmUserId", CrmUserId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEquipmentSyncAssignmetBetweenInvoiceItem(long? AccountEquipmentID,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountEquipmentID", AccountEquipmentID, DbType.Int64);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId(string MonitoringStationOSID,int? EquipmentTypeID,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@MonitoringStationOSID", MonitoringStationOSID, DbType.AnsiString);
			sp.Command.AddParameter("@EquipmentTypeID", EquipmentTypeID, DbType.Int32);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountGetPurchasedAccounts(string City,string StateAB,string County,DateTime? PurchaseDateStart,DateTime? PurchaseDateEnd) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountGetPurchasedAccounts" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.String);
			sp.Command.AddParameter("@County", County, DbType.String);
			sp.Command.AddParameter("@PurchaseDateStart", PurchaseDateStart, DbType.DateTime);
			sp.Command.AddParameter("@PurchaseDateEnd", PurchaseDateEnd, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure MS_AccountHoldsCreate(long? AccountId,int? Catg2Id,string HoldDescription,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountHoldsCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@Catg2Id", Catg2Id, DbType.Int32);
			sp.Command.AddParameter("@HoldDescription", HoldDescription, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountMonitorInformationsByAccountID(long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountMonitorInformationsByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_AccountSalesInformationSaveSalesRepID(long? CustomerMasterFileID,long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountSalesInformationSaveSalesRepID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_AccountSalesInfoViewRead(long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountSalesInfoViewRead" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_AccountSearchAccounts(string FirstName,bool? FirstNameExact,string LastName,bool? LastNameExact,string DevicePhone,string SimProductBarcodeId,DateTime? DateOfBirth,string CSID,string Email,string Street,bool? StreetExact,string City,string ZipCode,string StateAB) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountSearchAccounts" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@FirstNameExact", FirstNameExact, DbType.Boolean);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@LastNameExact", LastNameExact, DbType.Boolean);
			sp.Command.AddParameter("@DevicePhone", DevicePhone, DbType.String);
			sp.Command.AddParameter("@SimProductBarcodeId", SimProductBarcodeId, DbType.AnsiString);
			sp.Command.AddParameter("@DateOfBirth", DateOfBirth, DbType.DateTime);
			sp.Command.AddParameter("@CSID", CSID, DbType.String);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@Street", Street, DbType.String);
			sp.Command.AddParameter("@StreetExact", StreetExact, DbType.Boolean);
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.String);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_AccountSetupCheckListSetByKey(long? AccountId,string Key) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountSetupCheckListSetByKey" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@Key", Key, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_AccountSiteGeneralDispatchByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountSiteGeneralDispatchByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_AccountZoneAssignmentsByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_AccountZoneAssignmentsByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_DispatchAgenciesSaveFromMoniEntity(int? EntityAgenciesID,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_DispatchAgenciesSaveFromMoniEntity" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EntityAgenciesID", EntityAgenciesID, DbType.Int32);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_DispatchAgencyGetByCityStateZips(string City,string State,string Zip) {
			StoredProcedure sp = new StoredProcedure("custMS_DispatchAgencyGetByCityStateZips" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@State", State, DbType.String);
			sp.Command.AddParameter("@Zip", Zip, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId(string City,string State,string Zip,int? DispatchAgencyTypeId) {
			StoredProcedure sp = new StoredProcedure("custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@State", State, DbType.String);
			sp.Command.AddParameter("@Zip", Zip, DbType.String);
			sp.Command.AddParameter("@DispatchAgencyTypeId", DispatchAgencyTypeId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure MS_EquipmentAccountZoneTypeEventsViewGet(string EquipmentId,int? EquipmentAccountZoneTypeId,string MonitoringStationOSId) {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentAccountZoneTypeEventsViewGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipmentId", EquipmentId, DbType.AnsiString);
			sp.Command.AddParameter("@EquipmentAccountZoneTypeId", EquipmentAccountZoneTypeId, DbType.Int32);
			sp.Command.AddParameter("@MonitoringStationOSId", MonitoringStationOSId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_EquipmentAccountZoneTypesViewGet(string EquipmentID) {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentAccountZoneTypesViewGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipmentID", EquipmentID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_EquipmentByBarcode(string BarcodeNumber) {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentByBarcode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@BarcodeNumber", BarcodeNumber, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_EquipmentExistings() {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentExistings" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_EquipmentList() {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentList" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_EquipmentLocationGetPanelLocationByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentLocationGetPanelLocationByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_EquipmentLocationsByMSOID(string MonitoringStationOSID,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentLocationsByMSOID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@MonitoringStationOSID", MonitoringStationOSID, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_EquipmentMostFrequents() {
			StoredProcedure sp = new StoredProcedure("custMS_EquipmentMostFrequents" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_IndustryAccountFindByCallerId(string CallerId) {
			StoredProcedure sp = new StoredProcedure("custMS_IndustryAccountFindByCallerId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CallerId", CallerId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_IndustryAccountFindByCsID(string Csid) {
			StoredProcedure sp = new StoredProcedure("custMS_IndustryAccountFindByCsID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Csid", Csid, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_IndustryAccountGenerate(long? AccountId,bool? IsPrimary,string AccountType,string MonitoringStationOSId,string GpEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custMS_IndustryAccountGenerate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@IsPrimary", IsPrimary, DbType.Boolean);
			sp.Command.AddParameter("@AccountType", AccountType, DbType.AnsiString);
			sp.Command.AddParameter("@MonitoringStationOSId", MonitoringStationOSId, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeId", GpEmployeeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_IndustryAccountNumbersViewGetByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_IndustryAccountNumbersViewGetByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID(long? AccountId,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_LeadTakeOverViewGetByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_LeadTakeOverViewGetByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsBusRulesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsBusRulesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsBusRulesSave(string ErrorNoID,string TableName,string BusRule,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsBusRulesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ErrorNoID", ErrorNoID, DbType.AnsiString);
			sp.Command.AddParameter("@TableName", TableName, DbType.AnsiString);
			sp.Command.AddParameter("@BusRule", BusRule, DbType.AnsiString);
			sp.Command.AddParameter("@IsActive", IsActive, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedOn", CreatedOn, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedOn", ModifiedOn, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsCellProviderSave(string CellProviderID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsCellProviderSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CellProviderID", CellProviderID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@IsActive", IsActive, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedOn", CreatedOn, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedOn", ModifiedOn, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsCellServicesSave(string OptionID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsCellServicesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@OptionID", OptionID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@IsActive", IsActive, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedOn", CreatedOn, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedOn", ModifiedOn, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsContractTypesSave(string ContractTypeID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsContractTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ContractTypeID", ContractTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@IsActive", IsActive, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedOn", CreatedOn, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedOn", ModifiedOn, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgenciesGetByCityStateZips(string City,string State,string Zip) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgenciesGetByCityStateZips" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@State", State, DbType.String);
			sp.Command.AddParameter("@Zip", Zip, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId(string City,string State,string Zip,int? DispatchAgencyTypeId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@State", State, DbType.String);
			sp.Command.AddParameter("@Zip", Zip, DbType.String);
			sp.Command.AddParameter("@DispatchAgencyTypeId", DispatchAgencyTypeId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgenciesGetForZip(string ZipCode) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgenciesGetForZip" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgenciesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgenciesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgenciesSave(string AgencyNumberID,string AgencyTypeId,string AgencyName,string CityName,string StateId,string ZipCode,string Phone1,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgenciesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AgencyNumberID", AgencyNumberID, DbType.AnsiString);
			sp.Command.AddParameter("@AgencyTypeId", AgencyTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@AgencyName", AgencyName, DbType.AnsiString);
			sp.Command.AddParameter("@CityName", CityName, DbType.AnsiString);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			sp.Command.AddParameter("@Phone1", Phone1, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgencyTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgencyTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAgencyTypesSave(string AgencyTypeID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAgencyTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AgencyTypeID", AgencyTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAuthoritiesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAuthoritiesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityAuthoritiesSave(string AuthID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityAuthoritiesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AuthID", AuthID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityBusRulesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityBusRulesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityBusRulesSave(int? ErrorNoID,string TableName,string BusRule,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityBusRulesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ErrorNoID", ErrorNoID, DbType.Int32);
			sp.Command.AddParameter("@TableName", TableName, DbType.AnsiString);
			sp.Command.AddParameter("@BusRule", BusRule, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellProvidersGetAll() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellProvidersGetAll" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellProvidersNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellProvidersNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellProvidersSave(string CellProviderID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellProvidersSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CellProviderID", CellProviderID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellServicesGet(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellServicesGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellServicesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellServicesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityCellServicesSave(string OptionID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityCellServicesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@OptionID", OptionID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityContactTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityContactTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityContactTypesSave(string ContactTypeID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityContactTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ContactTypeID", ContactTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipEventXRefNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipEventXRefNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipEventXRefSave(string EquipTypeID,string EventId,string SiteKind,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipEventXRefSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipTypeID", EquipTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@EventId", EventId, DbType.AnsiString);
			sp.Command.AddParameter("@SiteKind", SiteKind, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipmentLocationsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipmentLocationsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipmentLocationsSave(string EquipLocID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipmentLocationsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipLocID", EquipLocID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipmentTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipmentTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEquipmentTypesSave(string EquipTypeId,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEquipmentTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipTypeId", EquipTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEventsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEventsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityEventsSave(string EventID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityEventsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EventID", EventID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityLanguagesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityLanguagesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityLanguagesSave(string LanguageID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityLanguagesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@LanguageID", LanguageID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityNamePrefixesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityNamePrefixesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityNamePrefixesSave(string Prefix,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityNamePrefixesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Prefix", Prefix, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityNameSuffixesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityNameSuffixesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityNameSuffixesSave(string Suffix,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityNameSuffixesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Suffix", Suffix, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityOosCatsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityOosCatsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityOosCatsSave(string OosCatsID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityOosCatsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@OosCatsID", OosCatsID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityOptionsCellProvGet(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityOptionsCellProvGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityOptionsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityOptionsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityOptionsSave(string OptionID,string UsageId,string Description,string ValidValue,string ValueDescription,string ValueRequired,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityOptionsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@OptionID", OptionID, DbType.AnsiString);
			sp.Command.AddParameter("@UsageId", UsageId, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ValidValue", ValidValue, DbType.AnsiString);
			sp.Command.AddParameter("@ValueDescription", ValueDescription, DbType.AnsiString);
			sp.Command.AddParameter("@ValueRequired", ValueRequired, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPartialBatchesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPartialBatchesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPartialBatchesSave(int? WsiBatchNoID,string CustServNo,string SiteName,int? ServcoNo,DateTime? MmChangeDate,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPartialBatchesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@WsiBatchNoID", WsiBatchNoID, DbType.Int32);
			sp.Command.AddParameter("@CustServNo", CustServNo, DbType.AnsiString);
			sp.Command.AddParameter("@SiteName", SiteName, DbType.AnsiString);
			sp.Command.AddParameter("@ServcoNo", ServcoNo, DbType.Int32);
			sp.Command.AddParameter("@MmChangeDate", MmChangeDate, DbType.DateTime);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPermitTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPermitTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPermitTypesSave(string PermitTypeID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPermitTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PermitTypeID", PermitTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPhoneTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPhoneTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPhoneTypesSave(string PhoneTypeID,string Description,string Method,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPhoneTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PhoneTypeID", PhoneTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@Method", Method, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPrefixesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPrefixesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityPrefixesSave(string CellFlagID,byte? CsNoLength,string CmPurchase,int? ServCoNO,string CellProvider,string SystemTypeId,short? CoNo,string BrandedFlag,string ReceiverPhone,string AlarmNetCityCs,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityPrefixesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CellFlagID", CellFlagID, DbType.AnsiString);
			sp.Command.AddParameter("@CsNoLength", CsNoLength, DbType.Byte);
			sp.Command.AddParameter("@CmPurchase", CmPurchase, DbType.AnsiString);
			sp.Command.AddParameter("@ServCoNO", ServCoNO, DbType.Int32);
			sp.Command.AddParameter("@CellProvider", CellProvider, DbType.AnsiString);
			sp.Command.AddParameter("@SystemTypeId", SystemTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@CoNo", CoNo, DbType.Int16);
			sp.Command.AddParameter("@BrandedFlag", BrandedFlag, DbType.AnsiString);
			sp.Command.AddParameter("@ReceiverPhone", ReceiverPhone, DbType.AnsiString);
			sp.Command.AddParameter("@AlarmNetCityCs", AlarmNetCityCs, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityRelationsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityRelationsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityRelationsSave(string RelationID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityRelationsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@RelationID", RelationID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySecGroupsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySecGroupsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySecGroupsSave(string SecurityGroupID,string SecurityLevel,string AllUsers,string AllAccounts,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySecGroupsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@SecurityGroupID", SecurityGroupID, DbType.AnsiString);
			sp.Command.AddParameter("@SecurityLevel", SecurityLevel, DbType.AnsiString);
			sp.Command.AddParameter("@AllUsers", AllUsers, DbType.AnsiString);
			sp.Command.AddParameter("@AllAccounts", AllAccounts, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityServiceCompaniesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityServiceCompaniesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityServiceCompaniesSave(string ServCoNumberID,string ServCoName,string ServCoAddress1,string ServCoAddress2,string CityName,string StateId,string ZipCode,string Phone1,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityServiceCompaniesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ServCoNumberID", ServCoNumberID, DbType.AnsiString);
			sp.Command.AddParameter("@ServCoName", ServCoName, DbType.AnsiString);
			sp.Command.AddParameter("@ServCoAddress1", ServCoAddress1, DbType.AnsiString);
			sp.Command.AddParameter("@ServCoAddress2", ServCoAddress2, DbType.AnsiString);
			sp.Command.AddParameter("@CityName", CityName, DbType.AnsiString);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			sp.Command.AddParameter("@Phone1", Phone1, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteOptionsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteOptionsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteOptionsSave(string CsNumber,string OptionId,string OptionValue,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteOptionsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CsNumber", CsNumber, DbType.AnsiString);
			sp.Command.AddParameter("@OptionId", OptionId, DbType.AnsiString);
			sp.Command.AddParameter("@OptionValue", OptionValue, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteSystemInfoSave(long? IndustryAccountID,string site_name,string sitetype_id,string sitestat_id,string site_addr1,string site_addr2,string city_name,string county_name,string state_id,string zip_code,string phone1,string ext1,string street_no,string street_name,string country_name,int? timezone_no,string timezone_descr,int? servco_no,string install_servco_no,string cspart_no,string subdivision,string cross_street,string codeword1,string codeword2,DateTime? orig_install_date,string lang_id,string cs_no,string systype_id,string sec_systype_id,string panel_phone,string panel_location,string receiver_phone,short? ati_hours,byte? ati_minutes,string panel_code,string twoway_device_id,string alkup_cs_no,string blkup_cs_no,string ontest_flag,DateTime? ontest_expire_date,string oos_flag,DateTime? install_date,string monitor_type,string GpEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteSystemInfoSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@IndustryAccountID", IndustryAccountID, DbType.Int64);
			sp.Command.AddParameter("@site_name", site_name, DbType.AnsiString);
			sp.Command.AddParameter("@sitetype_id", sitetype_id, DbType.AnsiString);
			sp.Command.AddParameter("@sitestat_id", sitestat_id, DbType.AnsiString);
			sp.Command.AddParameter("@site_addr1", site_addr1, DbType.AnsiString);
			sp.Command.AddParameter("@site_addr2", site_addr2, DbType.AnsiString);
			sp.Command.AddParameter("@city_name", city_name, DbType.AnsiString);
			sp.Command.AddParameter("@county_name", county_name, DbType.AnsiString);
			sp.Command.AddParameter("@state_id", state_id, DbType.AnsiString);
			sp.Command.AddParameter("@zip_code", zip_code, DbType.AnsiString);
			sp.Command.AddParameter("@phone1", phone1, DbType.AnsiString);
			sp.Command.AddParameter("@ext1", ext1, DbType.AnsiString);
			sp.Command.AddParameter("@street_no", street_no, DbType.AnsiString);
			sp.Command.AddParameter("@street_name", street_name, DbType.AnsiString);
			sp.Command.AddParameter("@country_name", country_name, DbType.AnsiString);
			sp.Command.AddParameter("@timezone_no", timezone_no, DbType.Int32);
			sp.Command.AddParameter("@timezone_descr", timezone_descr, DbType.AnsiString);
			sp.Command.AddParameter("@servco_no", servco_no, DbType.Int32);
			sp.Command.AddParameter("@install_servco_no", install_servco_no, DbType.AnsiString);
			sp.Command.AddParameter("@cspart_no", cspart_no, DbType.AnsiString);
			sp.Command.AddParameter("@subdivision", subdivision, DbType.AnsiString);
			sp.Command.AddParameter("@cross_street", cross_street, DbType.AnsiString);
			sp.Command.AddParameter("@codeword1", codeword1, DbType.AnsiString);
			sp.Command.AddParameter("@codeword2", codeword2, DbType.AnsiString);
			sp.Command.AddParameter("@orig_install_date", orig_install_date, DbType.DateTime);
			sp.Command.AddParameter("@lang_id", lang_id, DbType.AnsiString);
			sp.Command.AddParameter("@cs_no", cs_no, DbType.AnsiString);
			sp.Command.AddParameter("@systype_id", systype_id, DbType.AnsiString);
			sp.Command.AddParameter("@sec_systype_id", sec_systype_id, DbType.AnsiString);
			sp.Command.AddParameter("@panel_phone", panel_phone, DbType.AnsiString);
			sp.Command.AddParameter("@panel_location", panel_location, DbType.AnsiString);
			sp.Command.AddParameter("@receiver_phone", receiver_phone, DbType.AnsiString);
			sp.Command.AddParameter("@ati_hours", ati_hours, DbType.Int16);
			sp.Command.AddParameter("@ati_minutes", ati_minutes, DbType.Byte);
			sp.Command.AddParameter("@panel_code", panel_code, DbType.AnsiString);
			sp.Command.AddParameter("@twoway_device_id", twoway_device_id, DbType.AnsiString);
			sp.Command.AddParameter("@alkup_cs_no", alkup_cs_no, DbType.AnsiString);
			sp.Command.AddParameter("@blkup_cs_no", blkup_cs_no, DbType.AnsiString);
			sp.Command.AddParameter("@ontest_flag", ontest_flag, DbType.AnsiString);
			sp.Command.AddParameter("@ontest_expire_date", ontest_expire_date, DbType.DateTime);
			sp.Command.AddParameter("@oos_flag", oos_flag, DbType.AnsiString);
			sp.Command.AddParameter("@install_date", install_date, DbType.DateTime);
			sp.Command.AddParameter("@monitor_type", monitor_type, DbType.AnsiString);
			sp.Command.AddParameter("@GpEmployeeID", GpEmployeeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteSystemOptionsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteSystemOptionsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteSystemOptionsSave(string CsNumberID,string OptionId,string OptionValue,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteSystemOptionsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CsNumberID", CsNumberID, DbType.AnsiString);
			sp.Command.AddParameter("@OptionId", OptionId, DbType.AnsiString);
			sp.Command.AddParameter("@OptionValue", OptionValue, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId(string SiteTypeID) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@SiteTypeID", SiteTypeID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySiteTypesSave(string SiteTypeID,string Description,string SiteKind,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySiteTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@SiteTypeID", SiteTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@SiteKind", SiteKind, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityStatesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityStatesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityStatesSave(string StateID,string StateName,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityStatesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@StateID", StateID, DbType.AnsiString);
			sp.Command.AddParameter("@StateName", StateName, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySystemTypesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySystemTypesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySystemTypesSave(string SystemTypeID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySystemTypesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@SystemTypeID", SystemTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySystemTypeXRefNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySystemTypeXRefNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySystemTypeXRefSave(string DigitalSystemTypeId,string TwoWayDeviceId,string CellSystemTypeId,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySystemTypeXRefSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DigitalSystemTypeId", DigitalSystemTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@TwoWayDeviceId", TwoWayDeviceId, DbType.AnsiString);
			sp.Command.AddParameter("@CellSystemTypeId", CellSystemTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySysTypesCellDeviceGet(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySysTypesCellDeviceGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySysTypesGetByPanelTypeId(string PanelTypeId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySysTypesGetByPanelTypeId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PanelTypeId", PanelTypeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntitySysTypesPanelGet(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntitySysTypesPanelGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityTestCatsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityTestCatsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityTestCatsSave(string TestCatID,string Description,short? DefaultHours,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityTestCatsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@TestCatID", TestCatID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@DefaultHours", DefaultHours, DbType.Int16);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityTwoWaysNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityTwoWaysNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityTwoWaysSave(string TwoWayDeviceID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityTwoWaysSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@TwoWayDeviceID", TwoWayDeviceID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityZipGetByZipCode(string ZipCode) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityZipGetByZipCode" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityZipsNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityZipsNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityZipsSave(string CityNameID,string CountyName,string StateId,string ZipCode,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityZipsSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CityNameID", CityNameID, DbType.AnsiString);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.AnsiString);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityZoneStatesNuke() {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityZoneStatesNuke" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEntityZoneStatesSave(string ZoneStateID,string Description,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEntityZoneStatesSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ZoneStateID", ZoneStateID, DbType.AnsiString);
			sp.Command.AddParameter("@Description", Description, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure MS_MonitronicsEquipEventXRefSave(string EquipTypeID,string EventId,string SiteKind,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn) {
			StoredProcedure sp = new StoredProcedure("custMS_MonitronicsEquipEventXRefSave" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@EquipTypeID", EquipTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@EventId", EventId, DbType.AnsiString);
			sp.Command.AddParameter("@SiteKind", SiteKind, DbType.AnsiString);
			sp.Command.AddParameter("@IsActive", IsActive, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedOn", CreatedOn, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedOn", ModifiedOn, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_ReceiverBlockCellDeviceInfoViewGetByAccountID(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure MS_ReceiverLineBlockAccountGetNewByAccountID(long? AccountID,string CreatedBy,string ReceiverLineID) {
			StoredProcedure sp = new StoredProcedure("custMS_ReceiverLineBlockAccountGetNewByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			sp.Command.AddParameter("@ReceiverLineID", ReceiverLineID, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure MS_TimeZoneLookupGetByStateAB(string StateAB) {
			StoredProcedure sp = new StoredProcedure("custMS_TimeZoneLookupGetByStateAB" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure MS_VendorAlarmComAccountsGetByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custMS_VendorAlarmComAccountsGetByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_CreditReportMaxScoreByCmfID(long? CustomerMasterFileId) {
			StoredProcedure sp = new StoredProcedure("custQL_CreditReportMaxScoreByCmfID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerMasterFileId", CustomerMasterFileId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_CreditReportTransactionAndTokenViewGet(long? CreditReportID) {
			StoredProcedure sp = new StoredProcedure("custQL_CreditReportTransactionAndTokenViewGet" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CreditReportID", CreditReportID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_LeadBasicInfoViewCreate(long? AddressId,int? DealerId,string LocalizationId,int? TeamLocationId,int? SeasonId,string SalesRepId,int? LeadSourceId,int? LeadDispositionId,string Salutation,string FirstName,string MiddleName,string LastName,string Suffix,string SSN,DateTime? DOB,string DL,string DLStateID,string Email,string PhoneHome,string PhoneWork,string PhoneMobile,string ProductSkwId) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadBasicInfoViewCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AddressId", AddressId, DbType.Int64);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@LocalizationId", LocalizationId, DbType.AnsiString);
			sp.Command.AddParameter("@TeamLocationId", TeamLocationId, DbType.Int32);
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			sp.Command.AddParameter("@LeadSourceId", LeadSourceId, DbType.Int32);
			sp.Command.AddParameter("@LeadDispositionId", LeadDispositionId, DbType.Int32);
			sp.Command.AddParameter("@Salutation", Salutation, DbType.String);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@MiddleName", MiddleName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Suffix", Suffix, DbType.String);
			sp.Command.AddParameter("@SSN", SSN, DbType.String);
			sp.Command.AddParameter("@DOB", DOB, DbType.DateTime);
			sp.Command.AddParameter("@DL", DL, DbType.String);
			sp.Command.AddParameter("@DLStateID", DLStateID, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.String);
			sp.Command.AddParameter("@PhoneWork", PhoneWork, DbType.String);
			sp.Command.AddParameter("@PhoneMobile", PhoneMobile, DbType.String);
			sp.Command.AddParameter("@ProductSkwId", ProductSkwId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure QL_LeadDispositionGetByDealerId(int? DealerId) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadDispositionGetByDealerId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure QL_LeadsByAccountId(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadsByAccountId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_LeadsCreateBasic(int? DealerId,string LocalizationId,int? TeamLocationId,int? SeasonId,string SalesRepId,int? LeadSourceId,int? LeadDispositionId,string Salutation,string FirstName,string MiddleName,string LastName,string Suffix,string SSN,DateTime? DOB,string DL,string DLStateID,string Email,string PhoneHome,string PhoneWork,string PhoneMobile,string StreetAddress,string City,string StateId,string PostalCode,string PlusFour,string CountryId,string Phone) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadsCreateBasic" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@LocalizationId", LocalizationId, DbType.AnsiString);
			sp.Command.AddParameter("@TeamLocationId", TeamLocationId, DbType.Int32);
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			sp.Command.AddParameter("@LeadSourceId", LeadSourceId, DbType.Int32);
			sp.Command.AddParameter("@LeadDispositionId", LeadDispositionId, DbType.Int32);
			sp.Command.AddParameter("@Salutation", Salutation, DbType.String);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@MiddleName", MiddleName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Suffix", Suffix, DbType.String);
			sp.Command.AddParameter("@SSN", SSN, DbType.String);
			sp.Command.AddParameter("@DOB", DOB, DbType.DateTime);
			sp.Command.AddParameter("@DL", DL, DbType.String);
			sp.Command.AddParameter("@DLStateID", DLStateID, DbType.AnsiString);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.String);
			sp.Command.AddParameter("@PhoneWork", PhoneWork, DbType.String);
			sp.Command.AddParameter("@PhoneMobile", PhoneMobile, DbType.String);
			sp.Command.AddParameter("@StreetAddress", StreetAddress, DbType.String);
			sp.Command.AddParameter("@City", City, DbType.String);
			sp.Command.AddParameter("@StateId", StateId, DbType.AnsiString);
			sp.Command.AddParameter("@PostalCode", PostalCode, DbType.AnsiString);
			sp.Command.AddParameter("@PlusFour", PlusFour, DbType.AnsiString);
			sp.Command.AddParameter("@CountryId", CountryId, DbType.AnsiString);
			sp.Command.AddParameter("@Phone", Phone, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure QL_LeadSearchResultViewSearch(string FirstName,string LastName,string Phone,int? DealerId,string Email,long? LeadId,int? LeadDispositionId,int? LeadSourceId,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadSearchResultViewSearch" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Phone", Phone, DbType.String);
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@LeadId", LeadId, DbType.Int64);
			sp.Command.AddParameter("@LeadDispositionId", LeadDispositionId, DbType.Int32);
			sp.Command.AddParameter("@LeadSourceId", LeadSourceId, DbType.Int32);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure QL_LeadSourceGetByDealerId(int? DealerId) {
			StoredProcedure sp = new StoredProcedure("custQL_LeadSourceGetByDealerId" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@DealerId", DealerId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure QL_QualifyCustomerInfoViewByAccountID(long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custQL_QualifyCustomerInfoViewByAccountID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_QualifyCustomerInfoViewByCustomerID(long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custQL_QualifyCustomerInfoViewByCustomerID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure QL_QualifyCustomerInfoViewByLeadID(long? LeadID) {
			StoredProcedure sp = new StoredProcedure("custQL_QualifyCustomerInfoViewByLeadID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@LeadID", LeadID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure SAE_AgingAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custSAE_AgingAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure SAE_BillingHistoryAddFromGreatPlains() {
			StoredProcedure sp = new StoredProcedure("custSAE_BillingHistoryAddFromGreatPlains" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure SAE_BillingHistoryByCMFID(long? CMFID) {
			StoredProcedure sp = new StoredProcedure("custSAE_BillingHistoryByCMFID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure SAE_BillingInfoSummaryByCMFID(long? CMFID,long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custSAE_BillingInfoSummaryByCMFID" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure SAE_CreditReportAbaraGetByRandomNumber(int? RndNumber) {
			StoredProcedure sp = new StoredProcedure("custSAE_CreditReportAbaraGetByRandomNumber" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@RndNumber", RndNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure SE_ScheduleBlockCreate(string Block,string ZipCode,double? MaxRadius,double? Distance,DateTime? StartTime,DateTime? EndTime,int? AvailableSlots,string TechnicianId,bool? IsTechConfirmed,string Color,bool? IsBlocked) {
			StoredProcedure sp = new StoredProcedure("custSE_ScheduleBlockCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@Block", Block, DbType.AnsiString);
			sp.Command.AddParameter("@ZipCode", ZipCode, DbType.AnsiString);
			sp.Command.AddParameter("@MaxRadius", MaxRadius, DbType.Double);
			sp.Command.AddParameter("@Distance", Distance, DbType.Double);
			sp.Command.AddParameter("@StartTime", StartTime, DbType.DateTime);
			sp.Command.AddParameter("@EndTime", EndTime, DbType.DateTime);
			sp.Command.AddParameter("@AvailableSlots", AvailableSlots, DbType.Int32);
			sp.Command.AddParameter("@TechnicianId", TechnicianId, DbType.AnsiString);
			sp.Command.AddParameter("@IsTechConfirmed", IsTechConfirmed, DbType.Boolean);
			sp.Command.AddParameter("@Color", Color, DbType.AnsiString);
			sp.Command.AddParameter("@IsBlocked", IsBlocked, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure SE_ScheduleTicketCreate(long? TicketId,long? BlockId,DateTime? AppointmentDate,int? TravelTime) {
			StoredProcedure sp = new StoredProcedure("custSE_ScheduleTicketCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@TicketId", TicketId, DbType.Int64);
			sp.Command.AddParameter("@BlockId", BlockId, DbType.Int64);
			sp.Command.AddParameter("@AppointmentDate", AppointmentDate, DbType.DateTime);
			sp.Command.AddParameter("@TravelTime", TravelTime, DbType.Int32);
			return sp;
		}
		public static StoredProcedure SE_ScheduleTicketTechUpdate(long? BlockId,string TechnicianId) {
			StoredProcedure sp = new StoredProcedure("custSE_ScheduleTicketTechUpdate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@BlockId", BlockId, DbType.Int64);
			sp.Command.AddParameter("@TechnicianId", TechnicianId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure SE_TechnicianAvailabilityCreate(string TechnicianId,DateTime? StartDateTime,DateTime? EndDateTime) {
			StoredProcedure sp = new StoredProcedure("custSE_TechnicianAvailabilityCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@TechnicianId", TechnicianId, DbType.AnsiString);
			sp.Command.AddParameter("@StartDateTime", StartDateTime, DbType.DateTime);
			sp.Command.AddParameter("@EndDateTime", EndDateTime, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure SE_TicketCreate(long? AccountId,long? MonitoringStationNo,int? TicketTypeId,int? StatusCodeId,string MoniConfirmation,string TechnicianId,decimal? TripCharges,string Appointment,string AgentConfirmation,DateTime? ExpirationDate,string Notes) {
			StoredProcedure sp = new StoredProcedure("custSE_TicketCreate" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@MonitoringStationNo", MonitoringStationNo, DbType.Int64);
			sp.Command.AddParameter("@TicketTypeId", TicketTypeId, DbType.Int32);
			sp.Command.AddParameter("@StatusCodeId", StatusCodeId, DbType.Int32);
			sp.Command.AddParameter("@MoniConfirmation", MoniConfirmation, DbType.String);
			sp.Command.AddParameter("@TechnicianId", TechnicianId, DbType.AnsiString);
			sp.Command.AddParameter("@TripCharges", TripCharges, DbType.Currency);
			sp.Command.AddParameter("@Appointment", Appointment, DbType.String);
			sp.Command.AddParameter("@AgentConfirmation", AgentConfirmation, DbType.String);
			sp.Command.AddParameter("@ExpirationDate", ExpirationDate, DbType.DateTime);
			sp.Command.AddParameter("@Notes", Notes, DbType.String);
			return sp;
		}
		public static StoredProcedure SE_TicketReScheduleList(int? HoursPassed) {
			StoredProcedure sp = new StoredProcedure("custSE_TicketReScheduleList" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@HoursPassed", HoursPassed, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_ApplicationGetCurrentApplicationPermissions(int? ApplicationID) {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationGetCurrentApplicationPermissions" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_ApplicationsGetApplicationList() {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationsGetApplicationList" ,DataService.GetInstance("SosCrmProvider"));
			return sp;
		}
		public static StoredProcedure UI_ApplicationsGetByPermission(string PrincipalName,int? PermissionTypeID) {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationsGetByPermission" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@PrincipalName", PrincipalName, DbType.String);
			sp.Command.AddParameter("@PermissionTypeID", PermissionTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_ApplicationVersionsCreateVersion(int? ApplicationID,int? DeployedFileID,int? MajorVersionNumber,int? MinorVersionNumber,int? BuildNumber,int? RevisionNumber,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationVersionsCreateVersion" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			sp.Command.AddParameter("@DeployedFileID", DeployedFileID, DbType.Int32);
			sp.Command.AddParameter("@MajorVersionNumber", MajorVersionNumber, DbType.Int32);
			sp.Command.AddParameter("@MinorVersionNumber", MinorVersionNumber, DbType.Int32);
			sp.Command.AddParameter("@BuildNumber", BuildNumber, DbType.Int32);
			sp.Command.AddParameter("@RevisionNumber", RevisionNumber, DbType.Int32);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure UI_ApplicationVersionsGetLatestVersionByApplication(int? ApplicationID) {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationVersionsGetLatestVersionByApplication" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_ApplicationVersionsSetActiveVersion(int? ApplicationVersionID) {
			StoredProcedure sp = new StoredProcedure("custUI_ApplicationVersionsSetActiveVersion" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationVersionID", ApplicationVersionID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_MenuItemDeleteItem(int? MenuItemID) {
			StoredProcedure sp = new StoredProcedure("custUI_MenuItemDeleteItem" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@MenuItemID", MenuItemID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure UI_MenuItemsGetActionApplicationMappingsAD(string UserNameAD,string GroupNamesAD) {
			StoredProcedure sp = new StoredProcedure("custUI_MenuItemsGetActionApplicationMappingsAD" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@UserNameAD", UserNameAD, DbType.String);
			sp.Command.AddParameter("@GroupNamesAD", GroupNamesAD, DbType.String);
			return sp;
		}
		public static StoredProcedure UI_MenuItemsGetCurrentApplicationMenu(int? ApplicationID,bool? IncludeNotVisible) {
			StoredProcedure sp = new StoredProcedure("custUI_MenuItemsGetCurrentApplicationMenu" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			sp.Command.AddParameter("@IncludeNotVisible", IncludeNotVisible, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure UI_MenuItemsGetCurrentApplicationMenuAD(int? ApplicationID,string UserNameAD,string GroupNamesAD) {
			StoredProcedure sp = new StoredProcedure("custUI_MenuItemsGetCurrentApplicationMenuAD" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			sp.Command.AddParameter("@UserNameAD", UserNameAD, DbType.String);
			sp.Command.AddParameter("@GroupNamesAD", GroupNamesAD, DbType.String);
			return sp;
		}
		public static StoredProcedure UI_MenuItemsGetDashboardMenuAD(string UserNameAD,string GroupNamesAD) {
			StoredProcedure sp = new StoredProcedure("custUI_MenuItemsGetDashboardMenuAD" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@UserNameAD", UserNameAD, DbType.String);
			sp.Command.AddParameter("@GroupNamesAD", GroupNamesAD, DbType.String);
			return sp;
		}
		public static StoredProcedure UI_MenusCopyCurrentMenu(int? ApplicationID,int? TargetVersionID,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custUI_MenusCopyCurrentMenu" ,DataService.GetInstance("SosCrmProvider"));
			sp.Command.AddParameter("@ApplicationID", ApplicationID, DbType.Int32);
			sp.Command.AddParameter("@TargetVersionID", TargetVersionID, DbType.Int32);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
	}
}
 