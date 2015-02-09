USE [WISE_CRM]
GO
/**********************************************************************************************************************
* DESC: This allows for the creation of a GPS Account

SELECT * FROM QL_Leads WHERE FirstName = 'Sheri'
SELECT * FROM dbo.AE_CustomerMasterFiles WHERE CustomerMasterFileID = 3000052
SELECT * FROM dbo.AE_Customers WHERE CustomerMasterFileId = 3000052
**********************************************************************************************************************/

/** Arguments */
DECLARE @CustomerMFID BIGINT;
DECLARE @CustomerID BIGINT;
DECLARE @LeadID BIGINT;
SET @CustomerID = 100300;							-- THINGS TO CHANGE SELECT * FROM dbo.AE_Customers WHERE CustomerID = 100300
SET @CustomerMFID = 3000052;						-- THINGS TO CHANGE
SET @LeadID = 1000075;								-- THINGS TO CHANGE
/** Monitoring Arugments */
DECLARE @Csid VARCHAR(15);
DECLARE @ReceiverLineId varchar(20);
DECLARE @ReceiverLineBlockId varchar(50);
DECLARE @SubscriberId varchar(6);

SET @Csid = '3132402915';							-- THINGS TO CHANGE
SET @ReceiverLineId = 'AG_GPSTRACK:AB';
SET @SubscriberId = '000008';						-- THINGS TO CHANGE
SET @ReceiverLineBlockId = @ReceiverLineId + ':' + @SubscriberId;

/** Device Information. */
DECLARE @SimProductBarcodeId nvarchar(50);
DECLARE @GpsWatchProductBarcodeId nvarchar(50);
DECLARE @GpsWatchPhoneNumber varchar(20);
DECLARE @GpsWatchPassword VARCHAR(50);
DECLARE @GpsWatchUnitID VARCHAR(50);
SET @SimProductBarcodeId = '8901260762215371940F'; -- THINGS TO CHANGE
SET @GpsWatchProductBarcodeId = '357304036112089'; -- THINGS TO CHANGE
SET @GpsWatchPhoneNumber = '1' + '3133751699';
SET @GpsWatchPassword = '02003008'; -- THINGS TO CHANGE


/** Local Variables */
DECLARE @AccountID BIGINT;


BEGIN TRY
	BEGIN TRANSACTION
		/** STEP 1 Create MC_Account */
		INSERT INTO dbo.MC_Accounts(ShipContactSameAsCustomer, ShipAddressSameAsCustomer) VALUES (1, 1);
		SET @AccountID = SCOPE_IDENTITY();
		
		/** Set the Unit ID */
		SET @GpsWatchUnitID = REPLICATE('0', 8 - LEN(CAST(@AccountID AS VARCHAR)));

		/** STEP 2 Create MS_Account */
		INSERT INTO dbo.MS_Accounts (
			AccountID ,
			IndustryAccountId ,
			SystemTypeId ,
			CellularTypeId ,
			PanelTypeId ,
			SimProductBarcodeId ,
			GpsWatchProductBarcodeId ,
			GpsWatchPhoneNumber,
			GpsWatchPassword,
			GpsWatchUnitID
		) VALUES (
			@AccountID , -- AccountID - bigint
			NULL , -- IndustryAccountId - bigint
			'GPSC' , -- SystemTypeId - varchar(20)
			'CELLPRI' , -- CellularTypeId - varchar(20)
			'PERS' , -- PanelTypeId - varchar(20)
			@SimProductBarcodeId , -- SimProductBarcodeId - nvarchar(50)
			@GpsWatchProductBarcodeId , -- GpsWatchProductBarcodeId - nvarchar(50)
			@GpsWatchPhoneNumber, -- GpsWatchPhoneNumber - varchar(20)
			@GpsWatchPassword, -- GpsWatchPassword - varchar(50)
			@GpsWatchUnitID -- GpsWatchUnitID - varchar(50)
		)
		
		/** Tie the Account to the Customer. */
		INSERT INTO dbo.AE_CustomerAccounts( CustomerId, AccountId ) VALUES  ( @CustomerID, @AccountID );
		
		/** Tie the customer and account in the MC_AccountCustomers. */
		INSERT INTO dbo.MC_AccountCustomers (
			AccountId ,
			CustomerId ,
			CustomerTypeId
		) VALUES (
			@AccountID , -- AccountId - bigint
			@CustomerID , -- CustomerId - bigint
			'GPSCLNT'  -- CustomerTypeId - varchar(20)
		)
		
		/** Create a GPS Customer entry on the dbo.AE_CustomerGpsClients. */
		INSERT INTO dbo.AE_CustomerGpsClients( CustomerID ) VALUES  ( @CustomerID )

		/** Create a Receiver Block */
		INSERT INTO dbo.MS_ReceiverLineBlocks (
			ReceiverLineBlockID ,
			ReceiverLineId ,
			CSID ,
			SubscriberNumber ,
			AccountId ,
			IsAssigned ,
			AssignedDate
		) VALUES (
			@ReceiverLineBlockID , -- ReceiverLineBlockID - varchar(50)
			@ReceiverLineId , -- ReceiverLineId - varchar(20)
			@Csid , -- CSID - varchar(11)
			@SubscriberId , -- SubscriberNumber - varchar(6)
			@AccountID , -- AccountId - bigint
			1 , -- IsAssigned - bit
			GETDATE() -- AssignedDate - datetime
		)

		/** Create an Industry Number entry */
		DECLARE @IndustryAccountID BIGINT;
		INSERT INTO dbo.MS_IndustryAccounts (
			AccountId,
			ReceiverLineId ,
			ReceiverLineBlockId ,
			SubscriberId ,
			Csid ,
			InTestMode ,
			IsMove ,
			IsTakeover
		) VALUES (
			@AccountID , -- AccountId - bigint
			@ReceiverLineId , -- ReceiverLineId - varchar(20)
			@ReceiverLineBlockId , -- ReceiverLineBlockId - varchar(50)
			@SubscriberId , -- SubscriberId - varchar(6)
			@Csid , -- Csid - varchar(15)
			0 , -- InTestMode - bit
			0 , -- IsMove - bit
			0 -- IsTakeover - bit
		);
		SET @IndustryAccountID = @@IDENTITY;

		/** Update MS_Accounts table with Industry Account ID. */
		UPDATE dbo.MS_Accounts SET IndustryAccountId = @IndustryAccountID WHERE AccountID = @AccountID;
--	COMMIT TRANSACTION
	ROLLBACK TRANSACTION
END TRY

BEGIN CATCH
	PRINT 'Made it here';
	ROLLBACK TRANSACTION
	EXEC dbo.wiseSP_ExceptionsThrown
	RETURN	
END CATCH
