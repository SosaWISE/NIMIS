USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationGetManagersByTeamLocation')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationGetManagersByTeamLocation'
		DROP  Procedure  dbo.custRU_TeamLocationGetManagersByTeamLocation
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationGetManagersByTeamLocation'
GO
/******************************************************************************
**		File: custRU_TeamLocationGetManagersByTeamLocation.sql
**		Name: custRU_TeamLocationGetManagersByTeamLocation
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
CREATE Procedure dbo.custRU_TeamLocationGetManagersByTeamLocation
(
	@TeamLocationID INT
	, @RoleLocationID INT
)
AS
BEGIN

	SELECT
		RU.*
	FROM
		RU_Teams AS TM
		INNER JOIN RU_Recruits AS RR
		ON
			(RR.TeamID = TM.TeamID)
		INNER JOIN VW_RecruitUser AS RU
		ON
			(RU.RecruitID = RR.RecruitID)
		INNER JOIN RU_UserType AS RUT
		ON
			(RU.UserTypeID = RUT.UserTypeID)
		INNER JOIN RU_UserTypeTeamTypes AS RUTTT
		ON
			(RU.UserTypeTeamTypeID = RUTTT.UserTypeTeamTypeID)
	WHERE
		(TM.TeamLocationID = @TeamLocationID)
		AND (TM.RoleLocationID = @RoleLocationID)
		AND (RUT.RoleLocationID = @RoleLocationID)
		AND (RUTTT.UserTypeTeamTypeID = 5 OR RUTTT.ParentID = 5) -- 5 = Team Manager
		AND (RR.IsActive = 1 AND RR.IsDeleted = 0)
		AND (RU.IsActiveUser = 1 AND RU.IsDeletedUser = 0)
	
END

GO

GRANT EXEC ON dbo.custRU_TeamLocationGetManagersByTeamLocation TO PUBLIC
GO