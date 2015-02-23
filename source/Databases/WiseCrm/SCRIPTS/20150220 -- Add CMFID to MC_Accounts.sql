USE [WISE_CRM]
GO

BEGIN TRANSACTION

UPDATE [dbo].[MC_Accounts] SET
	CustomerMasterFileId = QL.CustomerMasterFileId
--SELECT
--	MCA.AccountID
--	, AEC.CustomerID
--	, AEC.CustomerMasterFileId
FROM
	[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
	INNER JOIN [dbo].[MS_AccountCustomers] AS MSAC WITH (NOLOCK)
	ON
		(MSAC.AccountId = MCA.AccountID)
	--INNER JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
	--ON
	--	(AEC.CustomerID = MSAC.CustomerId)
	INNER JOIN [dbo].[QL_Leads] AS QL WITH (NOLOCK)
	ON
		(QL.LeadID = MSAC.LeadId)

ROLLBACK TRANSACTION

--BEGIN TRANSACTION

----SELECT
----	*
----FROM 
----	MC_Accounts AS MCA WITH (NOLOCK)
----WHERE
----	(MCA.CustomerMasterFileId IS NULL)
--DECLARE @HolderAccountID BIGINT = 181267;

--UPDATE dbo.MS_IndustryAccounts SET AccountId = @HolderAccountID WHERE IndustryAccountID IN (SELECT IndustryAccountID FROM MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM MC_Accounts WHERE (CustomerMasterFileId IS NULL))));
--DELETE MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM MC_Accounts WHERE (CustomerMasterFileId IS NULL)));
--DELETE dbo.MS_IndustryAccounts WHERE IndustryAccountID IN (SELECT IndustryAccountID FROM MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM MC_Accounts WHERE (CustomerMasterFileId IS NULL))));
--DELETE MC_Accounts WHERE (CustomerMasterFileId IS NULL);

--ROLLBACK TRANSACTION