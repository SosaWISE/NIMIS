USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_UsersGeneralAuthentication')
	BEGIN
		PRINT 'Dropping Procedure custAC_UsersGeneralAuthentication'
		DROP  Procedure  dbo.custAC_UsersGeneralAuthentication
	END
GO

PRINT 'Creating Procedure custAC_UsersGeneralAuthentication'
GO
/******************************************************************************
**		File: custAC_UsersGeneralAuthentication.sql
**		Name: custAC_UsersGeneralAuthentication
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
**		Date: 01/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/18/2012	Andres Sosa		Created By
*******************************************************************************/
CREATE Procedure dbo.custAC_UsersGeneralAuthentication
(
	@Username NVARCHAR(50)
	, @Password NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize */
	DECLARE @UserID INT
	
	/** Check to see if the account exits in Client GPS. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE (Username = @Username) AND ([Password] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE (Username = @Username) AND ([Password] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT');
		RETURN;
	END
	/** Check Model layer. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([MdlUsername] = @Username) AND ([MdlPassword] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([MdlUsername] = @Username) AND ([MdlPassword] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT');
		RETURN;
	END
	/** Check Model layer. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([ClnUsername] = @Username) AND ([ClnPassword] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([ClnUsername] = @Username) AND ([ClnPassword] = @Password) AND (ApplicationId = 'SOS_GPS_CLNT');
		RETURN;
	END
	
	/** Check to see if there is a dealer account. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE (Username = @Username) AND ([Password] = @Password) AND (ApplicationId = 'SOS_CRM')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE (Username = @Username) AND ([Password] = @Password) AND (ApplicationId = 'SOS_CRM');
		RETURN;
	END
	/** Check Model layer. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([MdlUsername] = @Username) AND ([MdlPassword] = @Password) AND (ApplicationId = 'SOS_CRM')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([MdlUsername] = @Username) AND ([MdlPassword] = @Password) AND (ApplicationId = 'SOS_CRM');
		RETURN;
	END
	/** Check Model layer. */
	IF(EXISTS(SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([DlrUsername] = @Username) AND ([DlrPassword] = @Password) AND (ApplicationId = 'SOS_CRM')))
	BEGIN
		SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE ([DlrUsername] = @Username) AND ([DlrPassword] = @Password) AND (ApplicationId = 'SOS_CRM');
		RETURN;
	END
	
	
	/** Throw error */
	RAISERROR (N'Msg:The Username %s with credentials are invalid',
		18, -- Severity
		1, -- State
		@Username);
	PRINT '|CODE:10000|INVALID CREDENTIALS'
	RETURN
	
	/** Return result. */
	SELECT * FROM [dbo].[vwAC_UserGeneralAuthentication] WHERE (Username = @Username);
	
END
GO

GRANT EXEC ON dbo.custAC_UsersGeneralAuthentication TO PUBLIC
GO

EXEC dbo.custAC_UsersGeneralAuthentication 'andres', 'wise';