USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamsInSeasonByDescription')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamsInSeasonByDescription'
		DROP  Procedure  dbo.custRU_TeamsGetTeamsInSeasonByDescription
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamsInSeasonByDescription'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamsInSeasonByDescription.sql
**		Name: custRU_TeamsGetTeamsInSeasonByDescription
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
CREATE Procedure dbo.custRU_TeamsGetTeamsInSeasonByDescription
(
	@SeasonID INT
	, @Description NVARCHAR(100)
)
AS
BEGIN

	SELECT
		*
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		(RT.TeamLocationID = RUTL.TeamLocationID)
	WHERE
		(RUTL.SeasonID = @SeasonID)
		AND ((@Description IS NULL) OR (RT.Description = @Description))
	ORDER BY
		RT.Description

END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamsInSeasonByDescription TO PUBLIC
GO