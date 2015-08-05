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
		, @Over3Months INT;

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
		[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM
		INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(PERFM.SalesRepId = RU.GPEmployeeId)
	WHERE
		(OfficeId = @officeId)
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
		/** Reset Values */
		SET @Contacts = 0;
		SET @Qualifications = 0;
		SET @NoSales = 0;
		SET @Installations = 0;
		SET @Over3Months = 0;

		/** GET CONTACTS */
		SELECT @Contacts = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			--AND (@DealerId IS NULL OR (PERFM.DealerId = @DealerId))
			--AND (PERFM.LeadDate IS NOT NULL)
			AND (PERFM.IsContact = 'TRUE')
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate OR PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** GET Qualifications */
		SELECT @Qualifications = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate OR PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** Get NO SALES */
		SELECT @NoSales = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			AND (PERFM.InstallDate IS NULL)
			AND (PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** Get INSTALLS */
		SELECT @Installations = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			AND (PERFM.InstallDate IS NOT NULL)
			AND ((PERFM.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) 
			OR (PERFM.InstallDate BETWEEN @startDate AND @endDate)
			OR (PERFM.LeadDate BETWEEN @startDate AND @endDate))
		GROUP BY
			PERFM.OfficeId

		/** Figure out the number of Over 3 Months */
		SELECT
			@Over3Months = COUNT(*)
		FROM
			[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
			LEFT OUTER JOIN [dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
			ON
				(PERFM.OfficeId = RT.TeamLocationID)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			--AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			--AND (@DealerId IS NULL OR (PERFM.DealerId = @DealerId))
			--AND (PERFM.LeadDate IS NOT NULL)
			AND (PERFM.Over3Months = 'TRUE')
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate
				OR PERFM.LeadDate BETWEEN @startDate AND @endDate)

		/** CREATE THE LIST */
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
			, ISNULL(@Contacts, 0) AS ContactsMade
			, ISNULL(@Qualifications, 0) AS CreditsRan
			, ISNULL(@NoSales, 0) AS NoSales
			, ISNULL(@Installations, 0) AS Installations
			, 0 AS [SalesPrice]
			, AVG(PERFM.Term) AS Term
			, CASE
				WHEN @Qualifications IS NULL OR @Qualifications = 0 THEN 0
				ELSE CAST(@Installations AS FLOAT)/CAST(@Qualifications AS FLOAT)
			  END  AS [CloseRate]
			, AVG(PERFM.SetupFee) AS SetupFee
			, AVG(PERFM.[1stMonth]) AS [FirstMonth]
			, @Over3Months AS [Over3Months]
			, COUNT(PERFM.[PackageSoldId]) AS [PackageSold] 

		FROM
			[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
			LEFT OUTER JOIN [dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
			ON
				(PERFM.OfficeId = RT.TeamLocationID)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			--AND (@salesRepId IS NULL OR (PERFM.SalesRepId = @salesRepId))
			--AND (@DealerId IS NULL OR (PERFM.DealerId = @DealerId))
			--AND (PERFM.LeadDate IS NOT NULL)
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate
				OR PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId
			, RT.Description

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
DECLARE @officeId INT = 11
	, @salesRepId VARCHAR(50)
	, @DealerId INT
	, @startDate DATETIME = '2015-05-06 06:00:00'
	, @endDate DATETIME = '2015-08-05 05:59:59';

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