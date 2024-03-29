﻿USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySysTypesPanelGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySysTypesPanelGet'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySysTypesPanelGet
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySysTypesPanelGet'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySysTypesPanelGet.sql
**		Name: custMS_MonitronicsEntitySysTypesPanelGet
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 01/15/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/15/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySysTypesPanelGet
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @SystemTypeID VARCHAR(50);
	
	BEGIN TRY

		SELECT TOP 1
			@SystemTypeID = MEMD.DeviceId
			--MAE.AccountEquipmentID
			--, MAE.EquipmentId
			--, MAE.EquipmentLocationId
			--, MET.EquipmentType
			--, MAE.BarcodeId
			--, EQM.ItemDescription
			--, EQM.AccountZoneTypeId
		FROM
			[dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
			INNER JOIN [dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
			ON
				(EQM.EquipmentID = MAE.EquipmentId)
				AND (MAE.IsActive = 1) AND (MAE.IsDeleted = 0)
			INNER JOIN [dbo].[MS_EquipmentTypes] AS MET WITH (NOLOCK)
			ON
				(EQM.EquipmentTypeId = MET.EquipmentTypeID)
			INNER JOIN [dbo].[MS_EquipmentMonitronicsDevices] AS MEMD WITH (NOLOCK)
			ON
				(EQM.EquipmentID = MEMD.EquipmentID)
		WHERE
			(MAE.AccountId = @AccountID)
			--AND (EQM.IsCellUnit = 1)
			--AND (EQM.ItemDescription LIKE '%Tel%')
			AND (EQM.AccountZoneTypeId = 'PANEL');	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- ** Result
	SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypes] WHERE (SystemTypeID = @SystemTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySysTypesPanelGet TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySysTypesPanelGet 130532 

		SELECT
			--@SystemTypeID = MEMD.DeviceId
			--MEMD.DeviceId
			EQM.EquipmentID
			, MAE.AccountEquipmentID
			--, MAE.EquipmentId
			--, MAE.EquipmentLocationId
			, MET.EquipmentType
			--, MAE.BarcodeId
			, EQM.ItemDescription
			, EQM.AccountZoneTypeId
		FROM
			[dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
			INNER JOIN [dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
			ON
				(EQM.EquipmentID = MAE.EquipmentId)
				AND (MAE.IsActive = 1) AND (MAE.IsDeleted = 0)
			INNER JOIN [dbo].[MS_EquipmentTypes] AS MET WITH (NOLOCK)
			ON
				(EQM.EquipmentTypeId = MET.EquipmentTypeID)
			--INNER JOIN [dbo].[MS_EquipmentMonitronicsDevices] AS MEMD WITH (NOLOCK)
			--ON
			--	(EQM.EquipmentID = MEMD.EquipmentID)
		WHERE
			(MAE.AccountId = 130532)
			--AND (EQM.IsCellUnit = 1)
			--AND (EQM.ItemDescription LIKE '%Tel%')
			AND (EQM.AccountZoneTypeId = 'PANEL');	


SELECT * FROM dbo.MS_Equipments WHERE EquipmentID IN ('EQPM_INVT26', 'EQPM_INVT10')*/