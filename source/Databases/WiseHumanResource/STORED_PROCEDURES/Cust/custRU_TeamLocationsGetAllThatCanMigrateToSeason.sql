USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetAllThatCanMigrateToSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetAllThatCanMigrateToSeason'
		DROP  Procedure  dbo.custRU_TeamLocationsGetAllThatCanMigrateToSeason
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetAllThatCanMigrateToSeason'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetAllThatCanMigrateToSeason.sql
**		Name: custRU_TeamLocationsGetAllThatCanMigrateToSeason
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
CREATE Procedure dbo.custRU_TeamLocationsGetAllThatCanMigrateToSeason
(
	@MigrateToSeasonID INT
	, @ExcludeOfficesAlreadyInSeason BIT
)
AS
BEGIN


	SELECT
		*
	FROM
	(
		SELECT
			*
			, ROW_NUMBER() OVER (PARTITION BY TeamLocationID ORDER BY SeasonID DESC) RowNumber
		FROM RU_TeamLocations AS RUTL WITH(NOLOCK)
		WHERE
			(RUTL.IsActive = 1)
			AND (RUTL.IsDeleted = 0)
	) AS RUTL
	WHERE
		(RUTL.RowNumber = 1)-- Most recent season
		AND (@ExcludeOfficesAlreadyInSeason = 1 AND RUTL.SeasonID <> @MigrateToSeasonID)--exclude those already in the current season
	ORDER BY
		RUTL.Description
	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetAllThatCanMigrateToSeason TO PUBLIC
GO