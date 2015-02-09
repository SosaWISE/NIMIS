USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetManagingTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetManagingTeams'
		DROP  Procedure  dbo.custRU_TeamsGetManagingTeams
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetManagingTeams'
GO
/******************************************************************************
**		File: custRU_TeamsGetManagingTeams.sql
**		Name: custRU_TeamsGetManagingTeams
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
CREATE Procedure dbo.custRU_TeamsGetManagingTeams
(
	@UserID INT
	, @SeasonID INT = NULL
)
AS
BEGIN

	--DECLARE @SeasonID INT
	--SET @SeasonID = 11

	--DECLARE @UserID INT
	----SET @UserID = 4146 --??
	----SET @UserID = 6697 --me

	----SET @UserID = 4702 -- Jonathan Hernandez
	--SET @UserID = 4146 -- Neal Rogers
	--SET @UserID = 4233 -- Bubba


	DECLARE @RecruitIDs TABLE(RecruitID INT)
	INSERT INTO @RecruitIDs
	SELECT
		RecruitID
	FROM vw_RecruitUser AS RecUser
	WHERE
		((@SeasonID IS NULL) OR (RecUser.SeasonID = @SeasonID))
		AND RecUser.UserID = @UserID
		--AND RecUser.IsDeleted = 0


	SELECT DISTINCT
		RT.*
		, RTL.Description AS OfficeName
	FROM SAE_RecruitingStructure AS Struct WITH (NOLOCK)
	INNER JOIN RU_Teams AS RT WITH (NOLOCK)
	ON
		Struct.TeamID = RT.TeamID

	--HACK FOR: Recruit.SeasonID <> Manager.Team.TeamLocation.SeasonID
	INNER JOIN RU_TeamLocations AS RTL WITH (NOLOCK)
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	WHERE
		(
			ManagerID IN (SELECT * FROM @RecruitIDs)
			OR RegionID IN (SELECT * FROM @RecruitIDs)
			OR NationalRegionID IN (SELECT * FROM @RecruitIDs)
		)
		AND ((@SeasonID IS NULL) OR (RTL.SeasonID = @SeasonID)) --@SeasonID is optional

	ORDER BY
		RT.Description


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetManagingTeams TO PUBLIC
GO