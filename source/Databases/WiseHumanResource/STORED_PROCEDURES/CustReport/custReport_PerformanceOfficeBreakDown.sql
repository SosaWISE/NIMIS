USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_PerformanceOfficeBreakDown')
	BEGIN
		PRINT 'Dropping Procedure custReport_PerformanceOfficeBreakDown'
		DROP  Procedure  dbo.custReport_PerformanceOfficeBreakDown
	END
GO

PRINT 'Creating Procedure custReport_PerformanceOfficeBreakDown'
GO
/******************************************************************************
**		File: custReport_PerformanceOfficeBreakDown.sql
**		Name: custReport_PerformanceOfficeBreakDown
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
**		Date: 07/31/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/31/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_PerformanceOfficeBreakDown
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
	DECLARE @Contacts INT
		, @Qualifications INT
		, @NoSales INT
		, @Installations INT
		, @RepName VARCHAR(50)

	PRINT 'OfficeID: ' + CAST(@officeId AS VARCHAR(20));
	--SET @startDate = '1/1/2013';
	--SET @endDate = DATEADD(day, 1, GETUTCDATE());

	/** RESULT TABLE */
	DECLARE @TableResult TABLE (
		SalesRepID VARCHAR(50)
		, RepName VARCHAR(50)
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
		, [PackageSold] INT
		, Margins MONEY
	);

	/** DECLARE CURSOR */
	DECLARE repCur CURSOR FOR 
	SELECT DISTINCT
		SalesRepID 
		, RU.FullName AS RepName
	FROM
		[WISE_CRM].[dbo].[SAE_ReportsPerformance] AS PERFM
		INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(PERFM.SalesRepId = RU.GPEmployeeId)
	WHERE
		(OfficeId = @officeId 
		AND ((PERFM.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) OR (PERFM.InstallDate BETWEEN @startDate AND @endDate)))
	ORDER BY
		RU.FullName;
--	SELECT TeamLocationID, Description AS [OfficeName] FROM [WISE_HumanResource].[dbo].[RU_TeamLocations] WHERE (@officeId IS NULL OR (TeamLocationID = @officeId)) AND (IsActive = 1 AND IsDeleted = 0);

	OPEN repCur;
	
	FETCH NEXT FROM repCur
	INTO @salesRepId, @RepName;

	WHILE (@@FETCH_STATUS = 0)
	BEGIN

		SELECT @Contacts = COUNT(*) FROM [NXSE_Sales].[dbo].fxRepts_ContactsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		SELECT @Qualifications = COUNT(*) FROM [WISE_CRM].[dbo].fxRepts_QualifiedByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		SELECT @NoSales = COUNT(*) FROM [WISE_CRM].[dbo].fxRepts_NoSalesByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		INSERT INTO @TableResult (
			SalesRepID
			, RepName
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
		)
		SELECT
			@salesRepId
			, @RepName
			, @Contacts AS ContactsMade
			, @Qualifications AS CreditsRun
			, @NoSales AS NoSales
			, FXF.Installs AS Installations
			, 0 AS [SalesPrice]
			, FXF.Term
			, CASE
				WHEN @Qualifications IS NULL OR @Qualifications = 0 THEN 0
				ELSE CAST(FXF.Installs AS FLOAT)/CAST(@Qualifications AS FLOAT)
			  END  AS [CloseRate]
			, FXF.SetupFee
			, FXF.[1stMonth]
			, FXF.[Over3Months]
			, FXF.[PackageSoldId]
		FROM
			[WISE_CRM].[dbo].fxRepts_InstallsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate) AS [FXF];

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

GRANT EXEC ON dbo.custReport_PerformanceOfficeBreakDown TO PUBLIC
GO

/*
*/
DECLARE @officeId INT = 1
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME = '1/1/2013'
	, @endDate DATETIME = '2015-08-01 05:00:00';

EXEC dbo.custReport_PerformanceOfficeBreakDown @officeId, NULL, @DealerId, @startDate, @endDate

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