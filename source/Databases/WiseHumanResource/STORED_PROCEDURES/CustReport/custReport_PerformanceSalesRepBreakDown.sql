USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_PerformanceSalesRepBreakDown')
	BEGIN
		PRINT 'Dropping Procedure custReport_PerformanceSalesRepBreakDown'
		DROP  Procedure  dbo.custReport_PerformanceSalesRepBreakDown
	END
GO

PRINT 'Creating Procedure custReport_PerformanceSalesRepBreakDown'
GO
/******************************************************************************
**		File: custReport_PerformanceSalesRepBreakDown.sql
**		Name: custReport_PerformanceSalesRepBreakDown
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
**		Date: 08/03/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/03/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_PerformanceSalesRepBreakDown
(
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN
	/** INITIALIZATION */
	DECLARE @RepName VARCHAR(50)

	PRINT 'OfficeID: ' + CAST(@officeId AS VARCHAR(20));
	--SET @startDate = '1/1/2013';
	--SET @endDate = DATEADD(day, 1, GETUTCDATE());

	/** RESULT TABLE */
	DECLARE @TableResult TABLE (
		CMFID BIGINT
		, LeadId BIGINT
		, AccountID BIGINT
		, SalesRepID VARCHAR(50)
		, RepName VARCHAR(50)
		, CustomerName VARCHAR(100)
		, ContactsMade INT
		, CreditsRun INT
		, NoSales INT
		, Installations INT
		, SalesPrice MONEY
		, Term INT
		, EzPay DECIMAL(5,2)
		, CloseRate FLOAT
		, SetupFee MONEY
		, FirstMonth MONEY
		, [Over3Months] MONEY
		, Referrals INT
		, [PackageSold] VARCHAR(100)
		, Margins MONEY
		, InstallDate DATETIME
		, LeadDate DATETIME
	);

	/** DECLARE CURSOR */
	DECLARE repCur CURSOR FOR 
	SELECT DISTINCT
		SalesRepID 
		, RU.FullName AS RepName
	FROM
		[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM
		INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(PERFM.SalesRepId = RU.GPEmployeeId)
	WHERE
		(OfficeId = @officeId)
		AND (SalesRepId = @salesRepId)
		AND ((PERFM.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) 
		OR (PERFM.InstallDate BETWEEN @startDate AND @endDate)
		OR (PERFM.LeadDate BETWEEN @startDate AND @endDate))
	ORDER BY
		RU.FullName;
--	SELECT TeamLocationID, Description AS [OfficeName] FROM [WISE_HumanResource].[dbo].[RU_TeamLocations] WHERE (@officeId IS NULL OR (TeamLocationID = @officeId)) AND (IsActive = 1 AND IsDeleted = 0);

	OPEN repCur;
	
	FETCH NEXT FROM repCur
	INTO @salesRepId, @RepName;

	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO @TableResult (
			CMFID
			, LeadId
			, AccountID
			, SalesRepID
			, RepName
			, CustomerName
			, ContactsMade
			, CreditsRun
			, NoSales
			, Installations
			, SalesPrice
			, Term
			, CloseRate
			, SetupFee
			, [FirstMonth]
			, [Over3Months]
			, [PackageSold]
			, [InstallDate]
			, [LeadDate]
		)
		SELECT
			FXF.CustomerMasterFileId
			, FXF.LeadId
			, FXF.AccountID
			, @salesRepId
			, @RepName
			, CASE
				WHEN FXF.AccountID IS NOT NULL THEN FXF.FirstName + ' ' + FXF.LastName + ' ([C:' + CAST(FXF.CustomerMasterFileId AS VARCHAR) + ']|[' + 'A:' + CAST(FXF.AccountID AS VARCHAR) + '])' 
				WHEN FXF.CustomerMasterFileId IS NOT NULL THEN FXF.FirstName + ' ' + FXF.LastName + ' ([C:' + CAST(FXF.CustomerMasterFileId AS VARCHAR) + ']|[' + 'L:' + CAST(FXF.LeadId AS VARCHAR) + '])' 
				WHEN FXF.ContactId IS NOT NULL THEN FXF.FirstName + ' ' + FXF.LastName + ' ([N:' + CAST(FXF.ContactId AS VARCHAR) + '])'
				ELSE FXF.FirstName + ' ' + FXF.LastName
			  END AS CustomerName
			, FXF.IsContact
			, CASE
				WHEN FXF.InstallDate IS NOT NULL THEN 1
				WHEN FXF.LeadDate IS NOT NULL THEN 1
				ELSE 0
			  END AS CreditsRun
			, CASE 
				WHEN FXF.LeadDate IS NOT NULL AND FXF.InstallDate IS NULL THEN 1
				ELSE 0
			  END AS NoSales
			, CASE 
				WHEN FXF.InstallDate IS NOT NULL THEN 1
				ELSE 0
			  END AS Installations
			, 0 AS [SalesPrice]
			, FXF.Term
			,  CASE
				WHEN FXF.LeadDate IS NULL THEN 0
				WHEN FXF.InstallDate IS NOT NULL THEN 1
				ELSE 0
			  END  AS [CloseRate]
			, FXF.SetupFee
			, FXF.[1stMonth]
			, FXF.[Over3Months]
			, MAP.AccountPackageName
			, FXF.InstallDate
			, FXF.LeadDate
		FROM
			[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS [FXF]
			LEFT OUTER JOIN [WISE_CRM].[dbo].[MS_AccountPackages] AS MAP WITH (NOLOCK)
			ON
				(FXF.PackageSoldId = MAP.AccountPackageID)
		WHERE
			((FXF.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) 
			OR (FXF.InstallDate BETWEEN @startDate AND @endDate)
			OR (FXF.LeadDate BETWEEN @startDate AND @endDate))
			AND (@officeId IS NULL OR (FXF.OfficeId = @officeId))
			AND (@SalesRepID IS NULL OR (FXF.SalesRepId = @SalesRepID));

		-- Get Next
		FETCH NEXT FROM repCur
		INTO @salesRepId, @RepName;
	END

	/** CLOSE CURSOR */
	CLOSE repCur;
	DEALLOCATE repCur;

	/** RETURN RESULT */
	SELECT * FROM @TableResult;

END
GO

GRANT EXEC ON dbo.custReport_PerformanceSalesRepBreakDown TO PUBLIC
GO

/*
*/
DECLARE @officeId INT = 10
	, @salesRepId VARCHAR(50) = 'FARRB001'
	, @DealerId INT
	, @startDate DATETIME = '2015-05-05 06:00:00'
	, @endDate DATETIME = '2015-08-05 05:59:59';

EXEC [dbo].[custReport_PerformanceSalesRepBreakDown]
	@officeId
	, @salesRepId
	, @DealerId
	, @startDate
	, @endDate;

--SELECT * FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] WHERE LeadID = 1091469;
--SELECT
--	RUT.TeamLocationID 
--	, FXF.OfficeId
--	, FXF.Term
--	, FXF.CloseRate
--	, FXF.SetupFee
--	, FXF.[1stMonth] 
--	, FXF.Over3Months
--	, FXF.PackageSoldId
--FROM
--	[dbo].[RU_TeamLocations] AS RUT WITH (NOLOCK)
--	INNER JOIN
--	[WISE_CRM].[dbo].fxRepts_InstallsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate) AS [FXF]
--	ON
--		(RUT.TeamLocationID = FXF.OfficeId);