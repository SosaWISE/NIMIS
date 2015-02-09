USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityServiceCompaniesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityServiceCompaniesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityServiceCompaniesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityServiceCompaniesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityServiceCompaniesNuke.sql
**		Name: custMS_MonitronicsEntityServiceCompaniesNuke
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
**		Auth: Andrew McFadden
**		Date: 12/9/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/9/2014	Andrew McFadden		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityServiceCompaniesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityServiceCompanies] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityServiceCompanies] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityServiceCompaniesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityServiceCompaniesNuke */