USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetPayrollStats')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetPayrollStats'
		DROP  Procedure  dbo.custRU_RecruitsGetPayrollStats
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetPayrollStats'
GO
/******************************************************************************
**		File: custRU_RecruitsGetPayrollStats.sql
**		Name: custRU_RecruitsGetPayrollStats
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
CREATE Procedure dbo.custRU_RecruitsGetPayrollStats
(
	@RecruitID INT
)
--WITH RECOMPILE
AS
BEGIN


	--DECLARE @RecruitID INT
	--SET @RecruitID = 14768--Neal Rogers 2009 Summer

	DECLARE @RoleLocationID INT
	DECLARE @UserID INT
	DECLARE @SeasonID INT
	DECLARE @ExcellentCreditScoreThreshold INT
	DECLARE @PassCreditScoreThreshold INT
	SELECT
		@RoleLocationID = RoleLocationID
		, @UserID = UserID
		, @SeasonID = RecUser.SeasonID
		, @ExcellentCreditScoreThreshold = RS.ExcellentCreditScoreThreshold
		, @PassCreditScoreThreshold = RS.PassCreditScoreThreshold
	FROM VW_RecruitUser AS RecUser WITH(NOLOCK)
	INNER JOIN RU_Season AS RS WITH(NOLOCK)
	ON
		RecUser.SeasonID = RS.SeasonID
	WHERE
		RecruitID = @RecruitID
		
	--SELECT @RoleLocationID
	--SELECT @UserID
	--SELECT @SeasonID

	DECLARE @IsRepOrTech BIT
	SET @IsRepOrTech = CASE WHEN @RoleLocationID = 2 THEN 0 ELSE 1 END
	--SELECT @IsRepOrTech

	SELECT
		RecUser.RecruitID
		
		, Totals.NNetInstalls
		, Totals.NPassInstalls
		--, Totals.NFrontEndHolds
		--, Totals.NBackEndHolds
		, Totals.NActivationWaives
		, Totals.NManualBills
		, Totals.NMissingInvoiceFees
		
		, PB.PointBank AS NPointBank
	FROM
	(
		SELECT
			COUNT(*) AS NGross
			, SUM(CASE
					WHEN AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status = 'OK' THEN 1
					ELSE 0
				END) AS NNetInstalls
			, SUM(CASE
					WHEN AI.CreditScore >= @PassCreditScoreThreshold AND CreditScore < @ExcellentCreditScoreThreshold AND AI.Status = 'OK' THEN 1
					ELSE 0
				END) AS NPassInstalls
			--, SUM(CASE
			--		WHEN AI.ActivationFee = 0 THEN 1
			--		ELSE 0
			--	END) AS NActivationWaives
				
			, SUM(CASE
					WHEN HLD.IsFrontEndHold = 1 THEN 1
					ELSE 0
				END) AS NFrontEndHolds
			, SUM(CASE
					WHEN HLD.IsBackEndHold = 1 THEN 1
					ELSE 0
				END) AS NBackEndHolds
				
			
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
				
			
			, RecUser.RecruitID
		FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			AI.SalesRepUserID = RecUser.UserID
			AND AI.SeasonID = RecUser.SeasonID
			AND RecUser.RecruitID = @RecruitID
		LEFT OUTER JOIN
		(
			SELECT
				AAH.AccountID
				, AAH.IsFrontEndHold
				, AAH.IsBackEndHold
			FROM Platinum_Protection_InterimCRM.dbo.vwMC_AccountHoldsActive AS AAH WITH(NOLOCK)
			INNER JOIN SAE_AccountsInstalled AS AI WITH(NOLOCK)
			ON
				AAH.AccountID = AI.AccountID
			WHERE
				AI.SalesRepUserID = @UserID
				AND AI.SeasonID = @SeasonID
				--
				AND AAH.RepOrTech = @IsRepOrTech
		) AS HLD
		ON
			AI.AccountID = HLD.AccountID
		INNER JOIN
		--LEFT OUTER JOIN
		(
			SELECT
				PayrollAccounts.AccountID
				, PayrollAccounts.IsActivationWaive
				, PayrollAccounts.IsAutoPay
				, PayrollAccounts.IsMissingInvoiceFee
			FROM vwPR_AccountPayrollStatesLatestByAccount AS PayrollAccounts WITH(NOLOCK)
		) AS PayrollAccounts
		ON
			AI.AccountID = PayrollAccounts.AccountID
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

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetPayrollStats TO PUBLIC
GO