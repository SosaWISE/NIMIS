USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_UsersDealerUsersAuthenticate')
	BEGIN
		PRINT 'Dropping Procedure custAC_UsersDealerUsersAuthenticate'
		DROP  Procedure  dbo.custAC_UsersDealerUsersAuthenticate
	END
GO

PRINT 'Creating Procedure custAC_UsersDealerUsersAuthenticate'
GO
/******************************************************************************
**		File: custAC_UsersDealerUsersAuthenticate.sql
**		Name: custAC_UsersDealerUsersAuthenticate
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
CREATE Procedure dbo.custAC_UsersDealerUsersAuthenticate
(
	@SessionId BIGINT
	, @DealerId BIGINT
	, @Username NVARCHAR(50)
	, @Password NVARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize */
	DECLARE @SessionIdString VARCHAR(20)
	SET @SessionIdString = CAST(@SessionId AS VARCHAR(20))
	
	BEGIN TRANSACTION
	/** Validate SessionId */
	IF (EXISTS(SELECT * FROM WISE_AuthenticationControl.dbo.AC_Sessions WHERE (SessionID = @SessionId) 
				AND (DATEADD(mi, 20, LastAccessedOn) <= GETDATE())))
	BEGIN
		RAISERROR (N'Msg:The SessionId %s has expired',
			18, -- Severity
			1, -- State
			@SessionIdString);
		PRINT '|CODE:20100|SESSION ID EXP'
		ROLLBACK TRANSACTION
		RETURN
	END
	
	/** Perform authentication against CRM. */
	DECLARE @DealerUserID INT
	SELECT @DealerUserID = DealerUserID FROM WISE_CRM.dbo.MC_DealerUsers AS DU WITH (NOLOCK) WHERE (DU.Username = @Username AND DU.Password = @Password AND DU.IsActive = 1)
	IF (@DealerUserID IS NOT NULL)
	BEGIN
		/** Initilize the session. */
			DECLARE @UserID INT
			SELECT @UserID = UserID FROM WISE_AuthenticationControl.dbo.AC_Users AS AU WITH (NOLOCK) WHERE (AU.Username = @Username AND AU.Password = @Password AND AU.IsActive = 1)
		-- Check that there is a User in AC.
		IF (@UserID IS NULL)
		BEGIN
			/** Init */
			INSERT INTO WISE_AuthenticationControl.dbo.AC_Users (
				Username ,
				Password 
			) VALUES (
				@Username ,
				@Password 
			)
			SET @UserID = @@Identity
			PRINT 'Got User ID ' + CAST(@UserID AS VARCHAR)
			/** Update Dealer User account with User ID */
			UPDATE WISE_CRM.dbo.MC_DealerUsers SET
				AuthUserID = @UserID
			WHERE
				(DealerUserID = @DealerUserID)
		END
		ELSE
		BEGIN
			PRINT ('Found UserID in AuthControl DB: ' + CAST(@UserID AS VARCHAR)
			+ ' | Found DealerUserID: ' + CAST(@DealerUserID AS VARCHAR));
		END
		
		/** Update AuthControl User entity */
		UPDATE WISE_AuthenticationControl.dbo.AC_Users SET
			[Password] = @Password
		WHERE
			(UserID = @UserID)
		/** Update Session */
		UPDATE WISE_AuthenticationControl.dbo.AC_Sessions SET
			LastAccessedOn = GETDATE()
		WHERE
			(SessionID = @SessionId)
		/** Update Dealer Last access date. */
		PRINT('Update LastLoginOn');
		UPDATE WISE_CRM.dbo.MC_DealerUsers SET
			LastLoginOn = GETDATE()
		WHERE
			(DealerUserID = @DealerUserID)
			
		/** Bind to unbound SessionID */
		IF (NOT EXISTS(SELECT * FROM [dbo].[AC_Sessions] AS ASS WITH (NOLOCK) WHERE (ASS.SessionID = @SessionId) AND (ASS.UserId IS NULL)))
		BEGIN
			RAISERROR (N'Msg:The SessionId %s has expired, in use, or does not exits.',
				18, -- Severity
				1, -- State
				@SessionIdString);
			PRINT '|CODE:20110|SESSION ID USE OR EXP'
			ROLLBACK TRANSACTION
			RETURN
		END
		
		/** Bind session to AuthUser */
		UPDATE [dbo].[AC_Sessions] SET
			UserId = @UserID
		WHERE
			(SessionID = @SessionID);
			
		/** Return result */
		--SELECT * FROM WISE_CRM.dbo.MC_DealerUsers AS DU WITH (NOLOCK) WHERE (DU.DealerUserID = @DealerUserID)
		SELECT 
			*
		FROM
			[dbo].[vwAC_UsersDealerUsersAuthenticate] AS VW
		WHERE
			(VW.DealerUserID = @DealerUserID)
			AND (VW.SessionID = @SessionId)
	END
	ELSE
	BEGIN
		RAISERROR (N'Invalid credentials.', 18, 1);
		PRINT '|CODE:20210|INVALID CREDENTIALS'
		ROLLBACK TRANSACTION
		RETURN
	END
	
	COMMIT TRANSACTION
END
GO

GRANT EXEC ON dbo.custAC_UsersDealerUsersAuthenticate TO PUBLIC
GO

--EXEC dbo.custAC_UsersDealerUsersAuthenticate 100411, 5000, 'SosaWISE', 'Jugete!98'