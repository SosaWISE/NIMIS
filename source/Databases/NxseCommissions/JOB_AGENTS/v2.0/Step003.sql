/********************  HEADER  ********************
Deductions get applied for:
	Agreement length
	Payment type
	Activation fee
	Points of protection
	Lowered RMR
	Special deals
	Manager Deductions (Team):
		Waived Activation Fees
		Credit Scores Below 625
		RMR lower than Package BaseRMR
*/
USE [NXSE_Commissions]
GO

DECLARE	@CommissionContractID INT
	, @CommissionPeriodID BIGINT
	, @CommissionEngineID VARCHAR(10) = 'SCv2.0'
	, @CommissionPeriodStrDate DATETIME
	, @CommissionPeriodEndDate DATETIME
	, @DEBUG_MODE VARCHAR(20) = 'OFF'
	, @TRUNCATE VARCHAR(20) = 'OFF';
SELECT TOP 1
	@CommissionContractID = CommissionContractID
	, @CommissionPeriodID = CommissionPeriodID
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
DECLARE @CommissionDeductionID VARCHAR(20)
	, @DeductionAmount MONEY;

PRINT '/*************************';
PRINT '***  AGREEMENT LENGTH  ***';
PRINT '*************************/';

-- DEDUCT FOR AGREEMENT LENGTH = 36 or other than 
SET @CommissionDeductionID = 'CONLENLESS60';
SELECT @DeductionAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts
WHERE 
	(ContractLength < 60)
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************';
PRINT '***	PAYMENT TYPE ***';
PRINT '*******************/'

-- DEDUCT FOR PAYMENT TYPE IS CREDIT CARD
SET @CommissionDeductionID = 'PMTCC'
SELECT @DeductionAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);

-- Create entry for payment types that are not ACH
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	,@CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts
WHERE 
	(PaymentType = 'CC')
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/*********************';
PRINT '***	ACTIVATION FEE ***';
PRINT '*********************/';
-- DEDUCT FOR ACTIVATION FEE WAIVED
SET @CommissionDeductionID = 'ACTWAIVED'
SELECT @DeductionAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);

-- Create entry for waived activation fee
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts
WHERE 
	(ActivationFee < 69.00)
	AND (CommissionPeriodId = @CommissionPeriodID);

PRINT '/***************************';
PRINT '***	POINTS OF PROTECTION ***';
PRINT '***************************/';
SET @CommissionDeductionID = 'POINTSGIVEN';
DECLARE @PointDeductionInDollars MONEY = 30.00; -- Default value
SELECT @PointDeductionInDollars = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);

INSERT INTO [dbo].[SC_WorkAccountAdjustments] (WorkAccountId, CommissionPeriodId, CommissionDeductionId, AdjustmentAmount) 
SELECT
	WorkAccountId
	, @CommissionPeriodID
	, @CommissionDeductionID
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
		SET @CommissionDeductionID = 'EQUIPUPGRADE';
		INSERT INTO [dbo].[SC_WorkAccountAdjustments] (WorkAccountId, CommissionDeductionId, AdjustmentAmount) 
		SELECT
			@WorkAccountID
			, @CommissionDeductionID
			, RBUG.BonusUpgrade --  AS [Bonus Upgrade]
		FROM
			dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId(@AccountID) AS RBUG
		WHERE
			(RBUG.BonusUpgrade IS NOT NULL);
	END
		
	PRINT '/*******************************';
	PRINT '***	LOWERED RMR WITHIN RANGE ***';
	PRINT '*******************************/';
	PRINT 'ACCOUNTID: ' + CAST(@AccountID AS VARCHAR);
	--DEDUCT Or Adjust based on the Points
	INSERT SC_WorkAccountAdjustments
	(
		WorkAccountId
		, CommissionPeriodId
		, CommissionDeductionId
		, CommissionBonusId
		, AdjustmentAmount
	)
	SELECT
		@WorkAccountID
		, @CommissionPeriodID
		, RMRI.CommissionDeductionId
		, RMRI.CommissionBonusId
		, RMRI.[AdjustmentAmount]
	FROM
		[WISE_CRM].[dbo].fxSCv2_0GetRMRInformationByAccountID(@AccountID) AS RMRI
	WHERE
		(RMRI.CommissionDeductionID IS NOT NULL OR RMRI.CommissionBonusID IS NOT NULL);

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

PRINT '/**********************************';
PRINT '***	Waiving first month''s RMR ***';
PRINT '***********************************/';

--DEDUCT FOR WAIVING THE 1ST MONTH RMR
SET @CommissionDeductionID = 'WAIVED1STMONTH';
SELECT @DeductionAmount = (-1) * DeductionAmount FROM [dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);

INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT
	WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts
WHERE
	(Waive1stMonth = 1)
	AND (CommissionPeriodId = @CommissionPeriodID);

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it's all between the rep and the customer.


/*
** Team Manager Deductions
*/

PRINT '/******************************';
PRINT '***	Team Activations Waived ***';
PRINT '******************************/';

--DEDUCT FOR TEAM ACTIVATIONS WAIVED
SET @CommissionDeductionID = 'TEAMACTWAIVED'
SELECT @DeductionAmount = (-1) * DeductionAmount FROM dbo.SC_CommissionDeductions WHERE (CommissionDeductionID = @CommissionDeductionID)

-- Create entry for team waived activation fee
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	SCWA.WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts AS SCWA WITH (NOLOCK)
WHERE 
	(SCWA.ActivationFee < 69.00)
	AND (SCWA.CommissionPeriodId = @CommissionPeriodID);



PRINT '/*****************************';
PRINT '***	Team Credits below 625 ***';
PRINT '*****************************/';

--DEDUCT FOR TEAM CREDIT SCORES BELOW 625
SET @CommissionDeductionID = 'TEAMCREDITSUB625'
SELECT @DeductionAmount = (-1) * DeductionAmount FROM dbo.SC_CommissionDeductions WHERE (CommissionDeductionID = @CommissionDeductionID)

-- Create entry for Team Credit below 625
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts
WHERE 
	(CreditScore < 625)
	AND (CommissionPeriodId = @CommissionPeriodID);


PRINT '/***********************';
PRINT '***	Team Lowered RMR ***';
PRINT '***********************/';

--DEDUCT FOR TEAM LOWERED RMR (This is a flat fee, it is not based off how much the rep lowered)
SET @CommissionDeductionID = 'TEAMLOWRMR'
SELECT @DeductionAmount = (-1) * DeductionAmount FROM dbo.SC_CommissionDeductions WHERE (CommissionDeductionID = @CommissionDeductionID)

-- Create entry for Team Lowered RMR
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, CommissionDeductionId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @CommissionDeductionID
	, @DeductionAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].dbo.[MS_AccountSalesInformations] AS msasi ON scwa.AccountID = msasi.AccountID
	JOIN [WISE_CRM].dbo.[MS_AccountPackages] AS msap ON msasi.AccountPackageId = msap.AccountPackageID
WHERE 
	(scwa.RMR < msap.BaseRMR)
	AND (CommissionPeriodId = @CommissionPeriodID);

IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END