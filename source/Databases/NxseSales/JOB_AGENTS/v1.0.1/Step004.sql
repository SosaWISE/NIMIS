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

DECLARE @CommissionPeriodID BIGINT
	, @CommissionPeriodStrDate DATETIME
	, @CommissionPeriodEndDate DATETIME
	, @DEBUG_MODE VARCHAR(20) = 'OFF';

SELECT @DEBUG_MODE = GlobalPropertyValue FROM [dbo].[SC_GlobalProperties] WHERE (GlobalPropertyID = 'DEBUG_MODE');

SELECT TOP 1
	@CommissionPeriodID = CommissionPeriodID
	, @CommissionPeriodEndDate = CommissionPeriodEndDate
	, @CommissionPeriodStrDate = DATEADD(d, -7, CommissionPeriodEndDate)
FROM
	NXSE_Sales.dbo.SC_CommissionPeriods 
ORDER BY
	CommissionPeriodID DESC;

DECLARE @CommissionsAdjustmentID VARCHAR(20)
	, @CommissionAdjustmentAmount MONEY;

/********************  END HEADER ********************/

/*************************
***  AGREEMENT LENGTH  ***
*************************/
-- DEDUCT FOR AGREEMENT LENGTH = 36 or other than 
SET @CommissionsAdjustmentID = 'CONLENLESS60';
SELECT @CommissionAdjustmentAmount = (-1) * CommissionAdjustmentAmount FROM [dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionsAdjustmentID
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionsAdjustmentID
	, @CommissionAdjustmentAmount
FROM
	dbo.SC_workAccounts
WHERE 
	(ContractLength < 60)
	AND (CommissionPeriodId = @CommissionPeriodID);

/*******************
***	PAYMENT TYPE ***
*******************/

-- DEDUCT FOR PAYMENT TYPE IS CREDIT CARD
SET @CommissionsAdjustmentID = 'PMTCC'
SELECT @CommissionAdjustmentAmount = (-1) * CommissionAdjustmentAmount FROM [dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

-- Create entry for payment types that are not ACH
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionsAdjustmentID
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionsAdjustmentID
	, @CommissionAdjustmentAmount
FROM
	dbo.SC_workAccounts
WHERE 
	(PaymentType = 'CC')
	AND (CommissionPeriodId = @CommissionPeriodID);

/*********************
***	ACTIVATION FEE ***
*********************/

-- DEDUCT FOR ACTIVATION FEE WAIVED
SET @CommissionsAdjustmentID = 'ACTWAIVED'
SELECT @CommissionAdjustmentAmount = (-1) * CommissionAdjustmentAmount FROM [dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

-- Create entry for waived activation fee
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionsAdjustmentID
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @commissionsAdjustmentId
	, @CommissionAdjustmentAmount
FROM
	dbo.SC_workAccounts
WHERE 
	(ActivationFee < 69.00)
	AND (CommissionPeriodId = @CommissionPeriodID);

/***************************
***	POINTS OF PROTECTION ***
***************************/
SET @CommissionsAdjustmentID = 'POINTSGIVEN';
DECLARE @PointDeductionInDollars MONEY = 30.00; -- Default value
SELECT @PointDeductionInDollars = (-1) * CommissionAdjustmentAmount FROM [dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

INSERT INTO [dbo].[SC_WorkAccountAdjustments] (WorkAccountId, CommissionsAdjustmentID, AdjustmentAmount) 
SELECT
	WorkAccountId
	, @commissionsAdjustmentId
	, CAST(SCWA.PointsAssignedToRep AS MONEY) * @PointDeductionInDollars
FROM
	[dbo].[SC_WorkAccounts] AS SCWA WITH (NOLOCK)
WHERE
	(SCWA.PointsAssignedToRep IS NOT NULL)
	AND (SCWA.PointsAssignedToRep > 0);

/*******************************
***	CUSTOMER UPGRADE BONUSES ***
*******************************/
	
/*******************************
***	LOWERED RMR WITHIN RANGE ***
*******************************/

--DEDUCT FOR LOWERING RMR AND STAYING WITHIN THE ALLOWED RANGE
SET @CommissionsAdjustmentID = 'RMRLOWINRANGE';
DECLARE @LowRMRPerDollarAmount MONEY = 30.00; -- Default value]
SELECT @LowRMRPerDollarAmount = (-1) * CommissionAdjustmentAmount FROM [dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM
	dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap 
	ON
		(scwa.AccountPackageId = msap.AccountPackageID)
WHERE
	(scwa.RMR < msap.BaseRMR)
	AND (CommissionPeriodId = @CommissionPeriodID);


/*********************************
***	ADJUSTED RMR OUTSIDE RANGE ***
*********************************/

--DEDUCT FOR LOWERING RMR AND GOING OUTSIDE THE ALLOWED RANGE OR GOING ABOVE THE MAX RMR
SET @CommissionsAdjustmentID = 'ADJRMROUTRANGE';

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	(CommissionsAdjustmentTypeId = @CommissionsAdjustmentID);

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM
	dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap
	ON
		(scwa.AccountPackageId = msap.AccountPackageID)
WHERE
	((scwa.RMR < msap.MinRMR) OR (scwa.RMR > msap.MaxRMR))
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

/********************
***	SPECIAL DEALS ***
********************/

/********************************
***	waiving first month's RMR ***
********************************/

--DEDUCT FOR WAIVING THE 1ST MONTH RMR
SET @CommissionsAdjustmentID = 'WAIVED1STMONTH';

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	(CommissionsAdjustmentTypeId = @CommissionsAdjustmentID);

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM
	dbo.SC_workAccounts
WHERE
	(Waive1stMonth = 1)
	AND (CommissionPeriodId = @CommissionPeriodID);

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it's all between the rep and the customer.

IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END