--USE [WISE_CRM]
--GO

DECLARE @DealersTable TABLE (DealerID INT);
INSERT INTO @DealersTable (DealerID) SELECT DealerID FROM dbo.AE_Dealers WHERE (DealerID NOT IN (5000));
--SELECT * FROM @DealersTable;

/**
DealerId IN (SELECT * FROM @DealersTable)
*/

BEGIN TRANSACTION

UPDATE dbo.AE_CustomerMasterFiles SET DealerId = 5016 WHERE CustomerMasterFileID = 3000042;
DELETE dbo.QL_LeadProductOffers WHERE LeadId IN (SELECT LeadId FROM dbo.QL_Leads WHERE LeadID IN (SELECT LeadId FROM dbo.MC_AccountNotes WHERE CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable))));
DELETE dbo.MC_AccountNotes WHERE CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable));
DELETE dbo.QL_Leads WHERE LeadID IN (SELECT LeadId FROM dbo.MC_AccountNotes WHERE CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)));

DELETE dbo.AE_CustomerMasterToCustomer WHERE (CustomerId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));
DELETE dbo.AE_BillingCustomers WHERE (CustomerId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));
DELETE dbo.AE_CustomerAccounts WHERE (CustomerId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));
DELETE dbo.AE_CustomerAddress WHERE (CustomerId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));
DELETE dbo.AE_CustomerGpsClients WHERE (CustomerID IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));
DELETE dbo.MS_AccountCustomersOLD WHERE (CustomerID IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));

DELETE dbo.AE_InvoiceItems WHERE(InvoiceID IN (SELECT InvoiceId FROM dbo.AE_Invoices WHERE (AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))))))));
DELETE dbo.AE_Invoices WHERE (AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))))));

DELETE dbo.SAE_BillingInfoSummary WHERE(AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))))));

DELETE dbo.MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))))));


DELETE dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))))));

DELETE dbo.AE_Customers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)))));
DELETE dbo.QL_Leads WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileID FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)));
--UPDATE dbo.AE_CustomerMasterFiles SET DealerId = 5016 WHERE CustomerMasterFileID = 3000042;

DELETE dbo.SAE_BillingInfoSummary WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileId FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)));
DELETE dbo.AE_CustomerMasterToCustomer WHERE (CustomerMasterFileId IN (SELECT CustomerMasterFileId FROM dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable)));
DELETE dbo.AE_CustomerMasterFiles WHERE DealerId IN (SELECT * FROM @DealersTable);

DELETE dbo.MC_DealerUsers WHERE DealerID IN (SELECT * FROM @DealersTable);

DECLARE @AccountID BIGINT;
SELECT TOP 1 @AccountID = AccountID FROM dbo.MS_Accounts ORDER BY AccountID DESC;
UPDATE dbo.MS_IndustryAccounts SET AccountId = @AccountID WHERE (AccountID IN (SELECT AccountId FROM dbo.MS_IndustryAccounts WHERE (AccountId IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));

DELETE dbo.MS_IndustryAccounts WHERE (AccountId IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
DELETE dbo.MS_EmergencyContacts WHERE (AccountId IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));

--SELECT * FROM dbo.MS_EmergencyContacts WHERE (AccountId IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
--SELECT * FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

--SELECT * FROM dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (AccountID IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));
DELETE dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (AccountID IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));
--SELECT * FROM dbo.MS_AccountEquipment WHERE (AccountID IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
DELETE dbo.MS_AccountEquipment WHERE (AccountID IN (SELECT AccountId FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
--SELECT * FROM dbo.MS_AccountSalesInformations WHERE (AccountID IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
DELETE dbo.MS_AccountSalesInformations WHERE (AccountID IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));

DELETE dbo.MS_Accounts WHERE (PremiseAddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

--SELECT * FROM dbo.AE_CustomerAddress WHERE (AddressId IN (SELECT AddressId FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));
DELETE dbo.AE_CustomerAddress WHERE (AddressId IN (SELECT AddressId FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

DELETE dbo.MS_AccountCustomersOLD WHERE (AccountId IN (SELECT AccountId FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));
DELETE dbo.AE_CustomerAccounts WHERE (AccountId IN (SELECT AccountId FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));
DELETE dbo.SAE_BillingInfoSummary WHERE (AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));
DELETE dbo.AE_InvoiceItems WHERE (InvoiceId IN (SELECT InvoiceID FROM dbo.AE_Invoices WHERE (AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))))));
DELETE dbo.AE_Invoices WHERE (AccountId IN (SELECT AccountID FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))))));

--SELECT * FROM dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
DELETE dbo.MC_Accounts WHERE (ShipContactId IN (SELECT CustomerID FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));

--SELECT * FROM dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));
DELETE dbo.AE_Customers WHERE (AddressId IN (SELECT AddressID FROM dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

DELETE dbo.MC_Addresses WHERE (QlAddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))));

--SELECT * FROM dbo.QL_LeadAddress WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))));
DELETE dbo.QL_LeadAddress WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))));

DELETE dbo.QL_CustomerMasterLeads WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

DECLARE @CreditReportVendorAbaraId BIGINT;
SELECT TOP 1 @CreditReportVendorAbaraId = CreditReportVendorAbaraId FROM dbo.QL_CreditReports ORDER BY CreditReportID DESC;
UPDATE dbo.QL_CreditReports SET CreditReportVendorAbaraId = @CreditReportVendorAbaraId WHERE (CreditReportID IN (SELECT CreditReportID FROM dbo.QL_CreditReports WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
--SELECT * FROM dbo.QL_CreditReportVendorAbara WHERE (CreditReportId IN (SELECT CreditReportID FROM dbo.QL_CreditReports WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));
DELETE dbo.QL_CreditReportVendorAbara WHERE (CreditReportId IN (SELECT CreditReportID FROM dbo.QL_CreditReports WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))))));

DELETE dbo.QL_CreditReports WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

--SELECT * FROM dbo.QL_LeadProductOffers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));
DELETE dbo.QL_LeadProductOffers WHERE (LeadId IN (SELECT LeadID FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))))));

--SELECT * FROM dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))));
DELETE dbo.QL_Leads WHERE (AddressId IN (SELECT AddressID FROM dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable))));

DELETE dbo.QL_Address WHERE (DealerId IN (SELECT * FROM @DealersTable));

DELETE dbo.QL_LeadSources WHERE (DealerId IN (SELECT * FROM @DealersTable));

SELECT * FROM dbo.AE_Dealers;
--DELETE dbo.AE_Dealers WHERE DealerID IN (SELECT * FROM @DealersTable);
--ROLLBACK TRANSACTION
COMMIT TRANSACTION