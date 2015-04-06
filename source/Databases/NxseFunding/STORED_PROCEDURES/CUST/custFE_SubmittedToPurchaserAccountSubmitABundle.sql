USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_SubmittedToPurchaserAccountSubmitABundle')
	BEGIN
		PRINT 'Dropping Procedure custFE_SubmittedToPurchaserAccountSubmitABundle'
		DROP  Procedure  dbo.custFE_SubmittedToPurchaserAccountSubmitABundle
	END
GO

PRINT 'Creating Procedure custFE_SubmittedToPurchaserAccountSubmitABundle'
GO
/******************************************************************************
**		File: custFE_SubmittedToPurchaserAccountSubmitABundle.sql
**		Name: custFE_SubmittedToPurchaserAccountSubmitABundle
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
**		Date: 03/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/30/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_SubmittedToPurchaserAccountSubmitABundle
(
	@BundleId INT
	, @CreatedBy VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountID BIGINT = NULL
		, @PacketId INT
		, @PacketItemId BIGINT
		, @AccountFundingStatusID BIGINT
		, @SubmittedToPurchaserId BIGINT;
	DECLARE @SubmitToPurchaserTable TABLE (SubmittedToPurchaserId BIGINT NOT NULL);
	
	BEGIN TRY
		BEGIN TRANSACTION
		DECLARE	bundleItem_cursor CURSOR FOR
			SELECT PacketId FROM [dbo].[FE_BundleItems] WHERE (BundleId = @BundleId) AND (IsDeleted = 0);

		OPEN bundleItem_cursor;

		FETCH NEXT FROM bundleItem_cursor
		INTO @PacketId;

		/** Loop through */
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			/** DECLARETIONS */
			DECLARE packetItem_cursor CURSOR FOR
				SELECT PacketItemID, AccountId FROM [dbo].[FE_PacketItems] WHERE (PacketId = @PacketId) AND (IsDeleted = 0);
			
			OPEN packetItem_cursor;

			FETCH NEXT FROM packetItem_cursor
			INTO @PacketItemId, @AccountID;

			WHILE(@@FETCH_STATUS = 0)
			BEGIN
				/** Create an Account Status entry */
				INSERT INTO [dbo].[FE_AccountFundingStatus] (
					[AccountFundingStatusTypeId]
					, [AccountId]
					, [AccountStatusEventId]
					, [AccountStatusNote]
					, [CreatedBy]
				) VALUES (
					5  -- AccountFundingStatusTypeId - Submitted to Purchaser
					, @AccountId  -- AccountId - int
					, NULL  -- AccountStatusEventId - bigint
					, NULL  -- AccountStatusNote - ntext
					, @CreatedBy  -- CreatedBy - nvarchar(50)
				);
				SET @AccountFundingStatusID = SCOPE_IDENTITY();

				/** Update MS_AccountSalesInformations */
				UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET
					AccountFundingStatusId = @AccountFundingStatusID
				WHERE
					(AccountID = @AccountId);

				/** Submit to purchaser */
				INSERT INTO [dbo].[FE_SubmittedToPurchaserAccounts] (
					[PacketItemId]
					, [AccountFundingStatusId]
					, [CreatedBy]
		        ) VALUES (
					@PacketItemId -- PacketItemId - bigint
					, @AccountFundingStatusID  -- AccountFundingStatusId - bigint
					, @CreatedBy  -- CreatedBy - nvarchar(50)
		        );
				SET @SubmittedToPurchaserId = SCOPE_IDENTITY();

				/** Insert into temp table */
				INSERT INTO @SubmitToPurchaserTable (SubmittedToPurchaserId) VALUES  (@SubmittedToPurchaserId);

				/** Next row */				
				FETCH NEXT FROM packetItem_cursor
				INTO @PacketItemId, @AccountID;
			END

			CLOSE packetItem_cursor;
			DEALLOCATE packetItem_cursor;
			
			/** Get next*/
			FETCH NEXT FROM bundleItem_cursor
			INTO @PacketId;

			/** Flag this bundle as submitted to purchaser */
			UPDATE [dbo].[FE_Bundles] SET 
				SubmittedBy = @CreatedBy
				, SubmittedOn = GETUTCDATE()
			WHERE
				(BundleID = @BundleId);
		END

		CLOSE bundleItem_cursor;
		DEALLOCATE bundleItem_cursor;


		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT 
		FES.*
	FROM
		[dbo].[FE_SubmittedToPurchaserAccounts] AS FES WITH (NOLOCK)
		INNER JOIN @SubmitToPurchaserTable AS STP 
		ON
			(STP.SubmittedToPurchaserId = FES.SubmittedToPurchaserId);
END
GO

GRANT EXEC ON dbo.custFE_SubmittedToPurchaserAccountSubmitABundle TO PUBLIC
GO

/** 
EXEC dbo.custFE_SubmittedToPurchaserAccountSubmitABundle 500101, 'SOSA001';
*/