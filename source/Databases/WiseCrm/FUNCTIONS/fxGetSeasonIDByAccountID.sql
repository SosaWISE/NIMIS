USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetSeasonIDByAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetSeasonIDByAccountID'
		DROP FUNCTION  dbo.fxGetSeasonIDByAccountID
	END
GO

PRINT 'Creating FUNCTION fxGetSeasonIDByAccountID'
GO
/******************************************************************************
**		File: fxGetSeasonIDByAccountID.sql
**		Name: fxGetSeasonIDByAccountID
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
CREATE FUNCTION dbo.fxGetSeasonIDByAccountID
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @SeasonID INT = 4;  -- This defaults to the Outside Sales season.

	/** Execute actions. */
	SELECT 
		@SeasonID = ISNULL(QL.SeasonId, 4) -- This defaults to the Outside Sales season.
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

	RETURN @SeasonID;
END
GO