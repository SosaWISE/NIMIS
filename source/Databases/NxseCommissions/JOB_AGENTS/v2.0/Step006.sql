/********************  HEADER  ********************
* Team Overrides includes
*	1. Weekly Team / Office Override
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

DECLARE @TeamSummary TABLE (TeamID INT, NumberOfAccounts INT, CommissionTeamOfficeOverrideScaleID VARCHAR(20), Amount MONEY);
DECLARE @ManSalesRepId VARCHAR(25)
	, @WorkAccountID BIGINT
	, @AccountId BIGINT
	, @WorkAccountAdjustmentId BIGINT
	, @CommissionTeamOfficeOverrideScaleID VARCHAR(20)
	, @Amount MONEY
	, @SalesRepID VARCHAR(25);

INSERT INTO @TeamSummary (TeamID, NumberOfAccounts)
SELECT DISTINCT
	TML.TeamID
	, COUNT(*) AS NumberOfAccounts
	--, SCWA.WorkAccountID
	--, SCWA.AccountID
FROM
	(SELECT DISTINCT
		TMLI.TeamID
		, TMLI.SalesRepId
	FROM
		[dbo].fxSCv2_0GetTeamMembersByCommissionContractID(@CommissionContractID) AS TMLI) AS TML
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

-- Add to WorkAcountTeamOfficeOverrideBonuses
DECLARE teamSumCursor CURSOR FOR
	SELECT TS.TeamID, TS.CommissionTeamOfficeOverrideScaleID, TS.Amount, TML.SalesRepID
	FROM 
		@TeamSummary AS TS
		INNER JOIN [dbo].fxSCv2_0GetTeamMembersByCommissionContractID(@CommissionContractID) AS TML
		ON
			(TS.TeamID = TML.TeamID)

OPEN teamSumCursor;
FETCH NEXT FROM teamSumCursor INTO
	@ManSalesRepId
	, @CommissionTeamOfficeOverrideScaleID
	, @Amount
	, @SalesRepID;

WHILE (@@FETCH_STATUS = 0)
BEGIN
	-- Get Manager ID
	SELECT TOP 1
		@ManSalesRepID = ManSalesRepID
	FROM
		[dbo].fxSCv2_0GetTeamMembersByCommissionContractID(@CommissionContractID)
	WHERE
		(TeamID = @ManSalesRepId);

	/** Now have to loop because a rep can have more than one account in the WorkAccounts table. */
	DECLARE workAccountBySalesRepCurosr CURSOR FOR
		SELECT SCWA.WorkAccountID, SCWA.AccountID FROM [dbo].[SC_WorkAccounts] AS SCWA WITH (NOLOCK) WHERE (SalesRepId = @SalesRepID) AND (CommissionPeriodId = @CommissionPeriodID);
	OPEN workAccountBySalesRepCurosr;
	FETCH NEXT FROM workAccountBySalesRepCurosr INTO @WorkAccountID, @AccountId;

	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		/** ADD THE APPROPIRATE ROWS */

		-- Create the WorkAccountAdjustment
		INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
			[WorkAccountId]
			, [CommissionPeriodId]
			, [CommissionTeamOfficeOverrideScaleId]
			, [AdjustmentAmount]
		) VALUES (
			@WorkAccountId -- bigint
			, @CommissionPeriodID
			, @CommissionTeamOfficeOverrideScaleId -- varchar(20)
			, @Amount -- money
		);

		SET @WorkAccountAdjustmentId = SCOPE_IDENTITY();

		-- Create the WorkAccountTeamOfficeOverrideBonuses
		INSERT INTO [dbo].[SC_WorkAccountTeamOfficeOverrideBonuses] (
			[WorkAccountID]
			, [WorkAccountAdjustmentId]
			, [CommissionPeriodId]
			, [CommissionTeamOfficeOverrideScaleId]
			, [AccountId]
			, [PaidToSalesManRepId]
		) VALUES (
			@WorkAccountID
			, @WorkAccountAdjustmentId
			, @CommissionPeriodID
			, @CommissionTeamOfficeOverrideScaleID
			, @AccountId
			, @ManSalesRepID
		);
		
		-- Get next row
		FETCH NEXT FROM workAccountBySalesRepCurosr INTO @WorkAccountID, @AccountId;
	END

	CLOSE workAccountBySalesRepCurosr;
	DEALLOCATE workAccountBySalesRepCurosr;

	-- Get next item	
	FETCH NEXT FROM teamSumCursor INTO
		@ManSalesRepId
		, @CommissionTeamOfficeOverrideScaleID
		, @Amount
		, @SalesRepID;
END
CLOSE teamSumCursor;
DEALLOCATE teamSumCursor;
	
IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_workAccountAdjustments] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
	SELECT * FROM [dbo].[SC_WorkAccountTeamOfficeOverrideBonuses] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END