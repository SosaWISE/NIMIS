USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamInLocationAndSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamInLocationAndSeason'
		DROP  Procedure  dbo.custRU_TeamsGetTeamInLocationAndSeason
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamInLocationAndSeason'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamInLocationAndSeason.sql
**		Name: custRU_TeamsGetTeamInLocationAndSeason
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
CREATE Procedure dbo.custRU_TeamsGetTeamInLocationAndSeason
(
	@SeasonID INT
	, @TeamName NVARCHAR(100)
	, @TeamLocationName NVARCHAR(100)
)
AS
BEGIN

--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @TeamName NVARCHAR(100)
--	SET @TeamName = 'Office Staff'
--
--	DECLARE @TeamLocationName NVARCHAR(100)
--	SET @TeamLocationName = 'Atlanta 1'

	SELECT
		RT.*
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		(RT.TeamLocationID = RUTL.TeamLocationID)
	WHERE
		(RUTL.SeasonID = @SeasonID)
		AND (RT.Description = @TeamName)
		AND (RUTL.Description = @TeamLocationName)

END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamInLocationAndSeason TO PUBLIC
GO