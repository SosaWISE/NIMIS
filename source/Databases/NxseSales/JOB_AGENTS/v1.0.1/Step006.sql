/********************  HEADER  ********************
Deductions get applied for:
	Agreement length		DONE; TEST PASSED
	Payment type			DONE; TEST PASSED
	Activation fee			DONE; TEST PASSED
	Points of protection		
	Lowered RMR in range	DONE; TEST PASSED
	Lowered RMR out range	DONE; BELOW MINRMR: TEST PASSED | ABOVE MAXRMR: TEST PASSED
	Special deals			DONE; TEST PASSED
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT
DECLARE @AdjustmentAmount MONEY
DECLARE @RMRChange MONEY

/********************  END HEADER ********************/

/*************************
***  AGREEMENT LENGTH  ***
*************************/
SET @commissionsAdjustmentTypeId = 'AGRMT36'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*******************
***	PAYMENT TYPE ***
*******************/
SET @commissionsAdjustmentTypeId = 'PMTCC'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*********************
***	ACTIVATION FEE ***
*********************/
SET @commissionsAdjustmentTypeId = 'ACTWAIVED'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/***************************
***	POINTS OF PROTECTION ***
***************************/



/*******************************
***	LOWERED RMR WITHIN RANGE ***
*******************************/
SET @commissionsAdjustmentTypeId = 'LOWRMRINRANGE'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		CASE WHEN (scwa.RMR >= msap.MinRMR) THEN (msap.BaseRMR - scwa.RMR)
		ELSE (msap.BaseRMR - msap.MinRMR)
		END AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)
--PRINT 'RMR CHANGE = ' + CONVERT(VARCHAR(10), @RMRChange)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we're putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*********************************
***	ADJUSTED RMR OUTSIDE RANGE ***
*********************************/
SET @commissionsAdjustmentTypeId = 'ADJRMROUTRANGE'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		CASE
			WHEN (scwa.RMR > msap.MaxRMR) THEN (scwa.RMR - msap.MaxRMR)
			WHEN (scwa.RMR < msap.MinRMR) THEN (msap.MinRMR - scwa.RMR)
		END AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we're putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId


/********************
***	SPECIAL DEALS ***
********************/

/********************************
***	waiving first month's RMR ***
********************************/
SET @commissionsAdjustmentTypeId = 'WAIVED1STMONTH'

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @AdjustmentAmount = (SELECT (scca.CommissionAdjustmentAmount + scwa.RMR)
							FROM SC_WorkAccounts AS scwa
								JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
								JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
							WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we're putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it's all between the rep and the customer.