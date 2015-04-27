/********************  HEADER  ********************
* Team Overrides includes
*	1. Weekly Tea / Office Override
*	2. Monthly Annual Residual Incentive
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

--DECLARE teamMembersCursor CURSOR FOR
DECLARE @TeamSummary TABLE (TeamID INT, NumberOfAccounts INT, CommissionTeamOfficeOverrideScaleID VARCHAR(20), Amount MONEY);

INSERT INTO @TeamSummary (TeamID, NumberOfAccounts)
SELECT DISTINCT
	TML.TeamID
	, COUNT(*) AS NumberOfAccounts
	--, SCWA.WorkAccountID
	--, SCWA.AccountID
FROM
	[dbo].fxSCv2_0GetTeamMembersByCommissionContractID(@CommissionContractID) AS TML
	INNER JOIN [dbo].[SC_WorkAccounts] AS SCWA WITH (NOLOCK)
	ON
		(SCWA.SalesRepId = TML.SalesRepId)
		AND (SCWA.CommissionPeriodId = @CommissionPeriodID)
GROUP BY
	TML.TeamID;

-- Update Values for the override
UPDATE TSS SET
	CommissionTeamOfficeOverrideScaleID = SCCT.CommissionTeamOfficeOverrideScaleID
	, Amount = SCCT.Amount
FROM 
	@TeamSummary AS TSS
	INNER JOIN [dbo].[SC_CommissionTeamOfficeOverrideScales] AS SCCT WITH (NOLOCK)
	ON
		(TSS.NumberOfAccounts BETWEEN SCCT.Start AND SCCT.[End]);

SELECT * FROM @TeamSummary;
SELECT * FROM fxSCv2_0GetTeamMembersByCommissionContractID(@CommissionContractID)