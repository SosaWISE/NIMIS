USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityRelationsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityRelationsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityRelationsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityRelationsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityRelationsSave.sql
**		Name: custMS_MonitronicsEntityRelationsSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityRelationsSave
(
	@RelationID VARCHAR(10)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityRelations] WHERE (RelationID = @RelationID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityRelations] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(RelationID = @RelationID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityRelations] (
					RelationID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@RelationID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityRelations] WHERE (RelationID = @RelationID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityRelationsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityRelationsSave */