USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_PendingInstalls')
	BEGIN
		PRINT 'Dropping Procedure custReport_PendingInstalls'
		DROP  Procedure  dbo.custReport_PendingInstalls
	END
GO

PRINT 'Creating Procedure custReport_PendingInstalls'
GO
/******************************************************************************
**		File: custReport_PendingInstalls.sql
**		Name: custReport_PendingInstalls
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
**		Date: 07/23/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/23/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_PendingInstalls
(
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
	, @preSurvey BIT = NULL
)
AS
BEGIN
	/** INITIALIZATION */
	DECLARE @Contacts INT
		, @Qualifications INT
		, @Installations INT
		, @OfficeName VARCHAR(50)

	PRINT 'OfficeID: ' + CAST(@officeId AS VARCHAR(20));
	--SET @startDate = '1/1/2013';
	--SET @endDate = '7/24/2015';

	IF (@preSurvey IS NOT NULL AND @preSurvey = 'TRUE')
	BEGIN
		SELECT
			AEC.CustomerMasterFileId AS [CustomerNumber]
			, MSASC.AccountID
			, AEC.FirstName + ' ' + AEC.LastName AS CustomerName
			, MSASC.Qualify
			, MSASC.SalesInfo
			, MSASC.PreSurvey
			, MSASC.IndustryNumbers
			, MSASC.EmergencyContacts
			, MSASC.SystemDetails
			, MSASC.RegisterCell
			, MSASC.SystemTest
			, MSASC.TechInspection
			, MSASC.PostSurvey
			, MSASC.InitialPayment
			, MSASC.SubmitAccountOnline
		FROM 
			[WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASC WITH (NOLOCK)
			INNER JOIN [WISE_CRM].[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
			ON
				(MSASI.AccountID = MSASC.AccountID)
			INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
			ON
				(AECA.AccountId = MSASC.AccountID)
				AND (AECA.CustomerTypeId = 'PRI')
			INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
			ON
				(AEC.CustomerID = AECA.CustomerId)
		WHERE
			(MSASC.PreSurvey IS NOT NULL)
			AND (MSASC.PreSurvey BETWEEN @startDate AND @endDate)
			AND ((MSASC.SubmitAccountOnline IS NULL) OR (MSASI.InstallDate IS NULL));
	END
	ELSE
	BEGIN
		SELECT
			AEC.CustomerMasterFileId AS [CustomerNumber]
			, MSASC.AccountID
			, AEC.FirstName + ' ' + AEC.LastName AS CustomerName
			, MSASC.Qualify
			, MSASC.SalesInfo
			, MSASC.PreSurvey
			, MSASC.IndustryNumbers
			, MSASC.EmergencyContacts
			, MSASC.SystemDetails
			, MSASC.RegisterCell
			, MSASC.SystemTest
			, MSASC.TechInspection
			, MSASC.PostSurvey
			, MSASC.InitialPayment
			, MSASC.SubmitAccountOnline
		FROM 
			[WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASC WITH (NOLOCK)
			INNER JOIN [WISE_CRM].[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
			ON
				(MSASI.AccountID = MSASC.AccountID)
			INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
			ON
				(AECA.AccountId = MSASC.AccountID)
				AND (AECA.CustomerTypeId = 'PRI')
			INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
			ON
				(AEC.CustomerID = AECA.CustomerId)
		WHERE
			(MSASC.PostSurvey IS NOT NULL)
			AND (MSASC.PostSurvey BETWEEN @startDate AND @endDate)
			AND ((MSASC.SubmitAccountOnline IS NULL) OR (MSASI.InstallDate IS NULL));
	END
END
GO

GRANT EXEC ON dbo.custReport_PendingInstalls TO PUBLIC
GO

/*

*/

EXEC dbo.custReport_PendingInstalls '', NULL, NULL, '1/1/2013', '2015-08-01 05:00:00', 1

