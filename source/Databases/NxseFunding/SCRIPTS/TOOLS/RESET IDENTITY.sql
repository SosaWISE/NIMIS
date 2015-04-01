USE [NXSE_Funding]
GO

--DELETE dbo.FE_Bundles
--DBCC CHECKIDENT ('[dbo].[FE_Bundles]', RESEED, 500100);

--DELETE dbo.FE_AccountFundingStatus
--DBCC CHECKIDENT ('[dbo].[FE_AccountFundingStatus]', RESEED, 0);

--DELETE [dbo].[FE_Criterias]
DBCC CHECKIDENT ('[dbo].[FE_Criterias]', RESEED, 1);

DELETE [dbo].[FE_RejectedAccounts]
DBCC CHECKIDENT ('[dbo].[FE_RejectedAccounts]', RESEED, 0);