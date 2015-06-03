/********************  HEADER  ********************
* DESCRIPTION: Pull the accounts to be commissioned into the work table
*	Accounts to be commissioned are:
*	Customer is a homeowner
*	Installed
*	Past the NOC date
*	All paperwork completed - this means the paperwork has been approved in the Contract Admin module in CRM
*	Pre- and Post-surveys completed 
*	No Holds
*	Not previously commissioned
*	Accounts approved in Contract Mgmt <= period end date
*	
*	Employee and Friends and Family accounts - must be done at full-price for a Nexsense package to be commissioned
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
IF (@TRUNCATE = 'ON' AND @CommissionPeriodID = 1)
BEGIN
	PRINT '|* -DELETING  [dbo].[SC_WorkAccounts]- *|';
	DELETE [dbo].[SC_WorkAccounts];
END
/** Reset the CommissionPeriods table to 1 for first pass.*/
IF (@TRUNCATE = 'ON' AND @CommissionPeriodID = 1)
BEGIN
	DECLARE @FirstPeriodID INT;
	SELECT TOP 1 @FirstPeriodID = CommissionPeriodID FROM [dbo].[SC_CommissionPeriods] WHERE (CommissionEngineId = @CommissionEngineId) ORDER BY CommissionPeriodID;
	PRINT '|* -RESETTING  [dbo].[SC_CommissionPeriods]- *|';
	UPDATE [dbo].[SC_CommissionPeriods] SET IsCurrent = NULL WHERE (CommissionEngineId = @CommissionEngineId);
	UPDATE [dbo].[SC_CommissionPeriods] SET IsCurrent = 'TRUE' WHERE (CommissionEngineId = @CommissionEngineId) AND (CommissionPeriodID = @FirstPeriodID);

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
END

PRINT N'/******************';
PRINT N'***  CUSTOMERS  ***';
PRINT N'*******************/';
INSERT dbo.SC_workAccountsAll
(
	CommissionPeriodId
	, AccountID
	, CustomerMasterFileId
	, AccountPackageId
	, SalesTeamId
	, SalesRepId
	, ManSalesRepId
	, RecByRepId
	, TechId
	, ManTechId
	, FriendsAndFamilyTypeId
	, QualifyDate
	, SaleDate
	, PostSurveyDate
	, InstallDate
	, AMASignedDate
	, NOCDateCalculated
	, ApprovedDate
	, ApproverId
	, SeasonId
	, DealerId
	, CreditScore
	, CreditCustomerType
	, ContractLength
	, PaymentType
	, RMR
	, ActivationFee
	, PointsOfProtection
	, PointsAllowed
	, PointsAssignedToRep
)
SELECT DISTINCT
	@CommissionPeriodID AS CommissionPeriodId
	, MSASI.AccountID
	, MC_Accounts.CustomerMasterFileId
	, MSASI.AccountPackageId
	, dbo.fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId(MSASI.SalesRepId, MSASI.SeasonId) AS SalesTeamId
	, MSASI.SalesRepId
	, dbo.fxSCv2_0GetManagerBySalesRepIdAndSeasonId(MSASI.SalesRepId, MSASI.SeasonId) AS [ManSaleRepId]
	, dbo.fxSCv2_0GetRecByRepIdBySalesRepId(MSASI.SalesRepId) AS RecByRepId
	, MSASI.TechId
	, NULL AS [ManTechId]
	, MSASI.FriendsAndFamilyTypeId
	, QL.QualifyDate
	, SRV.SaleDate
	, SRVPost.PostSurveyDate
	, MSASI.InstallDate
	, MSASI.AMASignDate
	, MSASI.NOCDateCalculated
	, MSASI.ApprovedDate
	, MSASI.ApproverID
	, MSASI.SeasonId
	, MSASI.DealerId
	, MSASI.CreditScore
	, CASE
			WHEN MSASI.CreditScore >= 700 THEN 'EXCELLENT'
			WHEN MSASI.CreditScore BETWEEN 625 AND 699 THEN 'GOOD'
		
			WHEN MSASI.CreditScore BETWEEN 600 AND 624 THEN 'SUB'
		
			--WHEN MSASI.CreditScore < 600 THEN 'UNAPPROVED'
			ELSE 'UNAPPROVED'
		END
	, MSASI.ContractLength
	, MSASI.PaymentType
	, WISE_CRM.dbo.fxMsAccountMMRGet(MSASI.AccountID)
	, MSASI.ActivationFee -- WISE_CRM.dbo.fxMsAccountSetupFeeGet(MSASI.AccountID, 0)
	, MSASI.TotalPoints AS PointsOfProtection  -- [dbo].fxMsAccountsTotalPoints(MSA.AccountID) AS [TotalPoints]
	, MSASI.TotalPointsAllowed AS PointsAllowed -- [dbo].fxMsAccountTotalPointsAllowed(MSA.AccountID) AS [TotalPointsAllowed]
	, MSASI.RepPoints AS PointsAssignedToRep -- [dbo].fxMsAccountTotalPointsRep(MSA.AccountID) AS RepPoints
FROM
	-- MS_AccountSalesInformations
	[WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)

	-- MC_Accounts
	INNER JOIN [WISE_CRM].[dbo].MC_Accounts
	ON
		(MSASI.AccountID = MC_Accounts.AccountID)

	-- Tie SessionIDs or Contracts
	INNER JOIN [dbo].fxSCV2_0GetSessionIdByPeriodId(@CommissionPeriodID) AS CNTENG
	ON
		(MSASI.SeasonId = CNTENG.SeasonId)
		AND (MSASI.DealerId = CNTENG.DealerId)

	-- ACCOUNTS ALREADY PAID
	LEFT JOIN [NXSE_Commissions].dbo.SC_AccountCommissionHistory
	ON
		(MSASI.AccountID = SC_AccountCommissionHistory.AccountID)

	-- HOLDS
	LEFT OUTER JOIN (
		SELECT TOP 1
			AccountId 
			, MS_AccountHolds.FixedOn
		FROM 
			[WISE_CRM].[dbo].MS_AccountHolds
			JOIN [WISE_CRM].[dbo].MS_AccountHoldCatg2
			ON
				(MS_AccountHolds.Catg2Id = MS_AccountHoldCatg2.Catg2ID)
				AND (IsRepFrontEndHold = 'TRUE' OR IsRepBackEndHold = 'TRUE')
		) AS hold_qry
	ON
		(MSASI.AccountId = hold_qry.AccountId)
	LEFT OUTER JOIN (
		SELECT
			AccountID
			, SurveyTypeId
			, IsComplete
			, Passed
			, CreatedOn AS SaleDate
			, ROW_NUMBER() OVER (PARTITION BY AccountID, SurveyTypeId ORDER BY CreatedOn DESC) AS ROWN
		FROM
			[WISE_SurveyEngine].[dbo].vwSV_Results
		WHERE
			(SurveyTypeId = 1000)  -- Pre Survey
			AND (IsComplete = 1 AND Passed = 1)
	) AS SRV
	ON
		(MSASI.AccountID = SRV.AccountID)
		AND (SRV.ROWN = 1)
	LEFT OUTER JOIN (
		SELECT
			AccountID
			, SurveyTypeId
			, IsComplete
			, Passed
			, CreatedOn AS PostSurveyDate
			, ROW_NUMBER() OVER (PARTITION BY AccountID, SurveyTypeId ORDER BY CreatedOn DESC) AS ROWN
		FROM
			[WISE_SurveyEngine].[dbo].vwSV_Results
		WHERE
			(SurveyTypeId = 1001)  -- Post Survey
			AND (IsComplete = 1 AND Passed = 1)
	) AS SRVPost
	ON
		(MSASI.AccountID = SRVPost.AccountID)
		AND (SRV.ROWN = 1)
	LEFT OUTER JOIN (
		SELECT --TOP 1
			AECA.AccountId
			, MIN(QL.CreatedOn) AS QualifyDate
		FROM
			[WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
			INNER JOIN [WISE_CRM].[dbo].[QL_Leads] AS QL WITH (NOLOCK)
			ON
				(QL.LeadID = AECA.LeadId)
		--WHERE
		--	AECA.AccountId IN (191189,191186,191206,191205,191207,191209,191210)
		GROUP BY
			AECA.AccountId
		) AS QL
	ON
		(QL.AccountId = MSASI.AccountID)
WHERE
	-- INSTALLED
	(MSASI.InstallDate BETWEEN @CommissionPeriodStrDate AND @CommissionPeriodEndDate)

	-- Right SessionID


	---- HOMEOWNER
	--AND (MSASI.IsOwner = 'TRUE')

	---- PAPERWORK APPROVED
	----AND (MSASI.AMASignDate < @CommissionPeriodEndDate)
	--AND (MSASI.ApprovedDate IS NOT NULL)

	---- PAST THE 3 DAY CANCELLATION PERIOD
	----AND (MSASI.NOCDateCalculated <= GETUTCDATE())

	---- NOT CANCELLED
	--AND (MSASI.CancelledDate IS NULL)

	---- NOT A PREVIOUSLY COMMISSIONED ACCOUNT
	--AND (SC_AccountCommissionHistory.AccountID IS NULL)

	---- Has no holds
	--AND ((hold_qry.AccountId IS NULL) OR (hold_qry.FixedOn IS NOT NULL))

	--/************************* Contract Length ********************************
	--* Only contracts greater than 36 months qualify for this commissions rules.
	--**************************************************************************/
	--AND (MSASI.ContractLength >= 36)

	--/************************** Payment Type *********************************
	--* Only allow those accounts that have CC and ACH
	--**************************************************************************/
	--AND (MSASI.PaymentType = 'CC' OR MSASI.PaymentType = 'ACH')

	--/************************* Activation Fee ********************************
	--* Activation Fee and Credit Score do not qualify
	--**************************************************************************/
	--AND 
	--	((MSASI.CreditScore < 600 AND MSASI.ActivationFee >= 299.00) 
	--	OR (MSASI.CreditScore BETWEEN 600 AND 624 AND MSASI.ActivationFee >= 199.00)
	--	OR (MSASI.CreditScore >= 625))
	;

IF (@DEBUG_MODE = 'ON')
BEGIN
	--SELECT * FROM [dbo].SC_WorkAccounts;

	SELECT
		RU.FullName
		,SCWA.*
	FROM
		[dbo].[SC_WorkAccountsAll] AS SCWA WITH (NOLOCK)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(SCWA.SalesRepId = RU.GPEmployeeId)
	WHERE
		(SCWA.CommissionPeriodId = @CommissionPeriodID)
	ORDER BY
		SCWA.InstallDate;
END
--SELECT TotalPoints FROM [WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP WHERE AccountID = 191168