/** TDD: custAE_InvoiceItemRefreshMsAccountInstall
*/
USE [WISE_CRM]
GO

/** LOCAL DECLARATIONS */
DECLARE @AccountID BIGINT = 100175; 
DECLARE @InvoiceID BIGINT; -- = 10000000;
DECLARE @ActivationFeeItemId VARCHAR(25) = 'SETUP_FEE_199';
DECLARE @MMRItemId VARCHAR(25) = 'MON_CONT_5000';
DECLARE @CellPckgItemId VARCHAR(25) = 'CELL_SRV_AC_WSF';
DECLARE @Over3Months BIT = 0;

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

-- ** CASE 1:  Setup amount is the same as the actual amount
EXEC dbo.custAE_InvoiceItemRefreshMsAccountInstall @InvoiceID, @ActivationFeeItemId, 199, @MMRItemId, 39.95, @CellPckgItemId, @Over3Months, 'SOSA001', 'SALS001', NULL;
SELECT * FROM [dbo].AE_InvoiceItems WHERE InvoiceId = @InvoiceID AND IsDeleted = 0;

-- ** CASE 2:  Setup amount is less than the actual amount.
SET @CellPckgItemId = 'CELL_SRV_AC_BI';
EXEC dbo.custAE_InvoiceItemRefreshMsAccountInstall @InvoiceID, @ActivationFeeItemId, 299, @MMRItemId, 59.95, @CellPckgItemId, @Over3Months, 'SOSA002', 'SALS002', NULL;
SELECT * FROM [dbo].AE_InvoiceItems WHERE InvoiceId = @InvoiceID AND IsDeleted = 0;

-- ** CASE 3:  Setup amount is greater than the actual amount.
SET @CellPckgItemId = 'CELL_SRV_AC_IG';
SET @Over3Months = 1;
EXEC dbo.custAE_InvoiceItemRefreshMsAccountInstall @InvoiceID, @ActivationFeeItemId,   9, @MMRItemId, 19.95, @CellPckgItemId, @Over3Months, 'SOSA003', 'SALS002', NULL;
SELECT * FROM [dbo].AE_InvoiceItems WHERE InvoiceId = @InvoiceID AND IsDeleted = 0;

--				SELECT
----					AII.*
--					SUM(AII.RetailPrice)
--				FROM 
--					[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
--					INNER JOIN [dbo].AE_Items AS AEI WITH (NOLOCK)
--					ON
--						(AEI.ItemID = AII.ItemId)
--				WHERE
--					(AII.InvoiceId = @InvoiceID) 
--					-- AND ((AEI.ItemTypeId = 'SETUP_FEE') OR (AEI.ItemTypeId = 'SETUP_FEE_UPSL') OR (AEI.ItemTypeId = 'SETUP_FEE_DISC'))
--					AND (AEI.ItemTypeId LIKE 'SETUP_FEE%')
--					AND (AII.IsDeleted = 0)
--				GROUP BY
--					AII.InvoiceID
