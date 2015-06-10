USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySysTypesCellDeviceGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySysTypesCellDeviceGet'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySysTypesCellDeviceGet
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySysTypesCellDeviceGet'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySysTypesCellDeviceGet.sql
**		Name: custMS_MonitronicsEntitySysTypesCellDeviceGet
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
CREATE Procedure dbo.custMS_MonitronicsEntitySysTypesCellDeviceGet
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
			AND (EQM.IsCellUnit = 1);
			--AND (EQM.ItemDescription LIKE '%Tel%')
			--AND (EQM.AccountZoneTypeId = 'PANEL');	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- ** Result
	SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypes] WHERE (SystemTypeID = @SystemTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySysTypesCellDeviceGet TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySysTypesCellDeviceGet 130532 
		SELECT --TOP 1
			--@SystemTypeID = MEMD.DeviceId
			MAE.AccountEquipmentID
			, EQM.*
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
			--INNER JOIN [dbo].[MS_EquipmentTypes] AS MET WITH (NOLOCK)
			--ON
			--	(EQM.EquipmentTypeId = MET.EquipmentTypeID)
			--INNER JOIN [dbo].[MS_EquipmentMonitronicsDevices] AS MEMD WITH (NOLOCK)
			--ON
			--	(EQM.EquipmentID = MEMD.EquipmentID)
		WHERE
			(MAE.AccountId = 181070)
			AND (EQM.IsCellUnit = 1);
			--AND (EQM.ItemDescription LIKE '%Tel%')
			--AND (EQM.AccountZoneTypeId = 'PANEL');	


*/

SELECT 
	MSAE.EquipmentId
	, MSEQ.GenDescription
FROM
	dbo.MS_AccountEquipment AS MSAE WITH (NOLOCK)
	INNER JOIN dbo.MS_Equipments AS MSEQ WITH (NOLOCK)
	ON
	(MSEQ.EquipmentID = MSAE.EquipmentId)
WHERE
	(AccountID = 191274);