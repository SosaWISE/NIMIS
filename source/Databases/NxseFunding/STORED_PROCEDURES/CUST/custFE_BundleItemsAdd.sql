USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_BundleItemsAdd')
	BEGIN
		PRINT 'Dropping Procedure custFE_BundleItemsAdd'
		DROP  Procedure  dbo.custFE_BundleItemsAdd
	END
GO

PRINT 'Creating Procedure custFE_BundleItemsAdd'
GO
/******************************************************************************
**		File: custFE_BundleItemsAdd.sql
**		Name: custFE_BundleItemsAdd
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
**		Date: 03/26/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/26/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_BundleItemsAdd
(
	@BundleId INT
	, @PacketId INT
	, @CreatedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @BundleItemID INT;
	
	BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO [dbo].[FE_BundleItems](
			[BundleId]
			, [PacketId]
			, [CreatedBy]
		) VALUES (
			@BundleId -- BundleId - int
			, @PacketId -- PacketId - int
			, @CreatedBy  -- CreatedBy - nvarchar(50)
        );

		SET @BundleItemID = SCOPE_IDENTITY();

		DECLARE @PacketItemID BIGINT
			, @AccountId BIGINT
			, @AccountFundingStatusID BIGINT;
		DECLARE account_cursor CURSOR FOR
				SELECT PacketItemID, AccountId FROM [dbo].[FE_PacketItems] WHERE (PacketId = @PacketId) AND (IsDeleted = 0);

		OPEN account_cursor;

		FETCH NEXT FROM account_cursor
		INTO @PacketItemID, @AccountId;

		/** Loop through */
		WHILE (@@FETCH_STATUS = 0)
		BEGIN

			INSERT INTO [dbo].[FE_AccountFundingStatus] (
				[AccountFundingStatusTypeId]
				, [AccountId]
				, [AccountStatusEventId]
				, [PacketItemId]
				, [AccountStatusNote]
				, [CreatedBy]
			) VALUES (
				1  -- AccountFundingStatusTypeId - int PaperworkBeingPulled
				, @AccountId  -- AccountId - int
				, NULL  -- AccountStatusEventId - bigint
				, @BundleItemID  -- PacketItemId - int
				, NULL  -- AccountStatusNote - ntext
				, @CreatedBy  -- CreatedBy - nvarchar(50)
			);
			SET @AccountFundingStatusID = SCOPE_IDENTITY();

			/** Update MS_AccountSalesInformations */
			UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET
				AccountFundingStatusId = @AccountFundingStatusID
			WHERE
				(AccountID = @AccountId);
			
			-- GET NEXT
			FETCH NEXT FROM account_cursor
			INTO @PacketItemID, @AccountId;
		END

		CLOSE account_cursor;
		DEALLOCATE account_cursor;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[FE_PacketItems] WHERE (PacketItemID = @BundleItemID);
END
GO

GRANT EXEC ON dbo.custFE_BundleItemsAdd TO PUBLIC
GO

/** 
EXEC dbo.custFE_BundleItemsAdd 100100, 100293, 'SOSA001';


BEGIN TRANSACTION

DELETE FROM dbo.FE_PacketItems

ROLLBACK TRANSACTION

SELECT * FROM dbo.FE_PacketItems
SELECT * FROM WISE_CRM.dbo.AE_CustomerAccounts ORDER BY CustomerId DESC;

EXEC dbo.custFE_BundleItemsAdd 100100, 100290, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181284, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181283, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181282, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181281, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181280, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181279, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100100, 181278, 'SOSA001';
EXEC dbo.custFE_BundleItemsAdd 100101, 181276, 'SOSA001';
*/