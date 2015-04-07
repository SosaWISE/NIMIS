USE [WISE_CRM]
GO

DECLARE @StartDate DATETIME = DATEADD(hour, 6, '03/22/2015')
	, @EndDate DATETIME = DATEADD(HOUR, 6, '04/04/2015');

SELECT DISTINCT
	AEC.CustomerMasterFileId AS [Customer #]
	, MSASI.AccountID AS MsAccountID
	, AEC.FirstName
	, AEC.LastName
	, AEC.PhoneHome AS [Home Phone]
	, AEC.PhoneMobile AS [Mobile Phone]
	, AEC.PhoneWork AS [Work Phone]
	, DATEADD(HOUR, -6, MSASI.InstallDate) AS [Install Date]
	, MCD.Phone AS [Premise Phone]
	, MSASI.AccountSubmitId AS [Nexsense Confirm #]
	, MSASI.CsConfirmationNumber
	, MSIA.Csid
FROM
	[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
	INNER JOIN [dbo].[vwMS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = AECA.AccountId)
	INNER JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
	INNER JOIN [dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
	ON
		(MSA.AccountID = AECA.AccountId)
	INNER JOIN [dbo].[MC_Addresses] AS MCD WITH (NOLOCK)
	ON
		(MCD.AddressID = MSA.PremiseAddressId)
	INNER JOIN [dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
	ON
		(MSIA.IndustryAccountID = MSA.IndustryAccountId)
WHERE
	(MSASI.InstallDate BETWEEN @StartDate AND @EndDate)