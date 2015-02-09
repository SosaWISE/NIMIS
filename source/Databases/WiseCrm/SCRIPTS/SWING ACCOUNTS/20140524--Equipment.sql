/** Copy Item from Interim to CRM */
USE [WISE_CRM]
GO

BEGIN TRANSACTION
DECLARE @ItemTypeId VARCHAR(50) = 'EQPM_EXST_MS';

UPDATE [dbo].[MS_Equipments] SET EquipmentID = @ItemTypeId + EquipmentID;

INSERT INTO [dbo].[AE_Items] (
	[ItemID]
	,[ItemTypeId]
	,[TaxOptionId]
	,[AccountZoneTypeId]
	,[ItemFKID]
	,[ItemSKU]
	,[ItemDesc]
	,[Price]
	,[Cost]
	,[SystemPoints]
	,[IsCatalogItem]
	,[IsActive]
	,[IsDeleted] )
--) VALUES (
--	<ItemID, varchar(50),>
--	,<ItemTypeId, varchar(50),>
--	,<TaxOptionId, char(3),>
--	,<AccountZoneTypeId, varchar(10),>
--	,<ItemFKID, varchar(30),>
--	,<ItemSKU, nvarchar(50),>
--	,<ItemDesc, nvarchar(50),>
--	,<Price, money,>
--	,<Cost, money,>
--	,<SystemPoints, decimal(9,1),>
--	,<IsCatalogItem, bit,>
--	,<IsActive, bit,>
--	,<IsDeleted, bit,>
--);

SELECT
	EQ.EquipmentID AS ItemID
	, @ItemTypeId AS ItemTypeId
	, 'EXT' AS TaxOptionId
	, EQ.AccountZoneTypeId
	, EQ.GPItemNmbr AS [ItemFKID]
	, EQ.GPItemNmbr AS [ItemSKU]
	, EQ.FullName AS [ItemDesc]
	, EQ.RetailPrice AS [Price]
	, CAST(0 AS MONEY) AS [Cost]
	, EQ.Points AS [SystemPoints]
	, CAST(0 AS BIT) AS [IsCatalogItem]
	, EQ.IsActive AS [IsActive]
	, EQ.IsDeleted AS [IsDeleted]

FROM
	[dbo].[MS_Equipments] AS EQ WITH (NOLOCK);


ROLLBACK TRANSACTION