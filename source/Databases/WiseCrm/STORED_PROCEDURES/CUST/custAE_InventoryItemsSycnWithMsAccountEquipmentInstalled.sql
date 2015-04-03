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
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Check that the account has not been submitted to CS. 
	IF(EXISTS(SELECT AccountID FROM [dbo].[MS_AccountSalesInformations] WHERE (AccountID = @AccountID) AND (InstallDate IS NOT NULL)))
	BEGIN
		RETURN;
	END
	*/

	/** DECLARATIONS */
	DECLARE @AccountEquipmentID BIGINT
		, @EquipmentId VARCHAR(50)
		, @ActualPoints FLOAT
		, @BarcodeId NVARCHAR(25)
		, @ItemSKU VARCHAR(20)
		, @InvoiceItemID BIGINT
	
	BEGIN TRY
		BEGIN TRANSACTION

		/** Create a cursor */
		DECLARE accountEqCursor CURSOR FOR
		SELECT MSAE.AccountEquipmentID, MSAE.EquipmentId, MSAE.ActualPoints, MSAE.BarcodeId , AEI.ItemSKU
		FROM 
			[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
			INNER JOIN [dbo].[AE_Items] AS AEI WITH (NOLOCK)
			ON
				(AEI.ItemID = MSAE.EquipmentId)
		WHERE
			(AccountId = @AccountID) 
			AND (InvoiceItemID IS NULL) 
			AND (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0);

		OPEN accountEqCursor;

		FETCH NEXT FROM accountEqCursor
		INTO @AccountEquipmentID, @EquipmentId, @ActualPoints, @BarcodeId, @ItemSKU

		WHILE(@@FETCH_STATUS = 0)
		BEGIN
			/** Initialize */
			SET @InvoiceItemID = NULL;

			/** Find a matching SKU with no barcode ID */
			SELECT TOP 1 
				@InvoiceItemID = InvoiceItemID 
			FROM
				[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
				INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
				ON
					(AEII.InvoiceId = AEI.InvoiceID)
					AND (AEI.InvoiceTypeId = 'INSTALL')
					AND (AEI.AccountId = @AccountID)
			WHERE
				(AEII.ItemId = @EquipmentId)
				AND (AEII.ProductBarcodeId IS NULL)
				AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
				AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)

			/** Check that there is an invoiceitem otherwise create one. */
			IF (@InvoiceItemID IS NULL)
			BEGIN
				PRINT 'No Invoice Item found for EquipmentID: ' + @EquipmentId + ',  AccountEquipmentID: ' + CAST(@AccountEquipmentID AS VARCHAR) + ', and Part #: ' + @ItemSKU;
			END
			ELSE
			BEGIN
				PRINT 'Invoice Item ' + CAST(@InvoiceItemID AS VARCHAR) + ' for EquipmentID: ' + @EquipmentId + ', AccountEquipmentID: ' + CAST(@AccountEquipmentID AS VARCHAR) + ', and Part #: ' + @ItemSKU;
			END

			/** Move to the next item. */
			FETCH NEXT FROM accountEqCursor
			INTO @AccountEquipmentID, @EquipmentId, @ActualPoints, @BarcodeId, @ItemSKU
		END

		CLOSE accountEqCursor;
		DEALLOCATE accountEqCursor;

		ROLLBACK TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled TO PUBLIC
GO

/** EXEC dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled 150927 */