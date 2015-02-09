USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceIsOver3MonthsByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceIsOver3MonthsByScore'
		DROP FUNCTION  dbo.fxInvoiceIsOver3MonthsByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceIsOver3MonthsByScore'
GO
/******************************************************************************
**		File: fxInvoiceIsOver3MonthsByScore.sql
**		Name: fxInvoiceIsOver3MonthsByScore
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
**		Date: 02/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/11/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxInvoiceIsOver3MonthsByScore
(
	@Score INT
	, @DefaultItemId BIT = 0
)
RETURNS BIT
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 0;

	RETURN 1;
END
GO

/** 
SELECT dbo.fxInvoiceIsOver3MonthsByScore(NULL, 1) AS ItemID;
SELECT dbo.fxInvoiceIsOver3MonthsByScore(300, 1) AS ItemID;
SELECT dbo.fxInvoiceIsOver3MonthsByScore(650, 0) AS ItemID;
 */