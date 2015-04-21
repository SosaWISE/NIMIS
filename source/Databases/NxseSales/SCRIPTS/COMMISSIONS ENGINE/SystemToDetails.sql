USE [NXSE_Sales]
GO

DECLARE @CommPeriodID INT = 1;

SELECT
	*
FROM
	[dbo].[vwSC_WorkAccounts] AS SCWA WITH (NOLOCK)
WHERE
	(SCWA.CommissionPeriodId = @CommPeriodID);

SELECT
	SWAA.WorkAccountAdjustmentID AS ID
	, SWAA.WorkAccountId AS WAID
	, SWAA.CommissionsAdjustmentId AS [Adjustment ID]
	, SCCA.CommissionsAdjustmentDescription AS [Description]
	, SWAA.AdjustmentAmount AS [Adj Amount]
	, SWAA.CreatedOn AS [Created On]
FROM
	[dbo].[SC_WorkAccountAdjustments] AS SWAA WITH (NOLOCK)
	INNER JOIN [dbo].[vwSC_WorkAccounts] AS SCWA WITH (NOLOCK)
	ON
		(SCWA.WorkAccountID = SWAA.WorkAccountId)
	INNER JOIN [dbo].[SC_CommissionsAdjustments] AS SCCA WITH (NOLOCK)
	ON
		(SCCA.CommissionsAdjustmentID = SWAA.CommissionsAdjustmentId)
WHERE
	(SCWA.CommissionPeriodId = @CommPeriodID)
	AND (SWAA.AdjustmentAmount IS NOT NULL)
ORDER BY
	SWAA.WorkAccountId;
