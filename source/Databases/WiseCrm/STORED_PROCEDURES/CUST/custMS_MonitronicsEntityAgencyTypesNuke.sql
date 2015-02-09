USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgencyTypesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgencyTypesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgencyTypesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgencyTypesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgencyTypesNuke.sql
**		Name: custMS_MonitronicsEntityAgencyTypesNuke
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
**		Date: 11/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/24/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAgencyTypesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityAgencyTypes] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityAgencyTypes] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgencyTypesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAgencyTypesNuke */