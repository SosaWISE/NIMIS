USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetProductBarcodeCount')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetProductBarcodeCount'
		DROP FUNCTION  dbo.fxGetProductBarcodeCount
	END
GO

PRINT 'Creating FUNCTION fxGetProductBarcodeCount'
GO
/******************************************************************************
**		File: fxGetProductBarcodeCount.sql
**		Name: fxGetProductBarcodeCount
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
**	06/13/2014	Reagan	         created by /  this function will return a count of product barcode  /  [SimGUID]= ProductBarcode  (not sure)
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetProductBarcodeCount
(
	@PurchaseOrderItemId BIGINT
)
RETURNS INT
AS
BEGIN
	DECLARE @ProductBarcodeCount INT

	SET @ProductBarcodeCount = (SELECT COUNT([ProductBarcodeID]) FROM [WISE_CRM].[dbo].[IE_ProductBarcodes] AS IEPB
								 WHERE  IEPB.PurchaseOrderItemId=@PurchaseOrderItemId  )
	RETURN ISNULL(@ProductBarcodeCount,0)
END
GO