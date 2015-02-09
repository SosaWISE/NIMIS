USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetExpiringRightToWork')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetExpiringRightToWork'
		DROP  Procedure  dbo.custRU_UsersGetExpiringRightToWork
	END
GO

PRINT 'Creating Procedure custRU_UsersGetExpiringRightToWork'
GO
/******************************************************************************
**		File: custRU_UsersGetExpiringRightToWork.sql
**		Name: custRU_UsersGetExpiringRightToWork
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
CREATE Procedure dbo.custRU_UsersGetExpiringRightToWork
AS
BEGIN

SELECT UserID
		, GPEmployeeID
		, FullName
		, RightToWorkExpirationDate
FROM RU_Users
WHERE DATEADD(MONTH, -1, RightToWorkExpirationDate) < GETDATE()
	AND IsDeleted = 0
	
END
GO

GRANT EXEC ON dbo.custRU_UsersGetExpiringRightToWork TO PUBLIC
GO