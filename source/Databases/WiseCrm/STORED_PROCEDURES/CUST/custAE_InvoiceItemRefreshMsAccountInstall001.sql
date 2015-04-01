USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_InvoiceItemRefreshMsAccountInstall001')
	BEGIN
		PRINT 'Dropping Procedure custAE_InvoiceItemRefreshMsAccountInstall001'
		DROP  Procedure  dbo.custAE_InvoiceItemRefreshMsAccountInstall001
	END
GO

PRINT 'Creating Procedure custAE_InvoiceItemRefreshMsAccountInstall001'
GO
/******************************************************************************
**		File: custAE_InvoiceItemRefreshMsAccountInstall001.sql
**		Name: custAE_InvoiceItemRefreshMsAccountInstall001
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
**	06/12/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_InvoiceItemRefreshMsAccountInstall001
(
	@InvoiceID BIGINT
	, @AccountId BIGINT
	, @BillingDay SMALLINT
	, @CurrentMonitoringStation VARCHAR(50)
	, @Email NVARCHAR(250)
	, @ActivationFeeItemId VARCHAR(25)
	, @ActivationFeeActual MONEY
	, @MMRItemId VARCHAR(25)
	, @MMRActual MONEY
	, @PanelTypeId VARCHAR(20)
	, @CellularTypeId VARCHAR(25)
	, @CellPackageItemId VARCHAR(50)
	, @Over3Months BIT
	, @AlarmComPackageId NVARCHAR(50)
	, @PaymentTypeId VARCHAR(20)
	, @IsTakeOver BIT 
	, @IsOwner BIT
	, @IsMoni BIT
	, @ContractTemplateId INT
	, @ContractLength INT
	, @DealerId INT
	, @GpEmployeeID NVARCHAR(25)
	, @SalesmanID NVARCHAR(25)
	, @TechnicianID NVARCHAR(25)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @ActivationFee MONEY,
		@MMR MONEY, @CellRate MONEY, @CellCost MONEY, @Over3MonthsDeleted BIT,
		@StateID VARCHAR(4) = 'UT', @PostalCode VARCHAR(5) = '84097',
		@AlarmComPackageItemId VARCHAR(50), @ContractId INT;

	/** VALIDATIONS */
	IF (@ContractTemplateId = 0)
	BEGIN
		SELECT @ContractTemplateId = ContractTemplateID FROM [dbo].[AE_ContractTemplates] WHERE (ContractLength = 60);
	END

	/** Get StateID and PostalCode. */
	IF (EXISTS(	SELECT * FROM
		[dbo].AE_Invoices AS INV WITH (NOLOCK)
		INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountID = INV.AccountId)
			AND (INV.InvoiceID = @InvoiceID)
		INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
		ON
			(ADR.AddressID = MAC.ShipAddressId)))
	BEGIN
		SELECT
			@StateID = ADR.StateId
			, @PostalCode = ADR.PostalCode
		FROM
			[dbo].AE_Invoices AS INV WITH (NOLOCK)
			INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
			ON
				(MAC.AccountID = INV.AccountId)
				AND (INV.InvoiceID = @InvoiceID)
			INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
			ON
				(ADR.AddressID = MAC.ShipAddressId);
	END

	BEGIN TRY
		BEGIN TRANSACTION;
			/** Check for SETUP_FEE Invoice Item and MMR Items. */
			UPDATE [dbo].AE_InvoiceItems SET 
				TaxOptionId = 'TAX'
				, QTY = 1
				, Cost = AEI.Cost
				, RetailPrice = AEI.Price
				, PriceWithTax = AEI.Price
				, IsDeleted = 1
				, ModifiedBy = @GpEmployeeID
			FROM 
				[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				INNER JOIN [dbo].AE_Items AS AEI WITH (NOLOCK)
				ON
					(AEI.ItemID = AII.ItemId)
			WHERE
				(AII.InvoiceId = @InvoiceID) AND (AEI.ItemTypeId = 'SETUP_FEE');

			/** Get item prices. */
			SELECT @ActivationFee = Price FROM [dbo].AE_Items WHERE ItemID = @ActivationFeeItemId;
			SELECT @MMR = Price FROM [dbo].AE_Items WHERE ItemID = @MMRItemId;
			SELECT @CellRate = Price, @CellCost = Cost FROM [dbo].AE_Items WHERE ItemID = @CellularTypeId;
			SELECT @Over3MonthsDeleted = CASE WHEN @Over3Months = 1 THEN 0 ELSE 1 End;

			/** Set the correct Setup Fee Item. */
			IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				WHERE (AII.InvoiceId = @InvoiceID) AND (AII.ItemId = @ActivationFeeItemId)))
			BEGIN
				-- ** Set Correct Activation amount
				UPDATE [dbo].AE_InvoiceItems SET
					Qty = 1
					, RetailPrice = @ActivationFee
					, PriceWithTax = @ActivationFee
					, SalesmanID = @SalesmanID
					, TechnicianID = @TechnicianID
					, IsDeleted = 0
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				FROM 
					[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				WHERE
					(AII.InvoiceId = @InvoiceID) AND (AII.ItemId = @ActivationFeeItemId);
			END
			ELSE
			BEGIN
				-- ** Create ivoice item
				INSERT INTO dbo.AE_InvoiceItems (
					InvoiceId
					, ItemId
					, TaxOptionId
					, Qty
					, Cost
					, RetailPrice
					, PriceWithTax
					, SalesmanID
					, TechnicianID
					, ModifiedBy
					, CreatedBy
				) VALUES (
					@InvoiceID  -- InvoiceId - bigint
					, @ActivationFeeItemId -- ItemId - varchar(50)
					, 'TAX' -- TaxOptionId - char(3)
					, 1 -- Qty - smallint
					, 0 -- Cost - money
					, @ActivationFee -- RetailPrice - money
					, @ActivationFee -- PriceWithTax - money
					, @SalesmanID
					, @TechnicianID
					, @GpEmployeeID -- ModifiedBy - nvarchar(50)
					, @GpEmployeeID -- CreatedBy - nvarchar(50)
				);
			END

			/** Set the correct MMR Item. */
			IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				WHERE (AII.InvoiceId = @InvoiceID) AND (AII.ItemId = @MMRItemId)))
			BEGIN
				-- ** Set Correct Activation amount
				UPDATE [dbo].AE_InvoiceItems SET
					Qty = 1
					, RetailPrice = @MMR
					, PriceWithTax = @MMR
					, SalesmanID = @SalesmanID
					, TechnicianID = @TechnicianID
					, IsDeleted = 0
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				FROM 
					[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				WHERE
					(AII.InvoiceId = @InvoiceID) AND (AII.ItemId = @MMRItemId);
			END
			ELSE
			BEGIN
				-- ** Create ivoice item
				INSERT INTO dbo.AE_InvoiceItems (
					InvoiceId
					, ItemId
					, TaxOptionId
					, Qty
					, Cost
					, RetailPrice
					, PriceWithTax
					, ModifiedBy
					, CreatedBy
				) VALUES (
					@InvoiceID  -- InvoiceId - bigint
					, @MMRItemId -- ItemId - varchar(50)
					, 'TAX' -- TaxOptionId - char(3)
					, 1 -- Qty - smallint
					, 0 -- Cost - money
					, @MMR -- RetailPrice - money
					, @MMR -- PriceWithTax - money
					, @GpEmployeeID -- ModifiedBy - nvarchar(50)
					, @GpEmployeeID -- CreatedBy - nvarchar(50)
				);
			END

			-- START Scenerio 1 Activation Fees
			IF (@ActivationFee = @ActivationFeeActual)
			BEGIN
				-- ** Disable any discounts and/or increases to the activation fee
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND ((ItemId = 'SETUP_FEE_DISC') OR (ItemId = 'SETUP_FEE_UPSL'));

			END
			ELSE IF (@ActivationFeeActual > @ActivationFee)
			BEGIN
				-- ** Disable any discounts to the activation fee
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_DISC');

				/** Search for the SETUP_FEE_UPSL item. */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) WHERE (InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_UPSL')))
				BEGIN
					/** Update the item */
					UPDATE dbo.AE_InvoiceItems SET
						InvoiceId = @InvoiceID
						, ItemId = 'SETUP_FEE_UPSL'
						, TaxOptionId = 'TAX'
						, QTY = 1
						, Cost = 0
						, RetailPrice = @ActivationFeeActual - @ActivationFee
						, PriceWithTax = @ActivationFeeActual - @ActivationFee
						, IsActive = 1
						, IsDeleted = 0
						, ModifiedBy = @GpEmployeeID
						, ModifiedOn = GETUTCDATE()
						, SalesManID = @SalesmanID
						, TechnicianID = @TechnicianID
					WHERE
						(InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_UPSL');
				END
				ELSE
				BEGIN
					/** Insert new item */
					INSERT INTO dbo.AE_InvoiceItems (
						InvoiceId
						, ItemId
						, TaxOptionId
						, Qty
						, Cost
						, RetailPrice
						, PriceWithTax
						, IsActive
						, IsDeleted
						, ModifiedBy
						, CreatedBy 
						, SalesManID
						, TechnicianID
					) VALUES (
						@InvoiceID -- InvoiceId - bigint
						, 'SETUP_FEE_UPSL' -- ItemId - varchar(50)
						, 'TAX' -- TaxOptionId - char(3)
						, 1 -- Qty - smallint
						, 0 -- Cost - money
						, @ActivationFeeActual - @ActivationFee -- RetailPrice - money
						, @ActivationFeeActual - @ActivationFee -- PriceWithTax - money
						, 1 -- IsActive - bit
						, 0-- IsDeleted - bit
						, @GpEmployeeID -- ModifiedBy - nvarchar(50)
						, @GpEmployeeID -- CreatedBy - nvarchar(50)
						, @SalesmanID
						, @TechnicianID
					);
                END
			END
			ELSE -- IF (@ActivationFee > @ActivationFeeActual)
			BEGIN
				-- ** Disable any increase to the activation fee
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_UPSL');

				/** Search for the SETUP_FEE_DISC item. */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) WHERE (InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_DISC')))
				BEGIN
					/** Update the item */
					UPDATE dbo.AE_InvoiceItems SET
						InvoiceId = @InvoiceID
						, ItemId = 'SETUP_FEE_DISC'
						, TaxOptionId = 'TAX'
						, QTY = 1
						, Cost = 0
						, RetailPrice = @ActivationFeeActual - @ActivationFee
						, PriceWithTax = @ActivationFeeActual - @ActivationFee
						, IsActive = 1
						, IsDeleted = 0
						, ModifiedBy = @GpEmployeeID
						, ModifiedOn = GETUTCDATE()
						, SalesManID = @SalesmanID
						, TechnicianID = @TechnicianID
					WHERE
						(InvoiceId = @InvoiceID) AND (ItemId = 'SETUP_FEE_DISC');
				END
				ELSE
				BEGIN
					/** Insert new item */
					INSERT INTO dbo.AE_InvoiceItems (
						InvoiceId
						, ItemId
						, TaxOptionId
						, Qty
						, Cost
						, RetailPrice
						, PriceWithTax
						, IsActive
						, IsDeleted
						, ModifiedBy
						, CreatedBy 
						, SalesManID
						, TechnicianID
					) VALUES (
						@InvoiceID -- InvoiceId - bigint
						, 'SETUP_FEE_DISC' -- ItemId - varchar(50)
						, 'TAX' -- TaxOptionId - char(3)
						, 1 -- Qty - smallint
						, 0 -- Cost - money
						, @ActivationFeeActual - @ActivationFee -- RetailPrice - money
						, @ActivationFeeActual - @ActivationFee -- PriceWithTax - money
						, 1 -- IsActive - bit
						, 0-- IsDeleted - bit
						, @GpEmployeeID -- ModifiedBy - nvarchar(50)
						, @GpEmployeeID -- CreatedBy - nvarchar(50)
						, @SalesmanID
						, @TechnicianID
					);
                END
			END
			--   END Scenerio 1 Activation Fees

			-- START Scenerio 2 MMR
			IF(@MMR = @MMRActual)
			BEGIN
				-- ** Disable any discounts and or increases to the MMR
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND ((ItemId = 'MMR_SREP_DISC') OR (ItemId = 'MMR_SREP_UPSL'));
			END
			ELSE IF (@MMRActual > @MMR)
			BEGIN
				-- ** Disable any discounts to the activation fee
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_DISC');

				/** Search for the SETUP_FEE_UPSL item. */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) WHERE (InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_UPSL')))
				BEGIN
					/** Update the item */
					UPDATE dbo.AE_InvoiceItems SET
						InvoiceId = @InvoiceID
						, ItemId = 'MMR_SREP_UPSL'
						, TaxOptionId = 'TAX'
						, QTY = 1
						, Cost = 0
						, RetailPrice = @MMRActual - @MMR
						, PriceWithTax = @MMRActual - @MMR
						, IsActive = 1
						, IsDeleted = 0
						, ModifiedBy = @GpEmployeeID
						, ModifiedOn = GETUTCDATE()
						, SalesManID = @SalesmanID
						, TechnicianID = @TechnicianID
					WHERE
						(InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_UPSL');
				END
				ELSE
				BEGIN
					/** Insert new item */
					INSERT INTO dbo.AE_InvoiceItems (
						InvoiceId
						, ItemId
						, TaxOptionId
						, Qty
						, Cost
						, RetailPrice
						, PriceWithTax
						, IsActive
						, IsDeleted
						, ModifiedBy
						, CreatedBy 
						, SalesmanID
						, TechnicianID
					) VALUES (
						@InvoiceID -- InvoiceId - bigint
						, 'MMR_SREP_UPSL' -- ItemId - varchar(50)
						, 'TAX' -- TaxOptionId - char(3)
						, 1 -- Qty - smallint
						, 0 -- Cost - money
						, @MMRActual - @MMR -- RetailPrice - money
						, @MMRActual - @MMR -- PriceWithTax - money
						, 1 -- IsActive - bit
						, 0-- IsDeleted - bit
						, @GpEmployeeID -- ModifiedBy - nvarchar(50)
						, @GpEmployeeID -- CreatedBy - nvarchar(50)
						, @SalesmanID
						, @TechnicianID
					);
                END
			END
			ELSE 
			BEGIN
				-- ** Disable any increase to the mmr
				UPDATE [dbo].AE_InvoiceItems SET
					IsDeleted = 1
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				WHERE
					(InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_UPSL');

				/** Search for the SETUP_FEE_DISC item. */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK) WHERE (InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_DISC')))
				BEGIN
					/** Update the item */
					UPDATE dbo.AE_InvoiceItems SET
						InvoiceId = @InvoiceID
						, ItemId = 'MMR_SREP_DISC'
						, TaxOptionId = 'TAX'
						, QTY = 1
						, Cost = 0
						, RetailPrice = @MMRActual - @MMR
						, PriceWithTax = @MMRActual - @MMR
						, IsActive = 1
						, IsDeleted = 0
						, ModifiedBy = @GpEmployeeID
						, ModifiedOn = GETUTCDATE()
						, SalesmanID = @SalesmanID
						, TechnicianID = @TechnicianID
					WHERE
						(InvoiceId = @InvoiceID) AND (ItemId = 'MMR_SREP_DISC');
				END
				ELSE
				BEGIN
					/** Insert new item */
					INSERT INTO dbo.AE_InvoiceItems (
						InvoiceId
						, ItemId
						, TaxOptionId
						, Qty
						, Cost
						, RetailPrice
						, PriceWithTax
						, IsActive
						, IsDeleted
						, ModifiedBy
						, CreatedBy 
						, SalesManID
						, TechnicianID
					) VALUES (
						@InvoiceID -- InvoiceId - bigint
						, 'MMR_SREP_DISC' -- ItemId - varchar(50)
						, 'TAX' -- TaxOptionId - char(3)
						, 1 -- Qty - smallint
						, 0 -- Cost - money
						, @MMRActual - @MMR -- RetailPrice - money
						, @MMRActual - @MMR -- PriceWithTax - money
						, 1 -- IsActive - bit
						, 0-- IsDeleted - bit
						, @GpEmployeeID -- ModifiedBy - nvarchar(50)
						, @GpEmployeeID -- CreatedBy - nvarchar(50)
						, @SalesmanID
						, @TechnicianID
					);
                END
			END
			--   END Scenerio 2 MMR

			-- START Scenerio 3 Cellular Packages
			IF (EXISTS(SELECT * FROM [dbo].[MS_Accounts] WHERE AccountID = @AccountId))
			BEGIN
				UPDATE [dbo].[MS_Accounts] SET
					CellularTypeId = @CellularTypeId
					, CellPackageItemId = @CellPackageItemId
				WHERE
					(AccountID = @AccountID);
			END

			/** Set the over 3 months . */
			UPDATE [dbo].AE_InvoiceItems SET
				IsDeleted = @Over3MonthsDeleted
				, ModifiedBy = @GpEmployeeID
				, ModifiedOn = GETUTCDATE()
			FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK)
				INNER JOIN [dbo].AE_Items AS AIT WITH (NOLOCK)
				ON
					(AII.ItemId = AIT.ItemID)
			WHERE (AII.InvoiceId = @InvoiceID) AND (AIT.ItemTypeId = 'SETUP_FEE_OVR3');

			IF (@Over3Months = 1)
			BEGIN
				/** Calculate the discount amount. */
				DECLARE @Over3MonthsDisc DECIMAL;
				SELECT
					@Over3MonthsDisc = CAST(SUM(AII.RetailPrice) - (CAST(SUM(AII.RetailPrice) AS FLOAT) / 3) AS DECIMAL(6,2))
				FROM 
					[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
					INNER JOIN [dbo].AE_Items AS AEI WITH (NOLOCK)
					ON
						(AEI.ItemID = AII.ItemId)
				WHERE
					(AII.InvoiceId = @InvoiceID) 
					AND ((AEI.ItemTypeId = 'SETUP_FEE') OR (AEI.ItemTypeId = 'SETUP_FEE_UPSL') OR (AEI.ItemTypeId = 'SETUP_FEE_DISC'))
					AND (AII.IsDeleted = 0)
				GROUP BY
					AII.InvoiceId;
				PRINT 'Over 3 Month Disc: ' + CAST(@Over3MonthsDisc AS VARCHAR);

				/** Set over 3 months */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK)
					WHERE (AII.InvoiceId = @InvoiceID) AND (AII.ItemId = 'SETUP_FEE_OVR3')))
				BEGIN
					-- ** Set Correct Activation amount
					UPDATE [dbo].AE_InvoiceItems SET
					Qty = 1
					, RetailPrice = @Over3MonthsDisc * (-1)
					, PriceWithTax = @Over3MonthsDisc * (-1)
					, SalesmanID = @SalesmanID
					, TechnicianID = @TechnicianID
					, IsDeleted = 0
					, IsActive = 1
					, ModifiedBy = @GpEmployeeID
					, ModifiedOn = GETUTCDATE()
				FROM 
					[dbo].AE_InvoiceItems AS AII WITH (NOLOCK) 
				WHERE
					(AII.InvoiceId = @InvoiceID) AND (AII.ItemId = 'SETUP_FEE_OVR3');
				END
				ELSE
				BEGIN
					INSERT INTO [dbo].[AE_InvoiceItems] (
					[InvoiceId]
					,[ItemId]
					,[TaxOptionId]
					,[Qty]
					,[Cost]
					,[RetailPrice]
					,[PriceWithTax]
					,[SalesmanId]
					,[TechnicianId]
					,[IsActive]
					,[IsDeleted]
					,[ModifiedBy]
					,[CreatedBy]
				) VALUES (
					@InvoiceID
					, 'SETUP_FEE_OVR3'
					, 'TAX'
					, 1
					, 0
					, @Over3MonthsDisc * (-1)
					, @Over3MonthsDisc * (-1)
					, @SalesmanId
					, @TechnicianId
					, 1
					, 0
					, @GpEmployeeID
					, @GpEmployeeID
				);
				END
			END
			--   END Scenerio 4 Activation Over 3 Months

			-- START Scenerio 5 Alarm.Com Packages
			UPDATE [dbo].AE_InvoiceItems SET
				IsDeleted = 1
				, ModifiedBy = @GpEmployeeID
				, ModifiedOn = GETUTCDATE()
			FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK)
				INNER JOIN [dbo].AE_Items AS AIT WITH (NOLOCK)
				ON
					(AII.ItemId = AIT.ItemID)
			WHERE (AII.InvoiceId = @InvoiceID) AND (AIT.ItemTypeId = 'CELL_SRV');

			IF (@AlarmComPackageId IS NOT NULL)
			BEGIN
				/** Get ItemId */
				SELECT @AlarmComPackageItemId = dbo.fxInvoiceAlarmComPackageItemByPackageId(@AlarmComPackageId, @DealerId);
				SET @CellPackageItemId = @AlarmComPackageItemId;

				/** Check to see if there is an item already. */
				IF (EXISTS(SELECT * FROM [dbo].AE_InvoiceItems AS AII WITH (NOLOCK)
					INNER JOIN [dbo].AE_Items AS AIT WITH (NOLOCK)
					ON
						(AII.ItemId = AIT.ItemID)
				WHERE (AII.InvoiceId = @InvoiceID) AND (AIT.ItemTypeId = 'CELL_SRV')))

				BEGIN
					UPDATE [dbo].AE_InvoiceItems SET
						ItemId = @AlarmComPackageItemId
						, TaxOptionId = AIT.TaxOptionId
						, Qty = 1
						, Cost = AIT.Cost
						, RetailPrice = AIT.Price
						, PriceWithTax = AIT.Price
						, SystemPoints = AIT.SystemPoints
						, SalesmanId = @SalesmanID
						, TechnicianId = @TechnicianID
						, IsActive = 1
						, IsDeleted = 0
						, ModifiedBy = @GpEmployeeID
						, ModifiedOn = GETUTCDATE()
					FROM
						[dbo].AE_InvoiceItems AS AII WITH (NOLOCK)
						INNER JOIN [dbo].AE_Items AS AIT WITH (NOLOCK)
						ON
							(AII.ItemId = AIT.ItemID)
					WHERE 
						(AII.InvoiceId = @InvoiceID) 
						AND (AIT.ItemID = @AlarmComPackageItemId)
						AND (AIT.ItemTypeId = 'CELL_SRV');

				END
				ELSE
				BEGIN
					INSERT INTO [dbo].[AE_InvoiceItems] ( 
						[InvoiceId] ,
						[ItemId] ,
						[TaxOptionId] ,
						[Qty] ,
						[Cost] ,
						[RetailPrice] ,
						[PriceWithTax] ,
						[SystemPoints] ,
						[SalesmanId] ,
						[TechnicianId] ,
						[IsActive] ,
						[IsDeleted] ,
						[ModifiedOn] ,
						[ModifiedBy] ,
						[CreatedOn] ,
						[CreatedBy]
					) 
					SELECT 
						@InvoiceID , -- InvoiceId - bigint
						@AlarmComPackageItemId , -- ItemId - varchar(50)
						ITM.TaxOptionId , -- TaxOptionId - char(3)
						1 , -- Qty - smallint
						ITM.Cost , -- Cost - money
						ITM.Price , -- RetailPrice - money
						ITM.Price , -- PriceWithTax - money
						ITM.SystemPoints , -- SystemPoints - decimal
						@SalesmanId , -- SalesmanId - nvarchar(25)
						@TechnicianId , -- TechnicianId - nvarchar(25)
						1 , -- IsActive - bit
						0 , -- IsDeleted - bit
						GETUTCDATE() , -- ModifiedOn - datetime
						@GpEmployeeID , -- ModifiedBy - nvarchar(50)
						GETUTCDATE() , -- CreatedOn - datetime
						@GpEmployeeID -- CreatedBy - nvarchar(50)
					FROM
						[dbo].[AE_Items] AS ITM WITH (NOLOCK)
					WHERE
						(ITM.ItemID = @AlarmComPackageItemId);
				END
			END
			-- Update MS Account table with the right Alarm.com package ID
			UPDATE [dbo].[MS_Accounts] SET 
				PanelTypeId = @PanelTypeId
				, CellPackageItemId = @CellPackageItemId 
			WHERE 
				(AccountID = @AccountId);

			--   END Scenerio 5 Alarm.Com Packages

			-- START Save Account Sales Information
			-- ** Initialize arguments that were passed
			SELECT @IsMoni = ISNULL(@IsMoni, 0);
			IF (EXISTS(SELECT * FROM [dbo].[MS_AccountSalesInformations] WHERE (AccountID = @AccountID)))
			BEGIN
				UPDATE [dbo].[MS_AccountSalesInformations] SET
					PaymentTypeId = @PaymentTypeId
					, BillingDay = @BillingDay
					, Email = @Email
					, IsMoni = @IsMoni
					, IsTakeOver = @IsTakeOver
					, IsOwner = @IsOwner
					, ModifiedOn = GetUTCDate()
					, ModifiedBy = @GPEmployeeID
				WHERE
					(AccountID = @AccountID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_AccountSalesInformations] (
					AccountID
					, PaymentTypeId
					, BillingDay
					, Email
					, IsMoni
					, IsTakeOver
					, IsOwner
					, ModifiedBy
					, CreatedBy
				) VALUES (
					@AccountID
					, @PaymentTypeId
					, @BillingDay
					, @Email
					, @IsMoni
					, @IsTakeOver
					, @IsOwner
					, @GpEmployeeID
					, @GpEmployeeID
				);
			END
			-- END Save Account Sales Information

			-- START Save Contract Length Information
			IF(EXISTS(SELECT
					*
				FROM
					[dbo].MS_Accounts AS MAC WITH (NOLOCK)
					INNER JOIN [dbo].AE_Contracts AS CNT WITH (NOLOCK)
					ON
						(CNT.ContractID = MAC.ContractId)
						AND (CNT.IsDeleted = 0)
				WHERE
					(MAC.AccountID = @AccountId)))
			BEGIN
				IF (@ContractTemplateId IS NULL)
				BEGIN
					UPDATE dbo.AE_Contracts SET
						IsActive = 0
						, IsDeleted = 1
					FROM
						dbo.AE_Contracts AS AC WITH (NOLOCK)
						INNER JOIN dbo.MS_Accounts AS MAS WITH (NOLOCK)
						ON
							(MAS.ContractId = AC.ContractID)
							AND (MAS.AccountID = @AccountId);
				END
				ELSE
				BEGIN
					-- ** Update the TemplateId
					SELECT @ContractLength = ISNULL(@ContractLength, ACT.ContractLength) FROM [dbo].[AE_ContractTemplates] AS ACT WITH (NOLOCK) WHERE (ACT.ContractTemplateID = @ContractTemplateId);
					UPDATE [dbo].[AE_Contracts] SET
						ContractTemplateId = @ContractTemplateId
						, ContractLength = @ContractLength
						, IsActive = 1
						, IsDeleted = 0
					FROM
						dbo.AE_Contracts AS AC WITH (NOLOCK)
						INNER JOIN dbo.MS_Accounts AS MAS WITH (NOLOCK)
						ON
							(MAS.ContractId = AC.ContractID)
							AND (MAS.AccountID = @AccountId)
				END
			END
			ELSE
			BEGIN
				SELECT @ContractLength = ISNULL(@ContractLength, ACT.ContractLength) FROM [dbo].[AE_ContractTemplates] AS ACT WITH (NOLOCK) WHERE (ACT.ContractTemplateID = @ContractTemplateId);
				INSERT INTO [dbo].[AE_Contracts](
							[ContractTemplateId]
							,[AccountId]
							,[ContractName]
							,[ContractLength]
							,[MonthlyFee]
							,[ShortDesc]
							,[IsActive]
							,[IsDeleted]
							,[ModifiedById]
							,[CreatedById]
					)  
					SELECT
							ContractTemplateID
							,@AccountId AS AccountId
							,CT.ContractName
							,@ContractLength
							,CT.MonthlyFee
							,CT.ShortDesc
							,1 AS [IsActive]
							,0 AS [IsDeleted]
							,@GpEmployeeId
							,@GpEmployeeId
					FROM
						[dbo].[AE_ContractTemplates] AS CT WITH (NOLOCK)
					WHERE
						(CT.ContractTemplateID = @ContractTemplateId);

					-- ** Set the contract Id in the MsAccounts table.
					SET @ContractId = SCOPE_IDENTITY();
					UPDATE [dbo].MS_Accounts SET ContractId = @ContractId WHERE AccountID = @AccountId;
			END
			-- END Save Contract Length Information


			/** 
			* FINISH by recalculating the invoice header
			*/

			EXEC dbo.custAE_InvoiceCalculatePrices @InvoiceID, @StateID, @PostalCode;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT * FROM vwAE_InvoiceItems WHERE (InvoiceID = @InvoiceID) AND (IsActive = 1) AND (IsDeleted = 0);

END
GO

GRANT EXEC ON dbo.custAE_InvoiceItemRefreshMsAccountInstall001 TO PUBLIC
GO

/** TESTs
DECLARE @InvoiceID BIGINT = 10020170, @AccountID BIGINT = 130532;
EXEC dbo.custAE_InvoiceItemRefreshMsAccountInstall001 @InvoiceID, @AccountID
	, 4 -- BillingDay
	, NULL -- CurrentMonitoringStation
	, 'ssdfs@m22.com' -- Email
	, 'SETUP_FEE_199' -- ActivationFeeItemId
	, 13 -- ActivationFeeActual
	, 'MON_CONT_5001' -- MMRItemId
	, 39.95 -- MMRActual
	, 'CONCORD' -- PanelTypeId
	, NULL --cellularTypeId
	, 'CELL_SRV_AC_AI' -- CellPackageItemId
	, 1 --over3Months
	, NULL --'CELL_SRV_AC_WSF'  -- alarmComPackageId
	, 'CC'  -- paymentTypeId
	, 1 -- isTakeOver
	, 0 -- isOwner
	, 1 -- isMoni
	, 0 -- ContractTemplateId
	, NULL -- 30 -- ContractLength
	, 0 -- dealerId
	, 'PRIV001' -- gpEmployeeID
	, NULL -- salesmanID
	, NULL -- technicianID
	;

SELECT * FROM MS_Accounts WHERE AccountID = @AccountID;
*/