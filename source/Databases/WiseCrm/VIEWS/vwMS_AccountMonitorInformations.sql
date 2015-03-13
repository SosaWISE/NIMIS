USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountMonitorInformations')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountMonitorInformations'
		DROP VIEW dbo.vwMS_AccountMonitorInformations
	END
GO

PRINT 'Creating VIEW vwMS_AccountMonitorInformations'
GO

/****** Object:  View [dbo].[vwMS_AccountMonitorInformations]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountMonitorInformations.sql
**		Name: vwMS_AccountMonitorInformations
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
**		Date: 06/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/24/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountMonitorInformations]
AS
	-- Enter Query here
	SELECT
		MSA.AccountID
		, MSA.IndustryAccountId
		, IND.Csid
		, IND.ReceiverLineId
		, RLN.MonitoringStationOSId
		, MSA.IndustryAccount2Id
		, IN2.Csid AS [Csid2]
		, IN2.ReceiverLineId AS [ReceiverLine2Id]
		, MASI.TechId
		, [WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(MASI.TechId) AS TechFullName
		, LDS.SalesRepId
		, [WISE_HumanResource].[dbo].fxRU_UsersGetFullnameByGPEmployeeID(LDS.SalesRepId) AS SalesFullName
		, MSA.SystemTypeId
		, MSA.CellularTypeId
		, MSA.PanelTypeId
		, MSA.DslSeizureId
		, MSA.PanelItemId
		, MSA.CellPackageItemId
		, MSA.ContractId
		, MSA.AccountPassword
	FROM
		[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
		ON
			(MASI.AccountID = MSA.AccountID)
		LEFT OUTER JOIN [dbo].[MS_IndustryAccounts] AS IND WITH (NOLOCK)
		ON
			(IND.IndustryAccountID = MSA.IndustryAccountId)
		LEFT OUTER JOIN [dbo].[MS_ReceiverLines] AS RLN WITH (NOLOCK)
		ON
			(RLN.ReceiverLineID = IND.ReceiverLineId)
		LEFT OUTER JOIN [dbo].[MS_IndustryAccounts] AS IN2 WITH (NOLOCK)
		ON
			(IN2.IndustryAccountID = MSA.IndustryAccount2Id)
		INNER JOIN [dbo].[AE_CustomerAccounts] AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountId = MSA.AccountID)
			AND (MAC.CustomerTypeId IN ('MONI', 'PRI'))
		INNER JOIN [dbo].[QL_Leads] AS LDS WITH (NOLOCK)
		ON
			(LDS.LeadID = MAC.LeadId)
GO
/* TEST 
SELECT * FROM vwMS_AccountMonitorInformations WHERE AccountID = 130532;
*/
EXEC sp_refreshview N'[dbo].[vwMS_AccountMonitorInformations]'