/**
* Description: Cleans out the database for testing purposes.
*/
USE [WISE_SurveyEngine]
GO

BEGIN TRANSACTION

/** Answers */
PRINT 'Tuncating SV_Answers...';
TRUNCATE TABLE SV_Answers;
PRINT 'Deleting SV_PossibleAnswerTranslations...';
DELETE dbo.SV_PossibleAnswerTranslations;
PRINT 'Deleting SV_Questions_PossibleAnswers_Map...';
DELETE dbo.SV_Questions_PossibleAnswers_Map;
PRINT 'Deleting SV_PossibleAnswers...';
DELETE dbo.SV_PossibleAnswers;

/** Questions */
PRINT 'Deleting [dbo].[SV_QuestionTranslations]...';
DELETE [dbo].[SV_QuestionTranslations];
PRINT 'Deleting [dbo].[SV_Questions]...';
DELETE [dbo].[SV_Questions];
PRINT 'SV_QuestionMeanings_Tokens_Map...';
DELETE [dbo].[SV_QuestionMeanings_Tokens_Map];
PRINT 'Deleting SV_QuestionMeanings...'
DELETE [dbo].[SV_QuestionMeanings];

/** Results */
PRINT 'Deleting [dbo].[SV_Results]...';
DELETE [dbo].[SV_Results];

/** Surveys */
PRINT 'Deleting [dbo].[SV_SurveyTranslations]...';
DELETE [dbo].[SV_SurveyTranslations];
PRINT 'Deleting [dbo].[SV_Surveys]...';
DELETE [dbo].[SV_Surveys];
PRINT 'Deleting [dbo].[SV_SurveyTypes]...';
DELETE [dbo].[SV_SurveyTypes];

/** Tokens */
PRINT 'Deleting [dbo].[SV_Tokens]...';
DELETE [dbo].[SV_Tokens];

/**
* Now reset the Identity Seeds of each table.
*/

DBCC CHECKIDENT(SV_Answers, RESEED, 0);
DBCC CHECKIDENT(SV_PossibleAnswers, RESEED, 0);
DBCC CHECKIDENT(SV_PossibleAnswerTranslations, RESEED, 0);
DBCC CHECKIDENT(SV_QuestionMeanings, RESEED, 9999);
--DBCC CHECKIDENT(SV_QuestionMeanings_Tokens_Map,] -- This does not have an identity
DBCC CHECKIDENT(SV_Questions, RESEED, 9999);
--DBCC CHECKIDENT(SV_Questions_PossibleAnswers_Map] -- This does not have an identity
DBCC CHECKIDENT(SV_QuestionTranslations, RESEED, 9999);
DBCC CHECKIDENT(SV_Results, RESEED, 99999);
DBCC CHECKIDENT(SV_Surveys, RESEED, 999);
DBCC CHECKIDENT(SV_SurveyTranslations, RESEED, 0);
DBCC CHECKIDENT(SV_SurveyTypes, RESEED, 999);
DBCC CHECKIDENT(SV_Tokens, RESEED, 0);

--DBCC CHECKIDENT(SV_Questions, RESEED, 1003);


ROLLBACK TRANSACTION