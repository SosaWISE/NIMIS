USE [WISE_CRM]
GO
SELECT name, size ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB
FROM sys.database_files;

/** WISE_CRM */
DBCC SHRINKDATABASE (WISE_CRM, 7)
GO
DBCC SHRINKFILE (WISE_CRM, 7);
GO
DBCC SHRINKFILE (WISE_CRM_log, 7);
GO

USE [NXSE_DoNotCallList]
GO
SELECT name, size ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB
FROM sys.database_files;
/** NXSE_DoNotCallList */
DBCC SHRINKDATABASE (NXSE_DoNotCallList, 7)
GO
DBCC SHRINKFILE (NXSE_DoNotCallList, 7);
GO
DBCC SHRINKFILE (NXSE_DoNotCallList_log, 7);
GO


BACKUP DATABASE [WISE_CRM] TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Backup\WISE_CRM.bak';