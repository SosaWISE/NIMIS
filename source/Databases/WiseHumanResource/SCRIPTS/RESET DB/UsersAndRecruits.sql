/** WISE CRM Clean up script for the new database. */
USE [WISE_HumanResource]
GO

BEGIN TRANSACTION

DELETE [dbo].[RU_Recruits] WHERE UserID > 1;
DBCC CHECKIDENT('[dbo].[RU_Recruits]', RESEED, 1000);
DELETE [dbo].[RU_Users] WHERE UserID > 1;
DBCC CHECKIDENT ('[dbo].[RU_Users]', RESEED, 100);
DELETE [dbo].[RU_UsersHistory];
DBCC CHECKIDENT ('[dbo].[RU_UsersHistory]', RESEED, 1);

ROLLBACK TRANSACTION

/** TRUNCATE LOGS AND DATA. */
ALTER DATABASE [WISE_HumanResource] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_HumanResource_log, 2)
ALTER DATABASE [WISE_HumanResource] SET RECOVERY FULL WITH NO_WAIT
GO