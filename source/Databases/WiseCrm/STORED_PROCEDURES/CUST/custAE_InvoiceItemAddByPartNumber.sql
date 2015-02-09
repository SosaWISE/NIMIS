USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceItemAddByPartNumber')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceItemAddByPartNumber'
		DROP  Procedure  dbo.custAE_InvoiceItemAddByPartNumber
	END
GO

PRINT 'Creating Procedure custAE_InvoiceItemAddByPartNumber'
GO
/******************************************************************************
**		File: custAE_InvoiceItemAddByPartNumber.sql
**		Name: custAE_InvoiceItemAddByPartNumber
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
CREATE Procedure dbo.custAE_InvoiceItemAddByPartNumber
(
	@InvoiceID BIGINT
	, @ItemSku NVARCHAR(50)
	, @Qty INT
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

	/** Get ItemId from Items table */
	SELECT @ItemId = ITM.ItemID FROM [dbo].AE_Items AS ITM WITH (NOLOCK) WHERE (ITM.ItemSKU = @ItemSku AND ITM.IsActive = 1 AND ITM.IsDeleted = 0);
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
		INSERT INTO [dbo].[AE_InvoiceItems] (
			[InvoiceId]
			,[ItemId]
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
			, ITM.TaxOptionId
			, @Qty
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

GRANT EXEC ON dbo.custAE_InvoiceItemAddByPartNumber TO PUBLIC
GO

/** EXEC dbo.custAE_InvoiceItemAddByPartNumber */