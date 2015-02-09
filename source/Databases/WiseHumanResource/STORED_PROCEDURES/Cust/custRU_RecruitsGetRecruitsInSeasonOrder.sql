USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitsInSeasonOrder')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitsInSeasonOrder'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitsInSeasonOrder
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitsInSeasonOrder'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitsInSeasonOrder.sql
**		Name: custRU_RecruitsGetRecruitsInSeasonOrder
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitsInSeasonOrder
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

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitsInSeasonOrder TO PUBLIC
GO