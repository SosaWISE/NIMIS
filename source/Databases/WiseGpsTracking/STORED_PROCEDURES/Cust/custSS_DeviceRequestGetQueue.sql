USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSS_DeviceRequestGetQueue')
	BEGIN
		PRINT 'Dropping Procedure custSS_DeviceRequestGetQueue'
		DROP  Procedure  dbo.custSS_DeviceRequestGetQueue
	END
GO

PRINT 'Creating Procedure custSS_DeviceRequestGetQueue'
GO
/******************************************************************************
**		File: custSS_DeviceRequestGetQueue.sql
**		Name: custSS_DeviceRequestGetQueue
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
CREATE Procedure dbo.custSS_DeviceRequestGetQueue
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
		[WISE_GPSTRACKING].[dbo].[SS_DeviceRequests] AS REQ
	WHERE
		(REQ.ProcessDate IS NULL)
		AND (REQ.Attempts < @AttemptNumberPerCmd)
END
GO

GRANT EXEC ON dbo.custSS_DeviceRequestGetQueue TO PUBLIC
GO

/** EXEC dbo.custSS_DeviceRequestGetQueue 5 */