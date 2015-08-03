USE [WISE_HumanResource]
GO
/**********************************************************************************************************************
* DESC:  This script generates the necessary data to get the reports up and running.
* DATE: 07/31/2015
**********************************************************************************************************************/
DECLARE @officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME = '1/1/2000'
	, @endDate DATETIME = GETUTCDATE();

/** SETP 1 Update SAE_ReportsPerformance. */
BEGIN TRANSACTION 

TRUNCATE TABLE [WISE_CRM].[dbo].[SAE_ReportsPerformance];
--INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformance] 

INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformance] (
	[OfficeId]
	,[AccountID]
	,[Term]
	,[CloseRate]
	,[SetupFee]
	,[1stMonth]
	,[Over3Months]
	,[PackageSoldId]
	,[SubmitAccountOnline]
	,[InstallDate]
	,[DealerId]
	,[SalesRepId]
)
SELECT * FROM [WISE_CRM].[dbo].[vwReports_Performance];

/** Step 2 add the ALL DATA */
TRUNCATE TABLE [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData];
INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] (
	[OfficeId]
	,[CustomerMasterFileId]
	,[AccountID]
	,[CustomerId]
	,[Prefix]
	,[FirstName]
	,[MiddleName]
	,[LastName]
	,[Postfix]
	,[StreetAddress]
	,[StreetAddress2]
	,[City]
	,[StateId]
	,[PostalCode]
	,[County]
	,[Phone]
	,[Term]
	,[CloseRate]
	,[SetupFee]
	,[1stMonth]
	,[Over3Months]
	,[PackageSoldId]
	,[SubmitAccountOnline]
	,[InstallDate]
	,[DealerId]
	,[SalesRepId]
)
SELECT 
	PERFM.OfficeId
	, AECA.CustomerMasterFileId
	, PERFM.AccountID
	, AECA.CustomerId
	, AEC.Prefix
	, AEC.FirstName
	, AEC.MiddleName
	, AEC.LastName
	, AEC.Postfix
	, ADR.StreetAddress
	, ADR.StreetAddress2
	, ADR.City
	, ADR.StateId
	, ADR.PostalCode
	, ADR.County
	, ADR.Phone
	, PERFM.Term
	, PERFM.CloseRate
	, PERFM.SetupFee
	, PERFM.[1stMonth]
	, PERFM.Over3Months
	, PERFM.PackageSoldId
	, PERFM.SubmitAccountOnline
	, PERFM.InstallDate
	, PERFM.DealerId
	, PERFM.SalesRepId
FROM
	[WISE_CRM].[dbo].[SAE_ReportsPerformance] AS PERFM
	INNER JOIN [WISE_CRM].[dbo].[vwAE_CustomerAccounts] AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = PERFM.AccountID)
		AND (AECA.CustomerTypeId = 'PRI')
	INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
	INNER JOIN [WISE_CRM].[dbo].[MC_Addresses] AS ADR WITH (NOLOCK)
	ON
		(ADR.AddressID = AEC.AddressId)
ROLLBACK TRANSACTION

--WHERE
--	((PERFM.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) OR (PERFM.InstallDate BETWEEN @startDate AND @endDate));