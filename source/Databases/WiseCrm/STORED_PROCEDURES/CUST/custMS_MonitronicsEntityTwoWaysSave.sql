USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityTwoWaysSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityTwoWaysSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityTwoWaysSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityTwoWaysSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityTwoWaysSave.sql
**		Name: custMS_MonitronicsEntityTwoWaysSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityTwoWaysSave
(
	@TwoWayDeviceID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityTwoWays] WHERE (TwoWayDeviceID = @TwoWayDeviceID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityTwoWays] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(TwoWayDeviceID = @TwoWayDeviceID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityTwoWays] (
					TwoWayDeviceID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@TwoWayDeviceID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityTwoWays] WHERE (TwoWayDeviceID = @TwoWayDeviceID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityTwoWaysSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityTwoWaysSave */