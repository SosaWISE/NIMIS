USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_LeadTakeOverViewGetByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_LeadTakeOverViewGetByAccountId'
		DROP  Procedure  dbo.custMS_LeadTakeOverViewGetByAccountId
	END
GO

PRINT 'Creating Procedure custMS_LeadTakeOverViewGetByAccountId'
GO
/******************************************************************************
**		File: custMS_LeadTakeOverViewGetByAccountId.sql
**		Name: custMS_LeadTakeOverViewGetByAccountId
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
**		Date: 05/07/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/07/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_LeadTakeOverViewGetByAccountId
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
			[dbo].fxGetLeadTakeOverByAccountId(@AccountId);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_LeadTakeOverViewGetByAccountId TO PUBLIC
GO

/** EXEC dbo.custMS_LeadTakeOverViewGetByAccountId 211217*/