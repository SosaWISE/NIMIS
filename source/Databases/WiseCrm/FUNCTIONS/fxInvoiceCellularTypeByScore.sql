USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceCellularTypeByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceCellularTypeByScore'
		DROP FUNCTION  dbo.fxInvoiceCellularTypeByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceCellularTypeByScore'
GO
/******************************************************************************
**		File: fxInvoiceCellularTypeByScore.sql
**		Name: fxInvoiceCellularTypeByScore
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
CREATE FUNCTION dbo.fxInvoiceCellularTypeByScore
(
	@Score INT
	, @DefaultItemId VARCHAR(20) = 'NOCELL'
)
RETURNS VARCHAR(20)
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 'CELLSEC';

	RETURN 'CELLPRI';
END
GO

/** 
SELECT dbo.fxInvoiceCellularTypeByScore(NULL, 'CELLTRKR') AS ItemID;
SELECT dbo.fxInvoiceCellularTypeByScore(300, 'CELLSEC') AS ItemID;
SELECT dbo.fxInvoiceCellularTypeByScore(650, 'CELLSEC') AS ItemID;
 */