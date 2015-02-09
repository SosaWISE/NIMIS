USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAuthoritiesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAuthoritiesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAuthoritiesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAuthoritiesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAuthoritiesSave.sql
**		Name: custMS_MonitronicsEntityAuthoritiesSave
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
**		Date: 11/15/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/14/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAuthoritiesSave
(
	@AuthID VARCHAR(10)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityAuthorities] WHERE (AuthID = @AuthID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityAuthorities] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(AuthID = @AuthID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityAuthorities] (
					AuthID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@AuthID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityAuthorities] WHERE (AuthID = @AuthID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAuthoritiesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAuthoritiesSave */