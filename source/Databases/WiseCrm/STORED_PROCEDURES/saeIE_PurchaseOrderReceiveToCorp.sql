USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'saeIE_PurchaseOrderReceiveToCorp')
	BEGIN
		PRINT 'Dropping Procedure saeIE_PurchaseOrderReceiveToCorp'
		DROP  Procedure  dbo.saeIE_PurchaseOrderReceiveToCorp
	END
GO

PRINT 'Creating Procedure saeIE_PurchaseOrderReceiveToCorp'
GO
/******************************************************************************
**		File: saeIE_PurchaseOrderReceiveToCorp.sql
**		Name: saeIE_PurchaseOrderReceiveToCorp
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
**		Date: 07/01/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/01/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.saeIE_PurchaseOrderReceiveToCorp
(
	@ProductBarcodeID VARCHAR(50)
	, @PurchaseOrderID INT
	, @ItemSKU VARCHAR(30)
	, @PackingSlipNumber NVARCHAR(25)
	, @WarehouseSiteId VARCHAR(20)
	, @GPEmployeeID NVARCHAR(50)
	, @ArrivalDate  DATETIME
	, @CloseDate DATETIME
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @PackingSlipID INT
		, @PackingSlipItemID BIGINT
		, @PurchaseOrderItemID BIGINT
		, @ItemId VARCHAR(50)
		, @ProductBarcodeTrackingID BIGINT
		, @ProductBarcodeTrackingTypeId VARCHAR(20) = 'REC';
	
	BEGIN TRY
		BEGIN TRANSACTION;
		
		/** Check that the SKU is there. */
		SELECT @ItemId = ItemID FROM [dbo].[AE_Items] WHERE (ItemSKU = @ItemSKU);
		IF (@ItemId IS NULL)
		BEGIN
			RAISERROR (N'Unable to find ItemID from SKU %s' -- Message text.
					   , 10 -- Severity,
					   , 1 -- State,
					   , @ItemSKU);
		END

		/** Check that there is a slip. */
		IF (NOT EXISTS(SELECT * FROM [dbo].[IE_PackingSlips] WHERE (PurchaseOrderId = @PurchaseOrderID) AND (PackingSlipNumber = @PackingSlipNumber)))
		BEGIN
			INSERT INTO [dbo].[IE_PackingSlips] (
				[PurchaseOrderId]
				, [ArrivalDate]
				, [CloseDate]
				, [PackingSlipNumber]
				, [ModifiedBy]
				, [CreatedBy]
			) VALUES (
				 @PurchaseOrderID -- PurchaseOrderId - int
				, @ArrivalDate -- ArrivalDate - datetime
				, @CloseDate -- CloseDate - datetime
				, @PackingSlipNumber -- PackingSlipNumber - nvarchar(25)
				, @GPEmployeeID -- ModifiedBy - nvarchar(50)
				, @GPEmployeeID -- CreatedBy - nvarchar(50)
			);
			SET @PackingSlipID = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			SELECT @PackingSlipID = PackingSlipID FROM [dbo].[IE_PackingSlips] WHERE (PurchaseOrderId = @PurchaseOrderID) AND (PackingSlipNumber = @PackingSlipNumber);
		END

		/** Check to see if there is a PackingSlipItem for the item. */
		IF (NOT EXISTS(SELECT * FROM [dbo].[IE_PackingSlipItems] WHERE (PackingSlipId = @PackingSlipID) AND (ItemId = @ItemId)))
		BEGIN
			INSERT INTO [dbo].[IE_PackingSlipItems] (
				[PackingSlipId]
				, [ItemId]
				, [Quantity]
				, [ModifiedBy]
				, [CreatedBy]
			) VALUES (
				@PackingSlipID -- PackingSlipId - int
				, @ItemId -- ItemId - varchar(50)
				, 0 -- Quantity - int
				, @GPEmployeeID -- ModifiedBy - nvarchar(50)
				, @GPEmployeeID -- CreatedBy - nvarchar(50)
			);

			SET @PackingSlipItemID = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			SELECT @PackingSlipItemID = PackingSlipItemID FROM [dbo].[IE_PackingSlipItems] WHERE (PackingSlipId = @PackingSlipID) AND (ItemId = @ItemId);
		END

		/** Check to see if there is a ProductOrderItem for this SKU. */
		IF (NOT EXISTS(SELECT * FROM [dbo].[IE_PurchaseOrderItems] WHERE (PurchaseOrderId = @PurchaseOrderID) AND (ItemId = @ItemId)))
		BEGIN
			RAISERROR (N'Unable to find ItemID %s in PO ' -- Message text.
				, 10 -- Severity,
				, 1 -- State,
				, @ItemId);
		END
		SELECT @PurchaseOrderItemID = PurchaseOrderItemID FROM [dbo].[IE_PurchaseOrderItems] WHERE (PurchaseOrderId = @PurchaseOrderID) AND (ItemId = @ItemId);

		/** Insert barcode into barcode table. */
		INSERT INTO [dbo].[IE_ProductBarcodes] (
			[ProductBarcodeID]
			, [PurchaseOrderItemId]
			, [PackingSlipItemId]
			, [ModifiedBy]
			, [CreatedBy]
		) VALUES (
			@ProductBarcodeID -- ProductBarcodeID - nvarchar(50)
			, @PurchaseOrderItemID -- PurchaseOrderItemId - bigint
			, @PackingSlipItemID -- PackingSlipItemId - bigint
			, @GPEmployeeID -- ModifiedBy - nvarchar(50)
			, @GPEmployeeID -- CreatedBy - nvarchar(50)
		);

		/** insert default tracking record */
		INSERT [dbo].[IE_ProductBarcodeTracking] (
			[ProductBarcodeTrackingTypeId],
			[ProductBarcodeId],
			[LocationTypeID],
			[ModifiedBy],
			[CreatedBy]
		) VALUES (
			@ProductBarcodeTrackingTypeId, ----Receive Shipment  - default value 
			@ProductBarcodeID,
			'Received', -- set as default location type
			@GPEmployeeId,
			@GPEmployeeId
		);

		SET @ProductBarcodeTrackingID = SCOPE_IDENTITY()

		UPDATE [dbo].[IE_ProductBarcodes]
		SET 
			[LastProductBarcodeTrackingId] = @ProductBarcodeTrackingID
		WHERE
			([ProductBarcodeID] = @ProductBarcodeID);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.saeIE_PurchaseOrderReceiveToCorp TO PUBLIC
GO

/** EXEC dbo.saeIE_PurchaseOrderReceiveToCorp */