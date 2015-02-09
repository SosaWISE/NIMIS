USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityBusRulesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityBusRulesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityBusRulesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityBusRulesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityBusRulesSave.sql
**		Name: custMS_MonitronicsEntityBusRulesSave
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
**		Date: 11/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/26/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityBusRulesSave
(
	@ErrorNoID INT
	, @TableName VARCHAR(50)
	, @BusRule VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityBusRules] WHERE (ErrorNoID = @ErrorNoID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityBusRules] SET
					TableName = @TableName
					, BusRule = @BusRule
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(ErrorNoID = @ErrorNoID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityBusRules] (
					ErrorNoID
					, TableName
					, BusRule
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@ErrorNoID
					, @TableName
					, @BusRule
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityBusRules] WHERE (ErrorNoID = @ErrorNoID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityBusRulesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityBusRulesSave */