USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPhoneTypesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPhoneTypesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPhoneTypesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPhoneTypesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPhoneTypesNuke.sql
**		Name: custMS_MonitronicsEntityPhoneTypesNuke
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
**		Date: 11/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/14/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityPhoneTypesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityPhoneTypes] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityPhoneTypes] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPhoneTypesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPhoneTypesNuke */