USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SPROC_NAME')
	BEGIN
		PRINT 'Dropping Procedure SPROC_NAME'
		DROP  Procedure  dbo.SPROC_NAME
	END
GO

PRINT 'Creating Procedure SPROC_NAME'
GO
/******************************************************************************
**		File: SPROC_NAME.sql
**		Name: SPROC_NAME
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
**		Date: 10/15/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/15/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.SPROC_NAME
(
	@PurchaseDateEnd DATETIME = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.SPROC_NAME TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */