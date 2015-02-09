USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetEventDescUi'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetEventDescUi'
		DROP FUNCTION  dbo.fxGetEventDescUi
	END
GO

PRINT 'Creating FUNCTION fxGetEventDescUi'
GO
/******************************************************************************
**		File: fxGetEventDescUi.sql
**		Name: fxGetEventDescUi
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
CREATE FUNCTION dbo.fxGetEventDescUi
(
	@AccountName NVARCHAR(100)
	, @EventTypeID VARCHAR(20)
)
RETURNS NVARCHAR(150)
AS
BEGIN
	/** Declarations. */
	DECLARE @Result NVARCHAR(150) = 'unknown';

	/** Get the System Type. */
	SET @Result = CASE
			WHEN @EventTypeID = 'EMERG' THEN @AccountName + '''s SOS button pressed.' -- Emergency
			WHEN @EventTypeID = 'FALL' THEN @AccountName + '''s device detected a Fall Alert.' -- Fall Alert
			WHEN @EventTypeID = 'FENCE' THEN @AccountName + ' entered geo fence.' -- Fence Breach
			WHEN @EventTypeID = 'FENCE_RT' THEN @AccountName + ' exited geo fence.' -- Fence Restore
			WHEN @EventTypeID = 'FIRE' THEN @AccountName + '''s device issued a Fire Alert.' -- Fire
			WHEN @EventTypeID = 'LOWBAT' THEN @AccountName + '''s device issued Low Battery Alert.' -- Low Battery Alert
			WHEN @EventTypeID = 'MEDICAL' THEN @AccountName + '''s device issued Medical Alert.' -- Medical
			WHEN @EventTypeID = 'SPEED' THEN @AccountName + ' is going too fast.' -- Speed Alert
			WHEN @EventTypeID = 'TAMPER' THEN @AccountName + '''s device issued a Tamper Alert.' -- Tamper Alert
			WHEN @EventTypeID = 'TAMPER_RT' THEN @AccountName + '''s device issued a Tamper.' -- Tamper
			ELSE @AccountName + '''s device issued an alert.'
		END 

	/** Return result. */
	RETURN @Result;
END
GO