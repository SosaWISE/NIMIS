USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custKW_RequestIncrementAttempt')
	BEGIN
		PRINT 'Dropping Procedure custKW_RequestIncrementAttempt'
		DROP  Procedure  dbo.custKW_RequestIncrementAttempt
	END
GO

PRINT 'Creating Procedure custKW_RequestIncrementAttempt'
GO
/******************************************************************************
**		File: custKW_RequestIncrementAttempt.sql
**		Name: custKW_RequestIncrementAttempt
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
**		Date: 03/11/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	03/11/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custKW_RequestIncrementAttempt
(
	@RequestID BIGINT
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
		SELECT @CurrentCount = Attempts FROM [dbo].KW_Requests WHERE (RequestID = @RequestID);
		
		/** Update the count. */
		UPDATE [dbo].KW_Requests SET Attempts = (@IncrementBy + @CurrentCount), LastAttempDate = GETUTCDATE() WHERE (RequestID = @RequestID);
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Return result */
	SELECT * FROM [dbo].LP_Requests WHERE (RequestID = @RequestID);

END
GO

GRANT EXEC ON dbo.custKW_RequestIncrementAttempt TO PUBLIC
GO