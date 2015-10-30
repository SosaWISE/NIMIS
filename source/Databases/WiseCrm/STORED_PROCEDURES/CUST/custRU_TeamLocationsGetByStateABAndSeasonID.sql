USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetByStateABAndSeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetByStateABAndSeasonID'
		DROP  Procedure  dbo.custRU_TeamLocationsGetByStateABAndSeasonID
	END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
PRINT 'Creating SPROC dbo.custRU_TeamLocationsGetByStateABAndSeasonID';
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE PROCEDURE [dbo].[custRU_TeamLocationsGetByStateABAndSeasonID]
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
		INNER JOIN dbo.MC_PoliticalStates AS PS WITH (NOLOCK)
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


