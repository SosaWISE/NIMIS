USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_IndustryAccountNumbersViewGetByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_IndustryAccountNumbersViewGetByAccountId'
		DROP  Procedure  dbo.custMS_IndustryAccountNumbersViewGetByAccountId
	END
GO

PRINT 'Creating Procedure custMS_IndustryAccountNumbersViewGetByAccountId'
GO
/******************************************************************************
**		File: custMS_IndustryAccountNumbersViewGetByAccountId.sql
**		Name: custMS_IndustryAccountNumbersViewGetByAccountId
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
**		Date: 01/24/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/24/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_IndustryAccountNumbersViewGetByAccountId
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
			[dbo].[vwMS_IndustryAccountNumbers] AS MIAN WITH (NOLOCK)
			INNER JOIN [dbo].[vwMS_IndustryAccountNumbersWithReceiverLineInfo] AS RLI WITH (NOLOCK)
			ON
				(RLI.IndustryAccountID = MIAN.IndustryAccountID)
		WHERE
			(MIAN.AccountID = @AccountId)
			AND (MIAN.IsActive = 1) AND (MIAN.IsDeleted = 0)
		ORDER BY
			RLI.[PrimaryCSID] DESC;
			

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_IndustryAccountNumbersViewGetByAccountId TO PUBLIC
GO

/** EXEC dbo.custMS_IndustryAccountNumbersViewGetByAccountId 130532 */