USE [Platinum_Protection_InterimCRM]
GO

--BEGIN TRANSACTION

--SELECT 
--	CD.AccountID
--	, MAS.IndustryAccountID
--	, MAS.CellIndustryAccountID
--INTO NXS_CellData_1
--FROM
--	[dbo].[NXS_CellularData] AS CD WITH (NOLOCK)
--	INNER JOIN [dbo].[MS_Account] AS MAS WITH (NOLOCK)
--	ON
--		(CD.AccountID = MAS.AccountID);

/** Honeywell */
--	SELECT
--		CD.AccountID
--		, CD.IndustryAccountID
--		, IND.BlockAccountId AS [H2 BlockAccountId]
--		, RL.MSDesignator  + LTRIM(RTRIM(RLBA.SubscriberId)) AS [H2 MSID]
--		, RLBAN.MACAddress AS [H2 MACAddress]
--		, RLBAN.CRCNumber AS [H2 CRCNumber]
--		, RLBAN.RegisteredDate AS [H2 RegisteredDate]
--		, RLBAN.UnRegisteredDate AS [H2 UnRegisteredDate]
--		, ACM.CityID AS [H2 CityID]
--		, ACM.CSID AS [H2 CSID]
--		, ACM.SubscriberId AS [H2 SubscriberId]
--		, ACM.TransferredDate AS [H2 TransferredDate]
--		, ACM.TransferDirection AS [H2 TransferDirection]
--		, CASE 
--			WHEN ACM.DeviceID IS NULL THEN '00D02D' + RLBAN.MACAddress
--			ELSE ACM.DeviceID
--		END AS [H2 DeviceID]
--		, ACM.DeviceMode AS [H2 DeviceMode]
--		, ACM.DeviceType AS [H2 DeviceType]
--		, ACM.Supervision AS [H2 Supervision]
--		, ACM.RegisterStatus AS [H2 RegisterStatus]
--	INTO [dbo].NXS_CellularData_H2
--	FROM
--		[dbo].NXS_CellularDataIndexkeys AS CD WITH (NOLOCK)
--		LEFT OUTER JOIN [dbo].MS_IndustryAccount AS IND WITH (NOLOCK)
--		ON
--			(CD.CellIndustryAccountID = IND.IndustryAccountID)
--		LEFT OUTER JOIN [dbo].MS_ReceiverLine AS RL
--		ON
--			(RL.ReceiverLineID = IND.ReceiverLineID)
--		LEFT OUTER JOIN [dbo].MS_ReceiverLineBlockAccount AS RLBA
--		ON
--			(RLBA.BlockAccountId = IND.BlockAccountId)
--		LEFT OUTER JOIN [dbo].MS_ReceiverLineBlockAccountAlarmNet AS RLBAN
--		ON
--			(RLBAN.BlockAccountId = RLBA.BlockAccountId)
--		LEFT OUTER JOIN [dbo].[MS_AlarmnetMonitoringStatus] AS ACM
--		ON
--			(ACM.BlockAccountID = RLBA.BlockAccountId);

/** Alarm.Com 
	SELECT
		CD.AccountID
		, IND.IndustryAccountID
		, IND.BlockAccountId
		, RL.MSDesignator  + LTRIM(RTRIM(RLBA.SubscriberId)) AS [C2 MSID]
		, RLBAA.CustomerId AS [C2 CustomerId]
		, RLBAA.SerialNumber AS [C2 SerialNumber]
		, ModemFirmwareVersion AS [C2 ModemFirmwareVersion]
		, RLBAA.IsTwoWay AS [C2 IsTwoWay]
		, ACM.ServicePlanPackageId AS [C2 ServicePlanPackageId]
		, ACM.ServicePlanType AS [C2 ServicePlanType]
		, ACM.ServicePlanTotalPrice AS [C2 ServicePlanTotalPrice]
		, RLBAA.RegisteredDate AS [C2 RegisteredDate]
		, RLBAA.UnRegisteredDate AS [C2 UnRegisteredDate]
	INTO [dbo].NXS_CellularData_C2
	FROM
		[dbo].NXS_CellularDataIndexkeys AS CD WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].MS_IndustryAccount AS IND
		ON
			(CD.IndustryAccountID = IND.IndustryAccountID)
		LEFT OUTER JOIN [dbo].MS_ReceiverLine AS RL
		ON
			(RL.ReceiverLineID = IND.ReceiverLineID)
		LEFT OUTER JOIN [dbo].MS_ReceiverLineBlockAccount AS RLBA
		ON
			(RLBA.BlockAccountId = IND.BlockAccountId)
		LEFT OUTER JOIN [dbo].MS_ReceiverLineBlockAccountAlarmCom AS RLBAA
		ON
			(RLBAA.BlockAccountId = RLBA.BlockAccountId)
		LEFT OUTER JOIN [dbo].[MS_AlarmDotComAccountStatus] AS ACM
		ON
			(ACM.BlockAccountID = RLBA.BlockAccountId);
*/

SELECT 
	 C1.AccountID ,
	        C1.IndustryAccountID AS [C1 IndustryAccountID] ,
	        C1.BlockAccountId AS [C1 BlockAccountId] ,
	        C1.[C1 MSID] ,
	        C1.[C1 CustomerId] ,
	        C1.[C1 SerialNumber] ,
	        C1.[C1 ModemFirmwareVersion] ,
	        C1.[C1 IsTwoWay] ,
	        C1.[C1 ServicePlanPackageId] ,
	        C1.[C1 ServicePlanType] ,
	        C1.[C1 ServicePlanTotalPrice] ,
	        C1.[C1 RegisteredDate] ,
	        C1.[C1 UnRegisteredDate] ,
	--,  C2.AccountID ,
	        C2.IndustryAccountID AS [C2 IndustryAccountID] ,
	        C2.BlockAccountId AS [C2 BlockAccountId] ,
	        C2.[C2 MSID] ,
	        C2.[C2 CustomerId] ,
	        C2.[C2 SerialNumber] ,
	        C2.[C2 ModemFirmwareVersion] ,
	        C2.[C2 IsTwoWay] ,
	        C2.[C2 ServicePlanPackageId] ,
	        C2.[C2 ServicePlanType] ,
	        C2.[C2 ServicePlanTotalPrice] ,
	        C2.[C2 RegisteredDate] ,
	        C2.[C2 UnRegisteredDate] ,
	--,  H1.AccountID ,
	        H1.IndustryAccountID AS [H1 IndustryAccountID] ,
	        H1.[H1 BlockAccountId] AS [H1 BlockAccountId] ,
	        H1.[H1 MSID] ,
	        H1.[H1 MACAddress] ,
	        H1.[H1 CRCNumber] ,
	        H1.[H1 RegisteredDate] ,
	        H1.[H1 UnRegisteredDate] ,
	        H1.[H1 CityID] ,
	        H1.[H1 CSID] ,
	        H1.[H1 SubscriberId] ,
	        H1.[H1 TransferredDate] ,
	        H1.[H1 TransferDirection] ,
	        H1.[H1 DeviceID] ,
	        H1.[H1 DeviceMode] ,
	        H1.[H1 DeviceType] ,
	        H1.[H1 Supervision] ,
	        H1.[H1 RegisterStatus] ,
	--,  H2.AccountID ,
	        H2.IndustryAccountID AS [H2 IndustryAccountID] ,
	        H2.[H2 BlockAccountId] AS [H2 BlockAccountId] ,
	        H2.[H2 MSID] ,
	        H2.[H2 MACAddress] ,
	        H2.[H2 CRCNumber] ,
	        H2.[H2 RegisteredDate] ,
	        H2.[H2 UnRegisteredDate] ,
	        H2.[H2 CityID] ,
	        H2.[H2 CSID] ,
	        H2.[H2 SubscriberId] ,
	        H2.[H2 TransferredDate] ,
	        H2.[H2 TransferDirection] ,
	        H2.[H2 DeviceID] ,
	        H2.[H2 DeviceMode] ,
	        H2.[H2 DeviceType] ,
	        H2.[H2 Supervision] ,
	        H2.[H2 RegisterStatus]
INTO [dbo].[NXS_CellularData]
FROM
	[dbo].NXS_CellularData_C1 AS C1 WITH (NOLOCK)
	INNER JOIN [dbo].[NXS_CellularData_C2] AS C2 WITH (NOLOCK)
	ON
		(C1.AccountID = C2.AccountID)
	INNER JOIN [dbo].[NXS_CellularData_H1] AS H1 WITH (NOLOCK)
	ON
		(H1.AccountID = C1.AccountID)
	INNER JOIN [dbo].[NXS_CellularData_H2] AS H2 WITH (NOLOCK)
	ON
		(H2.AccountID = C1.AccountID)
----ROLLBACK TRANSACTION