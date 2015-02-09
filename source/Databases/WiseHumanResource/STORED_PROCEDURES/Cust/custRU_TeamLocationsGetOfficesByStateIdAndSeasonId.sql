USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficesByStateIdAndSeasonId')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficesByStateIdAndSeasonId'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficesByStateIdAndSeasonId
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficesByStateIdAndSeasonId'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficesByStateIdAndSeasonId.sql
**		Name: custRU_TeamLocationsGetOfficesByStateIdAndSeasonId
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficesByStateIdAndSeasonId
(
	@StateId INT
	, @SeasonId INT
)
AS
BEGIN

	-- Query
	SELECT
		RT.*
	FROM
		RU_TeamLocationStateMappings AS TSM WITH (NOLOCK)
		INNER JOIN RU_TeamLocations AS RT WITH (NOLOCK)
		ON
			TSM.TeamLocationId = RT.TeamLocationID
	WHERE
		(TSM.StateID = @StateID)
		AND (RT.SeasonID = @SeasonID)
		AND (RT.IsDeleted = 0)
	ORDER BY
		RT.Description DESC
	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficesByStateIdAndSeasonId TO PUBLIC
GO