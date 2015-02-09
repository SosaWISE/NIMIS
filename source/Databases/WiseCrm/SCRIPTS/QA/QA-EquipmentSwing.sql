USE [Platinum_Protection_InterimCRM]
GO


--SELECT DISTINCT TOP 20
--	--AccountInventoryID
--	AccountID
--	, COUNT(*) AS [Equip Count]
--FROM
--	[dbo].[MS_AccountInventory]
--WHERE
--	(AccountID NOT IN (SELECT InterimAccountID FROM [WISE_CRM].[dbo].[MS_AccountSwungInfo]))
--GROUP BY
--	AccountID
--ORDER BY
--	-- AccountInventoryID DESC
--	AccountID DESC; 

----SELECT InterimAccountID FROM [WISE_CRM].[dbo].[MS_AccountSwungInfo]

SELECT 
	--ROW_NUMBER()
	AI.AccountInventoryID
	, AI.AccountID
	, AZA.Zone
	, EQ.ItemDescription AS [Equipment]
	, EQL.EquipmentLocationDesc AS Location
	, EQ.GenDescription
	, EQ.GPItemNmbr
FROM 
	[dbo].[MS_AccountInventory] AS AI WITH (NOLOCK)
	INNER JOIN [dbo].[MS_Equipment] AS EQ WITH (NOLOCK)
	ON
		(EQ.EquipmentID = AI.EquipmentID)
	LEFT OUTER JOIN [dbo].[MS_EquipmentLocation] AS EQL WITH (NOLOCK)
	ON
		(EQL.EquipmentLocationId = AI.EquipmentLocationId)
	LEFT OUTER JOIN [dbo].[MS_AccountZoneAssignment] AS AZA WITH (NOLOCK)
	ON
		(AZA.AccountInventoryID = AI.AccountInventoryID)
WHERE
	(AccountID = 583600);