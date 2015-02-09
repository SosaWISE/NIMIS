USE [WISE_CRM]
GO
/**********************************************************************************************************************
* DESCRIPTION:  This script is for tie the Customer to an MS_Account.
**********************************************************************************************************************/
--SELECT [CustomerGpsClientID]
--      ,[CustomerID]
--      ,[AuthUserId]
--      ,[Username]
--      ,[Password]
--      ,[LastLoginOn]
--      ,[IsActive]
--      ,[IsDeleted]
--      ,[ModifiedOn]
--      ,[ModifiedBy]
--      ,[CreatedOn]
--      ,[CreatedBy]
--      ,[DEX_ROW_TS]
--  FROM [WISE_CRM].[dbo].[AE_CustomerGpsClients]
--GO

SELECT 
	MIA.IndustryAccountID
	, MIA.ReceiverLineBlockId
	, MAC.AccountID
	, MAC.SystemTypeId
	, MAC.PanelTypeId
	, MAC.SimProductBarcodeId
	, MAC.GpsWatchProductBarcodeId
	, MAC.GpsWatchPhoneNumber
	, ACA.AccountId
	, ACA.CustomerId	
FROM
	dbo.MS_IndustryAccounts AS MIA WITH (NOLOCK)
	INNER JOIN dbo.MS_Accounts AS MAC WITH (NOLOCK)
	ON
		(MIA.AccountId = MAC.AccountID)
	INNER JOIN dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK)
	ON
		(MAC.AccountID = ACA.AccountId)
	INNER JOIN dbo.AE_Customers AS CUST WITH (NOLOCK)
	ON
		(ACA.CustomerId = CUST.CustomerID)
WHERE
	(CSid = '5135107528')
	
/** Tie a Customer Account to an MS account. */
BEGIN TRANSACTION

DECLARE @CustomerID BIGINT;
SET @CustomerID = 100168;
DECLARE @AccountID BIGINT;
SET @AccountID = 100169;

SELECT * FROM dbo.AE_Customers WHERE CustomerID = @CustomerID

--INSERT INTO dbo.AE_CustomerAccounts (CustomerId, AccountId) VALUES  ( @CustomerID, @AccountID);

INSERT INTO dbo.AE_CustomerGpsClients (CustomerID, AuthUserId, Username, [Password]) VALUES (@CustomerID, 10000, 'SosaWISE', 'jugete');

ROLLBACK TRANSACTION
