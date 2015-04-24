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
DECLARE @CommissionsBonusID VARCHAR(20)
	, @BonusAmount MONEY;

/********************************
***	Adjustment for Package    ***
********************************/

-- Figure out the number of accounts for this week
DECLARE @NumThisWeekTbl TABLE (SalesRepID VARCHAR(50), NumThisWeek INT);
INSERT INTO @NumThisWeekTbl (SalesRepID, NumThisWeek) SELECT SalesRepID, COUNT(*) FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID) GROUP BY SalesRepID;

/** LOOP THROUGH Each Account and Add the corresponding Rate by the number of sales per pay period. */
DECLARE WorkAccountCursor CURSOR FOR SELECT WorkAccountID, AccountId, SalesRepID, SeasonId FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID);
DECLARE @SalesRepID VARCHAR(50)
	, @WorkAccountId BIGINT
	, @NumThisWeek INT
	, @AccountID BIGINT
	, @SeasonId INT
	, @WorkAccountAdjustmentId BIGINT;

OPEN WorkAccountCursor;
FETCH NEXT FROM WorkAccountCursor INTO
	@WorkAccountId
	, @AccountID
	, @SalesRepID
	, @SeasonId;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	SELECT @NumThisWeek = NumThisWeek FROM @NumThisWeekTbl WHERE (SalesRepID = @SalesRepID);

	INSERT SC_workAccountAdjustments (
		WorkAccountId
		, CommissionRateScaleId
		, AdjustmentAmount
	)
	SELECT
	 --VALUES (
		@WorkAccountId
		, CRS.CommissionRateScaleId
		, CRS.CommissionAmount
	FROM
		dbo.fxSCv2_0GetRateBasedOnScaleByAccountId(@SalesRepId, @SeasonId, @NumThisWeek) AS CRS;
	

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
		SET @CommissionsBonusID = 'SIGNINGBONUS';
		SELECT @BonusAmount = BonusAmount FROM [dbo].SC_CommissionBonus WHERE (CommissionBonusID = @CommissionsBonusID);

		INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
			[WorkAccountId]
			, [CommissionBonusId]
			, [AdjustmentAmount]
		) VALUES (
			@WorkAccountId -- WorkAccountId -- bigint
			, @CommissionsBonusID -- varchar(20)
			, @BonusAmount -- money
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
		, @SalesRepID
		, @SeasonId;
END

CLOSE WorkAccountCursor;
DEALLOCATE WorkAccountCursor;


IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END