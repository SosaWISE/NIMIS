USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetViewableTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetViewableTeams'
		DROP  Procedure  dbo.custRU_TeamsGetViewableTeams
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetViewableTeams'
GO
/******************************************************************************
**		File: custRU_TeamsGetViewableTeams.sql
**		Name: custRU_TeamsGetViewableTeams
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
CREATE Procedure dbo.custRU_TeamsGetViewableTeams
(
	
	@UserID INT
	, @SeasonID INT = NULL
)
AS
BEGIN

--	DECLARE @SeasonID INT
--	--SET @SeasonID = 6 --??
--	SET @SeasonID = 7
--
--	DECLARE @UserID INT
--	SET @UserID = 6697 --me
--	--SET @UserID = 4702 -- Jonathan Hernandez
--	SET @UserID = 4146 -- Neal Rogers

	SELECT DISTINCT
		RT.*
	FROM
	(
		-- On Teams
		SELECT
			RT.*
		FROM dbo.GetReportingTree(NULL, NULL, NULL, NULL, @SeasonID) AS Tree
		INNER JOIN RU_Teams AS RT WITH (NOLOCK)
		ON
			Tree.TeamID = RT.TeamID
		WHERE
			Tree.UserID = @UserID

		UNION ALL

		-- Managing Teams
		SELECT
			RT.*
		FROM dbo.GetReportingTree('UserID', @UserID, NULL, NULL, @SeasonID) AS Tree
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			Tree.UserID = RU.UserID
		INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
		ON
			Tree.RecruitID = RR.RecruitID
		INNER JOIN RU_Teams AS RT WITH (NOLOCK)
		ON
			Tree.TeamID = RT.TeamID
		WHERE
			(RU.IsActive = 1 AND RU.IsDeleted = 0)
			AND (RR.IsActive = 1 AND RR.IsDeleted = 0)			
	) AS RT
	INNER JOIN RU_TeamLocations AS RUTL WITH (NOLOCK)
	ON
		RT.TeamLocationID = RUTL.TeamLocationID
	WHERE
		((@SeasonID IS NULL) OR (RUTL.SeasonID = @SeasonID)) --@SeasonID is optional


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetViewableTeams TO PUBLIC
GO