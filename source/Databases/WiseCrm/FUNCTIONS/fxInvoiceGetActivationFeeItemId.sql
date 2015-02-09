USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceGetActivationFeeItemId')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceGetActivationFeeItemId'
		DROP FUNCTION  dbo.fxInvoiceGetActivationFeeItemId
	END
GO

PRINT 'Creating FUNCTION fxInvoiceGetActivationFeeItemId'
GO
/******************************************************************************
**		File: fxInvoiceGetActivationFeeItemId.sql
**		Name: fxInvoiceGetActivationFeeItemId
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
**		Date: 01/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	01/27/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxInvoiceGetActivationFeeItemId
(
	@AccountID BIGINT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declareations */
	DECLARE @Result VARCHAR(50);

	/** Find Credit report score. */

	/** Return result. */
	RETURN @Result;
END
GO