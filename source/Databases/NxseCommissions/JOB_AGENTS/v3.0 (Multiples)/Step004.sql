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

	PRINT '/*************************************';
	PRINT '***  Set Multiple for ACH Payments ***';
	PRINT '**************************************/';

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

	PRINT '/************************************';
	PRINT '***  Set Multiple for CC Payments ***';
	PRINT '*************************************/';

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

	PRINT '/***************************************************';
	PRINT '***	Set Multiple for Contract Length: 60 Months ***';
	PRINT '****************************************************/'

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

	PRINT '/******************************************************';
	PRINT '***	Set Multiple Penalty for Waived Activation Fee ***';
	PRINT '*******************************************************/'

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

	PRINT '/***********************************************';
	PRINT '***	Set Multiple for Weekly Volume of 40-59 ***';
	PRINT '************************************************/'

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

	PRINT '/*********************************************';
	PRINT '***	Set Multiple for Weekly Volume of 60+ ***';
	PRINT '**********************************************/'

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
		(scwa.CommissionPeriodId = @CommissionPeriodID);

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
		(scwa.CommissionPeriodId = @CommissionPeriodID);

	PRINT '/*********************************';
	PRINT '***	Create Summary table info ***';
	PRINT '**********************************/'

/**
** TODO:  Andres NEED TO FINISH BOTH SUMMARIES.
AR:  Not sure where to get the Base Multiple.  I think from the RU_Recruits table.
**/
	INSERT INTO [dbo].[SC_ICMultiples] (
		[WorkAccountId]
		,[CommissionPeriodId]
	)
	SELECT DISTINCT
		SIMD.WorkAccountId
		, SIMD.CommissionPeriodId
	FROM
		dbo.SC_ICMultipleDetails AS SIMD WITH (NOLOCK)
	WHERE
		(SIMD.CommissionPeriodId = @CommissionPeriodId);

	UPDATE SCICD SET 
		ICMultipleId = SCIC.ICMultipleID
	FROM
		[dbo].[SC_ICMultipleDetails] AS SCICD WITH (NOLOCK)
		INNER JOIN [dbo].[SC_ICMultiples] AS SCIC WITH (NOLOCK)
		ON
			(SCIC.WorkAccountId = SCICD.WorkAccountId)
			AND (SCICD.CommissionPeriodId = @CommissionPeriodId)

	/*******************************************
	**** Create a CURSOR To do the Summaries.***
	********************************************/
	DECLARE @ICMultipleID BIGINT, @WorkAccountID BIGINT;
	DECLARE summaryCur CURSOR FOR
	SELECT ICMultipleID, WorkAccountID FROM [dbo].[SC_ICMultiples] WHERE (CommissionPeriodId = @CommissionPeriodId); 

	OPEN summaryCur;
	FETCH NEXT FROM summaryCur INTO
	@ICMultipleID, @WorkAccountID;

	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		-- Update Base Multiple
		UPDATE SCIC SET
			BaseMultiple =  SCICD.MultipleAdjustment
		FROM
			[dbo].[SC_ICMultiples] AS SCIC WITH (NOLOCK)
			INNER JOIN [dbo].[SC_ICMultipleDetails] AS SCICD WITH (NOLOCK)
			ON
				(SCICD.ICMultipleId = SCIC.ICMultipleID)
				AND (SCIC.ICMultipleID = @ICMultipleID)
		WHERE 
			(SCIC.WorkAccountId = @WorkAccountID)
			AND (SCICD.MultipleAdjustmentId IN ('CommercialMultiple', 'GoodExcMultiple', 'SubCreditMultiple'));

		-- Update Rep Base Multiple
		UPDATE SCIC SET
			RepBaseMultiple =  RUR.PersonalMultiple
		FROM
			[dbo].[SC_ICMultiples] AS SCIC WITH (NOLOCK)
			INNER JOIN [dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
			ON
				(SCWAA.WorkAccountID = SCIC.WorkAccountId)
				AND (SCIC.ICMultipleID = @ICMultipleID)
			INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
			ON
				(RU.GPEmployeeId = SCWAA.SalesRepId)
			INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RUR WITH (NOLOCK)
			ON
				(RUR.UserId = RU.UserID)
				AND (RUR.SeasonId = SCWAA.SeasonId)
		WHERE 
			(SCIC.WorkAccountId = @WorkAccountID)

		-- Update Adjustment Summary
		UPDATE SCIC SET
			BaseMultiple = TMT.MultipleAdjustmentSum
		FROM
			[dbo].[SC_ICMultiples] AS SCIC WITH (NOLOCK)
			INNER JOIN 
			(SELECT
				SCICD.WorkAccountId
				, SCICD.CommissionPeriodId
				, SUM(SCICD.MultipleAdjustment) AS MultipleAdjustmentSum
			FROM
				[dbo].[SC_ICMultiples] AS SCIC1 WITH (NOLOCK)
				INNER JOIN [dbo].[SC_ICMultipleDetails] AS SCICD WITH (NOLOCK)
				ON
					(SCICD.ICMultipleId = SCIC1.ICMultipleID)
					AND (SCIC1.ICMultipleID = @ICMultipleID)
			WHERE 
				(SCIC1.WorkAccountId = @WorkAccountID)
				AND (SCICD.MultipleAdjustmentId NOT IN ('CommercialMultiple', 'GoodExcMultiple', 'SubCreditMultiple'))
			GROUP BY
				SCICD.WorkAccountId
				, SCICD.CommissionPeriodId) AS TMT
			ON 
				(TMT.CommissionPeriodId = SCIC.CommissionPeriodId)
				AND (TMT.WorkAccountId = SCIC.WorkAccountId)
				AND (TMT.WorkAccountId = @WorkAccountID);

		-- Calculate Effetive Multiple

		-- Get Next Values
		FETCH NEXT FROM summaryCur INTO
		@ICMultipleID, @WorkAccountID;
	END

	CLOSE summaryCur;
	DEALLOCATE summaryCur;

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