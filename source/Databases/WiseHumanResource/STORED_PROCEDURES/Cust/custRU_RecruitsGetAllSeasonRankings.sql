USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetAllSeasonRankings')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetAllSeasonRankings'
		DROP  Procedure  dbo.custRU_RecruitsGetAllSeasonRankings
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetAllSeasonRankings'
GO
/******************************************************************************
**		File: custRU_RecruitsGetAllSeasonRankings.sql
**		Name: custRU_RecruitsGetAllSeasonRankings
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_RecruitsGetAllSeasonRankings
(
	@StartDate DateTime
	, @EndDate DateTime
	, @SeasonIDList NVARCHAR(MAX)
)

WITH RECOMPILE
AS
BEGIN
	--	DECLARE @StartDate DateTime
--	SET @StartDate = '1/1/2007 00:00:00.000'
--
--	DECLARE @EndDate DateTime
--	SET @EndDate = '04/21/2008 23:59:59.997'
--
--	DECLARE @SeasonIDList NVARCHAR(MAX)
--	SET @SeasonIDList = '1,2,3,4,5,6,7'

	-- Get last recruit for user
	DECLARE @LastRecruit TABLE
	(
		RecruitID INT
		, SeasonID INT
		, UserID INT
		, [Description] NVARCHAR(50)
	)
	INSERT INTO @LastRecruit
	SELECT
		RR.RecruitID
		, RR.SeasonID
		, RR.UserID
		, RR.Description
	FROM
	(
		SELECT
			RR.RecruitID
			, RR.UserID
			, RR.SeasonID
			, RUT.Description
			-- Order for lastest using the start date of the season
			, Rank() OVER (PARTITION BY RR.UserID ORDER BY RS.StartDate DESC) AS RecruitOrder
		FROM RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
		ON
			(RR.UserTypeID = RUT.UserTypeID)
		INNER JOIN RU_Season AS RS WITH(NOLOCK)
		ON
			RR.SeasonID = RS.SeasonID
		INNER JOIN SplitIntList(@SeasonIDList) AS Ids
		ON
			(RR.SeasonID = ID)
	) RR
	WHERE RR.RecruitOrder = 1

	--Get all sales/installs in Season and Between dates	
	DECLARE @Both TABLE (GPSalesRepID NVARCHAR(25), GPTechnicianID NVARCHAR(25))
	INSERT INTO @Both
	SELECT
		MSA.GPSalesRepID
		, MSA.GPTechnicianID
	FROM Platinum_Protection_InterimCRM.dbo.MS_Account AS MSA WITH(NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
	ON
		MSA.AccountID = ST.AccountID
	INNER JOIN RU_TeamLocations AS TeamLocs
	ON
		(MSA.TeamLocationID = TeamLocs.TeamLocationID)
--		AND (TeamLocs.SeasonID = @SeasonID) -- Filter TeamLocation By Season
	INNER JOIN SplitIntList(@SeasonIDList) AS Ids
	ON
		(TeamLocs.SeasonID = ID)
	--INNER JOIN VW_MaxCreditScores AS MaxScoreTable
	INNER JOIN SAE_MaxCredit AS MaxScoreTable WITH (NOLOCK)
	ON
		(MSA.AccountID = MaxScoreTable.AccountID)
	WHERE
		-- Between Start and End Dates
		(ST.InstallDate BETWEEN @StartDate AND @EndDate)
		-- Only count Scores 600 and above
		AND (MaxScoreTable.CreditScore >= 600)
	ORDER BY
		MSA.GPSalesRepID
		, MSA.GPTechnicianID


	-- Sales Rank Table - sets Rank for Sales Reps
	DECLARE @SalesCount TABLE (Accounts INT, CompanyID NVARCHAR(25))
	INSERT INTO @SalesCount
	SELECT
		COUNT(GPSalesRepID)
		, GPSalesRepID
	FROM @Both
	GROUP BY
		GPSalesRepID
	ORDER BY
		COUNT(GPSalesRepID) DESC

	-- Installs Rank Table - sets Rank for Technicians
	DECLARE @InstallsCount TABLE (Accounts INT, CompanyID NVARCHAR(25))
	INSERT INTO @InstallsCount
	SELECT
		COUNT(GPTechnicianID)
		, GPTechnicianID
	FROM @Both
	GROUP BY
		GPTechnicianID
	ORDER BY
		COUNT(GPTechnicianID) DESC


	--Return Sales
	SELECT
		Rankings.Rank
		, Rankings.Accounts
		, Rankings.FullName
		, Rankings.UserID
		, LR.Description
		--, Rankings.CompanyID
	FROM
	(
		SELECT DISTINCT
			RANK() OVER(ORDER BY Counts.Accounts DESC) AS Rank
			, Counts.Accounts
			, RU.UserID
			, RU.FullName
			--, R.CompanyID
		FROM @SalesCount AS Counts
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(Counts.CompanyID = RU.GPEmployeeID)
	) AS Rankings
	LEFT OUTER JOIN @LastRecruit AS LR
	ON
		Rankings.UserID = LR.UserID
	ORDER BY
		Rankings.Rank
		, Rankings.FullName

	--Return Installs
	SELECT
		Rankings.Rank
		, Rankings.Accounts
		, Rankings.FullName
		, Rankings.UserID
		, LR.Description
		--, Rankings.CompanyID
	FROM
	(
		SELECT DISTINCT
			RANK() OVER(ORDER BY Counts.Accounts DESC) AS Rank
			, Counts.Accounts
			, RU.UserID
			, RU.FullName
			--, R.CompanyID
		FROM @InstallsCount AS Counts
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(Counts.CompanyID = RU.GPEmployeeID)
	) AS Rankings
	LEFT OUTER JOIN @LastRecruit AS LR
	ON
		Rankings.UserID = LR.UserID
	ORDER BY
		Rankings.Rank
		, Rankings.FullName

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetAllSeasonRankings TO PUBLIC
GO