USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_SessionStart')
	BEGIN
		PRINT 'Dropping Procedure custAC_SessionStart'
		DROP  Procedure  dbo.custAC_SessionStart
	END
GO

PRINT 'Creating Procedure custAC_SessionStart'
GO
/******************************************************************************
**		File: custAC_SessionStart.sql
**		Name: custAC_SessionStart
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
**		Date: 04/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAC_SessionStart
(
	@ApplicationId VARCHAR(50)
	, @IPAddress VARCHAR(15)
	, @TimezoneOffset INT = 0
)
AS
BEGIN
	/** Local Declarations */
	DECLARE @SessionID BIGINT;

	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Create Session */
		INSERT INTO [dbo].[AC_Sessions] (
			ApplicationId
			, [IPAddress]
			, [TimezoneOffset]
		) VALUES (
			@ApplicationId
			, @IPAddress
			, @TimezoneOffset
		);

		SET @SessionID = SCOPE_IDENTITY();
			
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	SELECT * FROM [dbo].[AC_Sessions] WHERE SessionID = @SessionID;
	
END
GO

GRANT EXEC ON dbo.custAC_SessionStart TO PUBLIC
GO