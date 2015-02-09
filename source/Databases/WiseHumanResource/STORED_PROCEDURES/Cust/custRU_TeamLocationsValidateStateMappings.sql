USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsValidateStateMappings')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsValidateStateMappings'
		DROP  Procedure  dbo.custRU_TeamLocationsValidateStateMappings
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsValidateStateMappings'
GO
/******************************************************************************
**		File: custRU_TeamLocationsValidateStateMappings.sql
**		Name: custRU_TeamLocationsValidateStateMappings
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
CREATE Procedure dbo.custRU_TeamLocationsValidateStateMappings
(
	@TeamLocationID INT
	, @StateAB CHAR(2)
)
AS
BEGIN
	-- Search for a hit on the mappings table
	SELECT
		TSM.StateMappingID
		, TSM.TeamLocationId
		, TSM.SeasonId
		, TSM.StateId
		, PS.StateAB
	FROM
		RU_TeamLocationStateMappings AS TSM WITH (NOLOCK)
		INNER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState AS PS WITH (NOLOCK)
		ON
			(TSM.StateId = PS.StateID)
		INNER JOIN RU_TeamLocations AS RTL WITH (NOLOCK)
		ON
			(TSM.TeamLocationID = RTL.TeamLocationID)
			AND (RTL.IsActive = 1)
			AND (RTL.IsDeleted = 0)
	WHERE
		(TSM.TeamLocationId = @TeamLocationID)
		AND (PS.StateAB = @StateAB)
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsValidateStateMappings TO PUBLIC
GO