USE [NXSE_Connext]
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
**		Date: 02/13/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/13/2015	Andres Sosa		Created By
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

	SELECT
		*
	FROM
		[dbo].[MS_AccountSiteGeneralDispatches] AS ASGD WITH (NOLOCK)
	WHERE
		(ASGD.AccountID = @AccountId)
		AND (ASGD.IsActive = 1) AND (ASGD.IsDeleted = 0);

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