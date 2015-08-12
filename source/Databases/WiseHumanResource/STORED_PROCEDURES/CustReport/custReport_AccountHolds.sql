USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_AccountHolds')
	BEGIN
		PRINT 'Dropping Procedure custReport_AccountHolds'
		DROP  Procedure  dbo.custReport_AccountHolds
	END
GO

PRINT 'Creating Procedure custReport_AccountHolds'
GO
/******************************************************************************
**		File: custReport_AccountHolds.sql
**		Name: custReport_AccountHolds
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
**		Date: 07/22/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/22/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_AccountHolds
(
	 @salesRepId VARCHAR(50) = NULL
	, @dealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN

	SELECT
		AECA.CustomerMasterFileId AS CustomerNumber
		, MAH.AccountHoldID
		, AECA.CustomerId
		, MAH.AccountId
		, AEC.FirstName + ' ' + AEC.LastName AS [CustomerName]
		--, MAH.Catg1ID
		--, MAH.Catg2Id
		, MAH.Catg1
		, MAH.Catg2
		, MAH.HoldDescription
		, MADR.StreetAddress
		--, MAH.FixedNote
		--, MAH.FixedBy
		--, MAH.FixedOn
		--, MAH.IsActive
		--, MAH.CreatedBy
		--, MAH.CreatedOn
		--, MAH.ModifiedBy
		--, MAH.ModifiedOn 
		, MSASI.SalesRepId
	FROM
		[WISE_CRM].[dbo].[vwMS_AccountHolds] AS MAH WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[vwAE_CustomerAccounts] AS AECA WITH (NOLOCK)
		ON
			(AECA.AccountId = MAH.AccountId)
			AND (AECA.CustomerTypeId = 'PRI')
			AND (MAH.FixedOn IS NULL)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
		INNER JOIN [WISE_CRM].[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
		ON
			(MSASI.AccountID = MAH.AccountId)
		INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
			(MSA.AccountID = MSASI.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[MC_Addresses] AS MADR WITH (NOLOCK)
		ON
			(MADR.AddressID = MSA.PremiseAddressId)
	WHERE
		(@salesRepId IS NULL OR MSASI.SalesRepId = @salesRepId)
		AND (MAH.CreatedOn BETWEEN @startDate AND @endDate);
END
GO

GRANT EXEC ON dbo.custReport_AccountHolds TO PUBLIC
GO

/*
EXEC dbo.custReport_AccountHolds 'WAITJ001', NULL, '1/1/2013', '2015-08-01 05:00:00'

SELECT
	MSASI.SalesRepId 
	, COUNT(*) 
FROM
	[WISE_CRM].[dbo].[vwMS_AccountHolds] AS MAH WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].[vwAE_CustomerAccounts] AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = MAH.AccountId)
		AND (AECA.CustomerTypeId = 'PRI')
		AND (MAH.FixedOn IS NULL)
	INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
	INNER JOIN [WISE_CRM].[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = MAH.AccountId)
	INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
	ON
		(MSA.AccountID = MSASI.AccountID)
	INNER JOIN [WISE_CRM].[dbo].[MC_Addresses] AS MADR WITH (NOLOCK)
	ON
		(MADR.AddressID = MSA.PremiseAddressId)
GROUP BY
	MSASI.SalesRepId 
ORDER BY
	COUNT(*) DESC;

*/