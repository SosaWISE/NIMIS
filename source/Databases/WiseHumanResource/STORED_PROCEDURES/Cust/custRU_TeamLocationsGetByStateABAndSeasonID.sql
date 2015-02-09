USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetByStateABAndSeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetByStateABAndSeasonID'
		DROP  Procedure  dbo.custRU_TeamLocationsGetByStateABAndSeasonID
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetByStateABAndSeasonID'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetByStateABAndSeasonID.sql
**		Name: custRU_TeamLocationsGetByStateABAndSeasonID
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
**		Auth: Andres Sosa
**		Date: 12/04/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	-----------		-------------------------------------------
**	12/04/2013	Andres Sosa		Created By
**			
*******************************************************************************/
CREATE Procedure dbo.custRU_TeamLocationsGetByStateABAndSeasonID
(
	@StateAB CHAR(2)
	, @SeasonID INT
)
AS
BEGIN
	-- Search for a hit on the mappings table
	SELECT
		RTL.*
	FROM
		RU_TeamLocationStateMappings AS TSM WITH (NOLOCK)
		INNER JOIN WISE_CRM.dbo.MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(TSM.StateId = PS.StateID)
		INNER JOIN RU_TeamLocations AS RTL WITH (NOLOCK)
		ON
			(TSM.TeamLocationID = RTL.TeamLocationID)
	WHERE
		(PS.StateAB = @StateAB)
		AND (RTL.SeasonID = @SeasonID)
	ORDER BY
		RTL.[Description] DESC
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetByStateABAndSeasonID TO PUBLIC
GO