USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fn_GetDeviceStatus')
	BEGIN
		PRINT 'Dropping FUNCTION fn_GetDeviceStatus'
		DROP FUNCTION  dbo.fn_GetDeviceStatus
	END
GO

PRINT 'Creating FUNCTION fn_GetDeviceStatus'
GO
/******************************************************************************
**		File: fn_GetDeviceStatus.sql
**		Name: fn_GetDeviceStatus
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
**		Auth: Andrés E. Sosa
**		Date: 05/26/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/26/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fn_GetDeviceStatus
(
	@DeviceStatusID VARCHAR(3)
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Initialize. */
	DECLARE @DeviceStatus varchar(50);

	/** Get Event Code */
	SELECT @DeviceStatus = DeviceStatus FROM [dbo].LP_DeviceStatus WHERE (DeviceStatusID = @DeviceStatusID);
	
	/** Check that it was found */
	IF (@DeviceStatus IS NULL)
	BEGIN
		SELECT @DeviceStatus = DeviceStatus FROM [dbo].LP_DeviceStatus WHERE (DeviceStatusID = '_UD');
	END
	
	RETURN @DeviceStatus;
END
GO