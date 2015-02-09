USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceRefreshHeader')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceRefreshHeader'
		DROP  Procedure  dbo.custAE_InvoiceRefreshHeader
	END
GO

PRINT 'Creating Procedure custAE_InvoiceRefreshHeader'
GO
/******************************************************************************
**		File: custAE_InvoiceRefreshHeader.sql
**		Name: custAE_InvoiceRefreshHeader
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceRefreshHeader
(
	@InvoiceID BIGINT
	, @GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_InvoiceRefreshHeader TO PUBLIC
GO

/** EXEC dbo.custAE_InvoiceRefreshHeader */