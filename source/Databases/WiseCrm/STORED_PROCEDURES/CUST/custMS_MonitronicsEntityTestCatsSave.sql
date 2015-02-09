USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityTestCatsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityTestCatsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityTestCatsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityTestCatsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityTestCatsSave.sql
**		Name: custMS_MonitronicsEntityTestCatsSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityTestCatsSave
(
	@TestCatID VARCHAR(50)
	, @Description VARCHAR(50)
	, @DefaultHours SMALLINT
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityTestCats] WHERE (TestCatID = @TestCatID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityTestCats] SET
					[Description] = @Description
					, DefaultHours = @DefaultHours
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(TestCatID = @TestCatID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityTestCats] (
					TestCatID
					, [Description]
					, DefaultHours
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@TestCatID
					, @Description
					, @DefaultHours
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityTestCats] WHERE (TestCatID = @TestCatID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityTestCatsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityTestCatsSave */