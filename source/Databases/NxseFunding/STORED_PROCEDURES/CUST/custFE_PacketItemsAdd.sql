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

PRINT '|-- Account ID: 191114';
EXEC dbo.custFE_PacketItemsAdd 100101, 191114, 'SOSA001';

PRINT '|-- Account ID: 191118';
EXEC dbo.custFE_PacketItemsAdd 100101, 191118, 'SOSA001';

PRINT '|-- Account ID: 191120';
EXEC dbo.custFE_PacketItemsAdd 100101, 191120, 'SOSA001';

PRINT '|-- Account ID: 191121';
EXEC dbo.custFE_PacketItemsAdd 100101, 191121, 'SOSA001';

PRINT '|-- Account ID: 191123';
EXEC dbo.custFE_PacketItemsAdd 100101, 191123, 'SOSA001';

PRINT '|-- Account ID: 191125';
EXEC dbo.custFE_PacketItemsAdd 100101, 191125, 'SOSA001';

PRINT '|-- Account ID: 191126';
EXEC dbo.custFE_PacketItemsAdd 100101, 191126, 'SOSA001';

PRINT '|-- Account ID: 191129';
EXEC dbo.custFE_PacketItemsAdd 100101, 191129, 'SOSA001';

PRINT '|-- Account ID: 191139';
EXEC dbo.custFE_PacketItemsAdd 100101, 191139, 'SOSA001';

PRINT '|-- Account ID: 191143';
EXEC dbo.custFE_PacketItemsAdd 100101, 191143, 'SOSA001';

PRINT '|-- Account ID: 191144';
EXEC dbo.custFE_PacketItemsAdd 100101, 191144, 'SOSA001';

PRINT '|-- Account ID: 191151';
EXEC dbo.custFE_PacketItemsAdd 100101, 191151, 'SOSA001';

PRINT '|-- Account ID: 191152';
EXEC dbo.custFE_PacketItemsAdd 100101, 191152, 'SOSA001';

PRINT '|-- Account ID: 191162';
EXEC dbo.custFE_PacketItemsAdd 100101, 191162, 'SOSA001';

PRINT '|-- Account ID: 191163';
EXEC dbo.custFE_PacketItemsAdd 100101, 191163, 'SOSA001';

PRINT '|-- Account ID: 191165';
EXEC dbo.custFE_PacketItemsAdd 100101, 191165, 'SOSA001';

PRINT '|-- Account ID: 191166';
EXEC dbo.custFE_PacketItemsAdd 100101, 191166, 'SOSA001';

PRINT '|-- Account ID: 191169';
EXEC dbo.custFE_PacketItemsAdd 100101, 191169, 'SOSA001';

PRINT '|-- Account ID: 191171';
EXEC dbo.custFE_PacketItemsAdd 100101, 191171, 'SOSA001';

PRINT '|-- Account ID: 191172';
EXEC dbo.custFE_PacketItemsAdd 100101, 191172, 'SOSA001';

PRINT '|-- Account ID: 191173';
EXEC dbo.custFE_PacketItemsAdd 100101, 191173, 'SOSA001';

PRINT '|-- Account ID: 191178';
EXEC dbo.custFE_PacketItemsAdd 100101, 191178, 'SOSA001';

PRINT '|-- Account ID: 191174';
EXEC dbo.custFE_PacketItemsAdd 100101, 191174, 'SOSA001';

PRINT '|-- Account ID: 191176';
EXEC dbo.custFE_PacketItemsAdd 100101, 191176, 'SOSA001';

PRINT '|-- Account ID: 191179';
EXEC dbo.custFE_PacketItemsAdd 100101, 191179, 'SOSA001';

PRINT '|-- Account ID: 191182';
EXEC dbo.custFE_PacketItemsAdd 100101, 191182, 'SOSA001';

*/
