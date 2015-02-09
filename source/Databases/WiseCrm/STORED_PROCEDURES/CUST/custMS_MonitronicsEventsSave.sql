USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEventsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEventsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEventsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEventsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEventsSave.sql
**		Name: custMS_MonitronicsEventsSave
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
CREATE Procedure dbo.custMS_MonitronicsEventsSave
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEvents] WHERE (EventID = @EventID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEvents] SET
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
				INSERT INTO [dbo].[MS_MonitronicsEvents] (
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
	SELECT * FROM [dbo].[MS_MonitronicsEvents] WHERE (EventID = @EventID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEventsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEventsSave */