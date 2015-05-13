USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_SalesSalespersonMonthlyCommissions')
	BEGIN
		PRINT 'Dropping Procedure custSAE_SalesSalespersonMonthlyCommissions'
		DROP  Procedure  dbo.custSAE_SalesSalespersonMonthlyCommissions
	END
GO

PRINT 'Creating Procedure custSAE_SalesSalespersonMonthlyCommissions'
GO
/******************************************************************************
**		File: custSAE_SalesSalespersonMonthlyCommissions.sql
**		Name: custSAE_SalesSalespersonMonthlyCommissions
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
**		Date: 12/12/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/12/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_SalesSalespersonMonthlyCommissions (@UserID INT, @SalesMonth INT, @SalesYear INT)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT
			UserID, 
			ContractDate, 
			SalesMonth, 
			SalesYear, 
			CustomerMasterFileID, 
			CustomerFirstName, 
			CustomerMiddleName, 
			CustomerLastName, 
			CreditRating, 
			ActivationFeeAmt, 
			ContractLength, 
			ServiceType, 
			MonthlyPaymentAmt, 
			PaymentMethod, 
			SalesCommissionAmt,
			RecurringCommissionAmt,
			isActive
		FROM 
			dbo.vwSAE_SalesSalespersonMonthlyCommissions WITH(NOLOCK)
		WHERE 
			(isActive = 'TRUE')
			AND (UserID = @UserID)
			AND (SalesMonth = @SalesMonth)
			AND (SalesYear = @SalesYear)
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_SalesSalespersonMonthlyCommissions TO PUBLIC
GO

/** EXEC dbo.custSAE_SalesSalespersonMonthlyCommissions 101,9,2014 */
