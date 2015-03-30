USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxQlCreditReportGetCreditBureauByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetCreditBureauByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetCreditBureauByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetCreditBureauByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetCreditBureauByMsAccountID.sql
**		Name: fxQlCreditReportGetCreditBureauByMsAccountID
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
**		Date: 03/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/30/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxQlCreditReportGetCreditBureauByMsAccountID
(
	@AccountId BIGINT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @Bureau VARCHAR(50);

	/** Execute actions. */
	SELECT @Bureau = CAST(BureauName AS VARCHAR(50)) FROM dbo.fxQlCreditReportGetByMsAccountID(@AccountId);

	RETURN @Bureau;
END
GO

--SELECT * FROM dbo.fxQlCreditReportGetByMsAccountID(12312);
--SELECT dbo.fxQlCreditReportGetCreditBureauByMsAccountID(12321) AS ReportID;