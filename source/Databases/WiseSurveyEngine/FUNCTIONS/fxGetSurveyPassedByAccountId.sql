USE [WISE_SurveyEngine]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetSurveyPassedByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetSurveyPassedByAccountId'
		DROP FUNCTION  dbo.fxGetSurveyPassedByAccountId
	END
GO

PRINT 'Creating FUNCTION fxGetSurveyPassedByAccountId'
GO
/******************************************************************************
**		File: fxGetSurveyPassedByAccountId.sql
**		Name: fxGetSurveyPassedByAccountId
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
**		Date: 04/16/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/16/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetSurveyPassedByAccountId
(
	@AccountID BIGINT
	, @SurveyTypeId INT
)
RETURNS 
@ParsedList table
(
	ResultID BIGINT
	, SurveyTranslationId INT
	, AccountId BIGINT
	, Passed BIT
	, IsComplete BIT
	, Context NVARCHAR(MAX)
	, CreatedBy NVARCHAR(50)
	, CreatedOn DATETIME
)
AS
BEGIN
	INSERT INTO @ParsedList (
		ResultID
		, SurveyTranslationId
		, AccountId
		, Passed
		, IsComplete
		, Context
		, CreatedBy
		, CreatedOn
	)
	SELECT
		SVR.*
	FROM
		[dbo].[SV_Results] AS SVR WITH (NOLOCK)
		INNER JOIN [dbo].[SV_SurveyTranslations] AS SVST WITH (NOLOCK)
		ON
			(SVST.SurveyTranslationID = SVR.SurveyTranslationId)
		INNER JOIN [dbo].[SV_Surveys] AS SVS WITH (NOLOCK)
		ON
			(SVS.SurveyID = SVST.SurveyId)
	WHERE
		(SVR.AccountId = @AccountID)
		AND (SVS.SurveyTypeId = @SurveyTypeId)
		AND (SVR.Passed = 1)
		AND (SVR.IsComplete = 1)
	ORDER BY
		SVR.CreatedOn DESC;

	RETURN
END
GO

/** TEST 
SELECT * FROM dbo.fxGetSurveyPassedByAccountId(191210, 1000);
*/