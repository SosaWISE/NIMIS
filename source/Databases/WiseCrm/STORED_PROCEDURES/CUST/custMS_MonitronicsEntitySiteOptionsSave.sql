USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySiteOptionsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySiteOptionsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySiteOptionsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySiteOptionsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySiteOptionsSave.sql
**		Name: custMS_MonitronicsEntitySiteOptionsSave
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
**		Date: 12/4/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/4/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySiteOptionsSave
(
	@CsNumber VARCHAR(50)
	, @OptionId VARCHAR(50)
	, @OptionValue VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySiteOptions] WHERE (CsNumber = @CsNumber)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntitySiteOptions] SET
					OptionId = @OptionId
					, OptionValue = @OptionValue
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(CsNumber = @CsNumber);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntitySiteOptions] (
					CsNumber
					, OptionId
					, OptionValue
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@CsNumber
					, @OptionId
					, @OptionValue
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
	SELECT * FROM [dbo].[MS_MonitronicsEntitySiteOptions] WHERE (CsNumber = @CsNumber);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySiteOptionsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySiteOptionsSave */