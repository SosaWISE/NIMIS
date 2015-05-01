USE [NXSE_Commissions]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SCv2_0_SC_WorkAccountRecruitingBonusReconciliation')
	BEGIN
		PRINT 'Dropping Procedure SCv2_0_SC_WorkAccountRecruitingBonusReconciliation'
		DROP  Procedure  dbo.SCv2_0_SC_WorkAccountRecruitingBonusReconciliation
	END
GO

PRINT 'Creating Procedure SCv2_0_SC_WorkAccountRecruitingBonusReconciliation'
GO
/******************************************************************************
**		File: SCv2_0_SC_WorkAccountRecruitingBonusReconciliation.sql
**		Name: SCv2_0_SC_WorkAccountRecruitingBonusReconciliation
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
**		Auth: Andres Sosa
**		Date: 04/22/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/22/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.SCv2_0_SC_WorkAccountRecruitingBonusReconciliation
(
	@WorkAccountID BIGINT
	, @CommissionPeriodId BIGINT = 1
	, @AccountId BIGINT = NULL
	, @SalesRepId VARCHAR(25)
	, @PaidToSalesRepId VARCHAR(25) -- Recruiter
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @WorkAccountAdjustmentID BIGINT;
	
	BEGIN TRY

		/** Check to see if this is @SalesRepId's first account. And that this account has not had his Recruiting bonus paid out. */
		IF EXISTS(
			SELECT
				RRL.AccountId
			FROM
				(SELECT TOP 1
					SCWA.AccountID
				FROM
					[dbo].SC_WorkAccountAdjustments AS SCWAA WITH (NOLOCK)
					INNER JOIN [dbo].SC_WorkAccountsAll AS SCWA WITH (NOLOCK)
					ON
						(SCWA.WorkAccountID = SCWAA.WorkAccountId)
				WHERE
					(SCWAA.CommissionRateScaleId IS NOT NULL) -- This checks to see that the rep has a quailified account being paid on for.
					AND (SCWA.SalesRepId = @SalesRepId)
				ORDER BY
					SCWA.WorkAccountID ASC) AS RRL
			WHERE
				(RRL.AccountID = @AccountId)
				AND (RRL.AccountID NOT IN (SELECT AccountId FROM [dbo].[SC_WorkAccountRecruitingBonuses] WHERE AccountId = @AccountId)))
		BEGIN
			/** Pay the RecSalesRepId for the first account. */
			INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
				[WorkAccountId]
				, [CommissionPeriodId]
				, [CommissionBonusId]
				, [AdjustmentAmount])
				SELECT 
					@WorkAccountId -- bigint
					, @CommissionPeriodId -- bigint
					, SCCA.CommissionBonusID -- varchar(20)
					, SCCA.BonusAmount -- money
				FROM
					[dbo].[SC_CommissionBonus] AS SCCA WITH (NOLOCK)
				WHERE
					(SCCA.CommissionBonusID = 'RECSIGNBONUSFIRST');
			SET @WorkAccountAdjustmentID = SCOPE_IDENTITY();

			INSERT INTO [dbo].[SC_WorkAccountRecruitingBonuses] (
				[WorkAccountAdjustmentId]
				, [AccountId]
				, [SalesRepId]
				, [PaidToSalesRepId]
			) VALUES (
				@WorkAccountAdjustmentID -- bigint
				, @AccountId -- bigint
				, @SalesRepId
				, @PaidToSalesRepId -- varchar(25)
			);
		END

		/** Check to see if the RECSIGNBONUSSECN has not been paid out for this SalesRepId */
		IF (EXISTS(
			SELECT 
				*
			FROM
				[dbo].[SC_WorkAccountRecruitingBonuses] AS SCWARB WITH (NOLOCK)
				INNER JOIN [dbo].[SC_WorkAccountAdjustments] AS SCWAA WITH (NOLOCK)
				ON
					(SCWAA.WorkAccountAdjustmentID = SCWARB.WorkAccountAdjustmentId)
			WHERE
				(SCWARB.SalesRepId = @SalesRepID) 
				AND (SCWAA.CommissionBonusId = 'RECSIGNBONUSSECN')))
			BEGIN
				PRINT 'This Recruiting Bonus has already been paid from SalesRepID: ' + @SalesRepId + ' to @PaidToSalesRepId: ' + @PaidToSalesRepId;
				RETURN;
			END

		/** Now check to see if there is a third account. */
		IF (EXISTS(SELECT 
				*
			FROM
				(SELECT DISTINCT
					SCWA.SalesRepId
					, SCWA.WorkAccountID
					, SCWA.AccountID
					, ROW_NUMBER() OVER (PARTITION BY SCWA.SalesRepId ORDER BY SCWA.InstallDate) AS [AcctOrdNumber]
				FROM
					[dbo].SC_WorkAccountsAll AS SCWA WITH (NOLOCK)
				WHERE
					(SCWA.SalesRepId = @SalesRepId)) AS COMACCT
			WHERE
				(COMACCT.AcctOrdNumber = 3)
				AND (COMACCT.AccountID = @AccountId))) -- Want to make sure that this is the third account.
		BEGIN
			/** Pay the RecSalesRepId for the first account. */
			INSERT INTO [dbo].[SC_WorkAccountAdjustments] (
				[WorkAccountId]
				, [CommissionPeriodId]
				, [CommissionBonusId]
				, [AdjustmentAmount])
				SELECT 
					@WorkAccountId -- bigint
					, @CommissionPeriodId -- bigint
					, SCCA.CommissionBonusID -- varchar(20)
					, SCCA.BonusAmount -- money
				FROM
					[dbo].[SC_CommissionBonus] AS SCCA WITH (NOLOCK)
				WHERE
					(SCCA.CommissionBonusID = 'RECSIGNBONUSSECN');
			SET @WorkAccountAdjustmentID = SCOPE_IDENTITY();

			INSERT INTO [dbo].[SC_WorkAccountRecruitingBonuses] (
				[WorkAccountAdjustmentId]
				, [AccountId]
				, [SalesRepId]
				, [PaidToSalesRepId]
			) VALUES (
				@WorkAccountAdjustmentID -- bigint
				, @AccountId -- bigint
				, @SalesRepId
				, @PaidToSalesRepId -- varchar(25)
			);
		END
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.SCv2_0_SC_WorkAccountRecruitingBonusReconciliation TO PUBLIC
GO

/** TEST 
DECLARE @WorkAccountID BIGINT = 14
	, @CommissionPeriodId BIGINT = 1
	, @AccountId BIGINT = 191207
	, @SalesRepId VARCHAR(25) = 'SHERJ001'
	, @PaidToSalesRepId VARCHAR(25) = 'VAZQA001'; -- Recruiter
	
--EXEC dbo.SCv2_0_SC_WorkAccountRecruitingBonusReconciliation @WorkAccountID
--	, @CommissionPeriodId
--	, @AccountId
--	, @SalesRepId
--	, @PaidToSalesRepId;

SELECT 
				*
			FROM
				(SELECT DISTINCT
					SCWA.SalesRepId
					, SCWA.WorkAccountID
					, SCWA.AccountID
					, ROW_NUMBER() OVER (PARTITION BY SCWA.SalesRepId ORDER BY SCWA.InstallDate) AS [AcctOrdNumber]
				FROM
					[dbo].SC_WorkAccountsAll AS SCWA WITH (NOLOCK)
				WHERE
					(SCWA.SalesRepId = @SalesRepId)) AS COMACCT
			WHERE
				(COMACCT.AcctOrdNumber = 3)
				AND (COMACCT.AccountID = @AccountId)
*/