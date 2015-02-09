USE [NXSE_DoNotCallList]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'wiseSP_ExceptionsThrown')
	BEGIN
		PRINT 'Dropping Procedure wiseSP_ExceptionsThrown'
		DROP  Procedure  dbo.wiseSP_ExceptionsThrown
	END
GO

PRINT 'Creating Procedure wiseSP_ExceptionsThrown'
GO
/******************************************************************************
**		File: wiseSP_ExceptionsThrown.sql
**		Name: wiseSP_ExceptionsThrown
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
**		Date: 05/24/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/24/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.wiseSP_ExceptionsThrown
--(
--	@PARAM INT
--	, @PARAM2 NVARCHAR(25) OUTPUT
--)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Capture exceptions and format the throw. */	
	DECLARE @ERROR_NUMBER INT
		, @ERROR_SEVERITY INT
		, @ERROR_STATE INT
		, @ERROR_PROCEDURE VARCHAR(500)
		, @ERROR_LINE INT
		, @ERROR_MESSAGE VARCHAR(4000)
	/** Save Exception information */
	SELECT
		@ERROR_NUMBER = ERROR_NUMBER()-- AS ErrorNumber
		, @ERROR_SEVERITY = ERROR_SEVERITY()-- AS ErrorSeverity
		, @ERROR_STATE = ERROR_STATE()-- AS ErrorState
		, @ERROR_PROCEDURE = ERROR_PROCEDURE()-- AS ErrorProcedure
		, @ERROR_LINE = ERROR_LINE()-- AS ErrorLine
		, @ERROR_MESSAGE = ERROR_MESSAGE()-- AS ErrorMessage;
	PRINT 'EXCEPTION WAS THROWN BY SP ''' + @ERROR_PROCEDURE + '''.'

	/** Throw formated exception to be cought on the .Net application end */
	RAISERROR (
		N'ERROR_NUMBER:%d|ERROR_SEVERITY:%d|ERROR_STATE:%d|ERROR_PROCEDURE:%s|ERROR_LINE:%d|ERROR_MESSAGE:%s'
		, 18 -- Severity
		, 1  -- State
		, @ERROR_NUMBER
		, @ERROR_SEVERITY
		, @ERROR_STATE
		, @ERROR_PROCEDURE
		, @ERROR_LINE
		, @ERROR_MESSAGE
	);

END
GO

GRANT EXEC ON dbo.wiseSP_ExceptionsThrown TO PUBLIC
GO