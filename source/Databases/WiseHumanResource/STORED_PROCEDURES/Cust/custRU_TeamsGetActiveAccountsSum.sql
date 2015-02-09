USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetActiveAccountsSum')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetActiveAccountsSum'
		DROP  Procedure  dbo.custRU_TeamsGetActiveAccountsSum
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetActiveAccountsSum'
GO
/******************************************************************************
**		File: custRU_TeamsGetActiveAccountsSum.sql
**		Name: custRU_TeamsGetActiveAccountsSum
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
CREATE Procedure dbo.custRU_TeamsGetActiveAccountsSum
(
	@TeamIDList NVARCHAR(MAX)
	, @SeasonID INT
	, @IsCanceled BIT = NULL
	, @IsDelinquent BIT = NULL
	, @HasRepHold BIT = NULL
	, @HasTechHold BIT = NULL
)
WITH RECOMPILE
AS
BEGIN



	SELECT
		RT.TeamID
		, RT.Description AS TeamName
		, RUTL.SeasonID
		, COALESCE(StatsTable.NAccounts, 0) AS NAccounts
	FROM RU_Teams AS RT WITH(NOLOCK)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		RT.TeamLocationID = RUTL.TeamLocationID
	INNER JOIN
	(
		SELECT
			TMap.TeamID
			, SUM(AccountStats.NAccounts) AS NAccounts
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
			TMap.TeamID IN (SELECT * FROM dbo.SplitIntList(@TeamIDList))
		GROUP BY
			TMap.TeamID
	) AS StatsTable
	ON
		RT.TeamID = StatsTable.TeamID
	ORDER BY
		TeamName



END
GO

GRANT EXEC ON dbo.custRU_TeamsGetActiveAccountsSum TO PUBLIC
GO