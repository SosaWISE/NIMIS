/********************  HEADER  ********************
Customers are one of these types:
Unapproved Customers - the customer does not have the minimum credit score
Sub Customers
Good Credit Customers

We will set a flag on each account in the work table to indicate the type of customer
*/
USE [NXSE_Commissions]
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
	[NXSE_Commissions].dbo.SC_CommissionPeriods 
ORDER BY
	CommissionPeriodID DESC;

/********************  END HEADER ********************/
---- GET THE CREDIT SCORES FOR EACH ACCOUNT
--UPDATE SC_workAccounts
--SET CreditScore = WISE_CRM.dbo.fxQlCreditReportGetScoreByMsAccountID(SC_workAccounts.AccountID)
--FROM
--	dbo.SC_workAccounts

-- SET THE CREDITCUSTOMERTYPE
	-- UNAPPROVED CUSTOMERS (<600)
	-- SUB CUSTOMERS (600-624)
	-- GOOD CREDIT CUSTOMERS (625-699)
	-- EXCELLENT CREDIT CUSTOMERS (700+)
UPDATE SC_workAccounts SET
	CreditCustomerType =
		CASE
			WHEN CreditScore >= 700 THEN 'EXCELLENT'
			WHEN CreditScore BETWEEN 625 AND 699 THEN 'GOOD'
		
			WHEN CreditScore BETWEEN 600 AND 624 THEN 'SUB'
		
			--WHEN CreditScore < 600 THEN 'UNAPPROVED'
			ELSE 'UNAPPROVED'
		END
FROM
	dbo.SC_workAccounts
WHERE
	(CommissionPeriodId = @CommissionPeriodID)

IF (@DEBUG_MODE = 'ON')
BEGIN
	SELECT * FROM [dbo].[SC_workAccounts] WHERE (CommissionPeriodId = @CommissionPeriodID);
END
