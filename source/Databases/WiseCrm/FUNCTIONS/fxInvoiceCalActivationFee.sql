USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceCalActivationFee')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceCalActivationFee'
		DROP FUNCTION  dbo.fxInvoiceCalActivationFee
	END
GO

PRINT 'Creating FUNCTION fxInvoiceCalActivationFee'
GO
/******************************************************************************
**		File: fxInvoiceCalActivationFee.sql
**		Name: fxInvoiceCalActivationFee
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
CREATE FUNCTION dbo.fxInvoiceCalActivationFee
(
	@InvoiceID BIGINT
)
RETURNS MONEY
AS
BEGIN
	/** Declareations */
	DECLARE @Result MONEY;

	/** Sum up all the activations fees */
	SELECT 
		  @Result = SUM([RetailPrice])
	FROM 
		[WISE_CRM].[dbo].[AE_InvoiceItems] AS AII WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[AE_Items] AS ITM WITH (NOLOCK)
		ON	(ITM.ItemID = AII.ItemId)
	WHERE
		(ITM.ItemTypeId IN ('SETUP_FEE', 'SETUP_FEE_UPSL', 'SETUP_FEE_DISC'))
		AND (AII.InvoiceId = @InvoiceID)
		AND (AII.IsActive = 1 AND AII.IsDeleted = 0)
	GROUP BY
		AII.InvoiceID
	--	, ITM.ItemTypeId

	/** Return result. */
	RETURN @Result;
END
GO