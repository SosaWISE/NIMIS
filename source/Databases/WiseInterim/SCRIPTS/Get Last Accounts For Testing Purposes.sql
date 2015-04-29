/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 
	 MSA.AccountID
	 , MCL.FirstName
	 , MCL.LastName
	 , MCL.DOB
	 , MCA.StreetAddress
	 , MCA.StreetAddress2
	 , MCA.City
	 , MCA.StateID
	 , MCPS.StateAB
	 , MCA.PostalCode
	 , MSA.PremisePhone
  FROM 
	[Platinum_Protection_InterimCRM].[dbo].[MS_Account] AS MSA WITH (NOLOCK)
	INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_AccountStatus] AS MSAS WITH (NOLOCK)
	ON
		(MSAS.AccountID = MSA.AccountID)
	INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MC_Lead] AS MCL WITH (NOLOCK)
	ON
		(MCL.LeadID = MSA.Customer1ID)
	INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MC_Address] AS MCA WITH (NOLOCK)
	ON
		(MCA.AddressID = MCL.CreditReportAddressID)
	INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalState] AS MCPS WITH (NOLOCK)
	ON
		(MCPS.StateID = MCA.StateID)
  ORDER BY
	MSAS.InstallDate DESC;