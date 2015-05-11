USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_SalesSalespersonMonthlyBonus')
	BEGIN
		PRINT 'Dropping Procedure custSAE_SalesSalespersonMonthlyBonus'
		DROP  Procedure  dbo.custSAE_SalesSalespersonMonthlyBonus
	END
GO

PRINT 'Creating Procedure custSAE_SalesSalespersonMonthlyBonus'
GO
/******************************************************************************
**		File: custSAE_SalesSalespersonMonthlyBonus.sql
**		Name: custSAE_SalesSalespersonMonthlyBonus
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
**		Date: 12/22/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/22/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_SalesSalespersonMonthlyBonus (@UserID INT, @SalesMonth INT, @SalesYear INT)
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
			AccountID,
			CustomerFirstName,
			CustomerMiddleName,
			CustomerLastName,
			BonusName,
			BonusDescription,
			BonusAmt
		FROM 
			dbo.vwSAE_SalesSalespersonMonthlyBonus WITH(NOLOCK)
		WHERE 
			(UserID = @UserID)
			AND (SalesMonth = @SalesMonth)
			AND (SalesYear = @SalesYear)
		ORDER BY ContractDate DESC
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_SalesSalespersonMonthlyBonus TO PUBLIC
GO

/** EXEC dbo.custSAE_SalesSalespersonMonthlyBonus 101,12,2014 */
