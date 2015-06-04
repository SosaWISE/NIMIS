USE [NXSE_Commissions]
GO

DECLARE @CommissionPeriodID BIGINT = 8;

SELECT * FROM dbo.SC_WorkAccounts WHERE (CommissionPeriodId = @CommissionPeriodID);
SELECT 
	SCWA.CustomerMasterFileId AS [Customer #]
	, SCWA.AccountID
	, SCWAA.WorkAccountAdjustmentID AS WAAID
	, SCWA.WorkAccountID
	, SCWA.SalesRepId
	, SCWA.RecByRepId
	, SCWA.ManSalesRepId
	, CASE 
		WHEN SCWAA.CommissionDeductionId IS NOT NULL THEN (SELECT 'DE:' + DeductionDescription FROM dbo.SC_CommissionDeductions WHERE (CommissionDeductionID = SCWAA.CommissionDeductionId))
		WHEN SCWAA.CommissionRateScaleId IS NOT NULL THEN (SELECT RateScaleDescription FROM dbo.SC_CommissionRateScales WHERE (CommissionRateScaleID = SCWAA.CommissionRateScaleId))
		WHEN SCWAA.CommissionBonusId IS NOT NULL THEN (SELECT BonusDescription FROM dbo.SC_CommissionBonus WHERE (CommissionBonusID = SCWAA.CommissionBonusId))
		WHEN SCWAA.CommissionTeamOfficeOverrideScaleId IS NOT NULL THEN (SELECT ScaleDescription FROM dbo.SC_CommissionTeamOfficeOverrideScales WHERE (CommissionTeamOfficeOverrideScaleID = SCWAA.CommissionTeamOfficeOverrideScaleId))
		ELSE '[?NOT SURE?]'
	  END AS [Desc]
	, SCWAA.AdjustmentAmount
FROM
	dbo.SC_WorkAccountAdjustments AS SCWAA WITH (NOLOCK)
	INNER JOIN dbo.SC_WorkAccounts AS SCWA WITH (NOLOCK)
	ON
		(SCWA.WorkAccountID = SCWAA.WorkAccountId)
		AND (SCWA.CommissionPeriodId = SCWAA.CommissionPeriodId)
		AND (SCWAA.CommissionPeriodId = @CommissionPeriodId)
ORDER BY
	SCWA.CustomerMasterFileId;
