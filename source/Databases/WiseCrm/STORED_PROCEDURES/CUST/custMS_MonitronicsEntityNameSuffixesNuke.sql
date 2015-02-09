USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityNameSuffixesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityNameSuffixesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityNameSuffixesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityNameSuffixesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityNameSuffixesNuke.sql
**		Name: custMS_MonitronicsEntityNameSuffixesNuke
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
**		Date: 12/2/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/2/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityNameSuffixesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityNameSuffixes] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityNameSuffixes] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityNameSuffixesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityNameSuffixesNuke */