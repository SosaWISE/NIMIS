using Dapper;
using NXS.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	public partial class Sprocs
	{
		private readonly DBase db;
		public Sprocs(DBase db)
		{
			this.db = db;
		}

		public Task<IEnumerable<T>> MC_DealerUserGetByDealerID<T>(int? DealerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerID", DealerID);
			return db.QueryAsync<T>("cust_MC_DealerUserGetByDealerID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_AgingStepByCMFID<T>(long? CMFID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			return db.QueryAsync<T>("custAE_AgingStepByCMFID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_ContractTemplatesGetByInvoiceTemplateId<T>(long? InvoiceTemplateId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceTemplateId", InvoiceTemplateId);
			return db.QueryAsync<T>("custAE_ContractTemplatesGetByInvoiceTemplateId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerAddToGreatPlains<T>(long? CMFID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			return db.QueryAsync<T>("custAE_CustomerAddToGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerCreateByCustomerID<T>(long? CustomerID,string CustomerTypeId,long? BillingAddressId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerID", CustomerID);
			p.Add("@CustomerTypeId", CustomerTypeId);
			p.Add("@BillingAddressId", BillingAddressId);
			return db.QueryAsync<T>("custAE_CustomerCreateByCustomerID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGetByAccountID<T>(long? AccountID,string CustomerTypeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			p.Add("@CustomerTypeId", CustomerTypeId);
			return db.QueryAsync<T>("custAE_CustomerGetByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGPSClientDelete<T>(long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_CustomerGPSClientDelete", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGPSClientRead<T>(long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_CustomerGPSClientRead", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGpsClientSignup<T>(int? DealerId,string SalesRepId,string LocalizationId,string FirstName,string LastName,string Gender,string PhoneHome,string PhoneWork,string PhoneMobile,string Email,string Username,string Password,int? LeadSourceId,int? LeadDispositionId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerId", DealerId);
			p.Add("@SalesRepId", SalesRepId);
			p.Add("@LocalizationId", LocalizationId);
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@Gender", Gender);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@PhoneWork", PhoneWork);
			p.Add("@PhoneMobile", PhoneMobile);
			p.Add("@Email", Email);
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			p.Add("@LeadSourceId", LeadSourceId);
			p.Add("@LeadDispositionId", LeadDispositionId);
			return db.QueryAsync<T>("custAE_CustomerGpsClientSignup", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGpsClientUpateLastLogin<T>(long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_CustomerGpsClientUpateLastLogin", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerGPSClientUpdate<T>(string LocalizationId,string Prefix,string FirstName,string LastName,string Gender,string PhoneHome,string PhoneWork,string PhoneMobile,string Email,DateTime? DOB,string SSN,string Username,string Password,string StateId,string CountryId,int? TimeZoneId,string StreetAddress,string StreetAddress2,string County,string City,string PostalCode,string PlusFour,string Phone)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@LocalizationId", LocalizationId);
			p.Add("@Prefix", Prefix);
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@Gender", Gender);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@PhoneWork", PhoneWork);
			p.Add("@PhoneMobile", PhoneMobile);
			p.Add("@Email", Email);
			p.Add("@DOB", DOB);
			p.Add("@SSN", SSN);
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			p.Add("@StateId", StateId);
			p.Add("@CountryId", CountryId);
			p.Add("@TimeZoneId", TimeZoneId);
			p.Add("@StreetAddress", StreetAddress);
			p.Add("@StreetAddress2", StreetAddress2);
			p.Add("@County", County);
			p.Add("@City", City);
			p.Add("@PostalCode", PostalCode);
			p.Add("@PlusFour", PlusFour);
			p.Add("@Phone", Phone);
			return db.QueryAsync<T>("custAE_CustomerGPSClientUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerInformationViewMonitoredPartyByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custAE_CustomerInformationViewMonitoredPartyByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerInformationViewSearchByAccountID<T>(int? DealerID,long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerID", DealerID);
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_CustomerInformationViewSearchByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerInformationViewSearchByCustomerID<T>(int? DealerID,long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerID", DealerID);
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_CustomerInformationViewSearchByCustomerID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerMasterFileGeneralSearch<T>(long? DealerId,string City,string StateId,string PostalCode,string Email,string FirstName,string LastName,string PhoneNumber,bool? ExcludeLeads,int? PageSize,int? PageNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerId", DealerId);
			p.Add("@City", City);
			p.Add("@StateId", StateId);
			p.Add("@PostalCode", PostalCode);
			p.Add("@Email", Email);
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@PhoneNumber", PhoneNumber);
			p.Add("@ExcludeLeads", ExcludeLeads);
			p.Add("@PageSize", PageSize);
			p.Add("@PageNumber", PageNumber);
			return db.QueryAsync<T>("custAE_CustomerMasterFileGeneralSearch", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerMasterFileSearchCustomers<T>(string FirstName,bool? FirstNameExact,string LastName,bool? LastNameExact,string PremisePhone,DateTime? DateOfBirth,string CSID,string Email,string Street,bool? StreetExact,string City,string ZipCode,string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@FirstNameExact", FirstNameExact);
			p.Add("@LastName", LastName);
			p.Add("@LastNameExact", LastNameExact);
			p.Add("@PremisePhone", PremisePhone);
			p.Add("@DateOfBirth", DateOfBirth);
			p.Add("@CSID", CSID);
			p.Add("@Email", Email);
			p.Add("@Street", Street);
			p.Add("@StreetExact", StreetExact);
			p.Add("@City", City);
			p.Add("@ZipCode", ZipCode);
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custAE_CustomerMasterFileSearchCustomers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerMasterFileSearchCustomersByCompanyID<T>(string CompanyID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CompanyID", CompanyID);
			return db.QueryAsync<T>("custAE_CustomerMasterFileSearchCustomersByCompanyID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGAdd_Dnc<T>(string PhoneNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PhoneNumber", PhoneNumber);
			return db.QueryAsync<T>("custAE_CustomerSWINGAdd_Dnc", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGAE_Customer<T>(long? CustomerIDOld,long? CustomerMasterFileID,long? LeadID,long? AddressID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerIDOld", CustomerIDOld);
			p.Add("@CustomerMasterFileID", CustomerMasterFileID);
			p.Add("@LeadID", LeadID);
			p.Add("@AddressID", AddressID);
			return db.QueryAsync<T>("custAE_CustomerSWINGAE_Customer", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGEquipment<T>(long? InterimAccountID,long? CustomerMasterFileID,long? MsAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@CustomerMasterFileID", CustomerMasterFileID);
			p.Add("@MsAccountID", MsAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGEquipment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGFromInterim<T>(long? InterimAccountID,bool? SwingEquipment)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@SwingEquipment", SwingEquipment);
			return db.QueryAsync<T>("custAE_CustomerSWINGFromInterim", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGGetEmergencyContacts<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGGetEmergencyContacts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGGetEquipments<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGGetEquipments", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGGetInfo<T>(long? InterimAccountID,string CustomerType)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@CustomerType", CustomerType);
			return db.QueryAsync<T>("custAE_CustomerSWINGGetInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGGetPrimeseAddress<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGGetPrimeseAddress", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGGetSystemDetail<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGGetSystemDetail", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMC_Accounts<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGMC_Accounts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMC_Addresses<T>(long? AddressID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AddressID", AddressID);
			return db.QueryAsync<T>("custAE_CustomerSWINGMC_Addresses", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMC_CreditReport<T>(long? InterimAccountID,long? LeadID,long? AddressID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@LeadID", LeadID);
			p.Add("@AddressID", AddressID);
			return db.QueryAsync<T>("custAE_CustomerSWINGMC_CreditReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMC_Lead<T>(long? CustomerIDOld,long? CustomerMasterFileID,long? AddressID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerIDOld", CustomerIDOld);
			p.Add("@CustomerMasterFileID", CustomerMasterFileID);
			p.Add("@AddressID", AddressID);
			return db.QueryAsync<T>("custAE_CustomerSWINGMC_Lead", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMS_Account<T>(long? InterimAccountID,long? AccountID,long? PremiseAddressId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@AccountID", AccountID);
			p.Add("@PremiseAddressId", PremiseAddressId);
			return db.QueryAsync<T>("custAE_CustomerSWINGMS_Account", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGMS_EmergencyContact<T>(long? InterimAccountID,long? Customer1IDNew,long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			p.Add("@Customer1IDNew", Customer1IDNew);
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGMS_EmergencyContact", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSWINGQL_Address<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSWINGQL_Address", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerSwungInfo<T>(long? InterimAccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InterimAccountID", InterimAccountID);
			return db.QueryAsync<T>("custAE_CustomerSwungInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_CustomerUpdateInGreatPlains<T>(long? CMFID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			return db.QueryAsync<T>("custAE_CustomerUpdateInGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID<T>(long? CustomerMasterFileId,long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerMasterFileId", CustomerMasterFileId);
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InventoryItemsSycnWithMsAccountEquipmentInstalled<T>(long? AccountID,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceCalculatePrices<T>(long? InvoiceID,string StateID,string PostalCode,bool? HideInvoiceHeader)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@StateID", StateID);
			p.Add("@PostalCode", PostalCode);
			p.Add("@HideInvoiceHeader", HideInvoiceHeader);
			return db.QueryAsync<T>("custAE_InvoiceCalculatePrices", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceCreateHeader<T>(long? AccountId,string InvoiceTypeID,int? TaxScheduleId,int? PaymentTermId,decimal? SalesAmount,decimal? OriginalTransactionAmount,decimal? CurrentTransactionAmount,decimal? CostAmount,decimal? TaxAmount,long? ContractID,DateTime? DocDate,DateTime? PostedDate,DateTime? DueDate,DateTime? GLPostDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@InvoiceTypeID", InvoiceTypeID);
			p.Add("@TaxScheduleId", TaxScheduleId);
			p.Add("@PaymentTermId", PaymentTermId);
			p.Add("@SalesAmount", SalesAmount);
			p.Add("@OriginalTransactionAmount", OriginalTransactionAmount);
			p.Add("@CurrentTransactionAmount", CurrentTransactionAmount);
			p.Add("@CostAmount", CostAmount);
			p.Add("@TaxAmount", TaxAmount);
			p.Add("@ContractID", ContractID);
			p.Add("@DocDate", DocDate);
			p.Add("@PostedDate", PostedDate);
			p.Add("@DueDate", DueDate);
			p.Add("@GLPostDate", GLPostDate);
			return db.QueryAsync<T>("custAE_InvoiceCreateHeader", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceCreateMinimal<T>(long? AccountId,string InvoiceTypeId,string CreatedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@InvoiceTypeId", InvoiceTypeId);
			p.Add("@CreatedBy", CreatedBy);
			return db.QueryAsync<T>("custAE_InvoiceCreateMinimal", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemAddByBarcode<T>(long? InvoiceID,string ProductBarcodeID,string SalesmanID,string TechnicianID,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@ProductBarcodeID", ProductBarcodeID);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custAE_InvoiceItemAddByBarcode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemAddByPartNumber<T>(long? InvoiceID,string ItemSku,int? Qty,string SalesmanID,string TechnicianID,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@ItemSku", ItemSku);
			p.Add("@Qty", Qty);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custAE_InvoiceItemAddByPartNumber", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemAddExistingEquipment<T>(long? InvoiceID,string ItemSku,int? Qty,string SalesmanID,string TechnicianID,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@ItemSku", ItemSku);
			p.Add("@Qty", Qty);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custAE_InvoiceItemAddExistingEquipment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemCreate<T>(long? InvoiceId,string ItemId,short? Qty,string SalesmanId,string TechnicianId,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceId", InvoiceId);
			p.Add("@ItemId", ItemId);
			p.Add("@Qty", Qty);
			p.Add("@SalesmanId", SalesmanId);
			p.Add("@TechnicianId", TechnicianId);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custAE_InvoiceItemCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemRefreshMsAccountInstall<T>(long? InvoiceID,long? AccountId,string ActivationFeeItemId,decimal? ActivationFeeActual,string MMRItemId,decimal? MMRActual,string PanelTypeId,string CellularTypeId,bool? Over3Months,string AlarmComPackageId,int? DealerId,string GpEmployeeID,string SalesmanID,string TechnicianID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@AccountId", AccountId);
			p.Add("@ActivationFeeItemId", ActivationFeeItemId);
			p.Add("@ActivationFeeActual", ActivationFeeActual);
			p.Add("@MMRItemId", MMRItemId);
			p.Add("@MMRActual", MMRActual);
			p.Add("@PanelTypeId", PanelTypeId);
			p.Add("@CellularTypeId", CellularTypeId);
			p.Add("@Over3Months", Over3Months);
			p.Add("@AlarmComPackageId", AlarmComPackageId);
			p.Add("@DealerId", DealerId);
			p.Add("@GpEmployeeID", GpEmployeeID);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			return db.QueryAsync<T>("custAE_InvoiceItemRefreshMsAccountInstall", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemRefreshMsAccountInstall001<T>(long? InvoiceID,long? AccountId,short? BillingDay,string CurrentMonitoringStation,string Email,string ActivationFeeItemId,decimal? ActivationFeeActual,string MMRItemId,decimal? MMRActual,string PanelTypeId,string CellularTypeId,string CellPackageItemId,bool? Over3Months,string AlarmComPackageId,string PaymentTypeId,bool? IsTakeOver,bool? IsOwner,bool? IsMoni,int? ContractTemplateId,int? ContractLength,int? DealerId,string GpEmployeeID,string SalesmanID,string TechnicianID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@AccountId", AccountId);
			p.Add("@BillingDay", BillingDay);
			p.Add("@CurrentMonitoringStation", CurrentMonitoringStation);
			p.Add("@Email", Email);
			p.Add("@ActivationFeeItemId", ActivationFeeItemId);
			p.Add("@ActivationFeeActual", ActivationFeeActual);
			p.Add("@MMRItemId", MMRItemId);
			p.Add("@MMRActual", MMRActual);
			p.Add("@PanelTypeId", PanelTypeId);
			p.Add("@CellularTypeId", CellularTypeId);
			p.Add("@CellPackageItemId", CellPackageItemId);
			p.Add("@Over3Months", Over3Months);
			p.Add("@AlarmComPackageId", AlarmComPackageId);
			p.Add("@PaymentTypeId", PaymentTypeId);
			p.Add("@IsTakeOver", IsTakeOver);
			p.Add("@IsOwner", IsOwner);
			p.Add("@IsMoni", IsMoni);
			p.Add("@ContractTemplateId", ContractTemplateId);
			p.Add("@ContractLength", ContractLength);
			p.Add("@DealerId", DealerId);
			p.Add("@GpEmployeeID", GpEmployeeID);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			return db.QueryAsync<T>("custAE_InvoiceItemRefreshMsAccountInstall001", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceItemUpdate<T>(long? InvoiceItemID,int? Qty,decimal? Price,decimal? SystemPoints,string SalesmanID,string TechnicianID,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceItemID", InvoiceItemID);
			p.Add("@Qty", Qty);
			p.Add("@Price", Price);
			p.Add("@SystemPoints", SystemPoints);
			p.Add("@SalesmanID", SalesmanID);
			p.Add("@TechnicianID", TechnicianID);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custAE_InvoiceItemUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceMsInstallInfoViewGetByIDs<T>(long? InvoiceID,long? AccountId,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@AccountId", AccountId);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custAE_InvoiceMsInstallInfoViewGetByIDs", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_InvoiceRefreshHeader<T>(long? InvoiceID,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceID", InvoiceID);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custAE_InvoiceRefreshHeader", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_ItemActivationFeesGet<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custAE_ItemActivationFeesGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_ItemAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custAE_ItemAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_ItemByBarcode<T>(string BarcodeNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BarcodeNumber", BarcodeNumber);
			return db.QueryAsync<T>("custAE_ItemByBarcode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> AE_ItemByInvoiceTemplateId<T>(long? InvoiceTemplateId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@InvoiceTemplateId", InvoiceTemplateId);
			return db.QueryAsync<T>("custAE_ItemByInvoiceTemplateId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> BE_BarcodesExistingBarcode<T>(int? RecruitID,int? DocTypeID,string BarcodeNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			p.Add("@DocTypeID", DocTypeID);
			p.Add("@BarcodeNumber", BarcodeNumber);
			return db.QueryAsync<T>("custBE_BarcodesExistingBarcode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> BE_BarcodesGetLastBarcodesForRecruitID<T>(int? RecruitID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			return db.QueryAsync<T>("custBE_BarcodesGetLastBarcodesForRecruitID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> BX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers<T>(string BarcodeTypeID,long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BarcodeTypeID", BarcodeTypeID);
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> CA_AppointmentGetByUserIdAndDateRange<T>(int? DealerUserId,DateTime? StartDate,DateTime? EndDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerUserId", DealerUserId);
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			return db.QueryAsync<T>("custCA_AppointmentGetByUserIdAndDateRange", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> DC_PhoneNumbersGetByPhoneNumber<T>(string PhoneNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PhoneNumber", PhoneNumber);
			return db.QueryAsync<T>("custDC_PhoneNumbersGetByPhoneNumber", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> GS_AccountGetByLaipacUnitID<T>(string UnitID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UnitID", UnitID);
			return db.QueryAsync<T>("custGS_AccountGetByLaipacUnitID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_LocationGetByLocationTypeID<T>(string LocationTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@LocationTypeID", LocationTypeID);
			return db.QueryAsync<T>("custIE_LocationGetByLocationTypeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_PackingSlipCreate<T>(string PackingSlipNumber,long? PurchaseOrderId,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PackingSlipNumber", PackingSlipNumber);
			p.Add("@PurchaseOrderId", PurchaseOrderId);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custIE_PackingSlipCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_PackingSlipItemCreate<T>(int? PackingSlipId,string ProductSkwId,string ItemId,int? Quantity,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PackingSlipId", PackingSlipId);
			p.Add("@ProductSkwId", ProductSkwId);
			p.Add("@ItemId", ItemId);
			p.Add("@Quantity", Quantity);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custIE_PackingSlipItemCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_ProductBarcodeCreate<T>(string ProductBarcodeID,long? ProductOrderItemId,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ProductBarcodeID", ProductBarcodeID);
			p.Add("@ProductOrderItemId", ProductOrderItemId);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custIE_ProductBarcodeCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_ProductBarcodesReconcileLostEquipment<T>(string Barcode,int? UserId,string EquipmentId,DateTime? LostDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Barcode", Barcode);
			p.Add("@UserId", UserId);
			p.Add("@EquipmentId", EquipmentId);
			p.Add("@LostDate", LostDate);
			return db.QueryAsync<T>("custIE_ProductBarcodesReconcileLostEquipment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_ProductBarcodeTrackingCreate<T>(string ProductBarcodeTrackingTypeId,string ProductBarcodeId,string LocationTypeID,string LocationID,string Comment,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ProductBarcodeTrackingTypeId", ProductBarcodeTrackingTypeId);
			p.Add("@ProductBarcodeId", ProductBarcodeId);
			p.Add("@LocationTypeID", LocationTypeID);
			p.Add("@LocationID", LocationID);
			p.Add("@Comment", Comment);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custIE_ProductBarcodeTrackingCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_ProductBarcodeTrackingHistoryByBarcodeID<T>(long? ProductBarcodeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ProductBarcodeId", ProductBarcodeId);
			return db.QueryAsync<T>("custIE_ProductBarcodeTrackingHistoryByBarcodeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_PurchaseOrdersAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custIE_PurchaseOrdersAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_PurchaseOrdersGet<T>(string GPPONumber,string GPEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPPONumber", GPPONumber);
			p.Add("@GPEmployeeID", GPEmployeeID);
			return db.QueryAsync<T>("custIE_PurchaseOrdersGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_VendorsAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custIE_VendorsAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> IE_WarehouseSitesAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custIE_WarehouseSitesAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AccountNoteCat1ByDepartmentId<T>(string DepartmentId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DepartmentId", DepartmentId);
			return db.QueryAsync<T>("custMC_AccountNoteCat1ByDepartmentId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AccountNotesAllInfoViewGetByIds<T>(long? CMFID,long? CustomerId,long? LeadId,int? PageSize,int? PageNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			p.Add("@CustomerId", CustomerId);
			p.Add("@LeadId", LeadId);
			p.Add("@PageSize", PageSize);
			p.Add("@PageNumber", PageNumber);
			return db.QueryAsync<T>("custMC_AccountNotesAllInfoViewGetByIds", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AccountNotesGetByIds<T>(long? CMFID,long? CustomerId,long? LeadId,int? PageSize,int? PageNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			p.Add("@CustomerId", CustomerId);
			p.Add("@LeadId", LeadId);
			p.Add("@PageSize", PageSize);
			p.Add("@PageNumber", PageNumber);
			return db.QueryAsync<T>("custMC_AccountNotesGetByIds", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AccountUpdate<T>(long? AccountID,string AccountName,string AccountDesc)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			p.Add("@AccountName", AccountName);
			p.Add("@AccountDesc", AccountDesc);
			return db.QueryAsync<T>("custMC_AccountUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AddressesCreateFromAddressID<T>(long? AddressId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AddressId", AddressId);
			return db.QueryAsync<T>("custMC_AddressesCreateFromAddressID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_AddressGetPremiseByAccountId<T>(long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custMC_AddressGetPremiseByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_DealerUserGetByUsernamePasswordAndDealerName<T>(string Username,string Password,string DealerName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			p.Add("@DealerName", DealerName);
			return db.QueryAsync<T>("custMC_DealerUserGetByUsernamePasswordAndDealerName", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_DealerUsersAuthenticate<T>(long? SessionId,long? DealerId,string Username,string Password)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SessionId", SessionId);
			p.Add("@DealerId", DealerId);
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			return db.QueryAsync<T>("custMC_DealerUsersAuthenticate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_LeadGetNonSolicitingAndNewCities<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMC_LeadGetNonSolicitingAndNewCities", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_LeadGetNonSolicitingAndNewCounties<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMC_LeadGetNonSolicitingAndNewCounties", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_LeadGetNonSolicitingAndNewCountries<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMC_LeadGetNonSolicitingAndNewCountries", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_LeadGetNonSolicitingAndNewStates<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMC_LeadGetNonSolicitingAndNewStates", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_LeadGetNonSolicitingTownships<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMC_LeadGetNonSolicitingTownships", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_PoliticalCountryGetCreditsRanByCountryname<T>(string CountryName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CountryName", CountryName);
			return db.QueryAsync<T>("custMC_PoliticalCountryGetCreditsRanByCountryname", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_PoliticalStateGetByStateAB<T>(string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custMC_PoliticalStateGetByStateAB", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MC_PoliticalStateGetCreditsRanByStateName<T>(string StateName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateName", StateName);
			return db.QueryAsync<T>("custMC_PoliticalStateGetCreditsRanByStateName", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountCellularTypesGet<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_AccountCellularTypesGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountCreditsAndInstallsBySalesRepByDate<T>(int? OfficeID,string SalesRepId,DateTime? begindate,DateTime? enddate,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@OfficeID", OfficeID);
			p.Add("@SalesRepId", SalesRepId);
			p.Add("@begindate", begindate);
			p.Add("@enddate", enddate);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_AccountCreditsAndInstallsBySalesRepByDate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountDispatchAgencyAssignmentSave<T>(long? AccountId,int? DispatchAgencyOsId,string MonitoringStationOSId,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@DispatchAgencyOsId", DispatchAgencyOsId);
			p.Add("@MonitoringStationOSId", MonitoringStationOSId);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_AccountDispatchAgencyAssignmentSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId<T>(int? DispatchAgencyId,long? AccountId,long? IndustryAccountId,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DispatchAgencyId", DispatchAgencyId);
			p.Add("@AccountId", AccountId);
			p.Add("@IndustryAccountId", IndustryAccountId);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEquipmentsAddEquipment<T>(long? AccountId,string AccountEquipmentUpgradeTypeID,int? AccountEventId,long? AccountZoneAssignmentID,string AccountZoneTypeId,decimal? ActualPoints,string BarcodeId,string Comments,string EquipmentId,int? EquipmentLocationId,string SalesmanId,bool? IsExisting,bool? IsExistingWiring,bool? IsMainPanel,bool? IsServiceUpgrade,string ItemDesc,string ItemSKU,decimal? Points,decimal? Price,string Zone,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@AccountEquipmentUpgradeTypeID", AccountEquipmentUpgradeTypeID);
			p.Add("@AccountEventId", AccountEventId);
			p.Add("@AccountZoneAssignmentID", AccountZoneAssignmentID);
			p.Add("@AccountZoneTypeId", AccountZoneTypeId);
			p.Add("@ActualPoints", ActualPoints);
			p.Add("@BarcodeId", BarcodeId);
			p.Add("@Comments", Comments);
			p.Add("@EquipmentId", EquipmentId);
			p.Add("@EquipmentLocationId", EquipmentLocationId);
			p.Add("@SalesmanId", SalesmanId);
			p.Add("@IsExisting", IsExisting);
			p.Add("@IsExistingWiring", IsExistingWiring);
			p.Add("@IsMainPanel", IsMainPanel);
			p.Add("@IsServiceUpgrade", IsServiceUpgrade);
			p.Add("@ItemDesc", ItemDesc);
			p.Add("@ItemSKU", ItemSKU);
			p.Add("@Points", Points);
			p.Add("@Price", Price);
			p.Add("@Zone", Zone);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custMS_AccountEquipmentsAddEquipment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEquipmentsViewAddExistingEquipment<T>(long? AccountId,string EquipmentID,int? EquipmentLocationId,int? ZoneEventTypeId,string Zone,string Comments,bool? IsExisting,bool? IsExistingWiring,bool? IsMainPanel,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@EquipmentID", EquipmentID);
			p.Add("@EquipmentLocationId", EquipmentLocationId);
			p.Add("@ZoneEventTypeId", ZoneEventTypeId);
			p.Add("@Zone", Zone);
			p.Add("@Comments", Comments);
			p.Add("@IsExisting", IsExisting);
			p.Add("@IsExistingWiring", IsExistingWiring);
			p.Add("@IsMainPanel", IsMainPanel);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custMS_AccountEquipmentsViewAddExistingEquipment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEquipmentsViewNextAssignment<T>(long? AccountId,string ItemSKU,string GpEmployeeId,string CrmUserId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@ItemSKU", ItemSKU);
			p.Add("@GpEmployeeId", GpEmployeeId);
			p.Add("@CrmUserId", CrmUserId);
			return db.QueryAsync<T>("custMS_AccountEquipmentsViewNextAssignment", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEquipmentsViewNextAssignmentByBarcode<T>(long? AccountId,string BarcodeNumber,string GpEmployeeId,string CrmUserId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@BarcodeNumber", BarcodeNumber);
			p.Add("@GpEmployeeId", GpEmployeeId);
			p.Add("@CrmUserId", CrmUserId);
			return db.QueryAsync<T>("custMS_AccountEquipmentsViewNextAssignmentByBarcode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEquipmentSyncAssignmetBetweenInvoiceItem<T>(long? AccountEquipmentID,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountEquipmentID", AccountEquipmentID);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId<T>(string MonitoringStationOSID,int? EquipmentTypeID,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@MonitoringStationOSID", MonitoringStationOSID);
			p.Add("@EquipmentTypeID", EquipmentTypeID);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountGetPurchasedAccounts<T>(string City,string StateAB,string County,DateTime? PurchaseDateStart,DateTime? PurchaseDateEnd)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@City", City);
			p.Add("@StateAB", StateAB);
			p.Add("@County", County);
			p.Add("@PurchaseDateStart", PurchaseDateStart);
			p.Add("@PurchaseDateEnd", PurchaseDateEnd);
			return db.QueryAsync<T>("custMS_AccountGetPurchasedAccounts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountHoldsCreate<T>(long? AccountId,int? Catg2Id,string HoldDescription,string CreatedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@Catg2Id", Catg2Id);
			p.Add("@HoldDescription", HoldDescription);
			p.Add("@CreatedBy", CreatedBy);
			return db.QueryAsync<T>("custMS_AccountHoldsCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountMonitorInformationsByAccountID<T>(long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custMS_AccountMonitorInformationsByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountSalesInformationSaveSalesRepID<T>(long? CustomerMasterFileID,long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerMasterFileID", CustomerMasterFileID);
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custMS_AccountSalesInformationSaveSalesRepID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountSalesInfoViewRead<T>(long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custMS_AccountSalesInfoViewRead", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountSearchAccounts<T>(string FirstName,bool? FirstNameExact,string LastName,bool? LastNameExact,string DevicePhone,string SimProductBarcodeId,DateTime? DateOfBirth,string CSID,string Email,string Street,bool? StreetExact,string City,string ZipCode,string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@FirstNameExact", FirstNameExact);
			p.Add("@LastName", LastName);
			p.Add("@LastNameExact", LastNameExact);
			p.Add("@DevicePhone", DevicePhone);
			p.Add("@SimProductBarcodeId", SimProductBarcodeId);
			p.Add("@DateOfBirth", DateOfBirth);
			p.Add("@CSID", CSID);
			p.Add("@Email", Email);
			p.Add("@Street", Street);
			p.Add("@StreetExact", StreetExact);
			p.Add("@City", City);
			p.Add("@ZipCode", ZipCode);
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custMS_AccountSearchAccounts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountSetupCheckListSetByKey<T>(long? AccountID,string Key)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			p.Add("@Key", Key);
			return db.QueryAsync<T>("custMS_AccountSetupCheckListSetByKey", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountSiteGeneralDispatchByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_AccountSiteGeneralDispatchByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_AccountZoneAssignmentsByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_AccountZoneAssignmentsByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_DispatchAgenciesSaveFromMoniEntity<T>(int? EntityAgenciesID,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EntityAgenciesID", EntityAgenciesID);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_DispatchAgenciesSaveFromMoniEntity", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_DispatchAgencyGetByCityStateZips<T>(string City,string State,string Zip)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@City", City);
			p.Add("@State", State);
			p.Add("@Zip", Zip);
			return db.QueryAsync<T>("custMS_DispatchAgencyGetByCityStateZips", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId<T>(string City,string State,string Zip,int? DispatchAgencyTypeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@City", City);
			p.Add("@State", State);
			p.Add("@Zip", Zip);
			p.Add("@DispatchAgencyTypeId", DispatchAgencyTypeId);
			return db.QueryAsync<T>("custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentAccountZoneTypeEventsViewGet<T>(string EquipmentId,int? EquipmentAccountZoneTypeId,string MonitoringStationOSId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipmentId", EquipmentId);
			p.Add("@EquipmentAccountZoneTypeId", EquipmentAccountZoneTypeId);
			p.Add("@MonitoringStationOSId", MonitoringStationOSId);
			return db.QueryAsync<T>("custMS_EquipmentAccountZoneTypeEventsViewGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentAccountZoneTypesViewGet<T>(string EquipmentID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipmentID", EquipmentID);
			return db.QueryAsync<T>("custMS_EquipmentAccountZoneTypesViewGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentByBarcode<T>(string BarcodeNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BarcodeNumber", BarcodeNumber);
			return db.QueryAsync<T>("custMS_EquipmentByBarcode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentExistings<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_EquipmentExistings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentList<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_EquipmentList", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentLocationGetPanelLocationByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_EquipmentLocationGetPanelLocationByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentLocationsByMSOID<T>(string MonitoringStationOSID,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@MonitoringStationOSID", MonitoringStationOSID);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custMS_EquipmentLocationsByMSOID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_EquipmentMostFrequents<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_EquipmentMostFrequents", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_IndustryAccountFindByCallerId<T>(string CallerId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CallerId", CallerId);
			return db.QueryAsync<T>("custMS_IndustryAccountFindByCallerId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_IndustryAccountFindByCsID<T>(string Csid)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Csid", Csid);
			return db.QueryAsync<T>("custMS_IndustryAccountFindByCsID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_IndustryAccountGenerate<T>(long? AccountId,bool? IsPrimary,string AccountType,string MonitoringStationOSId,string GpEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@IsPrimary", IsPrimary);
			p.Add("@AccountType", AccountType);
			p.Add("@MonitoringStationOSId", MonitoringStationOSId);
			p.Add("@GpEmployeeId", GpEmployeeId);
			return db.QueryAsync<T>("custMS_IndustryAccountGenerate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_IndustryAccountNumbersViewGetByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_IndustryAccountNumbersViewGetByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID<T>(long? AccountId,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_LeadTakeOverViewGetByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_LeadTakeOverViewGetByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsBusRulesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsBusRulesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsBusRulesSave<T>(string ErrorNoID,string TableName,string BusRule,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ErrorNoID", ErrorNoID);
			p.Add("@TableName", TableName);
			p.Add("@BusRule", BusRule);
			p.Add("@IsActive", IsActive);
			p.Add("@IsDeleted", IsDeleted);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@CreatedOn", CreatedOn);
			p.Add("@ModifiedBy", ModifiedBy);
			p.Add("@ModifiedOn", ModifiedOn);
			return db.QueryAsync<T>("custMS_MonitronicsBusRulesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsCellProviderSave<T>(string CellProviderID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CellProviderID", CellProviderID);
			p.Add("@Description", Description);
			p.Add("@IsActive", IsActive);
			p.Add("@IsDeleted", IsDeleted);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@CreatedOn", CreatedOn);
			p.Add("@ModifiedBy", ModifiedBy);
			p.Add("@ModifiedOn", ModifiedOn);
			return db.QueryAsync<T>("custMS_MonitronicsCellProviderSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsCellServicesSave<T>(string OptionID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@OptionID", OptionID);
			p.Add("@Description", Description);
			p.Add("@IsActive", IsActive);
			p.Add("@IsDeleted", IsDeleted);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@CreatedOn", CreatedOn);
			p.Add("@ModifiedBy", ModifiedBy);
			p.Add("@ModifiedOn", ModifiedOn);
			return db.QueryAsync<T>("custMS_MonitronicsCellServicesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsContractTypesSave<T>(string ContractTypeID,string Description,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ContractTypeID", ContractTypeID);
			p.Add("@Description", Description);
			p.Add("@IsActive", IsActive);
			p.Add("@IsDeleted", IsDeleted);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@CreatedOn", CreatedOn);
			p.Add("@ModifiedBy", ModifiedBy);
			p.Add("@ModifiedOn", ModifiedOn);
			return db.QueryAsync<T>("custMS_MonitronicsContractTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgenciesGetByCityStateZips<T>(string City,string State,string Zip)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@City", City);
			p.Add("@State", State);
			p.Add("@Zip", Zip);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgenciesGetByCityStateZips", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId<T>(string City,string State,string Zip,int? DispatchAgencyTypeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@City", City);
			p.Add("@State", State);
			p.Add("@Zip", Zip);
			p.Add("@DispatchAgencyTypeId", DispatchAgencyTypeId);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgenciesGetForZip<T>(string ZipCode)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ZipCode", ZipCode);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgenciesGetForZip", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgenciesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgenciesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgenciesSave<T>(string AgencyNumberID,string AgencyTypeId,string AgencyName,string CityName,string StateId,string ZipCode,string Phone1,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AgencyNumberID", AgencyNumberID);
			p.Add("@AgencyTypeId", AgencyTypeId);
			p.Add("@AgencyName", AgencyName);
			p.Add("@CityName", CityName);
			p.Add("@StateId", StateId);
			p.Add("@ZipCode", ZipCode);
			p.Add("@Phone1", Phone1);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgenciesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgencyTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgencyTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAgencyTypesSave<T>(string AgencyTypeID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AgencyTypeID", AgencyTypeID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAgencyTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAuthoritiesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityAuthoritiesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityAuthoritiesSave<T>(string AuthID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AuthID", AuthID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityAuthoritiesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityBusRulesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityBusRulesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityBusRulesSave<T>(int? ErrorNoID,string TableName,string BusRule,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ErrorNoID", ErrorNoID);
			p.Add("@TableName", TableName);
			p.Add("@BusRule", BusRule);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityBusRulesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellProvidersGetAll<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellProvidersGetAll", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellProvidersNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellProvidersNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellProvidersSave<T>(string CellProviderID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CellProviderID", CellProviderID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellProvidersSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellServicesGet<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellServicesGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellServicesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellServicesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityCellServicesSave<T>(string OptionID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@OptionID", OptionID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityCellServicesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityContactTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityContactTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityContactTypesSave<T>(string ContactTypeID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ContactTypeID", ContactTypeID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityContactTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipEventXRefNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipEventXRefNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipEventXRefSave<T>(string EquipTypeID,string EventId,string SiteKind,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipTypeID", EquipTypeID);
			p.Add("@EventId", EventId);
			p.Add("@SiteKind", SiteKind);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipEventXRefSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipmentLocationsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipmentLocationsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipmentLocationsSave<T>(string EquipLocID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipLocID", EquipLocID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipmentLocationsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipmentTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipmentTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEquipmentTypesSave<T>(string EquipTypeId,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipTypeId", EquipTypeId);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityEquipmentTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEventsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityEventsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityEventsSave<T>(string EventID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EventID", EventID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityEventsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityLanguagesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityLanguagesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityLanguagesSave<T>(string LanguageID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@LanguageID", LanguageID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityLanguagesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityNamePrefixesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityNamePrefixesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityNamePrefixesSave<T>(string Prefix,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Prefix", Prefix);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityNamePrefixesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityNameSuffixesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityNameSuffixesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityNameSuffixesSave<T>(string Suffix,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Suffix", Suffix);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityNameSuffixesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityOosCatsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityOosCatsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityOosCatsSave<T>(string OosCatsID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@OosCatsID", OosCatsID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityOosCatsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityOptionsCellProvGet<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_MonitronicsEntityOptionsCellProvGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityOptionsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityOptionsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityOptionsSave<T>(string OptionID,string UsageId,string Description,string ValidValue,string ValueDescription,string ValueRequired,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@OptionID", OptionID);
			p.Add("@UsageId", UsageId);
			p.Add("@Description", Description);
			p.Add("@ValidValue", ValidValue);
			p.Add("@ValueDescription", ValueDescription);
			p.Add("@ValueRequired", ValueRequired);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityOptionsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPartialBatchesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityPartialBatchesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPartialBatchesSave<T>(int? WsiBatchNoID,string CustServNo,string SiteName,int? ServcoNo,DateTime? MmChangeDate,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@WsiBatchNoID", WsiBatchNoID);
			p.Add("@CustServNo", CustServNo);
			p.Add("@SiteName", SiteName);
			p.Add("@ServcoNo", ServcoNo);
			p.Add("@MmChangeDate", MmChangeDate);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityPartialBatchesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPermitTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityPermitTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPermitTypesSave<T>(string PermitTypeID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PermitTypeID", PermitTypeID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityPermitTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPhoneTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityPhoneTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPhoneTypesSave<T>(string PhoneTypeID,string Description,string Method,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PhoneTypeID", PhoneTypeID);
			p.Add("@Description", Description);
			p.Add("@Method", Method);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityPhoneTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPrefixesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityPrefixesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityPrefixesSave<T>(string CellFlagID,byte? CsNoLength,string CmPurchase,int? ServCoNO,string CellProvider,string SystemTypeId,short? CoNo,string BrandedFlag,string ReceiverPhone,string AlarmNetCityCs,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CellFlagID", CellFlagID);
			p.Add("@CsNoLength", CsNoLength);
			p.Add("@CmPurchase", CmPurchase);
			p.Add("@ServCoNO", ServCoNO);
			p.Add("@CellProvider", CellProvider);
			p.Add("@SystemTypeId", SystemTypeId);
			p.Add("@CoNo", CoNo);
			p.Add("@BrandedFlag", BrandedFlag);
			p.Add("@ReceiverPhone", ReceiverPhone);
			p.Add("@AlarmNetCityCs", AlarmNetCityCs);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityPrefixesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityRelationsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityRelationsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityRelationsSave<T>(string RelationID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RelationID", RelationID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityRelationsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySecGroupsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySecGroupsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySecGroupsSave<T>(string SecurityGroupID,string SecurityLevel,string AllUsers,string AllAccounts,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SecurityGroupID", SecurityGroupID);
			p.Add("@SecurityLevel", SecurityLevel);
			p.Add("@AllUsers", AllUsers);
			p.Add("@AllAccounts", AllAccounts);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySecGroupsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityServiceCompaniesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityServiceCompaniesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityServiceCompaniesSave<T>(string ServCoNumberID,string ServCoName,string ServCoAddress1,string ServCoAddress2,string CityName,string StateId,string ZipCode,string Phone1,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ServCoNumberID", ServCoNumberID);
			p.Add("@ServCoName", ServCoName);
			p.Add("@ServCoAddress1", ServCoAddress1);
			p.Add("@ServCoAddress2", ServCoAddress2);
			p.Add("@CityName", CityName);
			p.Add("@StateId", StateId);
			p.Add("@ZipCode", ZipCode);
			p.Add("@Phone1", Phone1);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityServiceCompaniesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteOptionsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteOptionsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteOptionsSave<T>(string CsNumber,string OptionId,string OptionValue,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CsNumber", CsNumber);
			p.Add("@OptionId", OptionId);
			p.Add("@OptionValue", OptionValue);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteOptionsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteSystemInfoSave<T>(long? IndustryAccountID,string site_name,string sitetype_id,string sitestat_id,string site_addr1,string site_addr2,string city_name,string county_name,string state_id,string zip_code,string phone1,string ext1,string street_no,string street_name,string country_name,int? timezone_no,string timezone_descr,int? servco_no,string install_servco_no,string cspart_no,string subdivision,string cross_street,string codeword1,string codeword2,DateTime? orig_install_date,string lang_id,string cs_no,string systype_id,string sec_systype_id,string panel_phone,string panel_location,string receiver_phone,short? ati_hours,byte? ati_minutes,string panel_code,string twoway_device_id,string alkup_cs_no,string blkup_cs_no,string ontest_flag,DateTime? ontest_expire_date,string oos_flag,DateTime? install_date,string monitor_type,string GpEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@IndustryAccountID", IndustryAccountID);
			p.Add("@site_name", site_name);
			p.Add("@sitetype_id", sitetype_id);
			p.Add("@sitestat_id", sitestat_id);
			p.Add("@site_addr1", site_addr1);
			p.Add("@site_addr2", site_addr2);
			p.Add("@city_name", city_name);
			p.Add("@county_name", county_name);
			p.Add("@state_id", state_id);
			p.Add("@zip_code", zip_code);
			p.Add("@phone1", phone1);
			p.Add("@ext1", ext1);
			p.Add("@street_no", street_no);
			p.Add("@street_name", street_name);
			p.Add("@country_name", country_name);
			p.Add("@timezone_no", timezone_no);
			p.Add("@timezone_descr", timezone_descr);
			p.Add("@servco_no", servco_no);
			p.Add("@install_servco_no", install_servco_no);
			p.Add("@cspart_no", cspart_no);
			p.Add("@subdivision", subdivision);
			p.Add("@cross_street", cross_street);
			p.Add("@codeword1", codeword1);
			p.Add("@codeword2", codeword2);
			p.Add("@orig_install_date", orig_install_date);
			p.Add("@lang_id", lang_id);
			p.Add("@cs_no", cs_no);
			p.Add("@systype_id", systype_id);
			p.Add("@sec_systype_id", sec_systype_id);
			p.Add("@panel_phone", panel_phone);
			p.Add("@panel_location", panel_location);
			p.Add("@receiver_phone", receiver_phone);
			p.Add("@ati_hours", ati_hours);
			p.Add("@ati_minutes", ati_minutes);
			p.Add("@panel_code", panel_code);
			p.Add("@twoway_device_id", twoway_device_id);
			p.Add("@alkup_cs_no", alkup_cs_no);
			p.Add("@blkup_cs_no", blkup_cs_no);
			p.Add("@ontest_flag", ontest_flag);
			p.Add("@ontest_expire_date", ontest_expire_date);
			p.Add("@oos_flag", oos_flag);
			p.Add("@install_date", install_date);
			p.Add("@monitor_type", monitor_type);
			p.Add("@GpEmployeeID", GpEmployeeID);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteSystemInfoSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteSystemOptionsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteSystemOptionsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteSystemOptionsSave<T>(string CsNumberID,string OptionId,string OptionValue,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CsNumberID", CsNumberID);
			p.Add("@OptionId", OptionId);
			p.Add("@OptionValue", OptionValue);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteSystemOptionsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId<T>(string SiteTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SiteTypeID", SiteTypeID);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySiteTypesSave<T>(string SiteTypeID,string Description,string SiteKind,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SiteTypeID", SiteTypeID);
			p.Add("@Description", Description);
			p.Add("@SiteKind", SiteKind);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySiteTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityStatesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityStatesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityStatesSave<T>(string StateID,string StateName,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateID", StateID);
			p.Add("@StateName", StateName);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityStatesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySystemTypesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySystemTypesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySystemTypesSave<T>(string SystemTypeID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SystemTypeID", SystemTypeID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySystemTypesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySystemTypeXRefNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntitySystemTypeXRefNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySystemTypeXRefSave<T>(string DigitalSystemTypeId,string TwoWayDeviceId,string CellSystemTypeId,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DigitalSystemTypeId", DigitalSystemTypeId);
			p.Add("@TwoWayDeviceId", TwoWayDeviceId);
			p.Add("@CellSystemTypeId", CellSystemTypeId);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySystemTypeXRefSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySysTypesCellDeviceGet<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySysTypesCellDeviceGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySysTypesGetByPanelTypeId<T>(string PanelTypeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PanelTypeId", PanelTypeId);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySysTypesGetByPanelTypeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntitySysTypesPanelGet<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_MonitronicsEntitySysTypesPanelGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityTestCatsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityTestCatsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityTestCatsSave<T>(string TestCatID,string Description,short? DefaultHours,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TestCatID", TestCatID);
			p.Add("@Description", Description);
			p.Add("@DefaultHours", DefaultHours);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityTestCatsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityTwoWaysNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityTwoWaysNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityTwoWaysSave<T>(string TwoWayDeviceID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TwoWayDeviceID", TwoWayDeviceID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityTwoWaysSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityZipGetByZipCode<T>(string ZipCode)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ZipCode", ZipCode);
			return db.QueryAsync<T>("custMS_MonitronicsEntityZipGetByZipCode", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityZipsNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityZipsNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityZipsSave<T>(string CityNameID,string CountyName,string StateId,string ZipCode,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CityNameID", CityNameID);
			p.Add("@CountyName", CountyName);
			p.Add("@StateId", StateId);
			p.Add("@ZipCode", ZipCode);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityZipsSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityZoneStatesNuke<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custMS_MonitronicsEntityZoneStatesNuke", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEntityZoneStatesSave<T>(string ZoneStateID,string Description,string ModifiedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ZoneStateID", ZoneStateID);
			p.Add("@Description", Description);
			p.Add("@ModifiedBy", ModifiedBy);
			return db.QueryAsync<T>("custMS_MonitronicsEntityZoneStatesSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsEquipEventXRefSave<T>(string EquipTypeID,string EventId,string SiteKind,bool? IsActive,bool? IsDeleted,string CreatedBy,string CreatedOn,string ModifiedBy,string ModifiedOn)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@EquipTypeID", EquipTypeID);
			p.Add("@EventId", EventId);
			p.Add("@SiteKind", SiteKind);
			p.Add("@IsActive", IsActive);
			p.Add("@IsDeleted", IsDeleted);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@CreatedOn", CreatedOn);
			p.Add("@ModifiedBy", ModifiedBy);
			p.Add("@ModifiedOn", ModifiedOn);
			return db.QueryAsync<T>("custMS_MonitronicsEquipEventXRefSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_MonitronicsTwoWayInitSave<T>(long? IndustryAccountID,DateTime? TwoWayTestStartedOn,DateTime? ConfirmedOn,string ConfirmedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@IndustryAccountID", IndustryAccountID);
			p.Add("@TwoWayTestStartedOn", TwoWayTestStartedOn);
			p.Add("@ConfirmedOn", ConfirmedOn);
			p.Add("@ConfirmedBy", ConfirmedBy);
			return db.QueryAsync<T>("custMS_MonitronicsTwoWayInitSave", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_ReceiverBlockCellDeviceInfoViewGetByAccountID<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_ReceiverLineBlockAccountGetNewByAccountID<T>(long? AccountID,string CreatedBy,string ReceiverLineID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountID", AccountID);
			p.Add("@CreatedBy", CreatedBy);
			p.Add("@ReceiverLineID", ReceiverLineID);
			return db.QueryAsync<T>("custMS_ReceiverLineBlockAccountGetNewByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_TimeZoneLookupGetByStateAB<T>(string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custMS_TimeZoneLookupGetByStateAB", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> MS_VendorAlarmComAccountsGetByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custMS_VendorAlarmComAccountsGetByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_CreditReportMaxScoreByCmfID<T>(long? CustomerMasterFileId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerMasterFileId", CustomerMasterFileId);
			return db.QueryAsync<T>("custQL_CreditReportMaxScoreByCmfID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_CreditReportTransactionAndTokenViewGet<T>(long? CreditReportID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CreditReportID", CreditReportID);
			return db.QueryAsync<T>("custQL_CreditReportTransactionAndTokenViewGet", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadBasicInfoViewCreate<T>(long? AddressId,int? DealerId,string LocalizationId,int? TeamLocationId,int? SeasonId,string SalesRepId,int? LeadSourceId,int? LeadDispositionId,string Salutation,string FirstName,string MiddleName,string LastName,string Suffix,string SSN,DateTime? DOB,string DL,string DLStateID,string Email,string PhoneHome,string PhoneWork,string PhoneMobile,string ProductSkwId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AddressId", AddressId);
			p.Add("@DealerId", DealerId);
			p.Add("@LocalizationId", LocalizationId);
			p.Add("@TeamLocationId", TeamLocationId);
			p.Add("@SeasonId", SeasonId);
			p.Add("@SalesRepId", SalesRepId);
			p.Add("@LeadSourceId", LeadSourceId);
			p.Add("@LeadDispositionId", LeadDispositionId);
			p.Add("@Salutation", Salutation);
			p.Add("@FirstName", FirstName);
			p.Add("@MiddleName", MiddleName);
			p.Add("@LastName", LastName);
			p.Add("@Suffix", Suffix);
			p.Add("@SSN", SSN);
			p.Add("@DOB", DOB);
			p.Add("@DL", DL);
			p.Add("@DLStateID", DLStateID);
			p.Add("@Email", Email);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@PhoneWork", PhoneWork);
			p.Add("@PhoneMobile", PhoneMobile);
			p.Add("@ProductSkwId", ProductSkwId);
			return db.QueryAsync<T>("custQL_LeadBasicInfoViewCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadDispositionGetByDealerId<T>(int? DealerId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerId", DealerId);
			return db.QueryAsync<T>("custQL_LeadDispositionGetByDealerId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadsByAccountId<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custQL_LeadsByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadsCreateBasic<T>(int? DealerId,string LocalizationId,int? TeamLocationId,int? SeasonId,string SalesRepId,int? LeadSourceId,int? LeadDispositionId,string Salutation,string FirstName,string MiddleName,string LastName,string Suffix,string SSN,DateTime? DOB,string DL,string DLStateID,string Email,string PhoneHome,string PhoneWork,string PhoneMobile,string StreetAddress,string City,string StateId,string PostalCode,string PlusFour,string CountryId,string Phone)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerId", DealerId);
			p.Add("@LocalizationId", LocalizationId);
			p.Add("@TeamLocationId", TeamLocationId);
			p.Add("@SeasonId", SeasonId);
			p.Add("@SalesRepId", SalesRepId);
			p.Add("@LeadSourceId", LeadSourceId);
			p.Add("@LeadDispositionId", LeadDispositionId);
			p.Add("@Salutation", Salutation);
			p.Add("@FirstName", FirstName);
			p.Add("@MiddleName", MiddleName);
			p.Add("@LastName", LastName);
			p.Add("@Suffix", Suffix);
			p.Add("@SSN", SSN);
			p.Add("@DOB", DOB);
			p.Add("@DL", DL);
			p.Add("@DLStateID", DLStateID);
			p.Add("@Email", Email);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@PhoneWork", PhoneWork);
			p.Add("@PhoneMobile", PhoneMobile);
			p.Add("@StreetAddress", StreetAddress);
			p.Add("@City", City);
			p.Add("@StateId", StateId);
			p.Add("@PostalCode", PostalCode);
			p.Add("@PlusFour", PlusFour);
			p.Add("@CountryId", CountryId);
			p.Add("@Phone", Phone);
			return db.QueryAsync<T>("custQL_LeadsCreateBasic", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadSearchResultViewSearch<T>(string FirstName,string LastName,string Phone,int? DealerId,string Email,long? LeadId,int? LeadDispositionId,int? LeadSourceId,int? PageSize,int? PageNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@Phone", Phone);
			p.Add("@DealerId", DealerId);
			p.Add("@Email", Email);
			p.Add("@LeadId", LeadId);
			p.Add("@LeadDispositionId", LeadDispositionId);
			p.Add("@LeadSourceId", LeadSourceId);
			p.Add("@PageSize", PageSize);
			p.Add("@PageNumber", PageNumber);
			return db.QueryAsync<T>("custQL_LeadSearchResultViewSearch", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_LeadSourceGetByDealerId<T>(int? DealerId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DealerId", DealerId);
			return db.QueryAsync<T>("custQL_LeadSourceGetByDealerId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_QualifyCustomerInfoViewByAccountID<T>(long? AccountId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			return db.QueryAsync<T>("custQL_QualifyCustomerInfoViewByAccountID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_QualifyCustomerInfoViewByCustomerID<T>(long? CustomerID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CustomerID", CustomerID);
			return db.QueryAsync<T>("custQL_QualifyCustomerInfoViewByCustomerID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> QL_QualifyCustomerInfoViewByLeadID<T>(long? LeadID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@LeadID", LeadID);
			return db.QueryAsync<T>("custQL_QualifyCustomerInfoViewByLeadID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> Report_MyAccounts<T>(int? officeId,string salesRepId,DateTime? startDate,DateTime? endDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@officeId", officeId);
			p.Add("@salesRepId", salesRepId);
			p.Add("@startDate", startDate);
			p.Add("@endDate", endDate);
			return db.QueryAsync<T>("custReport_MyAccounts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SAE_AgingAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custSAE_AgingAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SAE_BillingHistoryAddFromGreatPlains<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custSAE_BillingHistoryAddFromGreatPlains", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SAE_BillingHistoryByCMFID<T>(long? CMFID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			return db.QueryAsync<T>("custSAE_BillingHistoryByCMFID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SAE_BillingInfoSummaryByCMFID<T>(long? CMFID,long? AccountID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@CMFID", CMFID);
			p.Add("@AccountID", AccountID);
			return db.QueryAsync<T>("custSAE_BillingInfoSummaryByCMFID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SAE_CreditReportAbaraGetByRandomNumber<T>(int? RndNumber)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RndNumber", RndNumber);
			return db.QueryAsync<T>("custSAE_CreditReportAbaraGetByRandomNumber", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_ScheduleBlockCreate<T>(string Block,string ZipCode,double? MaxRadius,double? Distance,DateTime? StartTime,DateTime? EndTime,int? AvailableSlots,string TechnicianId,bool? IsTechConfirmed,string Color,bool? IsBlocked)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Block", Block);
			p.Add("@ZipCode", ZipCode);
			p.Add("@MaxRadius", MaxRadius);
			p.Add("@Distance", Distance);
			p.Add("@StartTime", StartTime);
			p.Add("@EndTime", EndTime);
			p.Add("@AvailableSlots", AvailableSlots);
			p.Add("@TechnicianId", TechnicianId);
			p.Add("@IsTechConfirmed", IsTechConfirmed);
			p.Add("@Color", Color);
			p.Add("@IsBlocked", IsBlocked);
			return db.QueryAsync<T>("custSE_ScheduleBlockCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_ScheduleTicketCreate<T>(long? TicketId,long? BlockId,DateTime? AppointmentDate,int? TravelTime)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TicketId", TicketId);
			p.Add("@BlockId", BlockId);
			p.Add("@AppointmentDate", AppointmentDate);
			p.Add("@TravelTime", TravelTime);
			return db.QueryAsync<T>("custSE_ScheduleTicketCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_ScheduleTicketTechUpdate<T>(long? BlockId,string TechnicianId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BlockId", BlockId);
			p.Add("@TechnicianId", TechnicianId);
			return db.QueryAsync<T>("custSE_ScheduleTicketTechUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_TechnicianAvailabilityCreate<T>(string TechnicianId,DateTime? StartDateTime,DateTime? EndDateTime)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TechnicianId", TechnicianId);
			p.Add("@StartDateTime", StartDateTime);
			p.Add("@EndDateTime", EndDateTime);
			return db.QueryAsync<T>("custSE_TechnicianAvailabilityCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_TicketCreate<T>(long? AccountId,long? MonitoringStationNo,int? TicketTypeId,int? StatusCodeId,string MoniConfirmation,string TechnicianId,decimal? TripCharges,string Appointment,string AgentConfirmation,DateTime? ExpirationDate,string Notes)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AccountId", AccountId);
			p.Add("@MonitoringStationNo", MonitoringStationNo);
			p.Add("@TicketTypeId", TicketTypeId);
			p.Add("@StatusCodeId", StatusCodeId);
			p.Add("@MoniConfirmation", MoniConfirmation);
			p.Add("@TechnicianId", TechnicianId);
			p.Add("@TripCharges", TripCharges);
			p.Add("@Appointment", Appointment);
			p.Add("@AgentConfirmation", AgentConfirmation);
			p.Add("@ExpirationDate", ExpirationDate);
			p.Add("@Notes", Notes);
			return db.QueryAsync<T>("custSE_TicketCreate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> SE_TicketReScheduleList<T>(int? HoursPassed)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@HoursPassed", HoursPassed);
			return db.QueryAsync<T>("custSE_TicketReScheduleList", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationGetCurrentApplicationPermissions<T>(int? ApplicationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			return db.QueryAsync<T>("custUI_ApplicationGetCurrentApplicationPermissions", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationsGetApplicationList<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custUI_ApplicationsGetApplicationList", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationsGetByPermission<T>(string PrincipalName,int? PermissionTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PrincipalName", PrincipalName);
			p.Add("@PermissionTypeID", PermissionTypeID);
			return db.QueryAsync<T>("custUI_ApplicationsGetByPermission", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationVersionsCreateVersion<T>(int? ApplicationID,int? DeployedFileID,int? MajorVersionNumber,int? MinorVersionNumber,int? BuildNumber,int? RevisionNumber,string CreatedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			p.Add("@DeployedFileID", DeployedFileID);
			p.Add("@MajorVersionNumber", MajorVersionNumber);
			p.Add("@MinorVersionNumber", MinorVersionNumber);
			p.Add("@BuildNumber", BuildNumber);
			p.Add("@RevisionNumber", RevisionNumber);
			p.Add("@CreatedBy", CreatedBy);
			return db.QueryAsync<T>("custUI_ApplicationVersionsCreateVersion", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationVersionsGetLatestVersionByApplication<T>(int? ApplicationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			return db.QueryAsync<T>("custUI_ApplicationVersionsGetLatestVersionByApplication", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_ApplicationVersionsSetActiveVersion<T>(int? ApplicationVersionID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationVersionID", ApplicationVersionID);
			return db.QueryAsync<T>("custUI_ApplicationVersionsSetActiveVersion", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenuItemDeleteItem<T>(int? MenuItemID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@MenuItemID", MenuItemID);
			return db.QueryAsync<T>("custUI_MenuItemDeleteItem", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenuItemsGetActionApplicationMappingsAD<T>(string UserNameAD,string GroupNamesAD)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserNameAD", UserNameAD);
			p.Add("@GroupNamesAD", GroupNamesAD);
			return db.QueryAsync<T>("custUI_MenuItemsGetActionApplicationMappingsAD", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenuItemsGetCurrentApplicationMenu<T>(int? ApplicationID,bool? IncludeNotVisible)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			p.Add("@IncludeNotVisible", IncludeNotVisible);
			return db.QueryAsync<T>("custUI_MenuItemsGetCurrentApplicationMenu", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenuItemsGetCurrentApplicationMenuAD<T>(int? ApplicationID,string UserNameAD,string GroupNamesAD)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			p.Add("@UserNameAD", UserNameAD);
			p.Add("@GroupNamesAD", GroupNamesAD);
			return db.QueryAsync<T>("custUI_MenuItemsGetCurrentApplicationMenuAD", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenuItemsGetDashboardMenuAD<T>(string UserNameAD,string GroupNamesAD)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserNameAD", UserNameAD);
			p.Add("@GroupNamesAD", GroupNamesAD);
			return db.QueryAsync<T>("custUI_MenuItemsGetDashboardMenuAD", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> UI_MenusCopyCurrentMenu<T>(int? ApplicationID,int? TargetVersionID,string CreatedBy)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ApplicationID", ApplicationID);
			p.Add("@TargetVersionID", TargetVersionID);
			p.Add("@CreatedBy", CreatedBy);
			return db.QueryAsync<T>("custUI_MenusCopyCurrentMenu", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> wiseSP_ExceptionsThrown<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("wiseSP_ExceptionsThrown", p, commandType: System.Data.CommandType.StoredProcedure);
		}
	}
}

