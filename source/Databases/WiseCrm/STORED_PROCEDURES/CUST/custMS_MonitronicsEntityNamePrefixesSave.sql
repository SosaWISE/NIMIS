USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityNamePrefixesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityNamePrefixesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityNamePrefixesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityNamePrefixesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityNamePrefixesSave.sql
**		Name: custMS_MonitronicsEntityNamePrefixesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityNamePrefixesSave
(
	@Prefix VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityNamePrefixes] WHERE (Prefix = @Prefix)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityNamePrefixes] SET
					IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(Prefix = @Prefix);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityNamePrefixes] (
					Prefix
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@Prefix
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityNamePrefixes] WHERE (Prefix = @Prefix);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityNamePrefixesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityNamePrefixesSave */