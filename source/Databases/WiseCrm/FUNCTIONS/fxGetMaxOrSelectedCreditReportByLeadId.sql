USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetMaxOrSelectedCreditReportByLeadId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMaxOrSelectedCreditReportByLeadId'
		DROP FUNCTION  dbo.fxGetMaxOrSelectedCreditReportByLeadId
	END
GO

PRINT 'Creating FUNCTION fxGetMaxOrSelectedCreditReportByLeadId'
GO
/******************************************************************************
**		File: fxGetMaxOrSelectedCreditReportByLeadId.sql
**		Name: fxGetMaxOrSelectedCreditReportByLeadId
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
**		Date: 05/08/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/08/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMaxOrSelectedCreditReportByLeadId
(
	@LeadID BIGINT
)
RETURNS 
@ResultTable table
(
	CreditReportID BIGINT
)
AS
BEGIN
	/** DECLARATIONS */
	INSERT INTO @ResultTable ( CreditReportID )
	SELECT TOP 1
		CR.CreditReportID
	--	, CR1.RowNumber
	FROM
		[dbo].[QL_CreditReports] AS CR WITH (NOLOCK)
		INNER JOIN (
			SELECT
				CRI.CreditReportID
				, CRI.LeadId
				, CRI.Score
				, CRI.IsSelected
				, ROW_NUMBER() OVER (PARTITION BY CRI.LeadId ORDER BY CRI.IsSelected DESC, CRI.Score DESC) AS [RowNumber]
			FROM
				[dbo].[QL_CreditReports] AS CRI WITH (NOLOCK)
			WHERE
				(CRI.IsActive = 1) AND (CRI.IsDeleted = 0)
				AND (CRI.LeadId = @LeadID)
				) AS CR1
		ON
			(CR1.CreditReportID = CR.CreditReportID)
	WHERE
		(CR1.RowNumber = 1)
	RETURN
END
GO

/** TESTS
SELECT * FROM dbo.fxGetMaxOrSelectedCreditReportByLeadId(1070985);
*/