USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceMsInstallInfoViewGetByIDs')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceMsInstallInfoViewGetByIDs'
		DROP  Procedure  dbo.custAE_InvoiceMsInstallInfoViewGetByIDs
	END
GO

PRINT 'Creating Procedure custAE_InvoiceMsInstallInfoViewGetByIDs'
GO
/******************************************************************************
**		File: custAE_InvoiceMsInstallInfoViewGetByIDs.sql
**		Name: custAE_InvoiceMsInstallInfoViewGetByIDs
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceMsInstallInfoViewGetByIDs
(
	@InvoiceID BIGINT = NULL
	, @AccountId BIGINT
	, @GpEmployeeID NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS with default values */
	DECLARE @Score INT,
		@ActivationFeeItemId VARCHAR(50) = 'SETUP_FEE_199',
		@ActivationFee MONEY = 199,
		@MonthlyMonitoringRateItemId VARCHAR(50) = 'MON_CONT_5000',
		@MonthlyMonitoringRate MONEY = 39.95,
		@AlarmComPackageItemId VARCHAR(50) = 'CELL_SRV_AC_WSF',
		@AlarmComPackageId VARCHAR(20) = 'WRLFWN',
		@Over3Months BIT = 0,
		@CellularTypeId VARCHAR(20) = 'CELLSEC',
		@PanelTypeId VARCHAR(20) = 'CONCORD',
		@ContractId INT = 1;

	/** Find Credit Score */
	SELECT @Score = Score FROM [dbo].fxQlCreditReportGetByMsAccountID(@AccountId);
	SELECT @ActivationFeeItemId = [dbo].fxInvoiceActivationFeeItemByScore(@Score, 'SETUP_FEE_199');
	SELECT @ActivationFee = Price FROM [dbo].[AE_Items] WHERE (ItemID = @ActivationFeeItemId);
	SELECT @MonthlyMonitoringRateItemId = [dbo].fxInvoiceMonthlyMonitoringRateItemByScore(@Score, 'MON_CONT_5000');
	SELECT @MonthlyMonitoringRate = Price FROM [dbo].[AE_Items] WHERE (ItemID = @MonthlyMonitoringRateItemId);
	SELECT @AlarmComPackageItemId = [dbo].fxInvoiceAlarmComPackageItemByScore(@Score, 'CELL_SRV_AC_WSF');
	SELECT @AlarmComPackageId = ItemSku FROM [dbo].[AE_Items] WHERE (ItemID = @AlarmComPackageItemId);
	SELECT @Over3Months = [dbo].fxInvoiceIsOver3MonthsByScore(@Score, 0);
	SELECT @CellularTypeId = [dbo].fxInvoiceCellularTypeByScore(@Score, 'CELLSEC');
	--SELECT @PanelTypeId = ??
	--SELECT @ContractId = [dbo].fxInvoiceContractTemplateByScore(@Score, 1);

	/** Argument Validation. */
	-- Check that there is an AccountID
	IF (@InvoiceID IS NULL AND @AccountId IS NULL)
	BEGIN
		RAISERROR (N'Arguments InvoiceID and AccountId can not be "NULL" at the same time.', 19, 1, @MonthlyMonitoringRateItemId) ;
		RETURN;
	END
	ELSE IF (@AccountId IS NULL)
	BEGIN
		SELECT @AccountId = AccountId FROM [dbo].AE_Invoices WHERE InvoiceID = @InvoiceID;
	END

	-- Check that there is an MS_Account record for this
	IF (NOT EXISTS(SELECT * FROM dbo.MS_Accounts WHERE AccountID = @AccountId))
	BEGIN
		/** TODO:  
		 *  We need to add logic here for returning the appropriet fees with their respective Item ID's. */

		/** Insert values into the ms account table. */
		INSERT INTO dbo.MS_Accounts ( 
			AccountID
			, CellularTypeId
			, CellPackageItemId
			, PanelTypeId
			, ContractId
		) VALUES (
			@AccountId
			, @CellularTypeId
			, @AlarmComPackageItemId
			, @PanelTypeId
			, @ContractId
		);
	END
	ELSE
	BEGIN
		SELECT 
			@CellularTypeId = MS.CellularTypeId
			, @AlarmComPackageItemId = MS.CellPackageItemId
			, @PanelTypeId = MS.PanelTypeId
			, @ContractId = MS.ContractId
		FROM
			dbo.MS_Accounts AS MS WITH (NOLOCK)
		WHERE
			(MS.AccountID = @AccountId);
	END

	-- 
	
	BEGIN TRY
		/** Acquire Metadata. */
		SELECT @ActivationFee = Price FROM dbo.AE_Items WHERE (ItemID = @ActivationFeeItemId) AND (IsActive = 1 AND IsDeleted = 0);
		IF (@ActivationFee IS NULL)
		BEGIN
			RAISERROR (N'The itemid for the activation fee ''%s'' was not found.', 19, 1, @ActivationFeeItemId) ;
		END
		SELECT @MonthlyMonitoringRate = Price FROM dbo.AE_Items WHERE (ItemID = @MonthlyMonitoringRateItemId) AND (IsActive = 1 AND IsDeleted = 0);
		IF (@MonthlyMonitoringRate IS NULL)
		BEGIN
			RAISERROR (N'The itemid for the MMR ''%s'' was not found.', 19, 1, @MonthlyMonitoringRateItemId) ;
		END

		/** Check to see if the invoice exists. */
		IF (NOT EXISTS(SELECT
			*
		FROM
			[dbo].vwAE_InvoiceMsInstallInfo AS INV
		WHERE
			((@InvoiceID IS NULL) OR (INV.InvoiceID = @InvoiceID))
			AND ((@AccountId IS NULL) OR (INV.AccountId = @AccountId))))
		BEGIN
			INSERT INTO dbo.AE_Invoices (
				AccountId
				, InvoiceTypeId
				, DocDate
				, SalesAmount
				, OriginalTransactionAmount
				, CostAmount
				, TaxAmount
				, ModifiedById
				, CreatedById
			) VALUES (
				@AccountId -- AccountId - bigint
				, 'INSTALL' -- InvoiceTypeId - varchar(20)
				, GETUTCDATE() -- DocDate - date
				, 0
				, 0
				, 0
				, 0
				, @GpEmployeeID  -- ModifiedById - nvarchar(50)
				, @GpEmployeeID -- CreatedById - nvarchar(50)
			);
		END

		/** Transfer data */
		SELECT
			INV.InvoiceID
			, INV.AccountId
			, @ActivationFeeItemId AS ActivationFeeItemId
			, @ActivationFee AS ActivationFee
			, CASE
				WHEN dbo.fxInvoiceCalActivationFee(INV.InvoiceID) IS NULL THEN @ActivationFee
				ELSE dbo.fxInvoiceCalActivationFee(INV.InvoiceID)
			  END AS ActivationFeeActual
			, @MonthlyMonitoringRateItemId AS MonthlyMonitoringRateItemId
			, @MonthlyMonitoringRate AS MonthlyMonitoringRate
			, CASE
				WHEN dbo.fxInvoiceCalMonthlyMonitoringRate(INV.InvoiceID) IS NULL THEN @MonthlyMonitoringRate
				ELSE dbo.fxInvoiceCalMonthlyMonitoringRate(INV.InvoiceID)
			  END AS MonthlyMonitoringRateActual
			, INV.PaymentTypeId
			, INV.BillingDay
			, INV.Email
			, @AlarmComPackageId AS AlarmComPackageId
			, @AlarmComPackageItemId AS AlarmComPackageItemId
			, INV.CellVendorId
			, @Over3Months AS Over3Months
			, @CellularTypeId AS CellularTypeId
			, @PanelTypeId AS PanelTypeId
			, @ContractId AS ContractId
			, INV.IsMoni
			, INV.IsTakeOver
			, INV.IsOwner
		FROM
			[dbo].vwAE_InvoiceMsInstallInfo AS INV
		WHERE
			((@InvoiceID IS NULL) OR (INV.InvoiceID = @InvoiceID))
			AND ((@AccountId IS NULL) OR (INV.AccountId = @AccountId));
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_InvoiceMsInstallInfoViewGetByIDs TO PUBLIC
GO

/**  
EXEC dbo.custAE_InvoiceMsInstallInfoViewGetByIDs NULL, NULL, 'SOSA001';
EXEC dbo.custAE_InvoiceMsInstallInfoViewGetByIDs NULL, 100212, 'SOSA001';
EXEC dbo.custAE_InvoiceMsInstallInfoViewGetByIDs NULL, 100290, 'SOSA001';
EXEC dbo.custAE_InvoiceMsInstallInfoViewGetByIDs 10010064, NULL, 'PRIV001';

SELECT * FROM dbo.AE_InvoiceItems WHERE InvoiceId = 10010064;
*/