USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_ProductBarcodeLocation')
	BEGIN
		PRINT 'Dropping VIEW vwIE_ProductBarcodeLocation'
		DROP VIEW dbo.vwIE_ProductBarcodeLocation
	END
GO

PRINT 'Creating VIEW vwIE_ProductBarcodeLocation'
GO

/****** Object:  View [dbo].[vwIE_ProductBarcodeLocation]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_ProductBarcodeLocation.sql
**		Name: vwIE_ProductBarcodeLocation
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
**	06/25/2014	reagan			Created by
**	01/21/2015	Peter			Added ItemSKU to Template & the GROUP BY
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_ProductBarcodeLocation]
AS
	-- Enter Query here
	SELECT
		IEPT.[ProductBarcodeId]
		,AEI.[ItemSKU]
		,AEI.[ItemDesc]
		,IEPT.[LocationID]
	FROM
	[dbo].[IE_ProductBarcodeTracking] AS IEPT WITH (NOLOCK)
	INNER JOIN
	[dbo].[IE_ProductBarcodes] AS IEP WITH (NOLOCK)
	ON 
	IEPT.[ProductBarcodeTrackingID] = IEP.[LastProductBarcodeTrackingId] AND IEPT.[ProductBarcodeId] = IEP.[ProductBarcodeID]
	--IEPT.[ProductBarcodeTrackingID] = IEP.[LastProductBarcodeTrackingId] --AND IEPT.[ProductBarcodeId] = IEP.[ProductBarcodeID]

	INNER JOIN
	[dbo].[IE_PurchaseOrderItems] AS IEPOI WITH (NOLOCK)
	ON
	IEPOI.[PurchaseOrderItemID] = IEP.[PurchaseOrderItemId]
	INNER JOIN
	[dbo].[AE_Items] AS AEI
	ON
	AEI.[ItemID] = IEPOI.[ItemId]
	GROUP BY AEI.[ItemSKU], AEI.[ItemDesc], IEPT.[ProductBarcodeId],  IEPT.[LocationID]
		


GO
/* TEST */
-- SELECT * FROM vwIE_ProductBarcodeLocation where [LocationID] = '100154'
