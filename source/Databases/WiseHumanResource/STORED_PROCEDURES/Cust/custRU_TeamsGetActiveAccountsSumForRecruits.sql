USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetActiveAccountsSumForRecruits')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetActiveAccountsSumForRecruits'
		DROP  Procedure  dbo.custRU_TeamsGetActiveAccountsSumForRecruits
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetActiveAccountsSumForRecruits'
GO
/******************************************************************************
**		File: custRU_TeamsGetActiveAccountsSumForRecruits.sql
**		Name: custRU_TeamsGetActiveAccountsSumForRecruits
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
CREATE Procedure dbo.custRU_TeamsGetActiveAccountsSumForRecruits
(
	@TeamID INT
	, @IsCanceled BIT = NULL
	, @IsDelinquent BIT = NULL
	, @HasRepHold BIT = NULL
	, @HasTechHold BIT = NULL
)
WITH RECOMPILE
AS
BEGIN



	DECLARE @SeasonID INT
	SELECT
		@SeasonID = RUTL.SeasonID
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		RT.TeamLocationID = RUTL.TeamLocationID
	WHERE
		RT.TeamID = @TeamID


	SELECT
		RecUser.GPEmployeeID AS CompanyID
		, RecUser.UserID
		, RecUser.RecruitID
		, RecUser.FullName
		, RecUser.PublicFullName
		, RecUser.SeasonID
		, RecUser.IsDeleted
		, COALESCE(StatsTable.NAccounts, 0) AS NAccounts
	FROM VW_RecruitUser AS RecUser WITH(NOLOCK)
	INNER JOIN
	(
		SELECT
			RecUser.RecruitID
			, SUM(AccountStats.NAccounts) AS NAccounts
			
			--AccountStats.UserID
			--, AccountStats.NAccounts
		FROM SAE_RecruitingStructure AS TMap WITH(NOLOCK)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			(TMap.RecruitID = RecUser.RecruitID)
		LEFT OUTER JOIN
		(
			SELECT
				AI.SalesRepUserID AS UserID
				, COUNT(*) AS NAccounts
			FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
			INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_Account MSA WITH (NOLOCK)
			ON
				AI.AccountID = MSA.AccountID
			LEFT JOIN Platinum_Protection_InterimCRM.dbo.SAE_AccountDelinquentStatus AS ADS WITH (NOLOCK)
			ON
				(ADS.AccountID = AI.AccountID)
			LEFT JOIN Platinum_Protection_InterimCRM.dbo.vwMC_AccountHoldAccountHoldStatus AS HLD WITH (NOLOCK)
			ON
				(HLD.AccountID = AI.AccountID)
			WHERE
				(AI.InstallDate IS NOT NULL)
				AND (AI.SeasonID = @SeasonID)
				AND (@IsCanceled IS NULL OR (@IsCanceled = 0 AND AI.Status = 'OK') OR (@IsCanceled = 1 AND AI.Status <> 'OK'))
				AND (@IsDelinquent IS NULL OR
						(
							COALESCE(ADS.IsThirtyDaysPastDue, 0) = @IsDelinquent
							OR COALESCE(ADS.IsSixtyDaysPastDue, 0) = @IsDelinquent
							OR COALESCE(ADS.IsNinetyPlusDaysPastDue, 0) = @IsDelinquent
						)
					)
				AND (@HasRepHold IS NULL OR (COALESCE(HLD.HasRepFrontEndHold, 0) = @HasRepHold OR COALESCE(HLD.HasRepBackEndHold, 0) = @HasRepHold))
				AND (@HasTechHold IS NULL OR (COALESCE(HLD.HasRepFrontEndHold, 0) = @HasTechHold OR COALESCE(HLD.HasRepBackEndHold, 0) = @HasTechHold))
			GROUP BY
				AI.SalesRepUserID
		) AS AccountStats
		ON
			RecUser.UserID = AccountStats.UserID
		WHERE
			TMap.TeamID = @TeamID
		GROUP BY
			RecUser.RecruitID
	) AS StatsTable
	ON
		RecUser.RecruitID = StatsTable.RecruitID
	ORDER BY RecUser.IsDeleted ASC, StatsTable.NAccounts DESC, RecUser.FullName



END
GO

GRANT EXEC ON dbo.custRU_TeamsGetActiveAccountsSumForRecruits TO PUBLIC
GO