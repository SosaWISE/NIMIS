/************************
Objective is it update the WorkAccountsAll table and set the flag based off the qualified rule set

RULE SET IS AS FOLLOWS:
A Qualified account meets the following criteria
1. Customer is the Homeowner
2. Customer passes the Pre and Post Installation Surveys
3. Customer's paperwork is completed, signed and received by the corporate office
	(inlcuding AMA, SOP, QAF & voided check if applicable)
4. Customer is past their 3 day NOC period without cancelling
5. Customer's account is free of holds or unresolved issues

Additional Qualifications are as follows:
1. Poor or Unapproved Credit Customers must pay a $299 activation fee
2. Sub Credit Customers must pay a $199 activation fee
************************/

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
	IsCurrent DESC
	, CommissionPeriodID DESC;

PRINT '************************************************************ START ************************************************************';
PRINT '* Commission Period ID: ' + CAST(@CommissionPeriodID AS VARCHAR) + ' | Start: ' + CAST(@CommissionPeriodStrDate AS VARCHAR) + ' | End: ' + CAST(@CommissionPeriodEndDate AS VARCHAR);
PRINT '************************************************************ START ************************************************************';

/********************  END HEADER ********************/

--SELECT * FROM SC_WorkAccountsAll
--SELECT TOP 100 * FROM [WISE_CRM].[dbo].[MS_AccountHolds] WHERE IsActive = 1

/*	FLAGS TO PREVENT PAYMENT
	NotOwner
	AMANotSigned
	NOCPeriodNotExp
	Cancelled
	HasHolds
	ContractLengthLess36
	NoneCcOrAch
	SetupFeeNotQualified
	IgnoreAllRules
*/

/********************************
***	Customer is Not the Owner ***
********************************/
UPDATE SCWAA SET 
	HasHolds = 'TRUE'
FROM 
	[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = SCWAA.AccountID)
WHERE
	(SCWAA.CommissionPeriodId = @CommissionPeriodId)
	AND (MSASI.IsOwner = 'FALSE');

/*******************************************
***	AMA Sign Date And Approved Paperwork ***
*******************************************/
UPDATE SCWAA SET 
	ApprovedPaperWork = 'TRUE'
FROM 
	[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = SCWAA.AccountID)
WHERE
	(SCWAA.CommissionPeriodId = @CommissionPeriodId)
	AND (MSASI.ApprovedDate IS NULL);

/*********************************
***	NOC Period has not Expired ***
*********************************/
UPDATE SCWAA SET 
	NOCPeriodNotExp = 'TRUE'
FROM 
	[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = SCWAA.AccountID)
WHERE
	(SCWAA.CommissionPeriodId = @CommissionPeriodId)
	AND (MSASI.NOCDateCalculated > GETUTCDATE())

/***********************************
***	Signed NOC has been Received ***
***********************************/
UPDATE SCWAA SET 
	Cancelled = 'TRUE'
FROM 
	[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].vwAE_CustomerAccountInfoToGP AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = SCWAA.AccountID)
WHERE
	(SCWAA.CommissionPeriodId = @CommissionPeriodId)
	AND (MSASI.CancelledDate IS NOT NULL);

/************************
***	Account has Holds ***
************************/
UPDATE SCWAA SET 
	HasHolds = 'TRUE'
FROM 
	[dbo].[SC_WorkAccountsAll] AS SCWAA WITH (NOLOCK)
	INNER JOIN (
		SELECT TOP 1
			AccountId 
			, MS_AccountHolds.FixedOn
		FROM 
			[WISE_CRM].[dbo].MS_AccountHolds
			JOIN [WISE_CRM].[dbo].MS_AccountHoldCatg2
			ON
				(MS_AccountHolds.Catg2Id = MS_AccountHoldCatg2.Catg2ID)
				AND (IsRepFrontEndHold = 'TRUE' OR IsRepBackEndHold = 'TRUE')
				AND (MS_AccountHolds.IsActive = 1)
		) AS hold_qry
	ON
	(hold_qry.AccountId = SCWAA.AccountID)
WHERE
	(SCWAA.CommissionPeriodId = @CommissionPeriodID);
--LOGIC FOR THE WHERE NEEDS TO BE WORKED ON
-- THERE ARE CURRENTLY 2 HOLDS ON 1 OF THE ACCOUNTS IN SC_WorkAccountsAll

/*****************************************
***	ContractLength less than 36 Months ***
*****************************************/

UPDATE SC_WorkAccountsAll SET
	ContractLengthLess36 = 'TRUE'
WHERE 
	(CommissionPeriodId = @CommissionPeriodID)
	AND (ContractLength < 36);

/*******************************************
***	Payment Information Is not CC or ACH ***
*******************************************/

UPDATE SC_WorkAccountsAll SET
	NoneCcOrAch = 'TRUE'
WHERE
	(CommissionPeriodId = @CommissionPeriodID)
	AND (PaymentType NOT IN ('CC', 'ACH'));

/******************************************
***	Incorrect Setup Fee for Credit Tier ***
******************************************/
--Unapproved Activation Fee: $299
--Sub Activation Fee: $199
--Good/Exc Activation Fee: > 0

UPDATE SC_WorkAccountsAll SET
	SetupFeeNotQualified = 
		CASE 
			WHEN
				CreditCustomerType = 'UNAPPROVED'
					AND (ActivationFee <> 299)
			THEN 'TRUE'
			WHEN
				CreditCustomerType = 'SUB'
					AND (ActivationFee <> 199)
			THEN 'TRUE'
		END
WHERE
	(CommissionPeriodId = @CommissionPeriodID)
	AND (CreditCustomerType IN ('UNAPPROVED', 'SUB'));


/*******************
***	Ignore Rules ***
*******************/

/**************************************
***	Build the SC_WorkAccounts Table ***
**************************************/
INSERT INTO [dbo].[SC_WorkAccounts] (
	[WorkAccountID]
	, [CommissionPeriodId]
	, [AccountID]
	, [CustomerMasterFileId]
	, [AccountPackageId]
	, [SalesRepId]
	, [TechId]
	, [FriendsAndFamilyTypeId]
	, [QualifyDate]
	, [SaleDate]
	, [PostSurveyDate]
	, [InstallDate]
	, [AMASignedDate]
	, [NOCDateCalculated]
	, [ApprovedDate]
	, [ApproverId]
	, [SeasonId]
	, [DealerId]
	, [CreditScore]
	, [CreditCustomerType]
	, [ContractLength]
	, [PaymentType]
	, [PointsOfProtection]
	, [PointsAllowed]
	, [PointsAssignedToRep]
	, [ActivationFee]
	, [RMR]
	, [DoNotPay]
	, [Waive1stMonth]
)
SELECT
	WorkAccountID  -- bigint
	, CommissionPeriodId -- bigint
	, AccountID -- bigint
	, CustomerMasterFileId -- bigint
	, AccountPackageId -- int
	, SalesRepId -- varchar(25)
	, TechId -- varchar(25)
	, FriendsAndFamilyTypeId -- varchar(20)
	, QualifyDate -- datetime
	, SaleDate -- datetime
	, PostSurveyDate -- datetime
	, InstallDate -- datetime
	, AMASignedDate -- datetime
	, NOCDateCalculated -- datetime
	, ApprovedDate -- datetime
	, ApproverId -- varchar(50)
	, SeasonId -- int
	, DealerId -- int
	, CreditScore -- int
	, CreditCustomerType -- varchar(15)
	, ContractLength -- int
	, PaymentType -- varchar(20)
	, PointsOfProtection -- decimal
	, PointsAllowed -- decimal
	, PointsAssignedToRep -- decimal
	, ActivationFee -- money
	, RMR -- money
	, DoNotPay -- bit
	, Waive1stMonth -- bit
FROM
	[dbo].[SC_WorkAccountsAll] AS SWAA WITH (NOLOCK)
WHERE
	(SWAA.CommissionPeriodId = @CommissionPeriodID)
	AND (((SWAA.NotOwner = 'FALSE')
	AND (SWAA.ApprovedPaperWork = 'FALSE')
	AND (SWAA.NOCPeriodNotExp = 'FALSE')
	AND (SWAA.Cancelled = 'FALSE')
	AND (SWAA.HasHolds = 'FALSE')
	AND (SWAA.ContractLengthLess36 = 'FALSE')
	AND (SWAA.NoneCcOrAch = 'FALSE')
	AND (SWAA.SetupFeeNotQualified = 'FALSE'))
		OR (SWAA.IgnoreAllRules = 'TRUE'));