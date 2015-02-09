USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetManagersTeamIDForSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetManagersTeamIDForSeason'
		DROP  Procedure  dbo.custRU_UsersGetManagersTeamIDForSeason
	END
GO

PRINT 'Creating Procedure custRU_UsersGetManagersTeamIDForSeason'
GO
/******************************************************************************
**		File: custRU_UsersGetManagersTeamIDForSeason.sql
**		Name: custRU_UsersGetManagersTeamIDForSeason
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
CREATE Procedure dbo.custRU_UsersGetManagersTeamIDForSeason
(
	@UserID INT
	, @SeasonID INT
)
AS
BEGIN
	SELECT
		RR.TeamID 
	FROM
		RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			RR.UserId = RU.UserId
	WHERE
		(RR.IsActive = 1) AND (RR.IsDeleted = 0)
		AND (RU.UserID = @UserID) -- Filter by UserID
		AND (RR.SeasonID = @SeasonID) -- Filter by SeasonID
END
GO

GRANT EXEC ON dbo.custRU_UsersGetManagersTeamIDForSeason TO PUBLIC
GO