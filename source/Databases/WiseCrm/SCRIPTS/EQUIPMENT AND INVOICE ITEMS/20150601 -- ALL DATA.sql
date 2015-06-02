USE [WISE_CRM]
GO

--DECLARE @AccountID BIGINT = 191260;
DECLARE @AccountID BIGINT = 191262;

SELECT * FROM dbo.MS_AccountSalesInformations WHERE (AccountID = @AccountID);
SELECT * FROM dbo.MS_Accounts WHERE (AccountID = @AccountID);
SELECT * FROM dbo.AE_Contracts WHERE (ContractID = (SELECT ContractId FROM dbo.MS_Accounts WHERE (AccountID = @AccountID)));
SELECT * FROM dbo.AE_Invoices WHERE (InvoiceTypeId = 'INSTALL' AND (AccountId = (SELECT AccountID FROM dbo.MS_Accounts WHERE (AccountID = @AccountID))));
SELECT * FROM dbo.AE_InvoiceItems WHERE (InvoiceId = (SELECT InvoiceID FROM dbo.AE_Invoices WHERE (InvoiceTypeId = 'INSTALL' AND (AccountId = (SELECT AccountID FROM dbo.MS_Accounts WHERE (AccountID = @AccountID))))));
SELECT * FROM dbo.MS_AccountEquipment WHERE (AccountID = @AccountID);