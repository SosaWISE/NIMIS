USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'wiseAE_InvoiceItemsCleanUp')
	BEGIN
		PRINT 'Dropping Procedure wiseAE_InvoiceItemsCleanUp'
		DROP  Procedure  dbo.wiseAE_InvoiceItemsCleanUp
	END
GO

PRINT 'Creating Procedure wiseAE_InvoiceItemsCleanUp'
GO
/******************************************************************************
**		File: wiseAE_InvoiceItemsCleanUp.sql
**		Name: wiseAE_InvoiceItemsCleanUp
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
**		Date: 05/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/21/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.wiseAE_InvoiceItemsCleanUp
(
	@InvoiceItemID BIGINT
	, @DeleteZoneAssignments BIT = 0
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @ResultsTable TABLE (ID INT IDENTITY(1,1) NOT NULL, ActionType VARCHAR(50), [Description] VARCHAR(300));
	DECLARE @AccountEquipmentID BIGINT = NULL;
	
	BEGIN TRY
	BEGIN TRANSACTION

	SELECT @AccountEquipmentID = AccountEquipmentId FROM dbo.AE_InvoiceItems WHERE (InvoiceItemID = @InvoiceItemID);
	INSERT INTO @ResultsTable (ActionType, [Description]) VALUES ('SELECT', 'Acquiring AccountEquipmentID | ID: ' + CAST(@AccountEquipmentID AS VARCHAR));
	UPDATE dbo.MS_AccountEquipment SET InvoiceItemId = NULL WHERE (AccountEquipmentID = @AccountEquipmentID);
	INSERT INTO @ResultsTable (ActionType, [Description]) VALUES ('UPDATE', 'MS_AccountEquipment with InvoiceItemID to NULL');
	UPDATE dbo.AE_InvoiceItems SET AccountEquipmentId = NULL WHERE (InvoiceItemID = @InvoiceItemID);
	INSERT INTO @ResultsTable (ActionType, [Description]) VALUES ('UPDATE', 'AE_InvoiceItems with AccountEquipmentId to NULL');
	
	/** CHeck the Zone Assignemnts */
	IF (EXISTS(SELECT * FROM dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId = @AccountEquipmentID)) AND @DeleteZoneAssignments = 0)
	BEGIN
		INSERT INTO @ResultsTable (ActionType, Description ) VALUES ('WARNING', 'Has Account Zones');
		PRINT '|**-- There are Zone Assignments | DeleteZoneAssignments: ' + CAST(@DeleteZoneAssignments AS VARCHAR);
		SELECT * FROM @ResultsTable;
		ROLLBACK TRANSACTION
		RETURN;
	END

	IF (@DeleteZoneAssignments = 1)
	BEGIN
		DELETE dbo.MS_AccountZoneAssignments WHERE (AccountEquipmentId = @AccountEquipmentID)
	END

	DELETE dbo.MS_AccountEquipment WHERE (AccountEquipmentID = @AccountEquipmentId);
	INSERT INTO @ResultsTable (ActionType, [Description]) VALUES ('DELETE', 'MS_AccountEquipment with AccountEquipmentId of ' + CAST (@AccountEquipmentId AS VARCHAR));
	DELETE dbo.AE_InvoiceItems WHERE (InvoiceItemId = @InvoiceItemID);
	INSERT INTO @ResultsTable (ActionType, [Description]) VALUES ('DELETE', 'AE_InvoiceItems with InvoiceItemID of ' + CAST (@InvoiceItemID AS VARCHAR))

	SELECT * FROM @ResultsTable;
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SELECT * FROM @ResultsTable;
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.wiseAE_InvoiceItemsCleanUp TO PUBLIC
GO

/** */
DECLARE @CMFID BIGINT, @AccountID BIGINT = 191254, @InvoiceItemID BIGINT = 10065269 -- Has no barcode
--DECLARE @InvoiceItemID BIGINT = 10064653 -- Has barcode
EXEC dbo.wiseAE_InvoiceItemsCleanUp @InvoiceItemID;
SELECT TOP 1 @CMFID = AEC.CustomerMasterFileID FROM dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK) INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK) ON (AEC.CustomerID = AECA.CustomerId) WHERE (AECA.AccountId = @AccountID);
SELECT
	 MSAE.AccountEquipmentID ,
	        MSAE.AccountId ,
	        MSAE.EquipmentId ,
			MSE.GPItemNmbr ,
	        MSAE.BarcodeId ,
			MSE.ItemDescription,
	        MSAE.InvoiceItemId ,
	        MSAE.AccountEquipmentUpgradeTypeId ,
	        MSAE.CustomerLocation ,
	        MSAE.Points ,
	        MSAE.ActualPoints ,
	        MSAE.Price ,
	        MSAE.IsExisting ,
	        MSAE.IsServiceUpgrade ,
	        MSAE.IsExistingWiring ,
	        MSAE.IsMainPanel ,
	        MSAE.IsActive ,
	        MSAE.IsDeleted
FROM
	dbo.MS_AccountEquipment AS MSAE WITH (NOLOCK)
	INNER JOIN dbo.MS_Equipments AS MSE WITH (NOLOCK)
	ON
		(MSE.EquipmentID = MSAE.EquipmentId)
WHERE
	(MSAE.AccountId = @AccountID)
	AND (MSAE.BarcodeId IS NULL)
	AND (MSAE.AccountEquipmentID NOT IN (SELECT AccountEquipmentID FROM dbo.MS_AccountZoneAssignments));

SELECT
	 AEII.InvoiceItemID ,
	        AEII.InvoiceId ,
	        AEII.ItemId ,
			AEIT.ItemSKU ,
	        AEII.ProductBarcodeId ,
	        AEII.AccountEquipmentId ,
	        AEII.TaxOptionId ,
	        AEII.Qty ,
	        AEII.Cost ,
	        AEII.RetailPrice ,
	        AEII.PriceWithTax ,
	        AEII.SystemPoints ,
	        AEII.SalesmanId ,
	        AEII.TechnicianId ,
	        AEII.IsCustomerPaying ,
	        AEII.IsActive ,
	        AEII.IsDeleted
FROM
	dbo.AE_Invoices AS AEI WITH (NOLOCK)
	INNER JOIN dbo.AE_InvoiceItems AS AEII WITH (NOLOCK)
	ON
		(AEII.InvoiceId = AEI.InvoiceID)
		AND (AEI.AccountId = @AccountID)
	INNER JOIN dbo.AE_Items AS AEIT WITH (NOLOCK)
	ON
		(AEIT.ItemID = AEII.ItemId)


SELECT AccountID, @CMFID AS CustomerMasterFileID, TotalPoints, TotalPointsAllowed, RepPoints, TechPoints FROM dbo.vwMS_AccountSalesInformations WHERE (AccountID = @AccountID);