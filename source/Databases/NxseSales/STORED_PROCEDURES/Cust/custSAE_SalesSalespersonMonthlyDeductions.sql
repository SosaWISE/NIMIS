USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_SalesSalespersonMonthlyDeductions')
	BEGIN
		PRINT 'Dropping Procedure custSAE_SalesSalespersonMonthlyDeductions'
		DROP  Procedure  dbo.custSAE_SalesSalespersonMonthlyDeductions
	END
GO

PRINT 'Creating Procedure custSAE_SalesSalespersonMonthlyDeductions'
GO
/******************************************************************************
**		File: custSAE_SalesSalespersonMonthlyDeductions.sql
**		Name: custSAE_SalesSalespersonMonthlyDeductions
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
CREATE Procedure dbo.custSAE_SalesSalespersonMonthlyDeductions (@UserID INT, @SalesMonth INT, @SalesYear INT)
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
			DeductionName,
			DeductionDescription,
			DeductionAmt
		FROM 
			dbo.vwSAE_SalesSalespersonMonthlyDeductions WITH(NOLOCK)
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

GRANT EXEC ON dbo.custSAE_SalesSalespersonMonthlyDeductions TO PUBLIC
GO

/** EXEC dbo.custSAE_SalesSalespersonMonthlyDeductions 101,12,2014 */
