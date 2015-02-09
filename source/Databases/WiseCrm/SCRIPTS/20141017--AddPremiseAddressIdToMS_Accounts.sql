USE [WISE_CRM]
GO

BEGIN TRANSACTION

UPDATE [dbo].[MS_Accounts] SET 
	PremiseAddressId = PADR.AddressID
FROM 
	[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
	INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS PADR WITH (NOLOCK)
	ON 
		(PADR.AccountId = MSA.AccountID)

--SELECT * FROM MS_Accounts WHERE PremiseAddressId IS NULL AND CreatedOn > '06/01/2014';
--SELECT * FROM [dbo].[MS_AccountSalesInformations]

ROLLBACK TRANSACTION