USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxQlCreditReportGetReportGuidByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetReportGuidByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetReportGuidByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetReportGuidByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetReportGuidByMsAccountID.sql
**		Name: fxQlCreditReportGetReportGuidByMsAccountID
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
CREATE FUNCTION dbo.fxQlCreditReportGetReportGuidByMsAccountID
(
	@AccountId BIGINT
)
RETURNS VARCHAR(500)
AS
BEGIN
	/** Declarations */
	DECLARE @ReportGuid VARCHAR(500);

	/** Execute actions. */
	SELECT @ReportGuid = CAST(ReportGuid AS VARCHAR(500)) FROM dbo.fxQlCreditReportGetByMsAccountID(@AccountId);

	RETURN @ReportGuid;
END
GO

--SELECT * FROM dbo.fxQlCreditReportGetByMsAccountID(12312);
--SELECT dbo.fxQlCreditReportGetReportGuidByMsAccountID(12321) AS ReportID;