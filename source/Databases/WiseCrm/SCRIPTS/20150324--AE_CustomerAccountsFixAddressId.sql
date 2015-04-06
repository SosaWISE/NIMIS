USE [WISE_CRM]
GO

BEGIN TRANSACTION

UPDATE dbo.AE_CustomerAccounts SET
	AddressId = AEC.AddressId
--SELECT
--	AECA.*
--	, AEC.AddressId
FROM
	dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK)
	INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
WHERE
	(AECA.AddressId IS NULL)

ROLLBACK TRANSACTION