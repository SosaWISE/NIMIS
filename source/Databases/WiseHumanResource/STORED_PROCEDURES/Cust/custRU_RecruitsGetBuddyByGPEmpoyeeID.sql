USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetBuddyByGPEmpoyeeID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetBuddyByGPEmpoyeeID'
		DROP  Procedure  dbo.custRU_RecruitsGetBuddyByGPEmpoyeeID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetBuddyByGPEmpoyeeID'
GO
/******************************************************************************
**		File: custRU_RecruitsGetBuddyByGPEmpoyeeID.sql
**		Name: custRU_RecruitsGetBuddyByGPEmpoyeeID
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
CREATE Procedure dbo.custRU_RecruitsGetBuddyByGPEmpoyeeID
(
	@GPEmployeeID NVARCHAR(10) = NULL
	, @SeasonID INT = NULL
)
AS
BEGIN
	SELECT RUR.RecruitID
			, RUU.FullName + ' (' + RUU.GPEmployeeID + ')' AS FullDescription
	FROM RU_Recruits RUR
		INNER JOIN RU_Users RUU
		ON
			RUR.UserID = RUU.UserID
	WHERE RUU.GPEmployeeID = @GPEmployeeID
			AND RUR.SeasonID = @SeasonID
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetBuddyByGPEmpoyeeID TO PUBLIC
GO