USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceCalMonthlyMonitoringRate')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceCalMonthlyMonitoringRate'
		DROP FUNCTION  dbo.fxInvoiceCalMonthlyMonitoringRate
	END
GO

PRINT 'Creating FUNCTION fxInvoiceCalMonthlyMonitoringRate'
GO
/******************************************************************************
**		File: fxInvoiceCalMonthlyMonitoringRate.sql
**		Name: fxInvoiceCalMonthlyMonitoringRate
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
CREATE FUNCTION dbo.fxInvoiceCalMonthlyMonitoringRate
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
		[dbo].[AE_InvoiceItems] AS AII WITH (NOLOCK)
		INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
		ON	(ITM.ItemID = AII.ItemId)
	WHERE
		(ITM.ItemTypeId IN ('MON_CONT', 'MMR_SREP_UPSL', 'MMR_SREP_DISC'))
		AND (AII.InvoiceId = @InvoiceID)
		AND (AII.IsActive = 1 AND AII.IsDeleted = 0)
	GROUP BY
		AII.InvoiceID
	--	, ITM.ItemTypeId

	/** Return result. */
	RETURN @Result;
END
GO

/** TEST 
SELECT dbo.fxInvoiceCalMonthlyMonitoringRate(10010061) AS [SUM]
*/