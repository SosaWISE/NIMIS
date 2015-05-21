USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountEquipments')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountEquipments'
		DROP VIEW dbo.vwMS_AccountEquipments
	END
GO

PRINT 'Creating VIEW vwMS_AccountEquipments'
GO

/****** Object:  View [dbo].[vwMS_AccountEquipments]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountEquipments.sql
**		Name: vwMS_AccountEquipments
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
CREATE VIEW [dbo].[vwMS_AccountEquipments]
AS
	-- Enter Query here
	SELECT
		AEQ.AccountEquipmentID
		, AEQ.AccountId
		, AEQ.EquipmentId
		, AEQ.InvoiceItemId
		, EQM.GPItemNmbr AS ItemSKU
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
		INNER JOIN [dbo].MS_AccountZoneAssignments AS AZA WITH (NOLOCK)
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
DECLARE @AccountID BIGINT = 191230;
--SELECT AEQ.* 
--FROM 
--		[dbo].[MS_AccountEquipment] AS AEQ WITH (NOLOCK)
--		INNER JOIN [dbo].MS_Equipments AS EQM WITH (NOLOCK)
--		ON
--			(EQM.EquipmentID = AEQ.EquipmentId)
--		INNER JOIN [dbo].MS_AccountZoneAssignments AS AZA WITH (NOLOCK)
--		ON
--			(AZA.AccountEquipmentId = AEQ.AccountEquipmentID)
--WHERE (AEQ.AccountID = @AccountID) AND (AEQ.IsDeleted = 0);

SELECT * FROM vwMS_AccountEquipments WHERE (AccountID = @AccountID) AND (IsDeleted = 0) ORDER BY Zone;

