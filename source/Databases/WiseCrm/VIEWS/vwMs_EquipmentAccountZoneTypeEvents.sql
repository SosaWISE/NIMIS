USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_EquipmentAccountZoneTypeEvents')
	BEGIN
		PRINT 'Dropping VIEW vwMS_EquipmentAccountZoneTypeEvents'
		DROP VIEW dbo.vwMS_EquipmentAccountZoneTypeEvents
	END
GO

PRINT 'Creating VIEW vwMS_EquipmentAccountZoneTypeEvents'
GO

/****** Object:  View [dbo].[vwMS_EquipmentAccountZoneTypeEvents]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_EquipmentAccountZoneTypeEvents.sql
**		Name: vwMS_EquipmentAccountZoneTypeEvents
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
**		Date: 07/22/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/22/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_EquipmentAccountZoneTypeEvents]
AS
	-- Enter Query here
	SELECT 
		MAE.AccountEventID
		, EAZTE.EquipmentAccountZoneTypeId
		, EAZTE.MonitoringStationOSId
		, MAE.MoniEventId AS EventID
		, MEC.descr AS [Description]
		, CAST(0 AS BIT) AS [IsDefault]
	FROM
		[dbo].[MS_EquipmentAccountZoneTypeEvents] AS EAZTE WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountEvents] AS MAE WITH (NOLOCK)
		ON
			(MAE.AccountEventID = EAZTE.AccountEventId)
		INNER JOIN [dbo].[MS_MonitronicsEntityEventCodes] AS MEC WITH (NOLOCK)
		ON
			(MEC.MoniEventID = MAE.MoniEventId)
		/** AvantGuard Stages. */
	UNION
	SELECT 
		MAE.AccountEventID
		, EAZTE.EquipmentAccountZoneTypeId
		, EAZTE.MonitoringStationOSId
		, MAE.AvantGuardEventId AS EventID
		, AGEC.Description
		, CAST(0 AS BIT) AS [IsDefault]
	FROM
		[dbo].[MS_EquipmentAccountZoneTypeEvents] AS EAZTE WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountEvents] AS MAE WITH (NOLOCK)
		ON
			(MAE.AccountEventID = EAZTE.AccountEventId)
		INNER JOIN [dbo].[MS_AvantGuardEventCodes] AS AGEC WITH (NOLOCK)
		ON
			(AGEC.AGEventID = MAE.AvantGuardEventId)
	
GO
/* TEST
SELECT 
	*
FROM
	vwMS_EquipmentAccountZoneTypeEvents
WHERE
	(EquipmentAccountZoneTypeId = 182)
	AND (MonitoringStationOSId = 'AG_ALARMSYS')

 */