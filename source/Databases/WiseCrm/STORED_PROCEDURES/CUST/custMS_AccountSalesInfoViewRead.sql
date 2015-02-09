USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountSalesInfoViewRead')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountSalesInfoViewRead'
		DROP  Procedure  dbo.custMS_AccountSalesInfoViewRead
	END
GO

PRINT 'Creating Procedure custMS_AccountSalesInfoViewRead'
GO
/******************************************************************************
**		File: custMS_AccountSalesInfoViewRead.sql
**		Name: custMS_AccountSalesInfoViewRead
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
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/07/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountSalesInfoViewRead
(
	@AccountID BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		IF (@AccountID IS NULL)
			SELECT * FROM dbo.vwMS_AccountSalesInformations;
		ELSE
			SELECT * FROM dbo.vwMS_AccountSalesInformations WHERE (AccountID = @AccountID);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountSalesInfoViewRead TO PUBLIC
GO

/** EXEC dbo.custMS_AccountSalesInfoViewRead 181051 */