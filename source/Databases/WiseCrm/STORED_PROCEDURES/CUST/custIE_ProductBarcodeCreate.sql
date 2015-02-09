USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_ProductBarcodeCreate')
	BEGIN
		PRINT 'Dropping Procedure custIE_ProductBarcodeCreate'
		DROP  Procedure  dbo.custIE_ProductBarcodeCreate
	END
GO

PRINT 'Creating Procedure custIE_ProductBarcodeCreate'
GO
/******************************************************************************
**		File: custIE_ProductBarcodeCreate.sql
**		Name: custIE_ProductBarcodeCreate
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
**	06/16/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_ProductBarcodeCreate
(
	@ProductBarcodeID VARCHAR(50),
	@ProductOrderItemId BIGINT,
	@GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @ProductBarcodeTrackingID BIGINT
		, @msg VARCHAR(150);

	/** Argument validation */
	BEGIN TRY
	
		/** Validate Product Barcode */
		IF (LEN(@ProductBarcodeID) <> 9)
		BEGIN
			SET @msg = 'Bar code number ''' + @ProductBarcodeID + ''' failed to meet validation length of ''9''.';
			THROW 70140, @msg, 16;
		END
		-- Check that the barcode starts with a 7
		IF (SUBSTRING(@ProductBarcodeID,1,1) <> '7')
		BEGIN
			SET @msg = 'Bar code number ''' + @ProductBarcodeID + ''' failed to start with the number ''7''.';
			THROW 70140, @msg, 16;
		END

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH


	BEGIN TRY

		BEGIN TRANSACTION
			/** Get @ProductBarcodeID information */
			IF (NOT EXISTS(SELECT * FROM [dbo].[IE_ProductBarcodes] WHERE ([ProductBarcodeID] = @ProductBarcodeID)))
			BEGIN
				/** Create ProductBarcode */
				INSERT [dbo].[IE_ProductBarcodes](
					[ProductBarcodeID],
					[PurchaseOrderItemId],
					[ModifiedBy],
					[CreatedBy]
				) VALUES (
					@ProductBarcodeID, 
					@ProductOrderItemId,
					@GPEmployeeId,
					@GPEmployeeId
				);

				/** insert default tracking record */
				INSERT [dbo].[IE_ProductBarcodeTracking](
					[ProductBarcodeTrackingTypeId],
					[ProductBarcodeId],
					[LocationTypeID],
					[ModifiedBy],
					[CreatedBy]
				) VALUES (
					'REC', ----Receive Shipment  - default value 
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


			END
			ELSE
			BEGIN
				--SET @msg = 'Bar code number already in use.';
				SET @msg = 'Bar code number ''' + @ProductBarcodeID + ''' already in use.';
				THROW 70120, @msg, 16;
				--RAISERROR (N'The Barcode Number %s has already been entered into the system.' -- Message text.
				--	, 16 -- Severity,
				--	, 1 -- State,
				--	, @ProductBarcodeID);
			END
	
	
			/** Return result. */
			SELECT * FROM [dbo].[IE_ProductBarcodes] WHERE ([ProductBarcodeID] = @ProductBarcodeID);
		

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custIE_ProductBarcodeCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custIE_ProductBarcodeCreate '7378687', 1, 'SOSA';
 */