USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGpsClientUpateLastLogin')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGpsClientUpateLastLogin'
		DROP  Procedure  dbo.custAE_CustomerGpsClientUpateLastLogin
	END
GO

PRINT 'Creating Procedure custAE_CustomerGpsClientUpateLastLogin'
GO
/******************************************************************************
**		File: custAE_CustomerGpsClientUpateLastLogin.sql
**		Name: custAE_CustomerGpsClientUpateLastLogin
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerGpsClientUpateLastLogin
(
	@CustomerID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Initialization */

	BEGIN TRY
		BEGIN TRANSACTION
		/** Check to see if there is a GPS Client entry. */
		IF (NOT EXISTS(SELECT * FROM [dbo].AE_CustomerGpsClients WHERE (CustomerID = @CustomerID)))
		BEGIN
			INSERT INTO [dbo].AE_CustomerGpsClients (CustomerID, AuthUserId, Username, Password, LastLoginOn)
			SELECT CST.CustomerID, NULL, CST.Username, CST.[Password], GETUTCDATE() FROM [dbo].AE_Customers AS CST WITH (NOLOCK) WHERE (CST.CustomerID = @CustomerID);
		END
		ELSE
		BEGIN
			UPDATE [dbo].AE_CustomerGpsClients SET LastLoginOn = GETUTCDATE() WHERE (CustomerID = @CustomerID);
		END
		
	
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH	
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN	
	END CATCH

	SELECT * FROM [dbo].vwAE_CustomerGpsClients WHERE (CustomerID = @CustomerID);
END
GO

GRANT EXEC ON dbo.custAE_CustomerGpsClientUpateLastLogin TO PUBLIC
GO

--EXEC [dbo].custAE_CustomerGpsClientUpateLastLogin 100195;