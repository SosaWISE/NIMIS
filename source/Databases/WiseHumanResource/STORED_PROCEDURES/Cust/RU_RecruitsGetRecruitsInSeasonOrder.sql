USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RU_RecruitsGetRecruitsInSeasonOrder')
	BEGIN
		PRINT 'Dropping Procedure RU_RecruitsGetRecruitsInSeasonOrder'
		DROP  Procedure  dbo.RU_RecruitsGetRecruitsInSeasonOrder
	END
GO

PRINT 'Creating Procedure RU_RecruitsGetRecruitsInSeasonOrder'
GO
/******************************************************************************
**		File: RU_RecruitsGetRecruitsInSeasonOrder.sql
**		Name: RU_RecruitsGetRecruitsInSeasonOrder
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
CREATE Procedure dbo.RU_RecruitsGetRecruitsInSeasonOrder
(
	@UserID INT = NULL
)
AS
BEGIN

	SELECT RUR.RecruitID 
			, RUR.SeasonID
			, RUS.SeasonName
	FROM RU_Recruits RUR
		INNER JOIN RU_Season RUS
		ON
			RUR.SeasonID = RUS.SeasonID
	WHERE RUR.UserID = @UserID
	ORDER BY RUR.SeasonID DESC
	
END
GO

GRANT EXEC ON dbo.RU_RecruitsGetRecruitsInSeasonOrder TO PUBLIC
GO