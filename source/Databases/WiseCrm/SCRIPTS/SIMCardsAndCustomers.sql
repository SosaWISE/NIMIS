USE [WISE_CRM]
GO

SELECT 
	ACT.AccountID
	, ACT.SimProductBarcodeId
	, CAC.CustomerAccountID
	, CAC.CustomerId
	, CUST.FirstName
	, CUST.LastName
	, CUST.CreatedOn
FROM
	dbo.MS_Accounts AS ACT WITH (NOLOCK)
	LEFT OUTER JOIN dbo.AE_CustomerAccounts AS CAC WITH (NOLOCK)
	ON
		(ACT.AccountID = CAC.AccountId)
	LEFT OUTER JOIN dbo.AE_Customers AS CUST WITH (NOLOCK)
	ON
		(CAC.CustomerId = CUST.CustomerID)
WHERE
--	(ACT.GpsWatchPhoneNumber IN ('13132401588','13132402915','13132402965','13133584540','13133751699','13134159941','13134159967'));
	(ACT.SimProductBarcodeId IS NOT NULL) AND (ACT.SimProductBarcodeId LIKE '8901%')

--SELECT
--	*
--FROM
--	dbo.AE_Dealers AS DLR WITH (NOLOCK);