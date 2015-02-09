USE [Platinum_Protection_InterimCRM]
GO

SELECT * FROM [dbo].[MS_EquipmentType];
SELECT * FROM [dbo].[MS_EquipmentPanelType];

SELECT * FROM [dbo].[MS_Equipment] WHERE EquipmentTypeID IS NOT NULL;

SELECT 
	EQ.EquipmentTypeID
	, EQT.EquipmentType
	, COUNT(*)
FROM
	[dbo].[MS_Equipment] AS EQ WITH (NOLOCK)
	INNER JOIN [dbo].[MS_EquipmentType] AS EQT WITH (NOLOCK)
	ON
		(EQT.EquipmentTypeID = EQ.EquipmentTypeID)
GROUP BY
	EQ.EquipmentTypeID
	, EQT.EquipmentType;

SELECT 
	EQ.EquipmentTypeID
	, EQT.EquipmentType
	, COUNT(*)
FROM
	[dbo].[MS_Equipment] AS EQ WITH (NOLOCK)
	INNER JOIN [dbo].[MS_EquipmentType] AS EQT WITH (NOLOCK)
	ON
		(EQT.EquipmentTypeID = EQ.EquipmentTypeID)
	INNER JOIN [dbo].[MS_AccountInventory] AS AI WITH (NOLOCK)
	ON
		(AI.EquipmentID = EQ.EquipmentID)
GROUP BY
	EQ.EquipmentTypeID
	, EQT.EquipmentType;