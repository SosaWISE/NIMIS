﻿/***************************************************************
***  FILL IN SALES REP ON MS_ACCOUNTSALESINFORMATIONS TABLE  ***
****************************************************************/
BEGIN TRANSACTION
UPDATE MS_AccountSalesInformations
SET MS_AccountSalesInformations.SalesRepId = QL_Leads.SalesRepId
FROM
	WISE_CRM.dbo.MC_Accounts
	JOIN WISE_CRM.dbo.AE_CustomerAccounts
		ON MC_Accounts.AccountID = AE_CustomerAccounts.AccountId
	JOIN WISE_CRM.dbo.MS_Accounts 
		ON AE_CustomerAccounts.AccountId = MS_Accounts.AccountID
		AND MS_Accounts.IsDeleted = 'FALSE'
	JOIN WISE_CRM.dbo.MS_AccountSalesInformations
		ON MS_Accounts.AccountID = MS_AccountSalesInformations.AccountID
	JOIN QL_Leads
		ON AE_CustomerAccounts.LeadId = QL_Leads.LeadID
WHERE MS_AccountSalesInformations.InstallDate IS NOT NULL
AND MS_AccountSalesInformations.SalesRepId IS NULL
ROLLBACK

select  *
from MS_AccountSalesInformations
where InstallDate is not null
and SalesRepId is null
