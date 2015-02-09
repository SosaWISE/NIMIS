USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgencyTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgencyTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgencyTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgencyTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgencyTypesSave.sql
**		Name: custMS_MonitronicsEntityAgencyTypesSave
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
CREATE Procedure dbo.custMS_MonitronicsEntityAgencyTypesSave
(
	@AgencyTypeID VARCHAR(10)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityAgencyTypes] WHERE (AgencyTypeID = @AgencyTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityAgencyTypes] SET
					[Description] = @Description
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(AgencyTypeID = @AgencyTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityAgencyTypes] (
					AgencyTypeID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@AgencyTypeID
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityAgencyTypes] WHERE (AgencyTypeID = @AgencyTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgencyTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAgencyTypesSave */