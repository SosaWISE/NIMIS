USE [WISE_CRM]
GO

--SELECT * FROM [WISE_CRM].[dbo].[MS_EquipmentTypesZoneEventTypes];
--SELECT * FROM [dbo].[MS_AccountEvents];
--SELECT * FROM [dbo].[MS_EquipmentMonitoredTypes];
--SELECT * FROM [dbo].[MS_EquipmentTypes];
--SELECT * FROM [dbo].[MS_AccountZoneTypes];
--SELECT * FROM [dbo].[MS_EquipmentPanelTypes];
--SELECT
--	ETET.EquipmentTypeEventTypeID
--	, ETET.EquipmentTypeId
--	, EQT.EquipmentType
--	, ETET.MoniEventId
--	, ETET.AGEventId
--	, ETET.SortOrder
--	, ETET.DefaultItem
--FROM
--	[dbo].[MS_EquipmentTypeEventTypes] AS ETET WITH (NOLOCK)
--	INNER JOIN [dbo].[MS_EquipmentTypes] AS EQT WITH (NOLOCK)
--	ON
--		(EQT.EquipmentTypeID = ETET.EquipmentTypeId);

--BEGIN TRANSACTION

/** Declarations */
DECLARE @EquipmentID VARCHAR(20);

/** Build the MS_EquipmentAccountZoneTypes
INSERT INTO [dbo].[MS_EquipmentAccountZoneTypes] (EquipmentId, AccountZoneTypeId)
SELECT EquipmentID, AccountZoneTypeId FROM [dbo].[MS_Equipments] */

/** Build Events per item. 
INSERT INTO [dbo].[MS_EquipmentAccountZoneTypeEvents] (
	[EquipmentAccountZoneTypeId]
	, [MonitoringStationOSId]
	, [AccountEventId]
)
SELECT 
	EAZT.EquipmentAccountZoneTypeID
	, 'MI_MASTER' AS MonitoringStationOSId
	, ETET.AccountEventId
FROM
	[dbo].[MS_EquipmentAccountZoneTypes] AS EAZT WITH (NOLOCK)
	INNER JOIN [dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
	ON
		(EQM.EquipmentID = EAZT.EquipmentId)
	INNER JOIN [dbo].[MS_EquipmentTypesZoneEventTypes] AS ETET WITH (NOLOCK)
	ON
		(ETET.EquipmentTypeId = EQM.EquipmentTypeId);
--) VALUES (
--	0 -- EquipmentAccountZoneTypeId - int
--	, 'MI_MASTER' -- MonitoringStationOSId - varchar(50)
--	, 0  -- AccountEventId - int
--);

INSERT INTO [dbo].[MS_EquipmentAccountZoneTypeEvents] (
	[EquipmentAccountZoneTypeId]
	, [MonitoringStationOSId]
	, [AccountEventId]
)
SELECT 
	EAZT.EquipmentAccountZoneTypeID
	, 'AG_ALARMSYS' AS MonitoringStationOSId
	, ETET.AccountEventId
FROM
	[dbo].[MS_EquipmentAccountZoneTypes] AS EAZT WITH (NOLOCK)
	INNER JOIN [dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
	ON
		(EQM.EquipmentID = EAZT.EquipmentId)
	INNER JOIN [dbo].[MS_EquipmentTypesZoneEventTypes] AS ETET WITH (NOLOCK)
	ON
		(ETET.EquipmentTypeId = EQM.EquipmentTypeId);
*/


/**
* Test the drop down features
*/
/**
SELECT * FROM [dbo].[MS_Equipments] WHERE (ItemDescription LIKE '%2gig%');
EQPM_INVT10 -- 2GIG Go!Control Security and Home Automation Control Panel
EQPM_INVT16 -- 2GIG- ADI Wireless door/window contact
*/	
SET @EquipmentID = 'EQPM_INVT10';
SELECT
	EAZT.EquipmentAccountZoneTypeID
	, EQM.EquipmentID
	, EAZT.AccountZoneTypeId
	, AZT.AccountZoneType
FROM
	[dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
	INNER JOIN [dbo].[MS_EquipmentAccountZoneTypes] AS EAZT WITH (NOLOCK)
	ON
		(EAZT.EquipmentId = EQM.EquipmentID)
	INNER JOIN [dbo].[MS_AccountZoneTypes] AS AZT WITH (NOLOCK)
	ON
		(AZT.AccountZoneTypeID = EAZT.AccountZoneTypeId)
WHERE
	(EQM.EquipmentID = @EquipmentID);

	/** Monitronics MASTER*/
SELECT 
	MAE.AccountEventID
	, EAZTE.EquipmentAccountZoneTypeId
	, MAE.MoniEventId AS EventID
	, MEC.descr AS [Description]
FROM
	[dbo].[MS_EquipmentAccountZoneTypeEvents] AS EAZTE WITH (NOLOCK)
	INNER JOIN [dbo].[MS_AccountEvents] AS MAE WITH (NOLOCK)
	ON
		(MAE.AccountEventID = EAZTE.AccountEventId)
	INNER JOIN [dbo].[MS_MonitronicsEventCodes] AS MEC WITH (NOLOCK)
	ON
		(MEC.MoniEventID = MAE.MoniEventId)
	/** AvantGuard Stages. */
UNION
SELECT 
	MAE.AccountEventID
	, EAZTE.EquipmentAccountZoneTypeId
	, MAE.AvantGuardEventId AS EventID
	, AGEC.Description
FROM
	[dbo].[MS_EquipmentAccountZoneTypeEvents] AS EAZTE WITH (NOLOCK)
	INNER JOIN [dbo].[MS_AccountEvents] AS MAE WITH (NOLOCK)
	ON
		(MAE.AccountEventID = EAZTE.AccountEventId)
	INNER JOIN [dbo].[MS_AvantGuardEventCodes] AS AGEC WITH (NOLOCK)
	ON
		(AGEC.AGEventID = MAE.AvantGuardEventId)


--ROLLBACK TRANSACTION