USE NXSE_Commissions
GO

/*
WHAT DIFFERENT THINGS DO  WE NEED TO EXTRACT FROM ONE ACCOUNTS WORKACCOUNTADJUSTMENTS
	1. Sales Rep Commissions (using the following columns)
		CommissionRateScaleId
		CommissionDeductionId
		CommissionBonusId (but not the recruiting bonus)
	2. Recruiting Bonus
		Paid to the Recruiter for the 1st or 3rd sale
	3. Office/Manager Overrides
		Paid to the reps manager for weekly volume
		Deducted from the reps manager for various penalties
*/

/*
	1. Sales Rep Commissions (using the following columns)
		CommissionRateScaleId
		CommissionDeductionId
		CommissionBonusId (but not the recruiting bonus)
*/
SELECT scwaa.WorkAccountId 
	, scwaa.CommissionRateScaleId
	, scwaa.CommissionBonusId
	, scwaa.CommissionDeductionId
	--, scwaa.CommissionTeamOfficeOverrideScaleId
	--, scwaa.CommissionTeamOfficeAnnualResidualIncentiveId
	, scwaa.AdjustmentAmount
	FROM SC_WorkAccountAdjustments AS scwaa
	WHERE (scwaa.CommissionBonusId NOT IN ('RECSIGNBONUSFIRST', 'RECSIGNBONUSSECN') OR (scwaa.CommissionBonusId IS NULL))
		AND (scwaa.CommissionDeductionId NOT LIKE ('%TEAM%') or (scwaa.CommissionDeductionId IS NULL))
		AND (scwaa.CommissionTeamOfficeAnnualResidualIncentiveId IS NULL)
		AND (scwaa.CommissionTeamOfficeOverrideScaleId IS NULL)
		AND (scwaa.CommissionPeriodId = 4)
	ORDER BY scwaa.WorkAccountId, scwaa.CommissionRateScaleId DESC, scwaa.CommissionBonusId DESC, scwaa.CommissionDeductionId DESC

/*
	2. Recruiting Bonus
		Paid to the Recruiter for the 1st or 3rd sale
*/
SELECT scwaa.WorkAccountId 
	--, scwaa.CommissionRateScaleId
	, scwaa.CommissionBonusId
	--, scwaa.CommissionDeductionId
	--, scwaa.CommissionTeamOfficeOverrideScaleId
	--, scwaa.CommissionTeamOfficeAnnualResidualIncentiveId
	, scwaa.AdjustmentAmount
	FROM SC_WorkAccountAdjustments AS scwaa
	WHERE scwaa.CommissionBonusId IN ('RECSIGNBONUSFIRST', 'RECSIGNBONUSSECN')
			AND (scwaa.CommissionPeriodId = 4)
	ORDER BY scwaa.WorkAccountId, scwaa.CommissionBonusId DESC

/*
	3. Office/Manager Overrides
		Paid to the reps manager for weekly volume
		Deducted from the reps manager for various penalties
*/
SELECT scwaa.WorkAccountId 
	--, scwaa.CommissionRateScaleId
	--, scwaa.CommissionBonusId
	, scwaa.CommissionDeductionId
	, scwaa.CommissionTeamOfficeOverrideScaleId
	, scwaa.CommissionTeamOfficeAnnualResidualIncentiveId
	, scwaa.AdjustmentAmount
	FROM SC_WorkAccountAdjustments AS scwaa
	WHERE (scwaa.CommissionDeductionId LIKE ('%TEAM%'))
		OR ((scwaa.CommissionTeamOfficeAnnualResidualIncentiveId IS NOT NULL) OR (scwaa.CommissionTeamOfficeOverrideScaleId IS NOT NULL))
		--OR (scwaa.CommissionTeamOfficeOverrideScaleId IS NOT NULL)
		--AND (scwaa.CommissionDeductionId LIKE ('%TEAM%') OR (scwaa.CommissionDeductionId IS NULL))
		AND (scwaa.CommissionPeriodId = 4)
	ORDER BY scwaa.WorkAccountId, scwaa.CommissionDeductionId DESC, scwaa.CommissionTeamOfficeOverrideScaleId DESC, scwaa.CommissionTeamOfficeAnnualResidualIncentiveId DESC

