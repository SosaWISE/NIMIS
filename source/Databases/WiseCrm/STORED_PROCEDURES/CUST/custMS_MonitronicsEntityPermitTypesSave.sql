USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPermitTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPermitTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPermitTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPermitTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPermitTypesSave.sql
**		Name: custMS_MonitronicsEntityPermitTypesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityPermitTypesSave
(
	@PermitTypeID VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityPermitTypes] WHERE (PermitTypeID = @PermitTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityPermitTypes] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(PermitTypeID = @PermitTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityPermitTypes] (
					PermitTypeID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@PermitTypeID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityPermitTypes] WHERE (PermitTypeID = @PermitTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPermitTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPermitTypesSave */