USE [WISE_CRM]
GO

SELECT
	AEC.CustomerMasterFileId,
	MSAC.AccountCustomerID ,
	MSAC.AccountCustomerTypeId ,
	MSAC.LeadId ,
	MSAC.CustomerId ,
	MSAC.AccountId,
	AECA.CustomerAccountID ,
	AECA.LeadId ,
	AECA.AccountId ,
	AECA.CustomerId ,
	AECA.CustomerTypeId ,
	AEC.CreatedOn
FROM
	dbo.MS_AccountCustomers AS MSAC WITH (NOLOCK)
	LEFT OUTER JOIN AE_CustomerAccounts AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = MSAC.AccountId)
		AND (AECA.CustomerId = MSAC.CustomerId)
		AND (AECA.LeadId = MSAC.LeadId)
		AND (AECA.CustomerTypeId = MSAC.AccountCustomerTypeId)
	INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = MSAC.CustomerId)
WHERE
	(AECA.LeadId IS NULL)
	OR (AECA.AccountId IS NULL)
	OR (AECA.CustomerId IS NULL)
ORDER BY 
	AEC.CreatedOn DESC;

--SELECT  CustomerAccountID ,
--        LeadId ,
--        AccountId ,
--        CustomerId ,
--        CustomerTypeId
--FROM dbo.AE_CustomerAccounts ORDER BY CustomerId;


BEGIN TRANSACTION

INSERT INTO dbo.AE_CustomerAccounts (LeadId, AccountId, CustomerId, CustomerTypeId)
SELECT
	--AEC.CustomerMasterFileId,
	--MSAC.AccountCustomerID ,
	MSAC.LeadId ,
	MSAC.AccountId,
	MSAC.CustomerId ,
	MSAC.AccountCustomerTypeId 
	--AECA.CustomerAccountID ,
	--AECA.LeadId ,
	--AECA.AccountId ,
	--AECA.CustomerId ,
	--AECA.CustomerTypeId ,
	--AEC.CreatedOn
FROM
	dbo.MS_AccountCustomers AS MSAC WITH (NOLOCK)
	LEFT OUTER JOIN AE_CustomerAccounts AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = MSAC.AccountId)
		AND (AECA.CustomerId = MSAC.CustomerId)
		AND (AECA.LeadId = MSAC.LeadId)
		AND (AECA.CustomerTypeId = MSAC.AccountCustomerTypeId)
	INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = MSAC.CustomerId)
WHERE
	(AECA.LeadId IS NULL)
	OR (AECA.AccountId IS NULL)
	OR (AECA.CustomerId IS NULL)

ROLLBACK TRANSACTION