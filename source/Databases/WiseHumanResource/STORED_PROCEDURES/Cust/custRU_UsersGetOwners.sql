USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetOwners')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetOwners'
		DROP  Procedure  dbo.custRU_UsersGetOwners
	END
GO

PRINT 'Creating Procedure custRU_UsersGetOwners'
GO
/******************************************************************************
**		File: custRU_UsersGetOwners.sql
**		Name: custRU_UsersGetOwners
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
CREATE Procedure dbo.custRU_UsersGetOwners
AS
BEGIN
	-- Run Query
	SELECT
		RU.*
	FROM
		RU_Users AS RU WITH (NOLOCK)
	WHERE
		(
			(GPEmployeeID = N'ALLR001')
			OR (GPEmployeeID = N'DYER001')
			OR (GPEmployeeID = N'PIXT001')
			OR (GPEmployeeID = N'HALL001')
			OR (GPEmployeeID = N'PRUI001')
		)
		ANd RU.IsDeleted = 0
	ORDER BY
		FullName
END
GO

GRANT EXEC ON dbo.custRU_UsersGetOwners TO PUBLIC
GO