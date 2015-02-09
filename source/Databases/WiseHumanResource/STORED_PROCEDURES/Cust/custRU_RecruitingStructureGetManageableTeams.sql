USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitingStructureGetManageableTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitingStructureGetManageableTeams'
		DROP  Procedure  dbo.custRU_RecruitingStructureGetManageableTeams
	END
GO

PRINT 'Creating Procedure custRU_RecruitingStructureGetManageableTeams'
GO
/******************************************************************************
**		File: custRU_RecruitingStructureGetManageableTeams.sql
**		Name: custRU_RecruitingStructureGetManageableTeams
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
CREATE Procedure dbo.custRU_RecruitingStructureGetManageableTeams
(
	@SeasonID INT
	, @RoleLocationID INT
	, @RecruitID INT
	, @TeamLocationID INT = NULL
)
AS
BEGIN


SELECT
*
FROM RU_Teams AS RT WITH(NOLOCK)
WHERE
	RT.TeamID IN
	(
		SELECT DISTINCT
			RT.TeamID
		FROM VW_RecruitingStructure AS RS WITH(NOLOCK)
		INNER JOIN RU_Teams AS RT WITH(NOLOCK)
		ON
			RS.TeamID = RT.TeamID
		WHERE
			(
				--Everyone that can manage a team
				(ManagerID = @RecruitID) OR
				(RegionID = @RecruitID) OR
				(NationalRegionID = @RecruitID)
			)
			AND (RS.SeasonID = @SeasonID)
			AND (RT.IsDeleted = 0)
			AND (RT.RoleLocationID = @RoleLocationID)
			AND (@TeamLocationID IS NULL OR RT.TeamLocationID = @TeamLocationID)
	)



END
GO

GRANT EXEC ON dbo.custRU_RecruitingStructureGetManageableTeams TO PUBLIC
GO