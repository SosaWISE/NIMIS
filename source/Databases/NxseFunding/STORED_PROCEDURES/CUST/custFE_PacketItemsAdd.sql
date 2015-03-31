USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_PacketItemsAdd')
	BEGIN
		PRINT 'Dropping Procedure custFE_PacketItemsAdd'
		DROP  Procedure  dbo.custFE_PacketItemsAdd
	END
GO

PRINT 'Creating Procedure custFE_PacketItemsAdd'
GO
/******************************************************************************
**		File: custFE_PacketItemsAdd.sql
**		Name: custFE_PacketItemsAdd
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
CREATE Procedure dbo.custFE_PacketItemsAdd
(
	@PacketId INT
	, @AccountId INT
	, @CreatedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @PacketItemID INT
		, @AccountFundingStatusID BIGINT;
	
	BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO [dbo].[FE_PacketItems] (
			[PacketId]
			, [AccountId]
			, [ModifiedBy]
			, [CreatedBy]
		) VALUES (
			@PacketId  -- PacketId - int
			, @AccountId  -- AccountId - int
			, @CreatedBy  -- ModifiedBy - nvarchar(50)
			, @CreatedBy -- CreatedBy - nvarchar(50)
		);

		SET @PacketItemID = SCOPE_IDENTITY();

		INSERT INTO [dbo].[FE_AccountFundingStatus] (
			[AccountFundingStatusTypeId]
			, [AccountId]
			, [AccountStatusEventId]
			, [PacketItemId]
			, [AccountStatusNote]
			, [CreatedBy]
		) VALUES (
			3  -- AccountFundingStatusTypeId - int 'Submitted To Funding'
			, @AccountId  -- AccountId - int
			, NULL  -- AccountStatusEventId - bigint
			, @PacketItemID  -- PacketItemId - int
			, NULL  -- AccountStatusNote - ntext
			, @CreatedBy  -- CreatedBy - nvarchar(50)
		);

		SET @AccountFundingStatusID = SCOPE_IDENTITY();
		UPDATE [WISE_CRM].[dbo].[MS_AccountSalesInformations] SET 
			AccountFundingStatusId = @AccountFundingStatusID
		WHERE
			(AccountID = @AccountId);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[FE_PacketItems] WHERE (PacketItemID = @PacketItemID);
END
GO

GRANT EXEC ON dbo.custFE_PacketItemsAdd TO PUBLIC
GO

/** 
EXEC dbo.custFE_PacketItemsAdd 100100, 100293, 'SOSA001';


BEGIN TRANSACTION

DELETE FROM dbo.FE_PacketItems

ROLLBACK TRANSACTION

SELECT * FROM dbo.FE_PacketItems
SELECT * FROM WISE_CRM.dbo.AE_CustomerAccounts ORDER BY CustomerId DESC;

EXEC dbo.custFE_PacketItemsAdd 100100, 100290, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181284, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181283, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181282, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181281, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181280, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181279, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100100, 181278, 'SOSA001';
EXEC dbo.custFE_PacketItemsAdd 100101, 181276, 'SOSA001';
*/
