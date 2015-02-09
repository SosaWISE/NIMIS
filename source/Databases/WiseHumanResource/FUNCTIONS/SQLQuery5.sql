USE [WISE_CRM]
GO

SELECT
	*
FROM
	dbo.MS_AccountCustomers

SELECT 
	* 
FROM 
	[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
	INNER JOIN [dbo].[MS_AccountCustomers] AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountId = MSA.AccountID)
		AND (MAC.AccountCustomerTypeId = 'PRI')
	INNER JOIN [dbo].AE_CustomerAddress AS ACA WITH (NOLOCK)
	ON
		(ACA.CustomerId = MAC.CustomerId)
		AND (ACA.CustomerAddressTypeId = 'PRI')
	INNER JOIN [dbo].MC_Addresses AS MCA WITH (NOLOCK)
	ON
		(MCA.AddressID = ACA.AddressId)