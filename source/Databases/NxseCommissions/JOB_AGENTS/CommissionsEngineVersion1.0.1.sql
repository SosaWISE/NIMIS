USE [msdb]
GO

/****** Object:  Job [Sales Commissions]    Script Date: 4/11/2015 5:36:58 PM ******/
EXEC msdb.dbo.sp_delete_job @job_id=N'22ab0f4f-037c-41ad-9a02-302851e6b991', @delete_unused_schedule=1
GO

/****** Object:  Job [Sales Commissions]    Script Date: 4/11/2015 5:36:58 PM ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 4/11/2015 5:36:58 PM ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'Sales Commissions', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'No description available.', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'CORPNEXSENSE\bmcfadden', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Get accounts to be commissioned]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Get accounts to be commissioned', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Pull the accounts to be commissioned into the work table
Accounts to be commissioned are:
Customer is a homeowner
Installed
Past the NOC date
All paperwork completed - this means the paperwork has been approved in the Contract Admin module in CRM
Pre- and Post-surveys completed 
No Holds
Not previously commissioned
Accounts approved in Contract Mgmt <= period end date

Employee and Friends and Family accounts - must be done at full-price for a Nexsense package to be commissioned
*/
USE NXSE_Sales
GO

DECLARE @CommissionPeriodID BIGINT
DECLARE @CommissionPeriodEndDate DATE
SELECT 
	@CommissionPeriodID = CommissionPeriodID,
	@CommissionPeriodEndDate = CONVERT(DATE,MIN(CommissionPeriodEndDate))
FROM NXSE_Sales.dbo.SC_CommissionPeriods 
WHERE CommissionPeriodEndDate >= GETDATE()
GROUP BY CommissionPeriodID
/********************  END HEADER ********************/
TRUNCATE TABLE .dbo.SC_workAccounts

/******************
***  CUSTOMERS  ***
*******************/
INSERT dbo.SC_workAccounts
(
	CommissionPeriodId
	,AccountID
	,CustomerMasterFileId
	,SalesRepId
	,TechId
	,FriendsAndFamilyTypeId
	,PaymentType
	,RMR
	,ActivationFee
)
SELECT 
	@CommissionPeriodID
	,MS_AccountSalesInformations.AccountID
	,MC_Accounts.CustomerMasterFileId
	,MS_AccountSalesInformations.SalesRepId
	,MS_AccountSalesInformations.TechId
	,MS_AccountSalesInformations.FriendsAndFamilyTypeId
	,MS_AccountSalesInformations.PaymentTypeId
	,WISE_CRM.dbo.fxMsAccountMMRGet(MS_AccountSalesInformations.AccountID)
	,WISE_CRM.dbo.fxMsAccountSetupFeeGet(MS_AccountSalesInformations.AccountID, 0)
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
				AND (IsRepFrontEndHold = ''TRUE'' OR IsRepBackEndHold = ''TRUE'')
		) AS hold_qry
	ON MS_AccountSalesInformations.AccountId = hold_qry.AccountId
WHERE 
	-- HOMEOWNER
	(MS_AccountSalesInformations.IsOwner = ''TRUE'')

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
', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Classify accounts by credit score]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Classify accounts by credit score', 
		@step_id=2, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Customers are one of these types:
Unapproved Customers - the customer does not have the minimum credit score
Sub Customers
Good Credit Customers

We will set a flag on each account in the work table to indicate the type of customer
*/
USE NXSE_Sales
GO

/********************  END HEADER ********************/
-- GET THE CREDIT SCORES FOR EACH ACCOUNT
UPDATE SC_workAccounts
SET CreditScore = WISE_CRM.dbo.fxQlCreditReportGetScoreByMsAccountID(SC_workAccounts.AccountID)
FROM
	dbo.SC_workAccounts

-- SET THE CREDITCUSTOMERTYPE
	-- UNAPPROVED CUSTOMERS (<600)
	-- SUB CUSTOMERS (600-624)
	-- GOOD CREDIT CUSTOMERS (625-699)
	-- EXCELLENT CREDIT CUSTOMERS (700+)
UPDATE SC_workAccounts
SET CreditCustomerType =
	CASE
		WHEN CreditScore >= 700 THEN ''EXCELLENT''
		WHEN CreditScore BETWEEN 625 AND 699 THEN ''GOOD''
		
		WHEN CreditScore BETWEEN 600 AND 624 THEN ''SUB''
		
		--WHEN CreditScore < 600 THEN ''UNAPPROVED''
		ELSE ''UNAPPROVED''
	END
FROM dbo.SC_workAccounts
', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Calculate Points of Protection for each account]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Calculate Points of Protection for each account', 
		@step_id=3, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'Select ''Calculate Points of Protection for each account''
/*
Points of protection are based on:
Package
Activation Fee
Monitoring rate
Upgrades from the product list
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Flag Accounts w/Deductions]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Flag Accounts w/Deductions', 
		@step_id=4, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Deductions get applied for:
	Agreement length
	Payment type
	Activation fee
	Points of protection	--STILL NEED TO DO THIS ONE
	Lowered RMR
	Special deals
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT

/********************  END HEADER ********************/

/*************************
***  AGREEMENT LENGTH  ***
*************************/
-- GET CONTRACT LENGTHS
UPDATE SC_workAccounts
SET ContractLength = AE_Contracts.ContractLength
FROM 
	dbo.SC_workAccounts
	JOIN WISE_CRM.dbo.AE_Contracts
	ON
		SC_workAccounts.AccountID = AE_Contracts.AccountId
		AND AE_Contracts.IsDeleted = ''FALSE''

-- DEDUCT FOR AGREEMENT LENGTH = 36
SET @commissionsAdjustmentTypeId = ''AGRMT36''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(ContractLength = 36)

/*******************
***	PAYMENT TYPE ***
*******************/

-- DEDUCT FOR PAYMENT TYPE IS CREDIT CARD
SET @commissionsAdjustmentTypeId = ''PMTCC''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(PaymentType = ''CC'')

/*********************
***	ACTIVATION FEE ***
*********************/

-- DEDUCT FOR ACTIVATION FEE WAIVED
SET @commissionsAdjustmentTypeId = ''ACTWAIVED''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE 
	(ActivationFee = 0)

/***************************
***	POINTS OF PROTECTION ***
***************************/



/*******************************
***	LOWERED RMR WITHIN RANGE ***
*******************************/

--DEDUCT FOR LOWERING RMR AND STAYING WITHIN THE ALLOWED RANGE
SET @commissionsAdjustmentTypeId = ''LOWRMRINRANGE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
WHERE
	scwa.RMR < msap.BaseRMR


/*********************************
***	ADJUSTED RMR OUTSIDE RANGE ***
*********************************/

--DEDUCT FOR LOWERING RMR AND GOING OUTSIDE THE ALLOWED RANGE OR GOING ABOVE THE MAX RMR
SET @commissionsAdjustmentTypeId = ''ADJRMROUTRANGE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
WHERE
	scwa.RMR < msap.MinRMR
	or scwa.RMR > msap.MaxRMR

/********************
***	SPECIAL DEALS ***
********************/

/********************************
***	waiving first month''s RMR ***
********************************/

--DEDUCT FOR WAIVING THE 1ST MONTH RMR
SET @commissionsAdjustmentTypeId = ''WAIVED1STMONTH''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM
	SC_CommissionsAdjustments
WHERE
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts
WHERE
	(Waive1stMonth = 1)

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it''s all between the rep and the customer.', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Flag Accounts w/Upgrades]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Flag Accounts w/Upgrades', 
		@step_id=5, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Bonuses get applied for:
	Increasing the RMR and staying within the range
	Selling equipment to the customer
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT

/********************  END HEADER ********************/


/********************************
***	INCREASE RMR WITHIN RANGE ***
********************************/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = ''RAISERMRINRANGE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
WHERE 
	(scwa.RMR > msap.BaseRMR)

/************************
***	SELLING EQUIPMENT ***
************************/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = ''EQUIPUPGRADE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_workAccountAdjustments
(
	WorkAccountId,
	CommissionsAdjustmentID
)
SELECT 
	WorkAccountID,
	@commissionsAdjustmentId
FROM dbo.SC_workAccounts AS scwa
	JOIN WISE_CRM.dbo.MS_AccountEquipment AS msae ON scwa.AccountID = msae.AccountId
WHERE
	msae.AccountEquipmentUpgradeTypeId = ''SALESREP''
		AND scwa.SalesRepId = msae.GPEmployeeId', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Calculate Account Deductions]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Calculate Account Deductions', 
		@step_id=6, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Deductions get applied for:
	Agreement length		DONE; TEST PASSED
	Payment type			DONE; TEST PASSED
	Activation fee			DONE; TEST PASSED
	Points of protection		
	Lowered RMR in range	DONE; TEST PASSED
	Lowered RMR out range	DONE; BELOW MINRMR: TEST PASSED | ABOVE MAXRMR: TEST PASSED
	Special deals			DONE; TEST PASSED
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT
DECLARE @AdjustmentAmount MONEY
DECLARE @RMRChange MONEY

/********************  END HEADER ********************/

/*************************
***  AGREEMENT LENGTH  ***
*************************/
SET @commissionsAdjustmentTypeId = ''AGRMT36''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*******************
***	PAYMENT TYPE ***
*******************/
SET @commissionsAdjustmentTypeId = ''PMTCC''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*********************
***	ACTIVATION FEE ***
*********************/
SET @commissionsAdjustmentTypeId = ''ACTWAIVED''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/***************************
***	POINTS OF PROTECTION ***
***************************/



/*******************************
***	LOWERED RMR WITHIN RANGE ***
*******************************/
SET @commissionsAdjustmentTypeId = ''LOWRMRINRANGE''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		CASE WHEN (scwa.RMR >= msap.MinRMR) THEN (msap.BaseRMR - scwa.RMR)
		ELSE (msap.BaseRMR - msap.MinRMR)
		END AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)
--PRINT ''RMR CHANGE = '' + CONVERT(VARCHAR(10), @RMRChange)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we''re putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/*********************************
***	ADJUSTED RMR OUTSIDE RANGE ***
*********************************/
SET @commissionsAdjustmentTypeId = ''ADJRMROUTRANGE''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		CASE
			WHEN (scwa.RMR > msap.MaxRMR) THEN (scwa.RMR - msap.MaxRMR)
			WHEN (scwa.RMR < msap.MinRMR) THEN (msap.MinRMR - scwa.RMR)
		END AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we''re putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId


/********************
***	SPECIAL DEALS ***
********************/

/********************************
***	waiving first month''s RMR ***
********************************/
SET @commissionsAdjustmentTypeId = ''WAIVED1STMONTH''

-- get the id for this adjustment type so it can be inserted into the WorkAccountLedger table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @AdjustmentAmount = (SELECT (scca.CommissionAdjustmentAmount + scwa.RMR)
							FROM SC_WorkAccounts AS scwa
								JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
								JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
							WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription --right now we''re putting in the commission adjustment description. Peter would like to change it to show the basermr, actual rmr and the difference
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/********************************************
***	Takeover buyout of existing agreement ***
********************************************/

--As a company we are not doing anything with this it''s all between the rep and the customer.', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Calculate Account Upgrades]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Calculate Account Upgrades', 
		@step_id=7, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/********************  HEADER  ********************
Bonuses get applied for:
	Increasing the RMR and staying within the range		DONE; TEST PASSED
	Selling equipment to the customer					DONE; Single Upgrade Test passed | Multiple Upgrade for one account needs to be tested
*/
USE NXSE_Sales
GO

DECLARE @commissionsAdjustmentTypeId VARCHAR(20)
DECLARE @commissionsAdjustmentId BIGINT
DECLARE @AdjustmentAmount MONEY
DECLARE @RMRChange MONEY

/********************  END HEADER ********************/

/********************************
***	INCREASE RMR WITHIN RANGE ***
********************************/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = ''RAISERMRINRANGE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

SET @RMRChange = (
	SELECT
		(scwa.RMR - msap.BaseRMR) AS RMRChange
		--,msap.BaseRMR
		--,scwa.RMR
	FROM SC_WorkAccounts AS scwa
		JOIN WISE_CRM.dbo.MS_AccountPackages AS msap ON scwa.AccountPackageId = msap.AccountPackageID
		JOIN SC_WorkAccountAdjustments AS scwaa ON scwa.WorkAccountID = scwaa.WorkAccountId
		JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
	WHERE scca.CommissionsAdjustmentID = @commissionsAdjustmentId
	)
PRINT ''RMR CHANGE = '' + CONVERT(VARCHAR(10), @RMRChange)

SET @AdjustmentAmount = @RMRChange * (SELECT CommissionAdjustmentAmount FROM SC_CommissionsAdjustments WHERE CommissionsAdjustmentID = @commissionsAdjustmentId)

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsAdjustmentId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsAdjustmentId
	, @AdjustmentAmount AS Amount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId

/************************
***	SELLING EQUIPMENT ***
************************/
/*	RIGHT NOW WE''RE GIVING A FIXED VALUE FOR UPGRADES WE NEED TO BE PULLING THE UPGRADE BONUS COLUMN FROM GP''S ITEM TABLE AND USE THAT AMOUNT	*/

-- Bonus for RMR Increase within the range
SET @commissionsAdjustmentTypeId = ''EQUIPUPGRADE''

-- get the id for this adjustment type so it can be inserted into the workAccountAdjustments table
SELECT 
	@commissionsAdjustmentId = CommissionsAdjustmentID
FROM 
	SC_CommissionsAdjustments
WHERE 
	CommissionsAdjustmentTypeId = @commissionsAdjustmentTypeId

INSERT SC_WorkAccountLedger
(
	AccountId
	, SalesRepId
	--, WorkAccountPeriodId
	--, InvoiceItemId
	, CommissionsDeductionId
	, Amount
	, Description
)
SELECT scwa.AccountID
	, scwa.SalesRepId
	--, ''INT'' as WorkAccountPriodID --SHOULDN''T BE NULL BUT PETER ALLOWED IT FOR TESTING
	--, ''InvoiceItem'' as InvoiceItemId --
	, @commissionsAdjustmentId AS CommissionsDeductionId
	, scca.CommissionAdjustmentAmount
	, scca.CommissionsAdjustmentDescription
FROM SC_WorkAccountAdjustments AS scwaa
	JOIN SC_WorkAccounts AS scwa ON scwaa.WorkAccountId = scwa.WorkAccountID
	JOIN SC_CommissionsAdjustments AS scca ON scwaa.CommissionsAdjustmentID = scca.CommissionsAdjustmentID
WHERE scwaa.CommissionsAdjustmentID = @commissionsAdjustmentId', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Get Reps to be commissioned]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Get Reps to be commissioned', 
		@step_id=8, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
Get reps having accounts to be commissioned this period into the work table
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Calculate Number of Accounts per Rep]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Calculate Number of Accounts per Rep', 
		@step_id=9, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
Determine the number of Good Credit accounts to be commissioned for each rep during this period
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Update Reps reaching Experienced Rate]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Update Reps reaching Experienced Rate', 
		@step_id=10, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'Select ''DSC: Update Reps reaching Experienced Rate''
/*
Reps who have not sold in another door-to-door program reach Experienced Rate when they have:
x number of accumulated customers
OR
x number of consecutive periods attaining a minimum sales level
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Calculate Annual Bonus Incentive]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Calculate Annual Bonus Incentive', 
		@step_id=11, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
Determine who reaches volume thresholds for the Annual Bonus Incentive
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Get cancelled accounts]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Get cancelled accounts', 
		@step_id=12, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
select accounts that have been cancelled into a work table.
Accounts that have the following:
Commissions have been paid
Cancelled <= true-up period end date
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Calculate Cancelled account deductions]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Calculate Cancelled account deductions', 
		@step_id=13, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
There is a prorated scale on cancellations
Determine the amount of time between install and cancellation and use the scale to determine the amount to deduct
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [DSC: Update year-to-date commissions bonuses and deductions]    Script Date: 4/11/2015 5:36:59 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DSC: Update year-to-date commissions bonuses and deductions', 
		@step_id=14, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'/*
update year-to-date earnings for commissions, bonuses, and deductions
*/', 
		@database_name=N'NXSE_Sales', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO


