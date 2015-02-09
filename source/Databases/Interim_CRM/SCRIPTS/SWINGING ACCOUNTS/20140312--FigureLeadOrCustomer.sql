USE [Platinum_Protection_InterimCRM]
GO

SELECT TOP 10
	MAS.AccountID
	, CASE 
		WHEN MASS.IsQualifyCustomerComplete = 1 THEN 'Yes'
		ELSE 'No'
	  END AS [Passed Credit]
	, CASE
		WHEN MASS.IsAccountSetupComplete = 1 THEN 'Yes'
		ELSE 'No'
	  END AS [Full Customer]
	, CASE
		WHEN MASS.IsQualifyCustomerComplete = 1 AND MASS.IsAccountSetupComplete = 1 THEN 'Full Customer'
		WHEN MASS.IsQualifyCustomerComplete = 1 AND MASS.IsAccountSetupComplete = 0 THEN 'Passed Credit'
		ELSE 'Failed Credit'
	  END AS [Decision]
	--, MAS.Customer1ID
	--, MAS.Customer2ID
FROM
	[dbo].[MS_Account] AS MAS WITH (NOLOCK)
	LEFT OUTER JOIN [dbo].[MS_AccountSetupStatus] AS MASS WITH (NOLOCK)
	ON
		(MAS.AccountID = MASS.AccountID);