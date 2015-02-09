USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEventsNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEventsNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEventsNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEventsNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEventsNuke.sql
**		Name: custMS_MonitronicsEventsNuke
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
**		Date: 11/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/17/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEventsNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEvents] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEvents] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEventsNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEventsNuke */