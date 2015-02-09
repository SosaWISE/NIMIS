USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetActiveSeasonsForAUserByUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetActiveSeasonsForAUserByUserID'
		DROP  Procedure  dbo.custRU_SeasonGetActiveSeasonsForAUserByUserID
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetActiveSeasonsForAUserByUserID'
GO
/******************************************************************************
**		File: custRU_SeasonGetActiveSeasonsForAUserByUserID.sql
**		Name: custRU_SeasonGetActiveSeasonsForAUserByUserID
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
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	-------------	-------------------------------------------
**	12/05/2013	Andres Sosa		Created by
**
*******************************************************************************/
CREATE Procedure dbo.custRU_SeasonGetActiveSeasonsForAUserByUserID
(
	@UserID INT
)
AS
BEGIN
	/** ADD BODY HERE */
	SELECT
		--RR.RecruitID
		--, RU.UserID
		--, RS.SeasonID
		--, RS.IsCurrent
		--, RS.SeasonName
		RS.*
	FROM
		[dbo].RU_Users AS RU WITH (NOLOCK)
		INNER JOIN [dbo].RU_Recruits AS RR WITH (NOLOCK)
		ON
			(RU.UserID = RR.UserID)
		INNER JOIN [dbo].RU_Season AS RS WITH (NOLOCK)
		ON
			(RR.SeasonID = RS.SeasonID)
	WHERE	
		(RS.IsYearRound = 1 OR (GETUTCDATE() BETWEEN RS.StartDate AND RS.EndDate)) -- Sales Seasons
		AND (RR.IsActive = 1) AND (RR.IsDeleted = 0)
		AND RU.UserID = @UserID
END
GO

GRANT EXEC ON dbo.custRU_SeasonGetActiveSeasonsForAUserByUserID TO PUBLIC
GO

/** TEST
EXEC dbo.custRU_SeasonGetActiveSeasonsForAUserByUserID 1199

	SELECT
		RR.RecruitID
		, RU.UserID
		--, RS.SeasonID
		--, RS.IsCurrent
		--, RS.SeasonName
		, RS.*
	FROM
		[dbo].RU_Users AS RU WITH (NOLOCK)
		INNER JOIN [dbo].RU_Recruits AS RR WITH (NOLOCK)
		ON
			(RU.UserID = RR.UserID)
		INNER JOIN [dbo].RU_Season AS RS WITH (NOLOCK)
		ON
			(RR.SeasonID = RS.SeasonID)
	WHERE	
		(RS.IsYearRound = 1 OR (GETUTCDATE() BETWEEN RS.StartDate AND RS.EndDate)) -- Sales Seasons
		--AND (RR.IsActive = 1) AND (RR.IsDeleted = 0)
		AND RU.UserID = 1199

*/