USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLP_RequestGetQueue')
	BEGIN
		PRINT 'Dropping Procedure custLP_RequestGetQueue'
		DROP  Procedure  dbo.custLP_RequestGetQueue
	END
GO

PRINT 'Creating Procedure custLP_RequestGetQueue'
GO
/******************************************************************************
**		File: custLP_RequestGetQueue.sql
**		Name: custLP_RequestGetQueue
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
CREATE Procedure dbo.custLP_RequestGetQueue
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
		[WISE_GPSTRACKING].[dbo].[LP_Requests] AS REQ
	WHERE
		(REQ.ProcessDate IS NULL)
		AND (REQ.Attempts < @AttemptNumberPerCmd)
END
GO

GRANT EXEC ON dbo.custLP_RequestGetQueue TO PUBLIC
GO

/** EXEC dbo.custLP_RequestGetQueue 5 */