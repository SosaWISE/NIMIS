USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGetByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGetByAccountID'
		DROP  Procedure  dbo.custAE_CustomerGetByAccountID
	END
GO

PRINT 'Creating Procedure custAE_CustomerGetByAccountID'
GO
/******************************************************************************
**		File: custAE_CustomerGetByAccountID.sql
**		Name: custAE_CustomerGetByAccountID
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerGetByAccountID]
(
	@AccountID BIGINT
	, @CustomerTypeId VARCHAR(20) = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT
			CUST.*
		FROM 
			AE_Customers AS CUST WITH (NOLOCK)
			INNER JOIN dbo.AE_CustomerAccounts AS AC WITH (NOLOCK)
			ON
				(CUST.CustomerID = AC.CustomerId)
			INNER JOIN dbo.MS_Accounts AS MSA WITH (NOLOCK)
			ON
				(AC.AccountId = MSA.AccountID)
		WHERE
			(MSA.AccountID = @AccountID)
			AND (@CustomerTypeId IS NULL OR AC.CustomerTypeId = @CustomerTypeId) --@REVIEW: AE_Customers OR AE_CustomerAccounts CustomerTypeId????
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerGetByAccountID TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerGetByAccountID */