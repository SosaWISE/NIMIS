USE [NXSE_Commissions]
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
**		Date: 02/20/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/20/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.FUNCTION_TEMPLATE
(
	@CustomerId BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @Score INT;

	/** Execute actions. */
	SELECT @Score = Score FROM dbo.fxQlCreditReportGetByMsAccountID(@CustomerId);

	RETURN @Score;
END
GO