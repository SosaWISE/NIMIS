USE [NXSE_Sales]
GO
/******************************************************************************************
*	UNIT TESTING
* STEP:  001
* VERSION : 002
******************************************************************************************/

BEGIN TRANSACTION

/********************* 
 * Release all holds *
 *********************/
SELECT * FROM [WISE_CRM].[dbo].[MS_AccountHolds] WHERE AccountID IN (SELECT AccountID FROM [dbo].SC_WorkAccounts);

UPDATE [WISE_CRM].[dbo].[MS_AccountHolds] SET
	FixedBy = 'SOSAA001'
	, FixedOn = GETUTCDATE()
	, FixedNote = 'This is a Unit Test fix'
WHERE
	AccountID IN (SELECT AccountID FROM [dbo].SC_WorkAccounts);

/*********************************** 
 * Set Contract Signed Date		   *
 ***********************************/
UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET
	ContractSignedDate = InstallDate
WHERE
	AccountID IN (SELECT AccountID FROM [dbo].SC_WorkAccounts)
	AND (ContractSignedDate IS NULL);
	
ROLLBACK TRANSACTION