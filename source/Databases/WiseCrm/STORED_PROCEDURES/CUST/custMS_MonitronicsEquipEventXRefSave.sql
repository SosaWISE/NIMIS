USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEquipEventXRefSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEquipEventXRefSave'
		DROP  Procedure  dbo.custMS_MonitronicsEquipEventXRefSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEquipEventXRefSave'
GO

/******************************************************************************
**		File: custMS_MonitronicsEquipEventXRefSave.sql
**		Name: custMS_MonitronicsEquipEventXRefSave
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
**		Date: 11/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/17/2014	Andrew McFadden		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custMS_MonitronicsEquipEventXRefSave]
(
	@EquipTypeID VARCHAR(50) = NULL,
	@EventId VARCHAR(50) = NULL,
	@SiteKind VARCHAR(50) = NULL,
	@IsActive BIT = NULL,
	@IsDeleted BIT = NULL,
	@CreatedBy VARCHAR = NULL,
	@CreatedOn VARCHAR = NULL,
	@ModifiedBy VARCHAR = NULL,
	@ModifiedOn VARCHAR = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			--Check if exists
			IF (EXISTS(select*From [dbo].[MS_MonitronicsEquipEventXRef]WHERE(@EquipTypeID= @EquipTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEquipEventXRef] SET
					[EventId]=@EventId
					, [SiteKind]=@SiteKind
					, IsActive=1
					, IsDeleted=0
					, ModifiedBy=@ModifiedBy
					, ModifiedOn=GETUTCDATE()
				Where
					(EquipTypeID=@EquipTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEquipEventXRef](
					EquipTypeID
					, [EventId]
					, [SiteKind]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				)VALUES (
					@EquipTypeID
					, @EventId
					, @SiteKind
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedOn
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

	--Return row
	SELECT*FROM[dbo].[MS_MonitronicsEquipEventXRef] WHERE (EquipTypeID=@EquipTypeID);
END

GO

GRANT EXEC ON dbo.custMS_MonitronicsEquipEventXRefSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEquipEventXRefSave */