USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetActiveUserSeasons')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetActiveUserSeasons'
		DROP  Procedure  dbo.custRU_SeasonGetActiveUserSeasons
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetActiveUserSeasons'
GO
/******************************************************************************
**		File: custRU_SeasonGetActiveUserSeasons.sql
**		Name: custRU_SeasonGetActiveUserSeasons
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
CREATE Procedure dbo.custRU_SeasonGetActiveUserSeasons
(
	@UserID INT
)
AS
BEGIN


	SELECT
		RS.*
	FROM RU_Season AS RS WITH (NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		RS.SeasonID = RR.SeasonID
	WHERE
			(RR.UserID = @UserID)
		AND (RR.IsActive = 1 AND RR.IsDeleted = 0)
		AND (RS.IsActive = 1 AND RS.IsDeleted = 0)
END
GO

GRANT EXEC ON dbo.custRU_SeasonGetActiveUserSeasons TO PUBLIC
GO