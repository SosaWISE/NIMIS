USE [SG9_CRM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetSystemAdminGuid')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetSystemAdminGuid'
		DROP FUNCTION  dbo.fxGetSystemAdminGuid
	END
GO

PRINT 'Creating FUNCTION fxGetSystemAdminGuid'
GO
/******************************************************************************
**		File: fxGetSystemAdminGuid.sql
**		Name: fxGetSystemAdminGuid
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
**		Date: 10/27/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	10/27/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetSystemAdminGuid
(
	@UserName NVARCHAR(250) = NULL
)
RETURNS UNIQUEIDENTIFIER
WITH SCHEMABINDING
AS
BEGIN
	/** Declarations */
	DECLARE @Result UNIQUEIDENTIFIER = 'F4B7963E-27F7-42AF-BABF-77BB6737B451';

	/** Execute actions. */
	SELECT TOP 1
		@Result = UserId
	FROM
		USR.Users 
	WHERE
		(UserName = @UserName);
----		OR (UserName = 'admin@thevoid.com');

	RETURN @Result;
END
GO
PRINT 'Created FUNCTION fxGetSystemAdminGuid';
SELECT dbo.fxGetSystemAdminGuid('sosawise@gmail.com')