USE [WISE_SurveyEngine]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSV_Results')
	BEGIN
		PRINT 'Dropping VIEW vwSV_Results'
		DROP VIEW dbo.vwSV_Results
	END
GO

PRINT 'Creating VIEW vwSV_Results'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSV_Results.sql
**		Name: vwSV_Results
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
**		Auth: Aaron Shumway
**		Date: 04/23/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/23/2014	Aaron Shumway		Created
*******************************************************************************/
CREATE VIEW [dbo].[vwSV_Results]
AS

	SELECT
		R.ResultID
		, R.SurveyTranslationId
		, R.AccountId
		, R.Passed
		, R.IsComplete
		, R.Context
		, R.CreatedBy
		, R.CreatedOn

		, S.SurveyID AS SurveyId
		, S.Version

		, SType.SurveyTypeID AS SurveyTypeId
		, SType.Name AS SurveyType

		, ST.LocalizationCode
	FROM SV_Results AS R
	INNER JOIN SV_SurveyTranslations AS ST
	ON
		R.SurveyTranslationId = ST.SurveyTranslationID
	INNER JOIN SV_Surveys AS S
	ON
		ST.SurveyId = S.SurveyID
	INNER JOIN SV_SurveyTypes AS SType
	ON
		S.SurveyTypeId = SType.SurveyTypeID

GO
/* TEST */
-- SELECT * FROM vwSV_Results
