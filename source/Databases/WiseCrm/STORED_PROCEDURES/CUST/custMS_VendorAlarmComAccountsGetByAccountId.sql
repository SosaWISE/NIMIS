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
	
		/** Find DealerId */
		IF (EXISTS(SELECT 
			MSVACA.*
		FROM
			[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
			INNER JOIN [dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
			ON
				(MSIA.IndustryAccountID = MSA.IndustryAccountId)
			INNER JOIN [dbo].[MS_ReceiverLineBlockVendorAlarmComAccountsMap] AS MSMAP WITH (NOLOCK)
			ON
				(MSIA.ReceiverLineBlockId = MSMAP.ReceiverLineBlockId)
			INNER JOIN [dbo].[MS_VendorAlarmComAccounts] AS MSVACA WITH (NOLOCK)
			ON
				(MSVACA.AlarmComAccountID = MSMAP.AlarmComAccountId)
		WHERE
			(MSA.AccountID = @AccountId)))
		BEGIN
			SELECT 
				@AlarmComAccountID = MSVACA.AlarmComAccountID
			FROM
				[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
				INNER JOIN [dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
				ON
					(MSIA.IndustryAccountID = MSA.IndustryAccountId)
				INNER JOIN [dbo].[MS_ReceiverLineBlockVendorAlarmComAccountsMap] AS MSMAP WITH (NOLOCK)
				ON
					(MSIA.ReceiverLineBlockId = MSMAP.ReceiverLineBlockId)
				INNER JOIN [dbo].[MS_VendorAlarmComAccounts] AS MSVACA WITH (NOLOCK)
				ON
					(MSVACA.AlarmComAccountID = MSMAP.AlarmComAccountId)
			WHERE
				(MSA.AccountID = @AccountId)
		END
		ELSE
		BEGIN

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
				(MSA.AccountID = @AccountId);
		END

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

/** EXEC dbo.custMS_VendorAlarmComAccountsGetByAccountId 160927 */