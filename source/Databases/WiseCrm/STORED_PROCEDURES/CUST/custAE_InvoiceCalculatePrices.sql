USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceCalculatePrices')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceCalculatePrices'
		DROP  Procedure  dbo.custAE_InvoiceCalculatePrices
	END
GO

PRINT 'Creating Procedure custAE_InvoiceCalculatePrices'
GO
/******************************************************************************
**		File: custAE_InvoiceCalculatePrices.sql
**		Name: custAE_InvoiceCalculatePrices
**		Desc: This proc will take the invoice and treat it like a new invoice
** by calculating its prices.  This will be done by traversing all its
** invoice items
**
**		This template can be customized:
**              
**		Return values: AE_Invoices row.
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceCalculatePrices
(
	@InvoiceID BIGINT
	, @StateID VARCHAR(4)
	, @PostalCode VARCHAR(5)
	, @HideInvoiceHeader BIT = true
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON;
	
	/** Initialization */
	DECLARE @TotalRetail MONEY
		  , @PriceWithTax MONEY
		  , @Cost MONEY;
	SET @TotalRetail = 0.00;
	SET @PriceWithTax = 0.00;
	SET @Cost = 0.00;

	BEGIN TRY
	
		BEGIN TRANSACTION
		/** Calculate totals with taxed on items. */	
		UPDATE dbo.AE_InvoiceItems SET 
			PriceWithTax = CAST (CASE
							WHEN AII.TaxOptionId = 'EXT' THEN 1.00  -- Tax Exempt.
							ELSE dbo.fxGetTaxRateByStateIdAndPostal(@StateID, @PostalCode) -- Sales Tax will need to calculate based on State or Zip
						  END AS MONEY) * AII.RetailPrice
		FROM
			dbo.AE_InvoiceItems AS AII WITH (NOLOCK)
		WHERE
			(AII.InvoiceId = @InvoiceID)
			
		/** Sum totals */
		SELECT 
			@TotalRetail = SUM(AII.RetailPrice)
			, @PriceWithTax = SUM (AII.PriceWithTax)
			, @Cost = SUM(AII.Cost)
		FROM
			dbo.AE_InvoiceItems AS AII WITH (NOLOCK)
		WHERE
			(AII.InvoiceId = @InvoiceID)
			AND (AII.IsDeleted = 0)
			AND (AII.IsActive = 1);
		

		/** Update Invoice row */
		UPDATE dbo.AE_Invoices SET 
			CurrentTransactionAmount = @PriceWithTax
			, OriginalTransactionAmount = @PriceWithTax
			, SalesAmount = @PriceWithTax
			, CostAmount = @Cost
			, TaxAmount = @PriceWithTax - @TotalRetail
		WHERE
			(InvoiceID = @InvoiceID)
			
		COMMIT TRANSACTION
				
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN	
	END CATCH
	
	/** Return result */
	IF (@HideInvoiceHeader = 0)
	BEGIN
		SELECT * FROM AE_Invoices WHERE InvoiceID = @InvoiceID;
	END
END
GO

GRANT EXEC ON dbo.custAE_InvoiceCalculatePrices TO PUBLIC
GO