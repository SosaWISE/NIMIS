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
		, CloseRate MONEY
		, SetupFee MONEY
		, FirstMonth MONEY
		, [Over3Months] MONEY
		, Referrals INT
		, [PackageSold] INT
		, Margins MONEY
	);

	/** DECLARE CURSOR */
	DECLARE officeCur CURSOR FOR 
	SELECT TeamLocationID, Description AS [OfficeName] FROM [WISE_HumanResource].[dbo].[RU_TeamLocations] WHERE (@officeId IS NULL OR (TeamLocationID = @officeId)) AND (IsActive = 1 AND IsDeleted = 0);

	OPEN officeCur;
	
	FETCH NEXT FROM officeCur
	INTO @officeId, @OfficeName;

	WHILE (@@FETCH_STATUS = 0)
	BEGIN

		SELECT @Contacts = COUNT(*) FROM [NXSE_Sales].[dbo].fxRepts_ContactsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		SELECT @Qualifications = COUNT(*) FROM [WISE_CRM].[dbo].fxRepts_QualifiedByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		SELECT @NoSales = COUNT(*) FROM [WISE_CRM].[dbo].fxRepts_NoSalesByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate);

		INSERT INTO @TableResult (
			OfficeID
			, OfficeName
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
			@officeId
			, @OfficeName
			, @Contacts AS ContactsMade
			, @Qualifications AS CreditsRun
			, @NoSales AS NoSales
			, FXF.Installs AS Installations
			, 0 AS [SalesPrice]
			, FXF.Term
			, FXF.CloseRate
			, FXF.SetupFee
			, FXF.[1stMonth]
			, FXF.[Over3Months]
			, FXF.[PackageSoldId]
		FROM
			[WISE_CRM].[dbo].fxRepts_InstallsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate) AS [FXF];

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
	, @startDate DATETIME = '1/1/2013'
	, @endDate DATETIME = '2015-08-01 05:00:00';

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
