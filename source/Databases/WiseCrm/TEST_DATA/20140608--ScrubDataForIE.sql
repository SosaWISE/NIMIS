USE [WISE_CRM]
GO

--SELECT * FROM IE_PurchaseOrders;
SELECT * FROM IE_PurchaseOrderItems;
--SELECT * FROM IE_ProductBarcodes;
--SELECT * FROM IE_ProductBarcodeTracking;

BEGIN TRANSACTION

UPDATE IE_PurchaseOrders SET IsActive = 1, IsDeleted = 0;
UPDATE IE_ProductBarcodes SET IsDeleted = 1;
UPDATE IE_ProductBarcodeTracking SET IsDeleted = 1;

ROLLBACK TRANSACTION