USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetUserInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetUserInfo'
		DROP  Procedure  dbo.custRU_UsersGetUserInfo
	END
GO

PRINT 'Creating Procedure custRU_UsersGetUserInfo'
GO
/******************************************************************************
**		File: custRU_UsersGetUserInfo.sql
**		Name: custRU_UsersGetUserInfo
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
CREATE Procedure dbo.custRU_UsersGetUserInfo
(
	@UserID INT
)
AS
BEGIN

--	DECLARE @UserID INT
--	SET @UserID = 5152 --Andrés
--	SET @UserID = 4146 --Neal Rogers
--	--SET @UserID = 4233
--	SET @UserID = 4702 --Jonathan Hernandez

	DECLARE @Tree TABLE
	(
		UserID INT 
		, SeasonID INT
		, RecruitID INT
		, TeamID INT 
	)
	INSERT INTO @Tree
	SELECT DISTINCT
		Tree.UserID
		, Tree.SeasonID
		, Tree.RecruitID
		, Tree.TeamID
	FROM
	(
		--Get the teams the user manages
		SELECT
			Tree.*
		FROM GetReportingTree('UserID', @UserID, NULL, NULL, NULL) AS Tree
		INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
		ON
			Tree.SeasonID = RR.SeasonID
		WHERE
			(RR.UserID = @UserID)

		UNION

		--Get the teams that the user is on because of who the user reports to
		SELECT
			Tree.*
		FROM GetReportingTree('ReportingLevel', NULL, NULL, NULL, NULL) AS Tree
		WHERE
			UserID = @UserID
	) AS Tree


	DECLARE @DistinctTeams TABLE
	(
		TeamID INT
	)
	INSERT INTO @DistinctTeams
	SELECT DISTINCT TeamID FROM @Tree



	--Table 0 - User
	SELECT TOP 1
		*
	FROM RU_Users WITH(NOLOCK)
	WHERE
		UserID = @UserID

	--Table 1 - User Recruits
	SELECT DISTINCT
		RR.*
	FROM RU_Recruits AS RR WITH (NOLOCK)
	INNER JOIN @Tree AS AT
	ON
		RR.RecruitID = AT.RecruitID
	WHERE
		RR.UserID = @UserID
		AND (RR.IsActive = 1 AND RR.IsDeleted = 0)

	--Table 2 - User Teams/Team Locations
	SELECT DISTINCT
		RT.*
		, RTL.SeasonID
	FROM RU_Teams AS RT WITH (NOLOCK)
	INNER JOIN RU_TeamLocations RTL WITH (NOLOCK)
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	INNER JOIN @DistinctTeams AS DT
	ON
		RT.TeamID = DT.TeamID
	WHERE
		(RT.IsActive = 1 AND RT.IsDeleted = 0)

END
GO

GRANT EXEC ON dbo.custRU_UsersGetUserInfo TO PUBLIC
GO