USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxQlCreditReportGetTransactionIdByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetTransactionIdByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetTransactionIdByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetTransactionIdByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetTransactionIdByMsAccountID.sql
**		Name: fxQlCreditReportGetTransactionIdByMsAccountID
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
**		Date: 02/23/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/23/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxQlCreditReportGetTransactionIdByMsAccountID
(
	@AccountId BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @ReportID INT;

	/** Execute actions. */
	SELECT @ReportID = ReportID FROM dbo.fxQlCreditReportGetByMsAccountID(@AccountId);

	RETURN @ReportID;
END
GO

--SELECT * FROM dbo.fxQlCreditReportGetByMsAccountID(12312);
--SELECT dbo.fxQlCreditReportGetTransactionIdByMsAccountID(12321) AS ReportID;