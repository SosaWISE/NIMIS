USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRankInSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRankInSeason'
		DROP  Procedure  dbo.custRU_RecruitsGetRankInSeason
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRankInSeason'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRankInSeason.sql
**		Name: custRU_RecruitsGetRankInSeason
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
CREATE Procedure dbo.custRU_RecruitsGetRankInSeason
(
	@SeasonID INT
	, @UserID INT
	, @RoleLocationID INT
)

WITH RECOMPILE
AS
BEGIN

--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @UserID INT
--	SET @UserID = 4146
--
--	DECLARE @RoleLocationID INT
--	SET @RoleLocationID = 1

	DECLARE @StartDate DateTime
	DECLARE @EndDate DateTime

	SELECT
		@StartDate = StartDate
		, @EndDate = EndDate
	FROM RU_Season AS RS WITH(NOLOCK)
	WHERE
		RS.SeasonID = @SeasonID

	--Count Accounts that match criteria
	DECLARE @NumsCount TABLE (Accounts INT, UserID INT)
	INSERT INTO @NumsCount
	SELECT
		CASE
			WHEN @RoleLocationID = 2 THEN COUNT(VS.TechnicianUserID)
			ELSE COUNT(VS.SalesRepUserID)
		END
		, CASE
			WHEN @RoleLocationID = 2 THEN VS.TechnicianUserID
			ELSE VS.SalesRepUserID
		END
	FROM dbo.SAE_ValidSales AS VS WITH(NOLOCK)
	INNER JOIN SAE_MaxCredit AS MaxScoreTable WITH(NOLOCK)
	ON
		(VS.AccountID = MaxScoreTable.AccountID)
	WHERE
		-- In Season
		(VS.SeasonID = @SeasonID)
		---- Between Start and End Dates
		AND (VS.InstallDate BETWEEN @StartDate AND @EndDate)
		-- Only count Scores 600 and above
		AND (MaxScoreTable.CreditScore >= 600)
	GROUP BY
		CASE
			WHEN @RoleLocationID = 2 THEN VS.TechnicianUserID
			ELSE VS.SalesRepUserID
		END


	--Return Sales
	SELECT
		Rankings.Rank
	FROM
	(
		SELECT DISTINCT
			RANK() OVER(ORDER BY Counts.Accounts DESC) AS Rank
			, Counts.Accounts
			, Counts.UserID
		FROM @NumsCount AS Counts
	) AS Rankings 
	WHERE
		UserID = @UserID

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRankInSeason TO PUBLIC
GO