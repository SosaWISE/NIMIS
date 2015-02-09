USE [WISE_CRM]
GO

SELECT
	ITM.ItemID
	, ITM.ItemSKU
	, ITM.ItemDesc
	, ISNULL((SELECT TOP 1 1 AS Equipment FROM [dbo].[MS_Equipments] WHERE (EquipmentID = ITM.ItemID)), 0) AS Equipment
	, ISNULL((SELECT TOP 1 1 AS Interim FROM [dbo].[AE_ItemInterims] WHERE (ItemID = ITM.ItemID)), 0) AS Interim
	, ISNULL((SELECT TOP 1 1 AS InGP FROM [DYSNEYDAD].NEX.dbo.IV00101 AS GPINV WITH (NOLOCK) WHERE (LTRIM(RTRIM(GPINV.ITEMNMBR)) = LTRIM(RTRIM(ITM.ItemSKU)))), 0) AS [In GP]
	, '' AS Issue
	, ISNULL((SELECT TOP 1 1 AS FAVS FROM [dbo].[MS_EquipmentMostFrequents] WHERE (ItemID = ITM.ItemID)), 0) AS FAVS
	, ISNULL((SELECT TOP 1 1 AS MS FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'ALRM')), 0) AS MS
	, ISNULL((SELECT TOP 1 1 AS [IS] FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'INSEC')), 0) AS [IS]
	, ISNULL((SELECT TOP 1 1 AS MS FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'LFLCK')), 0) AS LL
	, ISNULL((SELECT TOP 1 1 AS NM FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'NUMAN')), 0) AS NM
	, ISNULL((SELECT TOP 1 1 AS GS FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'PERS')), 0) AS GS
	, ISNULL((SELECT TOP 1 1 AS SP FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'SKPLT')), 0) AS SP
	, ISNULL((SELECT TOP 1 1 AS WF FROM [dbo].[AE_ItemAccounts] WHERE (ItemID = ITM.ItemID) AND (AccountTypeId = 'WNFIL')), 0) AS WF
FROM
	[dbo].[AE_Items] AS ITM WITH (NOLOCK)