USE [WISE_CRM]
GO

BEGIN TRANSACTION

UPDATE [dbo].[MS_AccountSalesInformations] SET
	TechId = MS.TechId
FROM
	[dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
	INNER JOIN [dbo].[MS_Accounts] AS MS WITH (NOLOCK)
	ON
		(MS.AccountID = MASI.AccountID);


UPDATE [dbo].[MS_AccountSalesInformations] SET
	SalesRepId = QLD.SalesRepId
--SELECT
--	MASI.AccountID
--	, QLD.SalesRepId
FROM
	[dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
	INNER JOIN [dbo].[MS_AccountCustomers] AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountId = MASI.AccountID)
	INNER JOIN [dbo].[QL_Leads] AS QLD WITH (NOLOCK)
	ON
		(QLD.LeadID = MAC.LeadId)

ROLLBACK TRANSACTION