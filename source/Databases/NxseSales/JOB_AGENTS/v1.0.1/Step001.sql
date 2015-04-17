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
USE NXSE_Sales
GO

DECLARE @CommissionPeriodID BIGINT
	, @CommissionPeriodStrDate DATETIME
	, @CommissionPeriodEndDate DATETIME
	, @DEBUG_MODE VARCHAR(20) = 'OFF';

SELECT @DEBUG_MODE = GlobalPropertyValue FROM [dbo].[SC_GlobalProperties] WHERE (GlobalPropertyID = 'DEBUG_MODE');

SELECT TOP 1
	@CommissionPeriodID = CommissionPeriodID
	, @CommissionPeriodEndDate = CommissionPeriodEndDate
	, @CommissionPeriodStrDate = DATEADD(d, -7, CommissionPeriodEndDate)
FROM
	NXSE_Sales.dbo.SC_CommissionPeriods 
ORDER BY
	CommissionPeriodID DESC;

/********************  END HEADER ********************/
IF (@DEBUG_MODE = 'ON')
BEGIN
	TRUNCATE TABLE dbo.SC_WorkAccountAdjustments;
	DBCC CHECKIDENT ('[dbo].[SC_WorkAccountAdjustments]', RESEED, 0);

	DELETE dbo.SC_WorkAccountsAll;
	DBCC CHECKIDENT ('[dbo].[SC_WorkAccounts]', RESEED, 0);
END

/******************
***  CUSTOMERS  ***
*******************/
INSERT dbo.SC_workAccountsAll
(
	CommissionPeriodId
	, AccountID
	, CustomerMasterFileId
	, AccountPackageId
	, SalesRepId
	, TechId
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
	@CommissionPeriodID
	, MSASI.AccountID
	, MC_Accounts.CustomerMasterFileId
	, MSASI.AccountPackageId
	, MSASI.SalesRepId
	, MSASI.TechId
	, MSASI.FriendsAndFamilyTypeId
	, QL.QualifyDate
	, SRV.SaleDate
	, SRVPost.PostSurveyDate
	, MSASI.InstallDate
	, MSASI.AMASignDate
	, MSASI.NOCDateCalculated
	, MSASI.ApprovedDate
	, MSASI.ApproverID
	, [WISE_CRM].[dbo].fxGetSeasonIDByAccountID(MSASI.AccountID)
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
	, MSASI.TotalPoints
	, MSASI.TotalPointsAllowed
	, MSASI.RepPoints
FROM
	-- MS_AccountSalesInformations
	[WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)

	-- MC_Accounts
	INNER JOIN [WISE_CRM].[dbo].MC_Accounts
	ON
		(MSASI.AccountID = MC_Accounts.AccountID)

	-- ACCOUNTS ALREADY PAID
	LEFT JOIN NXSE_SALES.dbo.SC_AccountCommissionHistory
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
		SELECT TOP 1
			AECA.AccountId
			, QL.LeadID
			, QL.CreatedOn AS QualifyDate
			, ROW_NUMBER() OVER (PARTITION BY AECA.AccountID, QL.LeadID ORDER BY QL.CreatedOn) AS ROWNUM
		FROM
			[WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
			INNER JOIN [WISE_CRM].[dbo].[QL_Leads] AS QL WITH (NOLOCK)
			ON
				(QL.LeadID = AECA.LeadId)
		ORDER BY
			QL.CreatedOn
	) AS QL
	ON
		(QL.AccountId = MSASI.AccountID)
		AND (QL.ROWNUM = 1)
WHERE 
	-- INSTALLED
	(MSASI.InstallDate BETWEEN @CommissionPeriodStrDate AND @CommissionPeriodEndDate)

	---- HOMEOWNER
	--AND (MSASI.IsOwner = 'TRUE')

	---- PAPERWORK APPROVED
	----AND (MSASI.AMASignDate < @CommissionPeriodEndDate)
	--AND (MSASI.AMASignDate IS NOT NULL)

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
	ORDER BY
		SCWA.InstallDate;
END
