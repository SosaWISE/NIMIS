/** WISE CRM Clean up script for the new database. */
USE [WISE_AuthenticationControl]
GO

BEGIN TRANSACTION

DELETE [dbo].[AC_Authentications];
DBCC CHECKIDENT ('[dbo].[AC_Authentications]', RESEED, 1);
DELETE [dbo].[AC_Sessions];
DBCC CHECKIDENT('[dbo].[AC_Sessions]', RESEED, 100001);
DELETE [dbo].[AC_Users] WHERE (UserID > 1) AND (UserID <> 10001);
UPDATE [dbo].[AC_Users] SET Password = 'N3xs3ns3!!' WHERE UserID = 0;
DBCC CHECKIDENT('[dbo].[AC_Users]', RESEED, 10000);

ROLLBACK TRANSACTION

/** TRUNCATE LOGS AND DATA. */
ALTER DATABASE [WISE_AuthenticationControl] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_AuthenticationControl_log, 2)
ALTER DATABASE [WISE_AuthenticationControl] SET RECOVERY FULL WITH NO_WAIT
GO