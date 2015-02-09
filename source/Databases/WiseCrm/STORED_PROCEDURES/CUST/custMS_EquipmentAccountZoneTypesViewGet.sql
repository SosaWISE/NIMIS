USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentAccountZoneTypesViewGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentAccountZoneTypesViewGet'
		DROP  Procedure  dbo.custMS_EquipmentAccountZoneTypesViewGet
	END
GO

PRINT 'Creating Procedure custMS_EquipmentAccountZoneTypesViewGet'
GO
/******************************************************************************
**		File: custMS_EquipmentAccountZoneTypesViewGet.sql
**		Name: custMS_EquipmentAccountZoneTypesViewGet
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
**		Date: 06/30/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/30/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_EquipmentAccountZoneTypesViewGet
(
	@EquipmentID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountZoneTypeId VARCHAR(10);

	/** Initialize data */
	SELECT @AccountZoneTypeId = AccountZoneTypeId FROM [dbo].[MS_Equipments] WHERE (EquipmentID = @EquipmentId);
	
	BEGIN TRY
		SELECT
			EquipmentAccountZoneTypeID
			, EquipmentID
			, AccountZoneTypeId
			, AccountZoneType
			, CAST(CASE
				WHEN AccountZoneTypeId = @AccountZoneTypeId THEN 1
				ELSE 0
			  END AS BIT) AS IsDefault
		FROM
			[dbo].[vwMS_EquipmentAccountZoneTypes]
		WHERE
			(EquipmentId = @EquipmentID);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_EquipmentAccountZoneTypesViewGet TO PUBLIC
GO

/**

EXEC dbo.custMS_EquipmentAccountZoneTypesViewGet 'EQPM_INVT32'

*/