USE [WISE_CRM]
GO

/**********************************************************************************************************************
* Adds a new device to a customer.
**********************************************************************************************************************/

/** ARGUMENTS */
DECLARE @CustomerID BIGINT;
SET @CustomerID = 100204;  -- Jessie Bellamy
DECLARE @AccountID BIGINT;
SET @AccountID = 100194;
DECLARE @ReceiverLineId VARCHAR(20);
SET @ReceiverLineId = 'AG_GPSTRACK:AB';
DECLARE @SubscriberId VARCHAR(6);
SET @SubscriberId = '000009';
DECLARE @Csid VARCHAR(15);
SET @Csid = '3134676579';
DECLARE @CreatedBy VARCHAR(50);
SET @CreatedBy = 'SosaWISE';
DECLARE @SimProductBarcodeId NVARCHAR(50);
SET @SimProductBarcodeId = '8901260762215371395F';
DECLARE @GpsWatchProductBarcodeId NVARCHAR(50);
SET @GpsWatchProductBarcodeId = '354476021053024';
DECLARE @GpsWatchPhoneNumber NVARCHAR(20);
SET @GpsWatchPhoneNumber = '1' + @Csid;

/** Local Declarations */
DECLARE @CMFID BIGINT;
DECLARE @IndustryAccountId BIGINT;
DECLARE @ReceiverLineBlockId VARCHAR(50);
SET @ReceiverLineBlockId = @ReceiverLineId + ':' + @SubscriberId;

/** Initialize. */
SELECT @CMFID = CustomerMasterFileId FROM dbo.AE_Customers WHERE (CustomerID = @CustomerID);

SELECT * FROM AE_Customers WHERE FirstName = 'Jessie';
SELECT * FROM dbo.MS_IndustryAccounts WHERE (AccountId = @AccountID);
SELECT * FROM MC_Accounts WHERE (AccountID = @AccountID);
SELECT * FROM MS_Accounts WHERE (AccountID = @AccountID);
SELECT * FROM dbo.AE_CustomerAccounts AS ACA WITH (NOLOCK) WHERE (CustomerId = @CustomerID);
SELECT * FROM dbo.AE_CustomerMasterToCustomer;

BEGIN TRANSACTION

INSERT INTO dbo.MS_Accounts (
		AccountID ,
		SystemTypeId ,
		CellularTypeId ,
		PanelTypeId ,
		SimProductBarcodeId ,
		GpsWatchProductBarcodeId ,
		GpsWatchPhoneNumber ,
		ModifiedBy ,
		CreatedBy
	) VALUES (
		@AccountID , -- AccountID - bigint
		'GPSC', -- SystemTypeId - varchar(20)
		'CELLPRI', -- CellularTypeId - varchar(20)
		'PERS', -- PanelTypeId - varchar(20)
		@SimProductBarcodeId, -- SimProductBarcodeId - nvarchar(50)
		@GpsWatchProductBarcodeId, -- GpsWatchProductBarcodeId - nvarchar(50)
		@GpsWatchPhoneNumber, -- GpsWatchPhoneNumber - varchar(20)
		@CreatedBy, -- ModifiedBy - nvarchar(50)
		@CreatedBy -- CreatedBy - nvarchar(50)
	);

INSERT INTO dbo.MS_IndustryAccounts (
		AccountId ,
		ReceiverLineId ,
		ReceiverLineBlockId ,
		SubscriberId ,
		Csid ,
		ModifiedBy ,
		CreatedBy
	) VALUES (
		@AccountID, -- AccountId - bigint
		@ReceiverLineId, -- ReceiverLineId - varchar(20)
		@ReceiverLineBlockId, -- ReceiverLineBlockId - varchar(50)
		@SubscriberId, -- SubscriberId - varchar(6)
		@Csid, -- Csid - varchar(15)
		@CreatedBy, -- ModifiedBy - nvarchar(50)
		@CreatedBy -- CreatedBy - nvarchar(50)
	);
/** Get Industry Id from identity */
SET @IndustryAccountId = SCOPE_IDENTITY();

UPDATE dbo.MS_Accounts SET IndustryAccountId = @IndustryAccountId;

INSERT INTO dbo.AE_CustomerAccounts (CustomerId, AccountId) VALUES (@CustomerID, @AccountID);

INSERT INTO dbo.AE_CustomerMasterToCustomer
	(
		CustomerMasterFileId ,
		CustomerId ,
		CustomerTypeId
	) VALUES (
		@CMFID, -- CustomerMasterFileId - bigint
		@CustomerID , -- CustomerId - bigint
		'GPSCLNT'  -- CustomerTypeId - varchar(20)
	);

ROLLBACK TRANSACTION