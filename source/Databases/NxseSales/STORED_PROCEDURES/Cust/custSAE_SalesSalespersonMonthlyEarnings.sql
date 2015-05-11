USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_SalesSalespersonMonthlyEarnings')
	BEGIN
		PRINT 'Dropping Procedure custSAE_SalesSalespersonMonthlyEarnings'
		DROP  Procedure  dbo.custSAE_SalesSalespersonMonthlyEarnings
	END
GO

PRINT 'Creating Procedure custSAE_SalesSalespersonMonthlyEarnings'
GO
/******************************************************************************
**		File: custSAE_SalesSalespersonMonthlyEarnings.sql
**		Name: custSAE_SalesSalespersonMonthlyEarnings
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
**		Date: 12/10/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_SalesSalespersonMonthlyEarnings (@UserID INT, @SalesMonth INT, @SalesYear INT)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			UserID, 
			SalesMonth, 
			SalesYear, 
			SalesAmt, 
			RecurringAmt, 
			RecruitingAmt, 
			BonusAmt, 
			DeductionAmt, 
			HoldAmt, 
			TotalCommissionAmt,
			YTDIncentiveBonusAmt
		FROM 
			dbo.vwSAE_SalesSalespersonMonthlyEarnings
		WHERE 
			(UserID = @UserID)
			AND (SalesMonth = @SalesMonth)
			AND (SalesYear = @SalesYear)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_SalesSalespersonMonthlyEarnings TO PUBLIC
GO

/** EXEC dbo.custSAE_SalesSalespersonMonthlyEarnings 101,1,2014 */
