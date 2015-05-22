


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.SosCrm
{
	public partial class SosCrmDataContext
	{
		#region Internal Instance

		private static SosCrmDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static SosCrmDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new SosCrmDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		AE_AccountAddressTypeController _AE_AccountAddressTypes;
		public AE_AccountAddressTypeController AE_AccountAddressTypes
		{
			get
			{
				if (_AE_AccountAddressTypes == null) _AE_AccountAddressTypes = new AE_AccountAddressTypeController();
				return _AE_AccountAddressTypes;
			}
		}

		AE_AgingStepController _AE_AgingSteps;
		public AE_AgingStepController AE_AgingSteps
		{
			get
			{
				if (_AE_AgingSteps == null) _AE_AgingSteps = new AE_AgingStepController();
				return _AE_AgingSteps;
			}
		}

		AE_BankAccountTypeController _AE_BankAccountTypes;
		public AE_BankAccountTypeController AE_BankAccountTypes
		{
			get
			{
				if (_AE_BankAccountTypes == null) _AE_BankAccountTypes = new AE_BankAccountTypeController();
				return _AE_BankAccountTypes;
			}
		}

		AE_BillingCustomerController _AE_BillingCustomers;
		public AE_BillingCustomerController AE_BillingCustomers
		{
			get
			{
				if (_AE_BillingCustomers == null) _AE_BillingCustomers = new AE_BillingCustomerController();
				return _AE_BillingCustomers;
			}
		}

		AE_BillingTypeController _AE_BillingTypes;
		public AE_BillingTypeController AE_BillingTypes
		{
			get
			{
				if (_AE_BillingTypes == null) _AE_BillingTypes = new AE_BillingTypeController();
				return _AE_BillingTypes;
			}
		}

		AE_ContractController _AE_Contracts;
		public AE_ContractController AE_Contracts
		{
			get
			{
				if (_AE_Contracts == null) _AE_Contracts = new AE_ContractController();
				return _AE_Contracts;
			}
		}

		AE_ContractTemplateController _AE_ContractTemplates;
		public AE_ContractTemplateController AE_ContractTemplates
		{
			get
			{
				if (_AE_ContractTemplates == null) _AE_ContractTemplates = new AE_ContractTemplateController();
				return _AE_ContractTemplates;
			}
		}

		AE_CreditCardTypeController _AE_CreditCardTypes;
		public AE_CreditCardTypeController AE_CreditCardTypes
		{
			get
			{
				if (_AE_CreditCardTypes == null) _AE_CreditCardTypes = new AE_CreditCardTypeController();
				return _AE_CreditCardTypes;
			}
		}

		AE_CustomerAccountController _AE_CustomerAccounts;
		public AE_CustomerAccountController AE_CustomerAccounts
		{
			get
			{
				if (_AE_CustomerAccounts == null) _AE_CustomerAccounts = new AE_CustomerAccountController();
				return _AE_CustomerAccounts;
			}
		}

		AE_CustomerAddressController _AE_CustomerAddresses;
		public AE_CustomerAddressController AE_CustomerAddresses
		{
			get
			{
				if (_AE_CustomerAddresses == null) _AE_CustomerAddresses = new AE_CustomerAddressController();
				return _AE_CustomerAddresses;
			}
		}

		AE_CustomerAddressTypeController _AE_CustomerAddressTypes;
		public AE_CustomerAddressTypeController AE_CustomerAddressTypes
		{
			get
			{
				if (_AE_CustomerAddressTypes == null) _AE_CustomerAddressTypes = new AE_CustomerAddressTypeController();
				return _AE_CustomerAddressTypes;
			}
		}

		AE_CustomerGpsClientController _AE_CustomerGpsClients;
		public AE_CustomerGpsClientController AE_CustomerGpsClients
		{
			get
			{
				if (_AE_CustomerGpsClients == null) _AE_CustomerGpsClients = new AE_CustomerGpsClientController();
				return _AE_CustomerGpsClients;
			}
		}

		AE_CustomerMasterFileController _AE_CustomerMasterFiles;
		public AE_CustomerMasterFileController AE_CustomerMasterFiles
		{
			get
			{
				if (_AE_CustomerMasterFiles == null) _AE_CustomerMasterFiles = new AE_CustomerMasterFileController();
				return _AE_CustomerMasterFiles;
			}
		}

		AE_CustomerMasterFileViewHistoryController _AE_CustomerMasterFileViewHistories;
		public AE_CustomerMasterFileViewHistoryController AE_CustomerMasterFileViewHistories
		{
			get
			{
				if (_AE_CustomerMasterFileViewHistories == null) _AE_CustomerMasterFileViewHistories = new AE_CustomerMasterFileViewHistoryController();
				return _AE_CustomerMasterFileViewHistories;
			}
		}

		AE_CustomerMasterToCustomerController _AE_CustomerMasterToCustomers;
		public AE_CustomerMasterToCustomerController AE_CustomerMasterToCustomers
		{
			get
			{
				if (_AE_CustomerMasterToCustomers == null) _AE_CustomerMasterToCustomers = new AE_CustomerMasterToCustomerController();
				return _AE_CustomerMasterToCustomers;
			}
		}

		AE_CustomerController _AE_Customers;
		public AE_CustomerController AE_Customers
		{
			get
			{
				if (_AE_Customers == null) _AE_Customers = new AE_CustomerController();
				return _AE_Customers;
			}
		}

		AE_CustomerSetupStatusController _AE_CustomerSetupStatuses;
		public AE_CustomerSetupStatusController AE_CustomerSetupStatuses
		{
			get
			{
				if (_AE_CustomerSetupStatuses == null) _AE_CustomerSetupStatuses = new AE_CustomerSetupStatusController();
				return _AE_CustomerSetupStatuses;
			}
		}

		AE_CustomerTypeController _AE_CustomerTypes;
		public AE_CustomerTypeController AE_CustomerTypes
		{
			get
			{
				if (_AE_CustomerTypes == null) _AE_CustomerTypes = new AE_CustomerTypeController();
				return _AE_CustomerTypes;
			}
		}

		AE_DealerPurchaseOrderItemController _AE_DealerPurchaseOrderItems;
		public AE_DealerPurchaseOrderItemController AE_DealerPurchaseOrderItems
		{
			get
			{
				if (_AE_DealerPurchaseOrderItems == null) _AE_DealerPurchaseOrderItems = new AE_DealerPurchaseOrderItemController();
				return _AE_DealerPurchaseOrderItems;
			}
		}

		AE_DealerPurchaseOrderController _AE_DealerPurchaseOrders;
		public AE_DealerPurchaseOrderController AE_DealerPurchaseOrders
		{
			get
			{
				if (_AE_DealerPurchaseOrders == null) _AE_DealerPurchaseOrders = new AE_DealerPurchaseOrderController();
				return _AE_DealerPurchaseOrders;
			}
		}

		AE_DealerController _AE_Dealers;
		public AE_DealerController AE_Dealers
		{
			get
			{
				if (_AE_Dealers == null) _AE_Dealers = new AE_DealerController();
				return _AE_Dealers;
			}
		}

		AE_GpsClientToCustomerMasterController _AE_GpsClientToCustomerMasters;
		public AE_GpsClientToCustomerMasterController AE_GpsClientToCustomerMasters
		{
			get
			{
				if (_AE_GpsClientToCustomerMasters == null) _AE_GpsClientToCustomerMasters = new AE_GpsClientToCustomerMasterController();
				return _AE_GpsClientToCustomerMasters;
			}
		}

		AE_InvoiceItemController _AE_InvoiceItems;
		public AE_InvoiceItemController AE_InvoiceItems
		{
			get
			{
				if (_AE_InvoiceItems == null) _AE_InvoiceItems = new AE_InvoiceItemController();
				return _AE_InvoiceItems;
			}
		}

		AE_InvoicePaymentJoinController _AE_InvoicePaymentJoins;
		public AE_InvoicePaymentJoinController AE_InvoicePaymentJoins
		{
			get
			{
				if (_AE_InvoicePaymentJoins == null) _AE_InvoicePaymentJoins = new AE_InvoicePaymentJoinController();
				return _AE_InvoicePaymentJoins;
			}
		}

		AE_InvoiceController _AE_Invoices;
		public AE_InvoiceController AE_Invoices
		{
			get
			{
				if (_AE_Invoices == null) _AE_Invoices = new AE_InvoiceController();
				return _AE_Invoices;
			}
		}

		AE_InvoiceTemplateItemController _AE_InvoiceTemplateItems;
		public AE_InvoiceTemplateItemController AE_InvoiceTemplateItems
		{
			get
			{
				if (_AE_InvoiceTemplateItems == null) _AE_InvoiceTemplateItems = new AE_InvoiceTemplateItemController();
				return _AE_InvoiceTemplateItems;
			}
		}

		AE_InvoiceTemplateController _AE_InvoiceTemplates;
		public AE_InvoiceTemplateController AE_InvoiceTemplates
		{
			get
			{
				if (_AE_InvoiceTemplates == null) _AE_InvoiceTemplates = new AE_InvoiceTemplateController();
				return _AE_InvoiceTemplates;
			}
		}

		AE_InvoiceTypeController _AE_InvoiceTypes;
		public AE_InvoiceTypeController AE_InvoiceTypes
		{
			get
			{
				if (_AE_InvoiceTypes == null) _AE_InvoiceTypes = new AE_InvoiceTypeController();
				return _AE_InvoiceTypes;
			}
		}

		AE_ItemAccountController _AE_ItemAccounts;
		public AE_ItemAccountController AE_ItemAccounts
		{
			get
			{
				if (_AE_ItemAccounts == null) _AE_ItemAccounts = new AE_ItemAccountController();
				return _AE_ItemAccounts;
			}
		}

		AE_ItemCrmOnlyController _AE_ItemCrmOnlies;
		public AE_ItemCrmOnlyController AE_ItemCrmOnlies
		{
			get
			{
				if (_AE_ItemCrmOnlies == null) _AE_ItemCrmOnlies = new AE_ItemCrmOnlyController();
				return _AE_ItemCrmOnlies;
			}
		}

		AE_ItemInterimController _AE_ItemInterims;
		public AE_ItemInterimController AE_ItemInterims
		{
			get
			{
				if (_AE_ItemInterims == null) _AE_ItemInterims = new AE_ItemInterimController();
				return _AE_ItemInterims;
			}
		}

		AE_ItemController _AE_Items;
		public AE_ItemController AE_Items
		{
			get
			{
				if (_AE_Items == null) _AE_Items = new AE_ItemController();
				return _AE_Items;
			}
		}

		AE_ItemTypeController _AE_ItemTypes;
		public AE_ItemTypeController AE_ItemTypes
		{
			get
			{
				if (_AE_ItemTypes == null) _AE_ItemTypes = new AE_ItemTypeController();
				return _AE_ItemTypes;
			}
		}

		AE_ManufacturerController _AE_Manufacturers;
		public AE_ManufacturerController AE_Manufacturers
		{
			get
			{
				if (_AE_Manufacturers == null) _AE_Manufacturers = new AE_ManufacturerController();
				return _AE_Manufacturers;
			}
		}

		AE_PaymentMethodController _AE_PaymentMethods;
		public AE_PaymentMethodController AE_PaymentMethods
		{
			get
			{
				if (_AE_PaymentMethods == null) _AE_PaymentMethods = new AE_PaymentMethodController();
				return _AE_PaymentMethods;
			}
		}

		AE_PaymentController _AE_Payments;
		public AE_PaymentController AE_Payments
		{
			get
			{
				if (_AE_Payments == null) _AE_Payments = new AE_PaymentController();
				return _AE_Payments;
			}
		}

		AE_PaymentTypeController _AE_PaymentTypes;
		public AE_PaymentTypeController AE_PaymentTypes
		{
			get
			{
				if (_AE_PaymentTypes == null) _AE_PaymentTypes = new AE_PaymentTypeController();
				return _AE_PaymentTypes;
			}
		}

		AE_ProductGroupController _AE_ProductGroups;
		public AE_ProductGroupController AE_ProductGroups
		{
			get
			{
				if (_AE_ProductGroups == null) _AE_ProductGroups = new AE_ProductGroupController();
				return _AE_ProductGroups;
			}
		}

		AE_ProductPriceSchemaController _AE_ProductPriceSchemas;
		public AE_ProductPriceSchemaController AE_ProductPriceSchemas
		{
			get
			{
				if (_AE_ProductPriceSchemas == null) _AE_ProductPriceSchemas = new AE_ProductPriceSchemaController();
				return _AE_ProductPriceSchemas;
			}
		}

		AE_ProductController _AE_Products;
		public AE_ProductController AE_Products
		{
			get
			{
				if (_AE_Products == null) _AE_Products = new AE_ProductController();
				return _AE_Products;
			}
		}

		AE_ProductTypeController _AE_ProductTypes;
		public AE_ProductTypeController AE_ProductTypes
		{
			get
			{
				if (_AE_ProductTypes == null) _AE_ProductTypes = new AE_ProductTypeController();
				return _AE_ProductTypes;
			}
		}

		AE_TaxOptionController _AE_TaxOptions;
		public AE_TaxOptionController AE_TaxOptions
		{
			get
			{
				if (_AE_TaxOptions == null) _AE_TaxOptions = new AE_TaxOptionController();
				return _AE_TaxOptions;
			}
		}

		BE_BarcodeController _BE_Barcodes;
		public BE_BarcodeController BE_Barcodes
		{
			get
			{
				if (_BE_Barcodes == null) _BE_Barcodes = new BE_BarcodeController();
				return _BE_Barcodes;
			}
		}

		BE_BarcodeSchemaController _BE_BarcodeSchemas;
		public BE_BarcodeSchemaController BE_BarcodeSchemas
		{
			get
			{
				if (_BE_BarcodeSchemas == null) _BE_BarcodeSchemas = new BE_BarcodeSchemaController();
				return _BE_BarcodeSchemas;
			}
		}

		BE_BundleAccountController _BE_BundleAccounts;
		public BE_BundleAccountController BE_BundleAccounts
		{
			get
			{
				if (_BE_BundleAccounts == null) _BE_BundleAccounts = new BE_BundleAccountController();
				return _BE_BundleAccounts;
			}
		}

		BE_BundleItemController _BE_BundleItems;
		public BE_BundleItemController BE_BundleItems
		{
			get
			{
				if (_BE_BundleItems == null) _BE_BundleItems = new BE_BundleItemController();
				return _BE_BundleItems;
			}
		}

		BE_BundleController _BE_Bundles;
		public BE_BundleController BE_Bundles
		{
			get
			{
				if (_BE_Bundles == null) _BE_Bundles = new BE_BundleController();
				return _BE_Bundles;
			}
		}

		BE_DocTypeColumnController _BE_DocTypeColumns;
		public BE_DocTypeColumnController BE_DocTypeColumns
		{
			get
			{
				if (_BE_DocTypeColumns == null) _BE_DocTypeColumns = new BE_DocTypeColumnController();
				return _BE_DocTypeColumns;
			}
		}

		BE_DocTypeController _BE_DocTypes;
		public BE_DocTypeController BE_DocTypes
		{
			get
			{
				if (_BE_DocTypes == null) _BE_DocTypes = new BE_DocTypeController();
				return _BE_DocTypes;
			}
		}

		BE_PrefixDocumentController _BE_PrefixDocuments;
		public BE_PrefixDocumentController BE_PrefixDocuments
		{
			get
			{
				if (_BE_PrefixDocuments == null) _BE_PrefixDocuments = new BE_PrefixDocumentController();
				return _BE_PrefixDocuments;
			}
		}

		BE_PrefixController _BE_Prefixes;
		public BE_PrefixController BE_Prefixes
		{
			get
			{
				if (_BE_Prefixes == null) _BE_Prefixes = new BE_PrefixController();
				return _BE_Prefixes;
			}
		}

		BE_PrefixPrinterController _BE_PrefixPrinters;
		public BE_PrefixPrinterController BE_PrefixPrinters
		{
			get
			{
				if (_BE_PrefixPrinters == null) _BE_PrefixPrinters = new BE_PrefixPrinterController();
				return _BE_PrefixPrinters;
			}
		}

		BX_BarcodeController _BX_Barcodes;
		public BX_BarcodeController BX_Barcodes
		{
			get
			{
				if (_BX_Barcodes == null) _BX_Barcodes = new BX_BarcodeController();
				return _BX_Barcodes;
			}
		}

		BX_BarcodeTypeController _BX_BarcodeTypes;
		public BX_BarcodeTypeController BX_BarcodeTypes
		{
			get
			{
				if (_BX_BarcodeTypes == null) _BX_BarcodeTypes = new BX_BarcodeTypeController();
				return _BX_BarcodeTypes;
			}
		}

		BX_DocTypeController _BX_DocTypes;
		public BX_DocTypeController BX_DocTypes
		{
			get
			{
				if (_BX_DocTypes == null) _BX_DocTypes = new BX_DocTypeController();
				return _BX_DocTypes;
			}
		}

		BX_DocumentFieldController _BX_DocumentFields;
		public BX_DocumentFieldController BX_DocumentFields
		{
			get
			{
				if (_BX_DocumentFields == null) _BX_DocumentFields = new BX_DocumentFieldController();
				return _BX_DocumentFields;
			}
		}

		BX_PrinterController _BX_Printers;
		public BX_PrinterController BX_Printers
		{
			get
			{
				if (_BX_Printers == null) _BX_Printers = new BX_PrinterController();
				return _BX_Printers;
			}
		}

		CA_AppointmentController _CA_Appointments;
		public CA_AppointmentController CA_Appointments
		{
			get
			{
				if (_CA_Appointments == null) _CA_Appointments = new CA_AppointmentController();
				return _CA_Appointments;
			}
		}

		CA_AppointmentTypeController _CA_AppointmentTypes;
		public CA_AppointmentTypeController CA_AppointmentTypes
		{
			get
			{
				if (_CA_AppointmentTypes == null) _CA_AppointmentTypes = new CA_AppointmentTypeController();
				return _CA_AppointmentTypes;
			}
		}

		CA_ReminderDelyTypeController _CA_ReminderDelyTypes;
		public CA_ReminderDelyTypeController CA_ReminderDelyTypes
		{
			get
			{
				if (_CA_ReminderDelyTypes == null) _CA_ReminderDelyTypes = new CA_ReminderDelyTypeController();
				return _CA_ReminderDelyTypes;
			}
		}

		CA_ReminderMediaTypeController _CA_ReminderMediaTypes;
		public CA_ReminderMediaTypeController CA_ReminderMediaTypes
		{
			get
			{
				if (_CA_ReminderMediaTypes == null) _CA_ReminderMediaTypes = new CA_ReminderMediaTypeController();
				return _CA_ReminderMediaTypes;
			}
		}

		DC_AreaCodeController _DC_AreaCodes;
		public DC_AreaCodeController DC_AreaCodes
		{
			get
			{
				if (_DC_AreaCodes == null) _DC_AreaCodes = new DC_AreaCodeController();
				return _DC_AreaCodes;
			}
		}

		DC_CompanyPhoneNumberController _DC_CompanyPhoneNumbers;
		public DC_CompanyPhoneNumberController DC_CompanyPhoneNumbers
		{
			get
			{
				if (_DC_CompanyPhoneNumbers == null) _DC_CompanyPhoneNumbers = new DC_CompanyPhoneNumberController();
				return _DC_CompanyPhoneNumbers;
			}
		}

		DC_PhoneNumberController _DC_PhoneNumbers;
		public DC_PhoneNumberController DC_PhoneNumbers
		{
			get
			{
				if (_DC_PhoneNumbers == null) _DC_PhoneNumbers = new DC_PhoneNumberController();
				return _DC_PhoneNumbers;
			}
		}

		GS_AccountController _GS_Accounts;
		public GS_AccountController GS_Accounts
		{
			get
			{
				if (_GS_Accounts == null) _GS_Accounts = new GS_AccountController();
				return _GS_Accounts;
			}
		}

		IE_AuditController _IE_Audits;
		public IE_AuditController IE_Audits
		{
			get
			{
				if (_IE_Audits == null) _IE_Audits = new IE_AuditController();
				return _IE_Audits;
			}
		}

		IE_LocationTypeController _IE_LocationTypes;
		public IE_LocationTypeController IE_LocationTypes
		{
			get
			{
				if (_IE_LocationTypes == null) _IE_LocationTypes = new IE_LocationTypeController();
				return _IE_LocationTypes;
			}
		}

		IE_PackingSlipItemController _IE_PackingSlipItems;
		public IE_PackingSlipItemController IE_PackingSlipItems
		{
			get
			{
				if (_IE_PackingSlipItems == null) _IE_PackingSlipItems = new IE_PackingSlipItemController();
				return _IE_PackingSlipItems;
			}
		}

		IE_PackingSlipController _IE_PackingSlips;
		public IE_PackingSlipController IE_PackingSlips
		{
			get
			{
				if (_IE_PackingSlips == null) _IE_PackingSlips = new IE_PackingSlipController();
				return _IE_PackingSlips;
			}
		}

		IE_ProductBarcodeBundleController _IE_ProductBarcodeBundles;
		public IE_ProductBarcodeBundleController IE_ProductBarcodeBundles
		{
			get
			{
				if (_IE_ProductBarcodeBundles == null) _IE_ProductBarcodeBundles = new IE_ProductBarcodeBundleController();
				return _IE_ProductBarcodeBundles;
			}
		}

		IE_ProductBarcodeController _IE_ProductBarcodes;
		public IE_ProductBarcodeController IE_ProductBarcodes
		{
			get
			{
				if (_IE_ProductBarcodes == null) _IE_ProductBarcodes = new IE_ProductBarcodeController();
				return _IE_ProductBarcodes;
			}
		}

		IE_ProductBarcodeTrackingController _IE_ProductBarcodeTrackings;
		public IE_ProductBarcodeTrackingController IE_ProductBarcodeTrackings
		{
			get
			{
				if (_IE_ProductBarcodeTrackings == null) _IE_ProductBarcodeTrackings = new IE_ProductBarcodeTrackingController();
				return _IE_ProductBarcodeTrackings;
			}
		}

		IE_ProductBarcodeTrackingTypeController _IE_ProductBarcodeTrackingTypes;
		public IE_ProductBarcodeTrackingTypeController IE_ProductBarcodeTrackingTypes
		{
			get
			{
				if (_IE_ProductBarcodeTrackingTypes == null) _IE_ProductBarcodeTrackingTypes = new IE_ProductBarcodeTrackingTypeController();
				return _IE_ProductBarcodeTrackingTypes;
			}
		}

		IE_PurchaseOrderItemController _IE_PurchaseOrderItems;
		public IE_PurchaseOrderItemController IE_PurchaseOrderItems
		{
			get
			{
				if (_IE_PurchaseOrderItems == null) _IE_PurchaseOrderItems = new IE_PurchaseOrderItemController();
				return _IE_PurchaseOrderItems;
			}
		}

		IE_PurchaseOrderController _IE_PurchaseOrders;
		public IE_PurchaseOrderController IE_PurchaseOrders
		{
			get
			{
				if (_IE_PurchaseOrders == null) _IE_PurchaseOrders = new IE_PurchaseOrderController();
				return _IE_PurchaseOrders;
			}
		}

		IE_ReturnToManufacturerItemController _IE_ReturnToManufacturerItems;
		public IE_ReturnToManufacturerItemController IE_ReturnToManufacturerItems
		{
			get
			{
				if (_IE_ReturnToManufacturerItems == null) _IE_ReturnToManufacturerItems = new IE_ReturnToManufacturerItemController();
				return _IE_ReturnToManufacturerItems;
			}
		}

		IE_ReturnToManufacturerController _IE_ReturnToManufacturers;
		public IE_ReturnToManufacturerController IE_ReturnToManufacturers
		{
			get
			{
				if (_IE_ReturnToManufacturers == null) _IE_ReturnToManufacturers = new IE_ReturnToManufacturerController();
				return _IE_ReturnToManufacturers;
			}
		}

		IE_StockingLevelController _IE_StockingLevels;
		public IE_StockingLevelController IE_StockingLevels
		{
			get
			{
				if (_IE_StockingLevels == null) _IE_StockingLevels = new IE_StockingLevelController();
				return _IE_StockingLevels;
			}
		}

		IE_VendorController _IE_Vendors;
		public IE_VendorController IE_Vendors
		{
			get
			{
				if (_IE_Vendors == null) _IE_Vendors = new IE_VendorController();
				return _IE_Vendors;
			}
		}

		IE_WarehouseSiteController _IE_WarehouseSites;
		public IE_WarehouseSiteController IE_WarehouseSites
		{
			get
			{
				if (_IE_WarehouseSites == null) _IE_WarehouseSites = new IE_WarehouseSiteController();
				return _IE_WarehouseSites;
			}
		}

		IS_AccountController _IS_Accounts;
		public IS_AccountController IS_Accounts
		{
			get
			{
				if (_IS_Accounts == null) _IS_Accounts = new IS_AccountController();
				return _IS_Accounts;
			}
		}

		LL_AccountController _LL_Accounts;
		public LL_AccountController LL_Accounts
		{
			get
			{
				if (_LL_Accounts == null) _LL_Accounts = new LL_AccountController();
				return _LL_Accounts;
			}
		}

		MC_AccountAddressController _MC_AccountAddresses;
		public MC_AccountAddressController MC_AccountAddresses
		{
			get
			{
				if (_MC_AccountAddresses == null) _MC_AccountAddresses = new MC_AccountAddressController();
				return _MC_AccountAddresses;
			}
		}

		MC_AccountCancelReasonController _MC_AccountCancelReasons;
		public MC_AccountCancelReasonController MC_AccountCancelReasons
		{
			get
			{
				if (_MC_AccountCancelReasons == null) _MC_AccountCancelReasons = new MC_AccountCancelReasonController();
				return _MC_AccountCancelReasons;
			}
		}

		MC_AccountFlagController _MC_AccountFlags;
		public MC_AccountFlagController MC_AccountFlags
		{
			get
			{
				if (_MC_AccountFlags == null) _MC_AccountFlags = new MC_AccountFlagController();
				return _MC_AccountFlags;
			}
		}

		MC_AccountFlagTypeController _MC_AccountFlagTypes;
		public MC_AccountFlagTypeController MC_AccountFlagTypes
		{
			get
			{
				if (_MC_AccountFlagTypes == null) _MC_AccountFlagTypes = new MC_AccountFlagTypeController();
				return _MC_AccountFlagTypes;
			}
		}

		MC_AccountInventoryController _MC_AccountInventories;
		public MC_AccountInventoryController MC_AccountInventories
		{
			get
			{
				if (_MC_AccountInventories == null) _MC_AccountInventories = new MC_AccountInventoryController();
				return _MC_AccountInventories;
			}
		}

		MC_AccountNoteCat1Controller _MC_AccountNoteCat1s;
		public MC_AccountNoteCat1Controller MC_AccountNoteCat1s
		{
			get
			{
				if (_MC_AccountNoteCat1s == null) _MC_AccountNoteCat1s = new MC_AccountNoteCat1Controller();
				return _MC_AccountNoteCat1s;
			}
		}

		MC_AccountNoteCat2Controller _MC_AccountNoteCat2s;
		public MC_AccountNoteCat2Controller MC_AccountNoteCat2s
		{
			get
			{
				if (_MC_AccountNoteCat2s == null) _MC_AccountNoteCat2s = new MC_AccountNoteCat2Controller();
				return _MC_AccountNoteCat2s;
			}
		}

		MC_AccountNoteController _MC_AccountNotes;
		public MC_AccountNoteController MC_AccountNotes
		{
			get
			{
				if (_MC_AccountNotes == null) _MC_AccountNotes = new MC_AccountNoteController();
				return _MC_AccountNotes;
			}
		}

		MC_AccountNoteTypeController _MC_AccountNoteTypes;
		public MC_AccountNoteTypeController MC_AccountNoteTypes
		{
			get
			{
				if (_MC_AccountNoteTypes == null) _MC_AccountNoteTypes = new MC_AccountNoteTypeController();
				return _MC_AccountNoteTypes;
			}
		}

		MC_AccountController _MC_Accounts;
		public MC_AccountController MC_Accounts
		{
			get
			{
				if (_MC_Accounts == null) _MC_Accounts = new MC_AccountController();
				return _MC_Accounts;
			}
		}

		MC_AccountStatusCategoryController _MC_AccountStatusCategories;
		public MC_AccountStatusCategoryController MC_AccountStatusCategories
		{
			get
			{
				if (_MC_AccountStatusCategories == null) _MC_AccountStatusCategories = new MC_AccountStatusCategoryController();
				return _MC_AccountStatusCategories;
			}
		}

		MC_AccountStatusEventController _MC_AccountStatusEvents;
		public MC_AccountStatusEventController MC_AccountStatusEvents
		{
			get
			{
				if (_MC_AccountStatusEvents == null) _MC_AccountStatusEvents = new MC_AccountStatusEventController();
				return _MC_AccountStatusEvents;
			}
		}

		MC_AccountStatusTypeController _MC_AccountStatusTypes;
		public MC_AccountStatusTypeController MC_AccountStatusTypes
		{
			get
			{
				if (_MC_AccountStatusTypes == null) _MC_AccountStatusTypes = new MC_AccountStatusTypeController();
				return _MC_AccountStatusTypes;
			}
		}

		MC_AccountSwungInfoController _MC_AccountSwungInfos;
		public MC_AccountSwungInfoController MC_AccountSwungInfos
		{
			get
			{
				if (_MC_AccountSwungInfos == null) _MC_AccountSwungInfos = new MC_AccountSwungInfoController();
				return _MC_AccountSwungInfos;
			}
		}

		MC_AccountTypeController _MC_AccountTypes;
		public MC_AccountTypeController MC_AccountTypes
		{
			get
			{
				if (_MC_AccountTypes == null) _MC_AccountTypes = new MC_AccountTypeController();
				return _MC_AccountTypes;
			}
		}

		MC_AddressCoordController _MC_AddressCoords;
		public MC_AddressCoordController MC_AddressCoords
		{
			get
			{
				if (_MC_AddressCoords == null) _MC_AddressCoords = new MC_AddressCoordController();
				return _MC_AddressCoords;
			}
		}

		MC_AddressCoordStatusCodeController _MC_AddressCoordStatusCodes;
		public MC_AddressCoordStatusCodeController MC_AddressCoordStatusCodes
		{
			get
			{
				if (_MC_AddressCoordStatusCodes == null) _MC_AddressCoordStatusCodes = new MC_AddressCoordStatusCodeController();
				return _MC_AddressCoordStatusCodes;
			}
		}

		MC_AddressDirectionalTypeController _MC_AddressDirectionalTypes;
		public MC_AddressDirectionalTypeController MC_AddressDirectionalTypes
		{
			get
			{
				if (_MC_AddressDirectionalTypes == null) _MC_AddressDirectionalTypes = new MC_AddressDirectionalTypeController();
				return _MC_AddressDirectionalTypes;
			}
		}

		MC_AddressController _MC_Addresses;
		public MC_AddressController MC_Addresses
		{
			get
			{
				if (_MC_Addresses == null) _MC_Addresses = new MC_AddressController();
				return _MC_Addresses;
			}
		}

		MC_AddressStreetTypeController _MC_AddressStreetTypes;
		public MC_AddressStreetTypeController MC_AddressStreetTypes
		{
			get
			{
				if (_MC_AddressStreetTypes == null) _MC_AddressStreetTypes = new MC_AddressStreetTypeController();
				return _MC_AddressStreetTypes;
			}
		}

		MC_AddressTypeController _MC_AddressTypes;
		public MC_AddressTypeController MC_AddressTypes
		{
			get
			{
				if (_MC_AddressTypes == null) _MC_AddressTypes = new MC_AddressTypeController();
				return _MC_AddressTypes;
			}
		}

		MC_AddressValidationStateController _MC_AddressValidationStates;
		public MC_AddressValidationStateController MC_AddressValidationStates
		{
			get
			{
				if (_MC_AddressValidationStates == null) _MC_AddressValidationStates = new MC_AddressValidationStateController();
				return _MC_AddressValidationStates;
			}
		}

		MC_AddressValidationVendorController _MC_AddressValidationVendors;
		public MC_AddressValidationVendorController MC_AddressValidationVendors
		{
			get
			{
				if (_MC_AddressValidationVendors == null) _MC_AddressValidationVendors = new MC_AddressValidationVendorController();
				return _MC_AddressValidationVendors;
			}
		}

		MC_CorporateUserGroupMappingController _MC_CorporateUserGroupMappings;
		public MC_CorporateUserGroupMappingController MC_CorporateUserGroupMappings
		{
			get
			{
				if (_MC_CorporateUserGroupMappings == null) _MC_CorporateUserGroupMappings = new MC_CorporateUserGroupMappingController();
				return _MC_CorporateUserGroupMappings;
			}
		}

		MC_CorporateUserGroupController _MC_CorporateUserGroups;
		public MC_CorporateUserGroupController MC_CorporateUserGroups
		{
			get
			{
				if (_MC_CorporateUserGroups == null) _MC_CorporateUserGroups = new MC_CorporateUserGroupController();
				return _MC_CorporateUserGroups;
			}
		}

		MC_CorporateUserController _MC_CorporateUsers;
		public MC_CorporateUserController MC_CorporateUsers
		{
			get
			{
				if (_MC_CorporateUsers == null) _MC_CorporateUsers = new MC_CorporateUserController();
				return _MC_CorporateUsers;
			}
		}

		MC_CorporateUserTypeController _MC_CorporateUserTypes;
		public MC_CorporateUserTypeController MC_CorporateUserTypes
		{
			get
			{
				if (_MC_CorporateUserTypes == null) _MC_CorporateUserTypes = new MC_CorporateUserTypeController();
				return _MC_CorporateUserTypes;
			}
		}

		MC_DealerUserController _MC_DealerUsers;
		public MC_DealerUserController MC_DealerUsers
		{
			get
			{
				if (_MC_DealerUsers == null) _MC_DealerUsers = new MC_DealerUserController();
				return _MC_DealerUsers;
			}
		}

		MC_DealerUserTypeController _MC_DealerUserTypes;
		public MC_DealerUserTypeController MC_DealerUserTypes
		{
			get
			{
				if (_MC_DealerUserTypes == null) _MC_DealerUserTypes = new MC_DealerUserTypeController();
				return _MC_DealerUserTypes;
			}
		}

		MC_DepartmentAccountNoteCat1Controller _MC_DepartmentAccountNoteCat1s;
		public MC_DepartmentAccountNoteCat1Controller MC_DepartmentAccountNoteCat1s
		{
			get
			{
				if (_MC_DepartmentAccountNoteCat1s == null) _MC_DepartmentAccountNoteCat1s = new MC_DepartmentAccountNoteCat1Controller();
				return _MC_DepartmentAccountNoteCat1s;
			}
		}

		MC_DepartmentController _MC_Departments;
		public MC_DepartmentController MC_Departments
		{
			get
			{
				if (_MC_Departments == null) _MC_Departments = new MC_DepartmentController();
				return _MC_Departments;
			}
		}

		MC_FriendsAndFamilyTypeController _MC_FriendsAndFamilyTypes;
		public MC_FriendsAndFamilyTypeController MC_FriendsAndFamilyTypes
		{
			get
			{
				if (_MC_FriendsAndFamilyTypes == null) _MC_FriendsAndFamilyTypes = new MC_FriendsAndFamilyTypeController();
				return _MC_FriendsAndFamilyTypes;
			}
		}

		MC_HolidayController _MC_Holidays;
		public MC_HolidayController MC_Holidays
		{
			get
			{
				if (_MC_Holidays == null) _MC_Holidays = new MC_HolidayController();
				return _MC_Holidays;
			}
		}

		MC_LocalizationController _MC_Localizations;
		public MC_LocalizationController MC_Localizations
		{
			get
			{
				if (_MC_Localizations == null) _MC_Localizations = new MC_LocalizationController();
				return _MC_Localizations;
			}
		}

		MC_MarketController _MC_Markets;
		public MC_MarketController MC_Markets
		{
			get
			{
				if (_MC_Markets == null) _MC_Markets = new MC_MarketController();
				return _MC_Markets;
			}
		}

		MC_PaymentBankAccountTypeController _MC_PaymentBankAccountTypes;
		public MC_PaymentBankAccountTypeController MC_PaymentBankAccountTypes
		{
			get
			{
				if (_MC_PaymentBankAccountTypes == null) _MC_PaymentBankAccountTypes = new MC_PaymentBankAccountTypeController();
				return _MC_PaymentBankAccountTypes;
			}
		}

		MC_PaymentCreditCardTypeController _MC_PaymentCreditCardTypes;
		public MC_PaymentCreditCardTypeController MC_PaymentCreditCardTypes
		{
			get
			{
				if (_MC_PaymentCreditCardTypes == null) _MC_PaymentCreditCardTypes = new MC_PaymentCreditCardTypeController();
				return _MC_PaymentCreditCardTypes;
			}
		}

		MC_PoliticalCountryController _MC_PoliticalCountries;
		public MC_PoliticalCountryController MC_PoliticalCountries
		{
			get
			{
				if (_MC_PoliticalCountries == null) _MC_PoliticalCountries = new MC_PoliticalCountryController();
				return _MC_PoliticalCountries;
			}
		}

		MC_PoliticalStateController _MC_PoliticalStates;
		public MC_PoliticalStateController MC_PoliticalStates
		{
			get
			{
				if (_MC_PoliticalStates == null) _MC_PoliticalStates = new MC_PoliticalStateController();
				return _MC_PoliticalStates;
			}
		}

		MC_PoliticalTimeZoneController _MC_PoliticalTimeZones;
		public MC_PoliticalTimeZoneController MC_PoliticalTimeZones
		{
			get
			{
				if (_MC_PoliticalTimeZones == null) _MC_PoliticalTimeZones = new MC_PoliticalTimeZoneController();
				return _MC_PoliticalTimeZones;
			}
		}

		MG_AuthorizeNetConfigController _MG_AuthorizeNetConfigs;
		public MG_AuthorizeNetConfigController MG_AuthorizeNetConfigs
		{
			get
			{
				if (_MG_AuthorizeNetConfigs == null) _MG_AuthorizeNetConfigs = new MG_AuthorizeNetConfigController();
				return _MG_AuthorizeNetConfigs;
			}
		}

		MG_AuthorizeNetTransactionController _MG_AuthorizeNetTransactions;
		public MG_AuthorizeNetTransactionController MG_AuthorizeNetTransactions
		{
			get
			{
				if (_MG_AuthorizeNetTransactions == null) _MG_AuthorizeNetTransactions = new MG_AuthorizeNetTransactionController();
				return _MG_AuthorizeNetTransactions;
			}
		}

		MG_GatewayController _MG_Gateways;
		public MG_GatewayController MG_Gateways
		{
			get
			{
				if (_MG_Gateways == null) _MG_Gateways = new MG_GatewayController();
				return _MG_Gateways;
			}
		}

		MG_TransactionController _MG_Transactions;
		public MG_TransactionController MG_Transactions
		{
			get
			{
				if (_MG_Transactions == null) _MG_Transactions = new MG_TransactionController();
				return _MG_Transactions;
			}
		}

		MS_AccountAGController _MS_AccountAGs;
		public MS_AccountAGController MS_AccountAGs
		{
			get
			{
				if (_MS_AccountAGs == null) _MS_AccountAGs = new MS_AccountAGController();
				return _MS_AccountAGs;
			}
		}

		MS_AccountCellularADCRegisterController _MS_AccountCellularADCRegisters;
		public MS_AccountCellularADCRegisterController MS_AccountCellularADCRegisters
		{
			get
			{
				if (_MS_AccountCellularADCRegisters == null) _MS_AccountCellularADCRegisters = new MS_AccountCellularADCRegisterController();
				return _MS_AccountCellularADCRegisters;
			}
		}

		MS_AccountCellularSubmitController _MS_AccountCellularSubmits;
		public MS_AccountCellularSubmitController MS_AccountCellularSubmits
		{
			get
			{
				if (_MS_AccountCellularSubmits == null) _MS_AccountCellularSubmits = new MS_AccountCellularSubmitController();
				return _MS_AccountCellularSubmits;
			}
		}

		MS_AccountCellularSubmitTypeController _MS_AccountCellularSubmitTypes;
		public MS_AccountCellularSubmitTypeController MS_AccountCellularSubmitTypes
		{
			get
			{
				if (_MS_AccountCellularSubmitTypes == null) _MS_AccountCellularSubmitTypes = new MS_AccountCellularSubmitTypeController();
				return _MS_AccountCellularSubmitTypes;
			}
		}

		MS_AccountCellularSubmitVendorController _MS_AccountCellularSubmitVendors;
		public MS_AccountCellularSubmitVendorController MS_AccountCellularSubmitVendors
		{
			get
			{
				if (_MS_AccountCellularSubmitVendors == null) _MS_AccountCellularSubmitVendors = new MS_AccountCellularSubmitVendorController();
				return _MS_AccountCellularSubmitVendors;
			}
		}

		MS_AccountCellularTypeController _MS_AccountCellularTypes;
		public MS_AccountCellularTypeController MS_AccountCellularTypes
		{
			get
			{
				if (_MS_AccountCellularTypes == null) _MS_AccountCellularTypes = new MS_AccountCellularTypeController();
				return _MS_AccountCellularTypes;
			}
		}

		MS_AccountCustomersOLDController _MS_AccountCustomersOLDs;
		public MS_AccountCustomersOLDController MS_AccountCustomersOLDs
		{
			get
			{
				if (_MS_AccountCustomersOLDs == null) _MS_AccountCustomersOLDs = new MS_AccountCustomersOLDController();
				return _MS_AccountCustomersOLDs;
			}
		}

		MS_AccountCustomerTypeController _MS_AccountCustomerTypes;
		public MS_AccountCustomerTypeController MS_AccountCustomerTypes
		{
			get
			{
				if (_MS_AccountCustomerTypes == null) _MS_AccountCustomerTypes = new MS_AccountCustomerTypeController();
				return _MS_AccountCustomerTypes;
			}
		}

		MS_AccountDispatchAgencyAssignmentController _MS_AccountDispatchAgencyAssignments;
		public MS_AccountDispatchAgencyAssignmentController MS_AccountDispatchAgencyAssignments
		{
			get
			{
				if (_MS_AccountDispatchAgencyAssignments == null) _MS_AccountDispatchAgencyAssignments = new MS_AccountDispatchAgencyAssignmentController();
				return _MS_AccountDispatchAgencyAssignments;
			}
		}

		MS_AccountDslSeizureTypeController _MS_AccountDslSeizureTypes;
		public MS_AccountDslSeizureTypeController MS_AccountDslSeizureTypes
		{
			get
			{
				if (_MS_AccountDslSeizureTypes == null) _MS_AccountDslSeizureTypes = new MS_AccountDslSeizureTypeController();
				return _MS_AccountDslSeizureTypes;
			}
		}

		MS_AccountEquipmentController _MS_AccountEquipments;
		public MS_AccountEquipmentController MS_AccountEquipments
		{
			get
			{
				if (_MS_AccountEquipments == null) _MS_AccountEquipments = new MS_AccountEquipmentController();
				return _MS_AccountEquipments;
			}
		}

		MS_AccountEquipmentUpgradeTypeController _MS_AccountEquipmentUpgradeTypes;
		public MS_AccountEquipmentUpgradeTypeController MS_AccountEquipmentUpgradeTypes
		{
			get
			{
				if (_MS_AccountEquipmentUpgradeTypes == null) _MS_AccountEquipmentUpgradeTypes = new MS_AccountEquipmentUpgradeTypeController();
				return _MS_AccountEquipmentUpgradeTypes;
			}
		}

		MS_AccountEventController _MS_AccountEvents;
		public MS_AccountEventController MS_AccountEvents
		{
			get
			{
				if (_MS_AccountEvents == null) _MS_AccountEvents = new MS_AccountEventController();
				return _MS_AccountEvents;
			}
		}

		MS_AccountHoldCatg1Controller _MS_AccountHoldCatg1s;
		public MS_AccountHoldCatg1Controller MS_AccountHoldCatg1s
		{
			get
			{
				if (_MS_AccountHoldCatg1s == null) _MS_AccountHoldCatg1s = new MS_AccountHoldCatg1Controller();
				return _MS_AccountHoldCatg1s;
			}
		}

		MS_AccountHoldCatg2Controller _MS_AccountHoldCatg2s;
		public MS_AccountHoldCatg2Controller MS_AccountHoldCatg2s
		{
			get
			{
				if (_MS_AccountHoldCatg2s == null) _MS_AccountHoldCatg2s = new MS_AccountHoldCatg2Controller();
				return _MS_AccountHoldCatg2s;
			}
		}

		MS_AccountHoldController _MS_AccountHolds;
		public MS_AccountHoldController MS_AccountHolds
		{
			get
			{
				if (_MS_AccountHolds == null) _MS_AccountHolds = new MS_AccountHoldController();
				return _MS_AccountHolds;
			}
		}

		MS_AccountHoldStockController _MS_AccountHoldStocks;
		public MS_AccountHoldStockController MS_AccountHoldStocks
		{
			get
			{
				if (_MS_AccountHoldStocks == null) _MS_AccountHoldStocks = new MS_AccountHoldStockController();
				return _MS_AccountHoldStocks;
			}
		}

		MS_AccountPackageItemController _MS_AccountPackageItems;
		public MS_AccountPackageItemController MS_AccountPackageItems
		{
			get
			{
				if (_MS_AccountPackageItems == null) _MS_AccountPackageItems = new MS_AccountPackageItemController();
				return _MS_AccountPackageItems;
			}
		}

		MS_AccountPackageItemTypeController _MS_AccountPackageItemTypes;
		public MS_AccountPackageItemTypeController MS_AccountPackageItemTypes
		{
			get
			{
				if (_MS_AccountPackageItemTypes == null) _MS_AccountPackageItemTypes = new MS_AccountPackageItemTypeController();
				return _MS_AccountPackageItemTypes;
			}
		}

		MS_AccountPackageController _MS_AccountPackages;
		public MS_AccountPackageController MS_AccountPackages
		{
			get
			{
				if (_MS_AccountPackages == null) _MS_AccountPackages = new MS_AccountPackageController();
				return _MS_AccountPackages;
			}
		}

		MS_AccountPanelTypePanicZoneController _MS_AccountPanelTypePanicZones;
		public MS_AccountPanelTypePanicZoneController MS_AccountPanelTypePanicZones
		{
			get
			{
				if (_MS_AccountPanelTypePanicZones == null) _MS_AccountPanelTypePanicZones = new MS_AccountPanelTypePanicZoneController();
				return _MS_AccountPanelTypePanicZones;
			}
		}

		MS_AccountPanelTypeController _MS_AccountPanelTypes;
		public MS_AccountPanelTypeController MS_AccountPanelTypes
		{
			get
			{
				if (_MS_AccountPanelTypes == null) _MS_AccountPanelTypes = new MS_AccountPanelTypeController();
				return _MS_AccountPanelTypes;
			}
		}

		MS_AccountPayoutTypeController _MS_AccountPayoutTypes;
		public MS_AccountPayoutTypeController MS_AccountPayoutTypes
		{
			get
			{
				if (_MS_AccountPayoutTypes == null) _MS_AccountPayoutTypes = new MS_AccountPayoutTypeController();
				return _MS_AccountPayoutTypes;
			}
		}

		MS_AccountController _MS_Accounts;
		public MS_AccountController MS_Accounts
		{
			get
			{
				if (_MS_Accounts == null) _MS_Accounts = new MS_AccountController();
				return _MS_Accounts;
			}
		}

		MS_AccountSalesInformationController _MS_AccountSalesInformations;
		public MS_AccountSalesInformationController MS_AccountSalesInformations
		{
			get
			{
				if (_MS_AccountSalesInformations == null) _MS_AccountSalesInformations = new MS_AccountSalesInformationController();
				return _MS_AccountSalesInformations;
			}
		}

		MS_AccountSignalFormatTypeController _MS_AccountSignalFormatTypes;
		public MS_AccountSignalFormatTypeController MS_AccountSignalFormatTypes
		{
			get
			{
				if (_MS_AccountSignalFormatTypes == null) _MS_AccountSignalFormatTypes = new MS_AccountSignalFormatTypeController();
				return _MS_AccountSignalFormatTypes;
			}
		}

		MS_AccountSiteGeneralDispatchController _MS_AccountSiteGeneralDispatches;
		public MS_AccountSiteGeneralDispatchController MS_AccountSiteGeneralDispatches
		{
			get
			{
				if (_MS_AccountSiteGeneralDispatches == null) _MS_AccountSiteGeneralDispatches = new MS_AccountSiteGeneralDispatchController();
				return _MS_AccountSiteGeneralDispatches;
			}
		}

		MS_AccountSiteTypeController _MS_AccountSiteTypes;
		public MS_AccountSiteTypeController MS_AccountSiteTypes
		{
			get
			{
				if (_MS_AccountSiteTypes == null) _MS_AccountSiteTypes = new MS_AccountSiteTypeController();
				return _MS_AccountSiteTypes;
			}
		}

		MS_AccountSubmitAGController _MS_AccountSubmitAGs;
		public MS_AccountSubmitAGController MS_AccountSubmitAGs
		{
			get
			{
				if (_MS_AccountSubmitAGs == null) _MS_AccountSubmitAGs = new MS_AccountSubmitAGController();
				return _MS_AccountSubmitAGs;
			}
		}

		MS_AccountSubmitMController _MS_AccountSubmitMs;
		public MS_AccountSubmitMController MS_AccountSubmitMs
		{
			get
			{
				if (_MS_AccountSubmitMs == null) _MS_AccountSubmitMs = new MS_AccountSubmitMController();
				return _MS_AccountSubmitMs;
			}
		}

		MS_AccountSubmitMsXmlController _MS_AccountSubmitMsXmls;
		public MS_AccountSubmitMsXmlController MS_AccountSubmitMsXmls
		{
			get
			{
				if (_MS_AccountSubmitMsXmls == null) _MS_AccountSubmitMsXmls = new MS_AccountSubmitMsXmlController();
				return _MS_AccountSubmitMsXmls;
			}
		}

		MS_AccountSubmitController _MS_AccountSubmits;
		public MS_AccountSubmitController MS_AccountSubmits
		{
			get
			{
				if (_MS_AccountSubmits == null) _MS_AccountSubmits = new MS_AccountSubmitController();
				return _MS_AccountSubmits;
			}
		}

		MS_AccountSubmitTypeController _MS_AccountSubmitTypes;
		public MS_AccountSubmitTypeController MS_AccountSubmitTypes
		{
			get
			{
				if (_MS_AccountSubmitTypes == null) _MS_AccountSubmitTypes = new MS_AccountSubmitTypeController();
				return _MS_AccountSubmitTypes;
			}
		}

		MS_AccountSwungInfoController _MS_AccountSwungInfos;
		public MS_AccountSwungInfoController MS_AccountSwungInfos
		{
			get
			{
				if (_MS_AccountSwungInfos == null) _MS_AccountSwungInfos = new MS_AccountSwungInfoController();
				return _MS_AccountSwungInfos;
			}
		}

		MS_AccountSystemTypeController _MS_AccountSystemTypes;
		public MS_AccountSystemTypeController MS_AccountSystemTypes
		{
			get
			{
				if (_MS_AccountSystemTypes == null) _MS_AccountSystemTypes = new MS_AccountSystemTypeController();
				return _MS_AccountSystemTypes;
			}
		}

		MS_AccountZoneAssignmentController _MS_AccountZoneAssignments;
		public MS_AccountZoneAssignmentController MS_AccountZoneAssignments
		{
			get
			{
				if (_MS_AccountZoneAssignments == null) _MS_AccountZoneAssignments = new MS_AccountZoneAssignmentController();
				return _MS_AccountZoneAssignments;
			}
		}

		MS_AccountZoneTypeController _MS_AccountZoneTypes;
		public MS_AccountZoneTypeController MS_AccountZoneTypes
		{
			get
			{
				if (_MS_AccountZoneTypes == null) _MS_AccountZoneTypes = new MS_AccountZoneTypeController();
				return _MS_AccountZoneTypes;
			}
		}

		MS_AlarmCompanyController _MS_AlarmCompanies;
		public MS_AlarmCompanyController MS_AlarmCompanies
		{
			get
			{
				if (_MS_AlarmCompanies == null) _MS_AlarmCompanies = new MS_AlarmCompanyController();
				return _MS_AlarmCompanies;
			}
		}

		MS_AvantGuardAccountStateController _MS_AvantGuardAccountStates;
		public MS_AvantGuardAccountStateController MS_AvantGuardAccountStates
		{
			get
			{
				if (_MS_AvantGuardAccountStates == null) _MS_AvantGuardAccountStates = new MS_AvantGuardAccountStateController();
				return _MS_AvantGuardAccountStates;
			}
		}

		MS_AvantGuardEventCodeController _MS_AvantGuardEventCodes;
		public MS_AvantGuardEventCodeController MS_AvantGuardEventCodes
		{
			get
			{
				if (_MS_AvantGuardEventCodes == null) _MS_AvantGuardEventCodes = new MS_AvantGuardEventCodeController();
				return _MS_AvantGuardEventCodes;
			}
		}

		MS_AvantGuardRelationController _MS_AvantGuardRelations;
		public MS_AvantGuardRelationController MS_AvantGuardRelations
		{
			get
			{
				if (_MS_AvantGuardRelations == null) _MS_AvantGuardRelations = new MS_AvantGuardRelationController();
				return _MS_AvantGuardRelations;
			}
		}

		MS_AvantGuardSystemTypeCodeController _MS_AvantGuardSystemTypeCodes;
		public MS_AvantGuardSystemTypeCodeController MS_AvantGuardSystemTypeCodes
		{
			get
			{
				if (_MS_AvantGuardSystemTypeCodes == null) _MS_AvantGuardSystemTypeCodes = new MS_AvantGuardSystemTypeCodeController();
				return _MS_AvantGuardSystemTypeCodes;
			}
		}

		MS_AvantGuardTestCategoryController _MS_AvantGuardTestCategories;
		public MS_AvantGuardTestCategoryController MS_AvantGuardTestCategories
		{
			get
			{
				if (_MS_AvantGuardTestCategories == null) _MS_AvantGuardTestCategories = new MS_AvantGuardTestCategoryController();
				return _MS_AvantGuardTestCategories;
			}
		}

		MS_DealerController _MS_Dealers;
		public MS_DealerController MS_Dealers
		{
			get
			{
				if (_MS_Dealers == null) _MS_Dealers = new MS_DealerController();
				return _MS_Dealers;
			}
		}

		MS_DeviceEventController _MS_DeviceEvents;
		public MS_DeviceEventController MS_DeviceEvents
		{
			get
			{
				if (_MS_DeviceEvents == null) _MS_DeviceEvents = new MS_DeviceEventController();
				return _MS_DeviceEvents;
			}
		}

		MS_DispatchAgencyController _MS_DispatchAgencies;
		public MS_DispatchAgencyController MS_DispatchAgencies
		{
			get
			{
				if (_MS_DispatchAgencies == null) _MS_DispatchAgencies = new MS_DispatchAgencyController();
				return _MS_DispatchAgencies;
			}
		}

		MS_DispatchAgencyCityZipLookupController _MS_DispatchAgencyCityZipLookups;
		public MS_DispatchAgencyCityZipLookupController MS_DispatchAgencyCityZipLookups
		{
			get
			{
				if (_MS_DispatchAgencyCityZipLookups == null) _MS_DispatchAgencyCityZipLookups = new MS_DispatchAgencyCityZipLookupController();
				return _MS_DispatchAgencyCityZipLookups;
			}
		}

		MS_DispatchAgencyCityZipController _MS_DispatchAgencyCityZips;
		public MS_DispatchAgencyCityZipController MS_DispatchAgencyCityZips
		{
			get
			{
				if (_MS_DispatchAgencyCityZips == null) _MS_DispatchAgencyCityZips = new MS_DispatchAgencyCityZipController();
				return _MS_DispatchAgencyCityZips;
			}
		}

		MS_DispatchAgencyTypeController _MS_DispatchAgencyTypes;
		public MS_DispatchAgencyTypeController MS_DispatchAgencyTypes
		{
			get
			{
				if (_MS_DispatchAgencyTypes == null) _MS_DispatchAgencyTypes = new MS_DispatchAgencyTypeController();
				return _MS_DispatchAgencyTypes;
			}
		}

		MS_EmergencyContactAuthorityController _MS_EmergencyContactAuthorities;
		public MS_EmergencyContactAuthorityController MS_EmergencyContactAuthorities
		{
			get
			{
				if (_MS_EmergencyContactAuthorities == null) _MS_EmergencyContactAuthorities = new MS_EmergencyContactAuthorityController();
				return _MS_EmergencyContactAuthorities;
			}
		}

		MS_EmergencyContactPhoneTypeController _MS_EmergencyContactPhoneTypes;
		public MS_EmergencyContactPhoneTypeController MS_EmergencyContactPhoneTypes
		{
			get
			{
				if (_MS_EmergencyContactPhoneTypes == null) _MS_EmergencyContactPhoneTypes = new MS_EmergencyContactPhoneTypeController();
				return _MS_EmergencyContactPhoneTypes;
			}
		}

		MS_EmergencyContactRelationshipController _MS_EmergencyContactRelationships;
		public MS_EmergencyContactRelationshipController MS_EmergencyContactRelationships
		{
			get
			{
				if (_MS_EmergencyContactRelationships == null) _MS_EmergencyContactRelationships = new MS_EmergencyContactRelationshipController();
				return _MS_EmergencyContactRelationships;
			}
		}

		MS_EmergencyContactController _MS_EmergencyContacts;
		public MS_EmergencyContactController MS_EmergencyContacts
		{
			get
			{
				if (_MS_EmergencyContacts == null) _MS_EmergencyContacts = new MS_EmergencyContactController();
				return _MS_EmergencyContacts;
			}
		}

		MS_EmergencyContactTypeController _MS_EmergencyContactTypes;
		public MS_EmergencyContactTypeController MS_EmergencyContactTypes
		{
			get
			{
				if (_MS_EmergencyContactTypes == null) _MS_EmergencyContactTypes = new MS_EmergencyContactTypeController();
				return _MS_EmergencyContactTypes;
			}
		}

		MS_EquipmentAccountZoneTypeEventController _MS_EquipmentAccountZoneTypeEvents;
		public MS_EquipmentAccountZoneTypeEventController MS_EquipmentAccountZoneTypeEvents
		{
			get
			{
				if (_MS_EquipmentAccountZoneTypeEvents == null) _MS_EquipmentAccountZoneTypeEvents = new MS_EquipmentAccountZoneTypeEventController();
				return _MS_EquipmentAccountZoneTypeEvents;
			}
		}

		MS_EquipmentAccountZoneTypeController _MS_EquipmentAccountZoneTypes;
		public MS_EquipmentAccountZoneTypeController MS_EquipmentAccountZoneTypes
		{
			get
			{
				if (_MS_EquipmentAccountZoneTypes == null) _MS_EquipmentAccountZoneTypes = new MS_EquipmentAccountZoneTypeController();
				return _MS_EquipmentAccountZoneTypes;
			}
		}

		MS_EquipmentCellularVendorController _MS_EquipmentCellularVendors;
		public MS_EquipmentCellularVendorController MS_EquipmentCellularVendors
		{
			get
			{
				if (_MS_EquipmentCellularVendors == null) _MS_EquipmentCellularVendors = new MS_EquipmentCellularVendorController();
				return _MS_EquipmentCellularVendors;
			}
		}

		MS_EquipmentExistingController _MS_EquipmentExistings;
		public MS_EquipmentExistingController MS_EquipmentExistings
		{
			get
			{
				if (_MS_EquipmentExistings == null) _MS_EquipmentExistings = new MS_EquipmentExistingController();
				return _MS_EquipmentExistings;
			}
		}

		MS_EquipmentLocationController _MS_EquipmentLocations;
		public MS_EquipmentLocationController MS_EquipmentLocations
		{
			get
			{
				if (_MS_EquipmentLocations == null) _MS_EquipmentLocations = new MS_EquipmentLocationController();
				return _MS_EquipmentLocations;
			}
		}

		MS_EquipmentMonitoredTypeController _MS_EquipmentMonitoredTypes;
		public MS_EquipmentMonitoredTypeController MS_EquipmentMonitoredTypes
		{
			get
			{
				if (_MS_EquipmentMonitoredTypes == null) _MS_EquipmentMonitoredTypes = new MS_EquipmentMonitoredTypeController();
				return _MS_EquipmentMonitoredTypes;
			}
		}

		MS_EquipmentMonitronicsCellProviderController _MS_EquipmentMonitronicsCellProviders;
		public MS_EquipmentMonitronicsCellProviderController MS_EquipmentMonitronicsCellProviders
		{
			get
			{
				if (_MS_EquipmentMonitronicsCellProviders == null) _MS_EquipmentMonitronicsCellProviders = new MS_EquipmentMonitronicsCellProviderController();
				return _MS_EquipmentMonitronicsCellProviders;
			}
		}

		MS_EquipmentMonitronicsCellServiceController _MS_EquipmentMonitronicsCellServices;
		public MS_EquipmentMonitronicsCellServiceController MS_EquipmentMonitronicsCellServices
		{
			get
			{
				if (_MS_EquipmentMonitronicsCellServices == null) _MS_EquipmentMonitronicsCellServices = new MS_EquipmentMonitronicsCellServiceController();
				return _MS_EquipmentMonitronicsCellServices;
			}
		}

		MS_EquipmentMonitronicsDeviceController _MS_EquipmentMonitronicsDevices;
		public MS_EquipmentMonitronicsDeviceController MS_EquipmentMonitronicsDevices
		{
			get
			{
				if (_MS_EquipmentMonitronicsDevices == null) _MS_EquipmentMonitronicsDevices = new MS_EquipmentMonitronicsDeviceController();
				return _MS_EquipmentMonitronicsDevices;
			}
		}

		MS_EquipmentMostFrequentController _MS_EquipmentMostFrequents;
		public MS_EquipmentMostFrequentController MS_EquipmentMostFrequents
		{
			get
			{
				if (_MS_EquipmentMostFrequents == null) _MS_EquipmentMostFrequents = new MS_EquipmentMostFrequentController();
				return _MS_EquipmentMostFrequents;
			}
		}

		MS_EquipmentPanelDefaultZoneController _MS_EquipmentPanelDefaultZones;
		public MS_EquipmentPanelDefaultZoneController MS_EquipmentPanelDefaultZones
		{
			get
			{
				if (_MS_EquipmentPanelDefaultZones == null) _MS_EquipmentPanelDefaultZones = new MS_EquipmentPanelDefaultZoneController();
				return _MS_EquipmentPanelDefaultZones;
			}
		}

		MS_EquipmentPanelTypeController _MS_EquipmentPanelTypes;
		public MS_EquipmentPanelTypeController MS_EquipmentPanelTypes
		{
			get
			{
				if (_MS_EquipmentPanelTypes == null) _MS_EquipmentPanelTypes = new MS_EquipmentPanelTypeController();
				return _MS_EquipmentPanelTypes;
			}
		}

		MS_EquipmentController _MS_Equipments;
		public MS_EquipmentController MS_Equipments
		{
			get
			{
				if (_MS_Equipments == null) _MS_Equipments = new MS_EquipmentController();
				return _MS_Equipments;
			}
		}

		MS_EquipmentTypeEventTypeController _MS_EquipmentTypeEventTypes;
		public MS_EquipmentTypeEventTypeController MS_EquipmentTypeEventTypes
		{
			get
			{
				if (_MS_EquipmentTypeEventTypes == null) _MS_EquipmentTypeEventTypes = new MS_EquipmentTypeEventTypeController();
				return _MS_EquipmentTypeEventTypes;
			}
		}

		MS_EquipmentTypeController _MS_EquipmentTypes;
		public MS_EquipmentTypeController MS_EquipmentTypes
		{
			get
			{
				if (_MS_EquipmentTypes == null) _MS_EquipmentTypes = new MS_EquipmentTypeController();
				return _MS_EquipmentTypes;
			}
		}

		MS_EquipmentTypesZoneEventTypeController _MS_EquipmentTypesZoneEventTypes;
		public MS_EquipmentTypesZoneEventTypeController MS_EquipmentTypesZoneEventTypes
		{
			get
			{
				if (_MS_EquipmentTypesZoneEventTypes == null) _MS_EquipmentTypesZoneEventTypes = new MS_EquipmentTypesZoneEventTypeController();
				return _MS_EquipmentTypesZoneEventTypes;
			}
		}

		MS_IndustryAccountController _MS_IndustryAccounts;
		public MS_IndustryAccountController MS_IndustryAccounts
		{
			get
			{
				if (_MS_IndustryAccounts == null) _MS_IndustryAccounts = new MS_IndustryAccountController();
				return _MS_IndustryAccounts;
			}
		}

		MS_LeadTakeOverController _MS_LeadTakeOvers;
		public MS_LeadTakeOverController MS_LeadTakeOvers
		{
			get
			{
				if (_MS_LeadTakeOvers == null) _MS_LeadTakeOvers = new MS_LeadTakeOverController();
				return _MS_LeadTakeOvers;
			}
		}

		MS_MarketController _MS_Markets;
		public MS_MarketController MS_Markets
		{
			get
			{
				if (_MS_Markets == null) _MS_Markets = new MS_MarketController();
				return _MS_Markets;
			}
		}

		MS_MarketSubConversionController _MS_MarketSubConversions;
		public MS_MarketSubConversionController MS_MarketSubConversions
		{
			get
			{
				if (_MS_MarketSubConversions == null) _MS_MarketSubConversions = new MS_MarketSubConversionController();
				return _MS_MarketSubConversions;
			}
		}

		MS_MonitoringStationOssController _MS_MonitoringStationOsses;
		public MS_MonitoringStationOssController MS_MonitoringStationOsses
		{
			get
			{
				if (_MS_MonitoringStationOsses == null) _MS_MonitoringStationOsses = new MS_MonitoringStationOssController();
				return _MS_MonitoringStationOsses;
			}
		}

		MS_MonitoringStationController _MS_MonitoringStations;
		public MS_MonitoringStationController MS_MonitoringStations
		{
			get
			{
				if (_MS_MonitoringStations == null) _MS_MonitoringStations = new MS_MonitoringStationController();
				return _MS_MonitoringStations;
			}
		}

		MS_MonitronicsDispatchAgencyController _MS_MonitronicsDispatchAgencies;
		public MS_MonitronicsDispatchAgencyController MS_MonitronicsDispatchAgencies
		{
			get
			{
				if (_MS_MonitronicsDispatchAgencies == null) _MS_MonitronicsDispatchAgencies = new MS_MonitronicsDispatchAgencyController();
				return _MS_MonitronicsDispatchAgencies;
			}
		}

		MS_MonitronicsEntityController _MS_MonitronicsEntities;
		public MS_MonitronicsEntityController MS_MonitronicsEntities
		{
			get
			{
				if (_MS_MonitronicsEntities == null) _MS_MonitronicsEntities = new MS_MonitronicsEntityController();
				return _MS_MonitronicsEntities;
			}
		}

		MS_MonitronicsEntityAgencyController _MS_MonitronicsEntityAgencies;
		public MS_MonitronicsEntityAgencyController MS_MonitronicsEntityAgencies
		{
			get
			{
				if (_MS_MonitronicsEntityAgencies == null) _MS_MonitronicsEntityAgencies = new MS_MonitronicsEntityAgencyController();
				return _MS_MonitronicsEntityAgencies;
			}
		}

		MS_MonitronicsEntityAgencyTypeController _MS_MonitronicsEntityAgencyTypes;
		public MS_MonitronicsEntityAgencyTypeController MS_MonitronicsEntityAgencyTypes
		{
			get
			{
				if (_MS_MonitronicsEntityAgencyTypes == null) _MS_MonitronicsEntityAgencyTypes = new MS_MonitronicsEntityAgencyTypeController();
				return _MS_MonitronicsEntityAgencyTypes;
			}
		}

		MS_MonitronicsEntityAuthorityController _MS_MonitronicsEntityAuthorities;
		public MS_MonitronicsEntityAuthorityController MS_MonitronicsEntityAuthorities
		{
			get
			{
				if (_MS_MonitronicsEntityAuthorities == null) _MS_MonitronicsEntityAuthorities = new MS_MonitronicsEntityAuthorityController();
				return _MS_MonitronicsEntityAuthorities;
			}
		}

		MS_MonitronicsEntityBusRuleController _MS_MonitronicsEntityBusRules;
		public MS_MonitronicsEntityBusRuleController MS_MonitronicsEntityBusRules
		{
			get
			{
				if (_MS_MonitronicsEntityBusRules == null) _MS_MonitronicsEntityBusRules = new MS_MonitronicsEntityBusRuleController();
				return _MS_MonitronicsEntityBusRules;
			}
		}

		MS_MonitronicsEntityCellProviderController _MS_MonitronicsEntityCellProviders;
		public MS_MonitronicsEntityCellProviderController MS_MonitronicsEntityCellProviders
		{
			get
			{
				if (_MS_MonitronicsEntityCellProviders == null) _MS_MonitronicsEntityCellProviders = new MS_MonitronicsEntityCellProviderController();
				return _MS_MonitronicsEntityCellProviders;
			}
		}

		MS_MonitronicsEntityCellServiceController _MS_MonitronicsEntityCellServices;
		public MS_MonitronicsEntityCellServiceController MS_MonitronicsEntityCellServices
		{
			get
			{
				if (_MS_MonitronicsEntityCellServices == null) _MS_MonitronicsEntityCellServices = new MS_MonitronicsEntityCellServiceController();
				return _MS_MonitronicsEntityCellServices;
			}
		}

		MS_MonitronicsEntityContactTypeController _MS_MonitronicsEntityContactTypes;
		public MS_MonitronicsEntityContactTypeController MS_MonitronicsEntityContactTypes
		{
			get
			{
				if (_MS_MonitronicsEntityContactTypes == null) _MS_MonitronicsEntityContactTypes = new MS_MonitronicsEntityContactTypeController();
				return _MS_MonitronicsEntityContactTypes;
			}
		}

		MS_MonitronicsEntityContractTypeController _MS_MonitronicsEntityContractTypes;
		public MS_MonitronicsEntityContractTypeController MS_MonitronicsEntityContractTypes
		{
			get
			{
				if (_MS_MonitronicsEntityContractTypes == null) _MS_MonitronicsEntityContractTypes = new MS_MonitronicsEntityContractTypeController();
				return _MS_MonitronicsEntityContractTypes;
			}
		}

		MS_MonitronicsEntityEquipEventXRefController _MS_MonitronicsEntityEquipEventXRefs;
		public MS_MonitronicsEntityEquipEventXRefController MS_MonitronicsEntityEquipEventXRefs
		{
			get
			{
				if (_MS_MonitronicsEntityEquipEventXRefs == null) _MS_MonitronicsEntityEquipEventXRefs = new MS_MonitronicsEntityEquipEventXRefController();
				return _MS_MonitronicsEntityEquipEventXRefs;
			}
		}

		MS_MonitronicsEntityEquipmentLocationController _MS_MonitronicsEntityEquipmentLocations;
		public MS_MonitronicsEntityEquipmentLocationController MS_MonitronicsEntityEquipmentLocations
		{
			get
			{
				if (_MS_MonitronicsEntityEquipmentLocations == null) _MS_MonitronicsEntityEquipmentLocations = new MS_MonitronicsEntityEquipmentLocationController();
				return _MS_MonitronicsEntityEquipmentLocations;
			}
		}

		MS_MonitronicsEntityEquipmentTypeController _MS_MonitronicsEntityEquipmentTypes;
		public MS_MonitronicsEntityEquipmentTypeController MS_MonitronicsEntityEquipmentTypes
		{
			get
			{
				if (_MS_MonitronicsEntityEquipmentTypes == null) _MS_MonitronicsEntityEquipmentTypes = new MS_MonitronicsEntityEquipmentTypeController();
				return _MS_MonitronicsEntityEquipmentTypes;
			}
		}

		MS_MonitronicsEntityEventCodeController _MS_MonitronicsEntityEventCodes;
		public MS_MonitronicsEntityEventCodeController MS_MonitronicsEntityEventCodes
		{
			get
			{
				if (_MS_MonitronicsEntityEventCodes == null) _MS_MonitronicsEntityEventCodes = new MS_MonitronicsEntityEventCodeController();
				return _MS_MonitronicsEntityEventCodes;
			}
		}

		MS_MonitronicsEntityEventHistoryController _MS_MonitronicsEntityEventHistories;
		public MS_MonitronicsEntityEventHistoryController MS_MonitronicsEntityEventHistories
		{
			get
			{
				if (_MS_MonitronicsEntityEventHistories == null) _MS_MonitronicsEntityEventHistories = new MS_MonitronicsEntityEventHistoryController();
				return _MS_MonitronicsEntityEventHistories;
			}
		}

		MS_MonitronicsEntityEventController _MS_MonitronicsEntityEvents;
		public MS_MonitronicsEntityEventController MS_MonitronicsEntityEvents
		{
			get
			{
				if (_MS_MonitronicsEntityEvents == null) _MS_MonitronicsEntityEvents = new MS_MonitronicsEntityEventController();
				return _MS_MonitronicsEntityEvents;
			}
		}

		MS_MonitronicsEntityLanguageController _MS_MonitronicsEntityLanguages;
		public MS_MonitronicsEntityLanguageController MS_MonitronicsEntityLanguages
		{
			get
			{
				if (_MS_MonitronicsEntityLanguages == null) _MS_MonitronicsEntityLanguages = new MS_MonitronicsEntityLanguageController();
				return _MS_MonitronicsEntityLanguages;
			}
		}

		MS_MonitronicsEntityNamePrefixController _MS_MonitronicsEntityNamePrefixes;
		public MS_MonitronicsEntityNamePrefixController MS_MonitronicsEntityNamePrefixes
		{
			get
			{
				if (_MS_MonitronicsEntityNamePrefixes == null) _MS_MonitronicsEntityNamePrefixes = new MS_MonitronicsEntityNamePrefixController();
				return _MS_MonitronicsEntityNamePrefixes;
			}
		}

		MS_MonitronicsEntityNameSuffixController _MS_MonitronicsEntityNameSuffixes;
		public MS_MonitronicsEntityNameSuffixController MS_MonitronicsEntityNameSuffixes
		{
			get
			{
				if (_MS_MonitronicsEntityNameSuffixes == null) _MS_MonitronicsEntityNameSuffixes = new MS_MonitronicsEntityNameSuffixController();
				return _MS_MonitronicsEntityNameSuffixes;
			}
		}

		MS_MonitronicsEntityOosCatController _MS_MonitronicsEntityOosCats;
		public MS_MonitronicsEntityOosCatController MS_MonitronicsEntityOosCats
		{
			get
			{
				if (_MS_MonitronicsEntityOosCats == null) _MS_MonitronicsEntityOosCats = new MS_MonitronicsEntityOosCatController();
				return _MS_MonitronicsEntityOosCats;
			}
		}

		MS_MonitronicsEntityOptionController _MS_MonitronicsEntityOptions;
		public MS_MonitronicsEntityOptionController MS_MonitronicsEntityOptions
		{
			get
			{
				if (_MS_MonitronicsEntityOptions == null) _MS_MonitronicsEntityOptions = new MS_MonitronicsEntityOptionController();
				return _MS_MonitronicsEntityOptions;
			}
		}

		MS_MonitronicsEntityPartialBatchController _MS_MonitronicsEntityPartialBatches;
		public MS_MonitronicsEntityPartialBatchController MS_MonitronicsEntityPartialBatches
		{
			get
			{
				if (_MS_MonitronicsEntityPartialBatches == null) _MS_MonitronicsEntityPartialBatches = new MS_MonitronicsEntityPartialBatchController();
				return _MS_MonitronicsEntityPartialBatches;
			}
		}

		MS_MonitronicsEntityPermitTypeController _MS_MonitronicsEntityPermitTypes;
		public MS_MonitronicsEntityPermitTypeController MS_MonitronicsEntityPermitTypes
		{
			get
			{
				if (_MS_MonitronicsEntityPermitTypes == null) _MS_MonitronicsEntityPermitTypes = new MS_MonitronicsEntityPermitTypeController();
				return _MS_MonitronicsEntityPermitTypes;
			}
		}

		MS_MonitronicsEntityPhoneTypeController _MS_MonitronicsEntityPhoneTypes;
		public MS_MonitronicsEntityPhoneTypeController MS_MonitronicsEntityPhoneTypes
		{
			get
			{
				if (_MS_MonitronicsEntityPhoneTypes == null) _MS_MonitronicsEntityPhoneTypes = new MS_MonitronicsEntityPhoneTypeController();
				return _MS_MonitronicsEntityPhoneTypes;
			}
		}

		MS_MonitronicsEntityPrefixController _MS_MonitronicsEntityPrefixes;
		public MS_MonitronicsEntityPrefixController MS_MonitronicsEntityPrefixes
		{
			get
			{
				if (_MS_MonitronicsEntityPrefixes == null) _MS_MonitronicsEntityPrefixes = new MS_MonitronicsEntityPrefixController();
				return _MS_MonitronicsEntityPrefixes;
			}
		}

		MS_MonitronicsEntityRelationController _MS_MonitronicsEntityRelations;
		public MS_MonitronicsEntityRelationController MS_MonitronicsEntityRelations
		{
			get
			{
				if (_MS_MonitronicsEntityRelations == null) _MS_MonitronicsEntityRelations = new MS_MonitronicsEntityRelationController();
				return _MS_MonitronicsEntityRelations;
			}
		}

		MS_MonitronicsEntitySecGroupController _MS_MonitronicsEntitySecGroups;
		public MS_MonitronicsEntitySecGroupController MS_MonitronicsEntitySecGroups
		{
			get
			{
				if (_MS_MonitronicsEntitySecGroups == null) _MS_MonitronicsEntitySecGroups = new MS_MonitronicsEntitySecGroupController();
				return _MS_MonitronicsEntitySecGroups;
			}
		}

		MS_MonitronicsEntityServiceCompanyController _MS_MonitronicsEntityServiceCompanies;
		public MS_MonitronicsEntityServiceCompanyController MS_MonitronicsEntityServiceCompanies
		{
			get
			{
				if (_MS_MonitronicsEntityServiceCompanies == null) _MS_MonitronicsEntityServiceCompanies = new MS_MonitronicsEntityServiceCompanyController();
				return _MS_MonitronicsEntityServiceCompanies;
			}
		}

		MS_MonitronicsEntitySiteOptionController _MS_MonitronicsEntitySiteOptions;
		public MS_MonitronicsEntitySiteOptionController MS_MonitronicsEntitySiteOptions
		{
			get
			{
				if (_MS_MonitronicsEntitySiteOptions == null) _MS_MonitronicsEntitySiteOptions = new MS_MonitronicsEntitySiteOptionController();
				return _MS_MonitronicsEntitySiteOptions;
			}
		}

		MS_MonitronicsEntitySiteStatController _MS_MonitronicsEntitySiteStats;
		public MS_MonitronicsEntitySiteStatController MS_MonitronicsEntitySiteStats
		{
			get
			{
				if (_MS_MonitronicsEntitySiteStats == null) _MS_MonitronicsEntitySiteStats = new MS_MonitronicsEntitySiteStatController();
				return _MS_MonitronicsEntitySiteStats;
			}
		}

		MS_MonitronicsEntitySiteSystemOptionController _MS_MonitronicsEntitySiteSystemOptions;
		public MS_MonitronicsEntitySiteSystemOptionController MS_MonitronicsEntitySiteSystemOptions
		{
			get
			{
				if (_MS_MonitronicsEntitySiteSystemOptions == null) _MS_MonitronicsEntitySiteSystemOptions = new MS_MonitronicsEntitySiteSystemOptionController();
				return _MS_MonitronicsEntitySiteSystemOptions;
			}
		}

		MS_MonitronicsEntitySiteSystemController _MS_MonitronicsEntitySiteSystems;
		public MS_MonitronicsEntitySiteSystemController MS_MonitronicsEntitySiteSystems
		{
			get
			{
				if (_MS_MonitronicsEntitySiteSystems == null) _MS_MonitronicsEntitySiteSystems = new MS_MonitronicsEntitySiteSystemController();
				return _MS_MonitronicsEntitySiteSystems;
			}
		}

		MS_MonitronicsEntitySiteTypeController _MS_MonitronicsEntitySiteTypes;
		public MS_MonitronicsEntitySiteTypeController MS_MonitronicsEntitySiteTypes
		{
			get
			{
				if (_MS_MonitronicsEntitySiteTypes == null) _MS_MonitronicsEntitySiteTypes = new MS_MonitronicsEntitySiteTypeController();
				return _MS_MonitronicsEntitySiteTypes;
			}
		}

		MS_MonitronicsEntityStateController _MS_MonitronicsEntityStates;
		public MS_MonitronicsEntityStateController MS_MonitronicsEntityStates
		{
			get
			{
				if (_MS_MonitronicsEntityStates == null) _MS_MonitronicsEntityStates = new MS_MonitronicsEntityStateController();
				return _MS_MonitronicsEntityStates;
			}
		}

		MS_MonitronicsEntitySystemTypeController _MS_MonitronicsEntitySystemTypes;
		public MS_MonitronicsEntitySystemTypeController MS_MonitronicsEntitySystemTypes
		{
			get
			{
				if (_MS_MonitronicsEntitySystemTypes == null) _MS_MonitronicsEntitySystemTypes = new MS_MonitronicsEntitySystemTypeController();
				return _MS_MonitronicsEntitySystemTypes;
			}
		}

		MS_MonitronicsEntitySystemTypeXRefController _MS_MonitronicsEntitySystemTypeXRefs;
		public MS_MonitronicsEntitySystemTypeXRefController MS_MonitronicsEntitySystemTypeXRefs
		{
			get
			{
				if (_MS_MonitronicsEntitySystemTypeXRefs == null) _MS_MonitronicsEntitySystemTypeXRefs = new MS_MonitronicsEntitySystemTypeXRefController();
				return _MS_MonitronicsEntitySystemTypeXRefs;
			}
		}

		MS_MonitronicsEntityTestCatController _MS_MonitronicsEntityTestCats;
		public MS_MonitronicsEntityTestCatController MS_MonitronicsEntityTestCats
		{
			get
			{
				if (_MS_MonitronicsEntityTestCats == null) _MS_MonitronicsEntityTestCats = new MS_MonitronicsEntityTestCatController();
				return _MS_MonitronicsEntityTestCats;
			}
		}

		MS_MonitronicsEntityTwoWayController _MS_MonitronicsEntityTwoWays;
		public MS_MonitronicsEntityTwoWayController MS_MonitronicsEntityTwoWays
		{
			get
			{
				if (_MS_MonitronicsEntityTwoWays == null) _MS_MonitronicsEntityTwoWays = new MS_MonitronicsEntityTwoWayController();
				return _MS_MonitronicsEntityTwoWays;
			}
		}

		MS_MonitronicsEntityZipController _MS_MonitronicsEntityZips;
		public MS_MonitronicsEntityZipController MS_MonitronicsEntityZips
		{
			get
			{
				if (_MS_MonitronicsEntityZips == null) _MS_MonitronicsEntityZips = new MS_MonitronicsEntityZipController();
				return _MS_MonitronicsEntityZips;
			}
		}

		MS_MonitronicsEntityZoneStateController _MS_MonitronicsEntityZoneStates;
		public MS_MonitronicsEntityZoneStateController MS_MonitronicsEntityZoneStates
		{
			get
			{
				if (_MS_MonitronicsEntityZoneStates == null) _MS_MonitronicsEntityZoneStates = new MS_MonitronicsEntityZoneStateController();
				return _MS_MonitronicsEntityZoneStates;
			}
		}

		MS_MonitronicsSubmitsGetDataErrorController _MS_MonitronicsSubmitsGetDataErrors;
		public MS_MonitronicsSubmitsGetDataErrorController MS_MonitronicsSubmitsGetDataErrors
		{
			get
			{
				if (_MS_MonitronicsSubmitsGetDataErrors == null) _MS_MonitronicsSubmitsGetDataErrors = new MS_MonitronicsSubmitsGetDataErrorController();
				return _MS_MonitronicsSubmitsGetDataErrors;
			}
		}

		MS_MonitronicsSubmitsGetDataController _MS_MonitronicsSubmitsGetDatas;
		public MS_MonitronicsSubmitsGetDataController MS_MonitronicsSubmitsGetDatas
		{
			get
			{
				if (_MS_MonitronicsSubmitsGetDatas == null) _MS_MonitronicsSubmitsGetDatas = new MS_MonitronicsSubmitsGetDataController();
				return _MS_MonitronicsSubmitsGetDatas;
			}
		}

		MS_ReceiverLineAlarmNetController _MS_ReceiverLineAlarmNets;
		public MS_ReceiverLineAlarmNetController MS_ReceiverLineAlarmNets
		{
			get
			{
				if (_MS_ReceiverLineAlarmNets == null) _MS_ReceiverLineAlarmNets = new MS_ReceiverLineAlarmNetController();
				return _MS_ReceiverLineAlarmNets;
			}
		}

		MS_ReceiverLineBlockAlarmComController _MS_ReceiverLineBlockAlarmComs;
		public MS_ReceiverLineBlockAlarmComController MS_ReceiverLineBlockAlarmComs
		{
			get
			{
				if (_MS_ReceiverLineBlockAlarmComs == null) _MS_ReceiverLineBlockAlarmComs = new MS_ReceiverLineBlockAlarmComController();
				return _MS_ReceiverLineBlockAlarmComs;
			}
		}

		MS_ReceiverLineBlockAlarmComHistoryController _MS_ReceiverLineBlockAlarmComHistories;
		public MS_ReceiverLineBlockAlarmComHistoryController MS_ReceiverLineBlockAlarmComHistories
		{
			get
			{
				if (_MS_ReceiverLineBlockAlarmComHistories == null) _MS_ReceiverLineBlockAlarmComHistories = new MS_ReceiverLineBlockAlarmComHistoryController();
				return _MS_ReceiverLineBlockAlarmComHistories;
			}
		}

		MS_ReceiverLineBlockAlarmnetController _MS_ReceiverLineBlockAlarmnets;
		public MS_ReceiverLineBlockAlarmnetController MS_ReceiverLineBlockAlarmnets
		{
			get
			{
				if (_MS_ReceiverLineBlockAlarmnets == null) _MS_ReceiverLineBlockAlarmnets = new MS_ReceiverLineBlockAlarmnetController();
				return _MS_ReceiverLineBlockAlarmnets;
			}
		}

		MS_ReceiverLineBlockController _MS_ReceiverLineBlocks;
		public MS_ReceiverLineBlockController MS_ReceiverLineBlocks
		{
			get
			{
				if (_MS_ReceiverLineBlocks == null) _MS_ReceiverLineBlocks = new MS_ReceiverLineBlockController();
				return _MS_ReceiverLineBlocks;
			}
		}

		MS_ReceiverLineBlockTelguardController _MS_ReceiverLineBlockTelguards;
		public MS_ReceiverLineBlockTelguardController MS_ReceiverLineBlockTelguards
		{
			get
			{
				if (_MS_ReceiverLineBlockTelguards == null) _MS_ReceiverLineBlockTelguards = new MS_ReceiverLineBlockTelguardController();
				return _MS_ReceiverLineBlockTelguards;
			}
		}

		MS_ReceiverLineController _MS_ReceiverLines;
		public MS_ReceiverLineController MS_ReceiverLines
		{
			get
			{
				if (_MS_ReceiverLines == null) _MS_ReceiverLines = new MS_ReceiverLineController();
				return _MS_ReceiverLines;
			}
		}

		MS_ReceiverLineTypeController _MS_ReceiverLineTypes;
		public MS_ReceiverLineTypeController MS_ReceiverLineTypes
		{
			get
			{
				if (_MS_ReceiverLineTypes == null) _MS_ReceiverLineTypes = new MS_ReceiverLineTypeController();
				return _MS_ReceiverLineTypes;
			}
		}

		MS_ReceiverLineVendorAlarmComAccountsMapController _MS_ReceiverLineVendorAlarmComAccountsMaps;
		public MS_ReceiverLineVendorAlarmComAccountsMapController MS_ReceiverLineVendorAlarmComAccountsMaps
		{
			get
			{
				if (_MS_ReceiverLineVendorAlarmComAccountsMaps == null) _MS_ReceiverLineVendorAlarmComAccountsMaps = new MS_ReceiverLineVendorAlarmComAccountsMapController();
				return _MS_ReceiverLineVendorAlarmComAccountsMaps;
			}
		}

		MS_TimeZoneLookupController _MS_TimeZoneLookups;
		public MS_TimeZoneLookupController MS_TimeZoneLookups
		{
			get
			{
				if (_MS_TimeZoneLookups == null) _MS_TimeZoneLookups = new MS_TimeZoneLookupController();
				return _MS_TimeZoneLookups;
			}
		}

		MS_VendorAlarmComAccountController _MS_VendorAlarmComAccounts;
		public MS_VendorAlarmComAccountController MS_VendorAlarmComAccounts
		{
			get
			{
				if (_MS_VendorAlarmComAccounts == null) _MS_VendorAlarmComAccounts = new MS_VendorAlarmComAccountController();
				return _MS_VendorAlarmComAccounts;
			}
		}

		MS_VendorAlarmComAddOnFeatureController _MS_VendorAlarmComAddOnFeatures;
		public MS_VendorAlarmComAddOnFeatureController MS_VendorAlarmComAddOnFeatures
		{
			get
			{
				if (_MS_VendorAlarmComAddOnFeatures == null) _MS_VendorAlarmComAddOnFeatures = new MS_VendorAlarmComAddOnFeatureController();
				return _MS_VendorAlarmComAddOnFeatures;
			}
		}

		MS_VendorAlarmComCentralStationForwardingOptionController _MS_VendorAlarmComCentralStationForwardingOptions;
		public MS_VendorAlarmComCentralStationForwardingOptionController MS_VendorAlarmComCentralStationForwardingOptions
		{
			get
			{
				if (_MS_VendorAlarmComCentralStationForwardingOptions == null) _MS_VendorAlarmComCentralStationForwardingOptions = new MS_VendorAlarmComCentralStationForwardingOptionController();
				return _MS_VendorAlarmComCentralStationForwardingOptions;
			}
		}

		MS_VendorAlarmComCheckCoverageController _MS_VendorAlarmComCheckCoverages;
		public MS_VendorAlarmComCheckCoverageController MS_VendorAlarmComCheckCoverages
		{
			get
			{
				if (_MS_VendorAlarmComCheckCoverages == null) _MS_VendorAlarmComCheckCoverages = new MS_VendorAlarmComCheckCoverageController();
				return _MS_VendorAlarmComCheckCoverages;
			}
		}

		MS_VendorAlarmComCsEventGroupsToForwardController _MS_VendorAlarmComCsEventGroupsToForwards;
		public MS_VendorAlarmComCsEventGroupsToForwardController MS_VendorAlarmComCsEventGroupsToForwards
		{
			get
			{
				if (_MS_VendorAlarmComCsEventGroupsToForwards == null) _MS_VendorAlarmComCsEventGroupsToForwards = new MS_VendorAlarmComCsEventGroupsToForwardController();
				return _MS_VendorAlarmComCsEventGroupsToForwards;
			}
		}

		MS_VendorAlarmComCustomerNotificationController _MS_VendorAlarmComCustomerNotifications;
		public MS_VendorAlarmComCustomerNotificationController MS_VendorAlarmComCustomerNotifications
		{
			get
			{
				if (_MS_VendorAlarmComCustomerNotifications == null) _MS_VendorAlarmComCustomerNotifications = new MS_VendorAlarmComCustomerNotificationController();
				return _MS_VendorAlarmComCustomerNotifications;
			}
		}

		MS_VendorAlarmComPackageController _MS_VendorAlarmComPackages;
		public MS_VendorAlarmComPackageController MS_VendorAlarmComPackages
		{
			get
			{
				if (_MS_VendorAlarmComPackages == null) _MS_VendorAlarmComPackages = new MS_VendorAlarmComPackageController();
				return _MS_VendorAlarmComPackages;
			}
		}

		MS_VendorAlarmComPanelTypeController _MS_VendorAlarmComPanelTypes;
		public MS_VendorAlarmComPanelTypeController MS_VendorAlarmComPanelTypes
		{
			get
			{
				if (_MS_VendorAlarmComPanelTypes == null) _MS_VendorAlarmComPanelTypes = new MS_VendorAlarmComPanelTypeController();
				return _MS_VendorAlarmComPanelTypes;
			}
		}

		MS_VendorAlarmComPanelVersionController _MS_VendorAlarmComPanelVersions;
		public MS_VendorAlarmComPanelVersionController MS_VendorAlarmComPanelVersions
		{
			get
			{
				if (_MS_VendorAlarmComPanelVersions == null) _MS_VendorAlarmComPanelVersions = new MS_VendorAlarmComPanelVersionController();
				return _MS_VendorAlarmComPanelVersions;
			}
		}

		NM_AccountController _NM_Accounts;
		public NM_AccountController NM_Accounts
		{
			get
			{
				if (_NM_Accounts == null) _NM_Accounts = new NM_AccountController();
				return _NM_Accounts;
			}
		}

		PD_ItemController _PD_Items;
		public PD_ItemController PD_Items
		{
			get
			{
				if (_PD_Items == null) _PD_Items = new PD_ItemController();
				return _PD_Items;
			}
		}

		QL_AddressController _QL_Addresses;
		public QL_AddressController QL_Addresses
		{
			get
			{
				if (_QL_Addresses == null) _QL_Addresses = new QL_AddressController();
				return _QL_Addresses;
			}
		}

		QL_AddressCoordController _QL_AddressCoords;
		public QL_AddressCoordController QL_AddressCoords
		{
			get
			{
				if (_QL_AddressCoords == null) _QL_AddressCoords = new QL_AddressCoordController();
				return _QL_AddressCoords;
			}
		}

		QL_CreditReportBureauController _QL_CreditReportBureaus;
		public QL_CreditReportBureauController QL_CreditReportBureaus
		{
			get
			{
				if (_QL_CreditReportBureaus == null) _QL_CreditReportBureaus = new QL_CreditReportBureauController();
				return _QL_CreditReportBureaus;
			}
		}

		QL_CreditReportController _QL_CreditReports;
		public QL_CreditReportController QL_CreditReports
		{
			get
			{
				if (_QL_CreditReports == null) _QL_CreditReports = new QL_CreditReportController();
				return _QL_CreditReports;
			}
		}

		QL_CreditReportVendorAbaraController _QL_CreditReportVendorAbaras;
		public QL_CreditReportVendorAbaraController QL_CreditReportVendorAbaras
		{
			get
			{
				if (_QL_CreditReportVendorAbaras == null) _QL_CreditReportVendorAbaras = new QL_CreditReportVendorAbaraController();
				return _QL_CreditReportVendorAbaras;
			}
		}

		QL_CreditReportVendorEasyAccessController _QL_CreditReportVendorEasyAccesses;
		public QL_CreditReportVendorEasyAccessController QL_CreditReportVendorEasyAccesses
		{
			get
			{
				if (_QL_CreditReportVendorEasyAccesses == null) _QL_CreditReportVendorEasyAccesses = new QL_CreditReportVendorEasyAccessController();
				return _QL_CreditReportVendorEasyAccesses;
			}
		}

		QL_CreditReportVendorHartSoftwareController _QL_CreditReportVendorHartSoftwares;
		public QL_CreditReportVendorHartSoftwareController QL_CreditReportVendorHartSoftwares
		{
			get
			{
				if (_QL_CreditReportVendorHartSoftwares == null) _QL_CreditReportVendorHartSoftwares = new QL_CreditReportVendorHartSoftwareController();
				return _QL_CreditReportVendorHartSoftwares;
			}
		}

		QL_CreditReportVendorManualController _QL_CreditReportVendorManuals;
		public QL_CreditReportVendorManualController QL_CreditReportVendorManuals
		{
			get
			{
				if (_QL_CreditReportVendorManuals == null) _QL_CreditReportVendorManuals = new QL_CreditReportVendorManualController();
				return _QL_CreditReportVendorManuals;
			}
		}

		QL_CreditReportVendorMicrobiltController _QL_CreditReportVendorMicrobilts;
		public QL_CreditReportVendorMicrobiltController QL_CreditReportVendorMicrobilts
		{
			get
			{
				if (_QL_CreditReportVendorMicrobilts == null) _QL_CreditReportVendorMicrobilts = new QL_CreditReportVendorMicrobiltController();
				return _QL_CreditReportVendorMicrobilts;
			}
		}

		QL_CreditReportVendorController _QL_CreditReportVendors;
		public QL_CreditReportVendorController QL_CreditReportVendors
		{
			get
			{
				if (_QL_CreditReportVendors == null) _QL_CreditReportVendors = new QL_CreditReportVendorController();
				return _QL_CreditReportVendors;
			}
		}

		QL_CreditScoreGroupController _QL_CreditScoreGroups;
		public QL_CreditScoreGroupController QL_CreditScoreGroups
		{
			get
			{
				if (_QL_CreditScoreGroups == null) _QL_CreditScoreGroups = new QL_CreditScoreGroupController();
				return _QL_CreditScoreGroups;
			}
		}

		QL_CreditScoreGroupsByDealersAndSeasonController _QL_CreditScoreGroupsByDealersAndSeasons;
		public QL_CreditScoreGroupsByDealersAndSeasonController QL_CreditScoreGroupsByDealersAndSeasons
		{
			get
			{
				if (_QL_CreditScoreGroupsByDealersAndSeasons == null) _QL_CreditScoreGroupsByDealersAndSeasons = new QL_CreditScoreGroupsByDealersAndSeasonController();
				return _QL_CreditScoreGroupsByDealersAndSeasons;
			}
		}

		QL_CustomerMasterLeadController _QL_CustomerMasterLeads;
		public QL_CustomerMasterLeadController QL_CustomerMasterLeads
		{
			get
			{
				if (_QL_CustomerMasterLeads == null) _QL_CustomerMasterLeads = new QL_CustomerMasterLeadController();
				return _QL_CustomerMasterLeads;
			}
		}

		QL_DealerLeadController _QL_DealerLeads;
		public QL_DealerLeadController QL_DealerLeads
		{
			get
			{
				if (_QL_DealerLeads == null) _QL_DealerLeads = new QL_DealerLeadController();
				return _QL_DealerLeads;
			}
		}

		QL_LeadAddressController _QL_LeadAddresses;
		public QL_LeadAddressController QL_LeadAddresses
		{
			get
			{
				if (_QL_LeadAddresses == null) _QL_LeadAddresses = new QL_LeadAddressController();
				return _QL_LeadAddresses;
			}
		}

		QL_LeadDispositionController _QL_LeadDispositions;
		public QL_LeadDispositionController QL_LeadDispositions
		{
			get
			{
				if (_QL_LeadDispositions == null) _QL_LeadDispositions = new QL_LeadDispositionController();
				return _QL_LeadDispositions;
			}
		}

		QL_LeadProductOfferController _QL_LeadProductOffers;
		public QL_LeadProductOfferController QL_LeadProductOffers
		{
			get
			{
				if (_QL_LeadProductOffers == null) _QL_LeadProductOffers = new QL_LeadProductOfferController();
				return _QL_LeadProductOffers;
			}
		}

		QL_LeadController _QL_Leads;
		public QL_LeadController QL_Leads
		{
			get
			{
				if (_QL_Leads == null) _QL_Leads = new QL_LeadController();
				return _QL_Leads;
			}
		}

		QL_LeadSourceController _QL_LeadSources;
		public QL_LeadSourceController QL_LeadSources
		{
			get
			{
				if (_QL_LeadSources == null) _QL_LeadSources = new QL_LeadSourceController();
				return _QL_LeadSources;
			}
		}

		SAE_AgingController _SAE_Agings;
		public SAE_AgingController SAE_Agings
		{
			get
			{
				if (_SAE_Agings == null) _SAE_Agings = new SAE_AgingController();
				return _SAE_Agings;
			}
		}

		SAE_BillingHistoryController _SAE_BillingHistories;
		public SAE_BillingHistoryController SAE_BillingHistories
		{
			get
			{
				if (_SAE_BillingHistories == null) _SAE_BillingHistories = new SAE_BillingHistoryController();
				return _SAE_BillingHistories;
			}
		}

		SAE_BillingInfoSummaryController _SAE_BillingInfoSummaries;
		public SAE_BillingInfoSummaryController SAE_BillingInfoSummaries
		{
			get
			{
				if (_SAE_BillingInfoSummaries == null) _SAE_BillingInfoSummaries = new SAE_BillingInfoSummaryController();
				return _SAE_BillingInfoSummaries;
			}
		}

		SAE_CreditReportAbaraController _SAE_CreditReportAbaras;
		public SAE_CreditReportAbaraController SAE_CreditReportAbaras
		{
			get
			{
				if (_SAE_CreditReportAbaras == null) _SAE_CreditReportAbaras = new SAE_CreditReportAbaraController();
				return _SAE_CreditReportAbaras;
			}
		}

		SAE_CreditRportController _SAE_CreditRports;
		public SAE_CreditRportController SAE_CreditRports
		{
			get
			{
				if (_SAE_CreditRports == null) _SAE_CreditRports = new SAE_CreditRportController();
				return _SAE_CreditRports;
			}
		}

		SAE_InterimPanelTypeMapController _SAE_InterimPanelTypeMaps;
		public SAE_InterimPanelTypeMapController SAE_InterimPanelTypeMaps
		{
			get
			{
				if (_SAE_InterimPanelTypeMaps == null) _SAE_InterimPanelTypeMaps = new SAE_InterimPanelTypeMapController();
				return _SAE_InterimPanelTypeMaps;
			}
		}

		SAE_TestNumberController _SAE_TestNumbers;
		public SAE_TestNumberController SAE_TestNumbers
		{
			get
			{
				if (_SAE_TestNumbers == null) _SAE_TestNumbers = new SAE_TestNumberController();
				return _SAE_TestNumbers;
			}
		}

		SP_AccountController _SP_Accounts;
		public SP_AccountController SP_Accounts
		{
			get
			{
				if (_SP_Accounts == null) _SP_Accounts = new SP_AccountController();
				return _SP_Accounts;
			}
		}

		TS_AppointmentController _TS_Appointments;
		public TS_AppointmentController TS_Appointments
		{
			get
			{
				if (_TS_Appointments == null) _TS_Appointments = new TS_AppointmentController();
				return _TS_Appointments;
			}
		}

		TS_ServiceTicketController _TS_ServiceTickets;
		public TS_ServiceTicketController TS_ServiceTickets
		{
			get
			{
				if (_TS_ServiceTickets == null) _TS_ServiceTickets = new TS_ServiceTicketController();
				return _TS_ServiceTickets;
			}
		}

		TS_ServiceTicketSkills_MapController _TS_ServiceTicketSkills_Maps;
		public TS_ServiceTicketSkills_MapController TS_ServiceTicketSkills_Maps
		{
			get
			{
				if (_TS_ServiceTicketSkills_Maps == null) _TS_ServiceTicketSkills_Maps = new TS_ServiceTicketSkills_MapController();
				return _TS_ServiceTicketSkills_Maps;
			}
		}

		TS_ServiceTypeController _TS_ServiceTypes;
		public TS_ServiceTypeController TS_ServiceTypes
		{
			get
			{
				if (_TS_ServiceTypes == null) _TS_ServiceTypes = new TS_ServiceTypeController();
				return _TS_ServiceTypes;
			}
		}

		TS_SkillController _TS_Skills;
		public TS_SkillController TS_Skills
		{
			get
			{
				if (_TS_Skills == null) _TS_Skills = new TS_SkillController();
				return _TS_Skills;
			}
		}

		TS_StatusCodeController _TS_StatusCodes;
		public TS_StatusCodeController TS_StatusCodes
		{
			get
			{
				if (_TS_StatusCodes == null) _TS_StatusCodes = new TS_StatusCodeController();
				return _TS_StatusCodes;
			}
		}

		TS_TechController _TS_Teches;
		public TS_TechController TS_Teches
		{
			get
			{
				if (_TS_Teches == null) _TS_Teches = new TS_TechController();
				return _TS_Teches;
			}
		}

		TS_TechSkills_MapController _TS_TechSkills_Maps;
		public TS_TechSkills_MapController TS_TechSkills_Maps
		{
			get
			{
				if (_TS_TechSkills_Maps == null) _TS_TechSkills_Maps = new TS_TechSkills_MapController();
				return _TS_TechSkills_Maps;
			}
		}

		TS_TechWeekDayController _TS_TechWeekDays;
		public TS_TechWeekDayController TS_TechWeekDays
		{
			get
			{
				if (_TS_TechWeekDays == null) _TS_TechWeekDays = new TS_TechWeekDayController();
				return _TS_TechWeekDays;
			}
		}

		UI_ActionController _UI_Actions;
		public UI_ActionController UI_Actions
		{
			get
			{
				if (_UI_Actions == null) _UI_Actions = new UI_ActionController();
				return _UI_Actions;
			}
		}

		UI_ApplicationPermissionController _UI_ApplicationPermissions;
		public UI_ApplicationPermissionController UI_ApplicationPermissions
		{
			get
			{
				if (_UI_ApplicationPermissions == null) _UI_ApplicationPermissions = new UI_ApplicationPermissionController();
				return _UI_ApplicationPermissions;
			}
		}

		UI_ApplicationController _UI_Applications;
		public UI_ApplicationController UI_Applications
		{
			get
			{
				if (_UI_Applications == null) _UI_Applications = new UI_ApplicationController();
				return _UI_Applications;
			}
		}

		UI_ApplicationVersionController _UI_ApplicationVersions;
		public UI_ApplicationVersionController UI_ApplicationVersions
		{
			get
			{
				if (_UI_ApplicationVersions == null) _UI_ApplicationVersions = new UI_ApplicationVersionController();
				return _UI_ApplicationVersions;
			}
		}

		UI_MenuItemPermissionController _UI_MenuItemPermissions;
		public UI_MenuItemPermissionController UI_MenuItemPermissions
		{
			get
			{
				if (_UI_MenuItemPermissions == null) _UI_MenuItemPermissions = new UI_MenuItemPermissionController();
				return _UI_MenuItemPermissions;
			}
		}

		UI_MenuItemController _UI_MenuItems;
		public UI_MenuItemController UI_MenuItems
		{
			get
			{
				if (_UI_MenuItems == null) _UI_MenuItems = new UI_MenuItemController();
				return _UI_MenuItems;
			}
		}

		UI_MenuController _UI_Menus;
		public UI_MenuController UI_Menus
		{
			get
			{
				if (_UI_Menus == null) _UI_Menus = new UI_MenuController();
				return _UI_Menus;
			}
		}

		UI_MessageActionParameterController _UI_MessageActionParameters;
		public UI_MessageActionParameterController UI_MessageActionParameters
		{
			get
			{
				if (_UI_MessageActionParameters == null) _UI_MessageActionParameters = new UI_MessageActionParameterController();
				return _UI_MessageActionParameters;
			}
		}

		UI_MessageActionController _UI_MessageActions;
		public UI_MessageActionController UI_MessageActions
		{
			get
			{
				if (_UI_MessageActions == null) _UI_MessageActions = new UI_MessageActionController();
				return _UI_MessageActions;
			}
		}

		UI_MessageController _UI_Messages;
		public UI_MessageController UI_Messages
		{
			get
			{
				if (_UI_Messages == null) _UI_Messages = new UI_MessageController();
				return _UI_Messages;
			}
		}

		UI_PermissionTypeController _UI_PermissionTypes;
		public UI_PermissionTypeController UI_PermissionTypes
		{
			get
			{
				if (_UI_PermissionTypes == null) _UI_PermissionTypes = new UI_PermissionTypeController();
				return _UI_PermissionTypes;
			}
		}

		UI_UserSettingsContainerController _UI_UserSettingsContainers;
		public UI_UserSettingsContainerController UI_UserSettingsContainers
		{
			get
			{
				if (_UI_UserSettingsContainers == null) _UI_UserSettingsContainers = new UI_UserSettingsContainerController();
				return _UI_UserSettingsContainers;
			}
		}

		WF_AccountController _WF_Accounts;
		public WF_AccountController WF_Accounts
		{
			get
			{
				if (_WF_Accounts == null) _WF_Accounts = new WF_AccountController();
				return _WF_Accounts;
			}
		}

		ZZTestCreateController _ZZTestCreates;
		public ZZTestCreateController ZZTestCreates
		{
			get
			{
				if (_ZZTestCreates == null) _ZZTestCreates = new ZZTestCreateController();
				return _ZZTestCreates;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		AE_AgingViewController _AE_AgingViews;
		public AE_AgingViewController AE_AgingViews
		{
			get
			{
				if (_AE_AgingViews == null) _AE_AgingViews = new AE_AgingViewController();
				return _AE_AgingViews;
			}
		}

		AE_CustomerAccountInfoToGPViewController _AE_CustomerAccountInfoToGPViews;
		public AE_CustomerAccountInfoToGPViewController AE_CustomerAccountInfoToGPViews
		{
			get
			{
				if (_AE_CustomerAccountInfoToGPViews == null) _AE_CustomerAccountInfoToGPViews = new AE_CustomerAccountInfoToGPViewController();
				return _AE_CustomerAccountInfoToGPViews;
			}
		}

		AE_CustomerGpsClientsViewController _AE_CustomerGpsClientsViews;
		public AE_CustomerGpsClientsViewController AE_CustomerGpsClientsViews
		{
			get
			{
				if (_AE_CustomerGpsClientsViews == null) _AE_CustomerGpsClientsViews = new AE_CustomerGpsClientsViewController();
				return _AE_CustomerGpsClientsViews;
			}
		}

		AE_CustomerInformationViewController _AE_CustomerInformationViews;
		public AE_CustomerInformationViewController AE_CustomerInformationViews
		{
			get
			{
				if (_AE_CustomerInformationViews == null) _AE_CustomerInformationViews = new AE_CustomerInformationViewController();
				return _AE_CustomerInformationViews;
			}
		}

		AE_CustomerMasterFileGeneralViewController _AE_CustomerMasterFileGeneralViews;
		public AE_CustomerMasterFileGeneralViewController AE_CustomerMasterFileGeneralViews
		{
			get
			{
				if (_AE_CustomerMasterFileGeneralViews == null) _AE_CustomerMasterFileGeneralViews = new AE_CustomerMasterFileGeneralViewController();
				return _AE_CustomerMasterFileGeneralViews;
			}
		}

		AE_CustomerMonitoredPartyViewController _AE_CustomerMonitoredPartyViews;
		public AE_CustomerMonitoredPartyViewController AE_CustomerMonitoredPartyViews
		{
			get
			{
				if (_AE_CustomerMonitoredPartyViews == null) _AE_CustomerMonitoredPartyViews = new AE_CustomerMonitoredPartyViewController();
				return _AE_CustomerMonitoredPartyViews;
			}
		}

		AE_CustomerSWINGViewController _AE_CustomerSWINGViews;
		public AE_CustomerSWINGViewController AE_CustomerSWINGViews
		{
			get
			{
				if (_AE_CustomerSWINGViews == null) _AE_CustomerSWINGViews = new AE_CustomerSWINGViewController();
				return _AE_CustomerSWINGViews;
			}
		}

		AE_CustomerSWINGAdd_DncViewController _AE_CustomerSWINGAdd_DncViews;
		public AE_CustomerSWINGAdd_DncViewController AE_CustomerSWINGAdd_DncViews
		{
			get
			{
				if (_AE_CustomerSWINGAdd_DncViews == null) _AE_CustomerSWINGAdd_DncViews = new AE_CustomerSWINGAdd_DncViewController();
				return _AE_CustomerSWINGAdd_DncViews;
			}
		}

		AE_CustomerSWINGEmergencyContactViewController _AE_CustomerSWINGEmergencyContactViews;
		public AE_CustomerSWINGEmergencyContactViewController AE_CustomerSWINGEmergencyContactViews
		{
			get
			{
				if (_AE_CustomerSWINGEmergencyContactViews == null) _AE_CustomerSWINGEmergencyContactViews = new AE_CustomerSWINGEmergencyContactViewController();
				return _AE_CustomerSWINGEmergencyContactViews;
			}
		}

		AE_CustomerSWINGEquipmentViewController _AE_CustomerSWINGEquipmentViews;
		public AE_CustomerSWINGEquipmentViewController AE_CustomerSWINGEquipmentViews
		{
			get
			{
				if (_AE_CustomerSWINGEquipmentViews == null) _AE_CustomerSWINGEquipmentViews = new AE_CustomerSWINGEquipmentViewController();
				return _AE_CustomerSWINGEquipmentViews;
			}
		}

		AE_CustomerSWINGInterimViewController _AE_CustomerSWINGInterimViews;
		public AE_CustomerSWINGInterimViewController AE_CustomerSWINGInterimViews
		{
			get
			{
				if (_AE_CustomerSWINGInterimViews == null) _AE_CustomerSWINGInterimViews = new AE_CustomerSWINGInterimViewController();
				return _AE_CustomerSWINGInterimViews;
			}
		}

		AE_CustomerSWINGPremiseAddressViewController _AE_CustomerSWINGPremiseAddressViews;
		public AE_CustomerSWINGPremiseAddressViewController AE_CustomerSWINGPremiseAddressViews
		{
			get
			{
				if (_AE_CustomerSWINGPremiseAddressViews == null) _AE_CustomerSWINGPremiseAddressViews = new AE_CustomerSWINGPremiseAddressViewController();
				return _AE_CustomerSWINGPremiseAddressViews;
			}
		}

		AE_CustomerSWINGSystemDetailViewController _AE_CustomerSWINGSystemDetailViews;
		public AE_CustomerSWINGSystemDetailViewController AE_CustomerSWINGSystemDetailViews
		{
			get
			{
				if (_AE_CustomerSWINGSystemDetailViews == null) _AE_CustomerSWINGSystemDetailViews = new AE_CustomerSWINGSystemDetailViewController();
				return _AE_CustomerSWINGSystemDetailViews;
			}
		}

		AE_CustomerSWUNGInfoViewController _AE_CustomerSWUNGInfoViews;
		public AE_CustomerSWUNGInfoViewController AE_CustomerSWUNGInfoViews
		{
			get
			{
				if (_AE_CustomerSWUNGInfoViews == null) _AE_CustomerSWUNGInfoViews = new AE_CustomerSWUNGInfoViewController();
				return _AE_CustomerSWUNGInfoViews;
			}
		}

		AE_GpsClientToCustomerMasterViewController _AE_GpsClientToCustomerMasterViews;
		public AE_GpsClientToCustomerMasterViewController AE_GpsClientToCustomerMasterViews
		{
			get
			{
				if (_AE_GpsClientToCustomerMasterViews == null) _AE_GpsClientToCustomerMasterViews = new AE_GpsClientToCustomerMasterViewController();
				return _AE_GpsClientToCustomerMasterViews;
			}
		}

		AE_InvoiceItemsViewController _AE_InvoiceItemsViews;
		public AE_InvoiceItemsViewController AE_InvoiceItemsViews
		{
			get
			{
				if (_AE_InvoiceItemsViews == null) _AE_InvoiceItemsViews = new AE_InvoiceItemsViewController();
				return _AE_InvoiceItemsViews;
			}
		}

		AE_InvoiceMsInstallInfoViewController _AE_InvoiceMsInstallInfoViews;
		public AE_InvoiceMsInstallInfoViewController AE_InvoiceMsInstallInfoViews
		{
			get
			{
				if (_AE_InvoiceMsInstallInfoViews == null) _AE_InvoiceMsInstallInfoViews = new AE_InvoiceMsInstallInfoViewController();
				return _AE_InvoiceMsInstallInfoViews;
			}
		}

		AE_PaymentFullViewController _AE_PaymentFullViews;
		public AE_PaymentFullViewController AE_PaymentFullViews
		{
			get
			{
				if (_AE_PaymentFullViews == null) _AE_PaymentFullViews = new AE_PaymentFullViewController();
				return _AE_PaymentFullViews;
			}
		}

		AeCustomersMsPrimaryViewController _AeCustomersMsPrimaryViews;
		public AeCustomersMsPrimaryViewController AeCustomersMsPrimaryViews
		{
			get
			{
				if (_AeCustomersMsPrimaryViews == null) _AeCustomersMsPrimaryViews = new AeCustomersMsPrimaryViewController();
				return _AeCustomersMsPrimaryViews;
			}
		}

		BX_BarcodesViewController _BX_BarcodesViews;
		public BX_BarcodesViewController BX_BarcodesViews
		{
			get
			{
				if (_BX_BarcodesViews == null) _BX_BarcodesViews = new BX_BarcodesViewController();
				return _BX_BarcodesViews;
			}
		}

		BX_BarcodeTypesAMAAndNOCViewController _BX_BarcodeTypesAMAAndNOCViews;
		public BX_BarcodeTypesAMAAndNOCViewController BX_BarcodeTypesAMAAndNOCViews
		{
			get
			{
				if (_BX_BarcodeTypesAMAAndNOCViews == null) _BX_BarcodeTypesAMAAndNOCViews = new BX_BarcodeTypesAMAAndNOCViewController();
				return _BX_BarcodeTypesAMAAndNOCViews;
			}
		}

		BX_DocumentFieldsAMNXS001ViewController _BX_DocumentFieldsAMNXS001Views;
		public BX_DocumentFieldsAMNXS001ViewController BX_DocumentFieldsAMNXS001Views
		{
			get
			{
				if (_BX_DocumentFieldsAMNXS001Views == null) _BX_DocumentFieldsAMNXS001Views = new BX_DocumentFieldsAMNXS001ViewController();
				return _BX_DocumentFieldsAMNXS001Views;
			}
		}

		BX_DocumentFieldsSONXS001ViewController _BX_DocumentFieldsSONXS001Views;
		public BX_DocumentFieldsSONXS001ViewController BX_DocumentFieldsSONXS001Views
		{
			get
			{
				if (_BX_DocumentFieldsSONXS001Views == null) _BX_DocumentFieldsSONXS001Views = new BX_DocumentFieldsSONXS001ViewController();
				return _BX_DocumentFieldsSONXS001Views;
			}
		}

		IE_LocationViewController _IE_LocationViews;
		public IE_LocationViewController IE_LocationViews
		{
			get
			{
				if (_IE_LocationViews == null) _IE_LocationViews = new IE_LocationViewController();
				return _IE_LocationViews;
			}
		}

		IE_PackingSlipViewController _IE_PackingSlipViews;
		public IE_PackingSlipViewController IE_PackingSlipViews
		{
			get
			{
				if (_IE_PackingSlipViews == null) _IE_PackingSlipViews = new IE_PackingSlipViewController();
				return _IE_PackingSlipViews;
			}
		}

		IE_ProductBarcodeLocationViewController _IE_ProductBarcodeLocationViews;
		public IE_ProductBarcodeLocationViewController IE_ProductBarcodeLocationViews
		{
			get
			{
				if (_IE_ProductBarcodeLocationViews == null) _IE_ProductBarcodeLocationViews = new IE_ProductBarcodeLocationViewController();
				return _IE_ProductBarcodeLocationViews;
			}
		}

		IE_ProductBarcodeTrackingViewController _IE_ProductBarcodeTrackingViews;
		public IE_ProductBarcodeTrackingViewController IE_ProductBarcodeTrackingViews
		{
			get
			{
				if (_IE_ProductBarcodeTrackingViews == null) _IE_ProductBarcodeTrackingViews = new IE_ProductBarcodeTrackingViewController();
				return _IE_ProductBarcodeTrackingViews;
			}
		}

		IE_PurchaseOrderItemsViewController _IE_PurchaseOrderItemsViews;
		public IE_PurchaseOrderItemsViewController IE_PurchaseOrderItemsViews
		{
			get
			{
				if (_IE_PurchaseOrderItemsViews == null) _IE_PurchaseOrderItemsViews = new IE_PurchaseOrderItemsViewController();
				return _IE_PurchaseOrderItemsViews;
			}
		}

		MC_AccountNotesAllInfoViewController _MC_AccountNotesAllInfoViews;
		public MC_AccountNotesAllInfoViewController MC_AccountNotesAllInfoViews
		{
			get
			{
				if (_MC_AccountNotesAllInfoViews == null) _MC_AccountNotesAllInfoViews = new MC_AccountNotesAllInfoViewController();
				return _MC_AccountNotesAllInfoViews;
			}
		}

		MC_AddressesViewController _MC_AddressesViews;
		public MC_AddressesViewController MC_AddressesViews
		{
			get
			{
				if (_MC_AddressesViews == null) _MC_AddressesViews = new MC_AddressesViewController();
				return _MC_AddressesViews;
			}
		}

		MC_AddressesMsPremiseViewController _MC_AddressesMsPremiseViews;
		public MC_AddressesMsPremiseViewController MC_AddressesMsPremiseViews
		{
			get
			{
				if (_MC_AddressesMsPremiseViews == null) _MC_AddressesMsPremiseViews = new MC_AddressesMsPremiseViewController();
				return _MC_AddressesMsPremiseViews;
			}
		}

		MS_AccountAndLeadInfoViewController _MS_AccountAndLeadInfoViews;
		public MS_AccountAndLeadInfoViewController MS_AccountAndLeadInfoViews
		{
			get
			{
				if (_MS_AccountAndLeadInfoViews == null) _MS_AccountAndLeadInfoViews = new MS_AccountAndLeadInfoViewController();
				return _MS_AccountAndLeadInfoViews;
			}
		}

		MS_AccountClientDetailsViewController _MS_AccountClientDetailsViews;
		public MS_AccountClientDetailsViewController MS_AccountClientDetailsViews
		{
			get
			{
				if (_MS_AccountClientDetailsViews == null) _MS_AccountClientDetailsViews = new MS_AccountClientDetailsViewController();
				return _MS_AccountClientDetailsViews;
			}
		}

		MS_AccountClientsViewController _MS_AccountClientsViews;
		public MS_AccountClientsViewController MS_AccountClientsViews
		{
			get
			{
				if (_MS_AccountClientsViews == null) _MS_AccountClientsViews = new MS_AccountClientsViewController();
				return _MS_AccountClientsViews;
			}
		}

		MS_AccountCreditsAndInstallsViewController _MS_AccountCreditsAndInstallsViews;
		public MS_AccountCreditsAndInstallsViewController MS_AccountCreditsAndInstallsViews
		{
			get
			{
				if (_MS_AccountCreditsAndInstallsViews == null) _MS_AccountCreditsAndInstallsViews = new MS_AccountCreditsAndInstallsViewController();
				return _MS_AccountCreditsAndInstallsViews;
			}
		}

		MS_AccountDispatchAgencyAssignmentViewController _MS_AccountDispatchAgencyAssignmentViews;
		public MS_AccountDispatchAgencyAssignmentViewController MS_AccountDispatchAgencyAssignmentViews
		{
			get
			{
				if (_MS_AccountDispatchAgencyAssignmentViews == null) _MS_AccountDispatchAgencyAssignmentViews = new MS_AccountDispatchAgencyAssignmentViewController();
				return _MS_AccountDispatchAgencyAssignmentViews;
			}
		}

		MS_AccountEquipmentInfoToGPViewController _MS_AccountEquipmentInfoToGPViews;
		public MS_AccountEquipmentInfoToGPViewController MS_AccountEquipmentInfoToGPViews
		{
			get
			{
				if (_MS_AccountEquipmentInfoToGPViews == null) _MS_AccountEquipmentInfoToGPViews = new MS_AccountEquipmentInfoToGPViewController();
				return _MS_AccountEquipmentInfoToGPViews;
			}
		}

		MS_AccountEquipmentsViewController _MS_AccountEquipmentsViews;
		public MS_AccountEquipmentsViewController MS_AccountEquipmentsViews
		{
			get
			{
				if (_MS_AccountEquipmentsViews == null) _MS_AccountEquipmentsViews = new MS_AccountEquipmentsViewController();
				return _MS_AccountEquipmentsViews;
			}
		}

		MS_AccountEquipmentsAllViewController _MS_AccountEquipmentsAllViews;
		public MS_AccountEquipmentsAllViewController MS_AccountEquipmentsAllViews
		{
			get
			{
				if (_MS_AccountEquipmentsAllViews == null) _MS_AccountEquipmentsAllViews = new MS_AccountEquipmentsAllViewController();
				return _MS_AccountEquipmentsAllViews;
			}
		}

		MS_AccountEventViewController _MS_AccountEventViews;
		public MS_AccountEventViewController MS_AccountEventViews
		{
			get
			{
				if (_MS_AccountEventViews == null) _MS_AccountEventViews = new MS_AccountEventViewController();
				return _MS_AccountEventViews;
			}
		}

		MS_AccountMonitorInformationsViewController _MS_AccountMonitorInformationsViews;
		public MS_AccountMonitorInformationsViewController MS_AccountMonitorInformationsViews
		{
			get
			{
				if (_MS_AccountMonitorInformationsViews == null) _MS_AccountMonitorInformationsViews = new MS_AccountMonitorInformationsViewController();
				return _MS_AccountMonitorInformationsViews;
			}
		}

		MS_AccountOnlineStatusInfoViewController _MS_AccountOnlineStatusInfoViews;
		public MS_AccountOnlineStatusInfoViewController MS_AccountOnlineStatusInfoViews
		{
			get
			{
				if (_MS_AccountOnlineStatusInfoViews == null) _MS_AccountOnlineStatusInfoViews = new MS_AccountOnlineStatusInfoViewController();
				return _MS_AccountOnlineStatusInfoViews;
			}
		}

		MS_AccountSalesInformationsViewController _MS_AccountSalesInformationsViews;
		public MS_AccountSalesInformationsViewController MS_AccountSalesInformationsViews
		{
			get
			{
				if (_MS_AccountSalesInformationsViews == null) _MS_AccountSalesInformationsViews = new MS_AccountSalesInformationsViewController();
				return _MS_AccountSalesInformationsViews;
			}
		}

		MS_DeviceEventsViewController _MS_DeviceEventsViews;
		public MS_DeviceEventsViewController MS_DeviceEventsViews
		{
			get
			{
				if (_MS_DeviceEventsViews == null) _MS_DeviceEventsViews = new MS_DeviceEventsViewController();
				return _MS_DeviceEventsViews;
			}
		}

		MS_DispatchAgenciesViewController _MS_DispatchAgenciesViews;
		public MS_DispatchAgenciesViewController MS_DispatchAgenciesViews
		{
			get
			{
				if (_MS_DispatchAgenciesViews == null) _MS_DispatchAgenciesViews = new MS_DispatchAgenciesViewController();
				return _MS_DispatchAgenciesViews;
			}
		}

		MS_EquipmentAccountZoneTypeEventsViewController _MS_EquipmentAccountZoneTypeEventsViews;
		public MS_EquipmentAccountZoneTypeEventsViewController MS_EquipmentAccountZoneTypeEventsViews
		{
			get
			{
				if (_MS_EquipmentAccountZoneTypeEventsViews == null) _MS_EquipmentAccountZoneTypeEventsViews = new MS_EquipmentAccountZoneTypeEventsViewController();
				return _MS_EquipmentAccountZoneTypeEventsViews;
			}
		}

		MS_EquipmentAccountZoneTypesViewController _MS_EquipmentAccountZoneTypesViews;
		public MS_EquipmentAccountZoneTypesViewController MS_EquipmentAccountZoneTypesViews
		{
			get
			{
				if (_MS_EquipmentAccountZoneTypesViews == null) _MS_EquipmentAccountZoneTypesViews = new MS_EquipmentAccountZoneTypesViewController();
				return _MS_EquipmentAccountZoneTypesViews;
			}
		}

		MS_EquipmentLocationsViewController _MS_EquipmentLocationsViews;
		public MS_EquipmentLocationsViewController MS_EquipmentLocationsViews
		{
			get
			{
				if (_MS_EquipmentLocationsViews == null) _MS_EquipmentLocationsViews = new MS_EquipmentLocationsViewController();
				return _MS_EquipmentLocationsViews;
			}
		}

		MS_EquipmentsViewController _MS_EquipmentsViews;
		public MS_EquipmentsViewController MS_EquipmentsViews
		{
			get
			{
				if (_MS_EquipmentsViews == null) _MS_EquipmentsViews = new MS_EquipmentsViewController();
				return _MS_EquipmentsViews;
			}
		}

		MS_IndustryAccountNumbersViewController _MS_IndustryAccountNumbersViews;
		public MS_IndustryAccountNumbersViewController MS_IndustryAccountNumbersViews
		{
			get
			{
				if (_MS_IndustryAccountNumbersViews == null) _MS_IndustryAccountNumbersViews = new MS_IndustryAccountNumbersViewController();
				return _MS_IndustryAccountNumbersViews;
			}
		}

		MS_IndustryAccountNumbersWithReceiverLineInfoViewController _MS_IndustryAccountNumbersWithReceiverLineInfoViews;
		public MS_IndustryAccountNumbersWithReceiverLineInfoViewController MS_IndustryAccountNumbersWithReceiverLineInfoViews
		{
			get
			{
				if (_MS_IndustryAccountNumbersWithReceiverLineInfoViews == null) _MS_IndustryAccountNumbersWithReceiverLineInfoViews = new MS_IndustryAccountNumbersWithReceiverLineInfoViewController();
				return _MS_IndustryAccountNumbersWithReceiverLineInfoViews;
			}
		}

		MS_IndustryNumberByCallerIdViewController _MS_IndustryNumberByCallerIdViews;
		public MS_IndustryNumberByCallerIdViewController MS_IndustryNumberByCallerIdViews
		{
			get
			{
				if (_MS_IndustryNumberByCallerIdViews == null) _MS_IndustryNumberByCallerIdViews = new MS_IndustryNumberByCallerIdViewController();
				return _MS_IndustryNumberByCallerIdViews;
			}
		}

		MS_LeadTakeOversViewController _MS_LeadTakeOversViews;
		public MS_LeadTakeOversViewController MS_LeadTakeOversViews
		{
			get
			{
				if (_MS_LeadTakeOversViews == null) _MS_LeadTakeOversViews = new MS_LeadTakeOversViewController();
				return _MS_LeadTakeOversViews;
			}
		}

		MS_MonitronicsEntitySystemTypeXRefViewController _MS_MonitronicsEntitySystemTypeXRefViews;
		public MS_MonitronicsEntitySystemTypeXRefViewController MS_MonitronicsEntitySystemTypeXRefViews
		{
			get
			{
				if (_MS_MonitronicsEntitySystemTypeXRefViews == null) _MS_MonitronicsEntitySystemTypeXRefViews = new MS_MonitronicsEntitySystemTypeXRefViewController();
				return _MS_MonitronicsEntitySystemTypeXRefViews;
			}
		}

		MS_ReceiverBlockCellDeviceInfoViewController _MS_ReceiverBlockCellDeviceInfoViews;
		public MS_ReceiverBlockCellDeviceInfoViewController MS_ReceiverBlockCellDeviceInfoViews
		{
			get
			{
				if (_MS_ReceiverBlockCellDeviceInfoViews == null) _MS_ReceiverBlockCellDeviceInfoViews = new MS_ReceiverBlockCellDeviceInfoViewController();
				return _MS_ReceiverBlockCellDeviceInfoViews;
			}
		}

		QL_CreditReportTransactionAndTokenViewController _QL_CreditReportTransactionAndTokenViews;
		public QL_CreditReportTransactionAndTokenViewController QL_CreditReportTransactionAndTokenViews
		{
			get
			{
				if (_QL_CreditReportTransactionAndTokenViews == null) _QL_CreditReportTransactionAndTokenViews = new QL_CreditReportTransactionAndTokenViewController();
				return _QL_CreditReportTransactionAndTokenViews;
			}
		}

		QL_LeadBasicInfoViewController _QL_LeadBasicInfoViews;
		public QL_LeadBasicInfoViewController QL_LeadBasicInfoViews
		{
			get
			{
				if (_QL_LeadBasicInfoViews == null) _QL_LeadBasicInfoViews = new QL_LeadBasicInfoViewController();
				return _QL_LeadBasicInfoViews;
			}
		}

		QL_LeadProductOffersViewController _QL_LeadProductOffersViews;
		public QL_LeadProductOffersViewController QL_LeadProductOffersViews
		{
			get
			{
				if (_QL_LeadProductOffersViews == null) _QL_LeadProductOffersViews = new QL_LeadProductOffersViewController();
				return _QL_LeadProductOffersViews;
			}
		}

		QL_LeadSearchResultViewController _QL_LeadSearchResultViews;
		public QL_LeadSearchResultViewController QL_LeadSearchResultViews
		{
			get
			{
				if (_QL_LeadSearchResultViews == null) _QL_LeadSearchResultViews = new QL_LeadSearchResultViewController();
				return _QL_LeadSearchResultViews;
			}
		}

		QL_QualifyCustomerInfoViewController _QL_QualifyCustomerInfoViews;
		public QL_QualifyCustomerInfoViewController QL_QualifyCustomerInfoViews
		{
			get
			{
				if (_QL_QualifyCustomerInfoViews == null) _QL_QualifyCustomerInfoViews = new QL_QualifyCustomerInfoViewController();
				return _QL_QualifyCustomerInfoViews;
			}
		}

		RandNumberViewController _RandNumberViews;
		public RandNumberViewController RandNumberViews
		{
			get
			{
				if (_RandNumberViews == null) _RandNumberViews = new RandNumberViewController();
				return _RandNumberViews;
			}
		}

		SAE_BillingHistoryViewController _SAE_BillingHistoryViews;
		public SAE_BillingHistoryViewController SAE_BillingHistoryViews
		{
			get
			{
				if (_SAE_BillingHistoryViews == null) _SAE_BillingHistoryViews = new SAE_BillingHistoryViewController();
				return _SAE_BillingHistoryViews;
			}
		}

		SAE_BillingInfoSummaryViewController _SAE_BillingInfoSummaryViews;
		public SAE_BillingInfoSummaryViewController SAE_BillingInfoSummaryViews
		{
			get
			{
				if (_SAE_BillingInfoSummaryViews == null) _SAE_BillingInfoSummaryViews = new SAE_BillingInfoSummaryViewController();
				return _SAE_BillingInfoSummaryViews;
			}
		}

		SE_AccountTicketsViewController _SE_AccountTicketsViews;
		public SE_AccountTicketsViewController SE_AccountTicketsViews
		{
			get
			{
				if (_SE_AccountTicketsViews == null) _SE_AccountTicketsViews = new SE_AccountTicketsViewController();
				return _SE_AccountTicketsViews;
			}
		}

		SE_ScheduleBlocksViewController _SE_ScheduleBlocksViews;
		public SE_ScheduleBlocksViewController SE_ScheduleBlocksViews
		{
			get
			{
				if (_SE_ScheduleBlocksViews == null) _SE_ScheduleBlocksViews = new SE_ScheduleBlocksViewController();
				return _SE_ScheduleBlocksViews;
			}
		}

		SE_ScheduleBlockTicketsViewController _SE_ScheduleBlockTicketsViews;
		public SE_ScheduleBlockTicketsViewController SE_ScheduleBlockTicketsViews
		{
			get
			{
				if (_SE_ScheduleBlockTicketsViews == null) _SE_ScheduleBlockTicketsViews = new SE_ScheduleBlockTicketsViewController();
				return _SE_ScheduleBlockTicketsViews;
			}
		}

		SE_TechnicianScheduleBlocksViewController _SE_TechnicianScheduleBlocksViews;
		public SE_TechnicianScheduleBlocksViewController SE_TechnicianScheduleBlocksViews
		{
			get
			{
				if (_SE_TechnicianScheduleBlocksViews == null) _SE_TechnicianScheduleBlocksViews = new SE_TechnicianScheduleBlocksViewController();
				return _SE_TechnicianScheduleBlocksViews;
			}
		}

		SE_TicketsViewController _SE_TicketsViews;
		public SE_TicketsViewController SE_TicketsViews
		{
			get
			{
				if (_SE_TicketsViews == null) _SE_TicketsViews = new SE_TicketsViewController();
				return _SE_TicketsViews;
			}
		}

		TS_ServiceTicketStatusViewController _TS_ServiceTicketStatusViews;
		public TS_ServiceTicketStatusViewController TS_ServiceTicketStatusViews
		{
			get
			{
				if (_TS_ServiceTicketStatusViews == null) _TS_ServiceTicketStatusViews = new TS_ServiceTicketStatusViewController();
				return _TS_ServiceTicketStatusViews;
			}
		}

		TS_TeamViewController _TS_TeamViews;
		public TS_TeamViewController TS_TeamViews
		{
			get
			{
				if (_TS_TeamViews == null) _TS_TeamViews = new TS_TeamViewController();
				return _TS_TeamViews;
			}
		}

		TS_TechViewController _TS_TechViews;
		public TS_TechViewController TS_TechViews
		{
			get
			{
				if (_TS_TechViews == null) _TS_TechViews = new TS_TechViewController();
				return _TS_TechViews;
			}
		}

		UI_ApplicationMenuViewController _UI_ApplicationMenuViews;
		public UI_ApplicationMenuViewController UI_ApplicationMenuViews
		{
			get
			{
				if (_UI_ApplicationMenuViews == null) _UI_ApplicationMenuViews = new UI_ApplicationMenuViewController();
				return _UI_ApplicationMenuViews;
			}
		}

		UI_ApplicationVersionsViewController _UI_ApplicationVersionsViews;
		public UI_ApplicationVersionsViewController UI_ApplicationVersionsViews
		{
			get
			{
				if (_UI_ApplicationVersionsViews == null) _UI_ApplicationVersionsViews = new UI_ApplicationVersionsViewController();
				return _UI_ApplicationVersionsViews;
			}
		}

		UI_ApplicationVersionsCurrentVersionsViewController _UI_ApplicationVersionsCurrentVersionsViews;
		public UI_ApplicationVersionsCurrentVersionsViewController UI_ApplicationVersionsCurrentVersionsViews
		{
			get
			{
				if (_UI_ApplicationVersionsCurrentVersionsViews == null) _UI_ApplicationVersionsCurrentVersionsViews = new UI_ApplicationVersionsCurrentVersionsViewController();
				return _UI_ApplicationVersionsCurrentVersionsViews;
			}
		}

		UI_MenuItemsExpandedPermissionsViewController _UI_MenuItemsExpandedPermissionsViews;
		public UI_MenuItemsExpandedPermissionsViewController UI_MenuItemsExpandedPermissionsViews
		{
			get
			{
				if (_UI_MenuItemsExpandedPermissionsViews == null) _UI_MenuItemsExpandedPermissionsViews = new UI_MenuItemsExpandedPermissionsViewController();
				return _UI_MenuItemsExpandedPermissionsViews;
			}
		}

		UI_MenusCurrentMenusViewController _UI_MenusCurrentMenusViews;
		public UI_MenusCurrentMenusViewController UI_MenusCurrentMenusViews
		{
			get
			{
				if (_UI_MenusCurrentMenusViews == null) _UI_MenusCurrentMenusViews = new UI_MenusCurrentMenusViewController();
				return _UI_MenusCurrentMenusViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class AE_AccountAddressTypeController : BaseTableController<AE_AccountAddressType, AE_AccountAddressTypeCollection> { }
	public class AE_AgingStepController : BaseTableController<AE_AgingStep, AE_AgingStepCollection> { }
	public class AE_BankAccountTypeController : BaseTableController<AE_BankAccountType, AE_BankAccountTypeCollection> { }
	public class AE_BillingCustomerController : BaseTableController<AE_BillingCustomer, AE_BillingCustomerCollection> { }
	public class AE_BillingTypeController : BaseTableController<AE_BillingType, AE_BillingTypeCollection> { }
	public class AE_ContractController : BaseTableController<AE_Contract, AE_ContractCollection> { }
	public class AE_ContractTemplateController : BaseTableController<AE_ContractTemplate, AE_ContractTemplateCollection> { }
	public class AE_CreditCardTypeController : BaseTableController<AE_CreditCardType, AE_CreditCardTypeCollection> { }
	public class AE_CustomerAccountController : BaseTableController<AE_CustomerAccount, AE_CustomerAccountCollection> { }
	public class AE_CustomerAddressController : BaseTableController<AE_CustomerAddress, AE_CustomerAddressCollection> { }
	public class AE_CustomerAddressTypeController : BaseTableController<AE_CustomerAddressType, AE_CustomerAddressTypeCollection> { }
	public class AE_CustomerGpsClientController : BaseTableController<AE_CustomerGpsClient, AE_CustomerGpsClientCollection> { }
	public class AE_CustomerMasterFileController : BaseTableController<AE_CustomerMasterFile, AE_CustomerMasterFileCollection> { }
	public class AE_CustomerMasterFileViewHistoryController : BaseTableController<AE_CustomerMasterFileViewHistory, AE_CustomerMasterFileViewHistoryCollection> { }
	public class AE_CustomerMasterToCustomerController : BaseTableController<AE_CustomerMasterToCustomer, AE_CustomerMasterToCustomerCollection> { }
	public class AE_CustomerController : BaseTableController<AE_Customer, AE_CustomerCollection> { }
	public class AE_CustomerSetupStatusController : BaseTableController<AE_CustomerSetupStatus, AE_CustomerSetupStatusCollection> { }
	public class AE_CustomerTypeController : BaseTableController<AE_CustomerType, AE_CustomerTypeCollection> { }
	public class AE_DealerPurchaseOrderItemController : BaseTableController<AE_DealerPurchaseOrderItem, AE_DealerPurchaseOrderItemCollection> { }
	public class AE_DealerPurchaseOrderController : BaseTableController<AE_DealerPurchaseOrder, AE_DealerPurchaseOrderCollection> { }
	public class AE_DealerController : BaseTableController<AE_Dealer, AE_DealerCollection> { }
	public class AE_GpsClientToCustomerMasterController : BaseTableController<AE_GpsClientToCustomerMaster, AE_GpsClientToCustomerMasterCollection> { }
	public class AE_InvoiceItemController : BaseTableController<AE_InvoiceItem, AE_InvoiceItemCollection> { }
	public class AE_InvoicePaymentJoinController : BaseTableController<AE_InvoicePaymentJoin, AE_InvoicePaymentJoinCollection> { }
	public class AE_InvoiceController : BaseTableController<AE_Invoice, AE_InvoiceCollection> { }
	public class AE_InvoiceTemplateItemController : BaseTableController<AE_InvoiceTemplateItem, AE_InvoiceTemplateItemCollection> { }
	public class AE_InvoiceTemplateController : BaseTableController<AE_InvoiceTemplate, AE_InvoiceTemplateCollection> { }
	public class AE_InvoiceTypeController : BaseTableController<AE_InvoiceType, AE_InvoiceTypeCollection> { }
	public class AE_ItemAccountController : BaseTableController<AE_ItemAccount, AE_ItemAccountCollection> { }
	public class AE_ItemCrmOnlyController : BaseTableController<AE_ItemCrmOnly, AE_ItemCrmOnlyCollection> { }
	public class AE_ItemInterimController : BaseTableController<AE_ItemInterim, AE_ItemInterimCollection> { }
	public class AE_ItemController : BaseTableController<AE_Item, AE_ItemCollection> { }
	public class AE_ItemTypeController : BaseTableController<AE_ItemType, AE_ItemTypeCollection> { }
	public class AE_ManufacturerController : BaseTableController<AE_Manufacturer, AE_ManufacturerCollection> { }
	public class AE_PaymentMethodController : BaseTableController<AE_PaymentMethod, AE_PaymentMethodCollection> { }
	public class AE_PaymentController : BaseTableController<AE_Payment, AE_PaymentCollection> { }
	public class AE_PaymentTypeController : BaseTableController<AE_PaymentType, AE_PaymentTypeCollection> { }
	public class AE_ProductGroupController : BaseTableController<AE_ProductGroup, AE_ProductGroupCollection> { }
	public class AE_ProductPriceSchemaController : BaseTableController<AE_ProductPriceSchema, AE_ProductPriceSchemaCollection> { }
	public class AE_ProductController : BaseTableController<AE_Product, AE_ProductCollection> { }
	public class AE_ProductTypeController : BaseTableController<AE_ProductType, AE_ProductTypeCollection> { }
	public class AE_TaxOptionController : BaseTableController<AE_TaxOption, AE_TaxOptionCollection> { }
	public class BE_BarcodeController : BaseTableController<BE_Barcode, BE_BarcodeCollection> { }
	public class BE_BarcodeSchemaController : BaseTableController<BE_BarcodeSchema, BE_BarcodeSchemaCollection> { }
	public class BE_BundleAccountController : BaseTableController<BE_BundleAccount, BE_BundleAccountCollection> { }
	public class BE_BundleItemController : BaseTableController<BE_BundleItem, BE_BundleItemCollection> { }
	public class BE_BundleController : BaseTableController<BE_Bundle, BE_BundleCollection> { }
	public class BE_DocTypeColumnController : BaseTableController<BE_DocTypeColumn, BE_DocTypeColumnCollection> { }
	public class BE_DocTypeController : BaseTableController<BE_DocType, BE_DocTypeCollection> { }
	public class BE_PrefixDocumentController : BaseTableController<BE_PrefixDocument, BE_PrefixDocumentCollection> { }
	public class BE_PrefixController : BaseTableController<BE_Prefix, BE_PrefixCollection> { }
	public class BE_PrefixPrinterController : BaseTableController<BE_PrefixPrinter, BE_PrefixPrinterCollection> { }
	public class BX_BarcodeController : BaseTableController<BX_Barcode, BX_BarcodeCollection> { }
	public class BX_BarcodeTypeController : BaseTableController<BX_BarcodeType, BX_BarcodeTypeCollection> { }
	public class BX_DocTypeController : BaseTableController<BX_DocType, BX_DocTypeCollection> { }
	public class BX_DocumentFieldController : BaseTableController<BX_DocumentField, BX_DocumentFieldCollection> { }
	public class BX_PrinterController : BaseTableController<BX_Printer, BX_PrinterCollection> { }
	public class CA_AppointmentController : BaseTableController<CA_Appointment, CA_AppointmentCollection> { }
	public class CA_AppointmentTypeController : BaseTableController<CA_AppointmentType, CA_AppointmentTypeCollection> { }
	public class CA_ReminderDelyTypeController : BaseTableController<CA_ReminderDelyType, CA_ReminderDelyTypeCollection> { }
	public class CA_ReminderMediaTypeController : BaseTableController<CA_ReminderMediaType, CA_ReminderMediaTypeCollection> { }
	public class DC_AreaCodeController : BaseTableController<DC_AreaCode, DC_AreaCodeCollection> { }
	public class DC_CompanyPhoneNumberController : BaseTableController<DC_CompanyPhoneNumber, DC_CompanyPhoneNumberCollection> { }
	public class DC_PhoneNumberController : BaseTableController<DC_PhoneNumber, DC_PhoneNumberCollection> { }
	public class GS_AccountController : BaseTableController<GS_Account, GS_AccountCollection> { }
	public class IE_AuditController : BaseTableController<IE_Audit, IE_AuditCollection> { }
	public class IE_LocationTypeController : BaseTableController<IE_LocationType, IE_LocationTypeCollection> { }
	public class IE_PackingSlipItemController : BaseTableController<IE_PackingSlipItem, IE_PackingSlipItemCollection> { }
	public class IE_PackingSlipController : BaseTableController<IE_PackingSlip, IE_PackingSlipCollection> { }
	public class IE_ProductBarcodeBundleController : BaseTableController<IE_ProductBarcodeBundle, IE_ProductBarcodeBundleCollection> { }
	public class IE_ProductBarcodeController : BaseTableController<IE_ProductBarcode, IE_ProductBarcodeCollection> { }
	public class IE_ProductBarcodeTrackingController : BaseTableController<IE_ProductBarcodeTracking, IE_ProductBarcodeTrackingCollection> { }
	public class IE_ProductBarcodeTrackingTypeController : BaseTableController<IE_ProductBarcodeTrackingType, IE_ProductBarcodeTrackingTypeCollection> { }
	public class IE_PurchaseOrderItemController : BaseTableController<IE_PurchaseOrderItem, IE_PurchaseOrderItemCollection> { }
	public class IE_PurchaseOrderController : BaseTableController<IE_PurchaseOrder, IE_PurchaseOrderCollection> { }
	public class IE_ReturnToManufacturerItemController : BaseTableController<IE_ReturnToManufacturerItem, IE_ReturnToManufacturerItemCollection> { }
	public class IE_ReturnToManufacturerController : BaseTableController<IE_ReturnToManufacturer, IE_ReturnToManufacturerCollection> { }
	public class IE_StockingLevelController : BaseTableController<IE_StockingLevel, IE_StockingLevelCollection> { }
	public class IE_VendorController : BaseTableController<IE_Vendor, IE_VendorCollection> { }
	public class IE_WarehouseSiteController : BaseTableController<IE_WarehouseSite, IE_WarehouseSiteCollection> { }
	public class IS_AccountController : BaseTableController<IS_Account, IS_AccountCollection> { }
	public class LL_AccountController : BaseTableController<LL_Account, LL_AccountCollection> { }
	public class MC_AccountAddressController : BaseTableController<MC_AccountAddress, MC_AccountAddressCollection> { }
	public class MC_AccountCancelReasonController : BaseTableController<MC_AccountCancelReason, MC_AccountCancelReasonCollection> { }
	public class MC_AccountFlagController : BaseTableController<MC_AccountFlag, MC_AccountFlagCollection> { }
	public class MC_AccountFlagTypeController : BaseTableController<MC_AccountFlagType, MC_AccountFlagTypeCollection> { }
	public class MC_AccountInventoryController : BaseTableController<MC_AccountInventory, MC_AccountInventoryCollection> { }
	public class MC_AccountNoteCat1Controller : BaseTableController<MC_AccountNoteCat1, MC_AccountNoteCat1Collection> { }
	public class MC_AccountNoteCat2Controller : BaseTableController<MC_AccountNoteCat2, MC_AccountNoteCat2Collection> { }
	public class MC_AccountNoteController : BaseTableController<MC_AccountNote, MC_AccountNoteCollection> { }
	public class MC_AccountNoteTypeController : BaseTableController<MC_AccountNoteType, MC_AccountNoteTypeCollection> { }
	public class MC_AccountController : BaseTableController<MC_Account, MC_AccountCollection> { }
	public class MC_AccountStatusCategoryController : BaseTableController<MC_AccountStatusCategory, MC_AccountStatusCategoryCollection> { }
	public class MC_AccountStatusEventController : BaseTableController<MC_AccountStatusEvent, MC_AccountStatusEventCollection> { }
	public class MC_AccountStatusTypeController : BaseTableController<MC_AccountStatusType, MC_AccountStatusTypeCollection> { }
	public class MC_AccountSwungInfoController : BaseTableController<MC_AccountSwungInfo, MC_AccountSwungInfoCollection> { }
	public class MC_AccountTypeController : BaseTableController<MC_AccountType, MC_AccountTypeCollection> { }
	public class MC_AddressCoordController : BaseTableController<MC_AddressCoord, MC_AddressCoordCollection> { }
	public class MC_AddressCoordStatusCodeController : BaseTableController<MC_AddressCoordStatusCode, MC_AddressCoordStatusCodeCollection> { }
	public class MC_AddressDirectionalTypeController : BaseTableController<MC_AddressDirectionalType, MC_AddressDirectionalTypeCollection> { }
	public class MC_AddressController : BaseTableController<MC_Address, MC_AddressCollection> { }
	public class MC_AddressStreetTypeController : BaseTableController<MC_AddressStreetType, MC_AddressStreetTypeCollection> { }
	public class MC_AddressTypeController : BaseTableController<MC_AddressType, MC_AddressTypeCollection> { }
	public class MC_AddressValidationStateController : BaseTableController<MC_AddressValidationState, MC_AddressValidationStateCollection> { }
	public class MC_AddressValidationVendorController : BaseTableController<MC_AddressValidationVendor, MC_AddressValidationVendorCollection> { }
	public class MC_CorporateUserGroupMappingController : BaseTableController<MC_CorporateUserGroupMapping, MC_CorporateUserGroupMappingCollection> { }
	public class MC_CorporateUserGroupController : BaseTableController<MC_CorporateUserGroup, MC_CorporateUserGroupCollection> { }
	public class MC_CorporateUserController : BaseTableController<MC_CorporateUser, MC_CorporateUserCollection> { }
	public class MC_CorporateUserTypeController : BaseTableController<MC_CorporateUserType, MC_CorporateUserTypeCollection> { }
	public class MC_DealerUserController : BaseTableController<MC_DealerUser, MC_DealerUserCollection> { }
	public class MC_DealerUserTypeController : BaseTableController<MC_DealerUserType, MC_DealerUserTypeCollection> { }
	public class MC_DepartmentAccountNoteCat1Controller : BaseTableController<MC_DepartmentAccountNoteCat1, MC_DepartmentAccountNoteCat1Collection> { }
	public class MC_DepartmentController : BaseTableController<MC_Department, MC_DepartmentCollection> { }
	public class MC_FriendsAndFamilyTypeController : BaseTableController<MC_FriendsAndFamilyType, MC_FriendsAndFamilyTypeCollection> { }
	public class MC_HolidayController : BaseTableController<MC_Holiday, MC_HolidayCollection> { }
	public class MC_LocalizationController : BaseTableController<MC_Localization, MC_LocalizationCollection> { }
	public class MC_MarketController : BaseTableController<MC_Market, MC_MarketCollection> { }
	public class MC_PaymentBankAccountTypeController : BaseTableController<MC_PaymentBankAccountType, MC_PaymentBankAccountTypeCollection> { }
	public class MC_PaymentCreditCardTypeController : BaseTableController<MC_PaymentCreditCardType, MC_PaymentCreditCardTypeCollection> { }
	public class MC_PoliticalCountryController : BaseTableController<MC_PoliticalCountry, MC_PoliticalCountryCollection> { }
	public class MC_PoliticalStateController : BaseTableController<MC_PoliticalState, MC_PoliticalStateCollection> { }
	public class MC_PoliticalTimeZoneController : BaseTableController<MC_PoliticalTimeZone, MC_PoliticalTimeZoneCollection> { }
	public class MG_AuthorizeNetConfigController : BaseTableController<MG_AuthorizeNetConfig, MG_AuthorizeNetConfigCollection> { }
	public class MG_AuthorizeNetTransactionController : BaseTableController<MG_AuthorizeNetTransaction, MG_AuthorizeNetTransactionCollection> { }
	public class MG_GatewayController : BaseTableController<MG_Gateway, MG_GatewayCollection> { }
	public class MG_TransactionController : BaseTableController<MG_Transaction, MG_TransactionCollection> { }
	public class MS_AccountAGController : BaseTableController<MS_AccountAG, MS_AccountAGCollection> { }
	public class MS_AccountCellularADCRegisterController : BaseTableController<MS_AccountCellularADCRegister, MS_AccountCellularADCRegisterCollection> { }
	public class MS_AccountCellularSubmitController : BaseTableController<MS_AccountCellularSubmit, MS_AccountCellularSubmitCollection> { }
	public class MS_AccountCellularSubmitTypeController : BaseTableController<MS_AccountCellularSubmitType, MS_AccountCellularSubmitTypeCollection> { }
	public class MS_AccountCellularSubmitVendorController : BaseTableController<MS_AccountCellularSubmitVendor, MS_AccountCellularSubmitVendorCollection> { }
	public class MS_AccountCellularTypeController : BaseTableController<MS_AccountCellularType, MS_AccountCellularTypeCollection> { }
	public class MS_AccountCustomersOLDController : BaseTableController<MS_AccountCustomersOLD, MS_AccountCustomersOLDCollection> { }
	public class MS_AccountCustomerTypeController : BaseTableController<MS_AccountCustomerType, MS_AccountCustomerTypeCollection> { }
	public class MS_AccountDispatchAgencyAssignmentController : BaseTableController<MS_AccountDispatchAgencyAssignment, MS_AccountDispatchAgencyAssignmentCollection> { }
	public class MS_AccountDslSeizureTypeController : BaseTableController<MS_AccountDslSeizureType, MS_AccountDslSeizureTypeCollection> { }
	public class MS_AccountEquipmentController : BaseTableController<MS_AccountEquipment, MS_AccountEquipmentCollection> { }
	public class MS_AccountEquipmentUpgradeTypeController : BaseTableController<MS_AccountEquipmentUpgradeType, MS_AccountEquipmentUpgradeTypeCollection> { }
	public class MS_AccountEventController : BaseTableController<MS_AccountEvent, MS_AccountEventCollection> { }
	public class MS_AccountHoldCatg1Controller : BaseTableController<MS_AccountHoldCatg1, MS_AccountHoldCatg1Collection> { }
	public class MS_AccountHoldCatg2Controller : BaseTableController<MS_AccountHoldCatg2, MS_AccountHoldCatg2Collection> { }
	public class MS_AccountHoldController : BaseTableController<MS_AccountHold, MS_AccountHoldCollection> { }
	public class MS_AccountHoldStockController : BaseTableController<MS_AccountHoldStock, MS_AccountHoldStockCollection> { }
	public class MS_AccountPackageItemController : BaseTableController<MS_AccountPackageItem, MS_AccountPackageItemCollection> { }
	public class MS_AccountPackageItemTypeController : BaseTableController<MS_AccountPackageItemType, MS_AccountPackageItemTypeCollection> { }
	public class MS_AccountPackageController : BaseTableController<MS_AccountPackage, MS_AccountPackageCollection> { }
	public class MS_AccountPanelTypePanicZoneController : BaseTableController<MS_AccountPanelTypePanicZone, MS_AccountPanelTypePanicZoneCollection> { }
	public class MS_AccountPanelTypeController : BaseTableController<MS_AccountPanelType, MS_AccountPanelTypeCollection> { }
	public class MS_AccountPayoutTypeController : BaseTableController<MS_AccountPayoutType, MS_AccountPayoutTypeCollection> { }
	public class MS_AccountController : BaseTableController<MS_Account, MS_AccountCollection> { }
	public class MS_AccountSalesInformationController : BaseTableController<MS_AccountSalesInformation, MS_AccountSalesInformationCollection> { }
	public class MS_AccountSignalFormatTypeController : BaseTableController<MS_AccountSignalFormatType, MS_AccountSignalFormatTypeCollection> { }
	public class MS_AccountSiteGeneralDispatchController : BaseTableController<MS_AccountSiteGeneralDispatch, MS_AccountSiteGeneralDispatchCollection> { }
	public class MS_AccountSiteTypeController : BaseTableController<MS_AccountSiteType, MS_AccountSiteTypeCollection> { }
	public class MS_AccountSubmitAGController : BaseTableController<MS_AccountSubmitAG, MS_AccountSubmitAGCollection> { }
	public class MS_AccountSubmitMController : BaseTableController<MS_AccountSubmitM, MS_AccountSubmitMCollection> { }
	public class MS_AccountSubmitMsXmlController : BaseTableController<MS_AccountSubmitMsXml, MS_AccountSubmitMsXmlCollection> { }
	public class MS_AccountSubmitController : BaseTableController<MS_AccountSubmit, MS_AccountSubmitCollection> { }
	public class MS_AccountSubmitTypeController : BaseTableController<MS_AccountSubmitType, MS_AccountSubmitTypeCollection> { }
	public class MS_AccountSwungInfoController : BaseTableController<MS_AccountSwungInfo, MS_AccountSwungInfoCollection> { }
	public class MS_AccountSystemTypeController : BaseTableController<MS_AccountSystemType, MS_AccountSystemTypeCollection> { }
	public class MS_AccountZoneAssignmentController : BaseTableController<MS_AccountZoneAssignment, MS_AccountZoneAssignmentCollection> { }
	public class MS_AccountZoneTypeController : BaseTableController<MS_AccountZoneType, MS_AccountZoneTypeCollection> { }
	public class MS_AlarmCompanyController : BaseTableController<MS_AlarmCompany, MS_AlarmCompanyCollection> { }
	public class MS_AvantGuardAccountStateController : BaseTableController<MS_AvantGuardAccountState, MS_AvantGuardAccountStateCollection> { }
	public class MS_AvantGuardEventCodeController : BaseTableController<MS_AvantGuardEventCode, MS_AvantGuardEventCodeCollection> { }
	public class MS_AvantGuardRelationController : BaseTableController<MS_AvantGuardRelation, MS_AvantGuardRelationCollection> { }
	public class MS_AvantGuardSystemTypeCodeController : BaseTableController<MS_AvantGuardSystemTypeCode, MS_AvantGuardSystemTypeCodeCollection> { }
	public class MS_AvantGuardTestCategoryController : BaseTableController<MS_AvantGuardTestCategory, MS_AvantGuardTestCategoryCollection> { }
	public class MS_DealerController : BaseTableController<MS_Dealer, MS_DealerCollection> { }
	public class MS_DeviceEventController : BaseTableController<MS_DeviceEvent, MS_DeviceEventCollection> { }
	public class MS_DispatchAgencyController : BaseTableController<MS_DispatchAgency, MS_DispatchAgencyCollection> { }
	public class MS_DispatchAgencyCityZipLookupController : BaseTableController<MS_DispatchAgencyCityZipLookup, MS_DispatchAgencyCityZipLookupCollection> { }
	public class MS_DispatchAgencyCityZipController : BaseTableController<MS_DispatchAgencyCityZip, MS_DispatchAgencyCityZipCollection> { }
	public class MS_DispatchAgencyTypeController : BaseTableController<MS_DispatchAgencyType, MS_DispatchAgencyTypeCollection> { }
	public class MS_EmergencyContactAuthorityController : BaseTableController<MS_EmergencyContactAuthority, MS_EmergencyContactAuthorityCollection> { }
	public class MS_EmergencyContactPhoneTypeController : BaseTableController<MS_EmergencyContactPhoneType, MS_EmergencyContactPhoneTypeCollection> { }
	public class MS_EmergencyContactRelationshipController : BaseTableController<MS_EmergencyContactRelationship, MS_EmergencyContactRelationshipCollection> { }
	public class MS_EmergencyContactController : BaseTableController<MS_EmergencyContact, MS_EmergencyContactCollection> { }
	public class MS_EmergencyContactTypeController : BaseTableController<MS_EmergencyContactType, MS_EmergencyContactTypeCollection> { }
	public class MS_EquipmentAccountZoneTypeEventController : BaseTableController<MS_EquipmentAccountZoneTypeEvent, MS_EquipmentAccountZoneTypeEventCollection> { }
	public class MS_EquipmentAccountZoneTypeController : BaseTableController<MS_EquipmentAccountZoneType, MS_EquipmentAccountZoneTypeCollection> { }
	public class MS_EquipmentCellularVendorController : BaseTableController<MS_EquipmentCellularVendor, MS_EquipmentCellularVendorCollection> { }
	public class MS_EquipmentExistingController : BaseTableController<MS_EquipmentExisting, MS_EquipmentExistingCollection> { }
	public class MS_EquipmentLocationController : BaseTableController<MS_EquipmentLocation, MS_EquipmentLocationCollection> { }
	public class MS_EquipmentMonitoredTypeController : BaseTableController<MS_EquipmentMonitoredType, MS_EquipmentMonitoredTypeCollection> { }
	public class MS_EquipmentMonitronicsCellProviderController : BaseTableController<MS_EquipmentMonitronicsCellProvider, MS_EquipmentMonitronicsCellProviderCollection> { }
	public class MS_EquipmentMonitronicsCellServiceController : BaseTableController<MS_EquipmentMonitronicsCellService, MS_EquipmentMonitronicsCellServiceCollection> { }
	public class MS_EquipmentMonitronicsDeviceController : BaseTableController<MS_EquipmentMonitronicsDevice, MS_EquipmentMonitronicsDeviceCollection> { }
	public class MS_EquipmentMostFrequentController : BaseTableController<MS_EquipmentMostFrequent, MS_EquipmentMostFrequentCollection> { }
	public class MS_EquipmentPanelDefaultZoneController : BaseTableController<MS_EquipmentPanelDefaultZone, MS_EquipmentPanelDefaultZoneCollection> { }
	public class MS_EquipmentPanelTypeController : BaseTableController<MS_EquipmentPanelType, MS_EquipmentPanelTypeCollection> { }
	public class MS_EquipmentController : BaseTableController<MS_Equipment, MS_EquipmentCollection> { }
	public class MS_EquipmentTypeEventTypeController : BaseTableController<MS_EquipmentTypeEventType, MS_EquipmentTypeEventTypeCollection> { }
	public class MS_EquipmentTypeController : BaseTableController<MS_EquipmentType, MS_EquipmentTypeCollection> { }
	public class MS_EquipmentTypesZoneEventTypeController : BaseTableController<MS_EquipmentTypesZoneEventType, MS_EquipmentTypesZoneEventTypeCollection> { }
	public class MS_IndustryAccountController : BaseTableController<MS_IndustryAccount, MS_IndustryAccountCollection> { }
	public class MS_LeadTakeOverController : BaseTableController<MS_LeadTakeOver, MS_LeadTakeOverCollection> { }
	public class MS_MarketController : BaseTableController<MS_Market, MS_MarketCollection> { }
	public class MS_MarketSubConversionController : BaseTableController<MS_MarketSubConversion, MS_MarketSubConversionCollection> { }
	public class MS_MonitoringStationOssController : BaseTableController<MS_MonitoringStationOss, MS_MonitoringStationOssCollection> { }
	public class MS_MonitoringStationController : BaseTableController<MS_MonitoringStation, MS_MonitoringStationCollection> { }
	public class MS_MonitronicsDispatchAgencyController : BaseTableController<MS_MonitronicsDispatchAgency, MS_MonitronicsDispatchAgencyCollection> { }
	public class MS_MonitronicsEntityController : BaseTableController<MS_MonitronicsEntity, MS_MonitronicsEntityCollection> { }
	public class MS_MonitronicsEntityAgencyController : BaseTableController<MS_MonitronicsEntityAgency, MS_MonitronicsEntityAgencyCollection> { }
	public class MS_MonitronicsEntityAgencyTypeController : BaseTableController<MS_MonitronicsEntityAgencyType, MS_MonitronicsEntityAgencyTypeCollection> { }
	public class MS_MonitronicsEntityAuthorityController : BaseTableController<MS_MonitronicsEntityAuthority, MS_MonitronicsEntityAuthorityCollection> { }
	public class MS_MonitronicsEntityBusRuleController : BaseTableController<MS_MonitronicsEntityBusRule, MS_MonitronicsEntityBusRuleCollection> { }
	public class MS_MonitronicsEntityCellProviderController : BaseTableController<MS_MonitronicsEntityCellProvider, MS_MonitronicsEntityCellProviderCollection> { }
	public class MS_MonitronicsEntityCellServiceController : BaseTableController<MS_MonitronicsEntityCellService, MS_MonitronicsEntityCellServiceCollection> { }
	public class MS_MonitronicsEntityContactTypeController : BaseTableController<MS_MonitronicsEntityContactType, MS_MonitronicsEntityContactTypeCollection> { }
	public class MS_MonitronicsEntityContractTypeController : BaseTableController<MS_MonitronicsEntityContractType, MS_MonitronicsEntityContractTypeCollection> { }
	public class MS_MonitronicsEntityEquipEventXRefController : BaseTableController<MS_MonitronicsEntityEquipEventXRef, MS_MonitronicsEntityEquipEventXRefCollection> { }
	public class MS_MonitronicsEntityEquipmentLocationController : BaseTableController<MS_MonitronicsEntityEquipmentLocation, MS_MonitronicsEntityEquipmentLocationCollection> { }
	public class MS_MonitronicsEntityEquipmentTypeController : BaseTableController<MS_MonitronicsEntityEquipmentType, MS_MonitronicsEntityEquipmentTypeCollection> { }
	public class MS_MonitronicsEntityEventCodeController : BaseTableController<MS_MonitronicsEntityEventCode, MS_MonitronicsEntityEventCodeCollection> { }
	public class MS_MonitronicsEntityEventHistoryController : BaseTableController<MS_MonitronicsEntityEventHistory, MS_MonitronicsEntityEventHistoryCollection> { }
	public class MS_MonitronicsEntityEventController : BaseTableController<MS_MonitronicsEntityEvent, MS_MonitronicsEntityEventCollection> { }
	public class MS_MonitronicsEntityLanguageController : BaseTableController<MS_MonitronicsEntityLanguage, MS_MonitronicsEntityLanguageCollection> { }
	public class MS_MonitronicsEntityNamePrefixController : BaseTableController<MS_MonitronicsEntityNamePrefix, MS_MonitronicsEntityNamePrefixCollection> { }
	public class MS_MonitronicsEntityNameSuffixController : BaseTableController<MS_MonitronicsEntityNameSuffix, MS_MonitronicsEntityNameSuffixCollection> { }
	public class MS_MonitronicsEntityOosCatController : BaseTableController<MS_MonitronicsEntityOosCat, MS_MonitronicsEntityOosCatCollection> { }
	public class MS_MonitronicsEntityOptionController : BaseTableController<MS_MonitronicsEntityOption, MS_MonitronicsEntityOptionCollection> { }
	public class MS_MonitronicsEntityPartialBatchController : BaseTableController<MS_MonitronicsEntityPartialBatch, MS_MonitronicsEntityPartialBatchCollection> { }
	public class MS_MonitronicsEntityPermitTypeController : BaseTableController<MS_MonitronicsEntityPermitType, MS_MonitronicsEntityPermitTypeCollection> { }
	public class MS_MonitronicsEntityPhoneTypeController : BaseTableController<MS_MonitronicsEntityPhoneType, MS_MonitronicsEntityPhoneTypeCollection> { }
	public class MS_MonitronicsEntityPrefixController : BaseTableController<MS_MonitronicsEntityPrefix, MS_MonitronicsEntityPrefixCollection> { }
	public class MS_MonitronicsEntityRelationController : BaseTableController<MS_MonitronicsEntityRelation, MS_MonitronicsEntityRelationCollection> { }
	public class MS_MonitronicsEntitySecGroupController : BaseTableController<MS_MonitronicsEntitySecGroup, MS_MonitronicsEntitySecGroupCollection> { }
	public class MS_MonitronicsEntityServiceCompanyController : BaseTableController<MS_MonitronicsEntityServiceCompany, MS_MonitronicsEntityServiceCompanyCollection> { }
	public class MS_MonitronicsEntitySiteOptionController : BaseTableController<MS_MonitronicsEntitySiteOption, MS_MonitronicsEntitySiteOptionCollection> { }
	public class MS_MonitronicsEntitySiteStatController : BaseTableController<MS_MonitronicsEntitySiteStat, MS_MonitronicsEntitySiteStatCollection> { }
	public class MS_MonitronicsEntitySiteSystemOptionController : BaseTableController<MS_MonitronicsEntitySiteSystemOption, MS_MonitronicsEntitySiteSystemOptionCollection> { }
	public class MS_MonitronicsEntitySiteSystemController : BaseTableController<MS_MonitronicsEntitySiteSystem, MS_MonitronicsEntitySiteSystemCollection> { }
	public class MS_MonitronicsEntitySiteTypeController : BaseTableController<MS_MonitronicsEntitySiteType, MS_MonitronicsEntitySiteTypeCollection> { }
	public class MS_MonitronicsEntityStateController : BaseTableController<MS_MonitronicsEntityState, MS_MonitronicsEntityStateCollection> { }
	public class MS_MonitronicsEntitySystemTypeController : BaseTableController<MS_MonitronicsEntitySystemType, MS_MonitronicsEntitySystemTypeCollection> { }
	public class MS_MonitronicsEntitySystemTypeXRefController : BaseTableController<MS_MonitronicsEntitySystemTypeXRef, MS_MonitronicsEntitySystemTypeXRefCollection> { }
	public class MS_MonitronicsEntityTestCatController : BaseTableController<MS_MonitronicsEntityTestCat, MS_MonitronicsEntityTestCatCollection> { }
	public class MS_MonitronicsEntityTwoWayController : BaseTableController<MS_MonitronicsEntityTwoWay, MS_MonitronicsEntityTwoWayCollection> { }
	public class MS_MonitronicsEntityZipController : BaseTableController<MS_MonitronicsEntityZip, MS_MonitronicsEntityZipCollection> { }
	public class MS_MonitronicsEntityZoneStateController : BaseTableController<MS_MonitronicsEntityZoneState, MS_MonitronicsEntityZoneStateCollection> { }
	public class MS_MonitronicsSubmitsGetDataErrorController : BaseTableController<MS_MonitronicsSubmitsGetDataError, MS_MonitronicsSubmitsGetDataErrorCollection> { }
	public class MS_MonitronicsSubmitsGetDataController : BaseTableController<MS_MonitronicsSubmitsGetData, MS_MonitronicsSubmitsGetDataCollection> { }
	public class MS_ReceiverLineAlarmNetController : BaseTableController<MS_ReceiverLineAlarmNet, MS_ReceiverLineAlarmNetCollection> { }
	public class MS_ReceiverLineBlockAlarmComController : BaseTableController<MS_ReceiverLineBlockAlarmCom, MS_ReceiverLineBlockAlarmComCollection> { }
	public class MS_ReceiverLineBlockAlarmComHistoryController : BaseTableController<MS_ReceiverLineBlockAlarmComHistory, MS_ReceiverLineBlockAlarmComHistoryCollection> { }
	public class MS_ReceiverLineBlockAlarmnetController : BaseTableController<MS_ReceiverLineBlockAlarmnet, MS_ReceiverLineBlockAlarmnetCollection> { }
	public class MS_ReceiverLineBlockController : BaseTableController<MS_ReceiverLineBlock, MS_ReceiverLineBlockCollection> { }
	public class MS_ReceiverLineBlockTelguardController : BaseTableController<MS_ReceiverLineBlockTelguard, MS_ReceiverLineBlockTelguardCollection> { }
	public class MS_ReceiverLineController : BaseTableController<MS_ReceiverLine, MS_ReceiverLineCollection> { }
	public class MS_ReceiverLineTypeController : BaseTableController<MS_ReceiverLineType, MS_ReceiverLineTypeCollection> { }
	public class MS_ReceiverLineVendorAlarmComAccountsMapController : BaseTableController<MS_ReceiverLineVendorAlarmComAccountsMap, MS_ReceiverLineVendorAlarmComAccountsMapCollection> { }
	public class MS_TimeZoneLookupController : BaseTableController<MS_TimeZoneLookup, MS_TimeZoneLookupCollection> { }
	public class MS_VendorAlarmComAccountController : BaseTableController<MS_VendorAlarmComAccount, MS_VendorAlarmComAccountCollection> { }
	public class MS_VendorAlarmComAddOnFeatureController : BaseTableController<MS_VendorAlarmComAddOnFeature, MS_VendorAlarmComAddOnFeatureCollection> { }
	public class MS_VendorAlarmComCentralStationForwardingOptionController : BaseTableController<MS_VendorAlarmComCentralStationForwardingOption, MS_VendorAlarmComCentralStationForwardingOptionCollection> { }
	public class MS_VendorAlarmComCheckCoverageController : BaseTableController<MS_VendorAlarmComCheckCoverage, MS_VendorAlarmComCheckCoverageCollection> { }
	public class MS_VendorAlarmComCsEventGroupsToForwardController : BaseTableController<MS_VendorAlarmComCsEventGroupsToForward, MS_VendorAlarmComCsEventGroupsToForwardCollection> { }
	public class MS_VendorAlarmComCustomerNotificationController : BaseTableController<MS_VendorAlarmComCustomerNotification, MS_VendorAlarmComCustomerNotificationCollection> { }
	public class MS_VendorAlarmComPackageController : BaseTableController<MS_VendorAlarmComPackage, MS_VendorAlarmComPackageCollection> { }
	public class MS_VendorAlarmComPanelTypeController : BaseTableController<MS_VendorAlarmComPanelType, MS_VendorAlarmComPanelTypeCollection> { }
	public class MS_VendorAlarmComPanelVersionController : BaseTableController<MS_VendorAlarmComPanelVersion, MS_VendorAlarmComPanelVersionCollection> { }
	public class NM_AccountController : BaseTableController<NM_Account, NM_AccountCollection> { }
	public class PD_ItemController : BaseTableController<PD_Item, PD_ItemCollection> { }
	public class QL_AddressController : BaseTableController<QL_Address, QL_AddressCollection> { }
	public class QL_AddressCoordController : BaseTableController<QL_AddressCoord, QL_AddressCoordCollection> { }
	public class QL_CreditReportBureauController : BaseTableController<QL_CreditReportBureau, QL_CreditReportBureauCollection> { }
	public class QL_CreditReportController : BaseTableController<QL_CreditReport, QL_CreditReportCollection> { }
	public class QL_CreditReportVendorAbaraController : BaseTableController<QL_CreditReportVendorAbara, QL_CreditReportVendorAbaraCollection> { }
	public class QL_CreditReportVendorEasyAccessController : BaseTableController<QL_CreditReportVendorEasyAccess, QL_CreditReportVendorEasyAccessCollection> { }
	public class QL_CreditReportVendorHartSoftwareController : BaseTableController<QL_CreditReportVendorHartSoftware, QL_CreditReportVendorHartSoftwareCollection> { }
	public class QL_CreditReportVendorManualController : BaseTableController<QL_CreditReportVendorManual, QL_CreditReportVendorManualCollection> { }
	public class QL_CreditReportVendorMicrobiltController : BaseTableController<QL_CreditReportVendorMicrobilt, QL_CreditReportVendorMicrobiltCollection> { }
	public class QL_CreditReportVendorController : BaseTableController<QL_CreditReportVendor, QL_CreditReportVendorCollection> { }
	public class QL_CreditScoreGroupController : BaseTableController<QL_CreditScoreGroup, QL_CreditScoreGroupCollection> { }
	public class QL_CreditScoreGroupsByDealersAndSeasonController : BaseTableController<QL_CreditScoreGroupsByDealersAndSeason, QL_CreditScoreGroupsByDealersAndSeasonCollection> { }
	public class QL_CustomerMasterLeadController : BaseTableController<QL_CustomerMasterLead, QL_CustomerMasterLeadCollection> { }
	public class QL_DealerLeadController : BaseTableController<QL_DealerLead, QL_DealerLeadCollection> { }
	public class QL_LeadAddressController : BaseTableController<QL_LeadAddress, QL_LeadAddressCollection> { }
	public class QL_LeadDispositionController : BaseTableController<QL_LeadDisposition, QL_LeadDispositionCollection> { }
	public class QL_LeadProductOfferController : BaseTableController<QL_LeadProductOffer, QL_LeadProductOfferCollection> { }
	public class QL_LeadController : BaseTableController<QL_Lead, QL_LeadCollection> { }
	public class QL_LeadSourceController : BaseTableController<QL_LeadSource, QL_LeadSourceCollection> { }
	public class SAE_AgingController : BaseTableController<SAE_Aging, SAE_AgingCollection> { }
	public class SAE_BillingHistoryController : BaseTableController<SAE_BillingHistory, SAE_BillingHistoryCollection> { }
	public class SAE_BillingInfoSummaryController : BaseTableController<SAE_BillingInfoSummary, SAE_BillingInfoSummaryCollection> { }
	public class SAE_CreditReportAbaraController : BaseTableController<SAE_CreditReportAbara, SAE_CreditReportAbaraCollection> { }
	public class SAE_CreditRportController : BaseTableController<SAE_CreditRport, SAE_CreditRportCollection> { }
	public class SAE_InterimPanelTypeMapController : BaseTableController<SAE_InterimPanelTypeMap, SAE_InterimPanelTypeMapCollection> { }
	public class SAE_TestNumberController : BaseTableController<SAE_TestNumber, SAE_TestNumberCollection> { }
	public class SP_AccountController : BaseTableController<SP_Account, SP_AccountCollection> { }
	public class TS_AppointmentController : BaseTableController<TS_Appointment, TS_AppointmentCollection> { }
	public class TS_ServiceTicketController : BaseTableController<TS_ServiceTicket, TS_ServiceTicketCollection> { }
	public class TS_ServiceTicketSkills_MapController : BaseTableController<TS_ServiceTicketSkills_Map, TS_ServiceTicketSkills_MapCollection> { }
	public class TS_ServiceTypeController : BaseTableController<TS_ServiceType, TS_ServiceTypeCollection> { }
	public class TS_SkillController : BaseTableController<TS_Skill, TS_SkillCollection> { }
	public class TS_StatusCodeController : BaseTableController<TS_StatusCode, TS_StatusCodeCollection> { }
	public class TS_TechController : BaseTableController<TS_Tech, TS_TechCollection> { }
	public class TS_TechSkills_MapController : BaseTableController<TS_TechSkills_Map, TS_TechSkills_MapCollection> { }
	public class TS_TechWeekDayController : BaseTableController<TS_TechWeekDay, TS_TechWeekDayCollection> { }
	public class UI_ActionController : BaseTableController<UI_Action, UI_ActionCollection> { }
	public class UI_ApplicationPermissionController : BaseTableController<UI_ApplicationPermission, UI_ApplicationPermissionCollection> { }
	public class UI_ApplicationController : BaseTableController<UI_Application, UI_ApplicationCollection> { }
	public class UI_ApplicationVersionController : BaseTableController<UI_ApplicationVersion, UI_ApplicationVersionCollection> { }
	public class UI_MenuItemPermissionController : BaseTableController<UI_MenuItemPermission, UI_MenuItemPermissionCollection> { }
	public class UI_MenuItemController : BaseTableController<UI_MenuItem, UI_MenuItemCollection> { }
	public class UI_MenuController : BaseTableController<UI_Menu, UI_MenuCollection> { }
	public class UI_MessageActionParameterController : BaseTableController<UI_MessageActionParameter, UI_MessageActionParameterCollection> { }
	public class UI_MessageActionController : BaseTableController<UI_MessageAction, UI_MessageActionCollection> { }
	public class UI_MessageController : BaseTableController<UI_Message, UI_MessageCollection> { }
	public class UI_PermissionTypeController : BaseTableController<UI_PermissionType, UI_PermissionTypeCollection> { }
	public class UI_UserSettingsContainerController : BaseTableController<UI_UserSettingsContainer, UI_UserSettingsContainerCollection> { }
	public class WF_AccountController : BaseTableController<WF_Account, WF_AccountCollection> { }
	public class ZZTestCreateController : BaseTableController<ZZTestCreate, ZZTestCreateCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class AE_AgingViewController : BaseViewController<AE_AgingView, AE_AgingViewCollection> { }
	public class AE_CustomerAccountInfoToGPViewController : BaseViewController<AE_CustomerAccountInfoToGPView, AE_CustomerAccountInfoToGPViewCollection> { }
	public class AE_CustomerGpsClientsViewController : BaseViewController<AE_CustomerGpsClientsView, AE_CustomerGpsClientsViewCollection> { }
	public class AE_CustomerInformationViewController : BaseViewController<AE_CustomerInformationView, AE_CustomerInformationViewCollection> { }
	public class AE_CustomerMasterFileGeneralViewController : BaseViewController<AE_CustomerMasterFileGeneralView, AE_CustomerMasterFileGeneralViewCollection> { }
	public class AE_CustomerMonitoredPartyViewController : BaseViewController<AE_CustomerMonitoredPartyView, AE_CustomerMonitoredPartyViewCollection> { }
	public class AE_CustomerSWINGViewController : BaseViewController<AE_CustomerSWINGView, AE_CustomerSWINGViewCollection> { }
	public class AE_CustomerSWINGAdd_DncViewController : BaseViewController<AE_CustomerSWINGAdd_DncView, AE_CustomerSWINGAdd_DncViewCollection> { }
	public class AE_CustomerSWINGEmergencyContactViewController : BaseViewController<AE_CustomerSWINGEmergencyContactView, AE_CustomerSWINGEmergencyContactViewCollection> { }
	public class AE_CustomerSWINGEquipmentViewController : BaseViewController<AE_CustomerSWINGEquipmentView, AE_CustomerSWINGEquipmentViewCollection> { }
	public class AE_CustomerSWINGInterimViewController : BaseViewController<AE_CustomerSWINGInterimView, AE_CustomerSWINGInterimViewCollection> { }
	public class AE_CustomerSWINGPremiseAddressViewController : BaseViewController<AE_CustomerSWINGPremiseAddressView, AE_CustomerSWINGPremiseAddressViewCollection> { }
	public class AE_CustomerSWINGSystemDetailViewController : BaseViewController<AE_CustomerSWINGSystemDetailView, AE_CustomerSWINGSystemDetailViewCollection> { }
	public class AE_CustomerSWUNGInfoViewController : BaseViewController<AE_CustomerSWUNGInfoView, AE_CustomerSWUNGInfoViewCollection> { }
	public class AE_GpsClientToCustomerMasterViewController : BaseViewController<AE_GpsClientToCustomerMasterView, AE_GpsClientToCustomerMasterViewCollection> { }
	public class AE_InvoiceItemsViewController : BaseViewController<AE_InvoiceItemsView, AE_InvoiceItemsViewCollection> { }
	public class AE_InvoiceMsInstallInfoViewController : BaseViewController<AE_InvoiceMsInstallInfoView, AE_InvoiceMsInstallInfoViewCollection> { }
	public class AE_PaymentFullViewController : BaseViewController<AE_PaymentFullView, AE_PaymentFullViewCollection> { }
	public class AeCustomersMsPrimaryViewController : BaseViewController<AeCustomersMsPrimaryView, AeCustomersMsPrimaryViewCollection> { }
	public class BX_BarcodesViewController : BaseViewController<BX_BarcodesView, BX_BarcodesViewCollection> { }
	public class BX_BarcodeTypesAMAAndNOCViewController : BaseViewController<BX_BarcodeTypesAMAAndNOCView, BX_BarcodeTypesAMAAndNOCViewCollection> { }
	public class BX_DocumentFieldsAMNXS001ViewController : BaseViewController<BX_DocumentFieldsAMNXS001View, BX_DocumentFieldsAMNXS001ViewCollection> { }
	public class BX_DocumentFieldsSONXS001ViewController : BaseViewController<BX_DocumentFieldsSONXS001View, BX_DocumentFieldsSONXS001ViewCollection> { }
	public class IE_LocationViewController : BaseViewController<IE_LocationView, IE_LocationViewCollection> { }
	public class IE_PackingSlipViewController : BaseViewController<IE_PackingSlipView, IE_PackingSlipViewCollection> { }
	public class IE_ProductBarcodeLocationViewController : BaseViewController<IE_ProductBarcodeLocationView, IE_ProductBarcodeLocationViewCollection> { }
	public class IE_ProductBarcodeTrackingViewController : BaseViewController<IE_ProductBarcodeTrackingView, IE_ProductBarcodeTrackingViewCollection> { }
	public class IE_PurchaseOrderItemsViewController : BaseViewController<IE_PurchaseOrderItemsView, IE_PurchaseOrderItemsViewCollection> { }
	public class MC_AccountNotesAllInfoViewController : BaseViewController<MC_AccountNotesAllInfoView, MC_AccountNotesAllInfoViewCollection> { }
	public class MC_AddressesViewController : BaseViewController<MC_AddressesView, MC_AddressesViewCollection> { }
	public class MC_AddressesMsPremiseViewController : BaseViewController<MC_AddressesMsPremiseView, MC_AddressesMsPremiseViewCollection> { }
	public class MS_AccountAndLeadInfoViewController : BaseViewController<MS_AccountAndLeadInfoView, MS_AccountAndLeadInfoViewCollection> { }
	public class MS_AccountClientDetailsViewController : BaseViewController<MS_AccountClientDetailsView, MS_AccountClientDetailsViewCollection> { }
	public class MS_AccountClientsViewController : BaseViewController<MS_AccountClientsView, MS_AccountClientsViewCollection> { }
	public class MS_AccountCreditsAndInstallsViewController : BaseViewController<MS_AccountCreditsAndInstallsView, MS_AccountCreditsAndInstallsViewCollection> { }
	public class MS_AccountDispatchAgencyAssignmentViewController : BaseViewController<MS_AccountDispatchAgencyAssignmentView, MS_AccountDispatchAgencyAssignmentViewCollection> { }
	public class MS_AccountEquipmentInfoToGPViewController : BaseViewController<MS_AccountEquipmentInfoToGPView, MS_AccountEquipmentInfoToGPViewCollection> { }
	public class MS_AccountEquipmentsViewController : BaseViewController<MS_AccountEquipmentsView, MS_AccountEquipmentsViewCollection> { }
	public class MS_AccountEquipmentsAllViewController : BaseViewController<MS_AccountEquipmentsAllView, MS_AccountEquipmentsAllViewCollection> { }
	public class MS_AccountEventViewController : BaseViewController<MS_AccountEventView, MS_AccountEventViewCollection> { }
	public class MS_AccountMonitorInformationsViewController : BaseViewController<MS_AccountMonitorInformationsView, MS_AccountMonitorInformationsViewCollection> { }
	public class MS_AccountOnlineStatusInfoViewController : BaseViewController<MS_AccountOnlineStatusInfoView, MS_AccountOnlineStatusInfoViewCollection> { }
	public class MS_AccountSalesInformationsViewController : BaseViewController<MS_AccountSalesInformationsView, MS_AccountSalesInformationsViewCollection> { }
	public class MS_DeviceEventsViewController : BaseViewController<MS_DeviceEventsView, MS_DeviceEventsViewCollection> { }
	public class MS_DispatchAgenciesViewController : BaseViewController<MS_DispatchAgenciesView, MS_DispatchAgenciesViewCollection> { }
	public class MS_EquipmentAccountZoneTypeEventsViewController : BaseViewController<MS_EquipmentAccountZoneTypeEventsView, MS_EquipmentAccountZoneTypeEventsViewCollection> { }
	public class MS_EquipmentAccountZoneTypesViewController : BaseViewController<MS_EquipmentAccountZoneTypesView, MS_EquipmentAccountZoneTypesViewCollection> { }
	public class MS_EquipmentLocationsViewController : BaseViewController<MS_EquipmentLocationsView, MS_EquipmentLocationsViewCollection> { }
	public class MS_EquipmentsViewController : BaseViewController<MS_EquipmentsView, MS_EquipmentsViewCollection> { }
	public class MS_IndustryAccountNumbersViewController : BaseViewController<MS_IndustryAccountNumbersView, MS_IndustryAccountNumbersViewCollection> { }
	public class MS_IndustryAccountNumbersWithReceiverLineInfoViewController : BaseViewController<MS_IndustryAccountNumbersWithReceiverLineInfoView, MS_IndustryAccountNumbersWithReceiverLineInfoViewCollection> { }
	public class MS_IndustryNumberByCallerIdViewController : BaseViewController<MS_IndustryNumberByCallerIdView, MS_IndustryNumberByCallerIdViewCollection> { }
	public class MS_LeadTakeOversViewController : BaseViewController<MS_LeadTakeOversView, MS_LeadTakeOversViewCollection> { }
	public class MS_MonitronicsEntitySystemTypeXRefViewController : BaseViewController<MS_MonitronicsEntitySystemTypeXRefView, MS_MonitronicsEntitySystemTypeXRefViewCollection> { }
	public class MS_ReceiverBlockCellDeviceInfoViewController : BaseViewController<MS_ReceiverBlockCellDeviceInfoView, MS_ReceiverBlockCellDeviceInfoViewCollection> { }
	public class QL_CreditReportTransactionAndTokenViewController : BaseViewController<QL_CreditReportTransactionAndTokenView, QL_CreditReportTransactionAndTokenViewCollection> { }
	public class QL_LeadBasicInfoViewController : BaseViewController<QL_LeadBasicInfoView, QL_LeadBasicInfoViewCollection> { }
	public class QL_LeadProductOffersViewController : BaseViewController<QL_LeadProductOffersView, QL_LeadProductOffersViewCollection> { }
	public class QL_LeadSearchResultViewController : BaseViewController<QL_LeadSearchResultView, QL_LeadSearchResultViewCollection> { }
	public class QL_QualifyCustomerInfoViewController : BaseViewController<QL_QualifyCustomerInfoView, QL_QualifyCustomerInfoViewCollection> { }
	public class RandNumberViewController : BaseViewController<RandNumberView, RandNumberViewCollection> { }
	public class SAE_BillingHistoryViewController : BaseViewController<SAE_BillingHistoryView, SAE_BillingHistoryViewCollection> { }
	public class SAE_BillingInfoSummaryViewController : BaseViewController<SAE_BillingInfoSummaryView, SAE_BillingInfoSummaryViewCollection> { }
	public class SE_AccountTicketsViewController : BaseViewController<SE_AccountTicketsView, SE_AccountTicketsViewCollection> { }
	public class SE_ScheduleBlocksViewController : BaseViewController<SE_ScheduleBlocksView, SE_ScheduleBlocksViewCollection> { }
	public class SE_ScheduleBlockTicketsViewController : BaseViewController<SE_ScheduleBlockTicketsView, SE_ScheduleBlockTicketsViewCollection> { }
	public class SE_TechnicianScheduleBlocksViewController : BaseViewController<SE_TechnicianScheduleBlocksView, SE_TechnicianScheduleBlocksViewCollection> { }
	public class SE_TicketsViewController : BaseViewController<SE_TicketsView, SE_TicketsViewCollection> { }
	public class TS_ServiceTicketStatusViewController : BaseViewController<TS_ServiceTicketStatusView, TS_ServiceTicketStatusViewCollection> { }
	public class TS_TeamViewController : BaseViewController<TS_TeamView, TS_TeamViewCollection> { }
	public class TS_TechViewController : BaseViewController<TS_TechView, TS_TechViewCollection> { }
	public class UI_ApplicationMenuViewController : BaseViewController<UI_ApplicationMenuView, UI_ApplicationMenuViewCollection> { }
	public class UI_ApplicationVersionsViewController : BaseViewController<UI_ApplicationVersionsView, UI_ApplicationVersionsViewCollection> { }
	public class UI_ApplicationVersionsCurrentVersionsViewController : BaseViewController<UI_ApplicationVersionsCurrentVersionsView, UI_ApplicationVersionsCurrentVersionsViewCollection> { }
	public class UI_MenuItemsExpandedPermissionsViewController : BaseViewController<UI_MenuItemsExpandedPermissionsView, UI_MenuItemsExpandedPermissionsViewCollection> { }
	public class UI_MenusCurrentMenusViewController : BaseViewController<UI_MenusCurrentMenusView, UI_MenusCurrentMenusViewCollection> { }

	#endregion //View Controllers
}
