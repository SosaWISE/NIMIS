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

	DELETE dbo.SC_WorkAccounts;
	DBCC CHECKIDENT ('[dbo].[SC_WorkAccounts]', RESEED, 0);
END

/******************
***  CUSTOMERS  ***
*******************/
INSERT dbo.SC_workAccounts
(
	CommissionPeriodId
	, AccountID
	, CustomerMasterFileId
	, AccountPackageId
	, SalesRepId
	, TechId
	, FriendsAndFamilyTypeId
	, InstallDate
	, AMASignedDate
	, NOCDateCalculated
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
SELECT 
	@CommissionPeriodID
	, MSASI.AccountID
	, MC_Accounts.CustomerMasterFileId
	, MSASI.AccountPackageId
	, MSASI.SalesRepId
	, MSASI.TechId
	, MSASI.FriendsAndFamilyTypeId
	, MSASI.InstallDate
	, MSASI.AMASignDate
	, MSASI.NOCDateCalculated
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
	LEFT JOIN (
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
WHERE 
	-- HOMEOWNER
	(MSASI.IsOwner = 'TRUE')

	-- INSTALLED
	AND (MSASI.InstallDate BETWEEN @CommissionPeriodStrDate AND @CommissionPeriodEndDate)

	-- PAPERWORK APPROVED
	--AND (MSASI.AMASignDate < @CommissionPeriodEndDate)
	AND (MSASI.AMASignDate IS NOT NULL)

	-- PAST THE 3 DAY CANCELLATION PERIOD
	--AND (MSASI.NOCDateCalculated <= GETUTCDATE())

	-- NOT CANCELLED
	AND (MSASI.CancelledDate IS NULL)

	-- NOT A PREVIOUSLY COMMISSIONED ACCOUNT
	AND (SC_AccountCommissionHistory.AccountID IS NULL)

	-- Has no holds
	AND ((hold_qry.AccountId IS NULL) OR (hold_qry.FixedOn IS NOT NULL))

	/************************* Contract Length ********************************
	* Only contracts greater than 36 months qualify for this commissions rules.
	**************************************************************************/
	AND (MSASI.ContractLength >= 36)

	/************************** Payment Type *********************************
	* Only allow those accounts that have CC and ACH
	**************************************************************************/
	AND (MSASI.PaymentType = 'CC' OR MSASI.PaymentType = 'ACH')

	/************************* Activation Fee ********************************
	* Only allow those accounts that have CC and ACH
	**************************************************************************/
	AND 
		((MSASI.CreditScore < 600 AND MSASI.ActivationFee >= 299.00) 
		OR (MSASI.CreditScore BETWEEN 600 AND 624 AND MSASI.ActivationFee >= 199.00)
		OR (MSASI.CreditScore >= 625))


IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].SC_WorkAccounts;
END
