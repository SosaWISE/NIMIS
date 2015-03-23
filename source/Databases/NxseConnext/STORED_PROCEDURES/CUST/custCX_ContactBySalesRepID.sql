USE [NXSE_Connext]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custCX_ContactBySalesRepID')
	BEGIN
		PRINT 'Dropping Procedure custCX_ContactBySalesRepID'
		DROP  Procedure  dbo.custCX_ContactBySalesRepID
	END
GO

PRINT 'Creating Procedure custCX_ContactBySalesRepID'
GO
/******************************************************************************
**		File: custCX_ContactBySalesRepID.sql
**		Name: custCX_ContactBySalesRepID
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
**		Date: 03/18/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/18/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custCX_ContactBySalesRepID
(
	@SalesRepId VARCHAR(20)
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
			[dbo].[CX_Contacts] AS CXCT WITH (NOLOCK)
		WHERE
			(CXCT.SalesRepId = @SalesRepId)
			AND (CXCT.IsActive = 1) AND (CXCT.IsDeleted = 0);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custCX_ContactBySalesRepID TO PUBLIC
GO

/** EXEC dbo.custCX_ContactBySalesRepID 'SOSA001'*/