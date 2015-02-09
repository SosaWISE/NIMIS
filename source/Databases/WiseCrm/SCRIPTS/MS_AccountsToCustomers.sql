USE [WISE_CRM]
GO

SELECT
	ACM.CustomerMasterFileId
	, ACM.CustomerID
	, ACA.AccountId
	, ACM.[Username]
	, ACM.[Password]
	, ACM.CustomerTypeId
	, MSA.SystemTypeId
	, MSA.PanelTypeId
	, MSA.IndustryAccountId
	, IAN.IndustryNumber
	, IAN.Designator
	, IAN.SubscriberNumber
FROM
	dbo.MS_Accounts AS MSA WITH (NOLOCK)
	INNER JOIN dbo.MC_Accounts AS MCA WITH (NOLOCK)
	ON
		(MSA.AccountID = MCA.AccountID)
	INNER JOIN dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK)
	ON
		(MCA.AccountID = ACA.AccountId)
	INNER JOIN dbo.AE_Customers AS ACM WITH (NOLOCK)
	ON
		(ACA.CustomerId = ACM.CustomerID)
	INNER JOIN dbo.AE_CustomerMasterFiles AS ACMF WITH (NOLOCK)
	ON
		(ACM.CustomerMasterFileId = ACMF.CustomerMasterFileID)
	INNER JOIN [dbo].[vwMS_IndustryAccountNumbers] AS IAN
	ON
		(MSA.IndustryAccountId = IAN.IndustryAccountID)		
		

BEGIN TRANSACTION 

/** Tie the AE_Customers to the MC and MS Accounts table. 
INSERT dbo.AE_CustomerAccounts (CustomerId, AccountId) VALUES (100255,/**CustomerId - bigint*/ 100164 /**AccountId - bigint*/)
INSERT dbo.AE_CustomerAccounts (CustomerId, AccountId) VALUES (100256,/**CustomerId - bigint*/ 100165 /**AccountId - bigint*/)
		*/
		
/** Flag the AE_Customers as GPS Clients. 
* This will allow the customer to be able to login to GPS Client.
*/
INSERT dbo.AE_CustomerMasterToCustomer ( CustomerMasterFileId, CustomerId, CustomerTypeId) VALUES ( 3000067 /**CustomerMasterFileId - bigint*/, 100255 /**CustomerId - bigint*/, 'GPSCLNT' /**CustomerTypeId - varchar(20)*/)
INSERT dbo.AE_CustomerMasterToCustomer ( CustomerMasterFileId, CustomerId, CustomerTypeId) VALUES ( 3000067 /**CustomerMasterFileId - bigint*/, 100256 /**CustomerId - bigint*/, 'GPSCLNT' /**CustomerTypeId - varchar(20)*/)

SELECT
	ACM.CustomerMasterFileId
	, ACM.CustomerID
	, ACA.AccountId
	, ACM.Username
	, ACM.Password
	, ACM.CustomerTypeId
FROM
	dbo.AE_Customers AS ACM WITH (NOLOCK)
	INNER JOIN dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK)
	ON
		(ACM.CustomerID = ACA.CustomerId)
	INNER JOIN dbo.vwAE_CustomerGpsClients AS VCG
	ON
		(ACA.CustomerId = VCG.CustomerID)

ROLLBACK TRANSACTION