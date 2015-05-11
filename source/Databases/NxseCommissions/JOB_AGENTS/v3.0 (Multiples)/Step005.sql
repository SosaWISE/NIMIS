/********************  HEADER  ********************
*	STEP 005: UNKNOWN RIGHT NOW IN MULTIPLES PROGRAM
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
	[dbo].fxSCV2_0GetScriptHeaderInfo() AS PROP;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Commission Engine: ' + @CommissionEngineID + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' (UTC) | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR) + ' (UTC)';
PRINT '************************************************************ START ************************************************************';
/********************  END HEADER ********************/
DECLARE @WorkAccountID BIGINT
	, @AccountID BIGINT
	, @SalesRepID VARCHAR(25)
	, @RecByRepID VARCHAR(25);
DECLARE workAccountCur CURSOR FOR
SELECT WorkAccountID, AccountID, SalesRepId, RecByRepId FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);

OPEN workAccountCur;

FETCH NEXT FROM workAccountCur INTO
	@WorkAccountID, @AccountID, @SalesRepID, @RecByRepID;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	/*************************
	***  RECRUITING BONUS  ***
	*************************/
	PRINT 'Calculating Recruiting Bonus for ' + @SalesRepID;

	PRINT 'SalesRepID: ' + @SalesRepID + ' | Recruited By: ' + @RecByRepID;

	/** Assign recruiting bonus to RecSalesRepID*/
	EXEC [dbo]. SCv2_0_SC_WorkAccountRecruitingBonusReconciliation @WorkAccountId
		, @CommissionPeriodId
		, @AccountID
		, @SalesRepId
		, @RecByRepID;

	/******Get Next */
	FETCH NEXT FROM workAccountCur INTO
		@WorkAccountID, @AccountID, @SalesRepID, @RecByRepID;
END

CLOSE workAccountCur;
DEALLOCATE workAccountCur;

IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
	SELECT * FROM [dbo].[SC_WorkAccountRecruitingBonuses]
END