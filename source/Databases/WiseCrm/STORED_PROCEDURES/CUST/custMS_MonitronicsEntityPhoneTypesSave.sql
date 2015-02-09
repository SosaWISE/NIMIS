USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPhoneTypesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPhoneTypesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPhoneTypesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPhoneTypesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPhoneTypesSave.sql
**		Name: custMS_MonitronicsEntityPhoneTypesSave
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
**		Date: 11/25/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/25/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityPhoneTypesSave
(
	@PhoneTypeID VARCHAR(10)
	, @Description NVARCHAR(50)
	, @Method VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityPhoneTypes] WHERE (PhoneTypeID = @PhoneTypeID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityPhoneTypes] SET
					[Description] = @Description
					, Method = @Method
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(PhoneTypeID = @PhoneTypeID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityPhoneTypes] (
					PhoneTypeID
					, [Description]
					, [Method]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@PhoneTypeID
					, @Description
					, @Method
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityPhoneTypes] WHERE (PhoneTypeID = @PhoneTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPhoneTypesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPhoneTypesSave */