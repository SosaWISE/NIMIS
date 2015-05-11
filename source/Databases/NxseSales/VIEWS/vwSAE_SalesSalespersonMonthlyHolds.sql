USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSAE_SalesSalespersonMonthlyHolds')
	BEGIN
		PRINT 'Dropping VIEW vwSAE_SalesSalespersonMonthlyHolds'
		DROP VIEW dbo.vwSAE_SalesSalespersonMonthlyHolds
	END
GO

PRINT 'Creating VIEW vwSAE_SalesSalespersonMonthlyHolds'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSAE_SalesSalespersonMonthlyHolds.sql
**		Name: vwSAE_SalesSalespersonMonthlyHolds
**		Desc: 
**		Get accounts that are on hold and information on the hold
**
**		This template can be customized:
**              
**		Return values: Accounts on hold along with the hold reason
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Bob McFadden
**		Date: 12/19/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	12/19/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwSAE_SalesSalespersonMonthlyHolds]
AS

SELECT
	RU_Users.UserID AS UserID,
	DATEADD(DAY,-CONVERT(INT,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3)),GETDATE()) AS ContractDate,
		-- RANDOMIZE THE ContractDate by subtracting the right 3 digits from the CustomerMasterFileID from today's date

	DATEPART(MONTH,DATEADD(DAY,-CONVERT(INT,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3)),GETDATE())) AS SalesMonth,
		-- RANDOMIZE THE ContractDate by subtracting the right 3 digits from the CustomerMasterFileID from today's date

	DATEPART(YEAR,DATEADD(DAY,-CONVERT(INT,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3)),GETDATE())) AS SalesYear,
		-- RANDOMIZE THE ContractDate by subtracting the right 3 digits from the CustomerMasterFileID from today's date

	AE_CustomerMasterFiles.CustomerMasterFileID AS CustomerMasterFileID,
	MS_ACCOUNTS.AccountID AS AccountID,
	QL_Leads.FirstName AS CustomerFirstName,
	ISNULL(QL_Leads.MiddleName,'') AS CustomerMiddleName,
	QL_Leads.LastName AS CustomerLastName,
	MS_AccountHoldCatg2.CatgName AS HoldName,
	MS_AccountHoldCatg2.CatgDescription AS HoldDescription,
	CONVERT(decimal,0.00) AS HoldAmt
FROM
		-- RU_USERS
		dbo.RU_Users WITH(NOLOCK)

		-- QL_LEADS
		JOIN WISE_CRM.dbo.QL_Leads WITH(NOLOCK)
			ON RU_Users.GPEmployeeId = QL_Leads.SalesRepId
			-- AND QL_Leads.IsActive = 'TRUE'
			AND QL_Leads.IsDeleted = 'FALSE'

		-- CUSTOMER MASTER FILE
		JOIN WISE_CRM.dbo.AE_CustomerMasterFiles WITH(NOLOCK)
			ON QL_Leads.CustomerMasterFileId = AE_CustomerMasterFiles.CustomerMasterFileID
			-- AND AE_CustomerMasterFiles.IsActive = 'TRUE'
			AND AE_CustomerMasterFiles.IsDeleted = 'FALSE'

		-- AE_CUSTOMERS
		JOIN WISE_CRM.dbo.AE_Customers WITH(NOLOCK)
			ON AE_CustomerMasterFiles.CustomerMasterFileID = AE_Customers.CustomerMasterFileId
			-- AND AE_Customers.IsActive = 'TRUE'
			AND AE_Customers.IsDeleted = 'FALSE'

		---- MS_ACCOUNTS
		JOIN WISE_CRM.dbo.MS_Accounts WITH(NOLOCK)
			ON AE_Customers.CustomerID = MS_Accounts.AccountID
			-- AND MS_Accounts.IsActive = 'TRUE'
			AND MS_Accounts.IsDeleted = 'FALSE'

		-- MS_AccountHolds
		JOIN WISE_CRM.dbo.MS_AccountHolds WITH(NOLOCK)
			ON MS_Accounts.AccountID = MS_AccountHolds.AccountID
			AND MS_AccountHolds.IsActive = 'TRUE'

		JOIN WISE_CRM.dbo.MS_AccountHoldCatg2 WITH(NOLOCK)
			ON MS_AccountHolds.Catg2Id = MS_AccountHoldCatg2.Catg2ID

	WHERE RU_Users.IsDeleted = 'FALSE'

GO
/* TEST */
-- SELECT * FROM vwSAE_SalesSalespersonMonthlyHolds
