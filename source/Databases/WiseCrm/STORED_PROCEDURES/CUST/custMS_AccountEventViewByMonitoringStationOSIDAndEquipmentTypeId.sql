USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId'
		DROP  Procedure  dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId
	END
GO

PRINT 'Creating Procedure custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId'
GO
/******************************************************************************
**		File: custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId.sql
**		Name: custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId
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
CREATE Procedure dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId
(
	@MonitoringStationOSID VARCHAR(50)
	, @EquipmentTypeID INT
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		/** Transfer data */
		SELECT 
			*
		FROM
			[dbo].[vwMS_AccountEvent] AS Events WITH (NOLOCK)
		WHERE
			(Events.MonitoringStationOSID = @MonitoringStationOSID)
			AND (Events.EquipmentTypeID = @EquipmentTypeID);
		
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId TO PUBLIC
GO

/** 
EXEC dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId 'AG_ALARMSYS', 1, 'MSTR001' 
EXEC dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId 'AG_ALARMSYS', 2, 'MSTR001' 
EXEC dbo.custMS_AccountEventViewByMonitoringStationOSIDAndEquipmentTypeId 'AG_ALARMSYS', 3, 'MSTR001' 
*/