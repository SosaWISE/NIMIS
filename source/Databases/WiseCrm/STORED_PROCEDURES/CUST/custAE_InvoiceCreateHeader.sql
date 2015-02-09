USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceCreateHeader')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceCreateHeader'
		DROP  Procedure  dbo.custAE_InvoiceCreateHeader
	END
GO

PRINT 'Creating Procedure custAE_InvoiceCreateHeader'
GO
/******************************************************************************
**		File: custAE_InvoiceCreateHeader.sql
**		Name: custAE_InvoiceCreateHeader
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
**		Date: 05/27/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/27/2012	Andres Sosa		Created By
**			
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceCreateHeader
(
	@AccountId BIGINT
	, @InvoiceTypeID VARCHAR(20)
	, @TaxScheduleId INT = NULL
	, @PaymentTermId INT = NULL
	, @SalesAmount MONEY = 0
	, @OriginalTransactionAmount MONEY = 0
	, @CurrentTransactionAmount MONEY = 0
	, @CostAmount MONEY = 0
	, @TaxAmount MONEY = 0
	, @ContractID BIGINT
	, @DocDate DATETIME = NULL
	, @PostedDate DATETIME = NULL
	, @DueDate DATETIME = NULL
	, @GLPostDate DATETIME = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		BEGIN TRANSACTION

		/** Initialize. */
		IF (@SalesAmount IS NULL) SET @SalesAmount = 0;
		IF (@OriginalTransactionAmount IS NULL) SET @OriginalTransactionAmount = 0;
		IF (@CurrentTransactionAmount IS NULL) SET @CurrentTransactionAmount = 0;
		IF (@CostAmount IS NULL) SET @CostAmount = 0;
		IF (@TaxAmount IS NULL) SET @TaxAmount = 0;

		-- Documented Date
		IF (@DocDate IS NULL) SET @DocDate = GETDATE();
		IF (@PostedDate IS NULL) SET @PostedDate = GETDATE();
		IF (@DueDate IS NULL) SET @DueDate = GETDATE();
		
		/** Create an Invoice */
		INSERT INTO dbo.AE_Invoices (
			AccountId
			, InvoiceTypeId
			, ContractId
			, TaxScheduleId
			, PaymentTermId
			, DocDate
			, PostedDate
			, DueDate
			, GLPostDate
			, CurrentTransactionAmount
			, SalesAmount
			, OriginalTransactionAmount
			, CostAmount
			, TaxAmount
		) VALUES (
			@AccountId , -- AccountId - bigint
			@InvoiceTypeId , -- InvoiceTypeId - varchar(20)
			@ContractID , -- ContractId - int
			@TaxScheduleId , -- TaxScheduleId - int
			@PaymentTermId , -- PaymentTermId - int
			@DocDate , -- DocDate - date
			@PostedDate , -- PostedDate - date
			@DueDate , -- DueDate - date
			@GLPostDate , -- GLPostDate - date
			@CurrentTransactionAmount , -- CurrentTransactionAmount - money
			@SalesAmount , -- SalesAmount - money
			@OriginalTransactionAmount , -- OriginalTransactionAmount - money
			@CostAmount , -- CostAmount - money
			@TaxAmount  -- TaxAmount - money
		);

		/** Get the IDentity value. */
		DECLARE @InvoiceID BIGINT;
		SET @InvoiceID = SCOPE_IDENTITY();

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN	
	END CATCH	

	/** Return result */
	SELECT * FROM dbo.AE_Invoices WHERE (InvoiceID = @InvoiceID);

END
GO

GRANT EXEC ON dbo.custAE_InvoiceCreateHeader TO PUBLIC
GO