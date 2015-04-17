USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem'
		DROP  Procedure  dbo.custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem
	END
GO

PRINT 'Creating Procedure custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem'
GO
/******************************************************************************
**		File: custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem.sql
**		Name: custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem
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
**		Date: 04/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/17/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem
(
	@AccountEquipmentID BIGINT
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountID BIGINT
		, @InvoiceItemId BIGINT = NULL
		, @AccountEquipmentUpgradeTypeId VARCHAR(10)
		, @EqGpEmployeeId VARCHAR(50);


	BEGIN TRY
		/***********************
		* START TRANSACTION 
		************************/
		BEGIN TRANSACTION

		/** INITIALIZE */
		SELECT
			@AccountID = MSAE.AccountId
			, @InvoiceItemId = MSAE.InvoiceItemId
			, @AccountEquipmentUpgradeTypeId = MSAE.AccountEquipmentUpgradeTypeId
			, @EqGpEmployeeId = MSAE.GPEmployeeId
		FROM
			[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
		WHERE
			(MSAE.AccountEquipmentID = @AccountEquipmentID);

		-- Check that the accountequipment has an invoice item assigned to it.
		IF(@InvoiceItemId IS NULL OR @InvoiceItemId = 0)
		BEGIN
			DECLARE @AccountIdStr VARCHAR(20) = CAST(@AccountID AS VARCHAR)
				, @AEQIdStr VARCHAR(20) = CAST(@AccountEquipmentID AS VARCHAR);
			RAISERROR (N'The Equipment for MsAccountID of "%s" and with AccountEquipmentId of "%s" is missing an InvoiceItemId.'
				, 18
				, 1
				, @AccountIdStr
				, @AEQIdStr);
		END

		/**
		* Pick what upgrade from CUST, SALESREP, TECH
		*/
		IF (@AccountEquipmentUpgradeTypeId = 'CUST')
			UPDATE [dbo].[AE_InvoiceItems] SET
				IsCustomerPaying = 'TRUE'
				, SalesmanId = @GpEmployeeId
				, TechnicianId = NULL
			WHERE
				(InvoiceItemID = @InvoiceItemId);
		ELSE IF (@AccountEquipmentUpgradeTypeId = 'SALESREP')
			UPDATE [dbo].[AE_InvoiceItems] SET
				IsCustomerPaying = 'FALSE'
				, SalesmanId = @GpEmployeeId
				, TechnicianId = NULL
			WHERE
				(InvoiceItemID = @InvoiceItemId);
		ELSE IF (@AccountEquipmentUpgradeTypeId = 'TECH')
			UPDATE [dbo].[AE_InvoiceItems] SET
				IsCustomerPaying = 'FALSE'
				, SalesmanId = NULL
				, TechnicianId = @GpEmployeeId
			WHERE
				(InvoiceItemID = @InvoiceItemId);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[MS_AccountEquipment] WHERE (AccountEquipmentID = @AccountEquipmentID);
	/** DEGUG 
	SELECT * FROM [dbo].[AE_InvoiceItems] WHERE (InvoiceItemID = @InvoiceItemId);*/
END
GO

GRANT EXEC ON dbo.custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem TO PUBLIC
GO

/** EXEC dbo.custMS_AccountEquipmentSyncAssignmetBetweenInvoiceItem 40887, 'SOSAA001' */