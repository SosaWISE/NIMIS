USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficeSeasonsMaps')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficeSeasonsMaps'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficeSeasonsMaps
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficeSeasonsMaps'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficeSeasonsMaps.sql
**		Name: custRU_TeamLocationsGetOfficeSeasonsMaps
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficeSeasonsMaps
(
	@FromSeasonID INT
	, @ToSeasonID INT
)
AS
BEGIN
	
	
	--DECLARE @FromSeasonID INT
	--SET @FromSeasonID = 16

	--DECLARE @ToSeasonID INT
	--SET @ToSeasonID = 17
	
	SELECT
		FromRUTL.TeamLocationID AS FromID
		, ToRUTL.TeamLocationID AS ToID
		, *
	FROM RU_TeamLocations AS FromRUTL WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS ToRUTL WITH(NOLOCK)
	ON
		FromRUTL.TeamLocationID = ToRUTL.CreatedFromTeamLocationID
	WHERE
		FromRUTL.IsDeleted = 0
		AND ToRUTL.IsDeleted = 0
		
		AND FromRUTL.SeasonID = @FromSeasonID
		AND ToRUTL.SeasonID = @ToSeasonID


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficeSeasonsMaps TO PUBLIC
GO