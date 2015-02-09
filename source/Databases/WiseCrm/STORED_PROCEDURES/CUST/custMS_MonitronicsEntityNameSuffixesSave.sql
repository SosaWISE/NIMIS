USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityNameSuffixesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityNameSuffixesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityNameSuffixesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityNameSuffixesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityNameSuffixesSave.sql
**		Name: custMS_MonitronicsEntityNameSuffixesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityNameSuffixesSave
(
	@Suffix VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityNameSuffixes] WHERE (Suffix = @Suffix)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityNameSuffixes] SET
					IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(Suffix = @Suffix);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityNameSuffixes] (
					Suffix
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@Suffix
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityNameSuffixes] WHERE (Suffix = @Suffix);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityNameSuffixesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityNameSuffixesSave */