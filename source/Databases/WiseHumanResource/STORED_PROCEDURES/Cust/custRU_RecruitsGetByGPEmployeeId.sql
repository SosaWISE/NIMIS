USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetByGPEmployeeId')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetByGPEmployeeId'
		DROP  Procedure  dbo.custRU_RecruitsGetByGPEmployeeId
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetByGPEmployeeId'
GO
/******************************************************************************
**		File: custRU_RecruitsGetByGPEmployeeId.sql
**		Name: custRU_RecruitsGetByGPEmployeeId
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
CREATE Procedure dbo.custRU_RecruitsGetByGPEmployeeId
(
	@GPEmployeeID NVARCHAR(25)
	, @SeasonID INT
)
AS
BEGIN
	SELECT
			RR.*
		FROM
			RU_Recruits AS RR WITH (NOLOCK)
			INNER JOIN RU_Users AS RU WITH (NOLOCK)
			ON
				RR.UserID = RU.UserID
		WHERE
			(RU.GPEmployeeID = @GPEmployeeID)
			AND (RR.SeasonID = @SeasonID)
			--AND (RR.IsActive = 1)
			AND (RR.IsDeleted = 0)
		ORDER BY
			RU.FullName ASC
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetByGPEmployeeId TO PUBLIC
GO