USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_SessionValidate')
	BEGIN
		PRINT 'Dropping Procedure custAC_SessionValidate'
		DROP  Procedure  dbo.custAC_SessionValidate
	END
GO

PRINT 'Creating Procedure custAC_SessionValidate'
GO
/******************************************************************************
**		File: custAC_SessionValidate.sql
**		Name: custAC_SessionValidate
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
**		Auth: Andrés Sosa
**		Date: 09/03/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/03/2012	Andrés Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAC_SessionValidate
(
	@SessionID BIGINT
	, @ApplicationId VARCHAR(50)
	, @MinutesThreshold INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize */
	SET @MinutesThreshold = (-1) * @MinutesThreshold;
	
	BEGIN TRANSACTION
	/** Transfer data */
	
	/** Check if session is valid */
	IF (EXISTS(SELECT ASS.[SessionID]
		  , ASS.[ApplicationId]
		  , ASS.[UserId]
		  , ASS.[IPAddress]
		  , ASS.[LastAccessedOn]
		  , ASS.[SessionTerminated]
		  , ASS.[CreatedOn]
	FROM
		[dbo].[AC_Sessions] AS ASS WITH (NOLOCK)
	WHERE
		(ASS.LastAccessedOn > DATEADD(minute, @MinutesThreshold, GETDATE()))
		AND (ASS.SessionID = @SessionID)
		AND (ASS.ApplicationId = @ApplicationId)
		AND (ASS.SessionTerminated = 0)
		))
	BEGIN
		PRINT 'Valid Session'
		UPDATE [dbo].[AC_Sessions] SET LastAccessedOn = GETDATE() WHERE SessionID = @SessionID;
	END
	ELSE
	BEGIN
		PRINT 'Invalid Session'
		UPDATE [dbo].[AC_Sessions] SET SessionTerminated = 1 WHERE SessionID = @SessionID;
	END
	
	COMMIT TRANSACTION
	
	/** Return result */
	SELECT
		*
	FROM
		[dbo].[AC_Sessions] AS ASS WITH (NOLOCK)
	WHERE
		(ASS.LastAccessedOn > DATEADD(minute, @MinutesThreshold, GETDATE()))
		AND (ASS.SessionID = @SessionID)
		AND (ASS.ApplicationId = @ApplicationId)
		AND (ASS.SessionTerminated = 0)
		
END
GO

GRANT EXEC ON dbo.custAC_SessionValidate TO PUBLIC
GO

--EXEC dbo.custAC_SessionValidate 442937, 'SSE_CMS_CORS', 30;