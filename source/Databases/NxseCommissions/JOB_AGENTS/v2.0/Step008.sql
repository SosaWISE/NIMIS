/*
* DESCRIPTION:
*	This script will move the dial to the next pay period
*/
USE [NXSE_Commissions]
GO

DECLARE	@CommissionContractID INT
	, @CommissionPeriodID INT
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
DECLARE @OldCommissionPeriodID INT = @CommissionPeriodID;

/** Initialize */
IF (@OldCommissionPeriodID IS NOT NULL)
BEGIN
	/** Move to the next commission period */
	UPDATE [dbo].[SC_CommissionPeriods] SET IsCurrent = NULL;
	SELECT TOP 1 @CommissionPeriodID = CommissionPeriodID FROM [dbo].[SC_CommissionPeriods] WHERE (CommissionPeriodID > @OldCommissionPeriodID) AND (CommissionEngineId = @CommissionEngineID) AND (IsActive = 1 AND IsDeleted = 0);
	UPDATE [dbo].[SC_CommissionPeriods] SET IsCurrent = 'TRUE' WHERE (CommissionPeriodID = @CommissionPeriodID);
END

/** Check to see if we need to truncate the tables */
UPDATE [dbo].[SC_GlobalProperties] SET GlobalPropertyValue = 'OFF' WHERE (GlobalPropertyID = 'TRUNCATE') AND (GlobalPropertyValue = 'ON');

/** Get Next Period Information */
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