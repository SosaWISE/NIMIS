USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityZipsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityZipsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityZipsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityZipsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityZipsSave.sql
**		Name: custMS_MonitronicsEntityZipsSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityZipsSave
(
	@CityNameID VARCHAR(50)
	, @CountyName VARCHAR(50)
	, @StateId VARCHAR(50)
	, @ZipCode VARCHAR(10)
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
			/*IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityZips] WHERE (CityNameID = @CityNameID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityZips] SET
					CountyName = @CountyName
					, StateId = @StateId
					, ZipCode = @ZipCode
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(CityNameID = @CityNameID);
			END
			ELSE
			BEGIN*/
				INSERT INTO [dbo].[MS_MonitronicsEntityZips] (
					CityNameID
					, CountyName
					, StateId
					, ZipCode
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@CityNameID
					, @CountyName
					, @StateId
					, @ZipCode
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedBy
					, GETUTCDATE()
				); 
			/*END*/
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityZips] WHERE (CityNameID = @CityNameID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityZipsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityZipsSave */