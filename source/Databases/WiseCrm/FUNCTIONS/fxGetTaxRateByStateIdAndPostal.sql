USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetTaxRateByStateIdAndPostal'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetTaxRateByStateIdAndPostal'
		DROP FUNCTION  dbo.fxGetTaxRateByStateIdAndPostal
	END
GO

PRINT 'Creating FUNCTION fxGetTaxRateByStateIdAndPostal'
GO
/******************************************************************************
**		File: fxGetTaxRateByStateIdAndPostal.sql
**		Name: fxGetTaxRateByStateIdAndPostal
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: A string with the group that the given score belongs to
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 05/29/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/29/2011	Andrés E. Sosa	Created By
**	SELECT dbo.fxGetTaxRateByStateIdAndPostal('UT', '84097')
*******************************************************************************/
CREATE FUNCTION dbo.fxGetTaxRateByStateIdAndPostal
(
	@StateID VARCHAR(4)
	, @PostalCode VARCHAR(5)
)
RETURNS NUMERIC(18, 5)
AS
BEGIN
	-- Locals
	DECLARE @Result NUMERIC(18, 5);
	
	/* Init values */
	SET @Result = 1.00;
	
	-- Return result
	RETURN @Result;
END
GO