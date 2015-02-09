USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersSalesRankingConnext')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersSalesRankingConnext'
		DROP VIEW dbo.vwRU_UsersSalesRankingConnext
	END
GO

PRINT 'Creating VIEW vwRU_UsersSalesRankingConnext'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersSalesRankingConnext.sql
**		Name: vwRU_UsersSalesRankingConnext
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
**		Auth: Bob McFadden
**		Date: 11/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	11/11/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_UsersSalesRankingConnext] 
AS

SELECT
	users.UserID as UserID,
	users.FirstName as FirstName,
	ISNULL(users.MiddleName,'') as MiddleName,
	users.LastName as LastName,
	'//www.wix.com/blog/wp-content/uploads/2013/05/Instagram_10_56.jpg' AS PhotoURL,
	results.PeriodEndingDate, 
	resulttype.SalespersonResultType AS ResultsType, 
	rankinggroup.SalespersonRankingGroup AS RankingGroup, 
	rankingperiod.SalespersonRankingPeriod as RankingPeriod, 
	CASE 
		WHEN rankingperiod.SalespersonRankingPeriod = 'YEAR' THEN results.YearToDateResults
		WHEN rankingperiod.SalespersonRankingPeriod = 'MONTH' THEN results.MonthToDateResults
		WHEN rankingperiod.SalespersonRankingPeriod = 'WEEK' THEN results.WeekToDateResults
		ELSE 0
	END AS CurrentResults,
	rankings.Sequence AS CurrentSequence, 
	rankings.Ranking as CurrentRanking,
	CASE 
		WHEN rankingperiod.SalespersonRankingPeriod = 'YEAR' THEN results.YearToDateResults
		WHEN rankingperiod.SalespersonRankingPeriod = 'MONTH' THEN results.MonthToDateResults
		WHEN rankingperiod.SalespersonRankingPeriod = 'WEEK' THEN results.WeekToDateResults
		ELSE 0
	END AS PreviousResults,
	rankings.Sequence AS PreviousSequence, 
	rankings.Ranking as PreviousRanking
FROM 
	-- USERS
	dbo.RU_Users as users WITH(NOLOCK)

	-- RESULTS
	JOIN dbo.RU_SalespersonResults AS results WITH(NOLOCK)
		ON users.UserID = results.UserID
		AND results.PeriodEndingDate IN (SELECT TOP 2 PeriodEndingDate FROM dbo.RU_SalespersonResults)

	-- RANKINGS
	JOIN WISE_Humanresource.dbo.RU_SalespersonRankings AS rankings WITH(NOLOCK)
		ON results.SalespersonResultsID = rankings.SalespersonResultsID

	-- RESULT TYPES
	JOIN WISE_HumanResource.dbo.RU_SalespersonResultType as resulttype WITH(NOLOCK)
		ON results.SalespersonResultTypeId = resulttype.SalespersonResultTypeID

	-- RANKING GROUP
	JOIN WISE_HumanResource.dbo.RU_SalespersonRankingGroup AS rankinggroup WITH(NOLOCK)
		ON rankings.SalespersonRankingGroupId = rankinggroup.SalespersonRankingGroupID

	-- RANKING PERIOD
	JOIN WISE_HumanResource.dbo.RU_SalespersonRankingPeriod AS rankingperiod WITH(NOLOCK)
		ON rankings.SalespersonRankingPeriodId = rankingperiod.SalespersonRankingPeriodID

GO
/* TEST */
-- SELECT * FROM vwRU_UsersSalesRankingConnext
