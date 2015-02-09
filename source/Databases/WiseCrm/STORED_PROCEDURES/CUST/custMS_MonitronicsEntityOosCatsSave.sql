USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityOosCatsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityOosCatsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityOosCatsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityOosCatsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityOosCatsSave.sql
**		Name: custMS_MonitronicsEntityOosCatsSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityOosCatsSave
(
	@OosCatsID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityOosCats] WHERE (OosCatsID = @OosCatsID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityOosCats] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(OosCatsID = @OosCatsID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityOosCats] (
					OosCatsID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@OosCatsID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityOosCats] WHERE (OosCatsID = @OosCatsID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityOosCatsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityOosCatsSave */