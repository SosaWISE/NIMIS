USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_Equipments')
	BEGIN
		PRINT 'Dropping VIEW vwMS_Equipments'
		DROP VIEW dbo.vwMS_Equipments
	END
GO

PRINT 'Creating VIEW vwMS_Equipments'
GO

/****** Object:  View [dbo].[vwMS_Equipments]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_Equipments.sql
**		Name: vwMS_Equipments
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2014	Reagan	Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_Equipments]
AS
	-- Enter Query here
	SELECT
		[EquipmentID]
		,[EquipmentMonitoredTypeId]
		,[EquipmentTypeId]
		,[AccountZoneTypeId]
		,[EquipmentPanelTypeId]
		,[GPItemNmbr]
		,[ItemDescription]
		,[ShortName]
		,[GenDescription]
		,[FullName]
		,[ShowInInventory]
		,[Points]
		,[ActualPoints]
		,[RetailPrice]
		,[IsCellUnit]
		,[AuditDay]
		,[EmployeeCost]
		,[DefaultTechStockLevel]
		,[IsHighlighted]
		,[IsWireless]
		,[IsExisting]
		,[IsActive]
		,[IsDeleted]
	FROM
		[dbo].[MS_Equipments]
	WHERE
		(IsActive = 1) 
		AND (IsDeleted = 0)
--		AND (EquipmentID NOT IN (SELECT ItemID FROM [dbo].[AE_ItemInterims]))
--		AND (AccountZoneTypeId <> 'NOZONE')
		;

GO

/** Testing 
SELECT * FROM vwMS_Equipments; 
SELECT * FROM AE_Items;  -- 239
SELECT * FROM MS_Equipments; -- 184

SELECT 
	*
FROM
	AE_Items AS ITM WITH (NOLOCK)
WHERE
	(ITM.ItemID NOT IN (SELECT EquipmentID FROM MS_Equipments));
*/