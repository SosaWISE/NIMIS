USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySystemTypeXRefNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySystemTypeXRefNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySystemTypeXRefNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySystemTypeXRefNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySystemTypeXRefNuke.sql
**		Name: custMS_MonitronicsEntitySystemTypeXRefNuke
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
CREATE Procedure dbo.custMS_MonitronicsEntitySystemTypeXRefNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			DELETE FROM [dbo].[MS_MonitronicsEntitySystemTypeXRef];
			DBCC CHECKIDENT ('dbo.MS_MonitronicsEntitySystemTypeXRef', RESEED, 1);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypeXRef] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySystemTypeXRefNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySystemTypeXRefNuke */