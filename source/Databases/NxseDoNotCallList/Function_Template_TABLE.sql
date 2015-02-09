USE [NXSE_DoNotCallList]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'FUNCTION_TEMPLATE_TABLE')
	BEGIN
		PRINT 'Dropping FUNCTION FUNCTION_TEMPLATE_TABLE'
		DROP FUNCTION  dbo.FUNCTION_TEMPLATE_TABLE
	END
GO

PRINT 'Creating FUNCTION FUNCTION_TEMPLATE_TABLE'
GO
/******************************************************************************
**		File: FUNCTION_TEMPLATE_TABLE.sql
**		Name: FUNCTION_TEMPLATE_TABLE
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
**		Date: 01/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	01/21/2014	Andrés E. Sosa	Created
**	
*******************************************************************************/
CREATE FUNCTION dbo.FUNCTION_TEMPLATE_TABLE
(
-- parameters
)
RETURNS -- return datatype
AS
BEGIN
-- declares
	RETURN
END
GO