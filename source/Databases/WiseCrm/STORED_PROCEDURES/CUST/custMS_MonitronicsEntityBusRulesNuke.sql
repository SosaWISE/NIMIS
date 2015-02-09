USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityBusRulesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityBusRulesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityBusRulesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityBusRulesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityBusRulesNuke.sql
**		Name: custMS_MonitronicsEntityBusRulesNuke
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
**		Date: 12/1/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/1/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityBusRulesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityBusRules] SET
				IsActive = 0
				, IsDeleted = 1;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityBusRules] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityBusRulesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityBusRulesNuke */