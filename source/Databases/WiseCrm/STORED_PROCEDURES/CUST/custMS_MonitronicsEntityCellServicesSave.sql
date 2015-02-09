USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityCellServicesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityCellServicesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityCellServicesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityCellServicesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityCellServicesSave.sql
**		Name: custMS_MonitronicsEntityCellServicesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityCellServicesSave
(
	@OptionID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityCellServices] WHERE (OptionID = @OptionID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityCellServices] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(OptionID = @OptionID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityCellServices] (
					OptionID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@OptionID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityCellServices] WHERE (OptionID = @OptionID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityCellServicesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityCellServicesSave */