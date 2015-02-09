USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityContactTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityContactTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityContactTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityContactTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityContactTypesSave.sql
**		Name: custMS_MonitronicsEntityContactTypesSave
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
**		Auth: Andres Sosa
**		Date: 11/25/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/25/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityContactTypesSave
(
	@ContactTypeID VARCHAR(10)
	, @Description NVARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityContactTypes] WHERE (ContactTypeID = @ContactTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityContactTypes] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(ContactTypeID = @ContactTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityContactTypes] (
					ContactTypeID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@ContactTypeID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityContactTypes] WHERE (ContactTypeID = @ContactTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityContactTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityContactTypesSave */