USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custKW_RequestGetQueue')
	BEGIN
		PRINT 'Dropping Procedure custKW_RequestGetQueue'
		DROP  Procedure  dbo.custKW_RequestGetQueue
	END
GO

PRINT 'Creating Procedure custKW_RequestGetQueue'
GO
/******************************************************************************
**		File: custKW_RequestGetQueue.sql
**		Name: custKW_RequestGetQueue
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
CREATE Procedure dbo.custKW_RequestGetQueue
(
	@AttemptNumberPerCmd INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Return result */	
	SELECT 
		*
	FROM
		[WISE_GPSTRACKING].[dbo].[KW_Requests] AS REQ
	WHERE
		(REQ.ProcessDate IS NULL)
		AND (REQ.Attempts < @AttemptNumberPerCmd)
END
GO

GRANT EXEC ON dbo.custKW_RequestGetQueue TO PUBLIC
GO

/** EXEC dbo.custKW_RequestGetQueue 5 */