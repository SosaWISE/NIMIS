USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountsTotalPoints')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountsTotalPoints'
		DROP FUNCTION  dbo.fxMsAccountsTotalPoints
	END
GO

PRINT 'Creating FUNCTION fxMsAccountsTotalPoints'
GO
/******************************************************************************
**		File: fxMsAccountsTotalPoints.sql
**		Name: fxMsAccountsTotalPoints
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 04/08/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/08/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountsTotalPoints
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @TotalPoints INT;

	/** Execute actions. */
	SELECT 
		@TotalPoints = SUM(AEII.SystemPoints) 
	FROM 
		[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		ON
			(AEII.InvoiceId = AEI.InvoiceID)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
			AND (AEI.AccountId = @AccountID);

	RETURN @TotalPoints;
END
GO

/**
SELECT AccountID, dbo.fxMsAccountsTotalPoints(AccountID) FROM [dbo].[MS_Accounts] WHERE AccountID = 191168;

SELECT 
	*
	, CustomerMasterFileId
FROM
	dbo.AE_CustomerAccounts
	INNER JOIN dbo.AE_Customers 
	ON
		(dbo.AE_Customers.CustomerID = dbo.AE_CustomerAccounts.CustomerId)
WHERE
	AccountId = 191168
*/
