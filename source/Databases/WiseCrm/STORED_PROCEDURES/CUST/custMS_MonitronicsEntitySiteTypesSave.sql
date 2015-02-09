USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySiteTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySiteTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySiteTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySiteTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySiteTypesSave.sql
**		Name: custMS_MonitronicsEntitySiteTypesSave
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
**		Date: 11/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/24/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySiteTypesSave
(
	@SiteTypeID VARCHAR(50)
	, @Description VARCHAR(50)
	, @SiteKind VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySiteTypes] WHERE (SiteTypeID = @SiteTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntitySiteTypes] SET
					[Description] = @Description
					, SiteKind = @SiteKind
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(SiteTypeID = @SiteTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntitySiteTypes] (
					SiteTypeID
					, [Description]
					, SiteKind
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@SiteTypeID
					, @Description
					, @SiteKind
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
	SELECT * FROM [dbo].[MS_MonitronicsEntitySiteTypes] WHERE (SiteTypeID = @SiteTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySiteTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySiteTypesSave */