USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetNewestRecruit')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetNewestRecruit'
		DROP  Procedure  dbo.custRU_RecruitsGetNewestRecruit
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetNewestRecruit'
GO
/******************************************************************************
**		File: custRU_RecruitsGetNewestRecruit.sql
**		Name: custRU_RecruitsGetNewestRecruit
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
CREATE Procedure dbo.custRU_RecruitsGetNewestRecruit
(
	@UserID INT
)
AS
BEGIN
	
--	DECLARE @UserID INT
--	SET @UserID = 5152

	SELECT TOP 1
		RR.*
	FROM RU_Recruits AS RR WITH (NOLOCK)
	INNER JOIN RU_Season AS RS WITH(NOLOCK)
	ON
		RR.SeasonID = RS.SeasonID
	WHERE
		RR.UserID = @UserID
		AND RR.IsDeleted = 0
	ORDER BY
		RS.StartDate DESC
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetNewestRecruit TO PUBLIC
GO