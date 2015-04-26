/********************  HEADER  ********************
* Calculate the Recruiting Bonus
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
	SELECT 
		@RecSalesRepID = RU1.GPEmployeeID 
	FROM
		[WISE_HumanResource].[dbo].RU_Users AS RU WITH (NOLOCK)
		INNER JOIN [WISE_HumanResource].[dbo].RU_Users AS RU1 WITH (NOLOCK)
		ON
			(RU1.UserID = RU.RecruitedById)
	WHERE
		(RU.GPEmployeeId = @SalesRepID);
	PRINT 'SalesRepID: ' + @SalesRepID + ' | Recruited By: ' + @RecSalesRepID;
	/** Assign recruiting bonus to RecSalesRepID*/

	/******Get Next */
	FETCH NEXT FROM workAccountCur INTO
		@WorkAccountID, @AccountID, @SalesRepID;
END

CLOSE workAccountCur;
DEALLOCATE workAccountCur;