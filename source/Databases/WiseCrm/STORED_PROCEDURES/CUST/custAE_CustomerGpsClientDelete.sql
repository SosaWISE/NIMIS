USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGPSClientDelete')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGPSClientDelete'
		DROP  Procedure  dbo.custAE_CustomerGPSClientDelete
	END
GO

PRINT 'Creating Procedure custAE_CustomerGPSClientDelete'
GO
/******************************************************************************
**		File: custAE_CustomerGPSClientDelete.sql
**		Name: custAE_CustomerGPSClientDelete
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
**		Date: 08/23/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/23/2013	Carly Chris 	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerGPSClientDelete
(
	@CustomerID BIGINT
)

AS
BEGIN
	/** Initialize Locals. */
	BEGIN TRY

		BEGIN TRANSACTION;

		/** Create a CustomerGPS Client. */
		UPDATE [dbo].[AE_CustomerGpsClients] SET 
			IsDeleted = 1 
		WHERE
			(CustomerID = @CustomerID);

		COMMIT TRANSACTION;

		/** Return result. */
		SELECT * FROM dbo.vwAE_CustomerGpsClients WHERE CustomerID = @CustomerID;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerGPSClientDelete TO PUBLIC
GO