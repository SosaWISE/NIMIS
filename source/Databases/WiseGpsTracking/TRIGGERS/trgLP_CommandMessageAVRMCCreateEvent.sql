USE [WISE_GPSTRACKING]
GO
/****** Object:  Trigger [dbo].[MS_AccountInstallationStatus_trgHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trgLP_CommandMessageAVRMCCreateEvent'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trgLP_CommandMessageAVRMCCreateEvent'
	DROP TRIGGER [dbo].[trgLP_CommandMessageAVRMCCreateEvent]
END
GO

PRINT 'CREATING TRIGGER trgLP_CommandMessageAVRMCCreateEvent'
GO
/**********************************************************************************************************************
**	File: MS_AccountInstallationStatus_trgHistory.sql
**	Name: MS_AccountInstallationStatus_trgHistory
**	Desc: 
**
**	This trigger will check the event and move it to the events table if
**	is is a reportable event.
**		Code	Description
**		Z	Low battery alert
**		X	 Geo-fence enter alert
**		T	Tamper detection switch is open alert
**		S	Tamper detection switch is close alert
**		8	G-Sensor alert 1
**		7	Instance Geo-fence exit alert
**		6	Over speed alert
**		4	Geo-fence exits alert
**		3	Panic/SOS button pressed alert
**		1	SOS button pressed alert
**             
**	Auth: Andrés Sosa
**	Date: 11/22/2012
**			
***********************************************************************************************************************
**	Change History
***********************************************************************************************************************
**	Date:		Author:			Description:
**	--------	--------		---------------------------------------------------------------------------------------
**	11/22/2012	Andrés Sosa		Created by
**********************************************************************************************************************/
CREATE TRIGGER [dbo].[trgLP_CommandMessageAVRMCCreateEvent]
   ON  [dbo].[LP_CommandMessageAVRMCs]
   FOR INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/** Check to see if this is a trackable event.
	SELECT * FROM LP_CommandMessageAVRMCs ORDER BY CommandMessageID DESC
	*/
	IF(EXISTS(SELECT * FROM INSERTED WHERE EventCodeId IN ('Z','X','T','S','G','8','7','6','4','3','1')))
	BEGIN
		INSERT INTO [dbo].GS_Events (
			EventTypeId
			, AccountId
			, EventName
			, EventDate
			, Lattitude
			, Longitude
			, Speed
			, Course
			, CurrentMilage
		) 
		SELECT
			CASE
				WHEN CMV.EventCodeId = 'Z' THEN 'LOWBAT'
				WHEN CMV.EventCodeId = 'X' THEN 'FENCE_RT'
				WHEN CMV.EventCodeId = 'T' THEN 'TAMPER'
				WHEN CMV.EventCodeId = 'S' THEN 'TAMPER_RT'
				WHEN CMV.EventCodeId = '8' THEN 'FALL'
				WHEN CMV.EventCodeId = '7' THEN 'FENCE'
				WHEN CMV.EventCodeId = '6' THEN 'SPEED'
				WHEN CMV.EventCodeId = '4' THEN 'FENCE'
				WHEN CMV.EventCodeId = '3' THEN 'EMERG'
				WHEN CMV.EventCodeId = '1' THEN 'EMERG'
			END
			, PDV.AccountID
			, PEC.EventCode
			, CMV.[UTCDateTime]
			, [dbo].fn_GetGoogleLattitudeFromLaipacLattitude(CMV.NSIndicator, CMV.Latitude)
			, [dbo].fn_GetGoogleLongitudeFromLaipacLongitude(CMV.EWIndicator, CMV.Longitude)
			, CMV.Speed
			, CMV.Course
			, CMV.CurrentMilage
		FROM 
			INSERTED AS CMV WITH (NOLOCK)
			INNER JOIN LP_CommandMessages AS CM WITH (NOLOCK)
			ON
				(CMV.CommandMessageID = CM.CommandMessageID)
			INNER JOIN LP_Devices AS PDV WITH (NOLOCK)
			ON
				(CM.UnitID = PDV.UnitID)
			INNER JOIN LP_EventCodes AS PEC WITH (NOLOCK)
			ON
				(CMV.EventCodeId = PEC.EventCodeID)
	END
END