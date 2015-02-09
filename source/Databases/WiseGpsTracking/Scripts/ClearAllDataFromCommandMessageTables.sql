USE [WISE_GPSTRACKING]
GO

BEGIN TRANSACTION

UPDATE dbo.LP_Requests SET CommandMessageId = NULL;
UPDATE dbo.LP_CommandMessages SET RequestId = NULL;

DELETE dbo.LP_Requests;
TRUNCATE TABLE dbo.LP_CommandMessageAVCFGFFs;
TRUNCATE TABLE dbo.LP_CommandMessageAVRMCs;
TRUNCATE TABLE dbo.LP_CommandMessageECHKs;
TRUNCATE TABLE dbo.LP_CommandMessageEAVACKs;
TRUNCATE TABLE dbo.LP_CommandMessageEAVRSP3s;
TRUNCATE TABLE dbo.LP_CommandMessageEAVRSP4s;

DELETE dbo.LP_CommandMessages;

ROLLBACK TRANSACTION

USE [WISE_LOGGING]
GO

BEGIN TRANSACTION

TRUNCATE TABLE dbo.LG_MessageStackFrames;
DELETE dbo.LG_Messages;

ROLLBACK TRANSACTION