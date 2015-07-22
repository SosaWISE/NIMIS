USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetTeamLocationIdByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetTeamLocationIdByAccountId'
		DROP FUNCTION  dbo.fxGetTeamLocationIdByAccountId
	END
GO

PRINT 'Creating FUNCTION fxGetTeamLocationIdByAccountId'
GO
/******************************************************************************
**		File: fxGetTeamLocationIdByAccountId.sql
**		Name: fxGetTeamLocationIdByAccountId
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
**		Date: 07/10/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/10/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetTeamLocationIdByAccountId
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @TeamLocationID INT;

	/** Execute actions. */
	SELECT TOP 1
		@TeamLocationID = QL.TeamLocationId
	FROM
		[WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
			(MSA.AccountID = AECA.AccountId)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
		INNER JOIN [WISE_CRM].[dbo].[QL_Leads] AS QL WITH (NOLOCK)
		ON
			(QL.LeadID = AEC.LeadId)
	WHERE
		(AECA.AccountId = @AccountID);

	RETURN @TeamLocationID;
END
GO