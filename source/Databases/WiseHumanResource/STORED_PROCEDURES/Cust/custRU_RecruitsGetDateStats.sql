USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetDateStats')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetDateStats'
		DROP  Procedure  dbo.custRU_RecruitsGetDateStats
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetDateStats'
GO
/******************************************************************************
**		File: custRU_RecruitsGetDateStats.sql
**		Name: custRU_RecruitsGetDateStats
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
CREATE Procedure dbo.custRU_RecruitsGetDateStats
(
	@RecruitID INT
	
	, @WeekStartDate DATETIME
	, @WeekEndDate DATETIME
	
	, @MonthStartDate DATETIME
	, @MonthEndDate DATETIME
)
AS
BEGIN
	DECLARE @IsRepOrTech INT
	DECLARE @UserID INT
	DECLARE @SeasonID INT
	DECLARE @PassCreditScoreThreshold INT
	SELECT
		@IsRepOrTech = (CASE WHEN RecUser.RoleLocationID = 2 THEN 0 ELSE 1 END)
		, @UserID = RecUser.UserID
		, @SeasonID = RecUser.SeasonID
		, @PassCreditScoreThreshold = RS.PassCreditScoreThreshold
	FROM VW_RecruitUser AS RecUser WITH(NOLOCK)
	INNER JOIN RU_Season AS RS WITH(NOLOCK)
	ON
		RecUser.SeasonID = RS.SeasonID
	WHERE
		RecruitID = @RecruitID
	

	SELECT
		COUNT(*) AS NTotalInstalls
		
		, SUM(CASE
				WHEN (
						(@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if rep check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NTotalNetInstalls
		, SUM(CASE
				WHEN (
						(@WeekStartDate <= AI.InstallDate AND AI.InstallDate <= @WeekEndDate)
						AND (@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if rep check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NWeekNetInstalls
		, SUM(CASE
				WHEN (
						(@MonthStartDate <= AI.InstallDate AND AI.InstallDate <= @MonthEndDate)
						AND (@IsRepOrTech = 0 OR (AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK'))--if rep check creditscore and status 
					) THEN 1
				ELSE 0
			END) AS NMonthNetInstalls

	FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
	INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
	ON
		(
			(@IsRepOrTech = 0 AND AI.TechnicianUserID = RecUser.UserID)
			OR (@IsRepOrTech = 1 AND AI.SalesRepUserID = RecUser.UserID)
		)
		AND AI.SeasonID = RecUser.SeasonID
		AND RecUser.RecruitID = @RecruitID
	GROUP BY
		RecUser.RecruitID
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetDateStats TO PUBLIC
GO