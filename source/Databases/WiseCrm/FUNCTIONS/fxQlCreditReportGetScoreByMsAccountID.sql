USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxQlCreditReportGetScoreByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetScoreByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetScoreByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetScoreByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetScoreByMsAccountID.sql
**		Name: fxQlCreditReportGetScoreByMsAccountID
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
**		Date: 02/20/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/20/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxQlCreditReportGetScoreByMsAccountID
(
	@AccountId BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @Score INT;

	/** Execute actions. */
	SELECT @Score = Score FROM dbo.fxQlCreditReportGetByMsAccountID(@AccountId);

	RETURN @Score;
END
GO

SELECT dbo.fxQlCreditReportGetScoreByMsAccountID(191194) AS SCORE;