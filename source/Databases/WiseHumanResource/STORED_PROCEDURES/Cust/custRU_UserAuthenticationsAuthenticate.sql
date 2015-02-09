USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UserAuthenticationsAuthenticate')
	BEGIN
		PRINT 'Dropping Procedure custRU_UserAuthenticationsAuthenticate'
		DROP  Procedure  dbo.custRU_UserAuthenticationsAuthenticate
	END
GO

PRINT 'Creating Procedure custRU_UserAuthenticationsAuthenticate'
GO
/******************************************************************************
**		File: custRU_UserAuthenticationsAuthenticate.sql
**		Name: custRU_UserAuthenticationsAuthenticate
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_UserAuthenticationsAuthenticate
(
	@Username NVARCHAR(50)
	, @Password NVARCHAR(50)
	, @IPAddress VARCHAR(16)
)
AS
BEGIN
	/** Turn couting off. */
	SET NOCOUNT ON
	
	/** Initialize */
	DECLARE @TokenID UNIQUEIDENTIFIER
	SET @TokenID = NEWID()
	DECLARE @XNumberOf INT
	DECLARE @UserID INT
	SET @UserID = NULL
	DECLARE @IsLocked BIT
	SET @IsLocked = 0
	
	/** Authenticate */
	SELECT @UserID = UserID, @IsLocked = IsLocked FROM RU_Users WHERE (Username = @Username) AND ([Password] = @Password) AND (IsActive = 1) AND (IsDeleted = 0)
	
	/** Check to see if it's locked.
		If this is so then exit Stored Procedure. */
	IF (@IsLocked = 1)
	BEGIN
		PRINT '010: ACCOUNT IS LOCKED.'
		/** Set an action */
		INSERT INTO dbo.RU_UserAuthentication (
			TokenID,
			UserId,
			Username,
			[Password],
			IPAddress,
			Successfull,
			[Message]
		) VALUES (
			@TokenID, -- ID - uniqueidentifier
			@UserID, -- UserId - int
			@Username, -- Username - nvarchar(50)
			@Password, -- Password - nvarchar(50)
			@IPAddress, -- IPAddress - varchar(16)
			0, -- Successfull
			'Failed to authenticate.  Invalid credentials or account is locked.' -- Message
		)
		
		/** Return */
		SELECT * FROM [dbo].RU_UserAuthentication WHERE (UserId = @UserID) AND (Successfull = 0) ORDER BY UserAuthenticationID DESC
		RETURN;
	END
	
	/** Save Attempt */
	IF (@UserID IS NOT NULL)
	BEGIN
		PRINT '100: Found UserID with Username and Password: ' + CAST(@UserID AS VARCHAR)

		/** Save a successfull result. */
		INSERT INTO dbo.RU_UserAuthentication (
			TokenID,
			UserId,
			Username,
			[Password],
			IPAddress,
			Successfull,
			[Message]
		) VALUES (
			@TokenID, -- ID - uniqueidentifier
			@UserID, -- UserId - int
			@Username, -- Username - nvarchar(50)
			@Password, -- Password - nvarchar(50)
			@IPAddress, -- IPAddress - varchar(16)
			1, -- Successfull
			'Successfull login.' -- Message
		)
		
		/** Return result. */
		SELECT * FROM [dbo].RU_UserAuthentication WHERE (UserId = @UserID) ORDER BY UserAuthenticationID DESC;
		RETURN;
	END
	ELSE
	BEGIN
		PRINT '200: NOT FOUND UserID with Username and Password.'
		
		/** Find user  */
		SELECT @UserID = UserID FROM [dbo].RU_Users WHERE (UserName = @Username) AND (IsActive = 1);
		
		/** Check to see if it was found. */
		IF (@UserID IS NOT NULL)
		BEGIN
			/** Print Result. */
			PRINT '210: FOUND BY USERNAME: ' + CAST(@UserID AS VARCHAR)
			
			/** Check to see if there were more than X number of attempts in the last 15 minutes from the same IP Address. */
			SELECT 
				@XNumberOf = COUNT(*)
			FROM 
				[dbo].RU_UserAuthentication AS RUA WITH (NOLOCK)
			WHERE
				(RUA.UserId = @UserID)
				AND (RUA.IPAddress = @IPAddress)
				AND (RUA.CreatedDate BETWEEN DATEADD(minute, -15, GETDATE()) AND GETDATE())
				AND (RUA.Successfull = 0)

			/** Check that there is no more than three (3) failed attempts in the last 15 minutes. */
			IF (@XNumberOf >= 3)
			BEGIN
				PRINT '220: TOO MANY FAILED ATTEMPTS IN LAST 15 MINUTES: ' + CAST(@XNumberOf AS VARCHAR)
				INSERT INTO dbo.RU_UserAuthentication (
					TokenID,
					UserId,
					Username,
					[Password],
					IPAddress,
					Successfull,
					[Message]
				) VALUES (
					@TokenID, -- ID - uniqueidentifier
					@UserID, -- UserId - int
					@Username, -- Username - nvarchar(50)
					@Password, -- Password - nvarchar(50)
					@IPAddress, -- IPAddress - varchar(16)
					0, -- Failed
					'Too many failed attempts for a given user account is locked.' -- Message
				)
				
				/** Lock the account. */
				PRINT '230: ACCOUNT IS BEING LOCKED.'
				UPDATE [dbo].RU_Users SET IsLocked = 1 WHERE UserID = @UserID;

				/** Print Result */
				SELECT * FROM [dbo].RU_UserAuthentication WHERE (UserId = @UserID) ORDER BY UserAuthenticationID DESC;
				RETURN;
			END

			PRINT '240: ANOTHER FAILED ATTEMPT.'
			/** Save Actions. */
			/** Save a successfull result. */
			INSERT INTO dbo.RU_UserAuthentication (
				TokenID,
				UserId,
				Username,
				[Password],
				IPAddress,
				Successfull,
				[Message]
			) VALUES (
				@TokenID, -- ID - uniqueidentifier
				@UserID, -- UserId - int
				@Username, -- Username - nvarchar(50)
				@Password, -- Password - nvarchar(50)
				@IPAddress, -- IPAddress - varchar(16)
				0, -- Successfull
				'Failed to authenticate.  Invalid credentials.' -- Message
			)

			/** Print Result. */
			SELECT * FROM [dbo].RU_UserAuthentication WHERE (UserId = @UserID) AND (Successfull = 0) ORDER BY UserAuthenticationID DESC;
			RETURN;

		END
		ELSE
		BEGIN
			/** Initialization. */
			DECLARE @UserAuthenticationID BIGINT

			PRINT '250: INVALID USERNAME: ''' + @Username + ''''
			/** Save Actions. */
			/** Save a successfull result. */
			INSERT INTO dbo.RU_UserAuthentication (
				TokenID,
				UserId,
				Username,
				[Password],
				IPAddress,
				Successfull,
				[Message]
			) VALUES (
				@TokenID, -- ID - uniqueidentifier
				@UserID, -- UserId - int
				@Username, -- Username - nvarchar(50)
				@Password, -- Password - nvarchar(50)
				@IPAddress, -- IPAddress - varchar(16)
				0, -- Successfull
				'Failed to authenticate.  Invalid username or locked account.' -- Message
			)
			
			/** Get Identity. */
			SET @UserAuthenticationID = @@IDENTITY

			/** Print Result. */
			SELECT * FROM [dbo].RU_UserAuthentication WHERE (UserAuthenticationID = @UserAuthenticationID);
			RETURN;
		END
	END
END
GO

GRANT EXEC ON dbo.custRU_UserAuthenticationsAuthenticate TO PUBLIC
GO