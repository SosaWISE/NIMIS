/********************  HEADER  ********************
* DESCRIPTION: Pull the accounts to be commissioned into the work table
*	Points of protection are based on:
*	Package
*	Activation Fee
*	Monitoring rate
*	Upgrades from the product list
*			
*						DEPRICATED
*						DEPRICATED
*						DEPRICATED
*
* DEPRICATED SINCE IT IS CALCULATED IN the [WISE_CRM].[dbo].[vwMS_AccountSalesInformations]
*	Here are the functions that are called and passed to [WISE_CRM].[dbo].[vwAE_CustomerAccountInfoToGP]
*				, [dbo].fxMsAccountsTotalPoints(MSA.AccountID) AS [TotalPoints]
*				, [dbo].fxMsAccountTotalPointsAllowed(MSA.AccountID) AS [TotalPointsAllowed]
*				, [dbo].fxMsAccountTotalPointsRep(MSA.AccountID) AS RepPoints
*		These are the functions that calculate that.

Select 'Calculate Points of Protection for each account'
USE NXSE_Sales
GO

DECLARE @CommissionPeriodID BIGINT
	, @CommissionPeriodEndDate DATE
	, @DEBUG_MODE VARCHAR(20) = 'OFF';

SELECT @DEBUG_MODE = GlobalPropertyValue FROM [dbo].[SC_GlobalProperties] WHERE (GlobalPropertyID = 'DEBUG_MODE');

SELECT 
	@CommissionPeriodID = CommissionPeriodID
	, @CommissionPeriodEndDate = CONVERT(DATE,MIN(CommissionPeriodEndDate))
FROM
	NXSE_Sales.dbo.SC_CommissionPeriods 
WHERE
	CommissionPeriodEndDate >= GETDATE()
GROUP BY
	CommissionPeriodID
*/
/********************  END HEADER ********************/
