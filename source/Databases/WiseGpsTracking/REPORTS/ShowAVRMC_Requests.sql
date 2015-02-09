USE WISE_GPSTRACKING
GO
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [CommandMessageID]
      ,[UnitID]
      ,[IPAddress]
      ,[Port]
      ,[UTCDateTime]
      ,[DeviceStatusId]
      ,[DeviceStatus]
      ,[Latitude]
      ,[NSIndicator]
      ,[Longitude]
      ,[EWIndicator]
      ,[Speed]
      ,[Course]
      ,[EventCodeId]
      ,[EventCode]
      ,[BatteryVoltage]
      ,[CurrentMilage]
      ,[GPSStatus]
      ,[AnalogPort1]
      ,[AnalogPort2]
      ,[CreatedOn]
FROM
	[WISE_GPSTRACKING].[dbo].[vwLP_CommandMessageAVRMC] AS AVRMC
ORDER BY
	AVRMC.CommandMessageID DESC