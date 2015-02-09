/**********************************************************************************************************************
* DESCRIPTION:  Truncate the database.
*  Below is how to shrink the database file
*	USE UserDB;
*	GO
*	DBCC SHRINKFILE (DataFile1, 7);
*	GO
**********************************************************************************************************************/
USE [WISE_CRM]
GO

ALTER DATABASE [WISE_CRM] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_CRM_log, 2)
ALTER DATABASE [WISE_CRM] SET RECOVERY FULL WITH NO_WAIT
GO
/** This shows the system files for this database. 
SELECT * FROM sys.database_files */

/** Truncate Authentication database. */
USE [WISE_AuthenticationControl]
GO

ALTER DATABASE [WISE_AuthenticationControl] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_AuthenticationControl_log, 2)
ALTER DATABASE [WISE_AuthenticationControl] SET RECOVERY FULL WITH NO_WAIT
GO

/** Truncate WISE_GPSTRACKING. */
USE [WISE_GPSTRACKING]
GO

ALTER DATABASE [WISE_GPSTRACKING] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(DLGpsSystemDB_log, 2)
ALTER DATABASE [WISE_GPSTRACKING] SET RECOVERY FULL WITH NO_WAIT
GO

/** Truncate WISE_HumanResource. */
USE [WISE_HumanResource]
GO

ALTER DATABASE [WISE_HumanResource] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_HumanResource_log, 2)
ALTER DATABASE [WISE_HumanResource] SET RECOVERY FULL WITH NO_WAIT
GO

/** Truncate NXSE_Letters. */
USE [NXSE_Letters]
GO

ALTER DATABASE [NXSE_Letters] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_Letters_log, 2)
ALTER DATABASE [NXSE_Letters] SET RECOVERY FULL WITH NO_WAIT
GO

/** Truncate NXSE_Licensing. */
USE [NXSE_Licensing]
GO

ALTER DATABASE [NXSE_Licensing] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(WISE_Licensing_log, 2)
ALTER DATABASE [NXSE_Licensing] SET RECOVERY FULL WITH NO_WAIT
GO

/** Truncate NXSE_DoNotCallList. */
USE [NXSE_DoNotCallList]
GO

ALTER DATABASE [NXSE_DoNotCallList] SET RECOVERY SIMPLE WITH NO_WAIT
DBCC SHRINKFILE(NXSE_DoNotCallList_log, 2)
ALTER DATABASE [NXSE_DoNotCallList] SET RECOVERY FULL WITH NO_WAIT
GO

