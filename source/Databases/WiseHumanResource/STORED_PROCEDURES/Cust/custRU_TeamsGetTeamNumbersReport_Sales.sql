USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamNumbersReport_Sales')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamNumbersReport_Sales'
		DROP  Procedure  dbo.custRU_TeamsGetTeamNumbersReport_Sales
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamNumbersReport_Sales'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamNumbersReport_Sales.sql
**		Name: custRU_TeamsGetTeamNumbersReport_Sales
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
CREATE Procedure dbo.custRU_TeamsGetTeamNumbersReport_Sales
(
	@SeasonID INT
	, @ExcludeCancels BIT = 0
	, @CreditScore INT = NULL
)
AS
BEGIN

--	DECLARE @SeasonID INT
--	SET @SeasonID = 7
--
--DECLARE @ExcludeCancels BIT
--SET @ExcludeCancels = 1
--
--DECLARE @CreditScore INT
--SET @CreditScore = 600

	
	DECLARE @RoleLocationID INT
	SET @RoleLocationID = 1

	DECLARE @TodayEnd DATETIME
	SET @TodayEnd = dbo.GetDateEnd(GETDATE())


	SELECT
		RT.Description AS Name
		, Nums.*
		, COALESCE(TeamMemberCount.TotalRecruits, 0) AS TotalRecruits
	FROM
	(
		SELECT
			TMap.TeamID AS ID

			, SUM(CASE
						WHEN (RU.RecruitedDate > @TodayEnd-7) THEN 1
						ELSE 0
					END) AS Last7DaysRecruits
			
			, SUM(RecruitNums.Last7DaysNums) AS Last7DaysNums
			, SUM(RecruitNums.Last30DaysNums) AS Last30DaysNums
			, SUM(RecruitNums.PersonalNums) AS PersonalNums
			, SUM(RecruitNums.TeamNums) AS TeamNums
			, SUM(RecruitNums.TotalOfficeNums) AS TotalOfficeNums


			, SUM(CASE
						WHEN (
								(RecruitNums.TotalOfficeNums > 0)
								AND (RU_NotDeleted.UserID IS NOT NULL)
							) THEN 1
						ELSE 0
					END) AS NumRepsWithNums
		FROM
		(
			SELECT
				RU.UserID

				--Manager Numbers
				, SUM(CASE
							WHEN ((RR.TeamID IS NOT NULL) AND (AI.InstallDate > @TodayEnd-7)) THEN 1
							ELSE 0
						END) AS Last7DaysNums
				, SUM(CASE
							WHEN ((RR.TeamID IS NOT NULL) AND (AI.InstallDate > @TodayEnd-30)) THEN 1
							ELSE 0
						END) AS Last30DaysNums
				, SUM(CASE
							WHEN ((RR.TeamID IS NOT NULL) AND (AI.InstallDate IS NOT NULL)) THEN 1
							ELSE 0
						END) AS PersonalNums

				--Rep Numbers
				, SUM(CASE
							WHEN ((RR.TeamID IS NULL) AND (AI.InstallDate IS NOT NULL)) THEN 1
							ELSE 0
						END) AS TeamNums
						
				--Total Numbers
				, COUNT(AI.AccountID) AS TotalOfficeNums
				
			FROM RU_Users AS RU WITH(NOLOCK)
			INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
			ON
				(RU.UserID = RR.UserID)
				AND (RR.SeasonID = @SeasonID) --only recruits in season
			INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
			ON
				RR.UserTypeId = RUT.UserTypeID
			LEFT OUTER JOIN SAE_AccountsInstalled AS AI WITH(NOLOCK)
			ON
				(RU.UserID = AI.SalesRepUserID)
				AND (AI.SeasonID = @SeasonID)
				--AND (AI.Status = 'OK')
				--AND (AI.CreditScore >= 600)
				AND ((@ExcludeCancels = 0) OR (AI.Status = 'OK'))
				AND ((@CreditScore IS NULL) OR (AI.CreditScore >= @CreditScore))
				
			WHERE
				(RUT.RoleLocationID = @RoleLocationID) -- Filter by role location
				--AND (RU.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
				--AND (RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
				--AND RR.RecruitID = 2337

			GROUP BY
				RU.UserID

		) AS RecruitNums
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(RecruitNums.UserID = RU.UserID)
		LEFT OUTER JOIN RU_Users AS RU_NotDeleted WITH(NOLOCK)
		ON
			(RecruitNums.UserID = RU_NotDeleted.UserID)
			AND (RU_NotDeleted.IsDeleted = 0)
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

		WHERE
			(RT.IsActive = 1 AND RT.IsDeleted = 0)
			AND (RT.RoleLocationID = @RoleLocationID)
			
			--AND (RUTL.IsActive = 1 AND RUTL.IsDeleted = 0)
			--AND (RUTL.IsDeleted = 0)
			AND (RUTL.SeasonID = @SeasonID)

		GROUP BY
			TMap.TeamID
	) AS Nums
	INNER JOIN RU_Teams AS RT WITH(NOLOCK)
	ON
		(Nums.ID = RT.TeamID)
	LEFT OUTER JOIN
	(
		SELECT
			TMap.TeamID
			, COUNT(*) AS TotalRecruits
		FROM SAE_RecruitTeamMappings AS TMap WITH(NOLOCK)
		INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
		ON
			(TMap.RecruitID = RR.RecruitID)
			AND (RR.IsDeleted = 0)
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(RR.UserID = RU.UserID)
			AND (RU.IsDeleted = 0)
		INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
		ON
			(RR.UserTypeId = RUT.UserTypeID)
		WHERE
			RUT.RoleLocationID = @RoleLocationID
		GROUP BY
			TMap.TeamID
	) AS TeamMemberCount
	ON
		(RT.TeamID = TeamMemberCount.TeamID)

	ORDER BY
		RT.Description


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamNumbersReport_Sales TO PUBLIC
GO