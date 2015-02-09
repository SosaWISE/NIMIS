USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitByEmailAndSeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitByEmailAndSeasonID'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitByEmailAndSeasonID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitByEmailAndSeasonID'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitByEmailAndSeasonID.sql
**		Name: custRU_RecruitsGetRecruitByEmailAndSeasonID
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitByEmailAndSeasonID
(
	@Email NVARCHAR(50)
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
		(RU.Email = @Email)
		AND (RR.IsDeleted = 0 AND RU.IsDeleted = 0)
		AND ((@SeasonIDList IS NULL) OR (RS.SeasonID IN (SELECT * FROM SplitIntList(@SeasonIDList))))
	ORDER BY
		RS.StartDate DESC

END

GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitByEmailAndSeasonID TO PUBLIC
GO