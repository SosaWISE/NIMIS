USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsBusRulesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsBusRulesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsBusRulesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsBusRulesNuke'
GO

/******************************************************************************
**		File: custMS_MonitronicsBusRulesNuke.sql
**		Name: custMS_MonitronicsBusRulesNuke
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
**		Date: 11/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/21/2014	Andrew McFadden		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custMS_MonitronicsBusRulesNuke]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsBusRules] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsBusRules] WHERE (IsActive = 1 AND IsDeleted = 0);
END

GO

GRANT EXEC ON dbo.custMS_MonitronicsBusRulesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsBusRulesNuke */