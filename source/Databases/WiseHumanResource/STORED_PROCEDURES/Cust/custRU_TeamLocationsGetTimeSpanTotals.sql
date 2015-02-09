USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetTimeSpanTotals')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetTimeSpanTotals'
		DROP  Procedure  dbo.custRU_TeamLocationsGetTimeSpanTotals
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetTimeSpanTotals'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetTimeSpanTotals.sql
**		Name: custRU_TeamLocationsGetTimeSpanTotals
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
CREATE Procedure dbo.custRU_TeamLocationsGetTimeSpanTotals
(
	@SeasonID INT
	, @ExcludeSubs BIT = 0
	, @WeekDateFirst INT = 1
	, @ByWeek BIT = 0
)
AS
BEGIN
	
--	DECLARE @SeasonID INT
--	SET @SeasonID = 6;
--
--	DECLARE @ExcludeSubs BIT
--	SET @ExcludeSubs = 1
--
--	DECLARE @WeekDateFirst INT
--	SET @WeekDateFirst = 1
--
--	DECLARE @ByMonth BIT
--	SET @ByMonth = 1


	SET DATEFIRST @WeekDateFirst;

	DECLARE @StartDate DATETIME
	DECLARE @EndDate DATETIME

	SELECT
		@StartDate = StartDate
		, @EndDate = EndDate
	FROM RU_Season AS RS WITH(NOLOCK)
	WHERE
		RS.SeasonID = @SeasonID

	SELECT
		RUTL.Description
		, RUTL.TeamLocationID
		, TimeSpanCounts.StartDate
		, ISNULL(TimeSpanCounts.NumAccounts, 0) AS NumAccounts
	FROM RU_TeamLocations AS RUTL WITH(NOLOCK)
	--LEFT OUTER JOIN
	INNER JOIN
	(
		SELECT
			TeamLocationID
			, StartDate
			-- Number of accounts on StartDate
			, COUNT(TeamLocationID) AS NumAccounts
		FROM
		(
			SELECT
				TeamLocationID
				, CASE
					WHEN @ByWeek = 0 THEN dbo.fxFirstDayOfMonth(InstallDate)--Get Start of Month
					ELSE dbo.fxFirstDay(InstallDate)--Get Start of Week
				END AS StartDate
			FROM SAE_AccountsInstalled AS AI
			WHERE
				--Accounts Installed With in Date Range
				(AI.InstallDate BETWEEN @StartDate AND @EndDate)
				--Exclude/Include Sub Accounts
				AND ((@ExcludeSubs <> 1) OR (AI.CreditScore >= 600))
				AND (AI.SeasonID = @SeasonID)
				--HACK
				AND (SalesRepUserID NOT IN (SELECT * FROM fxGetExcludeUserIDList()))
				--END HACK
		) AS WeekCounts
		GROUP BY
			TeamLocationID
			, StartDate
	) AS TimeSpanCounts
	--Get Description
	ON
		RUTL.TeamLocationID = TimeSpanCounts.TeamLocationID
	WHERE
		(RUTL.SeasonID = @SeasonID)
	ORDER BY
		RUTL.Description
		, TimeSpanCounts.TeamLocationID
		, TimeSpanCounts.StartDate

END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetTimeSpanTotals TO PUBLIC
GO