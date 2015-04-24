/********************  HEADER  ********************
Deductions get applied for:
	Agreement length
	Payment type
	Activation fee
	Points of protection	--STILL NEED TO DO THIS ONE
	Lowered RMR
	Special deals
*/
USE [NXSE_Commissions]
GO

DECLARE @CommissionPeriodID BIGINT
	, @CommissionEngineID VARCHAR(10) = 'SCv2.0'
	, @CommissionPeriodStrDate DATETIME
	, @CommissionPeriodEndDate DATETIME
	, @DEBUG_MODE VARCHAR(20) = 'OFF'
	, @TRUNCATE VARCHAR(20) = 'OFF';


SELECT TOP 1
	@CommissionPeriodID = CommissionPeriodID
	, @CommissionEngineID = CommissionEngineID
	, @CommissionPeriodStrDate = CommissionPeriodStrDate
	, @CommissionPeriodEndDate = CommissionPeriodEndDate
	, @DEBUG_MODE = DEBUG_MODE
	, @TRUNCATE = [TRUNCATE]
FROM
	[dbo].fxSCV2_0GetScriptHeaderInfo() AS PROP;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Commission Engine: ' + @CommissionEngineID + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' (UTC) | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR) + ' (UTC)';
PRINT '************************************************************ START ************************************************************';
/********************  END HEADER ********************/
/** Local Declarations */
DECLARE @CommissionsAdjustmentID VARCHAR(20)
	, @CommissionAdjustmentAmount MONEY;

PRINT '/*************************';
PRINT '***  AGREEMENT LENGTH  ***';
PRINT '*************************/';

-- DEDUCT FOR AGREEMENT LENGTH = 36 or other than 
SET @CommissionsAdjustmentID = 'CONLENLESS60';
SELECT @CommissionAdjustmentAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionsAdjustmentID);

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
	dbo.SC_WorkAccounts
WHERE 
	(ContractLength < 60)
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************';
PRINT '***	PAYMENT TYPE ***';
PRINT '*******************/'

-- DEDUCT FOR PAYMENT TYPE IS CREDIT CARD
SET @CommissionsAdjustmentID = 'PMTCC'
SELECT @CommissionAdjustmentAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionsAdjustmentID);

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
	dbo.SC_WorkAccounts
WHERE 
	(PaymentType = 'CC')
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/*********************';
PRINT '***	ACTIVATION FEE ***';
PRINT '*********************/';
-- DEDUCT FOR ACTIVATION FEE WAIVED
SET @CommissionsAdjustmentID = 'ACTWAIVED'
SELECT @CommissionAdjustmentAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionsAdjustmentID);

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
	dbo.SC_WorkAccounts
WHERE 
	(ActivationFee < 69.00)
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/***************************';
PRINT '***	POINTS OF PROTECTION ***';
PRINT '***************************/';
SET @CommissionsAdjustmentID = 'POINTSGIVEN';
DECLARE @PointDeductionInDollars MONEY = 30.00; -- Default value
SELECT @PointDeductionInDollars = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionsAdjustmentID);

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
	PRINT '/*******************************';
	PRINT '***	CUSTOMER UPGRADE BONUSES ***';
	PRINT '*******************************/';

	-- Check to see if there is a Equipment Upgrade
	IF (EXISTS(SELECT * FROM dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId(@AccountID)))
	BEGIN
		SET @CommissionsAdjustmentID = 'EQUIPUPGRADE';
		INSERT INTO [dbo].[SC_WorkAccountAdjustments] (WorkAccountId, CommissionsAdjustmentID, AdjustmentAmount) 
		SELECT
			@WorkAccountID
			, @commissionsAdjustmentId
			, RBUG.BonusUpgrade --  AS [Bonus Upgrade]
		FROM
			dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId(@AccountID) AS RBUG
		WHERE
			(RBUG.BonusUpgrade IS NOT NULL);
	END
		
	PRINT '/*******************************';
	PRINT '***	LOWERED RMR WITHIN RANGE ***';
	PRINT '*******************************/';
	--DEDUCT Or Adjust based on the Points
	INSERT SC_WorkAccountAdjustments
	(
		WorkAccountId
		, CommissionsAdjustmentID
		, AdjustmentAmount
	)
	SELECT
		@WorkAccountID
		, RMRI.CommissionsAdjustmentID
		, RMRI.[AdjustmentAmount]
	FROM
		[WISE_CRM].[dbo].fxSCv2_0GetRMRInformationByAccountID(@AccountID) AS RMRI
	WHERE
		(RMRI.CommissionsAdjustmentID <> 'NONE');

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

PRINT '/********************************';
PRINT '***	waiving first month''s RMR ***';
PRINT '********************************/';

--DEDUCT FOR WAIVING THE 1ST MONTH RMR
SET @CommissionsAdjustmentID = 'WAIVED1STMONTH';
SELECT @CommissionAdjustmentAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionsAdjustmentID);

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
	dbo.SC_WorkAccounts
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