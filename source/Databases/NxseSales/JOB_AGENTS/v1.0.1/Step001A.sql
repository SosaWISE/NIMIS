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

SELECT * FROM SC_WorkAccountsAll
SELECT * FROM [WISE_CRM].[dbo].[MS_AccountHolds]
	WHERE IsActive = 1

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


/********************
***	AMA Sign Date ***
********************/


/*********************************
***	NOC Period has not Expired ***
*********************************/


/***********************************
***	Signed NOC has been Received ***
***********************************/


/************************
***	Account has Holds ***
************************/
BEGIN TRANSACTION

SELECT * FROM SC_WorkAccountsAll AS scwaa
	JOIN [WISE_CRM].[dbo].[MS_AccountHolds] AS msah ON scwaa.AccountID = msah.AccountId
		AND msah.IsActive = 1

UPDATE SC_WorkAccountsAll
	SET HasHolds = 1
WHERE [WISE_CRM].[dbo].[MS_AccountHolds].AccountId is in sc_workaccountsAll
	AND [WISE_CRM].[dbo].[MS_AccountHolds].IsActive = 1 
--LOGIC FOR THE WHERE NEEDS TO BE WORKED ON
-- THERE ARE CURRENTLY 2 HOLDS ON 1 OF THE ACCOUNTS IN SC_WorkAccountsAll
	

ROLLBACK TRANSACTION

/*****************************************
***	ContractLength less than 36 Months ***
*****************************************/
BEGIN TRANSACTION

UPDATE SC_WorkAccountsAll
	SET ContractLengthLess36 = 1
WHERE ContractLength < 36

ROLLBACK TRANSACTION

/*******************************************
***	Payment Information Is not CC or ACH ***
*******************************************/
BEGIN TRANSACTION

UPDATE SC_WorkAccountsAll
	SET NoneCcOrAch = 1
WHERE PaymentType NOT IN ('CC', 'ACH')

ROLLBACK TRANSACTION

/******************************************
***	Incorrect Setup Fee for Credit Tier ***
******************************************/
--Unapproved Activation Fee: $299
--Sub Activation Fee: $199
--Good/Exc Activation Fee: > 0

BEGIN TRANSACTION

UPDATE SC_WorkAccountsAll
	SET SetupFeeNotQualified = 
		CASE 
			WHEN
				CreditCustomerType = 'UNAPPROVED'
					AND (ActivationFee <> 299)
			THEN 1
			WHEN
				CreditCustomerType = 'SUB'
					AND (ActivationFee <> 199)
			THEN 1
		END
WHERE CreditCustomerType IN ('UNAPPROVED', 'SUB')

ROLLBACK TRANSACTION

/*******************
***	Ignore Rules ***
*******************/

