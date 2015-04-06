USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountSiteGeneralDispatchByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountSiteGeneralDispatchByAccountId'
		DROP  Procedure  dbo.custMS_AccountSiteGeneralDispatchByAccountId
	END
GO

PRINT 'Creating Procedure custMS_AccountSiteGeneralDispatchByAccountId'
GO
/******************************************************************************
**		File: custMS_AccountSiteGeneralDispatchByAccountId.sql
**		Name: custMS_AccountSiteGeneralDispatchByAccountId
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
**		Date: 04/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/02/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountSiteGeneralDispatchByAccountId
(
	@AccountId BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY


	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountSiteGeneralDispatchByAccountId TO PUBLIC
GO

/** EXEC dbo.custMS_AccountSiteGeneralDispatchByAccountId */