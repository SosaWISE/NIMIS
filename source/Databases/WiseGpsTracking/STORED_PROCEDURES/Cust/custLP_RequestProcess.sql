USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custKW_RequestProcess')
	BEGIN
		PRINT 'Dropping Procedure custKW_RequestProcess'
		DROP  Procedure  dbo.custKW_RequestProcess
	END
GO

PRINT 'Creating Procedure custKW_RequestProcess'
GO
/******************************************************************************
**		File: custKW_RequestProcess.sql
**		Name: custKW_RequestProcess
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
CREATE Procedure dbo.custKW_RequestProcess
(
	@RequestID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY	
		BEGIN TRANSACTION
		/** Update the count. */
		UPDATE [dbo].KW_Requests SET ProcessDate = GETUTCDATE() WHERE (RequestID = @RequestID);
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Return result */
	SELECT * FROM [dbo].KW_Requests WHERE (RequestID = @RequestID);

END
GO

GRANT EXEC ON dbo.custKW_RequestProcess TO PUBLIC
GO