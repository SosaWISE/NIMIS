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
		, @RejectionID BIGINT;

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

	/** Update MS_AccountSalesInformation */
	UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET 
		AccountFundingStatusId = @AccountFundingStatusId
	WHERE (AccountID = @AccountID);

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


	ROLLBACK TRANSACTION