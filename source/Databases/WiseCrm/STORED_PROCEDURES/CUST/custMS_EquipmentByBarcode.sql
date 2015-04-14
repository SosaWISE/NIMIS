USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentByBarcode')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentByBarcode'
		DROP  Procedure  dbo.custMS_EquipmentByBarcode
	END
GO

PRINT 'Creating Procedure custMS_EquipmentByBarcode'
GO
/******************************************************************************
**		File: custMS_EquipmentByBarcode.sql
**		Name: custMS_EquipmentByBarcode
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
**		Date: 07/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_EquipmentByBarcode
(
	@BarcodeNumber VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	SELECT 
		EQM.*
	FROM
		MS_Equipments AS EQM WITH (NOLOCK)
		INNER JOIN IE_PurchaseOrderItems AS IPOI WITH (NOLOCK)
		ON
			(EQM.EquipmentID = IPOI.ItemId)
		INNER JOIN IE_ProductBarcodes AS IPB WITH (NOLOCK)
		ON
			(IPOI.PurchaseOrderItemID = IPB.PurchaseOrderItemId)
	WHERE
		(IPB.ProductBarcodeID = @BarcodeNumber)

END
GO

GRANT EXEC ON dbo.custMS_EquipmentByBarcode TO PUBLIC
GO

/**

SELECT * FROM IE_ProductBarcodes

EXEC dbo.custMS_EquipmentByBarcode 716514543

 */