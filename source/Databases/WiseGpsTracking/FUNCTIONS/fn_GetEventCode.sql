USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fn_GetEventCode')
	BEGIN
		PRINT 'Dropping FUNCTION fn_GetEventCode'
		DROP FUNCTION  dbo.fn_GetEventCode
	END
GO

PRINT 'Creating FUNCTION fn_GetEventCode'
GO
/******************************************************************************
**		File: fn_GetEventCode.sql
**		Name: fn_GetEventCode
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
**		Date: 11/16/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/16/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fn_GetEventCode
(
	@EventCodeID VARCHAR(3)
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Initialize. */
	DECLARE @EventCode varchar(50);

	/** Get Event Code */
	SELECT @EventCode = EventCode FROM [dbo].LP_EventCodes WHERE (EventCodeID = @EventCodeID);
	
	/** Check that it was found */
	IF (@EventCode IS NULL)
	BEGIN
		SELECT @EventCode = EventCode FROM [dbo].LP_EventCodes WHERE (EventCodeID = '_UD');;
	END
	
	RETURN @EventCode;
END
GO