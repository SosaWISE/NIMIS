USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamNumbersReport_SalesManagers')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamNumbersReport_SalesManagers'
		DROP  Procedure  dbo.custRU_TeamsGetTeamNumbersReport_SalesManagers
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamNumbersReport_SalesManagers'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamNumbersReport_SalesManagers.sql
**		Name: custRU_TeamsGetTeamNumbersReport_SalesManagers
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
CREATE Procedure dbo.custRU_TeamsGetTeamNumbersReport_SalesManagers
(
	@IDType NVARCHAR(10) = null
	, @ID INT
	, @ExcludeCancels BIT = 0
	, @CreditScore INT = NULL
)
AS
BEGIN



	--DECLARE @IDType NVARCHAR(10)
	--DECLARE @ID INT
	
	--DECLARE @ExcludeCancels BIT
	--SET @ExcludeCancels = 1
	----SET @ExcludeCancels = 0
	--
	--DECLARE @CreditScore INT
	--SET @CreditScore = 600
	----SET @CreditScore = NULL
	
	--IF (0 = 0)
	--BEGIN
		--SET @IDType = 'TeamID'
		--SET @ID = 156--248
	--END
	--ELSE
	--BEGIN
		--SET @IDType = 'SeasonID'
		--SET @ID = 6
	--END
	
	

	DECLARE @TeamID INT
	DECLARE @SeasonID INT 
	
	IF (@IDType = 'TeamID')
	BEGIN
	
		--set TeamID
		SELECT @TeamID = @ID
		
		--set SeasonID
		SELECT
			@SeasonID = RUTL.SeasonID
		FROM RU_TeamLocations AS RUTL WITH(NOLOCK)
		INNER JOIN RU_Teams AS RT WITH(NOLOCK)
		ON
			RUTL.TeamLocationID = RT.TeamLocationID
		WHERE
			(RT.TeamID = @TeamID)

	END
	ELSE
	BEGIN
	
		--set TeamID
		SET @TeamID = NULL
		
		--set SeasonID
		SET @SeasonID = @ID
					
	END
	
	DECLARE @RoleLocationID INT
	SET @RoleLocationID = 1

	DECLARE @TodayEnd DATETIME
	SET @TodayEnd = dbo.GetDateEnd(GETDATE())


	SELECT
		TopLevel.TeamLocationID
		, TopLevel.OfficeName
		, TopLevel.TeamID
		, TopLevel.TeamName
		, TopLevel.RecruitID
		, TopLevel.FullName
		
		, (TopLevel.Last7DaysRecruit + COALESCE(Peons.Last7DaysTeamRecruits, 0)) AS Last7DaysRecruits
		, 0 AS TotalRecruits
		
		, TopLevel.Last7DaysNums
		, TopLevel.Last30DaysNums
		, TopLevel.PersonalNums
		
		, TopLevel.Last7DaysNums + COALESCE(Peons.Last7DaysTeamNums, 0) AS TotalLast7DaysOfficeNums
		, TopLevel.Last30DaysNums + COALESCE(Peons.Last30DaysTeamNums, 0) AS TotalLast7DaysOfficeNums
		, COALESCE(Peons.TeamNums, 0) AS TeamNums
		
		, TopLevel.PersonalNums + COALESCE(Peons.TeamNums, 0) AS TotalOfficeNums
		
		, COALESCE(Peons.NumRepsWithNums, 0) AS NumRepsWithNums
	FROM
	(
		SELECT
			RUTL.TeamLocationID
			, RUTL.Description AS OfficeName
			, RT.TeamID
			, RT.Description AS TeamName
			, RR.RecruitID
			, RU.FullName
			, RR.ReportsToID
			
			, RecruitNums.UserID

			, CASE
					WHEN (RU.RecruitedDate > @TodayEnd-7) THEN 1
					ELSE 0
				END AS Last7DaysRecruit

			, RecruitNums.Last7DaysNums
			, RecruitNums.Last30DaysNums
			, RecruitNums.PersonalNums

		FROM
		(
			SELECT
				RU.UserID

				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-7) THEN 1
							ELSE 0
						END) AS Last7DaysNums
				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-30) THEN 1
							ELSE 0
						END) AS Last30DaysNums
				, SUM(CASE
							WHEN (AI.InstallDate IS NOT NULL) THEN 1
							ELSE 0
						END) AS PersonalNums

			FROM RU_Users AS RU WITH(NOLOCK)
			INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
			ON
				(RU.UserID = RR.UserID)
				AND (RR.SeasonID = @SeasonID) --only recruits in season
			INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
			ON
				RR.UserTypeId = RUT.UserTypeID
			INNER JOIN SAE_RecruitTeamMappings AS TMap WITH(NOLOCK)
			ON
				(RR.RecruitID = TMap.RecruitID)
			INNER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				(TMap.TeamID = RT.TeamID)
			LEFT OUTER JOIN SAE_AccountsInstalled AS AI WITH(NOLOCK)
			ON
				(RU.UserID = AI.SalesRepUserID)
				AND (AI.SeasonID = @SeasonID)
				AND ((@ExcludeCancels = 0) OR (AI.Status = 'OK'))
				AND ((@CreditScore IS NULL) OR (AI.CreditScore >= @CreditScore))

			WHERE
				(RUT.RoleLocationID = @RoleLocationID) -- Filter by role location
				AND (RT.RoleLocationID = @RoleLocationID)
				AND ((@TeamID IS NULL) OR (TMap.TeamID = @TeamID))
				AND (AI.SeasonID = @SeasonID)

			GROUP BY
				RU.UserID

		) AS RecruitNums
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(RecruitNums.UserID = RU.UserID)
		INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
		ON
			(RU.UserID = RR.UserID)
			AND (RR.SeasonID = @SeasonID) --only recruits in season
		INNER JOIN SAE_RecruitTeamMappings AS TMap WITH(NOLOCK)
		ON
			(RR.RecruitID = TMap.RecruitID)
		INNER JOIN RU_Teams AS RT WITH(NOLOCK)
		ON
			(TMap.TeamID = RT.TeamID)
		INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
		ON
			(TMap.TeamLocationID = RUTL.TeamLocationID)
	) AS TopLevel
		-----------------------------------------------------------
		-----------------------------------------------------------
		-----------------------------------------------------------
		-----------------------------------------------------------
	LEFT OUTER JOIN
		-----------------------------------------------------------
		-----------------------------------------------------------
		-----------------------------------------------------------
		-----------------------------------------------------------
	(
		SELECT
			RR.ReportsToID

			, SUM(CASE
						WHEN (RU.RecruitedDate > @TodayEnd-7) THEN 1
						ELSE 0
					END) AS Last7DaysTeamRecruits

			, SUM(RecruitNums.Last7DaysNums) AS Last7DaysTeamNums
			, SUM(RecruitNums.Last30DaysNums) AS Last30DaysTeamNums
			, SUM(RecruitNums.PersonalNums) AS TeamNums
			
			, COUNT(*) AS NumRepsWithNums

		FROM
		(
			SELECT
				RU.UserID

				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-7) THEN 1
							ELSE 0
						END) AS Last7DaysNums
				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-30) THEN 1
							ELSE 0
						END) AS Last30DaysNums
				, SUM(CASE
							WHEN (AI.InstallDate IS NOT NULL) THEN 1
							ELSE 0
						END) AS PersonalNums

			FROM RU_Users AS RU WITH(NOLOCK)
			INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
			ON
				(RU.UserID = RR.UserID)
				AND (RR.SeasonID = @SeasonID) --only recruits in season
			INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
			ON
				RR.UserTypeId = RUT.UserTypeID
			INNER JOIN SAE_RecruitTeamMappings AS TMap WITH(NOLOCK)
			ON
				(RR.RecruitID = TMap.RecruitID)
			INNER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				(TMap.TeamID = RT.TeamID)
			LEFT OUTER JOIN SAE_AccountsInstalled AS AI WITH(NOLOCK)
			ON
				(RU.UserID = AI.SalesRepUserID)
				AND (AI.SeasonID = @SeasonID)
				AND ((@ExcludeCancels = 0) OR (AI.Status = 'OK'))
				AND ((@CreditScore IS NULL) OR (AI.CreditScore >= @CreditScore))

			WHERE
				(RUT.RoleLocationID = @RoleLocationID) -- Filter by role location
				AND (RT.RoleLocationID = @RoleLocationID)
				AND ((@TeamID IS NULL) OR (TMap.TeamID = @TeamID))
				AND (AI.SeasonID = @SeasonID)

			GROUP BY
				RU.UserID

		) AS RecruitNums
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(RecruitNums.UserID = RU.UserID)
		INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
		ON
			(RU.UserID = RR.UserID)
			AND (RR.SeasonID = @SeasonID) --only recruits in season
		
		GROUP BY
			RR.ReportsToID
	) AS Peons
	ON
		(TopLevel.RecruitID = Peons.ReportsToID)

	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(TopLevel.RecruitID = RR.RecruitID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		(RR.UserTypeId = RUT.UserTypeID)
		
	WHERE
		RUT.UserTypeTeamTypeID IN (SELECT UserTypeTeamTypeID FROM dbo.GetUserTypeTeamTypes(5))-- Managers
		--RR.UserTypeId IN (2, 11)

	ORDER BY
		TopLevel.TeamID ASC


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamNumbersReport_SalesManagers TO PUBLIC
GO