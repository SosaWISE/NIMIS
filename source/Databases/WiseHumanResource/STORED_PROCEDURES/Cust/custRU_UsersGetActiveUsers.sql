USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetActiveUsers')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetActiveUsers'
		DROP  Procedure  dbo.custRU_UsersGetActiveUsers
	END
GO

PRINT 'Creating Procedure custRU_UsersGetActiveUsers'
GO
/******************************************************************************
**		File: custRU_UsersGetActiveUsers.sql
**		Name: custRU_UsersGetActiveUsers
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
CREATE Procedure dbo.custRU_UsersGetActiveUsers
AS
BEGIN

SELECT RUU.UserID
		, RUU.FullName
FROM RU_Users RUU
WHERE RUU.IsActive = 1	
		AND RUU.IsDeleted = 0
	
END
GO

GRANT EXEC ON dbo.custRU_UsersGetActiveUsers TO PUBLIC
GO