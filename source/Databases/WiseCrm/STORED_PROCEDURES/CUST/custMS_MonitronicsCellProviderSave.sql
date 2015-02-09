USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsCellProviderSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsCellProviderSave'
		DROP  Procedure  dbo.custMS_MonitronicsCellProviderSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsCellProviderSave'
GO

/******************************************************************************
**		File: custMS_MonitronicsCellProviderSave.sql
**		Name: custMS_MonitronicsCellProviderSave
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
CREATE Procedure [dbo].[custMS_MonitronicsCellProviderSave]
(
	@CellProviderID VARCHAR(50) = NULL,
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
			IF (EXISTS(select*From [dbo].[MS_MonitronicsCellProvider]WHERE(CellProviderID=@CellProviderID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsCellProvider] SET
					[Description]=@Description
					, IsActive=1
					, IsDeleted=0
					, ModifiedBy=@ModifiedBy
					, ModifiedOn=GETUTCDATE()
				Where
					(CellProviderID=@CellProviderID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsCellProvider](
					CellProviderID
					, [Description]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				)VALUES (
					@CellProviderID
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
	SELECT*FROM[dbo].[MS_MonitronicsCellProvider] WHERE (CellProviderID=@CellProviderID);
END

GO

GRANT EXEC ON dbo.custMS_MonitronicsCellProviderSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsCellProviderSave */