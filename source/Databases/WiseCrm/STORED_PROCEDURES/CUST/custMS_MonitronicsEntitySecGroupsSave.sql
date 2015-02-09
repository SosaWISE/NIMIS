USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySecGroupsSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySecGroupsSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySecGroupsSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySecGroupsSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySecGroupsSave.sql
**		Name: custMS_MonitronicsEntitySecGroupsSave
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
**		Date: 12/4/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/4/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySecGroupsSave
(
	@SecurityGroupID VARCHAR(50)
	, @SecurityLevel VARCHAR(50)
	, @AllUsers VARCHAR(50)
	, @AllAccounts VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySecGroups] WHERE (SecurityGroupID = @SecurityGroupID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntitySecGroups] SET
					SecurityLevel = @SecurityLevel
					, AllUsers = @AllUsers
					, AllAccounts = @AllAccounts
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(SecurityGroupID = @SecurityGroupID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntitySecGroups] (
					SecurityGroupID
					, SecurityLevel
					, AllUsers
					, AllAccounts
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@SecurityGroupID
					, @SecurityLevel
					, @AllUsers
					, @AllAccounts
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
	SELECT * FROM [dbo].[MS_MonitronicsEntitySecGroups] WHERE (SecurityGroupID = @SecurityGroupID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySecGroupsSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySecGroupsSave */