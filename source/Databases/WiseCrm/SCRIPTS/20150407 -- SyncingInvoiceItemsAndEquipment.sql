USE [WISE_CRM]
GO

--SELECT * FROM [dbo].[AE_ItemTypes]
--SELECT * FROM [dbo].[AE_Items]
SELECT
	IVIT.*
FROM
	[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
	INNER JOIN [dbo].[AE_InvoiceItems] AS IVIT WITH (NOLOCK)
	ON
		(IVIT.InvoiceId = AEI.InvoiceID)
		AND (AEI.AccountId = 150927)

DECLARE @AccountID BIGINT;
DECLARE accountCursor CURSOR FOR
SELECT AccountID FROM MS_Accounts WHERE IsDeleted = 0 AND IsActive = 1;

OPEN accountCursor;

FETCH NEXT FROM accountCursor
INTO @AccountID;

WHILE(@@FETCH_STATUS = 0)
BEGIN
	/** PRINT Message*/
	PRINT 'SYNCING AccountID ' + CAST(@AccountID AS VARCHAR);

	/** Execute the Sync SPROC */
	EXEC dbo.custAE_InventoryItemsSycnWithMsAccountEquipmentInstalled @AccountID, 'STAKE001'

	/** Move to next item */
	FETCH NEXT FROM accountCursor
	INTO @AccountID;
END

CLOSE accountCursor;
DEALLOCATE accountCursor;
