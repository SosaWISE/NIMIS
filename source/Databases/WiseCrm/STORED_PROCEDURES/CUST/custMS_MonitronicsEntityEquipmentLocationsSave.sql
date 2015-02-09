USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityEquipmentLocationsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityEquipmentLocationsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityEquipmentLocationsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityEquipmentLocationsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityEquipmentLocationsSave.sql
**		Name: custMS_MonitronicsEntityEquipmentLocationsSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityEquipmentLocationsSave
(
	@EquipLocID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityEquipmentLocations] WHERE (EquipLocID = @EquipLocID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityEquipmentLocations] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(EquipLocID = @EquipLocID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityEquipmentLocations] (
					EquipLocID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@EquipLocID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityEquipmentLocations] WHERE (EquipLocID = @EquipLocID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityEquipmentLocationsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityEquipmentLocationsSave */