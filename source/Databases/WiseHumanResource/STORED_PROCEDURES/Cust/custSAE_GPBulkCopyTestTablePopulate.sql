USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SAE_GPBulkCopyTestTablePopulate')
	BEGIN
		PRINT 'Dropping Procedure SAE_GPBulkCopyTestTablePopulate'
		DROP  Procedure  dbo.SAE_GPBulkCopyTestTablePopulate
	END
GO

PRINT 'Creating Procedure SAE_GPBulkCopyTestTablePopulate'
GO
/******************************************************************************
**		File: SAE_GPBulkCopyTestTablePopulate.sql
**		Name: SAE_GPBulkCopyTestTablePopulate
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
**		Date: 12/12/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/12/2014	Andrew McFadden pull data from CSV file into SAE_GPBulkCopyTestTable
**	
*******************************************************************************/
CREATE Procedure dbo.SAE_GPBulkCopyTestTablePopulate
/*(
-- add parameters
)*/
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.SAE_GPBulkCopyTestTablePopulate TO PUBLIC
GO

/** EXEC dbo.SAE_GPBulkCopyTestTablePopulate () */
