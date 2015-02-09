USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetVisibleUserSeasons')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetVisibleUserSeasons'
		DROP  Procedure  dbo.custRU_SeasonGetVisibleUserSeasons
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetVisibleUserSeasons'
GO
/******************************************************************************
**		File: custRU_SeasonGetVisibleUserSeasons.sql
**		Name: custRU_SeasonGetVisibleUserSeasons
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
CREATE Procedure dbo.custRU_SeasonGetVisibleUserSeasons
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
		AND (RS.IsVisibleToRecruits = 1 AND RS.IsDeleted = 0)
END
GO

GRANT EXEC ON dbo.custRU_SeasonGetVisibleUserSeasons TO PUBLIC
GO