USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityZoneStatesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityZoneStatesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityZoneStatesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityZoneStatesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityZoneStatesSave.sql
**		Name: custMS_MonitronicsEntityZoneStatesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityZoneStatesSave
(
	@ZoneStateID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityZoneStates] WHERE (ZoneStateID = @ZoneStateID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityZoneStates] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(ZoneStateID = @ZoneStateID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityZoneStates] (
					ZoneStateID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@ZoneStateID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityZoneStates] WHERE (ZoneStateID = @ZoneStateID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityZoneStatesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityZoneStatesSave */