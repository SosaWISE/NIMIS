USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityOptionsNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityOptionsNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityOptionsNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityOptionsNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityOptionsNuke.sql
**		Name: custMS_MonitronicsEntityOptionsNuke
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
**  12/2/2014	Jake Jenne		Altered the method to truncate table and reseed to identity 1
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityOptionsNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			DELETE FROM [dbo].[MS_MonitronicsEntityOptions];
			DBCC CHECKIDENT ('dbo.MS_MonitronicsEntityOptions', RESEED, 1);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	/*SELECT * FROM [dbo].[MS_MonitronicsEntityOptions] WHERE (IsActive = 1 AND IsDeleted = 0);*/
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityOptionsNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityOptionsNuke */