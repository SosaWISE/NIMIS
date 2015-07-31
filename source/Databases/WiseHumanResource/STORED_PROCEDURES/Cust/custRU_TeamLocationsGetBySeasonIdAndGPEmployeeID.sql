USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID'
		DROP  Procedure  dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID.sql
**		Name: custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID
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
**		Date: 02/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------------	-------------------------------------------
**	02/05/2014	Andres Sosa			Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID
(
	@SeasonId INT
	, @GPEmployeeId NVARCHAR(25)
)
AS
BEGIN

	IF EXISTS(SELECT
			RT.*
		FROM
			[dbo].[RU_Season] AS RS WITH (NOLOCK)
			INNER JOIN [dbo].[RU_Recruits] AS RR WITH (NOLOCK)
			ON
				(RR.SeasonId = RS.SeasonID)
			INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
			ON
				(RU.UserID = RR.UserId)
			INNER JOIN [dbo].[RU_Teams] AS RUT WITH (NOLOCK)
			ON
				(RUT.TeamID = RR.TeamId)
			INNER JOIN [dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
			ON
				(RT.TeamLocationID = RUT.TeamLocationId)
				AND (RT.SeasonID = RS.SeasonID)
		WHERE
			(RU.GPEmployeeId = @GPEmployeeID)
			AND (RS.SeasonID = @SeasonID))
	BEGIN
		SELECT
			RT.*
		FROM
			[dbo].[RU_Season] AS RS WITH (NOLOCK)
			INNER JOIN [dbo].[RU_Recruits] AS RR WITH (NOLOCK)
			ON
				(RR.SeasonId = RS.SeasonID)
			INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
			ON
				(RU.UserID = RR.UserId)
			INNER JOIN [dbo].[RU_Teams] AS RUT WITH (NOLOCK)
			ON
				(RUT.TeamID = RR.TeamId)
			INNER JOIN [dbo].[RU_TeamLocations] AS RT WITH (NOLOCK)
			ON
				(RT.TeamLocationID = RUT.TeamLocationId)
				AND (RT.SeasonID = RS.SeasonID)
		WHERE
			(RU.GPEmployeeId = @GPEmployeeID)
			AND (RS.SeasonID = @SeasonID);
	END 
	ELSE
	BEGIN
		
		SELECT TOP 1
			RT.*
		FROM
			dbo.RU_Users AS RU WITH (NOLOCK)
			INNER JOIN dbo.RU_Recruits AS RR WITH (NOLOCK)
			ON
				(RR.UserId = RU.UserID)
				AND (RU.GPEmployeeID = @GPEmployeeId)
				AND (RR.IsActive = 1 AND RR.IsDeleted = 0)
				AND (RU.IsActive = 1 AND RU.IsDeleted = 0)
			INNER JOIN dbo.RU_Season AS RS WITH (NOLOCK)
			ON
				(RR.SeasonId = RS.SeasonID)
				AND (RS.IsActive = 1 AND RS.IsDeleted = 0)
			INNER JOIN dbo.RU_TeamLocations AS RT WITH (NOLOCK)
			ON
				(RT.SeasonID = RS.SeasonID)
				AND (RS.SeasonID = @SeasonID)
				AND (RT.IsActive = 1 AND RT.IsDeleted = 0)
			LEFT OUTER JOIN [dbo].[RU_SeasonTeamLocationDefaults] AS RUSTD WITH (NOLOCK)
			ON
				(RS.SeasonID = RUSTD.SeasonID)
				AND (RT.TeamLocationID = RUSTD.TeamLocationId)
		ORDER BY
			RUSTD.TeamLocationId DESC;
	END
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID TO PUBLIC
GO

/** Testing 
*/

DECLARE @SeasonId INT = 4
	, @GPEmployeeId NVARCHAR(25) = 'NEXSE001';
EXEC dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID 4, 'SHEED001'
EXEC dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID 4, 'NEXSE001'

		SELECT TOP 1
			RT.TeamLocationID
			, RT.SeasonID
			, RUSTD.*
		FROM
			dbo.RU_Users AS RU WITH (NOLOCK)
			INNER JOIN dbo.RU_Recruits AS RR WITH (NOLOCK)
			ON
				(RR.UserId = RU.UserID)
				AND (RU.GPEmployeeID = @GPEmployeeId)
				AND (RR.IsActive = 1 AND RR.IsDeleted = 0)
				AND (RU.IsActive = 1 AND RU.IsDeleted = 0)
			INNER JOIN dbo.RU_Season AS RS WITH (NOLOCK)
			ON
				(RR.SeasonId = RS.SeasonID)
				AND (RS.IsActive = 1 AND RS.IsDeleted = 0)
			INNER JOIN dbo.RU_TeamLocations AS RT WITH (NOLOCK)
			ON
				(RT.SeasonID = RS.SeasonID)
				AND (RS.SeasonID = @SeasonID)
				AND (RT.IsActive = 1 AND RT.IsDeleted = 0)
			LEFT OUTER JOIN [dbo].[RU_SeasonTeamLocationDefaults] AS RUSTD WITH (NOLOCK)
			ON
				(RS.SeasonID = RUSTD.SeasonID)
				AND (RT.TeamLocationID = RUSTD.TeamLocationId)
		ORDER BY
			RUSTD.TeamLocationId DESC;