USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_ProductBarcodeTrackingCreate')
	BEGIN
		PRINT 'Dropping Procedure custIE_ProductBarcodeTrackingCreate'
		DROP  Procedure  dbo.custIE_ProductBarcodeTrackingCreate
	END
GO

PRINT 'Creating Procedure custIE_ProductBarcodeTrackingCreate'
GO
/******************************************************************************
**		File: custIE_ProductBarcodeTrackingCreate.sql
**		Name: custIE_ProductBarcodeTrackingCreate
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
**	06/17/2014	Reagan		Created By
**	08/13/2014	Bob McFadden	If the lastest inventory move was of type 'CUST'
**								prohibit the move of the barcode to 'CUST'
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_ProductBarcodeTrackingCreate
(
	@ProductBarcodeTrackingTypeId VARCHAR(20),
	@ProductBarcodeId NVARCHAR(50),
	@LocationTypeID NVARCHAR(50),
	@LocationID NVARCHAR(50),
	@Comment NVARCHAR(2000),
	@GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @CMFID VARCHAR(20) --Used to throw an exception if attempting to move inventory from one customer to another customer
	DECLARE @ProductBarcodeTrackingID BIGINT
	DECLARE @LastProductBarcodeTrackingTypeID VARCHAR(20)
	DECLARE @msg VARCHAR(150)
	DECLARE @ValidInventoryMove bit

	BEGIN TRY
		BEGIN TRANSACTION
			/** Determine whether the inventory move is valid */
			-- Default to Valid
			SET @ValidInventoryMove = 1

			-- Get the last inventory move
				SELECT TOP 1 
					@ProductBarcodeTrackingID = PBT.ProductBarcodeTrackingID,
					@LastProductBarcodeTrackingTypeID = PBT.ProductBarcodeTrackingTypeId
				FROM 
					dbo.IE_ProductBarcodeTracking AS PBT
					JOIN dbo.IE_ProductBarcodeTrackingTypes AS PBTT
						ON PBT.ProductBarcodeTrackingTypeId = PBTT.ProductBarcodeTrackingTypeID
						AND PBTT.IsInventoryMove = 1
						AND PBTT.IsActive = 1
						AND PBTT.IsDeleted = 0
				WHERE 
					PBT.ProductBarcodeId = @ProductBarcodeID
				ORDER BY PBT.ProductBarcodeTrackingID DESC;

			-- Turn off @ValidInventoryMove flag for the following:
			-- Transfer from CUST to CUST
			IF (@LastProductBarcodeTrackingTypeID = 'CUST') and (@ProductBarcodeTrackingTypeId = 'CUST') 
				SET @ValidInventoryMove = 0

			IF @ValidInventoryMove = 1
				BEGIN
					/** Create ProductBarcode */
					INSERT [dbo].[IE_ProductBarcodeTracking](
						[ProductBarcodeTrackingTypeId]
						,[ProductBarcodeId]
						,[LocationTypeID]
						,[LocationID]
						,[Comment]
						,[ModifiedBy]
						,[CreatedBy]
						) 
					VALUES (
						@ProductBarcodeTrackingTypeId
						,@ProductBarcodeId
						,@LocationTypeID
						,@LocationID
						,@Comment
						,@GPEmployeeId
						,@GPEmployeeId
						);

					/** Get Identity */
					SET @ProductBarcodeTrackingID = SCOPE_IDENTITY();

					PRINT @ProductBarcodeTrackingID

					-- ProductBarcodeTracking
					UPDATE [dbo].[IE_ProductBarcodes] SET [LastProductBarcodeTrackingId] = @ProductBarcodeTrackingID
					WHERE  [ProductBarcodeID]= @ProductBarcodeId

					/** Return result. */
					SELECT * FROM [dbo].[IE_ProductBarcodeTracking] WHERE ([ProductBarcodeTrackingID]= @ProductBarcodeTrackingID);

				END
			ELSE
				BEGIN
					IF (@LastProductBarcodeTrackingTypeID = 'CUST') and (@ProductBarcodeTrackingTypeId = 'CUST') 
						BEGIN
							SELECT 
								@CMFID = CONVERT(VARCHAR,Customers.CustomerMasterFileId)
							FROM 
								dbo.IE_ProductBarcodeTracking AS PBT
								JOIN dbo.AE_Customers as Customers
									on PBT.LocationID = Customers.CustomerID
							WHERE PBT.ProductBarcodeTrackingID = @ProductBarcodeTrackingID;
						SET @msg = 'Bar code number ''' + @ProductBarcodeID + ''' is already assigned to customer: ' + @CMFID + '.';
						THROW 70150, @msg, 16;
						END
				END

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custIE_ProductBarcodeTrackingCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custIE_ProductBarcodeTrackingCreate 
	@ProductBarcodeTrackingTypeId,
	@ProductBarcodeId ,
	@LocationTypeID ,
	@LocationID ,
	@Comment ,
	@GPEmployeeId
	
EXEC dbo.custIE_ProductBarcodeTrackingCreate  'CUST','789456213','SOLD',100290,'Test CUST-->CUST',''


*/