USE WISE_AuthenticationControl
GO

/* Boundering Testing */
BEGIN TRANSACTION 

/** Local Declarations */
DECLARE @MinutesThreshold INT;
SET @MinutesThreshold = -19;
DECLARE @SessionID BIGINT;

INSERT dbo.AC_Sessions
        ( ApplicationId ,
          UserId ,
          IPAddress ,
          LastAccessedOn ,
          SessionTerminated ,
          CreatedOn
        )
VALUES  ( 'SOS_GPS_CLNT' , -- ApplicationId - varchar(50)
          10001 , -- UserId - int
          '127.0.0.1' , -- IPAddress - varchar(15)
          DATEADD(minute, @MinutesThreshold, GETDATE()) , -- LastAccessedOn - datetime
          0 , -- SessionTerminated - bit
          GETDATE()  -- CreatedOn - datetime
        )
SET @SessionID = @@IDENTITY;

EXEC dbo.custAC_SessionValidate @SessionID, 'SOS_GPS_CLNT', 20;

SELECT * FROM dbo.AC_Sessions WHERE SessionID = @SessionID;

ROLLBACK TRANSACTION