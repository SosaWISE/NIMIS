/********************  HEADER  ********************
Deductions get applied for:
	Agreement length
	Payment type
	Activation fee
	Points of protection	--STILL NEED TO DO THIS ONE
	Lowered RMR
	Special deals
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT

/********************  END HEADER ********************/

/*************************
***  AGREEMENT LENGTH  ***
*************************/
-- GET CONTRACT LENGTHS
UPDATE SC_workAccounts
SET ContractLength = AE_Contracts.ContractLength
FROM 
	dbo.SC_workAccounts
	JOIN WISE_CRM.dbo.AE_Contracts
	ON
		SC_workAccounts.AccountID = AE_Contracts.AccountId
		AND AE_Contracts.IsDeleted = 'FALSE'

-- DEDUCT FOR AGREEMENT LENGTH = 36
SET @commissionsAdjustmentTypeId = 'AGRMT36'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(ContractLength = 36)

/*******************
***	PAYMENT TYPE ***
*******************/

-- DEDUCT FOR PAYMENT TYPE IS CREDIT CARD
SET @commissionsAdjustmentTypeId = 'PMTCC'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(PaymentType = 'CC')

/*********************
***	ACTIVATION FEE ***
*********************/

-- DEDUCT FOR ACTIVATION FEE WAIVED
SET @commissionsAdjustmentTypeId = 'ACTWAIVED'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(ActivationFee = 0)

/***************************
***	POINTS OF PROTECTION ***
***************************/



/*******************************
***	LOWERED RMR WITHIN RANGE ***
*******************************/

--DEDUCT FOR LOWERING RMR AND STAYING WITHIN THE ALLOWED RANGE
SET @commissionsAdjustmentTypeId = 'LOWRMRINRANGE'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
WHERE
	scwa.RMR < msap.BaseRMR


/*********************************
***	ADJUSTED RMR OUTSIDE RANGE ***
*********************************/

--DEDUCT FOR LOWERING RMR AND GOING OUTSIDE THE ALLOWED RANGE OR GOING ABOVE THE MAX RMR
SET @commissionsAdjustmentTypeId = 'ADJRMROUTRANGE'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
WHERE
	scwa.RMR < msap.MinRMR
	or scwa.RMR > msap.MaxRMR

/********************
***	SPECIAL DEALS ***
********************/

/********************************
***	waiving first month's RMR ***
********************************/

--DEDUCT FOR WAIVING THE 1ST MONTH RMR
SET @commissionsAdjustmentTypeId = 'WAIVED1STMONTH'

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE
	(Waive1stMonth = 1)

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it's all between the rep and the customer.