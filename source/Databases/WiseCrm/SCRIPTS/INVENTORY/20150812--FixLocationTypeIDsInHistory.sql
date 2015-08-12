/***************************************************************************************************************************
* Fix Location Types in the Barcode History
* Date: 08/12/2015
***************************************************************************************************************************/
USE [WISE_CRM]
GO

BEGIN TRANSACTION
--SELECT * FROM dbo.IE_ProductBarcodeTracking WHERE ProductBarcodeTrackingID = 37489

/** TECHNICIANS */
UPDATE PBT SET
	PBT.LocationTypeID = 'Technician'
--SELECT
--	PBT.*
FROM
	[dbo].[IE_ProductBarcodeTracking] AS PBT WITH (NOLOCK)
WHERE
	(PBT.LocationTypeId IS NULL)
	AND (LTRIM(RTRIM(PBT.LocationId)) IN (SELECT LTRIM(RTRIM(UserID)) FROM [WISE_HumanResource].[dbo].[RU_Users]));

/** Vendors */
UPDATE PBT SET
	PBT.LocationTypeID = 'Returned'
--SELECT
--	PBT.*
FROM
	[dbo].[IE_ProductBarcodeTracking] AS PBT WITH (NOLOCK)
WHERE
	(PBT.LocationTypeId IS NULL)
	AND (LTRIM(RTRIM(PBT.LocationId)) IN (SELECT LTRIM(RTRIM(VendorID)) FROM [dbo].[IE_Vendors]));

/** Sold to Customers */
UPDATE PBT SET
	PBT.LocationTypeID = 'SOLD'
--SELECT
--	PBT.*
FROM
	[dbo].[IE_ProductBarcodeTracking] AS PBT WITH (NOLOCK)
WHERE
	(PBT.LocationTypeId IS NULL)
	AND (LTRIM(RTRIM(PBT.LocationId)) IN (SELECT LTRIM(RTRIM(CustomerID)) FROM [dbo].[AE_Customers]));

/** Warehouses */
UPDATE PBT SET
	PBT.LocationTypeID = 'Transfer' -- or Received
--SELECT
--	PBT.*
FROM
	[dbo].[IE_ProductBarcodeTracking] AS PBT WITH (NOLOCK)
WHERE
	(PBT.LocationTypeId IS NULL)
	AND (LTRIM(RTRIM(PBT.LocationId)) IN (SELECT LTRIM(RTRIM(WarehouseSiteID)) FROM [dbo].[IE_WarehouseSites]));

ROLLBACK TRANSACTION
