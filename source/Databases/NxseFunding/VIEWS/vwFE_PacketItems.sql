USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_PacketItems')
	BEGIN
		PRINT 'Dropping VIEW vwFE_PacketItems'
		DROP VIEW dbo.vwFE_PacketItems
	END
GO

PRINT 'Creating VIEW vwFE_PacketItems'
GO

/****** Object:  View [dbo].[vwFE_PacketItems]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_PacketItems.sql
**		Name: vwFE_PacketItems
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
**		Date: 03/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/25/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_PacketItems]
AS
	-- Enter Query here
	SELECT
		FEPI.PacketItemID
		, FEPI.PacketId
		, CUST.CustomerMasterFileId AS [CustomerNumber]
		, AECA.CustomerId
		, FEPI.AccountId
		, CUST.FirstName
		, CUST.LastName
		, FEPI.ReturnAccountFundingStatusId
		, FEAFT.AccountFundingShortDesc
		, FEAFS.AccountStatusNote
		--, FEPI.IsDeleted
		, FEPI.ModifiedBy
		, FEPI.ModifiedOn
		, FEPI.CreatedBy
		, FEPI.CreatedOn
	FROM
		[dbo].[FE_PacketItems] AS FEPI WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Packets] AS FEP WITH (NOLOCK)
		ON
			(FEP.PacketID = FEPI.PacketId)
			AND (FEPI.IsDeleted = 0)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		ON
			(AECA.AccountId = FEPI.AccountId)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		ON
			(CUST.CustomerID = AECA.CustomerId)
		LEFT OUTER JOIN [dbo].[FE_AccountFundingStatus] AS FEAFS WITH (NOLOCK)
		ON
			(FEAFS.AccountFundingStatusID = FEPI.ReturnAccountFundingStatusId)
		LEFT OUTER JOIN [dbo].[FE_AccountFundingStatusTypes] AS FEAFT WITH (NOLOCK)
		ON
			(FEAFT.AccountFundingStatusTypeID = FEAFS.AccountFundingStatusTypeId)
GO
/* TEST */
-- SELECT * FROM vwFE_PacketItems
