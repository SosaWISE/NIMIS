USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountClients')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountClients'
		DROP VIEW dbo.vwMS_AccountClients
	END
GO

PRINT 'Creating VIEW vwMS_AccountClients'
GO

/****** Object:  View [dbo].[vwMS_AccountClients]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountClients.sql
**		Name: vwMS_AccountClients
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 08/02/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/02/2012	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountClients]
AS

	/** Query */
	SELECT
		ACM.CustomerMasterFileId
		, ACM.CustomerID
		, ACA.AccountId
		--, CASE 
		--	WHEN MCA.AccountName IS NULL THEN dbo.fxGetDeviceName(MSA.SystemTypeId, MSA.PanelTypeId)
		--	ELSE MCA.AccountName
		--  END AS [AccountName]
		, [dbo].fxGetDeviceName(MSA.SystemTypeId, MSA.PanelTypeId, MCA.AccountName) [AccountName]
		, MCA.AccountDesc
		, GEN.EventID
		, GEN.EventDate
		, GEN.Lattitude AS [LastLatt]
		, GEN.Longitude AS [LastLong]
		, MAP.UIName
--		, MSA.GpsWatchUnitID
		, ACM.[Username]
		, ACM.[Password]
		, ACM.CustomerTypeId
		, MSA.SystemTypeId
		, MSA.PanelTypeId
--		, MSA.InvItemId
		, MSA.IndustryAccountId
		, IAN.IndustryAccount
		, IAN.Designator
		, IAN.SubscriberNumber
		, GetDate() AS [LastEvent]
	FROM
		dbo.MS_Accounts AS MSA WITH (NOLOCK)
		INNER JOIN dbo.MC_Accounts AS MCA WITH (NOLOCK)
		ON
			(MSA.AccountID = MCA.AccountID)
		INNER JOIN dbo.MS_AccountPanelTypes AS MAP WITH (NOLOCK)
		ON
			(MSA.PanelTypeId = MAP.PanelTypeID)
		INNER JOIN dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK)
		ON
			(MCA.AccountID = ACA.AccountId)
		INNER JOIN dbo.AE_Customers AS ACM WITH (NOLOCK)
		ON
			(ACA.CustomerId = ACM.CustomerID)
		INNER JOIN dbo.AE_CustomerMasterFiles AS ACMF WITH (NOLOCK)
		ON
			(ACM.CustomerMasterFileId = ACMF.CustomerMasterFileID)
		INNER JOIN [dbo].[vwMS_IndustryAccountNumbers] AS IAN
		ON
			(MSA.IndustryAccountId = IAN.IndustryAccountID)
		LEFT OUTER JOIN (
			SELECT * FROM [WISE_GPSTRACKING].[dbo].[vwGS_EventsLast]
		) AS GEN
		ON
			(MCA.AccountID = GEN.AccountId)

GO
/* TEST */
--SELECT * FROM vwMS_AccountClients