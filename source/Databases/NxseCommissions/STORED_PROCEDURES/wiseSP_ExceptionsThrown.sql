USE [NXSE_Commissions]
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
**		Date: 05/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/01/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[wiseSP_ExceptionsThrown]
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
		, @SERVER_NAME NVARCHAR(128)
		, @DATABS_NAME NVARCHAR(128)
		, @SCHEMA_NAME NVARCHAR(128)
		, @TABLE_NAME NVARCHAR(128)
		, @PRIMARY_KEY NVARCHAR(50);

	/** Save Exception information */
	SELECT
		@ERROR_NUMBER = ERROR_NUMBER()-- AS ErrorNumber
		, @ERROR_SEVERITY = ERROR_SEVERITY()-- AS ErrorSeverity
		, @ERROR_STATE = ERROR_STATE()-- AS ErrorState
		, @ERROR_PROCEDURE = ERROR_PROCEDURE()-- AS ErrorProcedure
		, @ERROR_LINE = ERROR_LINE()-- AS ErrorLine
		, @ERROR_MESSAGE = ERROR_MESSAGE()-- AS ErrorMessage
		, @SERVER_NAME = @@SERVERNAME
		, @DATABS_NAME = DB_NAME()
		, @SCHEMA_NAME = SCHEMA_NAME()
		, @TABLE_NAME = NULL
		, @PRIMARY_KEY = NULL;
	PRINT 'EXCEPTION WAS THROWN BY SP ''' + @ERROR_PROCEDURE + '''.'

	/** Throw formated exception to be cought on the .Net application end */
	RAISERROR (
		N'ERROR_NUMBER:%d|ERROR_SEVERITY:%d|ERROR_STATE:%d|ERROR_PROCEDURE:%s|ERROR_LINE:%d|ERROR_MESSAGE:%s|SERVER_NAME:%s|DB_NAME:%s|SCHEMA_NAME:%s|TABLE_NAME:%s|PRIMARY_KEY:%s'
		, 18 -- Severity
		, 1  -- State
		, @ERROR_NUMBER
		, @ERROR_SEVERITY
		, @ERROR_STATE
		, @ERROR_PROCEDURE
		, @ERROR_LINE
		, @ERROR_MESSAGE
		, @SERVER_NAME
		, @DATABS_NAME
		, @SCHEMA_NAME
		, @TABLE_NAME
		, @PRIMARY_KEY
	);

END
GO

GRANT EXEC ON dbo.wiseSP_ExceptionsThrown TO PUBLIC
GO
/** 
SELECT @@SERVERNAME AS 'Server Name', DB_NAME() AS [Current Database], SCHEMA_NAME() AS [Schema Name];
SELECT object_schema_name(parent_id) + '.' + object_name(parent_id) 
*/