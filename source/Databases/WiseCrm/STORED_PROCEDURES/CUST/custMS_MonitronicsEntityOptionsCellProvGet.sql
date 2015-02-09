USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityOptionsCellProvGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityOptionsCellProvGet'
		DROP  Procedure  dbo.custMS_MonitronicsEntityOptionsCellProvGet
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityOptionsCellProvGet'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityOptionsCellProvGet.sql
**		Name: custMS_MonitronicsEntityOptionsCellProvGet
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
CREATE Procedure dbo.custMS_MonitronicsEntityOptionsCellProvGet
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @EntityOptionID INT;
	
	BEGIN TRY

		SELECT TOP 1
			@EntityOptionID = MEMD.EntityOptionId
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
			INNER JOIN [dbo].[MS_EquipmentMonitronicsCellProviders] AS MEMD WITH (NOLOCK)
			ON
				(EQM.EquipmentID = MEMD.EquipmentID)
		WHERE
			(MAE.AccountId = @AccountID)
			AND (EQM.IsCellUnit = 1);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- ** Result
	SELECT * FROM [dbo].[MS_MonitronicsEntityOptions] WHERE (EntityOptionID = @EntityOptionID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityOptionsCellProvGet TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityOptionsCellProvGet 123233 */