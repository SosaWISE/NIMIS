/********************  HEADER  ********************
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
SET @commissionsAdjustmentTypeId = 'RAISERMRINRANGE'

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
SET @commissionsAdjustmentTypeId = 'EQUIPUPGRADE'

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
	msae.AccountEquipmentUpgradeTypeId = 'SALESREP'
		AND scwa.SalesRepId = msae.GPEmployeeId