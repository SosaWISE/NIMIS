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
	, [AccountID]
	, [Term]
	, [CloseRate]
	, [SetupFee]
	, [1stMonth]
	, [Over3Months]
	, [PackageSoldId]
	, [SubmitAccountOnline]
	, [InstallDate]
	, [DealerId]
	, [SalesRepId]
	, [SeasonId]
)
SELECT * FROM [WISE_CRM].[dbo].[vwReports_Performance];

/** Step 2 add the ALL DATA */
TRUNCATE TABLE [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData];
INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] (
	[OfficeId]
	, [CustomerMasterFileId]
	, [AccountID]
	, [CustomerId]
	, [LeadId]
	, [SeasonId]
	, [Prefix]
	, [FirstName]
	, [MiddleName]
	, [LastName]
	, [Postfix]
	, [StreetAddress]
	, [StreetAddress2]
	, [City]
	, [StateId]
	, [PostalCode]
	, [County]
	, [Phone]
	, [Term]
	, [CloseRate]
	, [SetupFee]
	, [1stMonth]
	, [Over3Months]
	, [PackageSoldId]
	, [SubmitAccountOnline]
	, [InstallDate]
	, [DealerId]
	, [SalesRepId]
	, [LeadDate]
)
SELECT 
	PERFM.OfficeId
	, AECA.CustomerMasterFileId
	, PERFM.AccountID
	, AECA.CustomerId
	, AECA.LeadId
	, PERFM.SeasonId
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
	, QL.CreatedOn
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
	LEFT OUTER JOIN [WISE_CRM].[dbo].[QL_Leads] AS QL WITH (NOLOCK)
	ON
		(QL.LeadID = AEC.LeadId)

INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] (
	[CustomerMasterFileId]
	,[LeadId]
	,[DealerId]
	,[SalesRepId]
	,[OfficeId]
	,[SeasonId]
	,[IsContact]
	,[IsLead]
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
	--,[Term]
	,[CloseRate]
	--,[SetupFee]
	--,[1stMonth]
	--,[Over3Months]
	--,[PackageSoldId]
	--,[SubmitAccountOnline]
	--,[InstallDate]
	,[LeadDate]
)
SELECT
	QL.CustomerMasterFileId
	, QL.LeadID
	, QL.DealerId
	, QL.SalesRepId
	, QL.TeamLocationId AS OfficeId
	, QL.SeasonId
	, 0 AS IsContact
	, 1 AS IsLead
	, QL.Suffix AS Prefix
	, QL.FirstName
	, QL.MiddleName
	, QL.LastName
	, QL.Salutation AS PostFix
	, ADR.StreetAddress
	, ADR.StreetAddress2
	, ADR.City
	, ADR.StateId
	, ADR.PostalCode
	, ADR.County
	, ADR.Phone
	, 0 AS CloseRate
	, QL.CreatedOn
FROM
	[WISE_CRM].[dbo].[QL_Leads] AS QL
	LEFT OUTER JOIN [WISE_CRM].[dbo].[QL_Address] AS ADR
	ON
		(ADR.AddressID = QL.AddressId)
WHERE
	(LeadID NOT IN (SELECT LeadId FROM [WISE_CRM].[dbo].[AE_Customers])) -- This is a customer already.
--	AND (QL.CreatedOn BETWEEN @StartDate AND @EndDate)
--	AND (@dealerId IS NULL OR (QL.DealerId = @dealerId))
--	AND (QL.CustomerTypeId = 'PRI')
--	AND (@officeId IS NULL OR (QL.TeamLocationId = @officeId))
--	AND (@salesRepID IS NULL OR (QL.SalesRepId = @salesRepID));

/** Add Contacts */
INSERT INTO [WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] (
	[ContactId]
	,[SalesRepId]
	,[OfficeId]
	,[IsContact]
	,[CloseRate]
	,[FirstName]
	,[LastName]
	,[StreetAddress]
	,[StreetAddress2]
	,[City]
	,[StateId]
	,[PostalCode]
	,[LeadDate]
)
SELECT 
	SLCN.ContactID
	, RepCompanyID
	, ISNULL(SLC.TeamLocationId, 0)
	, 1 AS IsContact
	, 0 AS CloseRate
	, ISNULL(SLCN.FirstName, '') AS FirstName
	, ISNULL(SLCN.LastName, '') AS LastName
	, SLCADR.Address AS [StreetAddress]
	, SLCADR.Address2 AS [StreetAddress2]
	, SLCADR.City
	, SLCADR.[State] AS StateID
	, SLCADR.Zip AS PostalCode
	, SLC.CreatedOn AS [LeadDate]
FROM
	[NXSE_SALES].[dbo].[SL_Contacts] AS SLC WITH (NOLOCK)
	INNER JOIN [NXSE_SALES].[dbo].[SL_ContactNotes] AS SLCN WITH (NOLOCK)
	ON
		(SLCN.ContactID = SLC.ID)
	INNER JOIN [NXSE_Sales].[dbo].[SL_ContactAddresses] AS SLCADR
	ON
		(SLCADR.ContactId = SLCN.ContactId)

COMMIT TRANSACTION