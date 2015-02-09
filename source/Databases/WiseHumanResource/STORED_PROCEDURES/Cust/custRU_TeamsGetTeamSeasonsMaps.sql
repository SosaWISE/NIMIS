USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamSeasonsMaps')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamSeasonsMaps'
		DROP  Procedure  dbo.custRU_TeamsGetTeamSeasonsMaps
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamSeasonsMaps'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamSeasonsMaps.sql
**		Name: custRU_TeamsGetTeamSeasonsMaps
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
CREATE Procedure dbo.custRU_TeamsGetTeamSeasonsMaps
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
		FromRT.TeamID AS FromID
		, ToRT.TeamID AS ToID
		--, *
	FROM VW_Teams AS FromRT WITH(NOLOCK)
	INNER JOIN VW_Teams AS ToRT WITH(NOLOCK)
	ON
		FromRT.TeamID = ToRT.CreatedFromTeamID
	WHERE
		FromRT.IsDeleted = 0
		AND ToRT.IsDeleted = 0
		
		AND FromRT.SeasonID = @FromSeasonID
		AND ToRT.SeasonID = @ToSeasonID


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamSeasonsMaps TO PUBLIC
GO