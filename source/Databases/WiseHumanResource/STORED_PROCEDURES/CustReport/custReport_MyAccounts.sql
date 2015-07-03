USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_MyAccounts')
	BEGIN
		PRINT 'Dropping Procedure custReport_MyAccounts'
		DROP  Procedure  dbo.custReport_MyAccounts
	END
GO

PRINT 'Creating Procedure custReport_MyAccounts'
GO
/******************************************************************************
**		File: custReport_MyAccounts.sql
**		Name: custReport_MyAccounts
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 07/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/01/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_MyAccounts
(
	@officeId INT
	, @salesRepId VARCHAR(50) = NULL
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN

	--SELECT
	--	CGP.CustomerMasterFileID AS CMFID
	--	, CGP.PrimaryCustomerName AS CustomerName
	--	, ADR.StreetAddress
	--	, CASE
	--		WHEN MASCL.SubmitAccountOnline IS NOT NULL THEN 'ACCT ONLINE'
	--		WHEN MASCL.InitialPayment IS NOT NULL THEN 'PAYMNT TAKEN'
	--		WHEN MASCL.PostSurvey IS NOT NULL THEN 'POST SURVEY'
	--		WHEN MASCL.TechInspection IS NOT NULL THEN 'TECH INSP PASSED'
	--		WHEN MASCL.SystemTest IS NOT NULL THEN 'SYS TEST PASSED'
	--		WHEN MASCL.RegisterCell IS NOT NULL THEN 'CELL REGISTERED'
	--		WHEN MASCL.SystemDetails IS NOT NULL THEN 'SYS DETAILS ENTERING'
	--		WHEN MASCL.EmergencyContacts IS NOT NULL THEN 'EMC ENTERING'
	--		WHEN MASCL.IndustryNumbers IS NOT NULL THEN 'ISSUED CSID'
	--		WHEN MASCL.PreSurvey IS NOT NULL THEN 'PRE SURVEY'
	--		WHEN MASCL.SalesInfo IS NOT NULL THEN 'SALES INFO FILLED'
	--		WHEN MASCL.Qualify IS NOT NULL THEN 'QUALIFIED'
	--		WHEN CGP.InstallDATE IS NOT NULL THEN 'INSTALLED'
	--		ELSE 'LEAD' 
	--	  END AS [Status]
	--	, CASE
	--		WHEN MASCL.SubmitAccountOnline IS NOT NULL THEN MASCL.SubmitAccountOnline
	--		WHEN MASCL.InitialPayment IS NOT NULL THEN MASCL.InitialPayment
	--		WHEN MASCL.PostSurvey IS NOT NULL THEN MASCL.PostSurvey
	--		WHEN MASCL.TechInspection IS NOT NULL THEN MASCL.TechInspection
	--		WHEN MASCL.SystemTest IS NOT NULL THEN MASCL.SystemTest
	--		WHEN MASCL.RegisterCell IS NOT NULL THEN MASCL.RegisterCell
	--		WHEN MASCL.SystemDetails IS NOT NULL THEN MASCL.SystemDetails
	--		WHEN MASCL.EmergencyContacts IS NOT NULL THEN MASCL.EmergencyContacts
	--		WHEN MASCL.IndustryNumbers IS NOT NULL THEN MASCL.IndustryNumbers
	--		WHEN MASCL.PreSurvey IS NOT NULL THEN MASCL.PreSurvey
	--		WHEN MASCL.SalesInfo IS NOT NULL THEN MASCL.SalesInfo
	--		WHEN MASCL.Qualify IS NOT NULL THEN MASCL.Qualify
	--		WHEN CGP.InstallDATE IS NOT NULL THEN CGP.InstallDATE
	--	  END AS [Date]
	--	, dbo.fxRU_SeasonCreditGroup(CGP.SeasonId, CGP.CreditScore) AS Qualify
	--	, [WISE_CRM].[dbo].fxGetMS_AccountHoldsCountByAccountId(CGP.AccountID) AS [Holds]
	--	, CGP.ActivationFee AS SetupFee
	--	, CGP.RMR AS MMR
	--FROM
	--	[WISE_CRM].[dbo].[vwAE_CustomerAccountInfoToGP] AS CGP
	--	INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
	--	ON
	--		(MSA.AccountID = CGP.AccountID)
	--	INNER JOIN [WISE_CRM].[dbo].[MC_Addresses] AS ADR WITH (NOLOCK)
	--	ON
	--		(ADR.AddressID = MSA.PremiseAddressId)
	--	INNER JOIN [WISE_CRM].[dbo].MS_AccountSetupCheckLists AS MASCL WITH (NOLOCK)
	--	ON
	--		(MASCL.AccountID = MSA.AccountID)
	--WHERE
	--	(@salesRepId IS NULL OR CGP.SalesRepID = @salesRepId)
	--	AND ((CONVERT(DATE, MASCL.SubmitAccountOnline) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.InitialPayment) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.PostSurvey) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.TechInspection) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.SystemTest) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.RegisterCell) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.SystemDetails) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.EmergencyContacts) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.IndustryNumbers) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.PreSurvey) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.SalesInfo) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, MASCL.Qualify) BETWEEN @startDate AND @endDate)
	--		OR (CONVERT(DATE, CGP.InstallDATE) BETWEEN @startDate AND @endDate));

	SELECT DISTINCT
		AECA.CustomerMasterFileID AS CMFID
		, AEC.FirstName + ' ' + AEC.LastName AS [CustomerName]
		, MSASI.SalesRepId
		, ADR.StreetAddress
		, CASE
			WHEN MASCL.SubmitAccountOnline IS NOT NULL THEN 'ACCT ONLINE'
			WHEN MASCL.InitialPayment IS NOT NULL THEN 'PAYMNT TAKEN'
			WHEN MASCL.PostSurvey IS NOT NULL THEN 'POST SURVEY'
			WHEN MASCL.TechInspection IS NOT NULL THEN 'TECH INSP PASSED'
			WHEN MASCL.SystemTest IS NOT NULL THEN 'SYS TEST PASSED'
			WHEN MASCL.RegisterCell IS NOT NULL THEN 'CELL REGISTERED'
			WHEN MASCL.SystemDetails IS NOT NULL THEN 'SYS DETAILS ENTERING'
			WHEN MASCL.EmergencyContacts IS NOT NULL THEN 'EMC ENTERING'
			WHEN MASCL.IndustryNumbers IS NOT NULL THEN 'ISSUED CSID'
			WHEN MASCL.PreSurvey IS NOT NULL THEN 'PRE SURVEY'
			WHEN MASCL.SalesInfo IS NOT NULL THEN 'SALES INFO FILLED'
			WHEN MASCL.Qualify IS NOT NULL THEN 'QUALIFIED'
			WHEN MSASI.InstallDATE IS NOT NULL THEN 'INSTALLED'
			ELSE 'LEAD' 
		  END AS [Status]
		, CASE
			WHEN MASCL.SubmitAccountOnline IS NOT NULL THEN MASCL.SubmitAccountOnline
			WHEN MASCL.InitialPayment IS NOT NULL THEN MASCL.InitialPayment
			WHEN MASCL.PostSurvey IS NOT NULL THEN MASCL.PostSurvey
			WHEN MASCL.TechInspection IS NOT NULL THEN MASCL.TechInspection
			WHEN MASCL.SystemTest IS NOT NULL THEN MASCL.SystemTest
			WHEN MASCL.RegisterCell IS NOT NULL THEN MASCL.RegisterCell
			WHEN MASCL.SystemDetails IS NOT NULL THEN MASCL.SystemDetails
			WHEN MASCL.EmergencyContacts IS NOT NULL THEN MASCL.EmergencyContacts
			WHEN MASCL.IndustryNumbers IS NOT NULL THEN MASCL.IndustryNumbers
			WHEN MASCL.PreSurvey IS NOT NULL THEN MASCL.PreSurvey
			WHEN MASCL.SalesInfo IS NOT NULL THEN MASCL.SalesInfo
			WHEN MASCL.Qualify IS NOT NULL THEN MASCL.Qualify
			WHEN MSASI.InstallDATE IS NOT NULL THEN MSASI.InstallDATE
		  END AS [Date]
		, dbo.fxRU_SeasonCreditGroup(MSASI.SeasonId, [WISE_CRM].dbo.fxQlCreditReportGetScoreByMsAccountID(MSASI.AccountID)) AS Qualify
		, [WISE_CRM].[dbo].fxGetMS_AccountHoldsCountByAccountId(MSASI.AccountID) AS [Holds]
		, MSASI.SetupFee
		, MSASI.MMR
	FROM
		[WISE_CRM].[dbo].[vwMS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[vwAE_CustomerAccounts] AS AECA
		ON
			(AECA.AccountId = MSASI.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
--		[WISE_CRM].[dbo].[vwAE_CustomerAccountInfoToGP] AS CGP
		INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
--			(MSA.AccountID = CGP.AccountID)
			(MSA.AccountID = MSASI.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[MC_Addresses] AS ADR WITH (NOLOCK)
		ON
			(ADR.AddressID = MSA.PremiseAddressId)
		INNER JOIN [WISE_CRM].[dbo].MS_AccountSetupCheckLists AS MASCL WITH (NOLOCK)
		ON
			(MASCL.AccountID = MSA.AccountID)
	WHERE
		(@salesRepId IS NULL OR MSASI.SalesRepID = @salesRepId)
		AND ((CONVERT(DATE, MASCL.SubmitAccountOnline) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.InitialPayment) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.PostSurvey) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.TechInspection) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.SystemTest) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.RegisterCell) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.SystemDetails) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.EmergencyContacts) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.IndustryNumbers) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.PreSurvey) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.SalesInfo) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MASCL.Qualify) BETWEEN @startDate AND @endDate)
			OR (CONVERT(DATE, MSASI.InstallDATE) BETWEEN @startDate AND @endDate));



END
GO

GRANT EXEC ON dbo.custReport_MyAccounts TO PUBLIC
GO

/*
*/

EXEC dbo.custReport_MyAccounts @officeId=0, @endDate='2015-06-01 05:00:00', @startDate='1/2/2003'

