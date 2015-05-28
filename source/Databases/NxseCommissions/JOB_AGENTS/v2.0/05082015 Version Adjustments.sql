USE NXSE_Commissions
GO

SELECT * FROM SC_CommissionBonus

SELECT * FROM SC_CommissionDeductions

SELECT * FROM [WISE_CRM].[dbo].[MS_AccountPackages]


/* CHANGES TO BE IMPLEMENTED FROM THE Latest DSR/DSL agreements (Assumption: using the same version and making the necessary adjustments
	STEP 4 OR 5 UPDATE:
		Need to update the team deduction calculations to remove the Team Lowered RMR Deduction and Add the Team Lowered Contract Length

BEGIN TRANSACTION

--RMR OUTSIDE THE RANGE IS NO LONGER ALLOWED
UPDATE SC_CommissionDeductions
	SET IsActive = 0
	WHERE CommissionDeductionID IN ('RMRLOWOUTRANGE', 'RMRUPPOUTRANGE', 'TEAMLOWRMR')

--INSERT INTO SC_CommissionDeductions (CommissionDeductionID, CommissionEngineId, DeductionDescription, DeductionAmount, IsTeamDeduction, IsActive, IsDeleted)
--VALUES ('TEAMLOWCONLEN','SCv2.0', 'Lowered Contract Length to 36 Months', 20.00, 1, 1, 0)


UPDATE [WISE_CRM].[dbo].[MS_AccountPackages]
	SET IsActive = 0
	WHERE AccountPackageID IN (1, 2, 3)

INSERT INTO [WISE_CRM].[dbo].[MS_AccountPackages] (AccountPackageName, ShortName, Description, BasePoints, BaseRMR, MinRMR, MaxRMR)
VALUES ('Cellular / Interactive', 'Cell Inter', 'Cellular Interactive', 8, 48.99, 34.99, 59.99),
	('Energy Sense', 'Energy Sense', 'Energy Sense', 11.5, 52.49, 45.99, 64.99),
	('Security Sense', 'Security Sense', 'Security Sense', 16, 56.99, 45.99, 64.99)

ROLLBACK TRANSACTION

*/