USE [WISE_CRM]
GO
/** TDD: custAE_InvoiceItemRefreshMsAccountInstall 
*
*/
DECLARE @InvoiceID	BIGINT
	, @AccountId	BIGINT = 100181
	, @ItemSku	NVARCHAR(50) = 'GEC-6065295'
	, @Qty	INT = 1
	, @SalesmanID	NVARCHAR(25)
	, @TechnicianID	NVARCHAR(25) = 'PVIT001'
	, @GpEmployeeID	NVARCHAR(25) = 'SOSA001'

/** Check for an invoice and create if it does not exists. */
IF (EXISTS(SELECT * FROM AE_Invoices WHERE (AccountId = @AccountID) AND (InvoiceTypeId = 'INSTALL')))
BEGIN
	SELECT @InvoiceID = InvoiceID FROM AE_Invoices WHERE (AccountId = @AccountID) AND (InvoiceTypeId = 'INSTALL');
END
ELSE
BEGIN
	INSERT INTO dbo.AE_Invoices (
		AccountId ,
		InvoiceTypeId ,
		ContractId ,
		TaxScheduleId ,
		PaymentTermId ,
		DocDate ,
		PostedDate ,
		DueDate ,
		GLPostDate ,
		CurrentTransactionAmount ,
		SalesAmount ,
		OriginalTransactionAmount ,
		CostAmount ,
		TaxAmount
	) VALUES  (
		@AccountID -- AccountId - bigint
		, 'INSTALL' -- InvoiceTypeId - varchar(20)
		, NULL -- ContractId - int
		, NULL -- TaxScheduleId - int
		, NULL -- PaymentTermId - int
		, GETDATE() -- DocDate - date
		, NULL -- PostedDate - date
		, DATEADD(m, 1, GETDATE()) -- DueDate - date
		, NULL -- GLPostDate - date
		, NULL -- CurrentTransactionAmount - money
		, 0 -- SalesAmount - money
		, 0 -- OriginalTransactionAmount - money
		, 0 -- CostAmount - money
		, 0 -- TaxAmount - money
	);
	-- Get Identity
	SET @InvoiceID = SCOPE_IDENTITY();
END

/** Set values CASE 1*/
EXEC dbo.custAE_InvoiceItemAddByPartNumber @InvoiceID, @ItemSku, @Qty, @SalesmanID, @TechnicianID, @GpEmployeeID;


SET @ItemSku = 'GEC-60362N103195';
SET @SalesmanID = 'PVIT002';
SET @TechnicianID = NULL;
EXEC dbo.custAE_InvoiceItemAddByPartNumber @InvoiceID, @ItemSku, @Qty, @SalesmanID, @TechnicianID, @GpEmployeeID;


/** Special case from Integration test that is failing. */
--SET @InvoiceID = 10000068;
SET @ItemSku = 'GEC-6087395';
SET @SalesmanID = 'PRIVT001';
SET @TechnicianID = NULL;
SET @GpEmployeeID = 'PRIV001';
EXEC dbo.custAE_InvoiceItemAddByPartNumber @InvoiceID, @ItemSku, @Qty, @SalesmanID, @TechnicianID, @GpEmployeeID;


--SELECT * FROM dbo.AE_InvoiceItems WHERE InvoiceId = 10000087;