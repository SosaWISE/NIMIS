USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwLP_CommandMessageAVRMCs')
	BEGIN
		PRINT 'Dropping VIEW vwLP_CommandMessageAVRMCs'
		DROP VIEW dbo.vwLP_CommandMessageAVRMCs
	END
GO

PRINT 'Creating VIEW vwLP_CommandMessageAVRMCs'
GO

/****** Object:  View [dbo].[vwLP_CommandMessageAVRMCs]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwLP_CommandMessageAVRMCs.sql
**		Name: vwLP_CommandMessageAVRMCs
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
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwLP_CommandMessageAVRMCs]
AS
	/** Enter Query here */
	SELECT 
		AVRMC.CommandMessageID
		, AVRMC.ReqCommandMessageId
		, LCM.UnitID
		, LCM.IPAddress
		, LCM.Port
		, AVRMC.[UTCDateTime] AS [UTCDateTime]
		, AVRMC.DeviceStatusId
		, [dbo].fn_GetDeviceStatus(AVRMC.DeviceStatusID) AS DeviceStatus
		, AVRMC.Latitude
		, AVRMC.NSIndicator
		, AVRMC.Longitude
		, AVRMC.EWIndicator
		, AVRMC.Speed
		, AVRMC.Course
		, AVRMC.EventCodeId
		, [dbo].fn_GetEventCode(AVRMC.EventCodeID) AS EventCode
		, AVRMC.BatteryVoltage
		, AVRMC.CurrentMilage
		, AVRMC.GPSStatus
		, AVRMC.AnalogPort1
		, AVRMC.AnalogPort2
		, AVRMC.ProcessedDate
		, LCM.CreatedOn
		, AVRMC.DEX_ROW_TS
	FROM
		[dbo].LP_CommandMessageAVRMCs AS AVRMC WITH (NOLOCK)
		INNER JOIN [dbo].LP_CommandMessages AS LCM WITH (NOLOCK)
		ON
			(AVRMC.CommandMessageID = LCM.CommandMessageID)

GO
/* TEST */
SELECT * FROM vwLP_CommandMessageAVRMCs ORDER BY CommandMessageID DESC
