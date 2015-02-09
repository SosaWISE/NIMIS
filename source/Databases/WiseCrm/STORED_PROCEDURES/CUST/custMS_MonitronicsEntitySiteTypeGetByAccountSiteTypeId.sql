USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId.sql
**		Name: custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId
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
**		Date: 11/25/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/25/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId
(
	@SiteTypeID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT * FROM [dbo].[MS_MonitronicsEntitySiteTypes] WHERE (SiteTypeID = @SiteTypeID);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId 'RBFM  '*/