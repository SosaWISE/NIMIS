USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetDateStats')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetDateStats'
		DROP  Procedure  dbo.custRU_TeamLocationsGetDateStats
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetDateStats'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetDateStats.sql
**		Name: custRU_TeamLocationsGetDateStats
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
CREATE Procedure dbo.custRU_TeamLocationsGetDateStats
(
	@TeamLocationID INT
	, @IsRepOrTech BIT
	
	, @WeekStartDate DATETIME
	, @WeekEndDate DATETIME
	
	, @MonthStartDate DATETIME
	, @MonthEndDate DATETIME
)
AS
BEGIN


	DECLARE @PassCreditScoreThreshold INT
	SELECT
		@PassCreditScoreThreshold = RS.PassCreditScoreThreshold
	FROM RU_TeamLocations AS RUTL WITH(NOLOCK)
	INNER JOIN RU_Season AS RS WITH(NOLOCK)
	ON
		RUTL.SeasonID = RS.SeasonID
	WHERE
		TeamLocationID = @TeamLocationID
	
	SELECT
		COUNT(*) AS NTotalInstalls
		
		, SUM(CASE
				WHEN (
						(@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if sales check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NTotalNetInstalls
		, SUM(CASE
				WHEN (
						(@WeekStartDate <= AI.InstallDate AND AI.InstallDate <= @WeekEndDate)
						AND (@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if sales check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NWeekNetInstalls
		, SUM(CASE
				WHEN (
						(@MonthStartDate <= AI.InstallDate AND AI.InstallDate <= @MonthEndDate)
						AND (@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if sales check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NMonthNetInstalls

	FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
	WHERE
		AI.TeamLocationID = @TeamLocationID
	GROUP BY
		AI.TeamLocationID


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetDateStats TO PUBLIC
GO