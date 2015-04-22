/********************  HEADER  ********************
* Calculate the Recruiting Bonus
*/
USE NXSE_Sales
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
DECLARE @WorkAccountID BIGINT
	, @AccountID BIGINT
	, @SalesRepID VARCHAR(25);
DECLARE workAccountCur CURSOR FOR
SELECT WorkAccountID, AccountID, SalesRepId FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
SELECT WorkAccountID, AccountID, SalesRepId FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);

OPEN workAccountCur;

FETCH NEXT FROM workAccountCur INTO
	@WorkAccountID, @AccountID, @SalesRepID;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	/*************************
	***  RECRUITING BONUS  ***
	*************************/
	PRINT 'Calculating Recruiting Bonus for ' + @SalesRepID;

	/** Check to see if the Commission has been paid. */
	DECLARE @RecSalesRepID VARCHAR(25) = NULL;
	SELECT TOP 1 @RecSalesRepID = GPEmployeeID FROM [WISE_HumanResource].[dbo].fxRU_UsersGetRecruitsBySalesRepID(@SalesRepID);
	PRINT 'SalesRepID: ' + @SalesRepID;
	SELECT
		SCWAA.WorkAccountAdjustmentID
		, SWCA.WorkAccountID
		, SWCA.AccountID
		, SWCA.SalesRepId
		, ROW_NUMBER() OVER (PARTITION BY SWCA.WorkAccountID, SWCA.AccountID, SWCA.SalesRepId ORDER BY SWCA.WorkAccountID) AS [RowCount]
	FROM
		[dbo].[SC_WorkAccountAdjustments] AS SCWAA WITH (NOLOCK)
		INNER JOIN [dbo].[SC_WorkAccounts] AS SWCA WITH (NOLOCK)
		ON
			(SWCA.WorkAccountID = SCWAA.WorkAccountId)
			AND (SCWAA.CommissionsAdjustmentId = 'ACCTRATESCALEPAY')
		INNER JOIN [WISE_HumanResource].[dbo].fxRU_UsersGetRecruitsBySalesRepID(@SalesRepID) AS RRR
		ON
			(SWCA.SalesRepId = RRR.GPEmployeeId)
	ORDER BY
		SCWAA.WorkAccountAdjustmentID;

	/******Get Next */
	FETCH NEXT FROM workAccountCur INTO
		@WorkAccountID, @AccountID, @SalesRepID;
END

CLOSE workAccountCur;
DEALLOCATE workAccountCur;