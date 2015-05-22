USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEquipmentsAddEquipment')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEquipmentsAddEquipment'
		DROP  Procedure  dbo.custMS_AccountEquipmentsAddEquipment
	END
GO

PRINT 'Creating Procedure custMS_AccountEquipmentsAddEquipment'
GO
/******************************************************************************
**		File: custMS_AccountEquipmentsAddEquipment.sql
**		Name: custMS_AccountEquipmentsAddEquipment
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
**		Date: 04/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/02/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountEquipmentsAddEquipment
(
	@AccountId BIGINT = NULL
	, @AccountEquipmentUpgradeTypeID VARCHAR(10)
	, @AccountEventId INT
	, @AccountZoneAssignmentID BIGINT
	, @AccountZoneTypeId VARCHAR(10)
	, @ActualPoints DECIMAL(5,2)
	, @BarcodeId NVARCHAR(50)
	, @Comments NVARCHAR(MAX)
	, @EquipmentId VARCHAR(50)
	, @EquipmentLocationId INT
	, @SalesmanId VARCHAR(50)
	, @IsExisting BIT
	, @IsExistingWiring BIT
	, @IsMainPanel BIT
	, @IsServiceUpgrade BIT
	, @ItemDesc NVARCHAR(101)
	, @ItemSKU NVARCHAR(50)
	, @Points DECIMAL(5,2)
	, @Price MONEY
	, @Zone VARCHAR(3)
	, @GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountIdStr VARCHAR(20) = CAST(@AccountID AS VARCHAR)
		, @ItemID VARCHAR(50) = @EquipmentId
		, @ModelNumber VARCHAR(30) = NULL
		, @InvoiceID BIGINT = NULL
		, @InvoiceItemId BIGINT = NULL
		, @AccountEquipmentID BIGINT = NULL
		, @TechnicianId VARCHAR(25);

	/** Initialize */
	SELECT
		@TechnicianId = CASE
			WHEN @AccountEquipmentUpgradeTypeID = 'TECH' THEN @SalesmanId
			ELSE NULL
		END 
	
	BEGIN TRY
		BEGIN TRANSACTION

		/** If there is a barcode lets findout what the ItemID is for that barcode. */
		IF(@BarcodeId IS NOT NULL)
		BEGIN
			/** Check that this is a legitimate BarcodeID. */
			IF(NOT EXISTS(SELECT * FROM dbo.IE_ProductBarcodes WHERE (ProductBarcodeID = @BarcodeId)))
				RAISERROR (N'The Account with MsAccountID of %s with BarcodeId ''%s'' could not find a product in the inventory.', 18, 1, @AccountIdStr, @BarcodeId);

			/** Get the ItemID */
			SELECT 
				@ItemID = POI.ItemId
			FROM	
				dbo.IE_ProductBarcodes AS IEPB WITH (NOLOCK)
				INNER JOIN dbo.IE_PurchaseOrderItems AS POI WITH (NOLOCK)
				ON
					(POI.PurchaseOrderItemID = IEPB.PurchaseOrderItemId)
					AND (IEPB.ProductBarcodeID = @BarcodeId);
		END

		/** Lookup the model number of the equipment. */
		SELECT @ModelNumber = ModelNumber FROM [dbo].[AE_Items] WHERE (ItemID = @EquipmentId);
		IF (@ModelNumber IS NULL)
			RAISERROR (N'The Account with MsAccountID of %s with EquipmentId ''%s'' could not find a Model Number for it.', 18, 1, @AccountIdStr, @EquipmentId);
		PRINT 'Model Number: ''' + @ModelNumber + '''';

		/** Find the INSTALL invoice. */
		SELECT @InvoiceID = AEI.InvoiceID FROM dbo.AE_Invoices AS AEI WITH (NOLOCK) WHERE (AccountId = @AccountId) AND (InvoiceTypeId = 'INSTALL') AND (IsActive = 1 AND IsDeleted = 0);
		IF (@InvoiceID IS NULL) 
			RAISERROR (N'Unable to find an Install Invoice for MsAccountID of %s with EquipmentId ''%s'' could not find a Model Number for it.', 18, 1, @AccountIdStr, @EquipmentId);
		
		/** Find an InvoiceItem that has not been assigned already. */
		SELECT
			@InvoiceItemId = AEII.InvoiceItemID
			-- AEII.*
		FROM
			dbo.AE_InvoiceItems AS AEII WITH (NOLOCK)
			INNER JOIN dbo.AE_Invoices AS AEI WITH (NOLOCK)
			ON
				(AEI.InvoiceID = AEII.InvoiceId)
				AND (AEI.InvoiceTypeId = 'INSTALL')
				AND (AEI.AccountId = @AccountId)
			INNER JOIN dbo.AE_Items AS AEIT WITH (NOLOCK)
			ON
				(AEII.ItemId = AEIT.ItemID)
				AND (AEIT.ModelNumber = @ModelNumber)
		WHERE
			(AEII.AccountEquipmentId IS NULL)
			AND (AEII.ProductBarcodeId IS NULL);
		/** Check to see if the InvoiceItem was found. */
		IF (@InvoiceItemId IS NULL)
		BEGIN
			PRINT 'Invoice ITEM not found.'
			INSERT INTO [dbo].[AE_InvoiceItems] (
						[InvoiceId]
						,[ItemId]
						,[ProductBarcodeId]
						,[AccountEquipmentId]
						,[TaxOptionId]
						,[Qty]
						,[Cost]
						,[RetailPrice]
						,[PriceWithTax]
						,[SystemPoints]
						,[SalesmanId]
						,[TechnicianId]
						,[IsCustomerPaying]
						,[ModifiedBy]
						,[CreatedBy]
					) 
						SELECT
							@InvoiceID
							, @ItemId
							, @BarcodeId -- <ProductBarcodeId, nvarchar(50),>
							, NULL -- <AccountEquipmentId, bigint,>
							, 'TAX' -- <TaxOptionId, char(3),>
							, 1 -- <Qty, smallint,>
							, AEIT.Cost -- <Cost, money,>
							, AEIT.Price -- <RetailPrice, money,>
							, AEIT.Price -- <PriceWithTax, money,>
							, AEIT.SystemPoints -- <SystemPoints, decimal(9,2),>
							, @SalesmanId -- <SalesmanId, nvarchar(25),>
							, @TechnicianId -- <TechnicianId, nvarchar(25),>
							, @IsServiceUpgrade -- <IsCustomerPaying, bit,>
							, @GPEmployeeId -- <ModifiedBy, nvarchar(50),>
							, @GPEmployeeId -- <CreatedBy, nvarchar(50),>
						FROM
							[dbo].[AE_Items] AS AEIT WITH (NOLOCK)
						WHERE 
							(AEIT.ItemID = @ItemID);
			SET @InvoiceItemId = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			PRINT 'Invoice ITEM found. InvoiceItemID: ' + CAST(@InvoiceItemId AS VARCHAR) + ' | ITEMID: ' + @ItemID ;

			UPDATE AEII SET
				[ItemId] = @ItemID
				,[ProductBarcodeId] = @BarcodeId
				,[TaxOptionId] = 'TAX'
				,[Qty] = 1
				,[Cost] = AEIT.Cost 
				,[RetailPrice] = AEIT.Price
				,[PriceWithTax] = AEIT.Price
				,[SystemPoints] = AEIT.SystemPoints
				,[SalesmanId] = @SalesmanId
				,[TechnicianId] = @TechnicianId
				,[IsCustomerPaying] = @IsServiceUpgrade
				,[ModifiedOn] = GETUTCDATE()
				,[ModifiedBy] = @GPEmployeeId
			FROM 
				[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
				INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
				ON
					--(AEIT.ItemID = AEII.ItemId)
					(AEIT.ItemID = @ItemID)
			WHERE
				(AEII.InvoiceItemID = @InvoiceItemId);

			PRINT 'Updated BarcodeID in InvoiceItems: ' + @BarcodeId;
		END

		/**************************************
		*** Create MS_AccountEquipments		***
		***************************************/
		INSERT INTO [dbo].[MS_AccountEquipment] (
			[AccountId]
			,[EquipmentId]
			,[EquipmentLocationId]
			,[InvoiceItemId]
			,[GPEmployeeId]
			,[AccountEquipmentUpgradeTypeId]
			,[Points]
			,[ActualPoints]
			,[Price]
			,[IsExisting]
			,[BarcodeId]
			,[IsServiceUpgrade]
			,[IsExistingWiring]
			,[IsMainPanel]
			,[ModifiedBy]
			,[CreatedBy]
		) VALUES (
			@AccountId
			, @ItemId
			, @EquipmentLocationId
			, @InvoiceItemId
			, ISNULL(@SalesmanID, @TechnicianID)
			, @AccountEquipmentUpgradeTypeId
			, @ActualPoints
			, @Points
			, @Price
			, @IsExisting
			, @BarcodeId
			, @IsServiceUpgrade
			, @IsExistingWiring
			, @IsMainPanel
			, @GPEmployeeId
			, @GPEmployeeId
		);
		SET @AccountEquipmentID = SCOPE_IDENTITY();

		UPDATE dbo.AE_InvoiceItems SET AccountEquipmentId = @AccountEquipmentID WHERE (InvoiceItemID = @InvoiceItemID);
		PRINT 'UPDATED the AE_InvoiceItems table.  With BarcodeID of ' + @BarcodeId;

		/********************************************
		*** Create the Zone stuff.
		*********************************************/
		INSERT INTO [dbo].[MS_AccountZoneAssignments] (
			[AccountEquipmentId]
			,[AccountZoneTypeId]
			,[AccountEventId]
			,[Zone]
			,[Comments]
			,[IsExisting]
			,[ModifiedBy]
			,[CreatedBy]
		) VALUES (
			@AccountEquipmentId
			, @AccountZoneTypeId
			, @AccountEventId
			, @Zone
			, @Comments
			, @IsExisting
			, @GpEmployeeID
			, @GpEmployeeID
		);

		/**** DEBUGGING 
		SELECT * FROM dbo.AE_InvoiceItems WHERE InvoiceItemID = @InvoiceItemID;
		SELECT * FROM dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId = @AccountEquipmentID);
		*/
		SELECT * FROM dbo.MS_AccountEquipment WHERE (AccountEquipmentID = @AccountEquipmentID);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountEquipmentsAddEquipment TO PUBLIC
GO

/** UT
EXEC dbo.custMS_AccountEquipmentsAddEquipment 191230, 'CUST', NULL, NULL, 'NOZONE', 2.5, '716521312', 'Z WAVE THERMOST', 'EQPM_INVT961', NULL, 'VAZQA001', 'DARGC001', 'FALSE', 'FALSE', 'FALSE', 'FALSE', 'Z WAVE THERMOSTAT PROGRAMMABLE', '2GIGZCT100', 2, 67.2, '000', 'SOSAA001'
 */