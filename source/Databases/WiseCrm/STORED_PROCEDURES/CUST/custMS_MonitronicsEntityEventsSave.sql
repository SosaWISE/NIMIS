USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityEventsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityEventsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityEventsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityEventsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityEventsSave.sql
**		Name: custMS_MonitronicsEntityEventsSave
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
**	11/17/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityEventsSave
(
	@EventID VARCHAR(30)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityEvents] WHERE (EventID = @EventID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityEvents] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(EventID = @EventID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityEvents] (
					EventID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@EventID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityEvents] WHERE (EventID = @EventID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityEventsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityEventsSave */