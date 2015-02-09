USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsCellServicesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsCellServicesSave'
		DROP  Procedure  dbo.custMS_MonitronicsCellServicesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsCellServicesSave'
GO

/******************************************************************************
**		File: custMS_MonitronicsCellServicesSave.sql
**		Name: custMS_MonitronicsCellServicesSave
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
**	11/17/2014	Andrew McFadden		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custMS_MonitronicsCellServicesSave]
(
	@OptionID VARCHAR(50) = NULL,
	@Description VARCHAR(50) = NULL,
	@IsActive BIT = NULL,
	@IsDeleted BIT = NULL,
	@CreatedBy VARCHAR = NULL,
	@CreatedOn VARCHAR = NULL,
	@ModifiedBy VARCHAR = NULL,
	@ModifiedOn VARCHAR = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			--Check if exists
			IF (EXISTS(select*From [dbo].[MS_MonitronicsCellServices]WHERE(OptionID=@OptionID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsCellServices] SET
					[Description]=@Description
					, IsActive=1
					, IsDeleted=0
					, ModifiedBy=@ModifiedBy
					, ModifiedOn=GETUTCDATE()
				Where
					(OptionID=@OptionID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsCellServices](
					OptionID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				)VALUES (
					@OptionID
					, @Description
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedOn
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

	--Return row
	SELECT*FROM[dbo].[MS_MonitronicsCellServices] WHERE (OptionID=@OptionID);
END

GO

GRANT EXEC ON dbo.custMS_MonitronicsCellServicesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsCellServicesSave */