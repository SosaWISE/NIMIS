USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_VendorAlarmComAccountsGetByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_VendorAlarmComAccountsGetByAccountId'
		DROP  Procedure  dbo.custMS_VendorAlarmComAccountsGetByAccountId
	END
GO

PRINT 'Creating Procedure custMS_VendorAlarmComAccountsGetByAccountId'
GO
/******************************************************************************
**		File: custMS_VendorAlarmComAccountsGetByAccountId.sql
**		Name: custMS_VendorAlarmComAccountsGetByAccountId
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_VendorAlarmComAccountsGetByAccountId
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AlarmComAccountID INT;
	
	BEGIN TRY
	
		--/** Find DealerId */
		--SELECT 
		--	@DealerId = CMF.DealerId
		--FROM
		--	[dbo].[AE_CustomerAccounts] AS MAC WITH (NOLOCK)
		--	INNER JOIN [dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		--	ON
		--		(CUST.CustomerID = MAC.CustomerId)
		--		AND (MAC.AccountId  = @AccountId)
		--	INNER JOIN [dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
		--	ON
		--		(CMF.CustomerMasterFileID = CUST.CustomerMasterFileId);
	
		--/** Get the Account row. */
		--IF (NOT EXISTS(SELECT * FROM [dbo].[MS_VendorAlarmComAccounts] AS VAC WITH (NOLOCK) WHERE (DealerId = @DealerId)))
		--BEGIN
		--	SET @DealerId = 5000;
		--END

		SELECT
			@AlarmComAccountID = MSVACA.AlarmComAccountID
--			MSIA.*
		FROM
			[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
			INNER JOIN [dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
			ON
				(MSIA.IndustryAccountID = MSA.IndustryAccountId)
			INNER JOIN [dbo].[MS_ReceiverLineVendorAlarmComAccountsMap] AS MSMAP WITH (NOLOCK)
			ON
				(MSMAP.ReceiverLineId = MSIA.ReceiverLineId)
			INNER JOIN [dbo].[MS_VendorAlarmComAccounts] AS MSVACA WITH (NOLOCK)
			ON
				(MSVACA.AlarmComAccountID = MSMAP.AlarmComAccountId)
		WHERE
--			(MSA.AccountID = 211217)
			(MSA.AccountID = @AccountId);

		SELECT * FROM [dbo].[MS_VendorAlarmComAccounts] AS VAC WITH (NOLOCK) WHERE (AlarmComAccountID = @AlarmComAccountID)
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_VendorAlarmComAccountsGetByAccountId TO PUBLIC
GO

/** EXEC dbo.custMS_VendorAlarmComAccountsGetByAccountId 211217 */