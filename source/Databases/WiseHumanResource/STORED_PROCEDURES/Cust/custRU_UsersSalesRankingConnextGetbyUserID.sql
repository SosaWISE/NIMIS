USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersSalesRankingConnextGetbyUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersSalesRankingConnextGetbyUserID'
		DROP  Procedure  dbo.custRU_UsersSalesRankingConnextGetbyUserID
	END
GO

PRINT 'Creating Procedure custRU_UsersSalesRankingConnextGetbyUserID'
GO
/******************************************************************************
**		File: custRU_UsersSalesRankingConnextGetbyUserID.sql
**		Name: custRU_UsersSalesRankingConnextGetbyUserID
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersSalesRankingConnextGetbyUserID
(
	@UserId INT,
	@resultstype VARCHAR(10),
	@rankinggroup VARCHAR(10),
	@rankingperiod VARCHAR(10),
	@rows INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @userSequence INT
	DECLARE @maxperiod DATETIME
	DECLARE @prevperiod DATETIME
	
	BEGIN TRY
		-- GET LAST PERIOD
		SELECT @maxperiod = MAX(DISTINCT PeriodEndingDate) 
		FROM dbo.vwRU_UsersSalesRankingConnext WITH(NOLOCK)

		-- GET PREVIOUS PERIOD
		SELECT @prevperiod = MIN(DISTINCT PeriodEndingDate) 
		FROM dbo.vwRU_UsersSalesRankingConnext WITH(NOLOCK)

		-- GET THE USER'S CURRENT SEQUENCE NUMBER
		SELECT @userSequence = CurrentSequence
		FROM dbo.vwRU_UsersSalesRankingConnext WITH(NOLOCK)
		WHERE 
			UserID = @userID
			AND PeriodEndingDate = @maxperiod
			AND ResultsType = @resultstype
			AND RankingGroup = @rankinggroup
			AND RankingPeriod = @rankingperiod

		-- RETURN COLUMNS
		SELECT 
			CurrentPeriod.UserID AS UserID, 
			CurrentPeriod.FirstName AS FirstName, 
			CurrentPeriod.MiddleName AS MiddleName, 
			CurrentPeriod.LastName AS LastName, 
			CurrentPeriod.PhotoURL AS PhotoURL, 
			CurrentPeriod.PeriodEndingDate AS PeriodEndingDate, 
			CurrentPeriod.ResultsType AS ResultsType, 
			CurrentPeriod.RankingGroup AS RankingGroup, 
			CurrentPeriod.RankingPeriod AS RankingPeriod, 
			CurrentPeriod.CurrentResults AS CurrentResults, 
			CurrentPeriod.CurrentSequence AS CurrentSequence, 
			CurrentPeriod.CurrentRanking AS CurrentRanking,
			PreviousPeriod.CurrentResults AS PreviousResults,
			PreviousPeriod.CurrentSequence AS PreviousSequence,
			PreviousPeriod.CurrentRanking AS PreviousRanking
		FROM 
			dbo.vwRU_UsersSalesRankingConnext AS CurrentPeriod WITH(NOLOCK)
			JOIN dbo.vwRU_UsersSalesRankingConnext AS PreviousPeriod WITH(NOLOCK)
				ON CurrentPeriod.UserID = PreviousPeriod.UserID
				AND PreviousPeriod.PeriodEndingDate = @prevperiod
				AND PreviousPeriod.ResultsType = CurrentPeriod.ResultsType
				AND PreviousPeriod.RankingGroup = CurrentPeriod.RankingGroup
				AND PreviousPeriod.RankingPeriod = CurrentPeriod.RankingPeriod
		WHERE 
			CurrentPeriod.PeriodEndingDate = @maxperiod
			AND CurrentPeriod.ResultsType = @resultstype
			AND CurrentPeriod.RankingGroup = @rankinggroup
			AND CurrentPeriod.RankingPeriod = @rankingperiod
			AND CurrentPeriod.CurrentSequence BETWEEN 
				CASE WHEN @userSequence < @rows THEN 0 ELSE @usersequence-@rows END
				AND 
				@userSequence+@rows
		ORDER BY CurrentPeriod.CurrentSequence	
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custRU_UsersSalesRankingConnextGetbyUserID TO PUBLIC
GO

/** 
EXEC dbo.custRU_UsersSalesRankingConnextGetbyUserID 1164, 'SALES', 'NATIONAL', 'YEAR', 10 
EXEC dbo.custRU_UsersSalesRankingConnextGetbyUserID 1164, 'SALES', 'REGIONAL', 'YEAR', 10 
EXEC dbo.custRU_UsersSalesRankingConnextGetbyUserID 1164, 'SALES', OFFICE', 'YEAR', 10 
EXEC dbo.custRU_UsersSalesRankingConnextGetbyUserID 1164, 'SALES', 'TEAM', 'YEAR', 10 


custRU_UsersSalesRankingConnextGetbyUserID @UserId, @resultstype, @rankinggroup, @rankingperiod, @rows
resultstype = SALES, QUALITY - right now QUALITY not being used
rankinggroup = NATIONAL, REGIONAL, OFFICE, TEAM
rankingperiod = YEAR, MONTH, WEEK
*/
