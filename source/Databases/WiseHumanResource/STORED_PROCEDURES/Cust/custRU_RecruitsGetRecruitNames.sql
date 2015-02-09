USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitNames')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitNames'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitNames
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitNames'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitNames.sql
**		Name: custRU_RecruitsGetRecruitNames
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitNames
(
	@SeasonID INT
)
AS
BEGIN
	

	SELECT
		UserID AS ID
		, FullName AS [Text]
	FROM RU_Users WITH(NOLOCK)
	WHERE
		UserID IN
		(
			SELECT
				UserID
			FROM RU_Recruits WITH(NOLOCK)
			WHERE
				SeasonID = @SeasonID
		)

	--SELECT
	--	RecruitID AS ID
	--	, FullName AS [Text]
	--FROM VW_RecruitUser WITH(NOLOCK)
	--WHERE
	--	SeasonID = @SeasonID


END

GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitNames TO PUBLIC
GO