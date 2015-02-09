USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsBusRulesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsBusRulesSave'
		DROP  Procedure  dbo.custMS_MonitronicsBusRulesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsBusRulesSave'
GO

/******************************************************************************
**		File: custMS_MonitronicsBusRulesSave.sql
**		Name: custMS_MonitronicsBusRulesSave
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
CREATE Procedure [dbo].[custMS_MonitronicsBusRulesSave]
(
	@ErrorNoID VARCHAR(50) = NULL,
	@TableName VARCHAR(50) = NULL,
	@BusRule VARCHAR(50) = NULL,
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
			IF (EXISTS(select*From [dbo].[MS_MonitronicsBusRules]WHERE(@ErrorNoID= @ErrorNoID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsBusRules] SET
					[TableName]=@TableName
					, [BusRule]=@BusRule
					, IsActive=1
					, IsDeleted=0
					, ModifiedBy=@ModifiedBy
					, ModifiedOn=GETUTCDATE()
				Where
					(ErrorNoID=@ErrorNoID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsBusRules](
					ErrorNoID
					, [TableName]
					, [BusRule]
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				)VALUES (
					@ErrorNoID
					, @TableName
					, @BusRule
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
	SELECT*FROM[dbo].[MS_MonitronicsBusRules] WHERE (ErrorNoID=@ErrorNoID);
END

GO

GRANT EXEC ON dbo.custMS_MonitronicsBusRulesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsBusRulesSave */