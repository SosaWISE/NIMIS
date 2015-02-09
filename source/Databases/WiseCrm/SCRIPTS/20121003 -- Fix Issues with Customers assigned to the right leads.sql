USE [WISE_CRM]
GO

--SELECT 
--	*
--FROM
--	dbo.vwQL_LeadSearchResult
--WHERE
--	FirstName LIKE 'Don%'
	
	
SELECT
	MAS.AccountID
	, MAS.IndustryAccountId
	, ACA.*
	, QLD.DealerId
	, QLD.LeadID
	, CST.LeadId
	, QLD.FirstName
	, QLD.LastName
	, CST.FirstName
	, QLD.LastName
FROM 
	MS_Accounts AS MAS WITH (NOLOCK)
	INNER JOIN dbo.MS_IndustryAccounts AS IND WITH (NOLOCK)
	ON
		(MAS.IndustryAccountId = IND.IndustryAccountID)
	INNER JOIN dbo.MC_Accounts AS MAC WITH (NOLOCK)
	ON
		(MAS.AccountID = MAC.AccountID)
	INNER JOIN dbo.AE_CustomerAccounts AS ACA WITH(NOLOCK)
	ON
		(MAC.AccountID = ACA.AccountId)
	INNER JOIN dbo.AE_Customers AS CST WITH (NOLOCK)
	ON
		(ACA.CustomerId = CST.CustomerID)
	INNER JOIN dbo.AE_CustomerMasterFiles AS ACM WITH (NOLOCK)
	ON
		(CST.CustomerMasterFileId = ACM.CustomerMasterFileID)
	INNER JOIN dbo.QL_Leads AS QLD WITH (NOLOCK)
	ON
		(ACM.CustomerMasterFileID = QLD.CustomerMasterFileId)
WHERE 
	(MAS.GpsWatchPhoneNumber = '13132896563' OR MAS.GpsWatchPhoneNumber = '13133580068')

BEGIN TRANSACTION 

UPDATE QL_Leads SET DealerId = 5016, SalesRepId = 'BRID001' WHERE LeadID = 1000065


ROLLBACK TRANSACTION
