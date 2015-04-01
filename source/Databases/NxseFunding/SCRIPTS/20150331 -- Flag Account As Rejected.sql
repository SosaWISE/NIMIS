USE [NXSE_Funding]
GO


	BEGIN TRANSACTION
	/** Arguments */
	DECLARE @BundleID INT = 500101
		, @CMFID BIGINT = 3081375
		, @CSID VARCHAR(20) = '768260017'
		, @AccountStatusNote NVARCHAR(MAX) = 'Missing 2 Way signals'
		, @RejectionTypeId INT = 2 -- [Other Still Sellable]
		, @CreatedBy VARCHAR(50) = 'SOSAA001';

	/** Declarations */
	DECLARE @AccountID BIGINT
		, @AccountFundingStatusId INT
		, @PacketItemId BIGINT
		, @PurchaserId VARCHAR(10)
		, @RejectionID BIGINT
		, @RejectedAccountID BIGINT;

	SELECT @AccountID = AccountID FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = @CMFID) AND (Csid = @CSID);
	PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CMFID AS VARCHAR(20)) + ' | CSID: ' + @CSID;
	
	/** Get PacketItemId */
	SELECT
		@PacketItemId = PAKI.PacketItemID
		, @PurchaserId = BUN.PurchaserId
	FROM
		[dbo].[FE_BundleItems] AS BND WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Bundles] AS BUN WITH (NOLOCK)
		ON
			(BUN.BundleID = BND.BundleId)
		INNER JOIN [dbo].[FE_Packets] AS PAK WITH (NOLOCK)
		ON
			(PAK.PacketID = BND.PacketId)
		INNER JOIN [dbo].[FE_PacketItems] AS PAKI WITH (NOLOCK)
		ON
			(PAKI.PacketId = PAK.PacketID)
	WHERE
		(PAKI.AccountId = @AccountID)
		AND (BUN.BundleID = @BundleID);
	PRINT '|-- -- AQUIRED PacketItemID: ' + CAST(@PacketItemId AS VARCHAR(20)) + ' | PurchaserId: ' + CAST(@PurchaserId AS VARCHAR(20));

		/** Create the AccountFunding Status */
	INSERT INTO [dbo].[FE_AccountFundingStatus] (
		[AccountFundingStatusTypeId] ,
		[AccountId] ,
		[PacketItemId] ,
		[AccountStatusNote] ,
		[CreatedBy]
	) VALUES (
		7 -- AccountFundingStatusTypeId - int -- Rejected by Purchaser
		, @AccountID  -- AccountId - int
		, @PacketItemId -- PacketItemId - int
		, @AccountStatusNote -- AccountStatusNote - ntext
		, @CreatedBy  -- CreatedBy - nvarchar(50)
	);
	SET @AccountFundingStatusId = SCOPE_IDENTITY();
	PRINT '|-- -- INSERTED AccountFundingStatusId: ' + CAST(@AccountFundingStatusId AS VARCHAR(20));

	/** Update MS_AccountSalesInformation */
	UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET 
		AccountFundingStatusId = @AccountFundingStatusId
	WHERE (AccountID = @AccountID);
	PRINT '|-- -- UPDATED [WISE_CRM].[dbo].[MS_AccountSalesInformations] with AccountFundingStatusId: ' + CAST(@AccountFundingStatusId AS VARCHAR(20));

	/** Add to Rejection tables */
	INSERT INTO [dbo].[FE_Rejection] (
		[AccountId]
		, [RejectionTypeId]
		, [PurchaserId]
		, [AccountFundingStatusId]
		, [PacketItemId]
		, [RejectionDescription]
		, [CreatedBy]
	) VALUES (
		@AccountID -- AccountId - int
		, @RejectionTypeId -- RejectionTypeId - int
		, @PurchaserId -- PurchaserId - varchar(10)
		, @AccountFundingStatusId -- AccountFundingStatusId - bigint
		, @PacketItemId -- PacketItemId - bigint
		, @AccountStatusNote -- RejectionDescription - nvarchar(max)
		, @CreatedBy  -- CreatedBy - nvarchar(50)
	);
	SET @RejectionID = SCOPE_IDENTITY();
	PRINT '|-- -- INSERTED [dbo].[FE_Rejection] new Identity RejectionID: ' + CAST(@RejectionID AS VARCHAR(20));

	INSERT INTO [dbo].[FE_RejectedAccount] (
		[AccountId]
		, [RejectionId]
		, [PacketItemId]
		, [AccountFundingStatusId]
		, [CreatedBy]
	) VALUES (
		@AccountID -- AccountId - bigint
		, @RejectionID  -- bigint
		, @PacketItemId  -- PacketItemId - bigint
		, @AccountFundingStatusId  -- AccountFundingStatusId - bigint
		, @CreatedBy  -- CreatedBy - nvarchar(50)
	);
	SET @RejectedAccountID = SCOPE_IDENTITY();
	PRINT '|-- -- INSERTED [dbo].[FE_RejectedAccount] new Identity RejectedAccountID: ' + CAST(@RejectedAccountID AS VARCHAR(20));

	ROLLBACK TRANSACTION