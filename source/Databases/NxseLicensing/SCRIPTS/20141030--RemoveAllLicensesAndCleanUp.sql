USE [NXSE_Licensing]
GO

BEGIN TRANSACTION

--TRUNCATE TABLE dbo.LM_LicenseItems;
--DELETE FROM [dbo].[LM_Licenses];
--DBCC CHECKIDENT ('[dbo].[LM_Licenses]', RESEED, 500);

--UPDATE dbo.LM_Agencies SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE dbo.LM_Attachments SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE dbo.LM_LicenseItems SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE dbo.LM_Licenses SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE dbo.LM_Locations SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE [dbo].[LM_Notes] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE();
--UPDATE [dbo].[LM_RequirementItems] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();
--UPDATE [dbo].[LM_Requirements] SET CreatedByID = 'SYST001', CreatedbyDate = GETDATE(), ModifiedByID = 'SYST001', ModifiedByDate = GETDATE();


--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'jhill@pprotect.com', '[LicencingEmail]') WHERE (CallCenterMessage LIKE '%pprotect%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'licensing@pprotect.com', '[LicencingEmail1]') WHERE (CallCenterMessage LIKE '%pprotect%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'permits@pprotect.com', '[LicencingEmail2]') WHERE (CallCenterMessage LIKE '%pprotect%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'permits!pprotect.com', '[LicencingEmail2]') WHERE (CallCenterMessage LIKE '%pprotect%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'kpowell@nexsense.com', '[LicencingEmail]') WHERE (CallCenterMessage LIKE '%kpowell@nexsense.com%');

--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'Januari Hill', '[LicencingManagerName]') WHERE (CallCenterMessage LIKE '%Januari Hill%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, 'Kati Powell', '[LicencingManagerName]') WHERE (CallCenterMessage LIKE '%Kati Powell%');


--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '801.772.5896', '[LicensingOfficePhone]') WHERE (CallCenterMessage LIKE '%801.772.5896%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '801-772-5896', '[LicensingOfficePhone]') WHERE (CallCenterMessage LIKE '%801-772-5896%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '(801) 223-6575', '[LicensingOfficePhone]') WHERE (CallCenterMessage LIKE '%(801) 223-6575%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '801.223.6596', '[LicensingOfficePhone1]') WHERE (CallCenterMessage LIKE '%801.223.6596%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '(801)223-6596', '[LicensingOfficePhone1]') WHERE (CallCenterMessage LIKE '%(801)223-6596%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '801-223-6596', '[LicensingOfficePhone1]') WHERE (CallCenterMessage LIKE '%801-223-6596%');
--UPDATE [dbo].[LM_Requirements] SET CallCenterMessage = REPLACE(CallCenterMessage, '385.375.8171', '[LicensingOfficePhone]') WHERE (CallCenterMessage LIKE '%385.375.8171%');

--SELECT 
--	LMR.RequirementID
--	, LMR.CallCenterMessage
--	--, REPLACE(LMR.CallCenterMessage, 'jhill@pprotect.com', '[LicencingEmail]') AS CCMessage
--FROM
--	[dbo].[LM_Requirements] AS LMR WITH (NOLOCK)
--WHERE
--	(LMR.CallCenterMessage LIKE '%801-223-6596%');

ROLLBACK TRANSACTION