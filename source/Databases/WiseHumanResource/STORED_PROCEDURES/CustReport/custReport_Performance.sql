USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_Performance')
	BEGIN
		PRINT 'Dropping Procedure custReport_Performance'
		DROP  Procedure  dbo.custReport_Performance
	END
GO

PRINT 'Creating Procedure custReport_Performance'
GO
/******************************************************************************
**		File: custReport_Performance.sql
**		Name: custReport_Performance
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
CREATE Procedure dbo.custReport_Performance
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
		, @OfficeName VARCHAR(50)
		, @Over3Months INT
		, @PackagesSold INT;

	PRINT 'OfficeID: ' + CAST(@officeId AS VARCHAR(20));
	--SET @startDate = '1/1/2013';
	--SET @endDate = DATEADD(day, 1, GETUTCDATE());

	/** RESULT TABLE */
	DECLARE @TableResult TABLE (
		OfficeID INT
		, OfficeName VARCHAR(50)
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
	DECLARE officeCur CURSOR FOR 
	SELECT 
		TeamLocationID
		, Description AS [OfficeName]
	FROM
		[WISE_HumanResource].[dbo].[RU_TeamLocations]
	WHERE
		(@officeId IS NULL OR (TeamLocationID = @officeId)) AND (IsActive = 1 AND IsDeleted = 0);

	OPEN officeCur;
	
	FETCH NEXT FROM officeCur
	INTO @officeId, @OfficeName;

	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		
		INSERT INTO @TableResult (OfficeID, OfficeName) VALUES (@officeId, @OfficeName);

		/** GET CONTACTS */
		SELECT @Contacts = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (PERFM.IsContact = 'TRUE')
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate OR PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** GET Qualifications */
		SELECT @Qualifications = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate OR PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** Get NO SALES */
		SELECT @NoSales = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (PERFM.InstallDate IS NULL)
			AND (PERFM.LeadDate BETWEEN @startDate AND @endDate)
		GROUP BY
			PERFM.OfficeId

		/** Get INSTALLS */
		SELECT @Installations = COUNT(*) FROM [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
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
			AND (PERFM.Over3Months = 'TRUE')
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate
				OR PERFM.LeadDate BETWEEN @startDate AND @endDate)

		/** Sum Packages Sold */
		SELECT
			@PackagesSold = COUNT(*)
		FROM
			[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS PERFM WITH (NOLOCK)
			LEFT OUTER JOIN [dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
			ON
				(PERFM.OfficeId = RT.TeamLocationID)
		WHERE
			(@officeId IS NULL OR PERFM.OfficeId = @officeId)
			AND (PERFM.[PackageSoldId] IS NOT NULL)
			AND (PERFM.Over3Months = 'TRUE')
			AND (PERFM.InstallDate BETWEEN @startDate AND @endDate
				OR PERFM.LeadDate BETWEEN @startDate AND @endDate)

		/** EXECUTE UPDATING DATA */
		UPDATE TBR SET
			TBR.ContactsMade = AllData.ContactsMade
			, TBR.CreditsRun = AllData.CreditsRun
			, TBR.NoSales = AllData.NoSales
			, TBR.Installations = AllData.Installations
			, TBR.SalesPrice = AllData.SalesPrice
			, TBR.Term = AllData.Term
			, TBR.CloseRate = AllData.CloseRate
			, TBR.SetupFee = AllData.SetupFee
			, TBR.[FirstMonth] = AllData.FirstMonth
			, TBR.[Over3Months] = AllData.Over3Months
			, TBR.[PackageSold] = AllData.PackageSold
		FROM
			@TableResult AS TBR
			INNER JOIN (
			SELECT
			PERFM.OfficeId
			, ISNULL(@Contacts, 0) AS ContactsMade
			, ISNULL(@Qualifications, 0) AS CreditsRun
			, ISNULL(@NoSales, 0) AS NoSales
			, ISNULL(@Installations, 0) AS Installations
			, 0 AS SalesPrice
			, AVG(PERFM.Term) AS Term
			, CASE
				WHEN @Qualifications IS NULL OR @Qualifications = 0 THEN 0
				ELSE CAST(@Installations AS FLOAT)/CAST(@Qualifications AS FLOAT)
			  END AS CloseRate
			, AVG(PERFM.SetupFee) AS SetupFee
			, AVG(PERFM.[1stMonth]) AS FirstMonth
			, @Over3Months AS Over3Months
			, @PackagesSold AS [PackageSold]
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
				PERFM.OfficeId) AS AllData
			ON
				(TBR.OfficeId = AllData.OfficeId)

		-- Get Next
		FETCH NEXT FROM officeCur
		INTO @officeId, @OfficeName;
	END

	/** CLOSE CURSOR */
	CLOSE officeCur;
	DEALLOCATE officeCur;

	/** RETURN RESULT */
	SELECT * FROM @TableResult;

END
GO

GRANT EXEC ON dbo.custReport_Performance TO PUBLIC
GO

/*
*/
DECLARE @officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME = '2015-05-05 06:00:00'
	, @endDate DATETIME = '2015-08-05 05:59:59';

EXEC dbo.custReport_Performance @officeId, NULL, @DealerId, @startDate, @endDate

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