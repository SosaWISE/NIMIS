/**********************************************************************************************************************
* DESCRIPTION:  Truncate the database.
*  Below is how to shrink the database file
*	USE UserDB;
*	GO
*	DBCC SHRINKFILE (DataFile1, 7);
*	GO
**********************************************************************************************************************/
USE [Platinum_Protection_InterimCRM]
GO

ALTER DATABASE [Platinum_Protection_InterimCRM] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(Platinum_Protection_InterimCRM, 200)
DBCC SHRINKFILE(Platinum_Protection_InterimCRM_log, 2)
ALTER DATABASE [Platinum_Protection_InterimCRM] SET RECOVERY FULL WITH NO_WAIT
GO
