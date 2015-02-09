USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetAllForSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetAllForSeason'
		DROP  Procedure  dbo.custRU_TeamsGetAllForSeason
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetAllForSeason'
GO
/******************************************************************************
**		File: custRU_TeamsGetAllForSeason.sql
**		Name: custRU_TeamsGetAllForSeason
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
CREATE Procedure dbo.custRU_TeamsGetAllForSeason
(
	@SeasonID INT = NULL
	, @RoleLocationID INT = NULL
)
AS
BEGIN

--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @RoleLocationID INT
--	SET @RoleLocationID = 1

	SELECT
		RT.*
		, RUTL.Description AS OfficeName
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		(RT.TeamLocationID = RUTL.TeamLocationID)
	WHERE
		(@SeasonID IS NULL OR RUTL.SeasonID = @SeasonID)
		AND (@RoleLocationID IS NULL OR RT.RoleLocationID = @RoleLocationID)
		AND RT.IsDeleted = 0
	ORDER BY
		RT.[Description]

END
GO

GRANT EXEC ON dbo.custRU_TeamsGetAllForSeason TO PUBLIC
GO