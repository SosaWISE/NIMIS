USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_SalesSalespersonMonthlyHolds')
	BEGIN
		PRINT 'Dropping Procedure custSAE_SalesSalespersonMonthlyHolds'
		DROP  Procedure  dbo.custSAE_SalesSalespersonMonthlyHolds
	END
GO

PRINT 'Creating Procedure custSAE_SalesSalespersonMonthlyHolds'
GO
/******************************************************************************
**		File: custSAE_SalesSalespersonMonthlyHolds.sql
**		Name: custSAE_SalesSalespersonMonthlyHolds
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
CREATE Procedure dbo.custSAE_SalesSalespersonMonthlyHolds (@UserID INT, @SalesMonth INT, @SalesYear INT)
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
			HoldName,
			HoldDescription,
			HoldAmt
		FROM 
			dbo.vwSAE_SalesSalespersonMonthlyHolds WITH(NOLOCK)
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

GRANT EXEC ON dbo.custSAE_SalesSalespersonMonthlyHolds TO PUBLIC
GO

/** EXEC dbo.custSAE_SalesSalespersonMonthlyHolds 101,9,2014 */
