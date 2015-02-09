USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityOptionsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityOptionsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityOptionsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityOptionsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityOptionsSave.sql
**		Name: custMS_MonitronicsEntityOptionsSave
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
**		Date: 12/1/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/1/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityOptionsSave
(
	@OptionID VARCHAR(50)
	, @UsageId VARCHAR(50)
	, @Description VARCHAR(50)
	, @ValidValue VARCHAR(50)
	, @ValueDescription VARCHAR(50) = NULL
	, @ValueRequired VARCHAR(50) = NULL
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityOptions] WHERE (OptionID = @OptionID) AND (ValidValue = @ValidValue)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityOptions] SET
					UsageId = @UsageId
					, [Description] = @Description
					, ValidValue = @ValidValue
					, ValueDescription = @ValueDescription
					, ValueRequired = @ValueRequired
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(OptionID = @OptionID)
					AND (ValidValue = @ValidValue);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityOptions] (
					OptionID
					, UsageId
					, [Description]
					, ValidValue
					, ValueDescription
					, ValueRequired
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@OptionID
					, @UsageId
					, @Description
					, @ValidValue
					, @ValueDescription
					, @ValueRequired
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
	/*SELECT * FROM [dbo].[MS_MonitronicsEntityOptions] WHERE (OptionID = @OptionID);*/
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityOptionsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityOptionsSave */