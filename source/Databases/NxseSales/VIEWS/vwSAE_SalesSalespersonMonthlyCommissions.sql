USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSAE_SalesSalespersonMonthlyCommissions')
	BEGIN
		PRINT 'Dropping VIEW vwSAE_SalesSalespersonMonthlyCommissions'
		DROP VIEW dbo.vwSAE_SalesSalespersonMonthlyCommissions
	END
GO

PRINT 'Creating VIEW vwSAE_SalesSalespersonMonthlyCommissions'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSAE_SalesSalespersonMonthlyCommissions.sql
**		Name: vwSAE_SalesSalespersonMonthlyCommissions
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Bob McFadden
**		Date: 12/01/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/05/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwSAE_SalesSalespersonMonthlyCommissions]
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
		CONVERT(VARCHAR(20),CASE 
			WHEN (AE_CustomerMasterFiles.CustomerMasterFileID % 3) + 1 = 1 THEN 'Excellent'
			WHEN (AE_CustomerMasterFiles.CustomerMasterFileID % 3) + 1 = 2 THEN 'Good'
			ELSE 'Poor'
		END) AS CreditRating,
		CONVERT(MONEY,29.99) AS ActivationFeeAmt,
		(CONVERT(INT,CONVERT(INT,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3))) * 2) / 3 AS ContractLength,
			-- Randomize the ContractLengthy by multiplying the right 3 digits of the CustomerMasterFileID by 2/3
		CONVERT(VARCHAR(50),'Monitoring') AS ServiceType,
		CONVERT(MONEY,49.99) AS MonthlyPaymentAmt,
		CONVERT(VARCHAR(50),(
			SELECT 
				PaymentTypeName 
			FROM 
				WISE_CRM.dbo.AE_PaymentTypes WITH(NOLOCK) 
			WHERE 
				DEX_ROW_ID = (AE_CustomerMasterFiles.CustomerMasterFileID % 5) + 1)
			) AS PaymentMethod,
		CONVERT(MONEY,CONVERT(DECIMAL,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3))*2) AS SalesCommissionAmt,
			-- Randomize the TotalCommission by multiplying the last 3 digits of the CustomerMasterFileID by 2
		CONVERT(MONEY,CONVERT(DECIMAL,RIGHT(CONVERT(VARCHAR,AE_CustomerMasterFiles.CustomerMasterFileID),3))*.2) AS RecurringCommissionAmt,
			-- Randomize the TotalCommission by multiplying the last 3 digits of the CustomerMasterFileID by .2
		CONVERT(BIT,
			CASE
				WHEN RU_USERS.IsActive = 'FALSE' THEN 'FALSE'
				WHEN QL_Leads.IsActive = 'FALSE' THEN 'FALSE'
				WHEN AE_CustomerMasterFiles.IsActive = 'FALSE' THEN 'FALSE'
				WHEN AE_Customers.IsActive = 'FALSE' THEN 'FALSE'
				WHEN MS_Accounts.isActive = 'FALSE' THEN 'FALSE'
				ELSE 'TRUE'
			END) AS isActive
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

	WHERE RU_Users.IsDeleted = 'FALSE'
GO
/* TEST */
-- SELECT * FROM vwSAE_SalesSalespersonMonthlyCommissions
