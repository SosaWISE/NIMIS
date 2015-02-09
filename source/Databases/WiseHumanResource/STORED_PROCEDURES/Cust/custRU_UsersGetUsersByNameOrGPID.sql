USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetUsersByNameOrGPID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetUsersByNameOrGPID'
		DROP  Procedure  dbo.custRU_UsersGetUsersByNameOrGPID
	END
GO

PRINT 'Creating Procedure custRU_UsersGetUsersByNameOrGPID'
GO
/******************************************************************************
**		File: custRU_UsersGetUsersByNameOrGPID.sql
**		Name: custRU_UsersGetUsersByNameOrGPID
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
CREATE Procedure dbo.custRU_UsersGetUsersByNameOrGPID
(
	@NameOrID NVARCHAR(MAX) = NULL
)
AS
BEGIN

	SELECT RUU.UserID
			, RUU.FullName
	FROM RU_Users RUU
	WHERE RUU.FullName LIKE '%' + @NameOrID + '%' OR RUU.GPEmployeeID = @NameOrID
	
END
GO

GRANT EXEC ON dbo.custRU_UsersGetUsersByNameOrGPID TO PUBLIC
GO