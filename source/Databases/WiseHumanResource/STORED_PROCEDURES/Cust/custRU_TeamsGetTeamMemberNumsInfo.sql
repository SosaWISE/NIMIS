USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamMemberNumsInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamMemberNumsInfo'
		DROP  Procedure  dbo.custRU_TeamsGetTeamMemberNumsInfo
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamMemberNumsInfo'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamMemberNumsInfo.sql
**		Name: custRU_TeamsGetTeamMemberNumsInfo
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
CREATE Procedure dbo.custRU_TeamsGetTeamMemberNumsInfo
(@TeamID INT
	, @HasOwnTeam BIT = NULL -- (NULL, 0, 1)
	, @IsDeleted BIT = NULL -- (NULL, 0, 1)
)
AS
BEGIN


------TESTING
--	DECLARE @TeamID INT
--	SET @TeamID = 885
	
--	DECLARE @HasOwnTeam BIT
--	SET @HasOwnTeam = NULL

--	DECLARE @IsDeleted BIT
--	SET @IsDeleted = 0
------TESTING

	--DECLARE @CreditScore INT
	--SET @CreditScore = 600


	DECLARE @TodayEnd DATETIME
	SET @TodayEnd = dbo.GetDateEnd(GETDATE())

	DECLARE @SeasonID INT
	DECLARE @RoleLocationID INT
	SELECT
		@SeasonID = SeasonID
		, @RoleLocationID = RT.RoleLocationID
	FROM RU_Teams AS RT
	INNER JOIN RU_TeamLocations AS RTL
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	WHERE
		RT.TeamID = @TeamID

	SELECT
		RU.UserID
		, RU.FullName
		, RUT.Description
		, PS.Name AS PayScale
		, RR.IsDeleted
		, RR.TeamID
		
		, COALESCE(RecruitNums.Last7DaysNums, 0) AS Last7DaysNums
		, COALESCE(RecruitNums.Last30DaysNums, 0) AS Last30DaysNums
		, COALESCE(RecruitNums.TotalPersonalNums, 0) AS TotalPersonalNums
		
	FROM SAE_RecruitingStructure AS TMap WITH(NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(TMap.RecruitID = RR.RecruitID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		RR.UserTypeId = RUT.UserTypeID
	INNER JOIN RU_Payscales AS PS WITH(NOLOCK)
	ON
		(RR.PayscaleID = PS.PayscaleID)
	INNER JOIN RU_Users AS RU WITH(NOLOCK)
	ON
		(RR.UserID = RU.UserID)
	LEFT OUTER JOIN
	(
		SELECT
			(CASE
				WHEN @RoleLocationID = 2 THEN AI.TechnicianUserID
				ELSE AI.SalesRepUserID
			END) AS UserID
					
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
					END) AS TotalPersonalNums
					
		FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
		WHERE
			(AI.SeasonID = @SeasonID)
			--AND (AI.Status = 'OK')--Doesn't matter if RoleLocationID is 2
			--AND (AI.CreditScore >= @CreditScore)--Doesn't matter if RoleLocationID is 2

		GROUP BY
			(CASE
				WHEN @RoleLocationID = 2 THEN AI.TechnicianUserID
				ELSE AI.SalesRepUserID
			END)

	) AS RecruitNums
	ON
		RU.UserID = RecruitNums.UserID
	WHERE
		(TMap.TeamID = @TeamID)
		AND (@IsDeleted IS NULL OR RR.IsDeleted = @IsDeleted)
		AND (@HasOwnTeam IS NULL
				OR (@HasOwnTeam = 1 AND RR.TeamID IS NOT NULL)
				OR (@HasOwnTeam = 0 AND RR.TeamID IS NULL)
			)

----Order By
	ORDER BY
		RecruitNums.TotalPersonalNums DESC
		, RU.FullName
----Order By

		
END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamMemberNumsInfo TO PUBLIC
GO