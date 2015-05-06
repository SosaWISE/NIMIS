/************************
Objective is it update the WorkAccountsAll table and set the flag based off the qualified rule set
************************/
USE [NXSE_Commissions]
GO

DECLARE	@CommissionContractID INT
	, @CommissionPeriodID BIGINT
	, @CommissionEngineID VARCHAR(10) = 'SCv1.0'
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
	[dbo].fxSCV1_0GetScriptHeaderInfo() AS PROP;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Commission Engine: ' + @CommissionEngineID + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' (UTC) | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR) + ' (UTC)';
PRINT '************************************************************ START ************************************************************';
/********************  END HEADER ********************/
/**************************************
*	Create Table with Summary results *
**************************************/
DECLARE @SumTable TABLE (SalesRepID VARCHAR(25), FullName VARCHAR(100), [MonthName] VARCHAR(50), NumOfAccts INT, RateScaleAmount MONEY, TotalAmount MONEY)

INSERT INTO @SumTable (SalesRepID ,
          FullName,
          MonthName,
          NumOfAccts)
SELECT
	SCWA.SalesRepId
	, RU.FullName
	, DATENAME(mm, SCWA.InstallDate) AS [MonthName]
	, COUNT(*) AS NumOfAccounts
FROM
	[dbo].[SC_WorkAccountsAll] AS SCWA WITH (NOLOCK)
	INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
	ON
		(SCWA.SalesRepId = RU.GPEmployeeId)
GROUP BY
	SCWA.SalesRepId
	, RU.FullName
	, DATENAME(mm, SCWA.InstallDate)
ORDER BY
	SCWA.SalesRepId
	, DATENAME(mm, SCWA.InstallDate)
	, COUNT(*);

UPDATE SMT SET 
	RateScaleAmount = CASE
		WHEN SMT.NumOfAccts BETWEEN 0 AND 8 THEN 0
		WHEN SMT.NumOfAccts BETWEEN 9 AND 12 THEN 10
		WHEN SMT.NumOfAccts BETWEEN 13 AND 16 THEN 245
		WHEN SMT.NumOfAccts BETWEEN 17 AND 21 THEN 345
		WHEN SMT.NumOfAccts BETWEEN 22 AND 25 THEN 415
		WHEN SMT.NumOfAccts BETWEEN 26 AND 29 THEN 500
		WHEN SMT.NumOfAccts BETWEEN 30 AND 34 THEN 535
		WHEN SMT.NumOfAccts BETWEEN 35 AND 39 THEN 565
		WHEN SMT.NumOfAccts BETWEEN 40 AND 99999999 THEN 600
	END,
	TotalAmount = CASE
		WHEN SMT.NumOfAccts BETWEEN 0 AND 8 THEN 0
		WHEN SMT.NumOfAccts BETWEEN 9 AND 12 THEN 10 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 13 AND 16 THEN 245 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 17 AND 21 THEN 345 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 22 AND 25 THEN 415 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 26 AND 29 THEN 500 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 30 AND 34 THEN 535 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 35 AND 39 THEN 565 * SMT.NumOfAccts
		WHEN SMT.NumOfAccts BETWEEN 40 AND 99999999 THEN 600 * SMT.NumOfAccts
	END
FROM
	@SumTable AS SMT;

SELECT * FROM @SumTable;