USE [WISE_CRM]
--GO
DECLARE @AccountsTable TABLE (AccountID BIGINT);
DECLARE @IndAcctTable TABLE (IndustruyAccountID BIGINT, ReceiverLineBlockID VARCHAR(50));

INSERT INTO @AccountsTable (AccountID)
SELECT
	MSA.AccountID
	--, MSIA.Csid
	--, MSASI.InstallDate
	--, MSA.PremiseAddressId
	--, MSA.IndustryAccountId
	--, MSA.IndustryAccount2Id
	--, MSA.SiteTypeId
	--, MSA.SystemTypeId
	--, MSA.CellularTypeId
	--, MSA.PanelTypeId
	--, MSA.DslSeizureId
	--, MSA.PanelItemId
	--, MSA.CellPackageItemId
	--, MSA.ContractId
	--, MSA.SignalFormatTypeId
	--, MSA.PanelCode
	--, MSA.PanelPhone
	--, MSA.PanelLocation
	--, MSA.TransformerLocation
	--, MSA.Privacy
	--, MSA.AccountPassword
	--, MSA.SimProductBarcodeId
	--, MSA.DispatchMessage
	--, MSA.IsActive
	--, MSA.IsDeleted
	--, MSA.ModifiedOn
	--, MSA.ModifiedBy
	--, MSA.CreatedOn
	--, MSA.CreatedBy
	--, MSA.DEX_ROW_TS
	--, MSA.DEX_ROW_ID
FROM
	dbo.MS_Accounts AS MSA WITH (NOLOCK)
	LEFT OUTER JOIN dbo.MS_AccountSalesInformations AS MSASI WITH (NOLOCK)
	ON
		(MSASI.AccountID = MSA.AccountID)
	--INNER JOIN dbo.MS_IndustryAccounts AS MSIA WITH (NOLOCK)
	--ON
	--	(MSIA.IndustryAccountID = MSA.IndustryAccountId)
WHERE
	(MSA.DEX_ROW_ID < 50000)
	AND (MSA.AccountID <= 150922)
ORDER BY
	MSASI.AccountID;

BEGIN TRANSACTION

/**************************************************
*	CLEAN UP the Industry Accounts records.		  *
***************************************************/
INSERT INTO @IndAcctTable (IndustruyAccountID, ReceiverLineBlockID)
SELECT IndustryAccountID, ReceiverLineBlockId FROM dbo.MS_IndustryAccounts WHERE (AccountId IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM @AccountsTable))));
--SELECT * FROM @IndAcctTable;
DELETE dbo.MS_ReceiverLineBlockAlarmnet WHERE (ReceiverLineBlockId IN (SELECT ReceiverLineID FROM dbo.MS_ReceiverLineBlocks WHERE (ReceiverLineBlockID IN (SELECT ReceiverLineBlockId FROM @IndAcctTable))));
DELETE dbo.MS_ReceiverLineBlockAlarmComHistory WHERE (ReceiverLineBlockId IN (SELECT ReceiverLineID FROM dbo.MS_ReceiverLineBlocks WHERE (ReceiverLineBlockID IN (SELECT ReceiverLineBlockId FROM @IndAcctTable))));
DELETE dbo.MS_ReceiverLineBlockAlarmCom WHERE (ReceiverLineBlockId IN (SELECT ReceiverLineID FROM dbo.MS_ReceiverLineBlocks WHERE (ReceiverLineBlockID IN (SELECT ReceiverLineBlockId FROM @IndAcctTable))));
UPDATE dbo.MS_ReceiverLineBlocks SET AccountId = NULL, IsAssigned = 'FALSE', AssignedDate = NULL WHERE (ReceiverLineBlockID IN (SELECT ReceiverLineBlockId FROM @IndAcctTable));
--SELECT IndustryAccountID, ReceiverLineBlockId FROM dbo.MS_IndustryAccounts WHERE (AccountId IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM @AccountsTable))));
UPDATE dbo.MS_Accounts SET IndustryAccountId = NULL, IndustryAccount2Id = NULL WHERE (AccountID IN (SELECT AccountID FROM @AccountsTable));
DELETE dbo.MS_IndustryAccounts WHERE (AccountId IN (SELECT AccountID FROM dbo.MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM @AccountsTable))));

/**************************************************
*	CLEAN UP Device Events.						  *
***************************************************/

DELETE dbo.MS_Accounts WHERE (AccountID IN (SELECT AccountID FROM @AccountsTable));

ROLLBACK TRANSACTION