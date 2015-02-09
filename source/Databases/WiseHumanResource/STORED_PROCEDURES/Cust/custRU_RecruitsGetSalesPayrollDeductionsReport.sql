USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetSalesPayrollDeductionsReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetSalesPayrollDeductionsReport'
		DROP  Procedure  dbo.custRU_RecruitsGetSalesPayrollDeductionsReport
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetSalesPayrollDeductionsReport'
GO
/******************************************************************************
**		File: custRU_RecruitsGetSalesPayrollDeductionsReport.sql
**		Name: custRU_RecruitsGetSalesPayrollDeductionsReport
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
CREATE Procedure dbo.custRU_RecruitsGetSalesPayrollDeductionsReport
(
	@ID INT
	, @IDType VARCHAR(10)-- Team, Recruit(Default)
)
--WITH RECOMPILE
AS
BEGIN

	--DECLARE @ID INT
	--SET @ID = 14768--Neal Rogers 2009 Summer
	--SET @ID = 956--Dan Spirgen & Matt Collins Team 
	
	--DECLARE @IDType VARCHAR(10)
	--SET @IDType = 'Recruit'
	--SET @IDType = 'Team'

	DECLARE @SeasonID INT
	
	IF (@IDType = 'Team')
	BEGIN

		SELECT
			@SeasonID = SeasonID
		FROM RU_Teams AS RT
		INNER JOIN RU_TeamLocations AS RUTL
		ON
			(RT.TeamLocationID = RUTL.TeamLocationID)
		WHERE
			(RT.TeamID = @ID)

	END
	ELSE
	BEGIN

		SELECT 
			@SeasonID = SeasonID
		FROM RU_Recruits AS RR WITH (NOLOCK)
		WHERE
			(RR.RecruitID = @ID)

	END
	
	DECLARE @ExcellentCreditScoreThreshold INT
	DECLARE @PassCreditScoreThreshold INT
	SELECT
		@ExcellentCreditScoreThreshold = RS.ExcellentCreditScoreThreshold
		, @PassCreditScoreThreshold = RS.PassCreditScoreThreshold
	FROM RU_Season AS RS WITH(NOLOCK)
	WHERE
		RS.SeasonID = @SeasonID

	SELECT
		RecUser.FullName
		, RecUser.PublicFullName
		, RecUser.UserID
		, RecUser.RecruitID
		
		, Totals.NTotalInstalls
		, Totals.NNetInstalls
		, Totals.NActivationWaives
		
		, Totals.NTotalRMRLowered
		, Totals.NRMRLowered1
		, Totals.NRMRLowered2
		, Totals.NRMRLowered3
		, Totals.NRMRLowered4
		
		, Totals.NManualBills
		
		, Totals.N39Month
		
		, Totals.NMissingInvoiceFees
		
		, PB.PointBank AS NAdjustedPointBank
	FROM
	(
		SELECT
			COUNT(*) AS NTotalInstalls
			
			
			, SUM(CASE
					WHEN AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK' THEN 1
					ELSE 0
				END) AS NNetInstalls
			, SUM(CASE
					WHEN PayrollAccounts.IsActivationWaive = 1 THEN 1
					ELSE 0
				END) AS NActivationWaives
			, SUM(CASE
					WHEN PayrollAccounts.IsAutoPay = 0 THEN 1
					ELSE 0
				END) AS NManualBills
			, SUM(CASE
					WHEN PayrollAccounts.IsMissingInvoiceFee = 1 THEN 1
					ELSE 0
				END) AS NMissingInvoiceFees
				
			, SUM(CASE
					WHEN 0 < (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR) THEN 1
					ELSE 0
				END) AS NTotalRMRLowered
			, SUM(CASE
					WHEN 0 < (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR)
						AND (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR) <= 1 THEN 1
					ELSE 0
				END) AS NRMRLowered1
			, SUM(CASE
					WHEN 1 < (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR)
						AND (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR) <= 2 THEN 1
					ELSE 0
				END) AS NRMRLowered2
			, SUM(CASE
					WHEN 2 < (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR)
						AND (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR) <= 3 THEN 1--between 2 and 3(included)
					ELSE 0
				END) AS NRMRLowered3
			, SUM(CASE
					WHEN 3 < (PayrollAccounts.FullPriceRMR - PayrollAccounts.RMR) THEN 1--greater than 3
					ELSE 0
				END) AS NRMRLowered4
				
			, SUM(CASE
					WHEN AI.ContractLength < 60 THEN 1
					ELSE 0
				END) AS N39Month
				
				--AI.ContractLength < 60
			
			, RecUser.RecruitID
		FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			(AI.SalesRepUserID = RecUser.UserID)
			AND (AI.SeasonID = RecUser.SeasonID)
		INNER JOIN SAE_RecruitingStructure AS TMap WITH(NOLOCK)
		ON
			(RecUser.RecruitID = TMap.RecruitID)
		INNER JOIN--LEFT OUTER JOIN
		(
			SELECT
				PayrollAccounts.AccountID
				, PayrollAccounts.IsActivationWaive
				, PayrollAccounts.IsAutoPay
				, PayrollAccounts.IsMissingInvoiceFee
				, PayrollAccounts.FullPriceRMR
				, PayrollAccounts.RMR
			FROM vwPR_AccountPayrollStatesLatestByAccount AS PayrollAccounts WITH(NOLOCK)
		) AS PayrollAccounts
		ON
			AI.AccountID = PayrollAccounts.AccountID
		WHERE
			(
				((@IDType = 'Team') AND (TMap.TeamID = @ID))
				OR (RecUser.RecruitID = @ID)
			)
			AND (RecUser.IsDeleted = 0)
		GROUP BY
			RecUser.RecruitID
	) AS Totals
	INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
	ON
		Totals.RecruitID = RecUser.RecruitID
	LEFT OUTER JOIN
	(
		SELECT
			GPSalesRepID
			, PointBank
		FROM vwPR_SeasonPointBank AS SPB
		WHERE
			SeasonID = @SeasonID
	) AS PB
	ON
		RecUser.GPEmployeeID = PB.GPSalesRepID
	ORDER BY
		RecUser.FullName


END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetSalesPayrollDeductionsReport TO PUBLIC
GO