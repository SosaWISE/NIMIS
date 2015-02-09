USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSS_DeviceRequestIncrementAttempt')
	BEGIN
		PRINT 'Dropping Procedure custSS_DeviceRequestIncrementAttempt'
		DROP  Procedure  dbo.custSS_DeviceRequestIncrementAttempt
	END
GO

PRINT 'Creating Procedure custSS_DeviceRequestIncrementAttempt'
GO
/******************************************************************************
**		File: custSS_DeviceRequestIncrementAttempt.sql
**		Name: custSS_DeviceRequestIncrementAttempt
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
**		Date: 11/29/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/29/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSS_DeviceRequestIncrementAttempt
(
	@DeviceRequestID BIGINT
	, @IncrementBy INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY	
		BEGIN TRANSACTION
		/** Transfer data */
		
		/** Initialize. */
		DECLARE @CurrentCount INT;
		SELECT @CurrentCount = Attempts FROM [dbo].SS_DeviceRequests WHERE (DeviceRequestID = @DeviceRequestID);
		
		/** Update the count. */
		UPDATE [dbo].SS_DeviceRequests SET Attempts = (@IncrementBy + @CurrentCount), LastAttemptDate = GETUTCDATE() WHERE (DeviceRequestID = @DeviceRequestID);
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Return result */
	SELECT * FROM [dbo].LP_Requests WHERE (RequestID = @DeviceRequestID);

END
GO

GRANT EXEC ON dbo.custSS_DeviceRequestIncrementAttempt TO PUBLIC
GO