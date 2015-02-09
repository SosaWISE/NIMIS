USE [WISE_CRM]
GO

BEGIN TRANSACTION

DECLARE @AccountID BIGINT = 181257
		, @ReceiverLineID VARCHAR(30) = 'MI_MASTER:76824:D'
		, @CSID VARCHAR(11) = ''
		, @SubscriberNumber VARCHAR(6) = '7000'
		, @ReceiverLineBlockId VARCHAR(50) = 'MI_MASTER:76824:D::768247000'
		, @IndustryAccountId BIGINT;

INSERT INTO [dbo].[MS_ReceiverLineBlocks] (
	[ReceiverLineBlockID]
	, [ReceiverLineId]
	, [CSID]
	, [SubscriberNumber]
	, [AccountId]
	, [IsAssigned]
	, [AssignedDate]
) VALUES (
	@ReceiverLineBlockID -- ReceiverLineBlockID - varchar(50)
	, @ReceiverLineId -- ReceiverLineId - varchar(20)
	, @CSID -- CSID - varchar(11)
	, @SubscriberNumber -- SubscriberNumber - varchar(6)
	, @AccountID -- AccountId - bigint
	, 1 -- IsAssigned - bit
	, GETUTCDATE() -- AssignedDate - datetime
);


INSERT INTO [dbo].[MS_IndustryAccounts] (
	[AccountId]
	, [ReceiverLineId]
	, [ReceiverLineBlockId]
	, [SubscriberId]
	, [Csid]
	, [InTestMode]
	, [IsMove]
	, [IsTakeover]
) VALUES (
	@AccountID -- AccountId - bigint
	, @ReceiverLineId -- ReceiverLineId - varchar(20)
	, @ReceiverLineBlockId -- ReceiverLineBlockId - varchar(50)
	, @SubscriberNumber -- SubscriberId - varchar(6)
	, @CSID -- Csid - varchar(15)
	, 0 -- InTestMode - bit
	, 0 -- IsMove - bit
	, 0 -- IsTakeover - bit
);

/** Get IndustryAccountID */
SET @IndustryAccountId = SCOPE_IDENTITY();

UPDATE [dbo].[MS_Accounts] SET IndustryAccountId = @IndustryAccountId WHERE (AccountID = @AccountID);

SELECT * FROM MS_Accounts WHERE AccountID = 181257;

ROLLBACK TRANSACTION