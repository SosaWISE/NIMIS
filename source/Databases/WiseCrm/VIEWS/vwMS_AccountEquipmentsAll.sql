USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountEquipmentsAll')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountEquipmentsAll'
		DROP VIEW dbo.vwMS_AccountEquipmentsAll
	END
GO

PRINT 'Creating VIEW vwMS_AccountEquipmentsAll'
GO

/****** Object:  View [dbo].[vwMS_AccountEquipmentsAll]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountEquipmentsAll.sql
**		Name: vwMS_AccountEquipmentsAll
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
**	02/26/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountEquipmentsAll]
AS
	-- Enter Query here
	SELECT
		AEQ.AccountEquipmentID
		, AEQ.AccountId
		, AEQ.EquipmentId
		, AEQ.InvoiceItemId
		, EQM.GPItemNmbr AS ItemSKU
		, AEIT.ModelNumber
		, EQM.ItemDescription AS ItemDesc
		, AEQ.EquipmentLocationId
		, AEQ.GPEmployeeId
		, AEQ.AccountEquipmentUpgradeTypeId
		, AEQ.Points
		, AEQ.ActualPoints
		, AEQ.Price
		, AEQ.IsExisting
		, AEQ.BarcodeId
		, AEQ.IsServiceUpgrade
		, AEQ.IsExistingWiring
		, AEQ.IsMainPanel
		, AEQ.IsActive
		, AEQ.IsDeleted
		, AZA.AccountZoneAssignmentID
		, EQM.AccountZoneTypeId
		, AZA.AccountEventId
		, AZA.Zone
		, AZA.Comments
		, EL.EquipmentLocationDesc
		, AZT.AccountZoneType
	FROM
		[dbo].[MS_AccountEquipment] AS AEQ WITH (NOLOCK)
		INNER JOIN [dbo].MS_Equipments AS EQM WITH (NOLOCK)
		ON
			(EQM.EquipmentID = AEQ.EquipmentId)
		INNER JOIN [dbo].[AE_Items] AS AEIT WITH (NOLOCK)
		ON
			(AEIT.ItemID = EQM.EquipmentID)
		LEFT OUTER JOIN [dbo].MS_AccountZoneAssignments AS AZA WITH (NOLOCK)
		ON
			(AZA.AccountEquipmentId = AEQ.AccountEquipmentID)
		INNER JOIN [dbo].MS_AccountZoneTypes AS AZT WITH (NOLOCK)
		ON
			(EQM.AccountZoneTypeId = AZT.AccountZoneTypeID)
		LEFT OUTER JOIN [dbo].MS_EquipmentLocations AS EL WITH (NOLOCK)
		ON
			(AEQ.EquipmentLocationId = EL.EquipmentLocationID)
GO
/* TEST */
--SELECT * FROM vwMS_AccountEquipmentsAll WHERE AccountID = 130532;
