


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace NXS.Data.GreatPlains {
	public partial class GreatPlainsStoredProcedureManager {
		public static StoredProcedure GetEICFilingStatus() {
			StoredProcedure sp = new StoredProcedure("ppCust_GetEICFilingStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure GetFedFilingStatus() {
			StoredProcedure sp = new StoredProcedure("ppCust_GetFedFilingStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure GetStateFilingStatus(string StateAB) {
			StoredProcedure sp = new StoredProcedure("ppCust_GetStateFilingStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@StateAB", StateAB, DbType.String);
			return sp;
		}
		public static StoredProcedure GetWorkersComp() {
			StoredProcedure sp = new StoredProcedure("ppCust_GetWorkersComp" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure ActivateSalespersonIfInactive(string SalespersonID) {
			StoredProcedure sp = new StoredProcedure("ppCustActivateSalespersonIfInactive" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@SalespersonID", SalespersonID, DbType.String);
			return sp;
		}
		public static StoredProcedure CalculateContractPayoffAmount(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustCalculateContractPayoffAmount" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure CreateContract(string I_vCustomerNumber,short? I_vContractLength,short? I_vContractPeriod,decimal? I_vTotalContractValue,string I_vPriceSchedule,string I_vSalesRepID,string I_vAddressCode,string I_vCountry,string I_vTimeZone,string I_vTaxCode,string I_vLocationCode,short? I_vBillingDay,short? I_vBillPeriod,DateTime? I_vBillStartDate,DateTime? I_vBillEndDate,short? I_vBillingCycle,string I_vContractType,string I_vDescription,string I_vServiceType,DateTime? I_vInstallDate,DateTime? I_vInstallTime,string I_vCreatedByID,DateTime? I_vCreatedDate,DateTime? I_vCreatedTime,bool? I_vIsOnHold,string I_vItemSku,decimal? I_vQuantity,string I_vItemDescription,int? O_iErrorState,string oErrString) {
			StoredProcedure sp = new StoredProcedure("ppCustCreateContract" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@I_vCustomerNumber", I_vCustomerNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vContractLength", I_vContractLength, DbType.Int16);
			sp.Command.AddParameter("@I_vContractPeriod", I_vContractPeriod, DbType.Int16);
			sp.Command.AddParameter("@I_vTotalContractValue", I_vTotalContractValue, DbType.Currency);
			sp.Command.AddParameter("@I_vPriceSchedule", I_vPriceSchedule, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vSalesRepID", I_vSalesRepID, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vAddressCode", I_vAddressCode, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vCountry", I_vCountry, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vTimeZone", I_vTimeZone, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vTaxCode", I_vTaxCode, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vLocationCode", I_vLocationCode, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vBillingDay", I_vBillingDay, DbType.Int16);
			sp.Command.AddParameter("@I_vBillPeriod", I_vBillPeriod, DbType.Int16);
			sp.Command.AddParameter("@I_vBillStartDate", I_vBillStartDate, DbType.DateTime);
			sp.Command.AddParameter("@I_vBillEndDate", I_vBillEndDate, DbType.DateTime);
			sp.Command.AddParameter("@I_vBillingCycle", I_vBillingCycle, DbType.Int16);
			sp.Command.AddParameter("@I_vContractType", I_vContractType, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vDescription", I_vDescription, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vServiceType", I_vServiceType, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vInstallDate", I_vInstallDate, DbType.DateTime);
			sp.Command.AddParameter("@I_vInstallTime", I_vInstallTime, DbType.DateTime);
			sp.Command.AddParameter("@I_vCreatedByID", I_vCreatedByID, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vCreatedDate", I_vCreatedDate, DbType.DateTime);
			sp.Command.AddParameter("@I_vCreatedTime", I_vCreatedTime, DbType.DateTime);
			sp.Command.AddParameter("@I_vIsOnHold", I_vIsOnHold, DbType.Boolean);
			sp.Command.AddParameter("@I_vItemSku", I_vItemSku, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vQuantity", I_vQuantity, DbType.Decimal);
			sp.Command.AddParameter("@I_vItemDescription", I_vItemDescription, DbType.AnsiStringFixedLength);
			sp.Command.AddOutputParameter("@O_iErrorState", DbType.Int32);
			sp.Command.AddOutputParameter("@oErrString", DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure DeactivateSalesperson(string SalespersonID) {
			StoredProcedure sp = new StoredProcedure("ppCustDeactivateSalesperson" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@SalespersonID", SalespersonID, DbType.String);
			return sp;
		}
		public static StoredProcedure EnsureContractPricingSetup(string I_vPriceSchedule,string I_vItemSku) {
			StoredProcedure sp = new StoredProcedure("ppCustEnsureContractPricingSetup" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@I_vPriceSchedule", I_vPriceSchedule, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vItemSku", I_vItemSku, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure EnsureCustomerPriceLevelExists(string PriceLevel) {
			StoredProcedure sp = new StoredProcedure("ppCustEnsureCustomerPriceLevelExists" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@PriceLevel", PriceLevel, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure EnsureUniqueVendorDocumentNumber(string VendorID,string DocumentNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustEnsureUniqueVendorDocumentNumber" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@VendorID", VendorID, DbType.String);
			sp.Command.AddParameter("@DocumentNumber", DocumentNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCorpDepartments() {
			StoredProcedure sp = new StoredProcedure("ppCustGetCorpDepartments" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure GetCustomerAddressByADRSCODE(string CUSTNMBR,string ADRSCODE) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerAddressByADRSCODE" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CUSTNMBR", CUSTNMBR, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@ADRSCODE", ADRSCODE, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure GetCustomerAgeing(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerAgeing" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCustomerARIssue(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerARIssue" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCustomerBillDate(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerBillDate" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCustomerMaximumPaymentAmount(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerMaximumPaymentAmount" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCustomerOutstandingInvoices(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerOutstandingInvoices" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetCustomerPaymentMethod(string CUSTNMBR) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerPaymentMethod" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CUSTNMBR", CUSTNMBR, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure GetCustomerUnpostedPayments(string AccountId) {
			StoredProcedure sp = new StoredProcedure("ppCustGetCustomerUnpostedPayments" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@AccountId", AccountId, DbType.String);
			return sp;
		}
		public static StoredProcedure GetEftCashReceipts(DateTime? PostingDate,string BatchNumber,string CustomerIDList,int? NTransactionsExpected) {
			StoredProcedure sp = new StoredProcedure("ppCustGetEftCashReceipts" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@PostingDate", PostingDate, DbType.DateTime);
			sp.Command.AddParameter("@BatchNumber", BatchNumber, DbType.String);
			sp.Command.AddParameter("@CustomerIDList", CustomerIDList, DbType.String);
			sp.Command.AddParameter("@NTransactionsExpected", NTransactionsExpected, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GetEftTransactions(DateTime? PostingDate,string BatchNumber,string CustomerIDList,int? NTransactionsExpected) {
			StoredProcedure sp = new StoredProcedure("ppCustGetEftTransactions" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@PostingDate", PostingDate, DbType.DateTime);
			sp.Command.AddParameter("@BatchNumber", BatchNumber, DbType.String);
			sp.Command.AddParameter("@CustomerIDList", CustomerIDList, DbType.String);
			sp.Command.AddParameter("@NTransactionsExpected", NTransactionsExpected, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GetEmployeeInfo(bool? InActive,string FirstName,string LastName,string Department) {
			StoredProcedure sp = new StoredProcedure("ppCustGetEmployeeInfo" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@InActive", InActive, DbType.Boolean);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@Department", Department, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure GetInvoiceDetail(string DocumentNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetInvoiceDetail" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@DocumentNumber", DocumentNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetNOpenContractInvoicesByCustomer(string CustomerNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetNOpenContractInvoicesByCustomer" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure GetPaymentDetail(string DocumentNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustGetPaymentDetail" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@DocumentNumber", DocumentNumber, DbType.String);
			return sp;
		}
		public static StoredProcedure GetPhoneNumbers(string AccountID) {
			StoredProcedure sp = new StoredProcedure("ppCustGetPhoneNumbers" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.String);
			return sp;
		}
		public static StoredProcedure GetSalespersonInactiveStatus(string SalespersonID) {
			StoredProcedure sp = new StoredProcedure("ppCustGetSalespersonInactiveStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@SalespersonID", SalespersonID, DbType.String);
			return sp;
		}
		public static StoredProcedure GetStatementLettersInfo() {
			StoredProcedure sp = new StoredProcedure("ppCustGetStatementLettersInfo" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure GetTaxCodes() {
			StoredProcedure sp = new StoredProcedure("ppCustGetTaxCodes" ,DataService.GetInstance("GreatPlainsProvider"));
			return sp;
		}
		public static StoredProcedure PayrollGetTotalBackendPayments(string GPEmployeeID,DateTime? StartDate,DateTime? EndDate) {
			StoredProcedure sp = new StoredProcedure("ppCustPayrollGetTotalBackendPayments" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure PayrollGetTotalSalaryPayments(string GPEmployeeID,DateTime? StartDate,DateTime? EndDate) {
			StoredProcedure sp = new StoredProcedure("ppCustPayrollGetTotalSalaryPayments" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure SendCustomersToCollectionsForAgency(string UPSZone,string ShipmentMethod,string UserDef2,string AccountIDs) {
			StoredProcedure sp = new StoredProcedure("ppCustSendCustomersToCollectionsForAgency" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@UPSZone", UPSZone, DbType.String);
			sp.Command.AddParameter("@ShipmentMethod", ShipmentMethod, DbType.String);
			sp.Command.AddParameter("@UserDef2", UserDef2, DbType.String);
			sp.Command.AddParameter("@AccountIDs", AccountIDs, DbType.String);
			return sp;
		}
		public static StoredProcedure SetCustomerARIssue(string CustomerNumber,string ARIssue) {
			StoredProcedure sp = new StoredProcedure("ppCustSetCustomerARIssue" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.String);
			sp.Command.AddParameter("@ARIssue", ARIssue, DbType.String);
			return sp;
		}
		public static StoredProcedure SetCustomerClass(string I_vCustomerNumber,int? O_iErrorState,string oErrString) {
			StoredProcedure sp = new StoredProcedure("ppCustSetCustomerClass" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@I_vCustomerNumber", I_vCustomerNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddOutputParameter("@O_iErrorState", DbType.Int32);
			sp.Command.AddOutputParameter("@oErrString", DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure SetCustomerEFTInfo(string CustomerNumber,short? AccountType,string AccountNumber,string RoutingNumber) {
			StoredProcedure sp = new StoredProcedure("ppCustSetCustomerEFTInfo" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CustomerNumber", CustomerNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@AccountType", AccountType, DbType.Int16);
			sp.Command.AddParameter("@AccountNumber", AccountNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@RoutingNumber", RoutingNumber, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure SetPaymentInfoToMS273512(string I_vMSO_InstanceGUID,string I_vBACHNUMB,string I_vMSO_Doc_Number,int? O_iErrorState,string oErrString) {
			StoredProcedure sp = new StoredProcedure("ppCustSetPaymentInfoToMS273512" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@I_vMSO_InstanceGUID", I_vMSO_InstanceGUID, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vBACHNUMB", I_vBACHNUMB, DbType.AnsiStringFixedLength);
			sp.Command.AddOutputParameter("@I_vMSO_Doc_Number", DbType.AnsiStringFixedLength);
			sp.Command.AddOutputParameter("@O_iErrorState", DbType.Int32);
			sp.Command.AddOutputParameter("@oErrString", DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure SetupEFT(string I_vCustomerNumber,short? I_vAccountType,string I_vAccountNumber,string I_vRoutingNumber,string I_vBillingAddressCode,int? O_iErrorState,string oErrString) {
			StoredProcedure sp = new StoredProcedure("ppCustSetupEFT" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@I_vCustomerNumber", I_vCustomerNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vAccountType", I_vAccountType, DbType.Int16);
			sp.Command.AddParameter("@I_vAccountNumber", I_vAccountNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vRoutingNumber", I_vRoutingNumber, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@I_vBillingAddressCode", I_vBillingAddressCode, DbType.AnsiStringFixedLength);
			sp.Command.AddOutputParameter("@O_iErrorState", DbType.Int32);
			sp.Command.AddOutputParameter("@oErrString", DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure UpdateCustomerCardActiveStatus(bool? InactiveStatus,bool? HoldStatus,string AccountIDs) {
			StoredProcedure sp = new StoredProcedure("ppCustUpdateCustomerCardActiveStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@InactiveStatus", InactiveStatus, DbType.Boolean);
			sp.Command.AddParameter("@HoldStatus", HoldStatus, DbType.Boolean);
			sp.Command.AddParameter("@AccountIDs", AccountIDs, DbType.String);
			return sp;
		}
		public static StoredProcedure UpdateCustomersContractsHoldStatus(bool? CreditHold,string AccountIDs) {
			StoredProcedure sp = new StoredProcedure("ppCustUpdateCustomersContractsHoldStatus" ,DataService.GetInstance("GreatPlainsProvider"));
			sp.Command.AddParameter("@CreditHold", CreditHold, DbType.Boolean);
			sp.Command.AddParameter("@AccountIDs", AccountIDs, DbType.String);
			return sp;
		}
	}
}
 
