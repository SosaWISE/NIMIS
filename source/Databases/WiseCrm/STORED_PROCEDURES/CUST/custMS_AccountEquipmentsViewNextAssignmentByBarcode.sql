USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEquipmentsViewNextAssignmentByBarcode')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEquipmentsViewNextAssignmentByBarcode'
		DROP  Procedure  dbo.custMS_AccountEquipmentsViewNextAssignmentByBarcode
	END
GO

PRINT 'Creating Procedure custMS_AccountEquipmentsViewNextAssignmentByBarcode'
GO
/******************************************************************************
**		File: custMS_AccountEquipmentsViewNextAssignmentByBarcode.sql
**		Name: custMS_AccountEquipmentsViewNextAssignmentByBarcode
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountEquipmentsViewNextAssignmentByBarcode
(
	@AccountId BIGINT
	, @BarcodeNumber VARCHAR(20)
	, @GpEmployeeId VARCHAR(50)
	, @CrmUserId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @ItemID VARCHAR(50)
		, @AccountEquipmentID BIGINT
		, @AccountZoneAssignmentID BIGINT
		, @NextZone VARCHAR(3)
		, @ProductBarcodeTrackingId BIGINT
		, @LocationType VARCHAR(15) 
		, @msg VARCHAR(150);

	/** Initialize */
	SELECT 
		@ItemID = IPOI.ItemId 
	FROM
		[dbo].IE_ProductBarcodes AS IPB WITH (NOLOCK)
		INNER JOIN IE_PurchaseOrderItems AS IPOI WITH (NOLOCK)
		ON
			(IPB.PurchaseOrderItemId = IPOI.PurchaseOrderItemID)
	WHERE
		(IPB.ProductBarcodeID = @BarcodeNumber);

	-- check here if barcode is already sold, then produce an error message that says "product with the barcode number xxxx is already sold."
	-- LocationType is sold
	SET @LocationType = (
							SELECT 
								IEPBT.[LocationTypeID]
							FROM
							[dbo].[IE_ProductBarcodes] IEPB WITH (NOLOCK)
							INNER JOIN
							[dbo].[IE_ProductBarcodeTracking] IEPBT WITH (NOLOCK)
							ON
							IEPB.[LastProductBarcodeTrackingId] = IEPBT.[ProductBarcodeTrackingId]
							WHERE
							IEPB.[ProductBarcodeID] = @BarcodeNumber
			);

	IF @LocationType = 'Sold'
	BEGIN
			SET @msg = 'Bar code number ''' + @BarcodeNumber + ''' is already sold.';
			THROW 70140, @msg, 16;
	END
	

	BEGIN TRY
		BEGIN TRANSACTION;

		/** Insert equipment */
		INSERT INTO dbo.MS_AccountEquipment (
			AccountId 
			, EquipmentId 
			, EquipmentLocationId 
			, GPEmployeeId 
			, OfficeReconciliationItemId 
			, AccountEquipmentUpgradeTypeId 
			, Points 
			, ActualPoints 
			, Price 
			, IsExisting 
			, BarcodeId
			, IsServiceUpgrade 
			, IsExistingWiring 
			, IsMainPanel 
			, ModifiedBy 
			, CreatedBy 
		)
		SELECT
			@AccountId AS AccountId -- AccountId - bigint
			, ITM.ItemID
			, NULL -- EquipmentLocationId - int
			, @GpEmployeeId -- GPEmployeeId - varchar(50)
			, NULL -- OfficeReconciliationItemId - int
			, 'CUST' -- AccountEquipmentUpgradeTypeId - varchar(10)
			, ITM.SystemPoints AS Points
			, ITM.SystemPoints AS ActualPoints
			, ITM.Price
			, 0 -- IsExisting - bit
			, @BarcodeNumber
			, 0 -- IsServiceUpgrade - bit
			, 0 -- IsExistingWiring - bit
			, 0 -- IsMainPanel - bit
			, @CrmUserId -- ModifiedBy - nvarchar(50)
			, @CrmUserId -- CreatedBy - nvarchar(50)
		FROM
			[dbo].[AE_Items] AS ITM WITH (NOLOCK)
		WHERE
			(ITM.ItemID = @ItemID);
		-- Get IDENTITY
		SET @AccountEquipmentID = SCOPE_IDENTITY();

		/** Insert INTO MS_AccountZoneAssignments */
		SELECT @NextZone = [dbo].fxGetMsAccountLastAvailableZone(@AccountId);
		INSERT INTO [dbo].[MS_AccountZoneAssignments] (
			[AccountEquipmentId]
			, [Zone]
			, [ModifiedBy]
			, [CreatedBy]
		) VALUES (
			@AccountEquipmentID -- AccountEquipmentId - bigint
			, @NextZone-- Zone - varchar(3)
			, @CrmUserId -- ModifiedBy - nvarchar(50)
			, @CrmUserId -- CreatedBy - nvarchar(50)
		);
		-- Get Identity
		SET @AccountZoneAssignmentID = SCOPE_IDENTITY();



		-- add barcode tracking - reagan 07/24/2014
				/** Create ProductBarcodeTracking */
				INSERT [dbo].[IE_ProductBarcodeTracking](
					[ProductBarcodeTrackingTypeId]
					,[ProductBarcodeId]
					,[LocationTypeID]
					,[LocationID]
					,[ModifiedBy]
					,[CreatedBy]
				) VALUES (
						'CUST'
						,@BarcodeNumber
						,'Sold'
						,@AccountId
						,@GPEmployeeId
						,@GPEmployeeId
				);

				/** Get Identity */
				SET @ProductBarcodeTrackingID = SCOPE_IDENTITY();

			--END
			

				-- ProductBarcodeTracking
				UPDATE [dbo].[IE_ProductBarcodes] SET [LastProductBarcodeTrackingId] = @ProductBarcodeTrackingID
				WHERE  [ProductBarcodeID]= @BarcodeNumber

	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT *
	FROM
		[dbo].[vwMS_AccountEquipments] AS AEQ WITH (NOLOCK)
	WHERE
		(AEQ.AccountZoneAssignmentID = @AccountZoneAssignmentID);
	
END
GO

GRANT EXEC ON dbo.custMS_AccountEquipmentsViewNextAssignmentByBarcode TO PUBLIC
GO

/** EXEC dbo.custMS_AccountEquipmentsViewNextAssignmentByBarcode 120346, N'8901260910049574409F', 'SOSA001', 'PRIV001'; */