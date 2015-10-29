USE [WISE_CRM]
GO

--BEGIN TRANSACTION

--/** DELETE MISC Tables ...*/
--DELETE [dbo].[MS_AccountSwungInfo];
--DELETE [dbo].[AE_BillingCustomers];


--/** Delete all MC_Accounts data.*/
--/** ** Delete all Invoice items and CC Transaction tables*/
--DELETE [dbo].[MS_AccountZoneAssignments];
--UPDATE [dbo].[MS_AccountEquipment] SET InvoiceItemId = NULL;
--DELETE [dbo].[AE_InvoiceItems];
--DELETE [dbo].[MS_AccountEquipment];
--DELETE [dbo].[AE_InvoicePaymentJoins];
--DELETE [dbo].[MG_AuthorizeNetTransactions];
--DELETE [dbo].[MG_Transactions];
--DELETE [dbo].[AE_Invoices];

--/** ** Delete all MS_Accounts data */
--/** ** ** Delete all submit to CS data. */
--DELETE [dbo].[MS_AccountSubmitAGs];
--DELETE [dbo].[MS_AccountSubmitMsXmls];
--DELETE [dbo].[MS_AccountSubmitMs];
--DELETE [dbo].[MS_AccountSalesInformations];
--DELETE [dbo].[MS_AccountSubmits];
--UPDATE [dbo].[MS_Accounts] SET IndustryAccountId = NULL, IndustryAccount2Id = NULL;
--/** ** ** Delete Dispatch Agencies stuff */
--DELETE [dbo].[MS_AccountDispatchAgencyAssignments];
--/** ** ** Delete Cellular stuff */
--DELETE [dbo].[MS_AccountCellularADCRegisters];
--DELETE [dbo].[MS_AccountCellularSubmits]
--DELETE [dbo].[MS_IndustryAccounts];
--/** ** ** Central station stuff. */
--DELETE [dbo].[MS_DeviceEvents];
--DELETE [dbo].[MS_EmergencyContacts];
--DELETE [dbo].[MS_AccountAG];
--DELETE dbo.MS_AvantGuardAccountState;
--/** ** ** Service Tickets... */
--DELETE [dbo].[TS_Teams];
--DELETE [dbo].[TS_ServiceTicketSkills_Map];
--UPDATE [dbo].[TS_ServiceTickets] SET CurrentAppointmentId = NULL;
--DELETE [dbo].[TS_Appointments];
--DELETE [dbo].[TS_ServiceTickets];
--/** ** ** Account Holds and stuff*/
--DELETE [dbo].[MS_AccountHolds];
--DELETE [dbo].[MS_AccountHoldCatg2];
--DELETE [dbo].[MS_AccountHoldCatg1];
--DELETE [dbo].[MS_Accounts];
--DELETE [dbo].[MS_AccountCustomersOLD];
--DELETE [dbo].[SAE_BillingInfoSummary];
--DELETE [dbo].[AE_CustomerAccounts];
--DELETE [dbo].[AE_Payments];
--DELETE [dbo].[MC_Accounts];

--/** DELETING ALL CUSTOMER DATA. */
--DELETE [dbo].[AE_CustomerGpsClients];
--DELETE [dbo].[AE_CustomerAddress];
--DELETE [dbo].[AE_BillingCustomers];
--DELETE [dbo].[MC_AccountNotes];
--DELETE [dbo].[AE_CustomerMasterToCustomer];
--DELETE [dbo].[AE_CustomerAccounts];
--DELETE [dbo].AE_DealerPurchaseOrderItems;
--DELETE [dbo].AE_DealerPurchaseOrders;
--DELETE [dbo].AE_Customers;

--/** Delete all dealer information */
--DELETE [dbo].[MC_DealerUsers];
--DELETE [dbo].[QL_LeadProductOffers];
--DELETE [dbo].[QL_LeadAddress];
--DELETE [dbo].[QL_CustomerMasterLeads];
--/** ** Delete Credit Reports. */
--UPDATE [dbo].[QL_CreditReports] SET CreditReportVendorAbaraId = NULL, CreditReportVendorMicrobiltId = NULL, CreditReportVendorEasyAccessId = NULL, CreditReportVendorHartSoftwareId = NULL, CreditReportVendorManualId = NULL;
--DELETE [dbo].[QL_CreditReportVendorAbara];
--DELETE [dbo].[QL_CreditReportVendorEasyAccess];
--DELETE [dbo].[QL_CreditReportVendorHartSoftware];
--DELETE [dbo].[QL_CreditReportVendorMicrobilt];
--DELETE [dbo].[QL_CreditReportVendorManual];
--DELETE [dbo].[QL_CreditReports];
--DELETE [dbo].[QL_Leads];
--DELETE [dbo].[MC_Addresses];
--DELETE [dbo].[QL_Address];
--DELETE [dbo].[QL_LeadSources];
--/** ** Receiver Lines... */
--DELETE [dbo].[MS_ReceiverLineBlockAlarmComHistory];
--DELETE [dbo].[MS_ReceiverLineBlockAlarmCom];
--DELETE [dbo].[MS_ReceiverLineBlockAlarmnet];
--DELETE [dbo].[MS_ReceiverLineBlockTelguard];
--DELETE [dbo].[MS_ReceiverLineBlocks];
--DELETE [dbo].[MS_ReceiverLineAlarmNets];
--DELETE [dbo].[MS_ReceiverLines];
--DELETE [dbo].[SAE_Aging];
--DELETE [dbo].[SAE_BillingHistory]
--DELETE [dbo].[AE_CustomerMasterFiles];
--DELETE [dbo].[AE_InvoiceTemplateItems];
--DELETE [dbo].[AE_InvoiceTemplates];
--DELETE [dbo].[QL_LeadDispositions];
--DELETE [dbo].[MS_Dealers];
--DELETE [dbo].[AE_Dealers];

--DELETE [dbo].[AE_Contracts];

--/** Delete MISC tables */
--DELETE [dbo].[IE_PackingSlipItems];
--DELETE [dbo].[IE_PackingSlips];
--UPDATE [dbo].[IE_ProductBarcodes] SET LastProductBarcodeTrackingId = NULL;
--DELETE [dbo].[IE_ProductBarcodeTracking];
--DELETE [dbo].[IE_ProductBarcodes];
--DELETE [dbo].[IE_PurchaseOrderItems];
--DELETE [dbo].[IE_PurchaseOrders];

--PRINT 'DELETING VERTICAL ACCOUNTS...'
--DELETE [dbo].[GS_Accounts];
--DELETE [dbo].[IS_Accounts];
--DELETE [dbo].[LL_Accounts];
--DELETE [dbo].[NM_Accounts];
--DELETE [dbo].[SP_Accounts];
--DELETE [dbo].[WF_Accounts];


--/** DELETE MISC Tables */
--DELETE [dbo].[DC_PhoneNumbers];
--DELETE [dbo].[DC_CompanyPhoneNumbers];
--DELETE [dbo].[DC_AreaCodes];
--DELETE [dbo].[BE_BundleItems];
--DELETE [dbo].[BE_BundleAccounts];
--DELETE [dbo].[MS_MonitronicsEntityEventHistories];

----SELECT * FROM  [dbo].[SE_ZipCodes];
--DELETE [dbo].[SAE_CreditReportAbara];
--DELETE [dbo].[SAE_CreditRports];
--DELETE [dbo].[BE_Bundles];
--DELETE [dbo].[MS_MonitronicsSubmitsGetDatas];

BEGIN TRANSACTION
/** Equipment SKUS's */
DELETE [dbo].[MS_EquipmentAccountZoneTypeEvents];
DELETE [dbo].[MS_EquipmentAccountZoneTypes];
DELETE [dbo].[MS_EquipmentMonitronicsCellServices];
--SELECT * FROM [dbo].[MS_EquipmentMonitronicsCellServices];
--DELETE [dbo].[MS_EquipmentCellularVendors];

--SELECT * FROM dbo.MS_EquipmentCellularVendors;
DELETE [dbo].[MS_EquipmentCellularVendors];

--SELECT * FROM [dbo].[MS_EquipmentExistings];
DELETE [dbo].[MS_EquipmentExistings];

SELECT * FROM [dbo].[MS_EquipmentMonitronicsDevices];

DELETE [dbo].[MS_Equipments] WHERE EquipmentID NOT LIKE 'CELL_SRV%';
DELETE [dbo].[AE_Items];

ROLLBACK TRANSACTION

