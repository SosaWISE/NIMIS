/** WISE CRM Clean up script for the new database. */
USE [WISE_CRM]
GO

BEGIN TRANSACTION

DELETE [dbo].[AE_CustomerAccounts];
DBCC CHECKIDENT ('[dbo].[AE_CustomerAccounts]', RESEED, 0);
DELETE [dbo].[MS_AccountCustomers];
DBCC CHECKIDENT ('[dbo].[MS_AccountCustomers]', RESEED, 0);
DELETE [dbo].[MC_AccountNotes];
DBCC CHECKIDENT ('[dbo].[MC_AccountNotes]', RESEED, 0);

DELETE [dbo].[AE_InvoiceItems];
DBCC CHECKIDENT ('[dbo].[AE_InvoiceItems]', RESEED, 10000000);

UPDATE [IE_ProductBarcodes] SET LastProductBarcodeTrackingId = NULL;
DELETE [dbo].[IE_ProductBarcodeTracking];
DELETE [dbo].[IE_ProductBarcodes];

DELETE [dbo].[AE_DealerPurchaseOrderItems];
DELETE [dbo].[AE_DealerPurchaseOrders];

DELETE [dbo].[AE_CustomerMasterToCustomer];
DELETE [dbo].[AE_BillingCustomers];
DELETE [dbo].[AE_CustomerGpsClients];
DBCC CHECKIDENT ('[dbo].[AE_CustomerAddress]', RESEED, 0);
DELETE [dbo].[MS_EmergencyContacts];
DBCC CHECKIDENT ('[dbo].[MS_EmergencyContacts]', RESEED, 0);

DELETE [dbo].[AE_CustomerAddress];
DBCC CHECKIDENT ('[dbo].[AE_CustomerAddress]', RESEED, 0);
UPDATE [dbo].[MC_Accounts] SET ShipContactId = NULL;
DELETE [dbo].[AE_Customers];
DBCC CHECKIDENT ('[dbo].[AE_Customers]', RESEED, 100100);

DELETE [dbo].[AE_InvoiceItems];
DBCC CHECKIDENT ('[dbo].[AE_Invoices]', RESEED, 10000000);

DELETE [dbo].[AE_InvoicePaymentJoins];

DELETE [dbo].[MG_AuthorizeNetTransactions];
DELETE [dbo].[MG_Transactions];

DELETE [dbo].[AE_Invoices];
DBCC CHECKIDENT ('[dbo].[AE_Invoices]', RESEED, 10000000);

UPDATE [MS_Accounts] SET IndustryAccountId = NULL;
DELETE [dbo].[MS_IndustryAccounts];
DBCC CHECKIDENT ('[dbo].[MS_IndustryAccounts]', RESEED, 0);

DELETE [dbo].[MS_DeviceEvents];
DBCC CHECKIDENT ('[dbo].[MS_DeviceEvents]', RESEED, 0);
DELETE [dbo].[MS_AccountZoneAssignments];
DBCC CHECKIDENT ('[dbo].[MS_AccountZoneAssignments]', RESEED, 0);
DELETE [dbo].[MS_AccountEquipment];
DBCC CHECKIDENT ('[dbo].[MS_AccountEquipment]', RESEED, 0);
DELETE [dbo].[MS_AccountSubmits];
DBCC CHECKIDENT ('[dbo].[MS_AccountSubmits]', RESEED, 0);

DELETE [dbo].[MS_Accounts];

DELETE [dbo].[SAE_BillingHistory];
DBCC CHECKIDENT ('[dbo].[SAE_BillingHistory]', RESEED, 0);
DELETE [dbo].[SAE_BillingInfoSummary];
DBCC CHECKIDENT ('[dbo].[SAE_BillingInfoSummary]', RESEED, 0);
DELETE [dbo].[AE_Payments];
DBCC CHECKIDENT ('[dbo].[AE_Payments]', RESEED, 0);

DELETE [dbo].[MC_Accounts];
DBCC CHECKIDENT ('[dbo].[MC_Accounts]', RESEED, 1000);

DELETE [dbo].[AE_Customers]; 
DBCC CHECKIDENT ('[dbo].[AE_Customers]', RESEED, 100000);

DELETE [dbo].[QL_LeadProductOffers];
DBCC CHECKIDENT ('[dbo].[QL_LeadProductOffers]', RESEED, 0);
DELETE [dbo].[QL_CustomerMasterLeads];

UPDATE [dbo].[QL_CreditReportVendorAbara] SET CreditReportId = NULL;
UPDATE [dbo].[QL_CreditReports] SET CreditReportVendorAbaraId = NULL, CreditReportVendorMicrobiltId = NULL, CreditReportVendorManualId = NULL, CreditReportVendorEasyAccessId = NULL;
DELETE [dbo].[QL_CreditReportVendorMicrobilt];
DBCC CHECKIDENT ('[dbo].[QL_CreditReportVendorMicrobilt]', RESEED, 0);
DELETE [dbo].[QL_CreditReportVendorManual];
DBCC CHECKIDENT ('[dbo].[QL_CreditReportVendorManual]', RESEED, 0);
DELETE [dbo].[QL_CreditReportVendorAbara];
DBCC CHECKIDENT ('[dbo].[QL_CreditReportVendorAbara]', RESEED, 0);
DELETE [dbo].[QL_CreditReports];
DBCC CHECKIDENT ('[dbo].[QL_CreditReports]', RESEED, 0);
DELETE [dbo].[QL_LeadAddress];
DBCC CHECKIDENT ('[dbo].[QL_LeadAddress]', RESEED, 0);
DELETE [dbo].[QL_Leads];
DBCC CHECKIDENT ('[dbo].[QL_Leads]', RESEED, 1000000);

DELETE [dbo].[AE_Contracts];

DELETE SAE_Aging;
DELETE [dbo].[AE_CustomerMasterFiles];
DBCC CHECKIDENT ('[dbo].[AE_CustomerMasterFiles]', RESEED, 1000000);

/** RESET DEALERS. */
DELETE [dbo].[MC_Addresses];
DBCC CHECKIDENT ('[dbo].[MC_Addresses]', RESEED, 0);
DELETE [dbo].[QL_Address];
DBCC CHECKIDENT ('[dbo].[QL_Address]', RESEED, 0);
DELETE [dbo].[MC_DealerUsers];
DELETE [dbo].[QL_LeadSources] WHERE (DealerID <> 5000) AND (DealerID IS NOT NULL);
DELETE [dbo].[AE_Dealers] WHERE (DealerID <> 5000);
DBCC CHECKIDENT ('[dbo].[AE_Dealers]', RESEED, 5000);

DELETE [dbo].[BE_Barcodes];
DBCC CHECKIDENT ('[dbo].[BE_Barcodes]', RESEED, 0);

DELETE [dbo].[BX_Barcodes];
DBCC CHECKIDENT ('[dbo].[BX_Barcodes]', RESEED, 0);

DELETE [dbo].[IE_PurchaseOrderItems];
DBCC CHECKIDENT ('[dbo].[IE_PurchaseOrderItems]', RESEED, 1002100);
DELETE [dbo].[IE_PurchaseOrders];
DBCC CHECKIDENT ('[dbo].[IE_PurchaseOrders]', RESEED, 3300);
DELETE [dbo].[IE_Vendors];

DELETE [dbo].[IE_ReturnToManufacturerItems];
DELETE [dbo].[IE_ReturnToManufacturers];
DELETE [dbo].[IE_Vendors];
DELETE [dbo].[IE_WarehouseSites];

DELETE [dbo].[IS_Accounts];
DELETE [dbo].[LL_Accounts];

DELETE [dbo].MS_IndustryAccounts;
DELETE [dbo].MS_ReceiverLineBlockAlarmCom;
DELETE [dbo].MS_ReceiverLineBlockAlarmnet;
DELETE [dbo].MS_ReceiverLineAlarmNets;
DELETE [dbo].MS_ReceiverLineBlocks;

ROLLBACK TRANSACTION

/** TRUNCATE LOGS AND DATA. */
ALTER DATABASE [WISE_CRM] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_CRM_log, 2)
ALTER DATABASE [WISE_CRM] SET RECOVERY FULL WITH NO_WAIT
GO
