USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetEventTypeUi'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetEventTypeUi'
		DROP FUNCTION  dbo.fxGetEventTypeUi
	END
GO

PRINT 'Creating FUNCTION fxGetEventTypeUi'
GO
/******************************************************************************
**		File: fxGetEventTypeUi.sql
**		Name: fxGetEventTypeUi
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
**		Date: 07/02/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/02/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetEventTypeUi
(
	@EventTypeID VARCHAR(20)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	/** Declarations. */
	DECLARE @Result NVARCHAR(50) = 'unknown';

	/** Get the System Type. */
	SET @Result = CASE
			WHEN @EventTypeID = 'EMERG' THEN 'sos' -- Emergency
			WHEN @EventTypeID = 'FALL' THEN 'fall' -- Fall Alert
			WHEN @EventTypeID = 'FENCE' THEN 'enter' -- Fence Breach
			WHEN @EventTypeID = 'FENCE_RT' THEN 'exit' -- Fence Restore
			WHEN @EventTypeID = 'FIRE' THEN 'fire' -- Fire
			WHEN @EventTypeID = 'LOWBAT' THEN 'battery' -- Low Battery Alert
			WHEN @EventTypeID = 'MEDICAL' THEN 'medical' -- Medical
			WHEN @EventTypeID = 'SPEED' THEN 'speed' -- Speed Alert
			WHEN @EventTypeID = 'TAMPER' THEN 'tamper' -- Tamper Alert
			WHEN @EventTypeID = 'TAMPER_RT' THEN 'tamper' -- Tamper
			ELSE 'unknown'
		END 

	/** Return result. */
	RETURN @Result;
END
GO