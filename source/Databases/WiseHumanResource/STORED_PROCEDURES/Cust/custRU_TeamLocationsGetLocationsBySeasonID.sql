USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetLocationsBySeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetLocationsBySeasonID'
		DROP  Procedure  dbo.custRU_TeamLocationsGetLocationsBySeasonID
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetLocationsBySeasonID'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetLocationsBySeasonID.sql
**		Name: custRU_TeamLocationsGetLocationsBySeasonID
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
CREATE Procedure dbo.custRU_TeamLocationsGetLocationsBySeasonID
(
	@SeasonID INT = NULL
)
AS
BEGIN

	SELECT	RTL.TeamLocationID
		, RTL.Description
	FROM RU_TeamLocations RTL
	WHERE RTL.SeasonID = @SeasonID 
		AND RTL.TeamLocationID <> 114 --Excluding the [Default Offices] team

	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetLocationsBySeasonID TO PUBLIC
GO