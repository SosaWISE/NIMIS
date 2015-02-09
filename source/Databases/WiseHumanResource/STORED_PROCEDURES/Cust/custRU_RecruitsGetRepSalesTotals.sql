USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRepSalesTotals')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRepSalesTotals'
		DROP  Procedure  dbo.custRU_RecruitsGetRepSalesTotals
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRepSalesTotals'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRepSalesTotals.sql
**		Name: custRU_RecruitsGetRepSalesTotals
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
CREATE Procedure dbo.custRU_RecruitsGetRepSalesTotals
(
	@SnapShotDate DATETIME--Date for SnapShotDate column
	, @SeasonID INT = NULL----All accounts for season
	, @ExactDate DATETIME = NULL--All accounts on date
	, @StopDate DATETIME = NULL--All accounts before stop date
)
AS
BEGIN


	SELECT
		@SnapShotDate AS SnapShotDate
		
		, StatsTable.RecruitID
		, StatsTable.UserID
		, StatsTable.SeasonID
			
		, StatsTable.NGross
		, StatsTable.NGross_Cancels
		, StatsTable.NGross_ActivationWaives
		, StatsTable.NGross_ExtendedContracts
		, StatsTable.NGross_OtherContracts
		, StatsTable.NGross_CellUnits
		, StatsTable.NGross_SameDay
		, StatsTable.TotalGross_CreditScores
		
		, StatsTable.NExcellent
		, StatsTable.NExcellent_Cancels
		, StatsTable.NExcellent_ActivationWaives
		, StatsTable.NExcellent_ExtendedContracts
		, StatsTable.NExcellent_OtherContracts
		, StatsTable.NExcellent_CellUnits
		, StatsTable.NExcellent_SameDay
		, StatsTable.TotalExcellent_CreditScores
		
		, StatsTable.NPass
		, StatsTable.NPass_Cancels
		, StatsTable.NPass_ActivationWaives
		, StatsTable.NPass_ExtendedContracts
		, StatsTable.NPass_OtherContracts
		, StatsTable.NPass_CellUnits
		, StatsTable.NPass_SameDay
		, StatsTable.TotalPass_CreditScores
		
		, StatsTable.NSub
		, StatsTable.NSub_Cancels
		, StatsTable.NSub_ActivationWaives
		, StatsTable.NSub_ExtendedContracts
		, StatsTable.NSub_OtherContracts
		, StatsTable.NSub_CellUnits
		, StatsTable.NSub_SameDay
		, StatsTable.TotalSub_CreditScores
		
	FROM
	(
		SELECT
			
			RecUser.UserID
			, RecUser.RecruitID
			, AI.SeasonID
			
			--Gross
			, COUNT(*) AS NGross
			, SUM (CASE WHEN (AI.Status <> 'OK') THEN 1 ELSE 0 END) AS NGross_Cancels
			, SUM (CASE WHEN (AI.ActivationFee = 0) THEN 1 ELSE 0 END) AS NGross_ActivationWaives
			, SUM (CASE WHEN (AI.ContractLength = 60) THEN 1 ELSE 0 END) AS NGross_ExtendedContracts
			, SUM (CASE WHEN (AI.ContractLength <> 60) THEN 1 ELSE 0 END) AS NGross_OtherContracts
			, SUM (CASE WHEN (AI.IsCellAccount = 1) THEN 1 ELSE 0 END) AS NGross_CellUnits
			-------Count as same day install if the install is done before 4:00 AM on the day after qualification
			, SUM (CASE WHEN (DATEDIFF(SECOND, DATEADD(HOUR, 4, DATEADD(dd, 1, DATEDIFF(dd, 0, AI.QualificationDate))), AI.InstallDate) < 0) THEN 1 ELSE 0 END) AS NGross_SameDay
			, SUM (AI.CreditScore) AS TotalGross_CreditScores
			
			--Excellent
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) THEN 1 ELSE 0 END) AS NExcellent
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) AND (AI.Status <> 'OK') THEN 1 ELSE 0 END) AS NExcellent_Cancels
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) AND (AI.ActivationFee = 0) THEN 1 ELSE 0 END) AS NExcellent_ActivationWaives
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) AND (AI.ContractLength = 60) THEN 1 ELSE 0 END) AS NExcellent_ExtendedContracts
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) AND (AI.ContractLength <> 60) THEN 1 ELSE 0 END) AS NExcellent_OtherContracts
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) AND (AI.IsCellAccount = 1) THEN 1 ELSE 0 END) AS NExcellent_CellUnits
			-------Count as same day install if the install is done before 4:00 AM on the day after qualification
			, SUM (CASE
					WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold)
						AND (DATEDIFF(SECOND, DATEADD(HOUR, 4, DATEADD(dd, 1, DATEDIFF(dd, 0, AI.QualificationDate))), AI.InstallDate) < 0) THEN 1
					ELSE 0
				END) AS NExcellent_SameDay
			, SUM (CASE WHEN (AI.CreditScore >= RS.ExcellentCreditScoreThreshold) THEN AI.CreditScore ELSE 0 END) AS TotalExcellent_CreditScores
			
			--Pass
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) THEN 1 ELSE 0 END) AS NPass
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) AND (AI.Status <> 'OK') THEN 1 ELSE 0 END) AS NPass_Cancels
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) AND (AI.ActivationFee = 0) THEN 1 ELSE 0 END) AS NPass_ActivationWaives
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) AND (AI.ContractLength = 60) THEN 1 ELSE 0 END) AS NPass_ExtendedContracts
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) AND (AI.ContractLength <> 60) THEN 1 ELSE 0 END) AS NPass_OtherContracts
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) AND (AI.IsCellAccount = 1) THEN 1 ELSE 0 END) AS NPass_CellUnits
			-------Count as same day install if the install is done before 4:00 AM on the day after qualification
			, SUM (CASE
					WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold)
						AND (DATEDIFF(SECOND, DATEADD(HOUR, 4, DATEADD(dd, 1, DATEDIFF(dd, 0, AI.QualificationDate))), AI.InstallDate) < 0) THEN 1
					ELSE 0
				END) AS NPass_SameDay
			, SUM (CASE WHEN (AI.CreditScore >= RS.PassCreditScoreThreshold AND AI.CreditScore < RS.ExcellentCreditScoreThreshold) THEN AI.CreditScore ELSE 0 END) AS TotalPass_CreditScores
					
			--Subs
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) THEN 1 ELSE 0 END) AS NSub
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) AND (AI.Status <> 'OK') THEN 1 ELSE 0 END) AS NSub_Cancels
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) AND (AI.ActivationFee = 0) THEN 1 ELSE 0 END) AS NSub_ActivationWaives
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) AND (AI.ContractLength = 60) THEN 1 ELSE 0 END) AS NSub_ExtendedContracts
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) AND (AI.ContractLength <> 60) THEN 1 ELSE 0 END) AS NSub_OtherContracts
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) AND (AI.IsCellAccount = 1) THEN 1 ELSE 0 END) AS NSub_CellUnits
			-------Count as same day install if the install is done before 4:00 AM on the day after qualification
			, SUM (CASE
					WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold)
						AND (DATEDIFF(SECOND, DATEADD(HOUR, 4, DATEADD(dd, 1, DATEDIFF(dd, 0, AI.QualificationDate))), AI.InstallDate) < 0) THEN 1
					ELSE 0
				END) AS NSub_SameDay
			, SUM (CASE WHEN (AI.CreditScore >= 1 AND AI.CreditScore < RS.PassCreditScoreThreshold) THEN AI.CreditScore ELSE 0 END) AS TotalSub_CreditScores

		FROM SAE_AccountsInstalled AS AI WITH (NOLOCK)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			(AI.SalesRepUserID = RecUser.UserID)
			AND (AI.SeasonID = RecUser.SeasonID)
		INNER JOIN RU_Season AS RS WITH(NOLOCK)
		ON
			AI.SeasonID = RS.SeasonID
		WHERE
			(RecUser.UserID <> 5799)--Keep out corporate recruit
			AND (@SeasonID IS NULL OR AI.SeasonID = @SeasonID)
			AND (@ExactDate IS NULL OR dbo.NormalizeReportingDate(AI.InstallDate) = @ExactDate)
			AND (@StopDate IS NULL OR dbo.NormalizeReportingDate(AI.InstallDate) < @StopDate)
		GROUP BY
			RecUser.UserID
			, RecUser.RecruitID
			, AI.SeasonID
	) AS StatsTable
	ORDER BY
		StatsTable.UserID
		, StatsTable.RecruitID
		, StatsTable.SeasonID

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRepSalesTotals TO PUBLIC
GO