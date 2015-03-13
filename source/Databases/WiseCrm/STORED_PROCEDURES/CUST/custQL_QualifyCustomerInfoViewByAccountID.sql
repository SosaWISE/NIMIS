USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_QualifyCustomerInfoViewByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custQL_QualifyCustomerInfoViewByAccountID'
		DROP  Procedure  dbo.custQL_QualifyCustomerInfoViewByAccountID
	END
GO

PRINT 'Creating Procedure custQL_QualifyCustomerInfoViewByAccountID'
GO
/******************************************************************************
**		File: custQL_QualifyCustomerInfoViewByAccountID.sql
**		Name: custQL_QualifyCustomerInfoViewByAccountID
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
**		Date: 06/03/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/03/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_QualifyCustomerInfoViewByAccountID
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @LeadID BIGINT;

	/** Find Lead ID. */
	SELECT TOP 1
		@LeadID = CST.LeadId
	FROM
		[dbo].[AE_CustomerAccounts] AS MAC WITH (NOLOCK)
		INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
		ON
			(CST.CustomerID = MAC.CustomerId)
			AND ((MAC.CustomerTypeId = 'MONI') OR (MAC.CustomerTypeId = 'PRI'))
	WHERE
		(MAC.AccountId = @AccountId)
	ORDER BY 
		MAC.CustomerTypeId DESC;

	BEGIN TRY
		SELECT
			VW.*
		FROM
			[dbo].[vwQL_QualifyCustomerInfo] AS VW
			INNER JOIN dbo.fxGetMaxOrSelectedCreditReportByLeadId(@LeadID) AS FX
			ON
				(VW.CreditReportID = FX.CreditReportID)
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custQL_QualifyCustomerInfoViewByAccountID TO PUBLIC
GO

/** EXEC dbo.custQL_QualifyCustomerInfoViewByAccountID 151193 */