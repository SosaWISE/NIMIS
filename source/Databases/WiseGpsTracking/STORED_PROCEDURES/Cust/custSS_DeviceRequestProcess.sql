USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSS_DeviceRequestProcess')
	BEGIN
		PRINT 'Dropping Procedure custSS_DeviceRequestProcess'
		DROP  Procedure  dbo.custSS_DeviceRequestProcess
	END
GO

PRINT 'Creating Procedure custSS_DeviceRequestProcess'
GO
/******************************************************************************
**		File: custSS_DeviceRequestProcess.sql
**		Name: custSS_DeviceRequestProcess
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
**		Date: 10/01/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/01/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSS_DeviceRequestProcess
(
	@DeviceRequestID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY	
		BEGIN TRANSACTION
		/** Update the count. */
		UPDATE [dbo].SS_DeviceRequests SET ProcessDate = GETUTCDATE() WHERE (DeviceRequestID = @DeviceRequestID);
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Return result */
	SELECT * FROM [dbo].SS_DeviceRequests WHERE (DeviceRequestID = @DeviceRequestID);

END
GO

GRANT EXEC ON dbo.custSS_DeviceRequestProcess TO PUBLIC
GO