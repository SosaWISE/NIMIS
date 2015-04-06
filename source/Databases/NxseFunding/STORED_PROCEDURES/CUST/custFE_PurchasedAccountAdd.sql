USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_PurchasedAccountAdd')
	BEGIN
		PRINT 'Dropping Procedure custFE_PurchasedAccountAdd'
		DROP  Procedure  dbo.custFE_PurchasedAccountAdd
	END
GO

PRINT 'Creating Procedure custFE_PurchasedAccountAdd'
GO
/******************************************************************************
**		File: custFE_PurchasedAccountAdd.sql
**		Name: custFE_PurchasedAccountAdd
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
CREATE Procedure dbo.custFE_PurchasedAccountAdd
(
	@PurchaseContractID INT
	, @CMFID BIGINT
	, @CSID VARCHAR(20)
	, @CreatedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountID BIGINT = NULL
		, @AccountFundingStatusID BIGINT
		, @PurchasedAccountID BIGINT;

	SELECT @AccountID = AccountID FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = @CMFID) AND (Csid = @CSID);
	PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CMFID AS VARCHAR(20)) + ' | CSID: ' + @CSID;
	
	BEGIN TRY
		BEGIN TRANSACTION
		/** Check that there is an accountId. */
		IF (@AccountID IS NULL)
		BEGIN
			DECLARE @StrMsg NVARCHAR(200) = N'The Customer #: ''' + CAST(@CMFID AS VARCHAR(20)) + ''' and Csid: ''' + @CSID + ''' did not find a match.';
			RAISERROR (@StrMsg, 18, 1);			
		END

		INSERT INTO [dbo].[FE_AccountFundingStatus] (
			[AccountFundingStatusTypeId]
			, [AccountId]
			, [AccountStatusEventId]
			, [AccountStatusNote]
			, [CreatedBy]
		) VALUES (
			6  -- AccountFundingStatusTypeId - Purchased
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

		/** Add to the purchase list of accounts.*/
		INSERT INTO [dbo].[FE_PurchasedAccount] (
			[AccountId]
			, [PurchaseContractId]
			, [AccountFundingStatusId]
			, [CreatedBy]
		) VALUES (
			@AccountID -- AccountId - bigint
			, @PurchaseContractID -- PurchaseContractId - int
			, @AccountFundingStatusID -- AccountFundingStatusId - bigint
			, @CreatedBy -- CreatedBy - varchar(50)
		);
		SET @PurchasedAccountID = SCOPE_IDENTITY();

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[FE_PurchasedAccount] WHERE (PurchasedAccountID = @PurchasedAccountID);
END
GO

GRANT EXEC ON dbo.custFE_PurchasedAccountAdd TO PUBLIC
GO

/** 
EXEC dbo.custFE_PurchasedAccountAdd 1, 100293, '23432', 'SOSA001';
*/