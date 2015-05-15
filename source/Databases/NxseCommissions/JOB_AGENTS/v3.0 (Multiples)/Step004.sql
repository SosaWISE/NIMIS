/********************  HEADER  ********************
	STEP 004: CALCULATE THE MULTIPLE
	Multiples have a base which every account will have
	We'll then put in ledger items for each of the different adjustment types
		step 5+ needs to be a select of sorts to get the sum of the adjusted MMR and the multiples to then spit out the actual commission amount.

	TODO:	Weekly volume 40 & 60
			ind contractor
			dealer

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
	[dbo].fxSCV3_0GetScriptHeaderInfo() AS PROP;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Commission Engine: ' + @CommissionEngineID + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' (UTC) | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR) + ' (UTC)';
PRINT '************************************************************ START ************************************************************';
/********************  END HEADER ********************/
/** Local Declarations */
DECLARE @MultipleAdjustmentID VARCHAR(20)
	, @MultipleAdjustment INT;

BEGIN TRY
	BEGIN TRANSACTION;

	PRINT '/**********************************************';
	PRINT '***  Set Multiple for Good/Excellent Credit ***';
	PRINT '***********************************************/';

	-- Set Multiple for Good/Excellent Credit
	SET @MultipleAdjustmentID = 'GoodExcMultiple';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for all accounts with contract length less than 60
	INSERT INTO dbo.SC_ICMultipleDetails (
	WorkAccountId
	, CommissionPeriodId
	, MultipleAdjustmentId
	, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		, @CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		((scwa.CreditScore >= 625)
		OR (scwa.CreditCustomerType = 'GOOD')
		OR (scwa.CreditCustomerType = 'EXCELLENT'))
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************************';
	PRINT '***  Set Multiple for Sub Credit ***';
	PRINT '*******************************/';

	-- Set Multiple for Sub Credit 
	SET @MultipleAdjustmentID = 'SubCreditMultiple';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for all accounts with contract length less than 60
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		, @CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.CreditScore BETWEEN 600 and 624)
		OR (scwa.CreditCustomerType = 'SUB')
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************************';
	PRINT '***  Set Multiple for Commercial Accounts ***';
	PRINT '*******************************/';

	-- Set Multiple for Commercial Accounts
	SET @MultipleAdjustmentID = 'CommercialMultiple';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for all accounts with contract length less than 60
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		, @CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.FriendsAndFamilyTypeId = 'COMM')
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************************';
	PRINT '***  Set Multiple for ACH Payments ***';
	PRINT '*******************************/';

	-- Set Multiple for ACH Payments
	SET @MultipleAdjustmentID = 'PMTACH';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for all accounts with contract length less than 60
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		, @CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.PaymentType = 'ACH')
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************************';
	PRINT '***  Set Multiple for CC Payments ***';
	PRINT '*******************************/';

	-- Set Multiple for CC Payments
	SET @MultipleAdjustmentID = 'PMTCC';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for all accounts with contract length less than 60
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		, @CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.PaymentType = 'CC')
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************';
	PRINT '***	Set Multiple for Contract Length: 60 Months ***';
	PRINT '*******************/'

	--  Set Multiple for Contract Lenght: 60 Months
	SET @MultipleAdjustmentID = 'CONTLEN60';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.ContractLength = 60)
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************';
	PRINT '***	Set Multiple Penalty for Waived Activation Fee ***';
	PRINT '*******************/'

	--  Set Multiple Penalty for Waived Activation Fee
	SET @MultipleAdjustmentID = 'WaivedActFee';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.ActivationFee = 0)
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************';
	PRINT '***	Set Multiple for Weekly Volume of 40-59 ***';
	PRINT '*******************/'

	--  Set Multiple for Weekly Volume of 40-59
	SET @MultipleAdjustmentID = 'Volume40';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.PointsAllowed > scwa.PointsAssignedToRep)
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*******************';
	PRINT '***	Set Multiple for Weekly Volume of 60+ ***';
	PRINT '*******************/'

	--  Set Multiple for Weekly Volume of 60+
	SET @MultipleAdjustmentID = 'Volume60';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
	WHERE 
		(scwa.PointsAllowed > scwa.PointsAssignedToRep)
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/***********************************************';
	PRINT '***	Set Multiple for Independent Contractor ***';
	PRINT '************************************************/'

	/** Create a table that contains seasons and managers */
	DECLARE @SeasonId INT;
	DECLARE @SeasonManagers TABLE(SeasonId INT, ManRepSalesID VARCHAR(25));
	DECLARE seasonCur CURSOR FOR
	SELECT DISTINCT SeasonId FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodId);

	OPEN seasonCur;
	FETCH NEXT FROM seasonCur INTO @SeasonId;
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO @SeasonManagers (SeasonId, ManRepSalesID)
		SELECT @SeasonId AS SeasonId, ManSalesRepId FROM dbo.fxSCv3_0GetManagersBySeasonId(@SeasonId);
		
		-- Move next row
		FETCH NEXT FROM seasonCur INTO @SeasonId;
	END

	CLOSE seasonCur;
	DEALLOCATE seasonCur;
	--  Set Multiple for Independent Contractor
	SET @MultipleAdjustmentID = 'IndCont';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
		WorkAccountId
		, CommissionPeriodId
		, MultipleAdjustmentId
		, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
		INNER JOIN @SeasonManagers AS SMAN
		ON
			(SMAN.SeasonId = scwa.SeasonId)
			AND (scwa.SalesRepId = SMAN.ManRepSalesID)
	WHERE 
		(scwa.PointsAllowed > scwa.PointsAssignedToRep) --TODO:  Filter reps.  This is for Managers only
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/********************************';
	PRINT '***	Set Multiple for Dealers ***';
	PRINT '*********************************/'

	--  Set Multiple for Dealers
	SET @MultipleAdjustmentID = 'Dealer';
	SELECT @MultipleAdjustment = MultipleAdjustment FROM [dbo].[SC_MultipleAdjustments] WHERE (MultipleAdjustmentID = @MultipleAdjustmentID);

	-- Create entry for payment types that are not ACH
	INSERT INTO dbo.SC_ICMultipleDetails (
	WorkAccountId
	, CommissionPeriodId
	, MultipleAdjustmentId
	, MultipleAdjustment
	)
	SELECT 
		WorkAccountID
		,@CommissionPeriodID
		, @MultipleAdjustmentID
		, @MultipleAdjustment
	FROM
		dbo.SC_WorkAccounts AS scwa
		INNER JOIN @SeasonManagers AS SMAN
		ON
			(SMAN.SeasonId = scwa.SeasonId)
			AND (scwa.SalesRepId = SMAN.ManRepSalesID)
	WHERE 
		(scwa.PointsAllowed > scwa.PointsAssignedToRep) --TODO:  Filter reps.  This is for Managers only
		AND (scwa.CommissionPeriodId = @CommissionPeriodID);

	COMMIT TRANSACTION;
END TRY

BEGIN CATCH
	PRINT 'EXCEPTION THROWN';
	ROLLBACK TRANSACTION;
END CATCH

IF (@DEBUG_MODE = 'ON')
BEGIN

	SELECT * FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
	SELECT * FROM [dbo].[SC_ICMultipleDetails] WHERE (WorkAccountId IN (SELECT WorkAccountID FROM [dbo].[SC_WorkAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID)));
END