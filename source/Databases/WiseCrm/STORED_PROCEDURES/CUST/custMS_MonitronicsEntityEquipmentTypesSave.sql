USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityEquipmentTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityEquipmentTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityEquipmentTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityEquipmentTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityEquipmentTypesSave.sql
**		Name: custMS_MonitronicsEntityEquipmentTypesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityEquipmentTypesSave
(
	@EquipTypeId VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityEquipmentTypes] WHERE (EquipTypeId = @EquipTypeId)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityEquipmentTypes] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(EquipTypeId = @EquipTypeId);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityEquipmentTypes] (
					EquipTypeId
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@EquipTypeId
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityEquipmentTypes] WHERE (EquipTypeId = @EquipTypeId);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityEquipmentTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityEquipmentTypesSave */