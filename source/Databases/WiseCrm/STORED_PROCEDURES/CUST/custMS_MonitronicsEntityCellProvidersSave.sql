USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityCellProvidersSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityCellProvidersSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityCellProvidersSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityCellProvidersSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityCellProvidersSave.sql
**		Name: custMS_MonitronicsEntityCellProvidersSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityCellProvidersSave
(
	@CellProviderID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityCellProviders] WHERE (CellProviderID = @CellProviderID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityCellProviders] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(CellProviderID = @CellProviderID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityCellProviders] (
					CellProviderID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@CellProviderID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityCellProviders] WHERE (CellProviderID = @CellProviderID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityCellProvidersSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityCellProvidersSave */