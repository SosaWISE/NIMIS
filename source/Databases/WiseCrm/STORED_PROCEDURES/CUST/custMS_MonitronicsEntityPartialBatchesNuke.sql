USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPartialBatchesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPartialBatchesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPartialBatchesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPartialBatchesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPartialBatchesNuke.sql
**		Name: custMS_MonitronicsEntityPartialBatchesNuke
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
CREATE Procedure dbo.custMS_MonitronicsEntityPartialBatchesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityPartialBatches] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityPartialBatches] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPartialBatchesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPartialBatchesNuke */