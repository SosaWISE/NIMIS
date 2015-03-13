USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadsByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadsByAccountId'
		DROP  Procedure  dbo.custQL_LeadsByAccountId
	END
GO

PRINT 'Creating Procedure custQL_LeadsByAccountId'
GO
/******************************************************************************
**		File: custQL_LeadsByAccountId.sql
**		Name: custQL_LeadsByAccountId
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
**		Date: 03/12/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/12/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_LeadsByAccountId
(
	@AccountId BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

	SELECT TOP 1
		LD.*
	FROM
		[dbo].[QL_Leads] AS LD WITH (NOLOCK)
		INNER JOIN [dbo].[AE_CustomerAccounts] AS MSAC WITH (NOLOCK)
		ON
			(MSAC.LeadId = LD.LeadID)
			AND (MSAC.CustomerTypeId = 'PRI')
	WHERE
		(MSAC.AccountID = @AccountId)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custQL_LeadsByAccountId TO PUBLIC
GO

/** EXEC dbo.custQL_LeadsByAccountId */