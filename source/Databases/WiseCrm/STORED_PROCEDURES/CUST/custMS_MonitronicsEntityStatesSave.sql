USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityStatesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityStatesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityStatesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityStatesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityStatesSave.sql
**		Name: custMS_MonitronicsEntityStatesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityStatesSave
(
	@StateID VARCHAR(50)
	, @StateName VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityStates] WHERE (StateID = @StateID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityStates] SET
					StateName = @StateName
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(StateID = @StateID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityStates] (
					StateID
					, StateName
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@StateID
					, @StateName
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityStates] WHERE (StateID = @StateID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityStatesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityStatesSave */