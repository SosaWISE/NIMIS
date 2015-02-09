USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGPSClientRead')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGPSClientRead'
		DROP  Procedure  dbo.custAE_CustomerGPSClientRead
	END
GO

PRINT 'Creating Procedure custAE_CustomerGPSClientRead'
GO
/******************************************************************************
**		File: custAE_CustomerGPSClientRead.sql
**		Name: custAE_CustomerGPSClientRead
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
**		Auth: Carly Christiansen
**		Date: 08/26/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/26/2013	Carly Chris		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerGPSClientRead
(
	 @CustomerID BIGINT

)
	
AS
BEGIN
	
	BEGIN TRY

		/** Return result. */
		SELECT * FROM dbo.vwAE_CustomerGpsClients WHERE CustomerID = @CustomerID;

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerGPSClientRead TO PUBLIC
GO