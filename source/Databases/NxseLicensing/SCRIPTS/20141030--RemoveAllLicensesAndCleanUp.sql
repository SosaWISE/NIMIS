USE [NXSE_Licensing]
GO

BEGIN TRANSACTION

TRUNCATE TABLE dbo.LM_LicenseItems;
DELETE FROM dbo.LM_Licenses;

UPDATE dbo.LM_Agencies SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE dbo.LM_Attachments SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE dbo.LM_LicenseItems SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE dbo.LM_Licenses SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE dbo.LM_Locations SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE [dbo].[LM_Notes] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE();
UPDATE [dbo].[LM_RequirementItems] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
UPDATE [dbo].[LM_Requirements] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();

ROLLBACK TRANSACTION