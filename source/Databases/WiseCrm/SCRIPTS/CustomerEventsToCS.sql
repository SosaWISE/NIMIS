USE [WISE_CRM]
GO

/**
* Find signals from a CSID
*/
--DECLARE @CSID VARCHAR(50) = '3134787148';
DECLARE @CSID VARCHAR(50) = '3133751699';

SELECT 
	MIA.IndustryAccountID
	, MIA.Csid
	, MIA.AccountId AS [IndAccountId]
	, MAS.AccountID
	, ACA.CustomerId
	, CUS.CustomerMasterFileId
	, CUS.FirstName + ' ' + CUS.LastName AS [Full Name]
	, CUS.PhoneHome
FROM
	[dbo].[MS_IndustryAccounts] AS MIA WITH (NOLOCK)
	INNER JOIN [dbo].MS_Accounts AS MAS WITH (NOLOCK)
	ON
		(MIA.AccountId = MAS.AccountID)
	INNER JOIN [dbo].AE_CustomerAccounts AS ACA WITH (NOLOCK)
	ON
		(MAS.AccountID = ACA.AccountId)
	INNER JOIN [dbo].AE_Customers AS CUS WITH (NOLOCK)
	ON
		(ACA.CustomerId = CUS.CustomerID)
WHERE
	(MIA.AccountId IS NOT NULL)
	AND (MIA.Csid = @CSID);

/** Now that you have the Device ID or AccountID */
--DECLARE @AccountID BIGINT = 100195;
DECLARE @AccountID BIGINT = 100203;
SELECT TOP 200
	CMM.CommandMessageID AS [ID]
	, DVC.AccountID
	, CMM.CommandTypeId
	, CMM.CommandNameId
	, AVRMC.DeviceStatusId
	, CASE
		WHEN AVRMC.DeviceStatusId = 'A' THEN 'Real-time data'
		WHEN AVRMC.DeviceStatusId = 'V' THEN 'Since Unit was powered on or reset data'
		WHEN AVRMC.DeviceStatusId = 'R' THEN 'Not Real-time data'
		ELSE 'UNDEFINED'
	  END AS [Device Status Desc]
	, AVRMC.EventCodeId
	, CASE
		WHEN AVRMC.EventCodeId = 'T' THEN 'Tamper detection switch is open alert'
		WHEN AVRMC.EventCodeId = 'S' THEN 'Tamper detection switch is close alert'
		WHEN AVRMC.EventCodeId = '8' THEN 'G-Sensor alert 1'
		WHEN AVRMC.EventCodeId = '3' THEN 'Panic/SOS button pressed alert'
		WHEN AVRMC.EventCodeId = '1' THEN 'SOS button pressed alert'
		ELSE 'UNDEFINED'
	  END AS [Event Code Desc]
	, AVRMC.UTCDateTime AS [Device Time]
	, AVRMC.CreatedOn AS [Received (MST)]
	, AVRMC.Latitude
	, AVRMC.NSIndicator AS [N/S]
	, AVRMC.Longitude
	, AVRMC.EWIndicator AS [E/W]
FROM
	[WISE_GPSTRACKING].[dbo].LP_Devices AS DVC WITH (NOLOCK)
	INNER JOIN [WISE_GPSTRACKING].[dbo].LP_CommandMessages AS CMM WITH (NOLOCK)
	ON
		(DVC.AccountID = CMM.UnitID)
	INNER JOIN [WISE_GPSTRACKING].[dbo].LP_CommandMessageAVRMCs AS AVRMC WITH (NOLOCK)
	ON
		(CMM.CommandMessageID = AVRMC.CommandMessageID)
WHERE
	(DVC.AccountID = @AccountID)
	AND (AVRMC.EventCodeId IN ('T','S','8','3','1'))
ORDER BY
	CMM.CommandMessageID DESC;
	
SELECT 
	*
FROM
	[WISE_GPSTRACKING].[dbo].LP_CommandMessages AS CMM WITH (NOLOCK)
WHERE
	(CMM.UnitID = @AccountID)
ORDER BY
	CMM.CommandMessageID DESC;