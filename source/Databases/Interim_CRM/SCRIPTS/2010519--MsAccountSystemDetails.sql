USE [Platinum_Protection_InterimCRM]
GO

SELECT TOP 100
	MAS.AccountID
	, MAS.MonitoringTypeID
	, MAM.MonitoringType
	, MAS.AccountCellularTypeId
	, MAC.AccountCellularType
	, MAS.AbortCode AS [Pass Phrase]
	, MAS.PanelTypeID
	, MAS.DSLSeizure
FROM
	MS_Account AS MAS WITH (NOLOCK)
	INNER JOIN MS_AccountStatus AS MSS WITH (NOLOCK)
	ON
		(MSS.AccountID = MAS.AccountID)
	INNER JOIN dbo.MS_AccountMonitoringType AS MAM WITH (NOLOCK)
	ON
		(MAM.MonitoringTypeID = MAS.MonitoringTypeID)
	INNER JOIN dbo.MS_AccountCellularType AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountCellularTypeId = MAS.AccountCellularTypeId)
WHERE
	(MSS.InstallDate IS NOT NULL)
	--AND (MAS.DSLSeizure IS NOT NULL)
ORDER BY
	MAS.AccountID DESC;