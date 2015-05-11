/********************  HEADER  ********************
*	STEP007: UNKNOWN RIGHT NOW IN MULTIPLES PROGRAM
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
DECLARE @TeamResidualRates TABLE (TeamID INT, Volume INT, CommissionTeamOfficeAnnualResidualIncentiveId VARCHAR(20), ResidualRate MONEY, PayAmount MONEY, ManSalesRepId VARCHAR(25));
DECLARE @CommissionCycleStrDate DATETIME
	, @CommissionCycleEndDate DATETIME;

/** Check to see if we are in a */
IF (EXISTS(SELECT * FROM [dbo].[SC_CommissionCyclePeriods] WHERE (CommissionPayPeriodId = @CommissionPeriodID)))
BEGIN
	/** TODO: MAGIC NUMBER NEEDS TO BE CHANGED.  */
	PRINT '|- *Get the right Cycle period for the date ranges...';
	SELECT @CommissionCycleStrDate = Start, @CommissionCycleEndDate = [End] FROM [dbo].[SC_CommissionCyclePeriods] WHERE (CommissionPayPeriodId = @CommissionPeriodID);

	INSERT INTO @TeamResidualRates (TeamID, Volume, ManSalesRepId) 
	SELECT
		SCWA.SalesTeamId
		, COUNT(*)
		, [dbo].fxSCv2_0GetManagerBySalesRepIdAndSeasonId(SCWA.SalesRepId, SCWA.SeasonId)
	FROM
		[dbo].[SC_WorkAccountsAll] AS SCWA WITH (NOLOCK)
	WHERE
		-- INSTALLED
		(SCWA.InstallDate BETWEEN @CommissionCycleStrDate AND @CommissionCycleEndDate)
	GROUP BY
		SCWA.SalesTeamId
		, [dbo].fxSCv2_0GetManagerBySalesRepIdAndSeasonId(SCWA.SalesRepId, SCWA.SeasonId);

	UPDATE TRR SET 
		CommissionTeamOfficeAnnualResidualIncentiveId = SCTOAR.CommissionTeamOfficeAnnualResidualIncentiveID
		, ResidualRate = SCTOAR.Amount
		, PayAmount = TRR.Volume * SCTOAR.Amount
	FROM
		@TeamResidualRates AS TRR
		INNER JOIN [dbo].[SC_CommissionTeamOfficeAnnualResidualIncentive] AS SCTOAR WITH (NOLOCK)
		ON
			(SCTOAR.CommissionEngineId = @CommissionEngineId)
			AND (TRR.Volume BETWEEN SCTOAR.Start AND SCTOAR.[End])

	SELECT * FROM @TeamResidualRates;

	/** OK Now we can add this to the WorkAccountAdjustments. */
	INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
		[WorkAccountId]
		, [CommissionTeamOfficeAnnualResidualIncentiveId]
		, [AdjustmentAmount]
		, [CreatedOn]
	)
	SELECT
		SCWAA.WorkAccountID
		, TRR.CommissionTeamOfficeAnnualResidualIncentiveId -- varchar(20)
		, TRR.ResidualRate -- money
		, GETUTCDATE() -- datetime
	FROM
		[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
		INNER JOIN @TeamResidualRates AS TRR
		ON
			(TRR.TeamID = SCWAA.SalesTeamId)
	WHERE
		(SCWAA.InstallDate BETWEEN @CommissionCycleStrDate AND @CommissionCycleEndDate);
END
