USE [WISE_CRM]
GO

DECLARE @AccountID BIGINT = 191262
	, @BarcodeID VARCHAR(50) = '716489228'
	, @InvoiceID BIGINT = 10060527
	, @StartInvoiceItemID BIGINT = 10064509;


--SELECT 
--	AEII.* 
--FROM
--	dbo.AE_Invoices AS AEI WITH (NOLOCK)
--	INNER JOIN dbo.AE_InvoiceItems AS AEII WITH (NOLOCK)
--	ON
--		(AEII.InvoiceId = AEI.InvoiceID)
--		AND (AEI.AccountId = @AccountID)

--SELECT * FROM dbo.MS_AccountEquipment WHERE AccountID = @AccountID;

BEGIN TRANSACTION

DECLARE @AcctEquip TABLE (AccountEquipmentID BIGINT);
INSERT INTO @AcctEquip ( AccountEquipmentID )
	SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (InvoiceItemId IN (SELECT InvoiceItemID FROM dbo.AE_InvoiceItems WHERE (InvoiceId = @InvoiceID AND InvoiceItemID >= @StartInvoiceItemID)));
UPDATE dbo.MS_AccountEquipment SET InvoiceItemId = NULL WHERE (InvoiceItemId IN (SELECT InvoiceItemID FROM dbo.AE_InvoiceItems WHERE (InvoiceId = @InvoiceID AND InvoiceItemID >= @StartInvoiceItemID)));
UPDATE dbo.AE_InvoiceItems SET AccountEquipmentId = NULL WHERE (InvoiceItemId IN (SELECT InvoiceItemID FROM dbo.AE_InvoiceItems WHERE (InvoiceId = @InvoiceID AND InvoiceItemID >= @StartInvoiceItemID)));
DELETE dbo.AE_InvoiceItems WHERE (InvoiceId = @InvoiceID AND InvoiceItemID >= @StartInvoiceItemID)

DELETE dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM @AcctEquip));
DELETE dbo.MS_AccountEquipment WHERE (AccountEquipmentID IN (SELECT AccountEquipmentID FROM @AcctEquip));

ROLLBACK TRANSACTION