USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_InvoiceItems')
	BEGIN
		PRINT 'Dropping VIEW vwAE_InvoiceItems'
		DROP VIEW dbo.vwAE_InvoiceItems
	END
GO

PRINT 'Creating VIEW vwAE_InvoiceItems'
GO

/****** Object:  View [dbo].[vwAE_InvoiceItems]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_InvoiceItems.sql
**		Name: vwAE_InvoiceItems
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
**		Date: 01/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/21/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_InvoiceItems]
AS
	-- Enter Query here
	SELECT
		IVTM.InvoiceItemID
		, IVTM.InvoiceId
		, IVTM.AccountEquipmentId
		, IVTM.ItemId
		, IVTM.ProductBarcodeId
		, ITM.ItemSKU
		, ITM.ModelNumber
		, ITM.ItemDesc
		, IVTM.TaxOptionId
		, IVTM.Qty
		, IVTM.Cost
		, IVTM.RetailPrice
		, IVTM.PriceWithTax
		, IVTM.SystemPoints
		, IVTM.SalesmanId
		, IVTM.TechnicianId
		, IVTM.IsCustomerPaying
		, IVTM.IsActive
		, IVTM.IsDeleted
		, IVTM.ModifiedOn
		, IVTM.ModifiedBy
		, IVTM.CreatedOn
		, IVTM.CreatedBy
		, IVTM.DEX_ROW_TS
	FROM
		[dbo].AE_InvoiceItems AS IVTM WITH (NOLOCK)
		INNER JOIN [dbo].AE_Items AS ITM WITH (NOLOCK)
		ON
			(ITM.ItemID = IVTM.ItemId)
GO
/* TEST 
SELECT * FROM vwAE_InvoiceItems WHERE (InvoiceID = 10060465) AND (IsActive = 1) AND (IsDeleted = 0);
												   10020156) AND (ItemSku = 'GEC-TX4014012') */
