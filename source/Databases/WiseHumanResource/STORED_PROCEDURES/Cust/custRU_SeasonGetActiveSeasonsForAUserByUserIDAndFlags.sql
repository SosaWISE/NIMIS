USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags'
		DROP  Procedure  dbo.custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags'
GO
/******************************************************************************
**		File: custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags.sql
**		Name: custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags
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
CREATE Procedure dbo.custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags
(
	@UserID INT
	, @IsCurrent BIT = NULL
	, @IsVisibleToRecruits BIT = NULL
	, @IsInsideSales BIT = NULL
	, @IsPreseason BIT = NULL
	, @IsSummer BIT = NULL
	, @IsExtended BIT = NULL
	, @IsYearRound BIT = NULL
)
AS
BEGIN
	/** ADD BODY HERE */
	SELECT
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
		((@IsCurrent IS NULL) OR (RS.IsCurrent = @IsCurrent))
		AND ((@IsVisibleToRecruits IS NULL) OR (RS.IsVisibleToRecruits = @IsVisibleToRecruits))
		AND ((@IsInsideSales IS NULL) OR (RS.IsInsideSales = @IsInsideSales))
		AND ((@IsPreseason IS NULL) OR (RS.IsPreseason = @IsPreseason))
		AND ((@IsSummer IS NULL) OR (RS.IsSummer = @IsSummer))
		AND ((@IsExtended IS NULL) OR (RS.IsExtended = @IsExtended))
		AND ((@IsYearRound IS NULL) OR (RS.IsYearRound = @IsYearRound))
		AND ((RS.StartDate IS NULL) OR (GETUTCDATE() BETWEEN RS.StartDate AND RS.EndDate))
		AND (RR.IsActive = 1) AND (RR.IsDeleted = 0)
END
GO

GRANT EXEC ON dbo.custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags TO PUBLIC
GO

--EXEC dbo.custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL  