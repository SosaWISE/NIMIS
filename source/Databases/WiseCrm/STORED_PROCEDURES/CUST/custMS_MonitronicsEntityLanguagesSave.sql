USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityLanguagesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityLanguagesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityLanguagesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityLanguagesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityLanguagesSave.sql
**		Name: custMS_MonitronicsEntityLanguagesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityLanguagesSave
(
	@LanguageID VARCHAR(50)
	, @Description VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityLanguages] WHERE (LanguageID = @LanguageID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityLanguages] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(LanguageID = @LanguageID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityLanguages] (
					LanguageID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@LanguageID
					, @Description
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityLanguages] WHERE (LanguageID = @LanguageID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityLanguagesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityLanguagesSave */