USE [Platinum_Protection_InterimCRM]
GO

SELECT 
	COUNT(*)
FROM
	(SELECT [AccountID]
		, ADR.StateID
		  ,[PremisePhone]
		  , CASE
				WHEN [PremisePhone] IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList([PremisePhone])
				ELSE NULL
			END AS [Premise In]
		  ,[AlternativePhone]
		  , CASE
				WHEN [AlternativePhone] IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList([AlternativePhone])
				ELSE NULL
			END AS [Alt In]
		  ,[AlternativePhoneTypeId]
		  ,[WorkPhone]
		  , CASE
				WHEN [WorkPhone] IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList([WorkPhone])
				ELSE NULL
			END AS [Work In]
	FROM
		[dbo].[MS_Account] AS MAS WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].[MC_Address] AS ADR WITH (NOLOCK)
		ON
			(MAS.PremiseAddressID = ADR.AddressID)
	WHERE
		(ADR.StateID = 1)) AS DNC
WHERE
	(DNC.[Premise In] = 0 OR DNC.[Alt In] = 0 OR DNC.[Work In] = 0);

SELECT
	COUNT(*)
FROM
	(SELECT 
		LED.LeadID
		, LED.PhoneHome
		, CASE
			WHEN LED.PhoneHome IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList(LED.PhoneHome)
			ELSE NULL
		  END AS [PhoneHome In]
		, LED.PhoneWork
		, CASE
			WHEN LED.PhoneWork IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList(LED.PhoneWork)
			ELSE NULL
		  END AS [PhoneWork In]
		, LED.PhoneCell
		, CASE
			WHEN LED.PhoneCell IS NOT NULL THEN [dbo].fxGetPhoneNumberIsInDNCList(LED.PhoneCell)
			ELSE NULL
		  END AS [PhoneCell In]
	FROM
		[dbo].[MC_Lead] AS LED WITH (NOLOCK)
	WHERE
		(LED.LeadID NOT IN (SELECT Customer1ID FROM [dbo].[MS_Account]))) AS DNC
WHERE
	(DNC.[PhoneHome In] = 0 OR DNC.[PhoneWork In] = 0 OR DNC.[PhoneCell In] = 0);