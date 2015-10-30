USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceItemCreate')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceItemCreate'
		DROP  Procedure  dbo.custAE_InvoiceItemCreate
	END
GO

PRINT 'Creating Procedure custAE_InvoiceItemCreate'
GO
/******************************************************************************
**		File: custAE_InvoiceItemCreate.sql
**		Name: custAE_InvoiceItemCreate
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
CREATE Procedure dbo.custAE_InvoiceItemCreate
(
	@InvoiceId BIGINT
	, @ItemId VARCHAR(50)
	, @Qty SMALLINT
	, @SalesmanId NVARCHAR(25)
	, @TechnicianId NVARCHAR(25)
	, @GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @InvoiceItemID BIGINT,
		@TaxOptionId CHAR(3),
		@Cost MONEY,
		@RetailPrice MONEY,
		@PriceWithTax MONEY;

	BEGIN TRY
		BEGIN TRANSACTION;

			/** Get Items information */
			IF (NOT EXISTS(SELECT * FROM AE_Items WHERE (ItemID = @ItemId)))
			BEGIN
				RAISERROR (N'The ItemId ''%s'' did not produce a value from the AE_Items table.', 19, 1, @ItemId) ;
			END
			SELECT @TaxOptionId = TaxOptionId, @Cost = Cost, @RetailPrice = Price, @PriceWithTax = Price FROM [dbo].AE_Items WHERE (ItemID = @ItemId);
	
			/** Create InventoryItem */
			INSERT dbo.AE_InvoiceItems (
				InvoiceId,
				ItemId,
				TaxOptionId,
				Qty,
				Cost,
				RetailPrice,
				PriceWithTax,
				SalesmanId,
				TechnicianId,
				ModifiedBy,
				CreatedBy
			) VALUES (
				@InvoiceId, -- InvoiceId - bigint
				@ItemId, -- ItemId - varchar(50)
				@TaxOptionId, -- TaxOptionId - char(3)
				@Qty, -- Qty - smallint
				@Cost, -- Cost - money
				@RetailPrice, -- RetailPrice - money
				@RetailPrice * @Qty, -- PriceWithTax - money
				@SalesmanId,
				@TechnicianId,
				@GPEmployeeId,
				@GPEmployeeId
			);

			/** Get Identity */
			SET @InvoiceItemID = SCOPE_IDENTITY();

			/** Update Invoice Header information. */
			EXEC dbo.custAE_InvoiceRefreshHeader @InvoiceId, @GPEmployeeId;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT * FROM [dbo].vwAE_InvoiceItems WHERE (InvoiceItemID = @InvoiceItemID);
END
GO

GRANT EXEC ON dbo.custAE_InvoiceItemCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custAE_InvoiceItemCreate 213121, '234234', 1;  */