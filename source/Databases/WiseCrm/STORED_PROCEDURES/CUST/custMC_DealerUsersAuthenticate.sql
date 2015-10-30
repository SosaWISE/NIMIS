USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_DealerUsersAuthenticate')
	BEGIN
		PRINT 'Dropping Procedure custMC_DealerUsersAuthenticate'
		DROP  Procedure  dbo.custMC_DealerUsersAuthenticate
	END
GO

PRINT 'Creating Procedure custMC_DealerUsersAuthenticate'
GO
/******************************************************************************
**		File: custMC_DealerUsersAuthenticate.sql
**		Name: custMC_DealerUsersAuthenticate
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
**		Date: 01/16/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/16/2012	Andres Sosa		Created By
*******************************************************************************/
CREATE Procedure dbo.custMC_DealerUsersAuthenticate
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
	
	BEGIN TRANSACTION
	/** Validate SessionId */
	IF (EXISTS(SELECT * FROM dbo.AC_Sessions WHERE (SessionID = @SessionId) 
				AND (DATEADD(mi, 20, LastAccessedOn) <= GETDATE())))
	BEGIN
		DECLARE @SessionIdString VARCHAR(20)
		SET @SessionIdString = CAST(@SessionId AS VARCHAR(20))
		RAISERROR (N'The SessionId %s has expired',
			10, -- Severity
			1, -- State
			@SessionIdString);
		PRINT 'I WAS HERE'
	END
	
	/** Perform authentication against CRM. */
	DECLARE @DealerUserID INT
	SELECT @DealerUserID = DealerUserID FROM dbo.MC_DealerUsers AS DU WITH (NOLOCK) WHERE (DU.Username = @Username AND DU.Password = @Password)
	IF (@DealerUserID IS NOT NULL)
	BEGIN
		/** Initilize the session. */
			DECLARE @UserID INT
			SELECT @UserID = UserID FROM dbo.AC_Users AS AU WITH (NOLOCK) WHERE (AU.Username = @Username AND AU.Password = @Password)
		-- Check that there is a User in AC.
		IF (@UserID IS NULL)
		BEGIN
			/** Init */
			INSERT INTO dbo.AC_Users
			        ( Username ,
			          Password 
			        )
			VALUES  ( @Username ,
			          @Password 
			        )
			SET @UserID = @@Identity
			PRINT 'Got User ID ' + CAST(@UserID AS VARCHAR)
			/** Update Dealer User account with User ID */
			UPDATE dbo.MC_DealerUsers SET
				AuthUserID = @UserID
			WHERE
				(DealerUserID = @DealerUserID)
		END
		ELSE
		BEGIN
			PRINT ('UserID: ' + CAST(@UserID AS VARCHAR));
		END
		
		/** Update AuthControl User entity */
		UPDATE dbo.AC_Users SET
			[Password] = @Password
		WHERE
			(UserID = @UserID)
		/** Update Session */
		UPDATE dbo.AC_Sessions SET
			LastAccessedOn = GETDATE()
		WHERE
			(SessionID = @SessionId)
		/** Update Dealer Last access date. */
		PRINT('Update LastLoginOn');
		UPDATE dbo.MC_DealerUsers SET
			LastLoginOn = GETDATE()
		WHERE
			(DealerUserID = @DealerUserID)
			
		/** Return result */
		SELECT * FROM dbo.MC_DealerUsers AS DU WITH (NOLOCK) WHERE (DU.DealerUserID = @DealerUserID)
	END
	ELSE
	BEGIN
		RAISERROR (N'Invalid credentials.', 10, 1);
	END
	
	COMMIT TRANSACTION
END
GO

GRANT EXEC ON dbo.custMC_DealerUsersAuthenticate TO PUBLIC
GO

EXEC dbo.custMC_DealerUsersAuthenticate 100001, 4000, 'SosaWISE', 'Jugete!98'