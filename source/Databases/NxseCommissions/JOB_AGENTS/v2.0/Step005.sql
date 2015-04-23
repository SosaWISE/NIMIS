/********************  HEADER  ********************
Bonuses get applied for:
	Increasing the RMR and staying within the range
	Selling equipment to the customer
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
DECLARE @CommissionsAdjustmentID VARCHAR(20)
	, @CommissionAdjustmentAmount MONEY;

/********************************
***	Adjustment for Package    ***
********************************/

-- Figure out the number of accounts for this week
DECLARE @NumThisWeekTbl TABLE (SalesRepID VARCHAR(50), NumThisWeek INT);
INSERT INTO @NumThisWeekTbl (SalesRepID, NumThisWeek) SELECT SalesRepID, COUNT(*) FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID) GROUP BY SalesRepID;

/** LOOP THROUGH Each Account and Add the corresponding Rate by the number of sales per pay period. */
DECLARE WorkAccountCursor CURSOR FOR SELECT WorkAccountID, AccountId, SalesRepID FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID);
DECLARE @SalesRepID VARCHAR(50)
	, @WorkAccountId BIGINT
	, @NumThisWeek INT
	, @AccountID BIGINT
	, @WorkAccountAdjustmentId BIGINT;

OPEN WorkAccountCursor;
FETCH NEXT FROM WorkAccountCursor INTO
	@WorkAccountId
	, @AccountID
	, @SalesRepID;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	SET @CommissionsAdjustmentID = 'ACCTRATESCALEPAY';
	SELECT @NumThisWeek = NumThisWeek FROM @NumThisWeekTbl WHERE (SalesRepID = @SalesRepID);

	INSERT SC_workAccountAdjustments (
		WorkAccountId
		, CommissionsAdjustmentID
		, AdjustmentAmount
	) VALUES (
		@WorkAccountId
		, @CommissionsAdjustmentID
		, dbo.fxSCv2_0GetRateBasedOnScaleByAccountId(@AccountID, @NumThisWeek)
	);

	/********************************
	***	Check for Siging Bonus    ***
	********************************/
	DECLARE @SigningBonusCount INT = 0;
	SELECT
		@SigningBonusCount = COUNT(*)
	FROM
		[dbo].[SC_WorkAccounts] AS SWAA WITH (NOLOCK)
		INNER JOIN [dbo].[SC_WorkAccountSigningBonuses] AS SWASB WITH (NOLOCK)
		ON
			(SWASB.WorkAccountID = SWAA.WorkAccountID)
	WHERE
		(SWAA.SalesRepId = @SalesRepID);

	IF (@SigningBonusCount < 3)
	BEGIN
		SET @CommissionsAdjustmentID = 'SIGNINGBONUS';
		SELECT @CommissionAdjustmentAmount = CommissionAdjustmentAmount FROM [dbo].[SC_CommissionAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);

		INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
			[WorkAccountId]
			, [CommissionsAdjustmentId]
			, [AdjustmentAmount]
		) VALUES (
			@WorkAccountId -- WorkAccountId -- bigint
			, @CommissionsAdjustmentID -- varchar(20)
			, @CommissionAdjustmentAmount -- money
		);
		SET @WorkAccountAdjustmentId = SCOPE_IDENTITY();

		INSERT INTO [dbo].[SC_WorkAccountSigningBonuses] (
			[WorkAccountID]
			, [WorkAccountAdjustmentId]
			, [CommissionPeriodId]
			, [AccountID]
		) VALUES (
			@WorkAccountId -- bigint
			, @WorkAccountAdjustmentId
			, @CommissionPeriodId -- int
			, @AccountID -- bigint
        );
	END

	/** Move to the next record. */	
	FETCH NEXT FROM WorkAccountCursor INTO
		@WorkAccountId
		, @AccountID
		, @SalesRepID;
END

CLOSE WorkAccountCursor;
DEALLOCATE WorkAccountCursor;


IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END