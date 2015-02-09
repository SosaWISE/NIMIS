USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityEquipEventXRefSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityEquipEventXRefSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityEquipEventXRefSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityEquipEventXRefSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityEquipEventXRefSave.sql
**		Name: custMS_MonitronicsEntityEquipEventXRefSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityEquipEventXRefSave
(
	@EquipTypeID VARCHAR(50)
	, @EventId VARCHAR(50)
	, @SiteKind VARCHAR(50)
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
			/*IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityEquipEventXRef] WHERE (EquipTypeID = @EquipTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityEquipEventXRef] SET
					EventId = @EventId
					, SiteKind = @SiteKind
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(EquipTypeID = @EquipTypeID);
			END
			ELSE
			BEGIN*/
				INSERT INTO [dbo].[MS_MonitronicsEntityEquipEventXRef] (
					EquipTypeID
					, EventId
					, SiteKind
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@EquipTypeID
					, @EventId
					, @SiteKind
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedBy
					, GETUTCDATE()
				); 
			/*END*/
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	/*SELECT * FROM [dbo].[MS_MonitronicsEntityEquipEventXRef] WHERE (EquipTypeID = @EquipTypeID);*/
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityEquipEventXRefSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityEquipEventXRefSave */