USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_ItemByBarcode')
	BEGIN
		PRINT 'Dropping Procedure custAE_ItemByBarcode'
		DROP  Procedure  dbo.custAE_ItemByBarcode
	END
GO

PRINT 'Creating Procedure custAE_ItemByBarcode'
GO
/******************************************************************************
**		File: custAE_ItemByBarcode.sql
**		Name: custAE_ItemByBarcode
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Aaron Shumway
**		Date: 07/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_ItemByBarcode
(
	@BarcodeNumber VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	SELECT 
		ITM.*
	FROM AE_Items AS ITM WITH (NOLOCK)
	INNER JOIN IE_PurchaseOrderItems AS IPOI WITH (NOLOCK)
	ON
		(ITM.ItemID = IPOI.ItemId)
	INNER JOIN IE_ProductBarcodes AS IPB WITH (NOLOCK)
	ON
		(IPOI.PurchaseOrderItemID = IPB.PurchaseOrderItemId)
	WHERE
		(IPB.ProductBarcodeID = @BarcodeNumber)

END
GO

GRANT EXEC ON dbo.custAE_ItemByBarcode TO PUBLIC
GO

/**

SELECT * FROM IE_ProductBarcodes

EXEC dbo.custAE_ItemByBarcode 716514543

 */