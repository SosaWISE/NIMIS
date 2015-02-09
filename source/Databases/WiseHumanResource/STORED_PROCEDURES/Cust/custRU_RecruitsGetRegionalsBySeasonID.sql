USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRegionalsBySeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRegionalsBySeasonID'
		DROP  Procedure  dbo.custRU_RecruitsGetRegionalsBySeasonID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRegionalsBySeasonID'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRegionalsBySeasonID.sql
**		Name: custRU_RecruitsGetRegionalsBySeasonID
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
CREATE Procedure dbo.custRU_RecruitsGetRegionalsBySeasonID
(
	@SeasonID INT
)
AS
BEGIN
	-- Run Query
	SELECT
		RR.*
	FROM
		RU_Users AS RU WITH (NOLOCK)
		INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
		ON
			RU.UserID = RR.UserID
			AND ((RR.UserTypeID = 8) OR (RR.UserTypeID = 11)) -- ((Regional Manager - Technician) OR (Regional Manager - Sales))
		INNER JOIN RU_Season AS RS WITH (NOLOCK)
		ON
			RR.SeasonID = RS.SeasonID
			AND (RS.SeasonID = @SeasonID)
	ORDER BY 
		RU.FullName
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRegionalsBySeasonID TO PUBLIC
GO