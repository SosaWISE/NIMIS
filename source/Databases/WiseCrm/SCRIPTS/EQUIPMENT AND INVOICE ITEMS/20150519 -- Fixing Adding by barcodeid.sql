USE [WISE_CRM]
GO

DECLARE @AccountID BIGINT = 191265
	--, @BarcodeID VARCHAR(50) = '716489228'
	--, @InvoiceID BIGINT = 10060527
	--, @StartInvoiceItemID BIGINT = 10064509;

--SELECT * FROM dbo.MS_AccountEquipment WHERE (AccountID = @AccountID);
BEGIN TRANSACTION
SELECT * FROM dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (AccountID = @AccountID)));
DELETE dbo.MS_AccountZoneAssignments WHERE AccountZoneAssignmentID IN (
SELECT 
	m1.AccountZoneAssignmentID
	--, m1.AccountEquipmentId
	--, m1.ZONE
FROM
(
	SELECT
		MSAZA.AccountZoneAssignmentID
		, MSAZA.AccountEquipmentId
		, MSAZA.Zone
		, ROW_NUMBER() OVER (PARTITION BY MSAZA.AccountEquipmentId ORDER BY MSAZA.AccountZoneAssignmentID) AS ROWNUM
	FROM 
		dbo.MS_AccountZoneAssignments AS MSAZA WITH (NOLOCK)

	WHERE
		(MSAZA.AccountEquipmentId IN (SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (AccountID = @AccountID)))
) AS m1
WHERE
	m1.ROWNUM = 2
);
SELECT * FROM dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId IN (SELECT AccountEquipmentID FROM dbo.MS_AccountEquipment WHERE (AccountID = @AccountID)));
ROLLBACK TRANSACTION