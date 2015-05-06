USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSAE_SalesSalespersonMonthlyEarnings')
	BEGIN
		PRINT 'Dropping VIEW vwSAE_SalesSalespersonMonthlyEarnings'
		DROP VIEW dbo.vwSAE_SalesSalespersonMonthlyEarnings
	END
GO

PRINT 'Creating VIEW vwSAE_SalesSalespersonMonthlyEarnings'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSAE_SalesSalespersonMonthlyEarnings.sql
**		Name: vwSAE_SalesSalespersonMonthlyEarnings
**		Desc: 
**		Select Sales Earnings for Salespeople
**              
**		Return values: List of Monthly Sales Earnings
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
**	-----------	---------------	-------------------------------------------
**	12/10/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwSAE_SalesSalespersonMonthlyEarnings]
AS
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
		dbo.SAE_SalesSalespersonMonthlyEarnings WITH(NOLOCK)

GO
/* TEST */
-- SELECT * FROM vwSAE_SalesSalespersonMonthlyEarnings
