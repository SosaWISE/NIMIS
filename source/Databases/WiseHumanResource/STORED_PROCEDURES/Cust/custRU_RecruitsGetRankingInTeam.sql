USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRankingInTeam')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRankingInTeam'
		DROP  Procedure  dbo.custRU_RecruitsGetRankingInTeam
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRankingInTeam'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRankingInTeam.sql
**		Name: custRU_RecruitsGetRankingInTeam
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
CREATE Procedure dbo.custRU_RecruitsGetRankingInTeam
(
	@recruitID INT,
	@roleLocationID INT = 1
)
AS
BEGIN
	DECLARE @result BIGINT

	IF @roleLocationID = 1 -- Sales Rep
	BEGIN
		SET @result = (SELECT TeamRank
			FROM (SELECT *
						, RANK() OVER (ORDER BY NValidSales DESC) AS TeamRank
					FROM (SELECT DISTINCT RR.RecruitID
							, COUNT (SLS.AccountID) OVER (PARTITION BY SLS.SalesRepUserID) AS NValidSales
						FROM RU_Recruits AS RR WITH (NOLOCK)
						INNER JOIN SAE_RecruitTeamMappings AS TMS WITH (NOLOCK)
						ON
							RR.RecruitID = TMS.RecruitID
							AND TMS.TeamID = (SELECT TeamID FROM SAE_RecruitTeamMappings WHERE RecruitID = @recruitID)
						INNER JOIN RU_Users AS RU WITH (NOLOCK)
						ON
							RR.UserID = RU.UserID
						INNER JOIN SAE_ValidSales AS SLS WITH (NOLOCK)
						ON
							SLS.SalesRepUserID = RR.UserID
							AND SLS.SeasonID = RR.SeasonID
						WHERE
							RR.IsActive = 1 AND RR.IsDeleted = 0
						) AS Sales
					) AS TeamRankings
			WHERE RecruitID = @recruitID)
	END
	ELSE IF @roleLocationID = 2 -- Installer
	BEGIN
		SET @result = (SELECT TeamRank
			FROM (SELECT *
						, RANK() OVER (ORDER BY NValidInstalls DESC) AS TeamRank
					FROM (SELECT DISTINCT RR.RecruitID
							, COUNT (SLS.AccountID) OVER (PARTITION BY SLS.TechnicianUserID) AS NValidInstalls
						FROM RU_Recruits AS RR WITH (NOLOCK)
						INNER JOIN SAE_RecruitTeamMappings AS TMS WITH (NOLOCK)
						ON
							RR.RecruitID = TMS.RecruitID
							AND TMS.TeamID = (SELECT TeamID FROM SAE_RecruitTeamMappings WHERE RecruitID = @recruitID)
						INNER JOIN RU_Users AS RU WITH (NOLOCK)
						ON
							RR.UserID = RU.UserID
						INNER JOIN SAE_ValidSales AS SLS WITH (NOLOCK)
						ON
							SLS.TechnicianUserID = RR.UserID
							AND SLS.SeasonID = RR.SeasonID
						WHERE
							RR.IsActive = 1 AND RR.IsDeleted = 0
						) AS Sales
					) AS TeamRankings
			WHERE RecruitID = @recruitID)
	END
	ELSE
	BEGIN
		SET @result = NULL
	END

	SELECT @result AS RankInTeam
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRankingInTeam TO PUBLIC
GO