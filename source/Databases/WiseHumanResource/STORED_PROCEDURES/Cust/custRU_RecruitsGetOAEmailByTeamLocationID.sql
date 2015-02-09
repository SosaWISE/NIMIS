USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetOAEmailByTeamLocationID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetOAEmailByTeamLocationID'
		DROP  Procedure  dbo.custRU_RecruitsGetOAEmailByTeamLocationID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetOAEmailByTeamLocationID'
GO
/******************************************************************************
**		File: custRU_RecruitsGetOAEmailByTeamLocationID.sql
**		Name: custRU_RecruitsGetOAEmailByTeamLocationID
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
CREATE Procedure [dbo].[custRU_RecruitsGetOAEmailByTeamLocationID]
(
	@TeamLocationID INT = NULL
	, @SeasonID INT = NULL
)
AS
BEGIN

	SELECT RUU.FullName
			, RUU.Email
	FROM RU_Recruits RUR
		INNER JOIN RU_Teams RUT WITH (NOLOCK)
		ON
			RUR.TeamID = RUT.TeamID
		INNER JOIN RU_TeamLocations RUTL WITH (NOLOCK)
		ON
			RUT.TeamLocationID = RUTL.TeamLocationID
		INNER JOIN RU_Users RUU WITH (NOLOCK)
		ON
			RUR.UserID = RUU.UserID
	WHERE RUR.UserTypeID = 13--OA TYPE
			AND RUTL.TeamLocationID = @TeamLocationID
			AND RUR.SeasonID = @SeasonID
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetOAEmailByTeamLocationID TO PUBLIC
GO