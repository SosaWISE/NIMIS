USE [WISE_CRM]
GO

/** Declarations */
DECLARE @CMFID BIGINT = 3050456;

/** Show only ALARM Types. */
SELECT
	MST.CustomerMasterFileID
	, ACA.AccountId
	, MAC.AccountName
	, MAC.AccountDesc
	, MAS.PanelTypeId
	, MAS.SystemTypeId
	, MAS.CellularTypeId
	, MAS.DslSeizureId
FROM
	[dbo].AE_CustomerMasterFiles AS MST WITH (NOLOCK)
	INNER JOIN [dbo].AE_Customers AS ACM WITH (NOLOCK)
	ON
		(ACM.CustomerMasterFileId = MST.CustomerMasterFileID)
	INNER JOIN [dbo].AE_CustomerAccounts AS ACA WITH (NOLOCK)
	ON
		(ACA.CustomerId = ACM.CustomerID)
	INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountID = ACA.AccountId)
	INNER JOIN [dbo].MS_Accounts AS MAS WITH (NOLOCK)
	ON
		(MAS.AccountID = MAC.AccountID)
WHERE
	(MST.CustomerMasterFileID = @CMFID)
	AND (MAC.AccountTypeId = 'ALRM');