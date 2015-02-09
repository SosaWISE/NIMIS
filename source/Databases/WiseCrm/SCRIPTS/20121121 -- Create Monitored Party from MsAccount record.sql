USE [WISE_CRM]
GO

/** Local Declarations */
DECLARE @AccountID INT = 100169;

SELECT * FROM dbo.MS_Accounts WHERE AccountID = @AccountID;
SELECT 
	* 
FROM 
	dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK)
	INNER JOIN AE_Customers AS CUST WITH (NOLOCK)
	ON
		(ACA.CustomerId = CUST.CustomerID);
		
DECLARE @CustomerID AS BIGINT = 100195;
SELECT * FROM AE_Customers WHERE (CustomerID = @CustomerID);

BEGIN TRANSACTION
--INSERT INTO dbo.AE_CustomerAccounts (CustomerId, AccountId) VALUES  (@CustomerID, @AccountID);
UPDATE AE_Customers SET CustomerTypeId = 'GPSCLNT', Username= 'andres', Password='andres2012' WHERE CustomerID = @CustomerID;
COMMIT TRANSACTION

SELECT * FROM dbo.AE_Customers WHERE CustomerMasterFileID = 3000035;
