DECLARE @ItemSKU VARCHAR(50)
DECLARE @EquipmentMonitoredTypeId INT
DECLARE @EquipmentTypeId INT
DECLARE @AccountZoneTypeId VARCHAR(10)
DECLARE @EquipmentPanelTypeId INT
DECLARE @IsCellUnit BIT
DECLARE @IsExisting BIT
DECLARE @IsGeneric BIT
DECLARE @IsHighlighted BIT
DECLARE @IsWireless BIT
DECLARE @ShowInInventory BIT
DECLARE @UpdateFlg INT

SET @ItemSKU = 'HD100'
/*'99100-006'
'99100-004',
'99100-005',
'99100-006'
*/
SET @EquipmentMonitoredTypeId = NULL
/*
EquipmentMonitoredTypeId:
1 Contact(Door/Window
2 Contact(Door)
3 Contact(Window)
4 Pendant
5 Keychain
6 Keyfob
7 Fire (Smoke)
8 Flood/Waterbug
9 Gas(Carbon Monoxide)
10 Glassbreak/Shock
11 Motion/Photobeam
12 Keypad
13 Temperature Sensor
14 Open
15 Close
*/

SET @EquipmentTypeId = 15
/*
EquipmentTypeID:
1 Door / Window
2 Key Fob
3 Motion
4 Smoke
5 Glassbreak
6 Medical
7 Carbon
8 Flood
9 Freeze
10 Panel
11 Cell
12 No Zone
13 Tool
14 Activation Fee
15 Camera
16 Deadbolt
17 Doorbell
18 Kit
19 Keypad
20 Monitoring Fee
21 Payroll Deduction
22 Takeover
23 Z-Wave
24 Sign
25 Door Armor
26 Key Lock
27 Product Upgrade

*/

SET @AccountZoneTypeId = 'NOZONE'
/*
AccountZoneTypeID:
AccountZoneTypeID	AccountZoneType
FIRE	Fire
MEDICAL	Medical
NOZONE	[No Zone]
PANEL	Panel
POLICE	Police
*/

SET @EquipmentPanelTypeId = 5
/*
EquipmentPanelTypeId	PanelTypeName	AvantGuardCode
1	LYNX	LYNX
2	VISTA	VISTA
3	SIMON	GE Simon XT
4	CONCORD	CONCRD
5	GENERIC	Generic Panel
*/

SET @IsCellUnit = 0
SET @IsExisting = 0
SET @IsGeneric = 0
SET @IsHighlighted = 0
SET @IsWireless = 0
SET @ShowInInventory = 1

SELECT @UpdateFlg = Count(*) FROM MS_Equipments WHERE GPItemNmbr = @ItemSKU

IF @UpdateFlg = 0
	BEGIN
	INSERT dbo.MS_Equipments
	(
		EquipmentID, 
		EquipmentMonitoredTypeId, 
		EquipmentTypeId, 
		AccountZoneTypeId, 
		AccountEventId, 
		EquipmentPanelTypeId, 
		GPItemNmbr, 
		ItemDescription, 
		ShortName, 
		GenDescription, 
		--FullName, 
		ShowInInventory, 
		Points, 
		ActualPoints, 
		RetailPrice, 
		IsCellUnit, 
		AuditDay, 
		EmployeeCost, 
		DefaultTechStockLevel, 
		IsHighlighted, 
		IsWireless, 
		IsGeneric, 
		IsExisting, 
		IsActive, 
		IsDeleted, 
		ModifiedOn, 
		ModifiedBy, 
		CreatedOn, 
		CreatedBy
	)
	SELECT  
		AE_Items.ItemID AS EquipmentID, 
		@EquipmentMonitoredTypeId AS EquipmentMonitoredTypeId, 
		@EquipmentTypeId AS EquipmentTypeId, 
		@AccountZoneTypeId AS AccountZoneTypeId, 
		NULL AS AccountEventId, 
		@EquipmentPanelTypeId AS EquipmentPanelTypeId, 
		AE_Items.ItemSKU AS GPItemNmbr, 
		AE_Items.ItemDesc AS ItemDescription, 
		LTRIM(RTRIM(IV00101.ITMSHNAM)) AS ShortName, 
		LTRIM(RTRIM(IV00101.ITMSHNAM)) AS GenDescription, 
		--FullName, 
		@ShowInInventory AS ShowInInventory, 
		AE_Items.SystemPoints AS Points, 
		AE_Items.SystemPoints AS ActualPoints, 
		AE_Items.Price AS RetailPrice, 
		@IsCellUnit AS IsCellUnit, 
		NULL AS AuditDay, 
		AE_Items.Cost AS EmployeeCost, 
		NULL AS DefaultTechStockLevel, 
		@IsHighlighted AS IsHighlighted, 
		@IsWireless as IsWireless, 
		@IsGeneric AS IsGeneric,
		@IsExisting AS IsExisting, 
		1 AS IsActive,
		0 AS IsDeleted, 
		AE_Items.ModifiedOn AS ModifiedOn, 
		'SYSTEM' AS ModifiedBy, 
		AE_ITEMS.CreatedOn AS CreatedOn, 
		'SYSTEM' AS CreatedBy
	FROM 
		dbo.AE_ITEMS WITH(NOLOCK) 
		JOIN [DYSNEYDAD].NEX.dbo.IV00101 WITH(NOLOCK)
			ON AE_ITEMS.ItemSKU = IV00101.ITEMNMBR
	WHERE
		(AE_Items.ItemSKU = @ItemSKU )
		AND (AE_Items.IsDeleted = 'FALSE')
		AND (AE_Items.IsActive = 'TRUE');

	INSERT MS_EquipmentAccountZoneTypes
	(
		EquipmentId,
		AccountZoneTypeId
	)
	SELECT 
		EQUIPMENTID,
		@AccountZoneTypeId
	FROM 
		MS_Equipments 
	WHERE 
		GPItemNmbr = @ItemSKU
END

/*
SELECT 
	Equipment.*
FROM 
	WISE_CRM.dbo.AE_Items AS Items WITH (NOLOCK)
	JOIN WISE_CRM.dbo.MS_Equipments as Equipment WITH (NOLOCK)
		ON Items.ItemSKU  = Equipment.GPItemNmbr
		AND equipment.AccountZoneTypeId = 'FIRE'
--		AND Equipment.GPItemNmbr IN ('ECFF345','QSFF345')
ORDER BY 
	Equipment.EquipmentMonitoredTypeId,
	Equipment.GPItemNmbr
*/
/*
SELECT AE_Items.*, MS_Equipments.*
FROM WISE_CRM.dbo.AE_Items
LEFT JOIN WISE_CRM.dbo.MS_Equipments
	ON AE_ITEMS.ItemSKU = MS_Equipments.GPItemNmbr
WHERE MS_Equipments.GPItemNmbr IS NULL


SELECT 
	Equipment.*
FROM 
	WISE_CRM.dbo.AE_Items AS Items WITH (NOLOCK)
	JOIN WISE_CRM.dbo.MS_Equipments as Equipment WITH (NOLOCK)
		ON Items.ItemSKU  = Equipment.GPItemNmbr
		AND Equipment.GPItemNmbr IN (
		'2GIG-Z-PBD',
		'2GIG-Z-VBD',
		'CONCORD4',
		'EZA-DSL-10102',
		'FAJ-EXT-20000-RP',
		'KIT-DDR-10002',
		'PD300Z-2',
		'QSBAT-UB613',
		'SET-EZA-20000-BP',
		'SET-EZA-22000-BP',
		'SET-EZA-23000-BP',
		'SIGN-MAXWELL',
		'VISTA15P'
		)
ORDER BY Equipment.GPItemNmbr

select IV00101.* 
from 
	[DYSNEYDAD].NEX.dbo.IV00101 WITH (NOLOCK)
	JOIN dbo.AE_Items WITH (NOLOCK)
		ON IV00101.ITEMNMBR = AE_Items.ItemSKU
*/