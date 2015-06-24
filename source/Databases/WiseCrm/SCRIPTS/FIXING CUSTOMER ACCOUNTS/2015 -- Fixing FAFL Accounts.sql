USE WISE_CRM

DECLARE @custTable TABLE (CustomerMasterFileID BIGINT, AccountID BIGINT, LeadID BIGINT, CustomerID BIGINT, AddressID BIGINT);
DECLARE @SalesRepID VARCHAR(50) = 'FLFA000'
	, @DealerID INT = 5003;

INSERT INTO @custTable (CustomerMasterFileID, AccountID)
SELECT CustomerMasterFileID, AccountID FROM dbo.vwAE_CustomerAccountInfoToGP WHERE (CustomerMasterFileID IN (3091634,3091633,3091640,3091641,3091649,3091664,3091666,3091667));

UPDATE cTB SET
	LeadID = AECA.LeadId
	, CustomerID = AECA.CustomerId
	, AddressID = AECA.AddressId
FROM
	@custTable AS cTB
	INNER JOIN dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = cTB.AccountID);

--SELECT DISTINCT AccountId, LeadId, CustomerId FROM dbo.AE_CustomerAccounts WHERE AccountId IN (SELECT AccountID FROM @custTable);

BEGIN TRANSACTION

UPDATE QL SET
	DealerID = @DealerID
	, SalesRepId = @SalesRepID
FROM
	dbo.QL_Leads AS QL WITH (NOLOCK)
	INNER JOIN @custTable AS cTB
	ON
		(cTB.LeadID = QL.LeadID)

UPDATE QLA SET
	DealerId = @DealerID
FROM
	dbo.QL_Address AS QLA WITH (NOLOCK)
	INNER JOIN dbo.QL_Leads AS QL WITH (NOLOCK)
	ON
		(QL.AddressId = QLA.AddressID)
	INNER JOIN @custTable AS cTB
	ON
		(cTB.LeadID = QL.LeadID)

UPDATE AEC SET
	DealerId = @DealerID
--	, SalesRepId = @SalesRepID
FROM
	dbo.AE_Customers AS AEC WITH (NOLOCK)
	INNER JOIN @custTable AS CTB
	ON
		(CTB.CustomerID = AEC.CustomerID);

UPDATE MCADR SET
	DealerId = @DealerID
FROM
	MC_Addresses AS MCADR WITH (NOLOCK)
	INNER JOIN @custTable 
	ON
		([@custTable].AddressID = MCADR.AddressID);

COMMIT TRANSACTION
