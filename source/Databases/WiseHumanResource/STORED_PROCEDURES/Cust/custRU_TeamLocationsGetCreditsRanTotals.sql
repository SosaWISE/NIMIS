USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetCreditsRanTotals')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetCreditsRanTotals'
		DROP  Procedure  dbo.custRU_TeamLocationsGetCreditsRanTotals
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetCreditsRanTotals'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetCreditsRanTotals.sql
**		Name: custRU_TeamLocationsGetCreditsRanTotals
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
CREATE Procedure dbo.custRU_TeamLocationsGetCreditsRanTotals
(
	@TeamLocationID INT
	, @ExcludeSubs BIT = 0
	, @WeekDateFirst INT = 1
	, @ByWeek BIT = 0
	, @ShowOnlyActiveInRoster BIT = 0
)
AS
BEGIN
	
--	DECLARE @@TeamLocationID INT
--	SET @@TeamLocationID = 8;
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
	
	DECLARE @SeasonID INT

	SELECT
		@StartDate = RS.StartDate
		, @EndDate = RS.EndDate
		, @SeasonID = RS.SeasonID
	FROM RU_Season AS RS WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		RS.SeasonID = RUTL.SeasonID
	WHERE
		RUTL.TeamLocationID = @TeamLocationID
	
	DECLARE @T TABLE
	(
		GPEmployeeID NVARCHAR(25)
	)
	INSERT INTO @T
	SELECT DISTINCT
		RU.GPEmployeeID
	FROM SAE_AccountsInstalled AS AI
	INNER JOIN RU_Users AS RU
	ON
		AI.SalesRepUserID = RU.UserID
	WHERE
		AI.TeamLocationID = @TeamLocationID
	
	SELECT
		DT.GPEmployeeID
		, DT.UserID
		, DT.FullName
		, DT.Week
		, COALESCE(Reps.NCreditReports, 0) AS NCreditReports
	FROM
	(
		SELECT DISTINCT
			RU.GPEmployeeID
			, RU.UserID
			, RU.FullName
			, CASE
					WHEN @ByWeek = 0 THEN dbo.fxFirstDayOfMonth(DT.DATE)--Get Start of Month
					ELSE dbo.fxFirstDay(DT.DATE)--Get Start of Week
				END AS Week
		FROM RU_Users AS RU WITH(NOLOCK)
		INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
		ON
			(RU.UserID = RR.UserID)
			AND (RR.SeasonID = @SeasonID)
		INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
		ON
			(RR.UserTypeId = RUT.UserTypeID)
		CROSS JOIN SAE_Dates AS DT WITH(NOLOCK)
		WHERE
			((@StartDate) <= DT.DATE) AND (DT.DATE < (@EndDate))
			AND
			(
				(@ShowOnlyActiveInRoster = 0)
				OR RU.GPEmployeeID IN
					(
						SELECT
							RU.GPEmployeeID
						FROM RU_TeamLocationRoster AS ROS
						INNER JOIN RU_Recruits AS RR
						ON
							ROS.RecruitID = RR.RecruitID
						INNER JOIN RU_Users AS RU
						ON
							RR.UserID = RU.UserID
						INNER JOIN RU_UserType AS RUT
						ON
							RR.UserTypeId = RUT.UserTypeID
						WHERE
							(RUT.RoleLocationID = 1)
							AND (ROS.TeamLocationId = @TeamLocationID)
							AND (ROS.IsDeleted = 0)
							AND ((ROS.QuitDate IS NULL) OR (ROS.TerminationReasonID = 8))
					)
			)
			/* active and not deleted */
			AND
			(
				(RU.IsActive = 1 AND RU.IsDeleted = 0)
			)
			/* is sales */
			AND
			(
				RUT.RoleLocationID = 1
			)
	) AS DT
	LEFT OUTER JOIN
	(
		SELECT
			GPSalesRepID
			, Week
			, COUNT(GPSalesRepID) AS NCreditReports
		FROM
		(
			SELECT
				MCL.GPSalesRepID
				, CASE
						WHEN @ByWeek = 0 THEN dbo.fxFirstDayOfMonth(MCL.CreatedByDate)--Get Start of Month
						ELSE dbo.fxFirstDay(MCL.CreatedByDate)--Get Start of Week
					END AS Week
			FROM Platinum_Protection_InterimCRM..MC_Lead AS MCL WITH(NOLOCK)
			INNER JOIN Platinum_Protection_InterimCRM..MC_CreditReport AS MCR WITH(NOLOCK)
			ON
				MCL.LeadID = MCR.LeadID
			WHERE
				((@StartDate) <= MCL.CreatedByDate) AND (MCL.CreatedByDate < (@EndDate))
				AND (MCL.GPSalesRepID IN (SELECT * FROM @T))
		) AS WeekCounts
		GROUP BY
			GPSalesRepID
			, Week
	) AS Reps
	ON
		(DT.GPEmployeeID = Reps.GPSalesRepID)
		AND (DT.Week = Reps.Week)
	WHERE
		(DT.GPEmployeeID IN (SELECT * FROM @T))
	ORDER BY
		DT.FullName
		, DT.GPEmployeeID
		, DT.Week
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetCreditsRanTotals TO PUBLIC
GO