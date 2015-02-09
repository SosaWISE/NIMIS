USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_ProductBarcodeItem')
	BEGIN
		PRINT 'Dropping VIEW vwIE_ProductBarcodeItem'
		DROP VIEW dbo.vwIE_ProductBarcodeItem
	END
GO

PRINT 'Creating VIEW vwIE_ProductBarcodeItem'
GO

/****** Object:  View [dbo].[vwIE_ProductBarcodeItem]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_ProductBarcodeItem.sql
**		Name: vwIE_ProductBarcodeItem
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
**		Date: 05/27/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/25/2014	reagan		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_ProductBarcodeItem]
AS
	-- Enter Query here
	SELECT
		IEP.[ProductBarcodeId]
		,AEI.[ItemDesc]
	FROM
	[dbo].[IE_ProductBarcodes] AS IEP WITH (NOLOCK)
	INNER JOIN
	[dbo].[IE_PurchaseOrderItems] AS IEPOI WITH (NOLOCK)
	ON
	IEPOI.[PurchaseOrderItemID] = IEP.[PurchaseOrderItemId]
	INNER JOIN
	[dbo].[AE_Items] AS AEI
	ON
	AEI.[ItemID] = IEPOI.[ItemId]


GO
/* TEST */
-- SELECT * FROM vwIE_ProductBarcodeLocation where [LocationID] = 100154
