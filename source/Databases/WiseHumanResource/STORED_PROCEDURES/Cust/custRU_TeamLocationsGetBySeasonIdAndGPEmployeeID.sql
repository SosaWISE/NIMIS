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
	SELECT TOP 1
		RT.*
	FROM
		dbo.RU_Season AS RS WITH (NOLOCK)
		INNER JOIN dbo.RU_TeamLocations AS RT WITH (NOLOCK)
		ON
			(RT.SeasonID = RS.SeasonID)
			AND (RS.SeasonID = @SeasonID)
			AND (RS.IsActive = 1 AND RS.IsDeleted = 0)
			AND (RT.IsActive = 1 AND RT.IsDeleted = 0)
		INNER JOIN dbo.RU_Recruits AS RR WITH (NOLOCK)
		ON
			(RR.SeasonID = RS.SeasonID)
			AND (RR.IsActive = 1 AND RR.IsDeleted = 0)
		INNER JOIN dbo.RU_Users AS RU WITH (NOLOCK)
		ON
			(RU.UserID = RR.UserID)
			AND (RU.GPEmployeeID = @GPEmployeeId)
			AND (RU.IsActive = 1 AND RU.IsDeleted = 0)
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID TO PUBLIC
GO

/** Testing 
EXEC dbo.custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID 3, 'SOSA001'
*/