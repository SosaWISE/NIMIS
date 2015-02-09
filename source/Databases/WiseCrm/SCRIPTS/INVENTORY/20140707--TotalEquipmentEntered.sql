USE [WISE_CRM]
GO

/** Calculate the total and shortfall of equipment entered. */
SELECT
	POI.PurchaseOrderItemID
	, INV.ItemSKU
	, INV.ItemDesc
	, POI.Quantity
	, INV.Count AS [Count]
	, POI.Quantity - INV.Count AS [Short Fall]
FROM
	[dbo].IE_PurchaseOrderItems AS POI WITH (NOLOCK)
	INNER JOIN (
		SELECT
			ITM.ItemID
			, ITM.ItemSKU
			, ITM.ItemDesc
			, COUNT(*) AS [Count]
		FROM
			[dbo].[IE_ProductBarcodes] AS PB WITH (NOLOCK)
			INNER JOIN [dbo].[IE_PurchaseOrderItems] AS POI WITH (NOLOCK)
			ON
				(POI.PurchaseOrderItemID = PB.PurchaseOrderItemId)
			INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			ON
				(ITM.ItemID = POI.ItemId)
		GROUP BY
			ITM.ItemID
			, ITM.ItemSKU
			, ITM.ItemDesc
		) AS INV
	ON
		(INV.ItemID = POI.ItemId)

/** Show all barcodes for a specific SKU. */

SELECT 
	'''' + PB.ProductBarcodeID AS [Barcode #]
	, PBT.ProductBarcodeTrackingTypeId
	, PBT.LocationTypeID
	, PBT.LocationID
FROM
	[dbo].[IE_ProductBarcodes] AS PB WITH (NOLOCK)
	INNER JOIN [dbo].[IE_PurchaseOrderItems] AS POI WITH (NOLOCK)
	ON
		(POI.PurchaseOrderItemID = PB.PurchaseOrderItemId)
	INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
	ON
		(ITM.ItemID = POI.ItemId)
	INNER JOIN [dbo].[IE_ProductBarcodeTracking] AS PBT WITH (NOLOCK)
	ON
		(PBT.ProductBarcodeTrackingID = PB.LastProductBarcodeTrackingId)
WHERE
	(ITM.ItemSKU = 'PD300Z-2');


SELECT 
	LEN(PB.ProductBarCodeID) AS LEN
	, '''' + PB.ProductBarcodeID AS [Barcode #]

FROM 
	[dbo].[IE_ProductBarcodes] AS PB WITH (NOLOCK)
WHERE
	(LEN(PB.ProductBarCodeID) <> 9);