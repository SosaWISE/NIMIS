/********************  HEADER  ********************
Bonuses get applied for:
	Increasing the RMR and staying within the range
	Selling equipment to the customer
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
DECLARE @CommissionsBonusID VARCHAR(20)
	, @BonusAmount MONEY
	, @SeasonId INT
	, @OfficialContractStartDate DATETIME;

/********************************
***	Adjustment for Package    ***
********************************/

-- Figure out the number of accounts for this week
DECLARE @NumThisWeekTbl TABLE (SalesRepID VARCHAR(50), RepHireDate DATETIME, NumThisWeek INT);
INSERT INTO @NumThisWeekTbl (SalesRepID, NumThisWeek) SELECT SalesRepID, COUNT(*) FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID) GROUP BY SalesRepID;

/** FIND Reps Hire Date. */
UPDATE NTW SET 
	RepHireDate = ISNULL(RUR.HireDate, RUR.CreatedOn) 
FROM
	@NumThisWeekTbl AS NTW
	INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
	ON
		(RU.GPEmployeeId = NTW.SalesRepID)
	INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RUR WITH (NOLOCK)
	ON
		(RUR.UserId = RU.UserID)
		AND (RUR.SeasonId = @SeasonId);

/** Get Season ID from the Commissions Engine that is being used. */
SELECT 
	@SeasonId = SCCC.SeasonId
	, @OfficialContractStartDate = SCCC.OfficialStartDate
FROM
	[dbo].[SC_CommissionContracts] AS SCCC WITH (NOLOCK)
	INNER JOIN [dbo].[SC_CommissionEngines] AS SCCE WITH (NOLOCK)
	ON
		(SCCE.CommissionEngineID = SCCC.CommissionEngineId)
		AND (SCCE.CommissionEngineID = 'SCv2.0');

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
	/** LOCALS */
	DECLARE @SigningBonusCount INT = 0
		, @RepHireDate DATETIME;

	/** Get NumThisWeek and also the RepHireDate. */
	SELECT @NumThisWeek = NumThisWeek, @RepHireDate = RepHireDate FROM @NumThisWeekTbl WHERE (SalesRepID = @SalesRepID);

	/********************************
	***	ADD Commission Base Rate  ***
	********************************/
	INSERT SC_workAccountAdjustments (
		WorkAccountId
		, CommissionPeriodId
		, CommissionRateScaleId
		, AdjustmentAmount
	)
	SELECT
	 --VALUES (
		@WorkAccountId
		, @CommissionPeriodId
		, CRS.CommissionRateScaleId
		, CRS.CommissionAmount
	FROM
		dbo.fxSCv2_0GetRateBasedOnScaleByAccountId(@SalesRepId, @SeasonId, @NumThisWeek) AS CRS;

	/********************************
	***	ADD Signing Bonus		  ***
	********************************/
	-- Check to see if the rep qualifies for this bonus.
	IF(@RepHireDate >= @OfficialContractStartDate)
	BEGIN
		-- Calculate number of total sales by rep.
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