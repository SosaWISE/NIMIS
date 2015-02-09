USE [WISE_CRM]
GO


SELECT
	BIS.*
FROM
	[dbo].[SAE_CustomerMasterFileBillingInfoSummary] AS BIS WITH (NOLOCK)
	INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountID = BIS.AccountId)
	INNER JOIN [dbo].MS_Accounts AS MAS WITH (NOLOCK)
	ON
		(MAS.AccountID = MAC.AccountID)
UNION
SELECT
	BIS.*
FROM
	[dbo].[SAE_CustomerMasterFileBillingInfoSummary] AS BIS WITH (NOLOCK)
	INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountID = BIS.AccountId)
	INNER JOIN [dbo].IS_Accounts AS ISA WITH (NOLOCK)
	ON
		(ISA.AccountID = MAC.AccountID)
UNION 
SELECT
	BIS.*
FROM
	[dbo].[SAE_CustomerMasterFileBillingInfoSummary] AS BIS WITH (NOLOCK)
	INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
	ON
		(MAC.AccountID = BIS.AccountId)
	INNER JOIN [dbo].LL_Accounts AS LAC WITH (NOLOCK)
	ON
		(LAC.AccountID = MAC.AccountID)