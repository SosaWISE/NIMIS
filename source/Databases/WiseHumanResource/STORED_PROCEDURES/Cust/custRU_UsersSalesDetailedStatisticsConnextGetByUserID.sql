USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersSalesDetailedStatisticsConnextGetByUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersSalesDetailedStatisticsConnextGetByUserID'
		DROP  Procedure  dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID
	END
GO

PRINT 'Creating Procedure custRU_UsersSalesDetailedStatisticsConnextGetByUserID'
GO
/******************************************************************************
**		File: custRU_UsersSalesDetailedStatisticsConnextGetByUserID.sql
**		Name: custRU_UsersSalesDetailedStatisticsConnextGetByUserID
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
**		Auth: Bob McFadden
**		Date: 11/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/14/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID
(
	@UserID INT,
	@SalesMonth INT,
	@SalesYear INT,
	@OfficeRollup BIT	-- if 1, compute statistics for the offices managed by the UserID
						-- if 0, return either all reps managed by the office manager or statistics for the userid
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @OfficeManager BIT

	BEGIN TRY

	-- For Office Managers return either:
	-- 1) scores for their office
	-- 2) scores for each salesperson in their office

	IF @OfficeRollup = 'TRUE'
		BEGIN
			/**************************
			***  OFFICE STATISTICS  ***
			***************************/
			SELECT
				CONVERT(INT, 0) AS UserID,--detstats.UserID, 
				NULL AS FirstName, --detstats.FirstName, 
				NULL AS LastName, --detstats.LastName, 
				MAX(detstats.SalesMonth) AS SalesMonth, 
				MAX(detstats.SalesYear) AS SalesYear, 
				NULL AS RegionID, --detstats.RegionID, 
				NULL AS RegionName, --detstats.RegionName, 
				NULL AS TeamID, --detstats.TeamID, 
				NULL AS TeamName, --detstats.TeamName, 
				Max(detstats.OfficeID) AS OfficeID, 
				detstats.OfficeName AS OfficeName, 
				SUM(CASE WHEN detstats.HasRecruits = 'TRUE' THEN 1 ELSE 0 END) AS HasRecruits, 
				SUM(detstats.NumberCreditReportsPulled) AS NumberCreditReportsPulled,
				SUM(detstats.NumberCreditsPassed) as NumberCreditsPassed, 
				SUM(detstats.NumberExcellentCreditScores) as NumberExcellentCreditScores,
				SUM(detstats.NumberGoodCreditScores) as NumberGoodCreditScores,
				SUM(detstats.NumberBadCreditScores) as NumberBadCreditScores, 
				SUM(detstats.AverageCreditScore) as AverageCreditScore,
				SUM(detstats.CreditPassPercentage) as CreditPassPercentage,
				SUM(detstats.PassAndInstallPercentage) as PassAndInstallPercentage, 
				SUM(detstats.NumberCancels) as NumberCancels, 
				SUM(detstats.NumberNetSales) as NumberNetSales, 
				SUM(detstats.NumberPresurveys) as NumberPresurveys, 
				SUM(detstats.NumberPostsurveys) as NumberPostsurveys, 
				SUM(detstats.NumberInstallations) as NumberInstallations, 
				SUM(detstats.NumberSameDayInstallations) as NumberSameDayInstallations, 
				SUM(detstats.SameDayInstallationPercentage) as SameDAyInstallationPercentage, 
				SUM(detstats.NumberActivationsWaived) as NumberActivationsWaived, 
				SUM(detstats.ActivationsWaivedPercentage) as ActivationsWaivedPercentage, 
				SUM(detstats.NumberCCPayments) as NumberCCPayments, 
				SUM(detstats.NumberACHPayments) as NumberACHPayments, 
				SUM(detstats.NumberInvoicePayments) as NumberInvoicePayments, 
				SUM(detstats.NumberSystemsOver8Points) as NumberSystemsOver8Points, 
				SUM(detstats.NumberFreePointsGivenBySalesRep) as NumberFreePointsGivenBySalesRep, 
				SUM(detstats.NumberFreePointsGivenByTech) as NumberFreePointsGivenByTech
			FROM 
				dbo.vwRU_UsersGetDetailedStatisticsConnext AS detstats WITH(NOLOCK)
				JOIN dbo.RU_SalesOfficeManager as mgr WITH(NOLOCK)
					ON detstats.OfficeID = mgr.OfficeID
					AND mgr.UserID = @UserID
			WHERE
				detstats.SalesYear = @SalesYear
				AND detstats.SalesMonth = @SalesMonth
			GROUP BY OfficeName
		END -- @OfficeRollup = 'TRUE'

		/************************************
		*** OFFICE INDIVIDUAL STATISTICS  ***
		*************************************/
		ELSE
		
		BEGIN
			-- Determine if the UserID is an Office Manager
			SELECT 
			@OfficeManager = (
				SELECT 
					CASE WHEN COUNT(*) = 0 THEN 'FALSE' ELSE 'TRUE' END
				FROM dbo.RU_SalesOfficeManager
				WHERE UserID = @UserID
				)

			-- If UserID is an Office Manager return all salesreps in their offices
			IF @OfficeManager = 'TRUE'
				BEGIN
					/***********************************
					***  OFFICE SALESMAN STATISTICS  ***
					************************************/
					SELECT 
						detstats.UserID, 
						detstats.FirstName, 
						detstats.LastName, 
						detstats.SalesYear, 
						detstats.SalesMonth, 
						detstats.RegionID, 
						detstats.RegionName, 
						detstats.TeamID, 
						detstats.TeamName, 
						detstats.OfficeID, 
						detstats.OfficeName, 
						detstats.HasRecruits, 
						detstats.NumberCreditReportsPulled, 
						detstats.NumberCreditsPassed, 
						detstats.NumberExcellentCreditScores, 
						detstats.NumberGoodCreditScores, 
						detstats.NumberBadCreditScores, 
						detstats.AverageCreditScore, 
						detstats.CreditPassPercentage, 
						detstats.PassAndInstallPercentage, 
						detstats.NumberCancels, 
						detstats.NumberNetSales, 
						detstats.NumberPresurveys, 
						detstats.NumberPostsurveys, 
						detstats.NumberInstallations, 
						detstats.NumberSameDayInstallations, 
						detstats.SameDayInstallationPercentage, 
						detstats.NumberActivationsWaived, 
						detstats.ActivationsWaivedPercentage, 
						detstats.NumberCCPayments, 
						detstats.NumberACHPayments, 
						detstats.NumberInvoicePayments, 
						detstats.NumberSystemsOver8Points, 
						detstats.NumberFreePointsGivenBySalesRep, 
						detstats.NumberFreePointsGivenByTech
					FROM 
						dbo.vwRU_UsersGetDetailedStatisticsConnext AS detstats WITH(NOLOCK)
						JOIN dbo.RU_SalesOfficeManager as mgr WITH(NOLOCK)
							ON detstats.OfficeID = mgr.OfficeID
							AND mgr.UserID = @UserID
					WHERE
						detstats.SalesYear = @SalesYear
						AND detstats.SalesMonth = @SalesMonth
				END -- @OfficeManager = 'TRUE'

				ELSE

			-- If UserID is NOT an Office Manager return sales results for the UserID only
				BEGIN -- @OfficeManager NOT = 'TRUE'
				/***************************************
				***  INDIVIDUAL SALESMAN STATISTICS  ***
				****************************************/
					SELECT 
						detstats.UserID, 
						detstats.FirstName, 
						detstats.LastName, 
						detstats.SalesYear, 
						detstats.SalesMonth, 
						detstats.RegionID, 
						detstats.RegionName, 
						detstats.TeamID, 
						detstats.TeamName, 
						detstats.OfficeID, 
						detstats.OfficeName, 
						detstats.HasRecruits, 
						detstats.NumberCreditReportsPulled, 
						detstats.NumberCreditsPassed, 
						detstats.NumberExcellentCreditScores, 
						detstats.NumberGoodCreditScores, 
						detstats.NumberBadCreditScores, 
						detstats.AverageCreditScore, 
						detstats.CreditPassPercentage, 
						detstats.PassAndInstallPercentage, 
						detstats.NumberCancels, 
						detstats.NumberNetSales, 
						detstats.NumberPresurveys, 
						detstats.NumberPostsurveys, 
						detstats.NumberInstallations, 
						detstats.NumberSameDayInstallations, 
						detstats.SameDayInstallationPercentage, 
						detstats.NumberActivationsWaived, 
						detstats.ActivationsWaivedPercentage, 
						detstats.NumberCCPayments, 
						detstats.NumberACHPayments, 
						detstats.NumberInvoicePayments, 
						detstats.NumberSystemsOver8Points, 
						detstats.NumberFreePointsGivenBySalesRep, 
						detstats.NumberFreePointsGivenByTech
					FROM 
						dbo.vwRU_UsersGetDetailedStatisticsConnext AS detstats WITH(NOLOCK)
					WHERE
						detstats.UserID = @UserID
						AND detstats.SalesYear = @SalesYear
						AND detstats.SalesMonth = @SalesMonth
				END-- @OfficeManager NOT = 'TRUE'
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID TO PUBLIC
GO

/** EXEC dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID 103, 11, 2014, 1

@officemanager = true, @officerollup = true
EXEC dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID 103, 11, 2014, 1

@officemanager = true, @officerollup = false
EXEC dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID 1160, 11, 2014, 1

@officemanager = false, @officerollup = true
EXEC dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID 103, 11, 2014, 0

@officemanager = false, @officerollup = true
EXEC dbo.custRU_UsersSalesDetailedStatisticsConnextGetByUserID 1160, 11, 2014, 0


OfficeName AND ID
Denver = 2
New York = 3
Seattle = 1
*/
