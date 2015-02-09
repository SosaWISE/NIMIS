USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySiteSystemOptionsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySiteSystemOptionsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySiteSystemOptionsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySiteSystemOptionsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySiteSystemOptionsSave.sql
**		Name: custMS_MonitronicsEntitySiteSystemOptionsSave
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
**		Date: 01/12/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/12/2015	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySiteSystemOptionsSave
(
	@CsNumberID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySiteSystemOptions] WHERE (CsNumberID = @CsNumberID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntitySiteSystemOptions] SET
					OptionId = @OptionId
					, OptionValue = @OptionValue
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(CsNumberID = @CsNumberID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntitySiteSystemOptions] (
					CsNumberID
					, OptionId
					, OptionValue
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@CsNumberID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntitySiteSystemOptions] WHERE (CsNumberID = @CsNumberID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySiteSystemOptionsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySiteSystemOptionsSave */