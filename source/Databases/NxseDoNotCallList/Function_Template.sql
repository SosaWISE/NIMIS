USE [NXSE_DoNotCallList]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'FUNCTION_TEMPLATE')
	BEGIN
		PRINT 'Dropping FUNCTION FUNCTION_TEMPLATE'
		DROP FUNCTION  dbo.FUNCTION_TEMPLATE
	END
GO

PRINT 'Creating FUNCTION FUNCTION_TEMPLATE'
GO
/******************************************************************************
**		File: FUNCTION_TEMPLATE.sql
**		Name: FUNCTION_TEMPLATE
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
**		Date: 05/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/24/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.FUNCTION_TEMPLATE
(
	-- DECLARE PARAMETERS
)
RETURNS -- RETURN TYPE
AS
-- DECLARES
BEGIN

	RETURN
END
GO