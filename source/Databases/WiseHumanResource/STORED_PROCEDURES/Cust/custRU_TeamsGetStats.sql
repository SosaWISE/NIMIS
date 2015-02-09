USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetStats')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetStats'
		DROP  Procedure  dbo.custRU_TeamsGetStats
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetStats'
GO
/******************************************************************************
**		File: custRU_TeamsGetStats.sql
**		Name: custRU_TeamsGetStats
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
CREATE Procedure dbo.custRU_TeamsGetStats
(
	@TeamID INT
	, @IsDeleted BIT
)
AS
BEGIN


	DECLARE @ExcellentNum INT
	SET @ExcellentNum = 625
	DECLARE @PassNum INT
	SET @PassNum = 600
	
	DECLARE @TodayEnd DATETIME
	SET @TodayEnd = dbo.GetDateEnd(GETDATE())

	DECLARE @SeasonID INT
	DECLARE @RoleLocationID INT
	SELECT
		@SeasonID = SeasonID
		, @RoleLocationID = RT.RoleLocationID
	FROM RU_Teams AS RT
	INNER JOIN RU_TeamLocations AS RTL
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	WHERE
		RT.TeamID = @TeamID

	SELECT
		 StatsTable.UserID-- AS ID
		, StatsTable.RecruitID
		, StatsTable.RepName
		, StatsTable.FullName
		, StatsTable.PublicFullName
		
		, StatsTable.Description
		, StatsTable.PayScale
		, StatsTable.IsDeleted
		, StatsTable.TeamID
		
		, StatsTable.NLast7DaysInstalls
		, StatsTable.NLast30DaysInstalls
		, StatsTable.NTotalPersonalInstalls
			
		--/*
		--, StatsTable.SeasonID
		, StatsTable.NExcellentInstalls
		, StatsTable.NPassInstalls
		, StatsTable.NTotalValidInstalls
		, StatsTable.NCancels
		, StatsTable.NNetInstalls
		, StatsTable.NSubInstalls
		, StatsTable.NSubCancels
		, StatsTable.NSubNetInstalls
		, StatsTable.NCreditsRun
		, StatsTable.NCreditsPassed
		, CASE
				WHEN StatsTable.NCreditsRun > 0 THEN
					ROUND((CAST(StatsTable.NCreditsPassed AS FLOAT) / CAST(StatsTable.NCreditsRun AS FLOAT)) * 100, 0)
				ELSE 0
			END AS CreditPassPercent
		, CASE
				WHEN StatsTable.NCreditsPassed > 0 THEN
					ROUND((CAST(StatsTable.NTotalValidInstalls AS FLOAT) / CAST(StatsTable.NCreditsPassed AS FLOAT)) * 100, 0)
				ELSE 0
			END AS PassAndInstallPercent
		, StatsTable.NSurveysCompleted
		, CASE
				WHEN StatsTable.TotalCreditScores > 0 THEN
					ROUND((CAST(StatsTable.TotalCreditScores AS FLOAT) / CAST((StatsTable.NExcellentInstalls + StatsTable.NPassInstalls) AS FLOAT)), 0)
				ELSE 0
			END AS AvgCreditScore
		, StatsTable.NCellUnits
		, CASE
				WHEN StatsTable.NTotalValidInstalls > 0 OR StatsTable.NSubInstalls > 0 THEN
					ROUND((CAST(StatsTable.NCellUnits AS FLOAT) / CAST((StatsTable.NTotalValidInstalls + StatsTable.NSubInstalls) AS FLOAT)) * 100, 0)
				ELSE 0
			END AS CellUnitPercent
		, StatsTable.NSameDay
		, CASE
				WHEN StatsTable.NTotalValidInstalls > 0 OR StatsTable.NSubInstalls > 0 THEN
					ROUND((CAST(StatsTable.NSameDay AS FLOAT) / CAST((StatsTable.NTotalValidInstalls + StatsTable.NSubInstalls) AS FLOAT)) * 100, 0)
				ELSE 0
			END AS SameDayPercent
		, StatsTable.NActivationWaives
		, CASE
				WHEN StatsTable.NTotalValidInstalls > 0 OR StatsTable.NSubInstalls > 0 THEN
					ROUND((CAST(StatsTable.NActivationWaives AS FLOAT) / CAST((StatsTable.NTotalValidInstalls + StatsTable.NSubInstalls) AS FLOAT)) * 100, 0)
				ELSE 0
			END AS WaivedPercent
		, StatsTable.NExtendedContracts
		, CASE
				WHEN StatsTable.NTotalPersonalInstalls > 0 THEN
					ROUND((CAST(StatsTable.NExtendedContracts AS FLOAT) / CAST(StatsTable.NTotalPersonalInstalls AS FLOAT)) * 100, 0)
				ELSE 0
			END AS ExtendedContractPercent
		, StatsTable.NOtherContracts
		--*/
	FROM
		(SELECT
			RecUser.UserID
			, RecUser.RecruitID
			, RecUser.FullName AS RepName
			, RecUser.FullName
			, RecUser.PublicFullName
			, RecUser.UserType AS Description
			, RecUser.PayScaleName AS PayScale
			, RecUser.IsDeleted
			, RecUser.TeamID
			
			, COALESCE(AccountStats.NLast7DaysInstalls, 0) AS NLast7DaysInstalls
			, COALESCE(AccountStats.NLast30DaysInstalls, 0) AS NLast30DaysInstalls
			, COALESCE(AccountStats.NTotalPersonalInstalls, 0) AS NTotalPersonalInstalls
			
			, RecUser.SeasonID
			, COALESCE(AccountStats.NExcellentInstalls, 0) AS NExcellentInstalls
			, COALESCE(AccountStats.NPassInstalls, 0) AS NPassInstalls
			, COALESCE(AccountStats.NSubInstalls, 0) AS NSubInstalls
			, COALESCE(AccountStats.NCancels, 0) AS NCancels
			, COALESCE(AccountStats.NSubCancels, 0) AS NSubCancels
			, COALESCE(AccountStats.NCellUnits, 0) AS NCellUnits
			, COALESCE(AccountStats.TotalCreditScores, 0) AS TotalCreditScores
			, COALESCE(AccountStats.NActivationWaives, 0) AS NActivationWaives
			, COALESCE(AccountStats.NSameDay, 0) AS NSameDay
			, COALESCE(AccountStats.NExcellentInstalls, 0) + COALESCE(AccountStats.NPassInstalls, 0) AS NTotalValidInstalls
			, COALESCE(AccountStats.NExcellentInstalls, 0) + COALESCE(AccountStats.NPassInstalls, 0) - COALESCE(AccountStats.NCancels, 0) AS NNetInstalls
			, COALESCE(AccountStats.NSubInstalls, 0) - COALESCE(AccountStats.NSubCancels, 0) AS NSubNetInstalls
			, COALESCE(CreditStats.NRun, 0) AS NCreditsRun
			, COALESCE(CreditStats.NPassed, 0) AS NCreditsPassed
			, COALESCE(SurveyStats.NSurveysCompleted, 0) AS NSurveysCompleted
			, COALESCE(AccountStats.NExtendedContracts, 0) AS NExtendedContracts
			, COALESCE(AccountStats.NOtherContracts, 0) AS NOtherContracts

		FROM SAE_RecruitingStructure AS TMap WITH(NOLOCK)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			(TMap.RecruitID = RecUser.RecruitID)
		LEFT OUTER JOIN
			/****
			 * Get Account Stats
			 ****/
			(SELECT DISTINCT AI.SalesRepUserID AS UserID
				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-7) THEN 1
							ELSE 0
						END) AS NLast7DaysInstalls
						
				, SUM(CASE
							WHEN (AI.InstallDate > @TodayEnd-30) THEN 1
							ELSE 0
						END) AS NLast30DaysInstalls

				, SUM(CASE
							WHEN (AI.InstallDate IS NOT NULL) THEN 1
							ELSE 0
						END) AS NTotalPersonalInstalls
						
						
				, SUM (CASE
							WHEN AI.CreditScore >= @ExcellentNum THEN 1
							ELSE 0
						END) AS NExcellentInstalls
				, SUM (CASE
							WHEN AI.CreditScore >= @PassNum AND AI.CreditScore < @ExcellentNum THEN 1
							ELSE 0
						END) AS NPassInstalls
				, SUM (CASE
							WHEN AI.CreditScore >= 1 AND AI.CreditScore < @PassNum THEN 1
							ELSE 0
						END) AS NSubInstalls
				, SUM (CASE
							WHEN (AI.Status <> 'OK') AND (AI.CreditScore >= @PassNum) THEN 1
							ELSE 0
						END) AS NCancels
				, SUM (CASE
							WHEN (AI.Status <> 'OK') AND (AI.CreditScore < @PassNum) THEN 1
							ELSE 0
						END) AS NSubCancels
				, SUM (CASE
							WHEN AI.IsCellAccount = 1 THEN 1
							ELSE 0
						END) AS NCellUnits
				, SUM (CASE
							WHEN AI.CreditScore >= @PassNum THEN AI.CreditScore
							ELSE 0
						END) AS TotalCreditScores
				, SUM (CASE
							WHEN AI.ActivationFee = 0 THEN 1
							ELSE 0
						END) AS NActivationWaives
				, SUM (CASE
							WHEN DATEDIFF(DAY, AI.QualificationDate, AI.InstallDate) = 0 THEN 1
							ELSE 0
						END) AS NSameDay
				, SUM (CASE
							WHEN AI.ContractLength = 60 THEN 1
							ELSE 0
						END) AS NExtendedContracts
				, SUM (CASE
							WHEN AI.ContractLength <> 60 THEN 1
							ELSE 0
						END) AS NOtherContracts
			FROM SAE_AccountsInstalled AS AI WITH (NOLOCK)
			WHERE
				(AI.SeasonID = @SeasonID)
			GROUP BY
				AI.SalesRepUserID
			) AS AccountStats
		ON
			RecUser.UserID = AccountStats.UserID
		LEFT OUTER JOIN
			/****
			 * Get Credit Stats
			 ****/
			(SELECT DISTINCT GPSalesRepID
				, COUNT(CreditScore) AS NRun
				, SUM (CASE
							WHEN CreditScore >= @PassNum THEN 1
							ELSE 0
						END) AS NPassed
			FROM SAE_CreditsRun AS CR WITH(NOLOCK)
			INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
			ON
				CR.TeamLocationID = RUTL.TeamLocationID
			WHERE
				RUTL.SeasonID = @SeasonID
			GROUP BY GPSalesRepID) AS CreditStats
		ON
			RecUser.GPEmployeeID = CreditStats.GPSalesRepID
		LEFT OUTER JOIN
			/****
			 * Get Survey Stats
			 ****/
			(SELECT DISTINCT GPSalesRepID
				, COUNT (AccountID) AS NSurveysCompleted
			FROM VW_AccountPreSurveys AS PS WITH (NOLOCK)
			INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
			ON
				PS.TeamLocationID = RUTL.TeamLocationID
			--HACK
			INNER JOIN RU_Users AS RU WITH(NOLOCK)
			ON
				PS.GPSalesRepID = RU.GPEmployeeID
				AND (RU.UserID NOT IN (SELECT * FROM fxGetExcludeUserIDList()))
			--END HACK
			WHERE
				RUTL.SeasonID = @SeasonID
			GROUP BY GPSalesRepID) AS SurveyStats
		ON
			RecUser.GPEmployeeID = SurveyStats.GPSalesRepID
		
		--WHERE
		--	RR.SeasonID = @SeasonID-- (SELECT SeasonID FROM RU_TeamLocations WHERE TeamLocationID = @TeamLocationID)
			
		WHERE
			(TMap.TeamID = @TeamID)
			AND (@IsDeleted IS NULL OR RecUser.IsDeleted = @IsDeleted)
			AND (RecUser.RoleLocationID = @RoleLocationID)
			--AND (@HasOwnTeam IS NULL
			--		OR (@HasOwnTeam = 1 AND RecUser.TeamID IS NOT NULL)
			--		OR (@HasOwnTeam = 0 AND RecUser.TeamID IS NULL)
			--	)
		
	) AS StatsTable
	--WHERE StatsTable.NTotalValidInstalls > 0 OR StatsTable.NSubInstalls > 0-- OR NCreditsRun > 0
	ORDER BY StatsTable.IsDeleted ASC, StatsTable.NTotalValidInstalls DESC, StatsTable.NExcellentInstalls DESC, StatsTable.NPassInstalls DESC

END
GO

GRANT EXEC ON dbo.custRU_TeamsGetStats TO PUBLIC
GO