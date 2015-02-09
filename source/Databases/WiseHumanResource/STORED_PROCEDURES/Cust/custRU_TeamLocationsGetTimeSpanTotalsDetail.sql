USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetTimeSpanTotalsDetail')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetTimeSpanTotalsDetail'
		DROP  Procedure  dbo.custRU_TeamLocationsGetTimeSpanTotalsDetail
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetTimeSpanTotalsDetail'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetTimeSpanTotalsDetail.sql
**		Name: custRU_TeamLocationsGetTimeSpanTotalsDetail
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
CREATE Procedure dbo.custRU_TeamLocationsGetTimeSpanTotalsDetail
(
	@TrueOffice_FalseSeason BIT = 1
	, @ID INT --Office or Season ID
	, @ExcludeSubs BIT = 0
	, @WeekDateFirst INT = 1
	, @ByWeek BIT = 0
)
AS
BEGIN
	
--	DECLARE @ID INT
--	SET @ID = 107
--
--	DECLARE @ExcludeSubs BIT
--	SET @ExcludeSubs = 0

	SET DATEFIRST @WeekDateFirst;
	
	DECLARE @StartDate DATETIME
	DECLARE @EndDate DATETIME


	-- Get Start and End Dates
	SELECT
		@StartDate = StartDate
		, @EndDate = EndDate
	FROM RU_Season AS RS WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		RS.SeasonID = RUTL.SeasonID
	WHERE
		--Match ID to TeamLocationID
		((@TrueOffice_FalseSeason = 1) AND (RUTL.TeamLocationID = @ID))
		OR
		--Match ID to SeasonID
		((@TrueOffice_FalseSeason = 0) AND (RS.SeasonID = @ID))


	SELECT
		RU.FullName
		, RU.PublicFullName
		, RU.UserID
		, WeekCounts.StartDate
		, ISNULL(WeekCounts.NumAccounts, 0) AS NumAccounts
	FROM RU_Users AS RU WITH(NOLOCK)
	--LEFT OUTER JOIN
	INNER JOIN
	(
		SELECT
			UserID
			, StartDate
			-- Number of accounts on StartDate
			, COUNT(UserID) AS NumAccounts
		FROM
		(
			SELECT
				RU.UserID
				, RU.GPEmployeeID
				, CASE
					WHEN @ByWeek = 0 THEN dbo.fxFirstDayOfMonth(InstallDate)--Get Start of Month
					ELSE dbo.fxFirstDay(InstallDate)--Get Start of Week
				END AS StartDate
			FROM SAE_AccountsInstalled AS AI
			INNER JOIN RU_Users AS RU WITH (NOLOCK)
			ON
				RU.UserID = AI.SalesRepUserID
			WHERE
				--Accounts Installed With in Date Range
				(AI.InstallDate BETWEEN @StartDate AND @EndDate)
				--Exclude/Include Sub Accounts
				AND ((@ExcludeSubs <> 1) OR (AI.CreditScore >= 600))
				AND
				(
					--Match ID to TeamLocationID
					((@TrueOffice_FalseSeason = 1) AND (AI.TeamLocationID = @ID))
					OR
					--Match ID to SeasonID
					((@TrueOffice_FalseSeason = 0) AND (AI.SeasonID = @ID))
				)
				--HACK
				AND (SalesRepUserID NOT IN (SELECT * FROM fxGetExcludeUserIDList()))
				--END HACK
		) AS WeekCounts
		GROUP BY
			UserID
			, StartDate
	) AS WeekCounts
	--Get Description
	ON
		(RU.UserID = WeekCounts.UserID)
	ORDER BY
		RU.FullName
		, WeekCounts.UserID
		, WeekCounts.StartDate

END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetTimeSpanTotalsDetail TO PUBLIC
GO