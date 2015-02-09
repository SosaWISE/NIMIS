USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxRU_UsersGetFullnameByGPEmployeeID')
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_UsersGetFullnameByGPEmployeeID'
		DROP FUNCTION  dbo.fxRU_UsersGetFullnameByGPEmployeeID
	END
GO

PRINT 'Creating FUNCTION fxRU_UsersGetFullnameByGPEmployeeID'
GO
/******************************************************************************
**		File: fxRU_UsersGetFullnameByGPEmployeeID.sql
**		Name: fxRU_UsersGetFullnameByGPEmployeeID
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 06/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/24/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_UsersGetFullnameByGPEmployeeID
(
	@GPEmployeeID NVARCHAR(25)
)
RETURNS NVARCHAR(53)
AS
BEGIN
	-- Declarations
	DECLARE @Result NVARCHAR(53) = NULL;

	SELECT @Result = PublicFullName FROM [dbo].RU_Users WHERE (GPEmployeeID = @GPEmployeeID);

	RETURN @Result;
END
GO