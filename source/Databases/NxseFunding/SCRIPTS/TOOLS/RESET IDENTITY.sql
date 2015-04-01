USE [NXSE_Funding]
GO

--DELETE dbo.FE_Bundles
--DBCC CHECKIDENT ('[dbo].[FE_Bundles]', RESEED, 500100);

--DELETE dbo.FE_AccountFundingStatus
--DBCC CHECKIDENT ('[dbo].[FE_AccountFundingStatus]', RESEED, 0);

--DELETE [dbo].[FE_Criterias]
DBCC CHECKIDENT ('[dbo].[FE_Criterias]', RESEED, 1);