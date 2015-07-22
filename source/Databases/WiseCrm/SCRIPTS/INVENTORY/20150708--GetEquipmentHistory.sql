USE [WISE_CRM]
GO

DECLARE @ProductBarcodeId VARCHAR(50) ='716505230';

SELECT 
	PBT.ProductBarcodeTrackingID
	, PBT.ProductBarcodeTrackingTypeId
	, PBTT.ProductBarcodeTrackingTypeName
	, PBT.ProductBarcodeId
	, PBT.LocationTypeID
	, LT.LocationTypeName
	, PBT.LocationID
	, PBT.Comment
	, PBT.IsDeleted
	--, PBT.ModifiedOn
	--, PBT.ModifiedBy
	--, PBT.CreatedOn
	--, PBT.CreatedBy
	--, PBT.DEX_ROW_TS
FROM
	dbo.IE_ProductBarcodeTracking AS PBT WITH (NOLOCK)
	INNER JOIN dbo.IE_ProductBarcodeTrackingTypes AS PBTT WITH (NOLOCK)
	ON
		(PBTT.ProductBarcodeTrackingTypeID = PBT.ProductBarcodeTrackingTypeId)
	LEFT OUTER JOIN dbo.IE_LocationTypes AS LT WITH (NOLOCK)
	ON
		(LT.LocationTypeID = PBT.LocationTypeID)
WHERE
--	(PBT.ProductBarcodeId = @ProductBarcodeId)
	(PBT.ProductBarcodeId IN ('716467230','716505230','716500217','716474429','716495074','716521388','716494155'))
ORDER BY
	PBT.ProductBarcodeTrackingID DESC;