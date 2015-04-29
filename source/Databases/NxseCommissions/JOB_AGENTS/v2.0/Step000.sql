/**********************************************************************************************************************
* DESCRIPTION: 
*	This scripts reset the engine from the beginning.
* ***WARNING***	***WARNING***	***WARNING***	***WARNING***	***WARNING***	***WARNING***	***WARNING***	
*  THIS SCRITP SHOULD ONLY BE USED TO START THE ENGINE FROM THE BEGINING.
*	This is used for debugging.
**********************************************************************************************************************/
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

TRUNCATE TABLE [dbo].[SC_WorkAccountSigningBonuses];
TRUNCATE TABLE [dbo].[SC_WorkAccountsNonCommissioned];
TRUNCATE TABLE [dbo].[SC_WorkAccountTeamOfficeOverrideBonuses];
DELETE dbo.SC_WorkAccountAdjustments;
DBCC CHECKIDENT('dbo.SC_WorkAccountAdjustments', RESEED, 0);

DELETE dbo.SC_WorkAccounts;

DELETE dbo.SC_WorkAccountsAll;
DBCC CHECKIDENT ('[dbo].[SC_WorkAccountsAll]', RESEED, 0);
