USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw'
		DROP  Procedure  dbo.custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw'
GO
/******************************************************************************
**		File: custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw.sql
**		Name: custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw
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
CREATE Procedure dbo.custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw
(
	@GPEmployeeID NVARCHAR(25)
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
--			AND (RR.IsActive = 1)
--			AND (RR.IsDeleted = 0)
		ORDER BY
			RU.FullName ASC
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw TO PUBLIC
GO