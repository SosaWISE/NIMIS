USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitByUserAndSeasonIDs')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitByUserAndSeasonIDs'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitByUserAndSeasonIDs
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitByUserAndSeasonIDs'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitByUserAndSeasonIDs.sql
**		Name: custRU_RecruitsGetRecruitByUserAndSeasonIDs
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitByUserAndSeasonIDs
(
	@UserID INT
	, @SeasonIDList NVARCHAR(500) = NULL -- This is a comma delimited list of ints e.g. '1,2,3' 
)
AS
BEGIN

	SELECT
		RR.*
	FROM RU_Users AS RU WITH (NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		RU.UserID = RR.UserID
	INNER JOIN RU_Season AS RS WITH (NOLOCK)
	ON
		RR.SeasonID = RS.SeasonID
	WHERE
		RU.UserID = @UserID
		AND ((@SeasonIDList IS NULL) OR (RS.SeasonID IN (SELECT * FROM SplitIntList(@SeasonIDList))))
		AND RR.IsDeleted = 0
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitByUserAndSeasonIDs TO PUBLIC
GO