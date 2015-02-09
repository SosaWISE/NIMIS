USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityCellServicesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityCellServicesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityCellServicesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityCellServicesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityCellServicesNuke.sql
**		Name: custMS_MonitronicsEntityCellServicesNuke
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
CREATE Procedure dbo.custMS_MonitronicsEntityCellServicesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityCellServices] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityCellServices] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityCellServicesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityCellServicesNuke */