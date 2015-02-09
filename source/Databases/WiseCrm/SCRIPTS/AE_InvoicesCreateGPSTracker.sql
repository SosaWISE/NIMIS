USE WISE_CRM
GO

BEGIN TRANSACTION

/** Initialize. */
--SELECT * FROM dbo.MC_Accounts
DECLARE @AccountId BIGINT
SET @AccountId = 100165 --TODO:  Argument
--SELECT * FROM dbo.AE_InvoiceTypes
DECLARE @InvoiceTypeID VARCHAR(20)
SET @InvoiceTypeID = 'INSTALL'
-- Tax Schedule
DECLARE @TaxScheduleId INT
-- Payement Term Tables
DECLARE @PaymentTermId INT
-- Documented Date
DECLARE @DocDate DATETIME
SET @DocDate = GETDATE();
DECLARE @PostedDate DATETIME
SET @PostedDate = GETDATE();
DECLARE @DueDate DATETIME
SET @DueDate = GETDATE();
DECLARE @GLPostDate DATETIME
-- Sales Total Amount
DECLARE @SalesAmount MONEY
SET @SalesAmount = 0;
DECLARE @OriginalTransactionAmount MONEY -- This gets set after the payment goes through
SET @OriginalTransactionAmount = 0;
DECLARE @CurrentTransactionAmount MONEY
SET @CurrentTransactionAmount = 0;
DECLARE @CostAmount MONEY;
SET @CostAmount = 0;
DECLARE @TaxAmount MONEY;
SET @TaxAmount = 0;

--SELECT * FROM dbo.AE_Contracts
DECLARE @ContractID BIGINT
SET @ContractID = 1000000

/** Create an Invoice */
SELECT * FROM dbo.AE_Invoices

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
)

/** Get the IDentity value. */
DECLARE @InvoiceID BIGINT;
SET @InvoiceID = @@IDENTITY;
--SELECT * FROM dbo.AE_Invoices WHERE InvoiceID = @InvoiceID;


ROLLBACK TRANSACTION