/********************  HEADER  ********************
Bonuses get applied for:
	Increasing the RMR and staying within the range		DONE; TEST PASSED
	Selling equipment to the customer					DONE; Single Upgrade Test passed | Multiple Upgrade for one account needs to be tested
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT
DECLARE @AdjustmentAmount MONEY
DECLARE @RMRChange MONEY

/********************  END HEADER ********************/

/********************************
***	INCREASE RMR WITHIN RANGE ***
********************************/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = 'RAISERMRINRANGE'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		(scwa.RMR - msap.BaseRMR) AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)
PRINT 'RMR CHANGE = ' + CONVERT(VARCHAR(10), @RMRChange)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsAdjustmentId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, 'INT' as WorkAccountPriodID --SHOULDN'T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, 'InvoiceItem' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsAdjustmentId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/************************
***	SELLING EQUIPMENT ***
************************/
/*	RIGHT NOW WE'RE GIVING A FIXED VALUE FOR UPGRADES WE NEED TO BE PULLING THE UPGRADE BONUS COLUMN FROM GP'S ITEM TABLE AND USE THAT AMOUNT	*/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = 'EQUIPUPGRADE'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
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