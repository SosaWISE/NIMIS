USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountAndLeadInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountAndLeadInfo'
		DROP VIEW dbo.vwMS_AccountAndLeadInfo
	END
GO

PRINT 'Creating VIEW vwMS_AccountAndLeadInfo'
GO

/****** Object:  View [dbo].[vwMS_AccountAndLeadInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountAndLeadInfo.sql
**		Name: vwMS_AccountAndLeadInfo
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
**		Date: 08/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/18/2012	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountAndLeadInfo]
AS

	/** Query */
	SELECT
		MAC.AccountID
		, CUST.LeadId
		, CUST.CustomerId
		, CUST.CustomerMasterFileId
		, MAS.IndustryAccountId
		, MAS.SystemTypeId
		, MAS.CellularTypeId
		, MAS.PanelTypeId
		, MAS.PanelItemId
		, MAS.CellPackageItemId
		, MAS.ContractId
	FROM
		[dbo].[MC_Accounts] AS MAC WITH (NOLOCK)
		INNER JOIN [dbo].[MS_Accounts] AS MAS WITH (NOLOCK)
		ON
			(MAS.AccountID = MAC.AccountID)
		INNER JOIN [dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		ON
			(CUST.CustomerID = MAC.ShipContactId)
GO
/* TEST */
-- SELECT * FROM vwMS_AccountAndLeadInfo WHERE AccountID = 100218
