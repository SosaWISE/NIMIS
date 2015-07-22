USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxRepts_InstallsByRepIdOfficeIdAll')
	BEGIN
		PRINT 'Dropping FUNCTION fxRepts_InstallsByRepIdOfficeIdAll'
		DROP FUNCTION  dbo.fxRepts_InstallsByRepIdOfficeIdAll
	END
GO

PRINT 'Creating FUNCTION fxRepts_InstallsByRepIdOfficeIdAll'
GO
/******************************************************************************
**		File: fxRepts_InstallsByRepIdOfficeIdAll.sql
**		Name: fxRepts_InstallsByRepIdOfficeIdAll
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 07/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/21/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRepts_InstallsByRepIdOfficeIdAll
(
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
)
RETURNS 
@ParsedList table
(
	--[CustomerMasterFileID] [bigint] NOT NULL
	--, [AccountId] [bigint] NOT NULL
	[OfficeId] [INT] NOT NULL
	, [Term] [INT] NULL
	, [CloseRate] [INT] NULL
	, [SetupFee] [MONEY] NULL
	, [1stMonth] [MONEY] NULL
	, [Over3Months] [MONEY] NULL
	, [PackageSoldId] [INT] NULL
)
AS
BEGIN
	-- EXECUTE 
	INSERT INTO @ParsedList (
		--CustomerMasterFileID
		--, AccountId
		OfficeId
		, Term
		, CloseRate
		, SetupFee
		, [1stMonth]
		, Over3Months
		, PackageSoldId
	)
	SELECT
		--AEC.CustomerMasterFileId
		--, MSASI.AccountID
		MSASI.TeamLocationId AS OfficeId
		, AVG(MSASI.ContractLength) AS Term
		, 32 AS [CloseRate]
		, AVG(MSASI.SetupFee) AS [SetupFee]
		, AVG(MSASI.SetupFee1stMonth) AS [1stMonth]
		, COUNT(MSASI.Over3Months) AS [Over3Months]
		, COUNT(MSASI.AccountPackageId) AS [PackageSoldId]
	FROM
		[WISE_CRM].[dbo].vwMS_AccountSalesInformations AS MSASI WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASCL WITH (NOLOCK)
		ON
			(MSASCL.AccountID = MSASI.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		ON
			(AECA.AccountId = MSASI.AccountID)
			AND (AECA.CustomerTypeId = 'PRI')
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
	WHERE
		((MSASCL.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) OR (MSASI.InstallDate BETWEEN @startDate AND @endDate))
		AND (@dealerId IS NULL OR (MSASI.DealerId = @DealerId))
		AND (@officeId IS NULL OR (MSASI.TeamLocationId = @officeId))
		AND (@SalesRepID IS NULL OR (MSASI.SalesRepId = @SalesRepID))
	GROUP BY
		--AEC.CustomerMasterFileId
		--, MSASI.AccountID
		MSASI.TeamLocationId;

	RETURN;
END
GO

/** Execute */
SELECT * FROM [dbo].fxRepts_InstallsByRepIdOfficeIdAll(NULL, NULL, NULL, '7/9/2015', '7/18/2015');