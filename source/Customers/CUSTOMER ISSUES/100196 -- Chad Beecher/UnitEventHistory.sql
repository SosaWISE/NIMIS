USE WISE_GPSTRACKING
GO
SELECT
	LP_CommandMessages.CommandMessageID AS [C ID]
	--, LP_CommandMessages.CommandTypeId
	--, LP_CommandMessages.CommandNameId
	--, LP_CommandMessages.RequestId
	, LP_CommandMessages.IPAddress
	, LP_CommandMessages.Port
	--, LP_CommandMessages.UnitID
	--, LP_CommandMessages.MessageDate AS [Message Date]
	--, LP_CommandMessages.Sentence AS [Device Message]
	, LP_CommandMessages.CreatedOn AS [Event Date (MST)]
	--, LP_CommandMessages.DEX_ROW_TS
	, LP_CommandMessageAVRMCs.Latitude
	, LP_CommandMessageAVRMCs.NSIndicator
	, LP_CommandMessageAVRMCs.Longitude
	, LP_CommandMessageAVRMCs.EWIndicator
	, LP_CommandMessageAVRMCs.Course
	, LP_CommandMessageAVRMCs.Speed
	, LP_CommandMessageAVRMCs.EventCodeId
	, EC.EventCode
	, LP_CommandMessageAVRMCs.BatteryVoltage
	, LP_CommandMessageAVRMCs.CurrentMilage
	, LP_CommandMessageAVRMCs.GPSStatus
FROM
	LP_CommandMessages
	INNER JOIN LP_CommandMessageAVRMCs
	ON
		LP_CommandMessages.CommandMessageID = LP_CommandMessageAVRMCs.CommandMessageID
	LEFT OUTER JOIN dbo.LP_EventCodes AS EC WITH (NOLOCK)
	ON
		(LP_CommandMessageAVRMCs.EventCodeId = EC.EventCodeID)
WHERE
	(LP_CommandMessages.UnitID = 100195) AND (LP_CommandMessageAVRMCs.EventCodeId IS NOT NULL)
	--AND (dbo.LP_CommandMessageAVRMCs.EventCodeId NOT IN ('0', '6'))
ORDER BY 
	LP_CommandMessages.CommandMessageID DESC