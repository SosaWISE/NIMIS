USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountEvent')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountEvent'
		DROP VIEW dbo.vwMS_AccountEvent
	END
GO

PRINT 'Creating VIEW vwMS_AccountEvent'
GO

/****** Object:  View [dbo].[vwMS_AccountEvent]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountEvent.sql
**		Name: vwMS_AccountEvent
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 02/25/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/25/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountEvent]
AS
	-- Enter Query here
	SELECT
		ETZET.[EquipmentTypesZoneEventTypeID]
		, ETET.[EquipmentTypeID]
		, ETZET.[AccountEventId]
		, CAST('MI_DICE' AS VARCHAR(50)) AS MonitoringStationOSID
		, ETZET.[SortOrder]
		, ETET.MoniEventID
		, ETET.AGEventID
		, ETET.MoniEventID AS [ZoneEventTypeId]
		, MEC.event_id
		, MEC.servtype_id
		, MEC.descr AS [Description]
	FROM
		[dbo].[MS_EquipmentTypesZoneEventTypes] AS ETZET WITH (NOLOCK)
		INNER JOIN [dbo].[MS_EquipmentTypeEventTypes] AS ETET WITH (NOLOCK)
		ON
			(ETET.EquipmentTypeID = ETZET.EquipmentTypeID)
		INNER JOIN [dbo].[MS_MonitronicsEntityEventCodes] AS MEC WITH (NOLOCK)
		ON
			(MEC.MoniEventID = ETET.MoniEventID)
	UNION
	SELECT
		ETZET.[EquipmentTypesZoneEventTypeID]
		, ETET.[EquipmentTypeID]
		, ETZET.[AccountEventId]
		, CAST('AG_GPSTRACK' AS VARCHAR(50)) AS MonitoringStationOSID
		, ETZET.[SortOrder]
		, ETET.MoniEventID
		, ETET.AGEventID
		, ETET.AGEventID AS [ZoneEventTypeId]
		, AGEC.event_id
		, NULL AS servtype_id
		, AGEC.[Description]
	FROM
		[dbo].[MS_EquipmentTypesZoneEventTypes] AS ETZET WITH (NOLOCK)
		INNER JOIN [dbo].[MS_EquipmentTypeEventTypes] AS ETET WITH (NOLOCK)
		ON
			(ETET.EquipmentTypeID = ETZET.EquipmentTypeID)
		INNER JOIN [dbo].[MS_AvantGuardEventCodes] AS AGEC WITH (NOLOCK)
		ON
			(AGEC.AGEventID = ETET.AGEventID)
	UNION
	SELECT
		ETZET.[EquipmentTypesZoneEventTypeID]
		, ETET.[EquipmentTypeID]
		, ETZET.[AccountEventId]
		, CAST('AG_ALARMSYS' AS VARCHAR(50)) AS MonitoringStationOSID
		, ETZET.[SortOrder]
		, ETET.MoniEventID
		, ETET.AGEventID
		, ETET.AGEventID AS [ZoneEventTypeId]
		, AGEC.event_id
		, NULL AS servtype_id
		, AGEC.[Description]
	FROM
		[dbo].[MS_EquipmentTypesZoneEventTypes] AS ETZET WITH (NOLOCK)
		INNER JOIN [dbo].[MS_EquipmentTypeEventTypes] AS ETET WITH (NOLOCK)
		ON
			(ETET.EquipmentTypeID = ETZET.EquipmentTypeID)
		INNER JOIN [dbo].[MS_AvantGuardEventCodes] AS AGEC WITH (NOLOCK)
		ON
			(AGEC.AGEventID = ETET.AGEventID)

GO
/* TEST 
SELECT * FROM vwMS_AccountEvent
*/