USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetAllThatCanMigrateToSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetAllThatCanMigrateToSeason'
		DROP  Procedure  dbo.custRU_TeamsGetAllThatCanMigrateToSeason
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetAllThatCanMigrateToSeason'
GO
/******************************************************************************
**		File: custRU_TeamsGetAllThatCanMigrateToSeason.sql
**		Name: custRU_TeamsGetAllThatCanMigrateToSeason
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
CREATE Procedure dbo.custRU_TeamsGetAllThatCanMigrateToSeason
(
	@PreviousSeasonID INT
	, @MigrateToSeasonID INT
	, @ExcludeOfficesAlreadyInSeason BIT
)
AS
BEGIN


	SELECT
		*
	FROM RU_Teams AS RT WITH(NOLOCK)
	WHERE
		RT.TeamID IN
		(
			SELECT
				RT.TeamID
			FROM VW_Teams AS RT WITH(NOLOCK)
			WHERE
				RT.SeasonID = @PreviousSeasonID
				AND
				(	@ExcludeOfficesAlreadyInSeason = 0
					OR RT.TeamID NOT IN
					(
						--Teams without a child in the migrate to season
						SELECT
							RT.CreatedFromTeamID
						FROM VW_Teams AS RT WITH(NOLOCK)
						WHERE
							(RT.SeasonID = @MigrateToSeasonID)
							AND (RT.CreatedFromTeamID IS NOT NULL)
					)
				)
		)
		AND RT.IsDeleted = 0
	ORDER BY
		RT.Description


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetAllThatCanMigrateToSeason TO PUBLIC
GO