USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAuthoritiesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAuthoritiesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAuthoritiesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAuthoritiesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAuthoritiesNuke.sql
**		Name: custMS_MonitronicsEntityAuthoritiesNuke
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
CREATE Procedure dbo.custMS_MonitronicsEntityAuthoritiesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityAuthorities] SET
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityAuthorities] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAuthoritiesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAuthoritiesNuke */