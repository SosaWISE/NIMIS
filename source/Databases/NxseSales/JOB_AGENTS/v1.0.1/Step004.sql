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
	AND (SCWA.PointsAssignedToRep > 0)
	AND (SCWA.CommissionPeriodId = @CommissionPeriodID);
/********************************************** START CURSOR **********************************************************************************
* Create a cursor looping through work accounts.
***********************************************************************************************************************************************/
DECLARE @AccountID BIGINT
	, @WorkAccountID BIGINT;
DECLARE workAccountCursor CURSOR FOR
SELECT WorkAccountID, AccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
OPEN workAccountCursor;

FETCH NEXT FROM workAccountCursor INTO @WorkAccountID, @AccountID;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	/*******************************
	***	CUSTOMER UPGRADE BONUSES ***
	*******************************/
	SET @CommissionsAdjustmentID = 'EQUIPUPGRADE';
	INSERT INTO [dbo].[SC_WorkAccountAdjustments] (WorkAccountId, CommissionsAdjustmentID, AdjustmentAmount) 
	SELECT
		@WorkAccountID
		, @commissionsAdjustmentId
		, RBUG.BonusUpgrade --  AS [Bonus Upgrade]
	FROM
		dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId(@AccountID) AS RBUG;
	
	/*******************************
	***	LOWERED RMR WITHIN RANGE ***
	*******************************/
	--DEDUCT Or Adjust based on the Points
	INSERT SC_workAccountAdjustments
	(
		WorkAccountId,
		CommissionsAdjustmentID
	)
	SELECT
		@WorkAccountID
		, @commissionsAdjustmentId
		, RMRI.[AdjustmentAmount]
	FROM
		[WISE_CRM].[dbo].fxSCv2_0GetRMRInformationByAccountID(@AccountID) AS RMRI;

	/** Get Next Account */
	FETCH NEXT FROM workAccountCursor INTO @WorkAccountID, @AccountID;
END

CLOSE WorkAccountCursor;
DEALLOCATE WorkAccountCursor;

/**********************************************  END CURSOR  **********************************************************************************
* Create a cursor looping through work accounts.
***********************************************************************************************************************************************/


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