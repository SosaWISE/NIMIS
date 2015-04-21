USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetDealerIDByAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetDealerIDByAccountID'
		DROP FUNCTION  dbo.fxGetDealerIDByAccountID
	END
GO

PRINT 'Creating FUNCTION fxGetDealerIDByAccountID'
GO
/******************************************************************************
**		File: fxGetDealerIDByAccountID.sql
**		Name: fxGetDealerIDByAccountID
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
**		Date: 04/20/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/20/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetDealerIDByAccountID
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @DealerID INT = 5000;  -- This defaults to Nexsense Master dealer

	/** Execute actions. */
	SELECT 
		@DealerID = ISNULL(QL.DealerId, 5000) -- This defaults to the Outside Sales season.
	FROM
		dbo.AE_CustomerAccounts
		INNER JOIN dbo.AE_Customers 
		ON
			(dbo.AE_Customers.CustomerID = dbo.AE_CustomerAccounts.CustomerId)
			AND (AE_Customers.CustomerTypeId = 'PRI')
		INNER JOIN dbo.QL_Leads AS QL WITH (NOLOCK)
		ON
			(QL.LeadID = dbo.AE_CustomerAccounts.LeadId)
	WHERE
		(AccountId = @AccountID);

	RETURN @DealerID;
END
GO
