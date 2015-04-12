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
	, @CommissionPeriodEndDate DATE

SELECT 
	@CommissionPeriodID = CommissionPeriodID
	, @CommissionPeriodEndDate = CONVERT(DATE,MIN(CommissionPeriodEndDate))
FROM
	NXSE_Sales.dbo.SC_CommissionPeriods 
WHERE
	CommissionPeriodEndDate >= GETDATE()
GROUP BY
	CommissionPeriodID
/********************  END HEADER ********************/
TRUNCATE TABLE .dbo.SC_workAccounts

/******************
***  CUSTOMERS  ***
*******************/
INSERT dbo.SC_workAccounts
(
	CommissionPeriodId
	, AccountID
	, CustomerMasterFileId
	, SalesRepId
	, TechId
	, FriendsAndFamilyTypeId
	, PaymentType
	, RMR
	, ActivationFee
)
SELECT 
	@CommissionPeriodID
	, MS_AccountSalesInformations.AccountID
	, MC_Accounts.CustomerMasterFileId
	, MS_AccountSalesInformations.SalesRepId
	, MS_AccountSalesInformations.TechId
	, MS_AccountSalesInformations.FriendsAndFamilyTypeId
	, MS_AccountSalesInformations.PaymentTypeId
	, WISE_CRM.dbo.fxMsAccountMMRGet(MS_AccountSalesInformations.AccountID)
	, WISE_CRM.dbo.fxMsAccountSetupFeeGet(MS_AccountSalesInformations.AccountID, 0)
FROM
	-- MS_AccountSalesInformations
	WISE_CRM.dbo.MS_AccountSalesInformations

	-- MC_Accounts
	JOIN WISE_CRM.dbo.MC_Accounts
	ON
		(MS_AccountSalesInformations.AccountID = MC_Accounts.AccountID)

	-- ACCOUNTS ALREADY PAID
	LEFT JOIN NXSE_SALES.dbo.SC_AccountCommissionHistory
	ON
		(MS_AccountSalesInformations.AccountID = SC_AccountCommissionHistory.AccountID)

	-- HOLDS
	LEFT JOIN (
		SELECT 
			AccountId 
		FROM 
			WISE_CRM.dbo.MS_AccountHolds
			JOIN WISE_CRM.dbo.MS_AccountHoldCatg2
			ON
				(MS_AccountHolds.Catg2Id = MS_AccountHoldCatg2.Catg2ID)
				AND (IsRepFrontEndHold = 'TRUE' OR IsRepBackEndHold = 'TRUE')
		) AS hold_qry
	ON MS_AccountSalesInformations.AccountId = hold_qry.AccountId
WHERE 
	-- HOMEOWNER
	(MS_AccountSalesInformations.IsOwner = 'TRUE')

	-- INSTALLED
	AND (MS_AccountSalesInformations.InstallDate < @CommissionPeriodEndDate)

	-- PAPERWORK APPROVED
	AND (MS_AccountSalesInformations.ContractSignedDate < @CommissionPeriodEndDate)

	-- PAST THE 3 DAY CANCELLATION PERIOD
--	AND (MS_AccountSalesInformations.NOCDate < @CommissionPeriodEndDate)

	-- NOT CANCELLED
	AND (MS_AccountSalesInformations.CancelDate IS NULL)

	-- NOT A PREVIOUSLY COMMISSIONED ACCOUNT
	AND (SC_AccountCommissionHistory.AccountID IS NULL)

	AND (hold_qry.AccountId IS NULL)
