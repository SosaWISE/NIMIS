USE [Platinum_Protection_InterimCRM]
GO

SELECT TOP 300
--	MSA.AccountID
	MSA.AccountID
	, CUST1.FirstName
	, CUST1.LastName
	, CUST1.DOB
	, MSA.PremisePhone
	, ADR.StreetAddress
	, ADR.StreetAddress2
	, ADR.City
	, STA.StateAB
	, ADR.PostalCode
	, MSAS.InstallDate
FROM
	[dbo].[MS_Account] AS MSA WITH (NOLOCK)
	INNER JOIN [dbo].[MC_Lead] AS CUST1
	ON
		(CUST1.LeadID = MSA.Customer1ID)
	INNER JOIN [dbo].[MC_Address] AS ADR
	ON
		(ADR.AddressID = CUST1.PremiseAddressID)
	INNER JOIN [dbo].[MC_PoliticalState] AS STA
	ON
		(STA.StateID = ADR.StateID)
	INNER JOIN [dbo].[MS_AccountStatus] AS MSAS WITH (NOLOCK)
	ON
		(MSAS.AccountID = MSA.AccountID)
WHERE
	(STA.StateAB = 'FL')
ORDER BY
	MSAS.InstallDate DESC;