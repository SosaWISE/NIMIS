USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountEquipmentInfoToGP')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountEquipmentInfoToGP'
		DROP VIEW dbo.vwMS_AccountEquipmentInfoToGP
	END
GO

PRINT 'Creating VIEW vwMS_AccountEquipmentInfoToGP'
GO

/****** Object:  View [dbo].[vwMS_AccountEquipmentInfoToGP]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountEquipmentInfoToGP.sql
**		Name: vwMS_AccountEquipmentInfoToGP
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
**		Date: 02/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/17/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountEquipmentInfoToGP]
AS
	-- Enter Query here
	SELECT
		MCA.CustomerMasterFileId AS [CustomerMasterFileID]
		, MSA.AccountID
		, AEI.InvoiceID AS [Invoice ID]
		, AEIT.InvoiceTypeID AS [Invoice Type ID]
		, AEIT.InvoiceType AS [Invoice Type]
		, ITM.ItemSKU AS [Product Sku]
		, ITM.ModelNumber AS [Model Number]
		, AEITM.SalesmanId AS [Sales Rep ID]
		, AEITM.TechnicianId AS [Tech ID]
		, CASE
			WHEN AEITM.SalesmanId IS NULL AND AEITM.TechnicianId IS NULL THEN 1
			ELSE 0
		  END AS [Is Upgrade]
		, MSAE.IsExisting AS [Is Existing Equipment]
		, MSAEUT.AccountEquipmentUpgradeType AS [Rep Tech Cust Upgrade]  -- This shows who is paying for the upgrade.
	FROM
		[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
			(MSA.AccountID = MCA.AccountID)
		INNER JOIN [dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		ON
			(AEI.AccountId = MCA.AccountID)
--			AND (AEI.InvoiceTypeId = 'INSTALL')
		INNER JOIN [dbo].[AE_InvoiceTypes] AS AEIT WITH (NOLOCK)
		ON
			(AEIT.InvoiceTypeID = AEI.InvoiceTypeId)
		INNER JOIN [dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
		ON
			(MSAE.AccountId = MSA.AccountID)
			AND (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0)
		INNER JOIN [dbo].[MS_AccountEquipmentUpgradeTypes] AS MSAEUT WITH (NOLOCK)
		ON
			(MSAEUT.AccountEquipmentUpgradeTypeID = MSAE.AccountEquipmentUpgradeTypeId)
		INNER JOIN [dbo].[AE_InvoiceItems] AS AEITM WITH (NOLOCK)
		ON
			(AEITM.InvoiceId = AEI.InvoiceID)
			AND (AEITM.IsActive = 1 AND AEITM.IsDeleted = 0)
		INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
		ON
			(ITM.ItemID = AEITM.ItemId)
GO
/* TEST */
SELECT * FROM vwMS_AccountEquipmentInfoToGP
