USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_UsersCrmAuthentication')
	BEGIN
		PRINT 'Dropping Procedure custAC_UsersCrmAuthentication'
		DROP  Procedure  dbo.custAC_UsersCrmAuthentication
	END
GO

PRINT 'Creating Procedure custAC_UsersCrmAuthentication'
GO
/******************************************************************************
**		File: custAC_UsersCrmAuthentication.sql
**		Name: custAC_UsersCrmAuthentication
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
**		Date: 11/04/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/04/2013	Andres Sosa		Created By
**	09/15/2014  reagan			added UserEmployeeTypeID and UserEmployeeTypeName
**  09/18/2014  reagan          added SecurityLevel
*******************************************************************************/
CREATE Procedure dbo.custAC_UsersCrmAuthentication
(
	@username VARCHAR(50),
	@password VARCHAR(20),
	@SessionId BIGINT,
	@ApplicationId VARCHAR(50),
	@Groups NVARCHAR(MAX)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Initialize. */
	DECLARE @End DATETIME = GETDATE();
	DECLARE @Start DATETIME = DATEADD(mi, -20, @End);
	
	/** Check session validation. */
	IF (NOT EXISTS (SELECT * FROM [dbo].AC_Sessions AS SESS WITH (NOLOCK) WHERE (SESS.SessionID = @SessionId) AND (SESS.SessionTerminated = 0) AND (SESS.LastAccessedOn BETWEEN @Start AND @End)))
	BEGIN
		PRINT 'Session is bad. START: ''' + CAST(@Start AS VARCHAR) + '''| END: ''' + CAST(@End AS VARCHAR) + '''';
		
		UPDATE [dbo].AC_Sessions SET
			SessionTerminated = 1
		WHERE
			(SessionID = @SessionId);

		/** This returns an empty table. */
		SELECT * FROM [dbo].[vwAC_UsersAppAuthentication] AS USR WITH (NOLOCK) WHERE (USR.UserID IS NULL);

		/** Exit SP. */
		return;
	END

	/** Get USER information if you can authenticate */
	DECLARE @userTable AS TABLE(
		UserID INT NOT NULL
		, DealerId INT NOT NULL
		, HRUserID INT
		, GPEmployeeID NVARCHAR(25)
		, SSID UNIQUEIDENTIFIER
		, Username VARCHAR(50)
		, [Password] NVARCHAR(20)
		, FullName NVARCHAR(101)
		, FirstName NVARCHAR(50)
		, LastName NVARCHAR(50)
		, UserEmployeeTypeID NVARCHAR(50)
		, UserEmployeeTypeName NVARCHAR(50)
		, SecurityLevel TINYINT
		, SessionId BIGINT
		, IsActive BIT
		, IsDeleted BIT);
	INSERT INTO @userTable
	SELECT 
		USR.[UserID]
		, USR.DealerId
		, USR.[HRUserId]
		, USR.[GPEmployeeID]
		, USR.[SSID]
		, USR.[Username]
		, USR.[Password]
		, USR.[FullName]
		, USR.[FirstName]
		, USR.[LastName]
		, USR.[UserEmployeeTypeID]
		, USR.[UserEmployeeTypeName]
		, dbo.fxGetUserSecurityLevelByUserId(USR.[HRUserId]) AS 'SecurityLevel'
		, @SessionId AS [SessionId]
		, USR.[IsActive]
		, USR.[IsDeleted]
	FROM
		[dbo].[vwAC_UsersAppAuthentication] AS USR WITH (NOLOCK)
		INNER JOIN [dbo].AC_UserACLs AS ACL WITH (NOLOCK)
		ON
			(USR.UserID = ACL.UserId)

	WHERE
		(USR.Username = @username)
		AND (USR.[Password] = @password)
		AND (ACL.ApplicationId = @ApplicationId)
		AND (USR.IsActive = 1) AND (USR.IsDeleted = 0);

	/** Try to authenticate the user */
	IF (EXISTS(SELECT * FROM @userTable))
	BEGIN
		/** Reset lastaccess colunm of session */
		UPDATE [dbo].AC_Sessions SET
			LastAccessedOn = GetDate()
			, UserId = USR.UserID
			, Groups = @Groups
		FROM
			[dbo].AC_Sessions AS SESS WITH (NOLOCK)
			INNER JOIN @userTable AS USR
			ON
				(SESS.SessionID = USR.SessionId)
		WHERE
			(SESS.SessionID = @SessionId);

		/** Create an Authentication tuple */
		INSERT INTO dbo.AC_Authentications
		SELECT 
			@SessionId AS [SessionId]
			, USR.UserId
			, USR.Username
			, USR.Password
			, 1 -- Success
			, GETDATE()  -- CreatedOn - datetime
		FROM
			@userTable AS USR;
	END
	ELSE
	BEGIN
		/** Set the authentication info */
		INSERT dbo.AC_Authentications(SessionId, UserId, Username, Password, IsSuccessful, CreatedOn)	VALUES 
		(
			@SessionId
			, 0 -- UserId - int
			, @Username -- Username - nvarchar(50)
			, @Password -- Password - nvarchar(20)
			, 0 -- Failure
			, GETDATE()  -- CreatedOn - datetime
		);
	END

	/** Perform result query */
	SELECT * FROM @userTable;
END
GO

GRANT EXEC ON dbo.custAC_UsersCrmAuthentication TO PUBLIC
GO

/**
UPDATE [dbo].AC_Sessions SET LastAccessedOn = GetDate(), SessionTerminated = 0 WHERE SessionID = 212240;
EXEC dbo.custAC_UsersCrmAuthentication 'DevUser', 'NexSense', 432943, 'SSE_CMS_CORS';


	/** Initialize. */
	DECLARE @Start DATETIME = GETDATE();
	DECLARE @End DATETIME = DATEADD(mi, 20, @Start);
	DECLARE @SessionId BIGINT = 100175;
	
	/** Check session validation. */
	SELECT * FROM [dbo].AC_Sessions AS SESS WITH (NOLOCK) WHERE (SESS.SessionID = @SessionId) AND (SESS.SessionTerminated = 0) AND (SESS.LastAccessedOn BETWEEN @Start AND @End);
	PRINT 'Session is bad. START: ''' + CAST(@Start AS VARCHAR) + '''| END: ''' + CAST(@End AS VARCHAR) + '''';

*/

/**
SELECT 
	HRU.*
FROM
	[dbo].AC_Users AS URS WITH (NOLOCK)
	INNER JOIN [dbo].RU_Users AS HRU WITH (NOLOCK)
	ON
		(URS.HRUserID = HRU.UserID)
WHERE
	(URS.UserID = 10001);

SELECT * FROM [dbo].AC_Users AS HRU WITH (NOLOCK) WHERE UserID = 10001; */