USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentAccountZoneTypeEventsViewGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentAccountZoneTypeEventsViewGet'
		DROP  Procedure  dbo.custMS_EquipmentAccountZoneTypeEventsViewGet
	END
GO

PRINT 'Creating Procedure custMS_EquipmentAccountZoneTypeEventsViewGet'
GO
/******************************************************************************
**		File: custMS_EquipmentAccountZoneTypeEventsViewGet.sql
**		Name: custMS_EquipmentAccountZoneTypeEventsViewGet
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
CREATE Procedure dbo.custMS_EquipmentAccountZoneTypeEventsViewGet
(
	@EquipmentId VARCHAR(50)
	, @EquipmentAccountZoneTypeId INT
	, @MonitoringStationOSId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @DefaultAccountEventID INT;

	/** Initialize data */
	SELECT @DefaultAccountEventID = AccountEventId FROM [dbo].[MS_Equipments] WHERE (EquipmentID = @EquipmentId);
	
	BEGIN TRY
		SELECT 
			AccountEventID
			, EquipmentAccountZoneTypeId
			, MonitoringStationOSId
			, EventID
			, Description
			, CAST(CASE
				WHEN AccountEventID = @DefaultAccountEventID THEN 1
				ELSE 0
			  END AS BIT) AS IsDefault
		FROM
			[dbo].[vwMs_EquipmentAccountZoneTypeEvents] 
		WHERE 
			(EquipmentAccountZoneTypeId = @EquipmentAccountZoneTypeId)
			AND (MonitoringStationOSId = @MonitoringStationOSId);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_EquipmentAccountZoneTypeEventsViewGet TO PUBLIC
GO

/**

SELECT * FROM vwMS_EquipmentAccountZoneTypeEvents

EXEC dbo.custMS_EquipmentAccountZoneTypeEventsViewGet 'EQPM_INVT32',  17, 'AG_ALARMSYS'

SELECT * FROM AE_Items WHERE ItemSKU='2GIG-KEY2-345'
EXEC dbo.custMS_EquipmentAccountZoneTypesViewGet 'EQPM_INVT32'

*/