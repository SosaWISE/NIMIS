USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetSummaryReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetSummaryReport'
		DROP  Procedure  dbo.custRU_RecruitsGetSummaryReport
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetSummaryReport'
GO
/******************************************************************************
**		File: custRU_RecruitsGetSummaryReport.sql
**		Name: custRU_RecruitsGetSummaryReport
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
CREATE Procedure dbo.custRU_RecruitsGetSummaryReport
(
	@RecruitID INT
	, @SeasonID INT
	, @StartDate DATETIME
	, @EndDate DATETIME
)
AS
BEGIN

	DECLARE @TeamLocationID INT
	SET @TeamLocationID = (SELECT TeamLocationID FROM RU_Teams WHERE TeamID =
							(SELECT TeamID FROM SAE_RecruitTeamMappings WHERE RecruitID = @RecruitID))

	DECLARE @SalesRepGPID NVARCHAR(15)
	SET @SalesRepGPID = (SELECT GPEmployeeID FROM RU_Users WHERE UserID =
							(SELECT UserID FROM RU_Recruits WHERE RecruitID = @recruitID))

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
		FROM RU_Users AS RU WITH (NOLOCK)
		LEFT OUTER JOIN
			/****
			 * Get Credit Stats
			 ****/
			(SELECT DISTINCT GPSalesRepID
				, COUNT(CreditScore) AS NRun
				, SUM (CASE
							WHEN CreditScore >= @PassNum THEN 1
							ELSE 0
						END) AS NPassed
			FROM VW_CreditsRun
			WHERE
				QualificationDate BETWEEN @StartDate AND @EndDate
				AND TeamLocationID = @TeamLocationID
				AND GPSalesRepID = @SalesRepGPID
			GROUP BY GPSalesRepID) AS CreditStats
		ON
			RU.GPEmployeeID = CreditStats.GPSalesRepID
		LEFT OUTER JOIN
			/****
			 * Get Account Stats
			 ****/
			(SELECT ACCT.GPSalesRepID
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
				AND (ACCT.GPSalesRepID = @SalesRepGPID)
			GROUP BY ACCT.GPSalesRepID
			) AS AccountStats
		ON
			RU.GPEmployeeID = AccountStats.GPSalesRepID
		LEFT OUTER JOIN
			/****
			 * Get Survey Stats
			 ****/
			(SELECT DISTINCT GPSalesRepID
				, COUNT (AccountID) AS NSurveysCompleted
			FROM VW_AccountPreSurveys
			WHERE
				QualificationDate BETWEEN @StartDate AND @EndDate
				AND TeamLocationID = @TeamLocationID
				AND GPSalesRepID = @SalesRepGPID
			GROUP BY GPSalesRepID) AS SurveyStats
		ON
			RU.GPEmployeeID = SurveyStats.GPSalesRepID
		WHERE
			RU.UserID = (SELECT UserID FROM RU_Recruits WHERE RecruitID = @RecruitID)
	) AS StatsTable
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetSummaryReport TO PUBLIC
GO