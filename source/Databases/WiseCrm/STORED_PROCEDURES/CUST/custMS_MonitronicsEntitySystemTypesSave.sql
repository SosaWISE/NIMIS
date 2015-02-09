USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySystemTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySystemTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySystemTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySystemTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySystemTypesSave.sql
**		Name: custMS_MonitronicsEntitySystemTypesSave
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
**		Date: 11/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/24/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySystemTypesSave
(
	@SystemTypeID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypes] WHERE (SystemTypeID = @SystemTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntitySystemTypes] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(SystemTypeID = @SystemTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntitySystemTypes] (
					SystemTypeID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@SystemTypeID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypes] WHERE (SystemTypeID = @SystemTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySystemTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySystemTypesSave */