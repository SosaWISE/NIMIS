USE [NXSE_Licensing]
GO

TRUNCATE TABLE dbo.LM_LicenseItems;
DELETE FROM [dbo].[LM_Licenses];
DBCC CHECKIDENT ('[dbo].[LM_Licenses]', RESEED, 500);
