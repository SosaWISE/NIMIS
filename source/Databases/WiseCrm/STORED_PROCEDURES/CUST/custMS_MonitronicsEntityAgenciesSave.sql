USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgenciesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgenciesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgenciesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgenciesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgenciesSave.sql
**		Name: custMS_MonitronicsEntityAgenciesSave
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
**		Auth: Jake Jenne
**		Date: 12/2/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/2/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAgenciesSave
(
	@AgencyNumberID VARCHAR(50)
	, @AgencyTypeId VARCHAR(50)
	, @AgencyName VARCHAR(50)
	, @CityName VARCHAR(50)
	, @StateId VARCHAR(50)
	, @ZipCode VARCHAR(50)
	, @Phone1 VARCHAR(50)
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			-- Check if exists
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityAgencies] WHERE (AgencyNumberID = @AgencyNumberID) AND (AgencyTypeId = @AgencyTypeId)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityAgencies] SET
					AgencyTypeId = @AgencyTypeId
					, AgencyName = @AgencyName
					, CityName = @CityName
					, StateId = @StateId
					, ZipCode = @ZipCode
					, Phone1 = @Phone1
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(AgencyNumberID = @AgencyNumberID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityAgencies] (
					AgencyNumberID
					, AgencyTypeId
					, AgencyName
					, CityName
					, StateId
					, ZipCode
					, Phone1
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@AgencyNumberID
					, @AgencyTypeId
					, @AgencyName
					, @CityName
					, @StateId
					, @ZipCode
					, @Phone1
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedBy
					, GETUTCDATE()
				); 
			END
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityAgencies] WHERE ((AgencyNumberID = @AgencyNumberID) AND (AgencyTypeId = @AgencyTypeId));
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgenciesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAgenciesSave */