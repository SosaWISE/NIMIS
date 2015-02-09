USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceItemAddByBarcode')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceItemAddByBarcode'
		DROP  Procedure  dbo.custAE_InvoiceItemAddByBarcode
	END
GO

PRINT 'Creating Procedure custAE_InvoiceItemAddByBarcode'
GO
/******************************************************************************
**		File: custAE_InvoiceItemAddByBarcode.sql
**		Name: custAE_InvoiceItemAddByBarcode
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
**		Auth: Andres Sosa
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceItemAddByBarcode
(
	@InvoiceID BIGINT
	, @ProductBarcodeID NVARCHAR(50)
	, @SalesmanID NVARCHAR(25)
	, @TechnicianID NVARCHAR(25)
	, @GpEmployeeID NVARCHAR(25)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @StateID VARCHAR(4) = 'UT', @PostalCode VARCHAR(5) = '84097', @ItemId VARCHAR(50);

	/** Get StateID and PostalCode. */
	SELECT @StateID = StateId, @PostalCode = PostalCode FROM [dbo].fxGetMcAddressByInvoiceId(@InvoiceID);

	/** Get ItemId by barcode ID */
	SELECT 
		@ItemId = POI.ItemId
	FROM
		[dbo].IE_ProductBarcodes AS PB WITH (NOLOCK)
		INNER JOIN [dbo].IE_PurchaseOrderItems AS POI WITH (NOLOCK)
		ON
			(PB.PurchaseOrderItemId = POI.PurchaseOrderItemID)
	WHERE
		(PB.ProductBarcodeID = @ProductBarcodeID);

	IF (@ItemId IS NULL) RETURN;
		
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
		INSERT INTO [dbo].[AE_InvoiceItems] (
			[InvoiceId]
			,[ItemId]
			,[ProductBarcodeId]
			,[TaxOptionId]
			,[Qty]
			,[Cost]
			,[RetailPrice]
			,[PriceWithTax]
			,[SystemPoints]
			,[SalesmanId]
			,[TechnicianId]
			,[IsActive]
			,[IsDeleted]
			,[ModifiedOn]
			,[ModifiedBy]
			,[CreatedOn]
			,[CreatedBy]
		) 
		SELECT
			@InvoiceID
			, ITM.ItemID
			, @ProductBarcodeID
			, ITM.TaxOptionId
			, 1
			, ITM.Cost
			, ITM.Price
			, ITM.Price
			, ITM.SystemPoints
			, @SalesmanID
			, @TechnicianID
			, 1
			, 0
			, GETUTCDATE()
			, @GpEmployeeID
			, GETUTCDATE()
			, @GpEmployeeID
		FROM
			[dbo].AE_Items AS ITM WITH (NOLOCK)
		WHERE
			(ITM.ItemID = @ItemId);

		/** 
		* FINISH by recalculating the invoice header
		*/

		EXEC dbo.custAE_InvoiceCalculatePrices @InvoiceID, @StateID, @PostalCode;
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return invoice items. */
	SELECT * FROM [dbo].vwAE_InvoiceItems WHERE (InvoiceId = @InvoiceID) AND (IsActive = 1) AND (IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custAE_InvoiceItemAddByBarcode TO PUBLIC
GO

/** EXEC dbo.custAE_InvoiceItemAddByBarcode */