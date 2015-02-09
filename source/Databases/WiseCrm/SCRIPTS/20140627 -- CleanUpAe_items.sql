USE [WISE_CRM]
GO

--SELECT * FROM [dbo].[AE_Items];

BEGIN TRANSACTION

DECLARE @ItemID VARCHAR(50) = 'S911BRC-HC';
DECLARE @Invoices AS TABLE (InvoiceID BIGINT, InvoiceItemID BIGINT);

INSERT INTO @Invoices (InvoiceID, InvoiceItemID)
	SELECT DISTINCT AINV.InvoiceID, AIVIT.InvoiceItemID
	FROM
		[dbo].[AE_Invoices] AS AINV WITH (NOLOCK)
		INNER JOIN [dbo].[AE_InvoiceItems] AS AIVIT WITH (NOLOCK)
		ON
			(AINV.InvoiceID = AIVIT.InvoiceId)
	WHERE
		(AIVIT.ItemId = @ItemId);

--SELECT * FROM @Invoices;
--DELETE FROM [dbo].[AE_InvoiceItems] WHERE (InvoiceId = 10000003);
--DELETE FROM [dbo].[AE_Invoices] WHERE (InvoiceID = 10000003);
--UPDATE [dbo].[AE_InvoiceItems] SET ProductBarcodeId = NULL;
--UPDATE [dbo].[IE_ProductBarcodes] SET LastProductBarcodeTrackingId = NULL;
UPDATE [dbo].MS_Accounts SET PanelItemId = NULL WHERE (PanelItemId = @ItemID);
DELETE [dbo].[AE_ItemMostFrequents];
DELETE [dbo].[IE_ProductBarcodeTracking];
DELETE [dbo].[IE_ProductBarcodes];
DELETE [dbo].[IE_PurchaseOrderItems];
DELETE [dbo].[IE_PurchaseOrders];
DELETE FROM [dbo].[AE_InvoiceItems] WHERE (ItemId = @ItemID);
DELETE FROM [dbo].[MS_AccountZoneAssignments] WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM [dbo].[MS_AccountEquipment] WHERE (ItemId IN (SELECT ItemID FROM [dbo].[AE_Items] WHERE (ItemID = @ItemID)))));
DELETE FROM [dbo].[MS_AccountEquipment] WHERE (ItemId IN (SELECT ItemID FROM [dbo].[AE_Items] WHERE (ItemID = @ItemID)));
DELETE FROM [dbo].[AE_Items] WHERE (ItemID = @ItemID);

ROLLBACK TRANSACTION
