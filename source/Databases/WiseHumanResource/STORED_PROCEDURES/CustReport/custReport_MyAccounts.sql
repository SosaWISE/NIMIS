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
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN
	/** INITIALIZE */
	IF (@officeId = 0)
	BEGIN
		SET @officeId = NULL;
	END
	
	/** EXECUTION */
	SELECT DISTINCT
		AECA.CustomerMasterFileID AS CMFID
		, MSASI.TeamLocationId
		, RT.Description AS [OfficeName]
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
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
		ON
			(RT.TeamLocationID = MSASI.TeamLocationId)
	WHERE
		(@salesRepId IS NULL OR MSASI.SalesRepID = @salesRepId)
		AND (@OfficeId IS NULL OR MSASI.TeamLocationId = @OfficeId)
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

EXEC dbo.custReport_MyAccounts 0, 'SOSAA001', '2013-02-01 00:00:00', '2015-08-06 05:00:00'

