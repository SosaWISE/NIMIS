USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceActivationFeeItemByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceActivationFeeItemByScore'
		DROP FUNCTION  dbo.fxInvoiceActivationFeeItemByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceActivationFeeItemByScore'
GO
/******************************************************************************
**		File: fxInvoiceActivationFeeItemByScore.sql
**		Name: fxInvoiceActivationFeeItemByScore
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
CREATE FUNCTION dbo.fxInvoiceActivationFeeItemByScore
(
	@Score INT
	, @DefaultItemId VARCHAR(50) = 'SETUP_FEE_199'
)
RETURNS VARCHAR(50)
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 'SETUP_FEE_199';

	RETURN 'SETUP_FEE_99';
END
GO
/**
SELECT dbo.fxInvoiceActivationFeeItemByScore(NULL, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceActivationFeeItemByScore(300, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceActivationFeeItemByScore(650, 'MON_CONT_5003') AS ItemID;
*/