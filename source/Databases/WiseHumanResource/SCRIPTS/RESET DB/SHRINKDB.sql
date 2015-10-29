USE [WISE_HumanResource]
GO


BEGIN TRANSACTION

DELETE RU_Recruits;
DELETE RU_UserPhotos;
DELETE RU_Users;
DELETE dbo.RU_UsersHistory;
DELETE dbo.RU_RecruitsHistory;
DELETE dbo.RU_Teams;
DELETE dbo.RU_TeamLocations;

ROLLBACK TRANSACTION