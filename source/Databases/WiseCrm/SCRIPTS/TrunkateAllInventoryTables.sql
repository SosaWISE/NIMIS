USE WISE_CRM
GO

--SELECT * FROM [dbo].[IE_ProductBarcodes];
--SELECT * FROM [dbo].[IE_PurchaseOrders];
--SELECT * FROM [dbo].[IE_PurchaseOrderItems];
--SELECT * FROM [dbo].[AE_Items];

BEGIN TRANSACTION

TRUNCATE TABLE [dbo].[IE_PackingSlipItems];
DELETE [dbo].[IE_PackingSlips];

UPDATE [dbo].[IE_ProductBarcodes] SET LastProductBarcodeTrackingId = NULL WHERE LastProductBarcodeTrackingId IS NOT NULL;
DELETE dbo.IE_ProductBarcodeTracking;
UPDATE [dbo].[AE_InvoiceItems] SET ProductBarcodeId = NULL WHERE ProductBarcodeId IS NOT NULL;
DELETE [dbo].[IE_ProductBarcodes];
DELETE [dbo].[IE_PurchaseOrderItems];
DELETE [dbo].[IE_PurchaseOrders];

--DELETE [dbo].[IE_WarehouseSites];
--DELETE [dbo].[IE_Vendors];

ROLLBACK TRANSACTION
