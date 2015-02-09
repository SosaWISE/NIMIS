USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRankings')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRankings'
		DROP  Procedure  dbo.custRU_RecruitsGetRankings
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRankings'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRankings.sql
**		Name: custRU_RecruitsGetRankings
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
CREATE Procedure dbo.custRU_RecruitsGetRankings
(
	@StartDate DateTime
	, @EndDate DateTime
	, @SeasonIDList NVARCHAR(MAX)
	, @RoleLocationID INT
	, @MaxRankToReturn INT = NULL
	, @UserID INT = NULL
	, @IncludeCancels BIT = 0
)

WITH RECOMPILE
AS
BEGIN

--DECLARE @StartDate DateTime
--DECLARE @EndDate DateTime
--DECLARE @SeasonIDList NVARCHAR(MAX)
--DECLARE @RoleLocationID INT
--DECLARE @MaxRankToReturn INT
--DECLARE @UserID INT
--
--SET @StartDate = '4/17/2008 4:00:00 AM'
--SET @EndDate = '7/25/2008 03:59:59.997'
--SET @SeasonIDList = '7'
--SET @RoleLocationID = 1
--SET @MaxRankToReturn = NULL
--SET @UserID = NULL


	DECLARE @PassNum INT
	SET @PassNum = 600
	
	DECLARE @TechRoleLocationID INT
	SET @TechRoleLocationID = 2

	SELECT
		CAST(Rankings.[Rank] AS INT) AS [Rank]
		, Rankings.Accounts
		, RU.FullName
		, RU.PublicFullName
		, Rankings.UserID
		, RecruitInfo.Description
		, RecruitInfo.TeamName
		, RecruitInfo.OfficeName
	FROM RU_Users AS RU WITH(NOLOCK)
	INNER JOIN
	(
		SELECT DISTINCT
			RANK() OVER(ORDER BY Counts.Accounts DESC) AS [Rank]
			, Counts.Accounts
			, Counts.UserID
		FROM
		(
			SELECT
				CASE
					WHEN @RoleLocationID = @TechRoleLocationID THEN COUNT(AI.TechnicianUserID)
					ELSE COUNT(AI.SalesRepUserID)
				END AS Accounts
				, CASE
					WHEN @RoleLocationID = @TechRoleLocationID THEN AI.TechnicianUserID
					ELSE AI.SalesRepUserID
				END AS UserID
			FROM dbo.SAE_AccountsInstalled AS AI WITH(NOLOCK)
			WHERE
				-- Filter by Season
				(AI.SeasonID IN (SELECT * FROM SplitIntList(@SeasonIDList)))
				-- Between Start and End Dates
				AND (AI.InstallDate BETWEEN @StartDate AND @EndDate)
				AND
				(
					--For Sales, check passing credit score and not cancelled
					--For Tech, just needs to be installed
					-- (is tech) or (passing credit and not cancelled)
					(@RoleLocationID = @TechRoleLocationID)
					OR
					(
						-- Only count Scores 600 and above
						(AI.CreditScore >= @PassNum)
						-- No Cancels
						AND
						(
							(@IncludeCancels = 1)--if IncludeCancels equals 1, all accounts are included
							OR (AI.Status = 'OK')
						)
					)
				)
				AND AI.SalesRepUserID NOT IN (5799,9854)--[PLatinum Protection], MOVE ACCOUNTS
			GROUP BY
				CASE
					WHEN @RoleLocationID = @TechRoleLocationID THEN AI.TechnicianUserID
					ELSE AI.SalesRepUserID
				END
		) AS Counts
	) AS Rankings
	ON
		(RU.UserID = Rankings.UserID)
	LEFT OUTER JOIN
	(
		SELECT
			LR.*
			, RT.Description AS TeamName
			, RUTL.Description AS OfficeName
		FROM dbo.fxLastRecruit(@SeasonIDList) AS LR
		INNER JOIN RU_UserType AS RUT
		ON
			(LR.UserTypeID = RUT.UserTypeID)
		LEFT OUTER JOIN SAE_RecruitTeamMappings AS Tree
		ON
			(LR.RecruitID = Tree.RecruitID)
		LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
		ON
			Tree.TeamID = RT.TeamID
		LEFT OUTER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
		ON
			RT.TeamLocationID = RUTL.TeamLocationID
		WHERE
			--Only the correct role location
			(RUT.RoleLocationID = @RoleLocationID)
	) AS RecruitInfo
	ON
		RU.UserID = RecruitInfo.UserID
	WHERE
		(UserTypeID NOT IN (1))
		AND ((@MaxRankToReturn IS NULL) OR (Rankings.[Rank] <= @MaxRankToReturn))
		AND ((@UserID IS NULL) OR (RU.UserID = @UserID))
		AND ((RU.IsDeleted = 0) AND (RU.IsActive = 1))
		--HACK
		AND (RU.UserID NOT IN (SELECT * FROM fxGetExcludeUserIDList()))
		--END HACK
	ORDER BY
		Rankings.[Rank]
		, RU.FullName


END

GO

GRANT EXEC ON dbo.custRU_RecruitsGetRankings TO PUBLIC
GO