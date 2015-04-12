USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled')
	BEGIN
		PRINT 'Dropping Procedure custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled'
		DROP  Procedure  dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled
	END
GO

PRINT 'Creating Procedure custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled'
GO
/******************************************************************************
**		File: custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled.sql
**		Name: custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled
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
CREATE Procedure dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled
(
	@AccountID BIGINT = NULL
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Check that the account has not been submitted to CS. */
	--IF(EXISTS(SELECT AccountID FROM [dbo].[MS_AccountSalesInformations] WHERE (AccountID = @AccountID) AND (InstallDate IS NOT NULL)))
	--BEGIN
	--	RETURN;
	--END
	

	/** DECLARATIONS */
	DECLARE @AccountEquipmentID BIGINT
		, @EquipmentId VARCHAR(50)
		, @ItemId VARCHAR(50)
		, @ActualPoints FLOAT
		, @BarcodeId NVARCHAR(25)
		, @ItemSKU VARCHAR(20)
		, @InvoiceID BIGINT
		, @InvoiceItemID BIGINT
		, @RetailPrice MONEY
		, @ModelNumber VARCHAR(30)
		, @Count INT = 0
	
	BEGIN TRY
		BEGIN TRANSACTION
		/** Initialize */
		SELECT @InvoiceID = InvoiceID FROM [dbo].[AE_Invoices] WHERE (AccountId = @AccountID) AND (InvoiceTypeId = 'INSTALL') AND (IsActive = 1 AND IsDeleted = 0);
		IF (@InvoiceID IS NULL)
		BEGIN
			DECLARE @AccountIdStr VARCHAR(20) = CAST(@AccountID AS VARCHAR);
			RAISERROR (N'The Account with MsAccountID of %s does not have an INSTALL invoice.', 18, 1, @AccountIdStr);
		END
		PRINT 'InvoiceID: ' + CAST(@InvoiceID AS VARCHAR);

		/** Create a cursor */
		DECLARE accountEqCursor CURSOR FOR
		SELECT MSAE.AccountEquipmentID, MSAE.EquipmentId, MSAE.ActualPoints, MSAE.BarcodeId , AEI.ItemSKU, AEI.ModelNumber
		FROM 
			[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
			INNER JOIN [dbo].[AE_Items] AS AEI WITH (NOLOCK)
			ON
				(AEI.ItemID = MSAE.EquipmentId)
		WHERE
			(AccountId = @AccountID) 
			AND (MSAE.InvoiceItemID IS NULL) 
			AND (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0);

		OPEN accountEqCursor;

		FETCH NEXT FROM accountEqCursor
		INTO @AccountEquipmentID, @EquipmentId, @ActualPoints, @BarcodeId, @ItemSKU, @ModelNumber

		WHILE(@@FETCH_STATUS = 0)
		BEGIN
			/** Initialize */
			SET @InvoiceItemID = NULL;
			SET @Count = @Count + 1;
			DECLARE @Temp BIGINT = NULL;

			/** Find a matching SKU with no barcode ID */
			SELECT TOP 1 
				@InvoiceItemID = AEII.InvoiceItemID
				, @Temp = AEII.AccountEquipmentId
			FROM
				[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
				INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
				ON
					(AEIT.ItemID = AEII.ItemId)
			WHERE
				(AEII.InvoiceId = @InvoiceID)
				--AND (AEII.ItemId = @EquipmentId)
				AND (AEIT.ModelNumber = @ModelNumber)
				AND (AEII.ProductBarcodeId IS NULL)
				AND (AEII.AccountEquipmentId IS NULL)
				AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0);

			--PRINT 'InvoiceItemID Found ' + CAST(@InvoiceItemID AS VARCHAR)

			/** Check that there is an invoiceitem otherwise create one. */
			IF (@InvoiceItemID IS NULL)
			BEGIN
				INSERT INTO [dbo].[AE_InvoiceItems] (
					[InvoiceId]
					, [ItemId]
					, [ProductBarcodeId]
					, [AccountEquipmentId]
					, [TaxOptionId]
					, [Qty]
					, [Cost]
					, [RetailPrice]
					, [PriceWithTax]
					, [SystemPoints]
					, [SalesmanId]
					, [TechnicianId]
					, [ModifiedBy]
					, [CreatedBy]
				) 
				SELECT
					@InvoiceID AS InvoiceId
					, MSAE.EquipmentId AS ItemId
					, MSAE.BarcodeId AS ProductBarcodeId
					, MSAE.AccountEquipmentID
					, AEIT.TaxOptionId
					, 1 AS Qty
					, AEIT.Cost
					, AEIT.Price AS RetailPrice
					, NULL AS PriceWithTax
					, AEIT.SystemPoints
					, CASE
						WHEN MSAE.AccountEquipmentUpgradeTypeId = 'SALESREP' THEN MSAE.GPEmployeeId
						ELSE NULL
					  END AS SalesmanId
					, CASE
						WHEN MSAE.AccountEquipmentUpgradeTypeId = 'TECH' THEN MSAE.GPEmployeeId
						ELSE NULL
					  END AS TechnicianId
					, @GPEmployeeId
					, @GPEmployeeId
				FROM
					[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
					INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
					ON
						(AEIT.ItemID = MSAE.EquipmentId)
				WHERE
					(MSAE.AccountEquipmentID = @AccountEquipmentID);

				SET @InvoiceItemID = SCOPE_IDENTITY();
				PRINT 'INSERT -- No Invoice Item found for EquipmentID: ' + @EquipmentId + ',  AccountEquipmentID: ' + CAST(@AccountEquipmentID AS VARCHAR) + ', and Part #: ' + @ItemSKU + ' | New InvoiceItemID: ' + CAST(@InvoiceItemID AS VARCHAR);
			END
			ELSE
			BEGIN
				PRINT 'UPDATE -- Invoice Item ' + CAST(@InvoiceItemID AS VARCHAR) + ' for EquipmentID: ' + @EquipmentId + ', AccountEquipmentID: ' + CAST(@AccountEquipmentID AS VARCHAR) + ', and Part #: ' + @ItemSKU;

				/** Figure out SalesmanId and TechnicianId */
				UPDATE [dbo].[AE_InvoiceItems] SET 
					AccountEquipmentId = MSAE.AccountEquipmentID
					, ProductBarcodeId = MSAE.BarcodeId
					, TaxOptionId = AEIT.TaxOptionId
					, Cost = AEIT.Cost
					, RetailPrice = AEIT.Price
					, SystemPoints = AEIT.SystemPoints
					, SalesmanId = CASE
						WHEN MSAE.AccountEquipmentUpgradeTypeId = 'SALESREP' THEN MSAE.GPEmployeeId
						ELSE NULL
					  END
					, TechnicianId = CASE
						WHEN MSAE.AccountEquipmentUpgradeTypeId = 'TECH' THEN MSAE.GPEmployeeId
						ELSE NULL
					  END
					, ModifiedBy = @GPEmployeeId
				FROM
					[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
					INNER JOIN [dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
					ON
						(MSAE.AccountEquipmentID = @AccountEquipmentId)
					INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
					ON
						(AEIT.ItemID = AEII.ItemId)
				WHERE
					(AEII.InvoiceItemID = @InvoiceItemID);
			END

			/** Check the Rowcount */
			--PRINT 'UPDATE CHECK OF ROWCOUT: ' + CAST(@@ROWCOUNT AS VARCHAR);

			/** Save InvoiceItemID into MS_AccountEquipments Tables*/
			UPDATE [dbo].[MS_AccountEquipment] SET InvoiceItemId = @InvoiceItemID WHERE (AccountEquipmentID = @AccountEquipmentID);

			/** Move to the next item. */
			FETCH NEXT FROM accountEqCursor
			INTO @AccountEquipmentID, @EquipmentId, @ActualPoints, @BarcodeId, @ItemSKU, @ModelNumber
		END

		CLOSE accountEqCursor;
		DEALLOCATE accountEqCursor;
		
		PRINT 'Total Rows Modified: ' + CAST(@Count AS VARCHAR);
		PRINT '============= NOW MOVING ON TO MS_AccountEquipment ==================='
		/*
		* Next Step is to loop through all those Invoice Items that do not have an AccountEquipmentID
		* DESC:  Only loop through things that are inventoried and have a cost
		*/
		DECLARE invoiceItemCursor CURSOR FOR
		SELECT AEII.InvoiceItemId, AEII.ItemId, AEII.SystemPoints, AEII.ProductBarcodeId, AEIT.ItemSKU, AEII.RetailPrice
		FROM
			[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
			INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
			ON
				(AEIT.ItemID = AEII.ItemId)
				AND (AEIT.ItemTypeId = 'EQPM_INVT' OR AEIT.ItemTypeId = 'EQPM_INVT_MS' OR AEIT.ItemTypeId = 'EQPM_EXST' OR AEIT.ItemTypeId = 'EQPM_EXST_MS')
		WHERE
			(AEII.InvoiceId = @InvoiceID)
			AND (AEII.AccountEquipmentId IS NULL)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0);

		OPEN invoiceItemCursor;

		FETCH NEXT FROM invoiceItemCursor
		INTO @InvoiceItemId, @ItemID, @ActualPoints, @BarcodeId, @ItemSKU, @RetailPrice

		SET @Count = 0;

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			/** Initialize */
			SET @Count = @Count + 1;
			SET @AccountEquipmentID = NULL;

			/** Check that there is a relationship otherwise create a row*/
			SELECT TOP 1
				@AccountEquipmentID = AccountEquipmentID
			FROM
				[dbo].[MS_AccountEquipment] AS MSAEQ WITH (NOLOCK)
			WHERE
				(MSAEQ.InvoiceItemId IS NULL)
				AND (MSAEQ.EquipmentId = @ItemID)
				AND (MSAEQ.AccountId = @AccountID)
				AND (MSAEQ.IsActive = 1 AND MSAEQ.IsDeleted = 0)
			
			/** Create or Update a record */
			IF (@AccountEquipmentID IS NULL)
			BEGIN
				PRINT 'Creating a record';
				INSERT INTO [dbo].[MS_AccountEquipment] ( 
					[AccountId]
					, [EquipmentId]
					, [InvoiceItemId]
					, [GPEmployeeId]
					, [AccountEquipmentUpgradeTypeId]
					, [Points]
					, [ActualPoints]
					, [Price]
					, [IsExisting]
					, [BarcodeId]
					, [IsServiceUpgrade]
					, [IsExistingWiring]
					, [IsMainPanel]
					, [ModifiedBy]
					, [CreatedBy]
				)
				SELECT
					@AccountID -- AccountId - bigint
					, @ItemId -- EquipmentId - varchar(50)
					, @InvoiceItemID -- InvoiceItemId - bigint
					, CASE
						WHEN AEII.SalesmanId IS NOT NULL THEN AEII.SalesmanId
						WHEN AEII.TechnicianId IS NOT NULL THEN AEII.TechnicianId
						ELSE NULL
					  END --NULL -- GPEmployeeId - varchar(50)
					, CASE
						WHEN AEII.SalesmanId IS NOT NULL THEN 'SALESREP'
						WHEN AEII.TechnicianId IS NOT NULL THEN 'TECH'
						ELSE 'CUST'
					  END -- AccountEquipmentUpgradeTypeId - varchar(10)
					, @ActualPoints -- Points - int
					, @ActualPoints -- ActualPoints - float
					, @RetailPrice -- Price - money
					, 0 -- IsExisting - bit
					, @BarcodeId -- BarcodeId - nvarchar(25)
					, 0 -- IsServiceUpgrade - bit
					, 0 -- IsExistingWiring - bit
					, 0 -- IsMainPanel - bit
					, @GpEmployeeId -- ModifiedBy - nvarchar(50)
					, @GpEmployeeId -- CreatedBy - nvarchar(50)
				FROM
					[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
				WHERE
					(InvoiceItemID = @InvoiceItemID);

				SET @AccountEquipmentId = SCOPE_IDENTITY();
			END
			ELSE
			BEGIN
				PRINT 'Updating a record.';
				UPDATE [dbo].[MS_AccountEquipment] SET
					InvoiceItemId = @InvoiceItemId
					, GPEmployeeId = CASE
						WHEN AEII.SalesmanId IS NOT NULL THEN AEII.SalesmanId
						WHEN AEII.TechnicianId IS NOT NULL THEN AEII.TechnicianId
						ELSE NULL
					  END
					, AccountEquipmentUpgradeTypeId = CASE
						WHEN AEII.SalesmanId IS NOT NULL THEN 'SALESREP'
						WHEN AEII.TechnicianId IS NOT NULL THEN 'TECH'
						ELSE 'CUST'
					  END
					, BarcodeId = @BarcodeId
					, Points = @ActualPoints
					, Price = @RetailPrice
				FROM
					[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
					INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
					ON
						--(AEII.AccountEquipmentId = MSAE.AccountEquipmentID)
						(AEII.InvoiceItemID = @InvoiceItemID)
				WHERE
					(MSAE.AccountEquipmentID = @AccountEquipmentId);

				PRINT 'ROW COUNT: ' + CAST(@@ROWCOUNT AS VARCHAR);
			END

			/** Save information */
			UPDATE [dbo].[AE_InvoiceItems] SET AccountEquipmentId = @AccountEquipmentId WHERE InvoiceItemID = @InvoiceItemID;

			/** Move to next item */
			FETCH NEXT FROM invoiceItemCursor
			INTO @InvoiceItemId, @ItemID, @ActualPoints, @BarcodeId, @ItemSKU, @RetailPrice
		END

		CLOSE invoiceItemCursor;
		DEALLOCATE invoiceItemCursor;

		PRINT 'Total Rows Modified: ' + CAST(@Count AS VARCHAR);
	/**
	* DEBUGGING REMOVE LATER
	*/
SELECT * FROM [dbo].[vwAE_InvoiceItems] AS AEII WITH (NOLOCK) WHERE (AEII.IsActive = 1 AND AEII.IsDeleted = 0) AND (AEII.InvoiceID = @InvoiceID);
SELECT * FROM [dbo].[vwMS_AccountEquipmentsAll] AS MSAE WITH (NOLOCK) WHERE (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0) AND (AccountId = @AccountId);
	
		ROLLBACK TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return results 
	SELECT * FROM [dbo].[vwAE_InvoiceItems] AS AEII WITH (NOLOCK) WHERE (AEII.IsActive = 1 AND AEII.IsDeleted = 0) AND (AEII.InvoiceID = @InvoiceID);
	SELECT * FROM [dbo].[vwMS_AccountEquipmentsAll] AS MSAE WITH (NOLOCK) WHERE (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0) AND (AccountId = @AccountId);
		*/
END
GO

GRANT EXEC ON dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled TO PUBLIC
GO

/** EXEC dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled 191168, 'STAKE001' */

--SELECT AEII.InvoiceItemId, AEII.ItemId, AEII.SystemPoints, AEII.ProductBarcodeId, AEIT.ItemSKU, AEII.RetailPrice
--		FROM
--			[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
--			INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
--			ON
--				(AEIT.ItemID = AEII.ItemId)
--				AND (AEIT.ItemTypeId = 'EQPM_INVT' OR AEIT.ItemTypeId = 'EQPM_INVT_MS' OR AEIT.ItemTypeId = 'EQPM_EXST' OR AEIT.ItemTypeId = 'EQPM_EXST_MS')
--		WHERE
--			(AEII.InvoiceId = 10060465)
--			AND (AEII.AccountEquipmentId IS NULL)
--			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0);