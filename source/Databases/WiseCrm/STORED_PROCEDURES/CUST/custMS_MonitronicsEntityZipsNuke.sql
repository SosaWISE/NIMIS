USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityZipsNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityZipsNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityZipsNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityZipsNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityZipsNuke.sql
**		Name: custMS_MonitronicsEntityZipsNuke
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
CREATE Procedure dbo.custMS_MonitronicsEntityZipsNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			/*UPDATE [dbo].[MS_MonitronicsEntityZips] SET
				IsActive = 0
				, IsDeleted = 1;*/
			DELETE FROM [dbo].[MS_MonitronicsEntityZips];
			DBCC CHECKIDENT ('dbo.MS_MonitronicsEntityZips', RESEED, 1);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityZips] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityZipsNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityZipsNuke */