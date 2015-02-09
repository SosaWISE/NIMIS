USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetByTeamIDs')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetByTeamIDs'
		DROP  Procedure  dbo.custRU_TeamLocationsGetByTeamIDs
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetByTeamIDs'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetByTeamIDs.sql
**		Name: custRU_TeamLocationsGetByTeamIDs
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
CREATE Procedure dbo.custRU_TeamLocationsGetByTeamIDs
(
	@TeamIDList NVARCHAR(MAX)
)
AS
BEGIN

--	DECLARE @TeamIDList NVARCHAR(MAX)
--	SET @TeamIDList = '35,149'

	SELECT DISTINCT
		RTL.*
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN SplitIntList(@TeamIDList) AS Ids
	ON
		RT.TeamID = Ids.ID
	INNER JOIN RU_TeamLocations AS RTL WITH(NOLOCK)
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetByTeamIDs TO PUBLIC
GO