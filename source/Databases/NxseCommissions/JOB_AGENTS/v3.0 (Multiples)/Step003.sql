/********************  HEADER  ********************
	STEP003: Calculate Adjusted MMR

Possible Reasons to adjust the MMR
Package Level
Package "Addons"
Over the allowed points
Under the allowed points

*/
USE [NXSE_Commissions]
GO

DECLARE	@CommissionContractID INT
	, @CommissionPeriodID BIGINT
	, @CommissionEngineID VARCHAR(10) = 'SCv3.0'
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
	[dbo].fxSCV3_0GetScriptHeaderInfo() AS PROP;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Commission Engine: ' + @CommissionEngineID + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' (UTC) | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR) + ' (UTC)';
PRINT '************************************************************ START ************************************************************';
/********************  END HEADER ********************/
/** Local Declarations */
DECLARE @MMRAdjustmentID VARCHAR(20)
	, @AdjustmentAmount MONEY;

/*
	THE FOLLOWING SECTION IS COMMENTED OUT FOR THE FOLLOWING REASONS.
	1. We are looking at the account equipment to determine the individual MMR Adjustments based off what is actually installed
	2. If there are parts in the package that were not used if we go by the package we might be penalizing the salesrep when we didn't need to
		i.e. HomeSense (-14.50) but if we don't do a thermostat we shouldn't adjust the MMR for something that's not there.
*/

/*
PRINT '/*******************************';
PRINT '***  Set Cell/Interactive Package Adjustments	***';
PRINT '*******************************/';

-- SET DEDUCTION FOR CELLULAR / INTERACTIVE PACKAGE 
SET @MMRAdjustmentID = 'CIPackage';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.AccountPackageId = 1)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************';
PRINT '***  Set Energy Sense Package Adjustments	***';
PRINT '*******************************/';

-- SET DEDUCTION FOR ENERGY SENSE PACKAGE 
SET @MMRAdjustmentID = 'ESPackage';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.AccountPackageId = 2)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************';
PRINT '***  Set Security Sense Package Adjustments	***';
PRINT '*******************************/';

-- SET DEDUCTION FOR SECURITY SENSE PACKAGE 
SET @MMRAdjustmentID = 'SSPackage';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.AccountPackageId = 3)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************';
PRINT '***  Set Home Sense Package Adjustments	***';
PRINT '*******************************/';

-- SET DEDUCTION FOR HOME SENSE PACKAGE 
SET @MMRAdjustmentID = 'HSPackage';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with contract length less than 60
INSERT SC_workAccountAdjustments
(
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.AccountPackageId = 4)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);
*/

PRINT '/***********************************';
PRINT '***  Set Cell Unit Adjustments	***';
PRINT '************************************/';

-- SET DEDUCTION FOR CELL UNIT ADJUSTMENTS
SET @MMRAdjustmentID = 'Cellular';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with a cellular unit
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].[dbo].[MS_AccountEquipment] AS msae ON scwa.AccountID = msae.AccountId
		AND IsActive = 1
		AND IsDeleted = 0
	JOIN [WISE_CRM].[dbo].[MS_Equipments] AS mse ON msae.EquipmentId = mse.EquipmentID
WHERE 
	(mse.EquipmentTypeId = 11)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************';
PRINT '***  Set Camera Adjustments	***';
PRINT '********************************/';

-- SET DEDUCTION FOR CAMERA ADJUSTMENTS
SET @MMRAdjustmentID = 'Camera';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with at least one Camera or Image Sensor
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].[dbo].[MS_AccountEquipment] AS msae ON scwa.AccountID = msae.AccountId
		AND IsActive = 1
		AND IsDeleted = 0
	JOIN [WISE_CRM].[dbo].[MS_Equipments] AS mse ON msae.EquipmentId = mse.EquipmentID
WHERE 
	(mse.EquipmentTypeId = 15)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/***************************************';
PRINT '***  Set Light Control Adjustments	***';
PRINT '****************************************/';

-- SET DEDUCTION FOR LIGHT CONTROL ADJUSTMENTS
SET @MMRAdjustmentID = 'LightControl';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with at least one lighting device
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].[dbo].[MS_AccountEquipment] AS msae ON scwa.AccountID = msae.AccountId
		AND IsActive = 1
		AND IsDeleted = 0
	JOIN [WISE_CRM].[dbo].[MS_Equipments] AS mse ON msae.EquipmentId = mse.EquipmentID
WHERE 
	(mse.EquipmentTypeId = 34)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/***********************************';
PRINT '***  Set LockControl Adjustments	***';
PRINT '************************************/';

-- SET DEDUCTION FOR LOCK CONTROL ADJUSTMENTS
SET @MMRAdjustmentID = 'LockControl';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with at least one lock
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].[dbo].[MS_AccountEquipment] AS msae ON scwa.AccountID = msae.AccountId
		AND IsActive = 1
		AND IsDeleted = 0
	JOIN [WISE_CRM].[dbo].[MS_Equipments] AS mse ON msae.EquipmentId = mse.EquipmentID
WHERE 
	(mse.EquipmentTypeId = 16)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************************';
PRINT '***  Set ThermostatControl Adjustments	***';
PRINT '********************************************/';

-- SET DEDUCTION FOR THERMOSTAT CONTROL ADJUSTMENTS
SET @MMRAdjustmentID = 'ThermostatControl';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);

-- Create entry for all accounts with at least one thermostat
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	, @CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
	JOIN [WISE_CRM].[dbo].[MS_AccountEquipment] AS msae ON scwa.AccountID = msae.AccountId
		AND IsActive = 1
		AND IsDeleted = 0
	JOIN [WISE_CRM].[dbo].[MS_Equipments] AS mse ON msae.EquipmentId = mse.EquipmentID
WHERE 
	(mse.EquipmentTypeId = 33)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/*******************************************';
PRINT '***	Set Over Allowed Points Adjustments ***';
PRINT '********************************************/'

--  SET DEDUCTION FOR GIVING MORE POINTS THAN ALLOWED
SET @MMRAdjustmentID = 'OverAllowedPoints';
SELECT @AdjustmentAmount = (-1) * AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);
SET @AdjustmentAmount = @AdjustmentAmount * (SELECT FLOOR(PointsAssignedToRep - PointsAllowed) FROM [dbo].[SC_WorkAccounts] WHERE (PointsAssignedToRep > PointsAllowed))

-- Create entry for payment types that are not ACH
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	,@CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.PointsAssignedToRep > scwa.PointsAllowed)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '/********************************************';
PRINT '***	Set Under Allowed Points Adjustments ***';
PRINT '*********************************************/'

--  SET DEDUCTION FOR GIVING MORE POINTS THAN ALLOWED
SET @MMRAdjustmentID = 'UnderAllowedPoints';
SELECT @AdjustmentAmount = AdjustmentAmount FROM [dbo].[SC_MMRAdjustments] WHERE (MMRAdjustmentID = @MMRAdjustmentID);
SET @AdjustmentAmount = @AdjustmentAmount * (SELECT FLOOR(PointsAllowed - PointsAssignedToRep) FROM [dbo].[SC_WorkAccounts] WHERE (PointsAllowed > PointsAssignedToRep))

-- Create entry for payment types that are not ACH
INSERT INTO dbo.SC_ICEffectiveMMRDetails (
	WorkAccountId
	, CommissionPeriodId
	, MMRAdjustmentId
	, AdjustmentAmount
)
SELECT 
	WorkAccountID
	,@CommissionPeriodID
	, @MMRAdjustmentID
	, @AdjustmentAmount
FROM
	dbo.SC_WorkAccounts AS scwa
WHERE 
	(scwa.PointsAllowed > scwa.PointsAssignedToRep)
	AND (scwa.CommissionPeriodId = @CommissionPeriodID);

PRINT '*********************************************';
PRINT '***	Summarize the details				 ***';
PRINT '*********************************************'
DECLARE @WorkAccountId BIGINT
	, @WorkAccountAdjustmentID BIGINT
	, @SumAdjustment MONEY;
DECLARE summaryCur CURSOR FOR
SELECT
	SCIC.WorkAccountId
	, SUM(SCIC.AdjustmentAmount)
FROM
	dbo.SC_ICEffectiveMMRDetails AS SCIC WITH (NOLOCK)
WHERE
	(CommissionPeriodId = @CommissionPeriodID)
GROUP BY
	SCIC.WorkAccountId;

OPEN summaryCur;
FETCH NEXT FROM summaryCur INTO
	@WorkAccountId
	, @SumAdjustment;
WHILE (@@FETCH_STATUS = 0)
BEGIN
	INSERT INTO dbo.SC_ICEffectiveMMR (
		WorkAccountId
		, CommissionPeriodId
		, TotalDeductions
	) VALUES (
		@WorkAccountId -- bigint
		, @CommissionPeriodId -- money
		, @SumAdjustment -- money
	);

	FETCH NEXT FROM summaryCur INTO
		@WorkAccountId
		, @SumAdjustment;
END

CLOSE summaryCur;
DEALLOCATE summaryCur;

IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_ICEffectiveMMRDetails] WHERE (CommissionPeriodId = @CommissionPeriodID);
END