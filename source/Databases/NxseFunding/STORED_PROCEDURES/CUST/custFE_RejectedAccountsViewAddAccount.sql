USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_RejectedAccountsViewAddAccount')
	BEGIN
		PRINT 'Dropping Procedure custFE_RejectedAccountsViewAddAccount'
		DROP  Procedure  dbo.custFE_RejectedAccountsViewAddAccount
	END
GO

PRINT 'Creating Procedure custFE_RejectedAccountsViewAddAccount'
GO
/******************************************************************************
**		File: custFE_RejectedAccountsViewAddAccount.sql
**		Name: custFE_RejectedAccountsViewAddAccount
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 02/13/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/13/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_RejectedAccountsViewAddAccount
(
	@BundleID INT-- = 500101
	, @CMFID BIGINT-- = 3081375
	, @CSID VARCHAR(20)-- = '768260017'
	, @AccountFundingStatusTypeId INT
	, @RejectionTypeId INT-- = 2 -- [Other Still Sellable]
	, @AccountStatusNote NVARCHAR(MAX)-- = 'Missing 2 Way signals'
	, @CreatedBy VARCHAR(50)-- = 'SOSAA001'
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION
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
			@AccountFundingStatusTypeId -- AccountFundingStatusTypeId - int -- Rejected by Purchaser
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
		INSERT INTO [dbo].[FE_Rejections] (
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
		PRINT '|-- -- INSERTED [dbo].[FE_Rejections] new Identity RejectionID: ' + CAST(@RejectionID AS VARCHAR(20));

		INSERT INTO [dbo].[FE_RejectedAccounts] (
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
		PRINT '|-- -- INSERTED [dbo].[FE_RejectedAccounts] new Identity RejectedAccountID: ' + CAST(@RejectedAccountID AS VARCHAR(20));

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custFE_RejectedAccountsViewAddAccount TO PUBLIC
GO

/** 
	PRINT '|-- Customer #: 3081375 | CSID: 768260017';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3081375, @CSID = '768260017', @AccountFundingStatusTypeId = 7, @RejectionTypeId = 2, @AccountStatusNote = 'Missing 2 Way signals', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091384 | CSID: 768260030';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091384, @CSID = '768260030', @AccountFundingStatusTypeId = 13, @RejectionTypeId = 3, @AccountStatusNote = 'Slammed', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091394 | CSID: 768260033';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091394, @CSID = '768260033', @AccountFundingStatusTypeId = 13, @RejectionTypeId = 3, @AccountStatusNote = 'Slammed', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091398 | CSID: 768260036';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091398, @CSID = '768260036', @AccountFundingStatusTypeId = 7, @RejectionTypeId = 2, @AccountStatusNote = 'EFT PPWK', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091416 | CSID: 768260049';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091416, @CSID = '768260049', @AccountFundingStatusTypeId = 13, @RejectionTypeId = 3, @AccountStatusNote = 'Slammed', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091418 | CSID: 768260050';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091418, @CSID = '768260050', @AccountFundingStatusTypeId = 13, @RejectionTypeId = 3, @AccountStatusNote = 'Slammed', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091420 | CSID: 768260052';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091420, @CSID = '768260052', @AccountFundingStatusTypeId = 7, @RejectionTypeId = 3, @AccountStatusNote = 'Cancel', @CreatedBy = 'SOSAA001';

	PRINT '|-- Customer #: 3091427 | CSID: 768260055';
	EXEC dbo.custFE_RejectedAccountsViewAddAccount @BundleID = 500101, @CMFID = 3091427, @CSID = '768260055', @AccountFundingStatusTypeId = 7, @RejectionTypeId = 2, @AccountStatusNote = 'SOP Missing Signature', @CreatedBy = 'SOSAA001';

*/