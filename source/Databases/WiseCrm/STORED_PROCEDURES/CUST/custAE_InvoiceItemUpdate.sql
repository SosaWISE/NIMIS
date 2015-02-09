USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceItemUpdate')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceItemUpdate'
		DROP  Procedure  dbo.custAE_InvoiceItemUpdate
	END
GO

PRINT 'Creating Procedure custAE_InvoiceItemUpdate'
GO
/******************************************************************************
**		File: custAE_InvoiceItemUpdate.sql
**		Name: custAE_InvoiceItemUpdate
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
CREATE Procedure dbo.custAE_InvoiceItemUpdate
(
	@InvoiceItemID BIGINT
	, @Qty INT
	, @Price MONEY
	, @SystemPoints DECIMAL(9,1)
	, @SalesmanID NVARCHAR(25)
	, @TechnicianID NVARCHAR(25)
	, @GpEmployeeID NVARCHAR(25)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @StateID VARCHAR(4) = 'UT', @PostalCode VARCHAR(5) = '84097', @InvoiceId BIGINT;

	/** Find InvoiceId. */
	SELECT @InvoiceId = InvoiceId FROM [dbo].[AE_InvoiceItems] WHERE InvoiceItemID = @InvoiceItemID;
	if (@InvoiceId IS NULL) RETURN;

	/** Get StateID and PostalCode. */
	SELECT @StateID = StateId, @PostalCode = PostalCode FROM [dbo].fxGetMcAddressByInvoiceId(@InvoiceID);
		
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
		UPDATE [dbo].[AE_InvoiceItems] SET
			  [Qty] = @Qty
			  ,[RetailPrice] = @Price
			  ,[PriceWithTax] = @Price
			  ,[SystemPoints] = @SystemPoints
			  ,[SalesmanId] = @SalesmanID
			  ,[TechnicianId] = @TechnicianID
			  ,[ModifiedOn] = GetUTCDate()
			  ,[ModifiedBy] = @GpEmployeeID
		 WHERE
			(InvoiceItemID = @InvoiceItemID);

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
	SELECT * FROM [dbo].vwAE_InvoiceItems WHERE (InvoiceItemId = @InvoiceItemID);
END
GO

GRANT EXEC ON dbo.custAE_InvoiceItemUpdate TO PUBLIC
GO

/** 
	EXEC dbo.custAE_InvoiceItemUpdate 10010167, 'EQPM_INVT123', 9, 500, 10, 'PRIVIT', null, 'SOSA001';
*/