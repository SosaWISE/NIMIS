USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UserGetManagerBySeasonId')
	BEGIN
		PRINT 'Dropping Procedure custRU_UserGetManagerBySeasonId'
		DROP  Procedure  dbo.custRU_UserGetManagerBySeasonId
	END
GO

PRINT 'Creating Procedure custRU_UserGetManagerBySeasonId'
GO
/******************************************************************************
**		File: custRU_UserGetManagerBySeasonId.sql
**		Name: custRU_UserGetManagerBySeasonId
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
CREATE Procedure dbo.custRU_UserGetManagerBySeasonId
(@SeasonId INT
)
AS
BEGIN
	-- Run Query
	SELECT 
		RR.RecruitId
		, RRL.Role
		, RUT.Description AS [UserType]
		, RU.*
	FROM 
		[RU_Users] AS RU WITH (NOLOCK)
		INNER JOIN [RU_Recruits] AS RR WITH (NOLOCK)
		ON
			RU.UserID = RR.UserID
			AND (RU.IsActive = 1)
			AND (RU.IsDeleted = 0)
		INNER JOIN [RU_UserType] AS RUT WITH (NOLOCK)
		ON
			RR.UserTypeID = RUT.UserTypeID
			AND (RR.IsActive = 1)
			AND (RR.IsDeleted = 0)
			AND (RUT.SecurityLevel >= 5)
		INNER JOIN [RU_RoleLocations] AS RRL WITH (NOLOCK)
		ON
			RUT.RoleLocationID = RRL.RoleLocationID
			AND (RRL.Role <> 'Corporate')
	WHERE
		(RR.SeasonID = @SeasonId)
		AND (RR.TeamID IS NULL)
		AND (RR.IsActive = 1)
		AND (RR.IsDeleted = 0)
	ORDER BY
		RU.FullName
END
GO

GRANT EXEC ON dbo.custRU_UserGetManagerBySeasonId TO PUBLIC
GO