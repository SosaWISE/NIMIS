USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentLocationsByMSOID')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentLocationsByMSOID'
		DROP  Procedure  dbo.custMS_EquipmentLocationsByMSOID
	END
GO

PRINT 'Creating Procedure custMS_EquipmentLocationsByMSOID'
GO
/******************************************************************************
**		File: custMS_EquipmentLocationsByMSOID.sql
**		Name: custMS_EquipmentLocationsByMSOID
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_EquipmentLocationsByMSOID
(
	@MonitoringStationOSID VARCHAR(50)
	, @GpEmployeeID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		/** Transfer data */
		SELECT
			MEL.[EquipmentLocationID]
			, MEL.[EquipmentLocationDesc]
			, MEL.[MonitronicsCode]
			, MEL.[CriticomCode]
			, MEL.[AvantGuardCode]
			, CASE
				WHEN @MonitoringStationOSID = 'MI_MASTER' THEN MEL.[MonitronicsCode]
				WHEN @MonitoringStationOSID = 'CC_MASTER' THEN MEL.[CriticomCode]
				ELSE MEL.[AvantGuardCode]
			  END AS [LocationCode]
			, MEL.[IsActive]
			, MEL.[IsDeleted]
		FROM
			[dbo].vwMS_EquipmentLocations AS MEL WITH (NOLOCK)
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_EquipmentLocationsByMSOID TO PUBLIC
GO

/** EXEC dbo.custMS_EquipmentLocationsByMSOID '' */