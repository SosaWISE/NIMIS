USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetLeadTakeOverByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetLeadTakeOverByAccountId'
		DROP FUNCTION  dbo.fxGetLeadTakeOverByAccountId
	END
GO

PRINT 'Creating FUNCTION fxGetLeadTakeOverByAccountId'
GO
/******************************************************************************
**		File: fxGetLeadTakeOverByAccountId.sql
**		Name: fxGetLeadTakeOverByAccountId
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
**		Date: 05/07/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/07/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetLeadTakeOverByAccountId
(
	@AccountID BIGINT
)
RETURNS 
@ResultList table
(
	AccountId BIGINT
	, LeadID BIGINT
	, FullName VARCHAR(200)
	, StreetAddress VARCHAR(50)
	, CityStZip VARCHAR(100)
	, AlarmCompanyId INT
	, AlarmCompanyName VARCHAR(50)
)
AS
BEGIN
	INSERT INTO @ResultList (
		AccountId,
		LeadID,
		FullName,
		StreetAddress,
		CityStZip, 
		AlarmCompanyId,
		AlarmCompanyName
	)
	SELECT * FROM vwMS_LeadTakeOvers WHERE (AccountId = @AccountID);

	RETURN
END
GO

SELECT * FROM dbo.fxGetLeadTakeOverByAccountId (211217);