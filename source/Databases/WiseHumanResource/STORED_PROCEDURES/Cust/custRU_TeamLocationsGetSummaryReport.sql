USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetSummaryReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetSummaryReport'
		DROP  Procedure  dbo.custRU_TeamLocationsGetSummaryReport
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetSummaryReport'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetSummaryReport.sql
**		Name: custRU_TeamLocationsGetSummaryReport
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_TeamLocationsGetSummaryReport
(
	@TeamLocationID INT
	, @SeasonID INT
	, @StartDate DATETIME
	, @EndDate DATETIME
) WITH RECOMPILE
AS
BEGIN

	DECLARE @ExcellentNum INT
	SET @ExcellentNum = 625
	DECLARE @PassNum INT
	SET @PassNum = 600


	SELECT StatsTable.NTotalValidInstalls
		, StatsTable.NCancels
		, StatsTable.NNetInstalls
		, StatsTable.NSubInstalls
		, StatsTable.NCreditsRun
		, StatsTable.NCreditsPassed
		, StatsTable.NSurveysCompleted
	FROM
		(SELECT
			COALESCE(AccountStats.NSubInstalls, 0) AS NSubInstalls
			, COALESCE(AccountStats.NCancels, 0) AS NCancels
			, COALESCE(AccountStats.NExcellentInstalls, 0) + COALESCE(AccountStats.NPassInstalls, 0) AS NTotalValidInstalls
			, COALESCE(AccountStats.NExcellentInstalls, 0) + COALESCE(AccountStats.NPassInstalls, 0) - COALESCE(AccountStats.NCancels, 0) AS NNetInstalls
			, COALESCE(CreditStats.NRun, 0) AS NCreditsRun
			, COALESCE(CreditStats.NPassed, 0) AS NCreditsPassed
			, COALESCE(SurveyStats.NSurveysCompleted, 0) AS NSurveysCompleted
		FROM RU_TeamLocations AS RUTL WITH (NOLOCK)
		LEFT OUTER JOIN
			/****
			 * Get Credit Stats
			 ****/
			(SELECT DISTINCT TeamLocationID
				, COUNT(CreditScore) AS NRun
				, SUM (CASE
							WHEN CreditScore >= @PassNum THEN 1
							ELSE 0
						END) AS NPassed
			FROM VW_CreditsRun WITH (NOLOCK)
			WHERE
				QualificationDate BETWEEN @StartDate AND @EndDate
				AND TeamLocationID = @TeamLocationID
			GROUP BY TeamLocationID) AS CreditStats
		ON
			RUTL.TeamLocationID = CreditStats.TeamLocationID
		LEFT OUTER JOIN
			/****
			 * Get Account Stats
			 ****/
			(SELECT ACCT.TeamLocationID
				, SUM (CASE
							WHEN CreditScore >= @ExcellentNum THEN 1
							ELSE 0
						END) AS NExcellentInstalls
				, SUM (CASE
							WHEN CreditScore >= @PassNum AND CreditScore < @ExcellentNum THEN 1
							ELSE 0
						END) AS NPassInstalls
				, SUM (CASE
							WHEN CreditScore >= 1 AND CreditScore < @PassNum THEN 1
							ELSE 0
						END) AS NSubInstalls
				, SUM (CASE
							WHEN ST.NOCDate IS NOT NULL THEN 1
							WHEN ST.CancelDate IS NOT NULL THEN 1
							ELSE 0
						END) AS NCancels
			FROM Platinum_Protection_InterimCRM.dbo.MS_Account AS ACCT WITH (NOLOCK)
			INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
			ON
				ACCT.AccountID = ST.AccountID
			INNER JOIN SAE_MaxCredit AS CREDIT WITH (NOLOCK)
			ON
				ACCT.AccountID = CREDIT.AccountID
			WHERE
				(ST.InstallDate BETWEEN @StartDate AND @EndDate)
				AND (ACCT.TeamLocationID = @TeamLocationID)
			GROUP BY ACCT.TeamLocationID
			) AS AccountStats
		ON
			RUTL.TeamLocationID = AccountStats.TeamLocationID
		LEFT OUTER JOIN
			/****
			 * Get Survey Stats
			 ****/
			(SELECT DISTINCT TeamLocationID
				, COUNT (AccountID) AS NSurveysCompleted
			FROM VW_AccountPreSurveys WITH (NOLOCK)
			WHERE
				QualificationDate BETWEEN @StartDate AND @EndDate
				AND TeamLocationID = @TeamLocationID
			GROUP BY TeamLocationID) AS SurveyStats
		ON
			RUTL.TeamLocationID = SurveyStats.TeamLocationID
		WHERE
			RUTL.TeamLocationID = @TeamLocationID
	) AS StatsTable
	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetSummaryReport TO PUBLIC
GO