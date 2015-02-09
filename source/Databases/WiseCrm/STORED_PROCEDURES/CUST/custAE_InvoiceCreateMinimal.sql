USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceCreateMinimal')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceCreateMinimal'
		DROP  Procedure  dbo.custAE_InvoiceCreateMinimal
	END
GO

PRINT 'Creating Procedure custAE_InvoiceCreateMinimal'
GO
/******************************************************************************
**		File: custAE_InvoiceCreateMinimal.sql
**		Name: custAE_InvoiceCreateMinimal
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
**		Date: 01/03/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/03/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceCreateMinimal
(
	@AccountId BIGINT
	, @InvoiceTypeId VARCHAR(20) = 'INSTALL'
	, @CreatedBy NVARCHAR(50)
)
AS
BEGIN
	/** DECLARATIONS. */
	DECLARE @InvoiceID BIGINT;

	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** START TRY. */
	BEGIN TRY
		BEGIN TRANSACTION

		/** Create invoice. */
		INSERT INTO dbo.AE_Invoices	(
			AccountId
			, InvoiceTypeId
			, DocDate
			, SalesAmount
			, OriginalTransactionAmount
			, CostAmount
			, TaxAmount
			, ModifiedDate
			, ModifiedById
			, CreatedDate
			, CreatedById
		) VALUES (
			@AccountId
			, @InvoiceTypeId
			, GetUtcDate()
			, 0
			, 0
			, 0
			, 0
			, GetDate()
			, @CreatedBy
			, GetDate()
			, @CreatedBy
		);

		/** Get Identity */
		SET @InvoiceID = SCOPE_IDENTITY();
	
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC [dbo].wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT * FROM AE_Invoices WHERE (InvoiceID = @InvoiceID);
END
GO

GRANT EXEC ON dbo.custAE_InvoiceCreateMinimal TO PUBLIC
GO

/** Exec dbo.custAE_InvoiceCreateMinimal */