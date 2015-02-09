USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsFindTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsFindTeams'
		DROP  Procedure  dbo.custRU_TeamsFindTeams
	END
GO

PRINT 'Creating Procedure custRU_TeamsFindTeams'
GO
/******************************************************************************
**		File: custRU_TeamsFindTeams.sql
**		Name: custRU_TeamsFindTeams
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
CREATE Procedure dbo.custRU_TeamsFindTeams
(
	@Top INT = NULL
	, @TeamID INT = NULL
	, @TeamName VARCHAR(50) = NULL
	, @OfficeName VARCHAR(50) = NULL
	, @SeasonID INT = NULL
	, @SeasonName NVARCHAR(50) = NULL
	, @RoleLocationID INT = NULL
	, @City VARCHAR(50) = NULL
	, @StateAB CHAR(2) = NULL
)
AS
BEGIN

	SELECT
		RT.*
	FROM vw_Teams AS RT WITH(NOLOCK)
	INNER JOIN
	(
		SELECT
			RR.TeamID
			, ROW_NUMBER() OVER(ORDER BY RR.TeamName) AS Row			
		FROM 
			vw_Teams AS RR WITH(NOLOCK)
		WHERE
			(@TeamID IS NULL OR (RR.TeamID = @TeamID))
			AND (@TeamName IS NULL OR (RR.TeamName LIKE @TeamName))
			AND (@OfficeName IS NULL OR (RR.OfficeName LIKE @OfficeName))
			AND (@SeasonID IS NULL OR (RR.SeasonID = @SeasonID))
			AND (@SeasonName IS NULL OR (RR.SeasonName LIKE @SeasonName))
			AND (@RoleLocationID IS NULL OR (RR.RoleLocationID = @RoleLocationID))
			AND (@City IS NULL OR (RR.City LIKE @City ))
			AND (@StateAB IS NULL OR (RR.StateID LIKE @StateAB ))

	) AS Results
	ON
		(RT.TeamID= Results.TeamID)
	WHERE
		(@Top IS NULL OR Results.Row <= @Top)
	ORDER BY
		RT.TeamName ASC


END
GO

GRANT EXEC ON dbo.custRU_TeamsFindTeams TO PUBLIC
GO