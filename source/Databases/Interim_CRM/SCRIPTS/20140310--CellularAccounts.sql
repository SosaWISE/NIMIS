USE [Platinum_Protection_InterimCRM]
GO

SELECT TOP 1000
	MAS.AccountID
	, MAS.IndustryAccountID
	, ACT.AccountCellularType
	--, MAS.SalesInfoCellularTypeId  -- No values
	, ACM.ModemSerial AS [AC ModemSerial]
	, ACM.ModemFirmwareVersion AS [AC ModemFirmwareVersion]
	, IAT.TypeName
	, IND.SubscriberNumber
	, RLBAA.CustomerId
	, RLBAA.IsTwoWay
	, RLBAA.RegisteredDate
	, RLBAA.SerialNumber
FROM
	[dbo].MS_Account AS MAS
	INNER JOIN [dbo].MS_AccountCellularType AS ACT
	ON
		(ACT.AccountCellularTypeId = MAS.AccountCellularTypeId)
		AND (MAS.AccountCellularTypeId <> 1)  -- Can't be No Cell
	LEFT OUTER JOIN [dbo].MS_AlarmDotComAccountStatus AS ACM
	ON
		(ACM.AccountId = MAS.AccountID)
	INNER JOIN [dbo].MS_IndustryAccount AS IND
	ON
		(IND.IndustryAccountID = MAS.IndustryAccountID)
	INNER JOIN [dbo].MS_IndustryAccountType AS IAT
	ON
		(IAT.IndustryAccountTypeID = IND.IndustryAccountTypeID)
	INNER JOIN [dbo].MS_ReceiverLineBlockAccount AS RLBA
	ON
		(RLBA.BlockAccountId = IND.BlockAccountId)
	LEFT OUTER JOIN [dbo].MS_ReceiverLineBlockAccountAlarmCom AS RLBAA
	ON
		(RLBAA.BlockAccountId = RLBA.BlockAccountId)
--WHERE
--	(MAS.SalesInfoCellularTypeId IS NOT NULL)
ORDER BY
	MAS.AccountID DESC;