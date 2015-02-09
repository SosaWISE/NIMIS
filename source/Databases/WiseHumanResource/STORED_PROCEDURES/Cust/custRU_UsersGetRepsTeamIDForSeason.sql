USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetRepsTeamIDForSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetRepsTeamIDForSeason'
		DROP  Procedure  dbo.custRU_UsersGetRepsTeamIDForSeason
	END
GO

PRINT 'Creating Procedure custRU_UsersGetRepsTeamIDForSeason'
GO
/******************************************************************************
**		File: custRU_UsersGetRepsTeamIDForSeason.sql
**		Name: custRU_UsersGetRepsTeamIDForSeason
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
CREATE Procedure dbo.custRU_UsersGetRepsTeamIDForSeason
(
	@UserID INT
	, @SeasonID INT
)
AS
BEGIN
	SELECT
		MAN.TeamID 
	FROM
		RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			RR.UserId = RU.UserId
		INNER JOIN RU_Recruits AS MAN WITH (NOLOCK) -- Find Manager
		ON 
			RR.ReportsToID = MAN.RecruitID 
	WHERE
		(RR.IsActive = 1) AND (RR.IsDeleted = 0)
		AND (RU.UserID = @UserID) -- Filter by UserID
		AND (RR.SeasonID = @SeasonID) -- Filter by SeasonID
END
GO

GRANT EXEC ON dbo.custRU_UsersGetRepsTeamIDForSeason TO PUBLIC
GO