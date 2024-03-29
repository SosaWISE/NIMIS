USE [WISE_HumanResource]
GO
/****** Object:  Trigger [dbo].[trgRU_UsersSaveHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trgRU_UsersSaveHistory'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trgRU_UsersSaveHistory'
	DROP TRIGGER [dbo].[trgRU_UsersSaveHistory]
END
GO

PRINT 'CREATING TRIGGER trgRU_UsersSaveHistory'
GO
/******************************************************************************
**		File: trgRU_UsersSaveHistory.sql
**		Name: trgRU_UsersSaveHistory
**		Desc: 
**
**		This trigger will save everytime there is a change in the alarmnet table
**			this is used for looking back at all the serial numbers used.
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés Sosa
**		Date: 04/22/2014
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/22/2014	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[trgRU_UsersSaveHistory]
   ON  [dbo].[RU_Users]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[RU_UsersHistory] (
	[UserID]
	,[FullName]
	,[PublicFullName]
	,[RecruitedByID]
	,[GPEmployeeID]
	,[UserEmployeeTypeId]
	,[PermanentAddressID]
	,[SSN]
	,[FirstName]
	,[MiddleName]
	,[LastName]
	,[PreferredName]
	,[CompanyName]
	,[MaritalStatus]
	,[SpouseName]
	,[UserName]
	,[Password]
	,[BirthDate]
	,[HomeTown]
	,[BirthCity]
	,[BirthState]
	,[BirthCountry]
	,[Sex]
	,[ShirtSize]
	,[HatSize]
	,[DLNumber]
	,[DLState]
	,[DLCountry]
	,[DLExpiresOn]
	,[Height]
	,[Weight]
	,[EyeColor]
	,[HairColor]
	,[PhoneHome]
	,[PhoneCell]
	,[PhoneCellCarrierID]
	,[PhoneFax]
	,[Email]
	,[CorporateEmail]
	,[TreeLevel]
	,[HasVerifiedAddress]
	,[RightToWorkExpirationDate]
	,[RightToWorkNotes]
	,[RightToWorkStatusID]
	,[IsLocked]
	,[IsActive]
	,[IsDeleted]
	,[RecruitedDate]
	,[CreatedBy]
	,[CreatedOn]
	,[ModifiedBy]
	,[ModifiedOn]
) 
SELECT [UserID]
      ,[FullName]
      ,[PublicFullName]
      ,[RecruitedByID]
      ,[GPEmployeeID]
      ,[UserEmployeeTypeId]
      ,[PermanentAddressID]
      ,[SSN]
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
      ,[PreferredName]
      ,[CompanyName]
      ,[MaritalStatus]
      ,[SpouseName]
      ,[UserName]
      ,[Password]
      ,[BirthDate]
      ,[HomeTown]
      ,[BirthCity]
      ,[BirthState]
      ,[BirthCountry]
      ,[Sex]
      ,[ShirtSize]
      ,[HatSize]
      ,[DLNumber]
      ,[DLState]
      ,[DLCountry]
      ,[DLExpiresOn]
      ,[Height]
      ,[Weight]
      ,[EyeColor]
      ,[HairColor]
      ,[PhoneHome]
      ,[PhoneCell]
      ,[PhoneCellCarrierID]
      ,[PhoneFax]
      ,[Email]
      ,[CorporateEmail]
      ,[TreeLevel]
      ,[HasVerifiedAddress]
      ,[RightToWorkExpirationDate]
      ,[RightToWorkNotes]
      ,[RightToWorkStatusID]
      ,[IsLocked]
      ,[IsActive]
      ,[IsDeleted]
      ,[RecruitedDate]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
  FROM 
	INSERTED

END
GO