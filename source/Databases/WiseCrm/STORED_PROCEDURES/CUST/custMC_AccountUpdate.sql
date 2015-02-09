USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AccountUpdate')
	BEGIN
		PRINT 'Dropping Procedure custMC_AccountUpdate'
		DROP  Procedure  dbo.custMC_AccountUpdate
	END
GO

PRINT 'Creating Procedure custMC_AccountUpdate'
GO
/******************************************************************************
**		File: custMC_AccountUpdate.sql
**		Name: custMC_AccountUpdate
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
**		Date: 08/14/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/14/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_AccountUpdate
(
	@AccountID BIGINT
	, @AccountName NVARCHAR(50)
	, @AccountDesc NVARCHAR(MAX)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRY
	BEGIN TRANSACTION

		/** Execute update */
		UPDATE [dbo].MC_Accounts SET
			AccountName = @AccountName
			, AccountDesc = @AccountDesc
		WHERE
			(AccountID = @AccountID);
	
	
	COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_AccountUpdate TO PUBLIC
GO