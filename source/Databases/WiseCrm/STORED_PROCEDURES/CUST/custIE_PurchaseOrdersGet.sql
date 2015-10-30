USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_PurchaseOrdersGet')
	BEGIN
		PRINT 'Dropping Procedure custIE_PurchaseOrdersGet'
		DROP  Procedure  dbo.custIE_PurchaseOrdersGet
	END
GO

PRINT 'Creating Procedure custIE_PurchaseOrdersGet'
GO
/******************************************************************************
**		File: custIE_PurchaseOrdersGet.sql
**		Name: custIE_PurchaseOrdersGet
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              r
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 06/30/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/30/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_PurchaseOrdersGet
(
	@GPPONumber VARCHAR(50)
	, @GPEmployeeID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @PurchaseOrderID INT;
	
	BEGIN TRY
		BEGIN TRANSACTION;
		/** Check to see if the PO already Exists*/
		IF(NOT EXISTS(SELECT * FROM [dbo].[IE_PurchaseOrders] WHERE (GPPONumber = @GPPONumber)))
		BEGIN

			/** TODO:  Change to point to a GP DB.*/
			--/*** WAREHOUSES  ***/
			--INSERT INTO [dbo].[IE_WarehouseSites] (
   --                  [WarehouseSiteID]
   --                  , [WarehouseSiteName]
			--)
			--SELECT 
			--	IV40700.LOCNCODE
			--	, IV40700.LOCNDSCR
			--FROM 
			--	[WISE_GP].[NEX}.dbo.IV40700
			--	LEFT JOIN [dbo].[IE_WarehouseSites]
			--	ON
			--		(IV40700.LOCNCODE = IE_WarehouseSites.WarehouseSiteID)
			--WHERE
			--	(IE_WarehouseSites.WarehouseSiteID IS NULL);

			/*** VENDOR  ***/
			--DECLARE @VENDORID VARCHAR(30)
			--SELECT @VENDORID = VENDORID FROM [WISE_GP].[NEX}.dbo.POP10100 WHERE (PONUMBER = @GPPONumber)

			--IF (NOT EXISTS(SELECT * FROM [dbo].[IE_Vendors] WHERE (VendorID = @VENDORID)))
			--BEGIN
			--	INSERT INTO [dbo].[IE_Vendors](
			--		[VendorID]
			--		, [VendorName]
			--	)
			--	SELECT
			--		VENDORID,
			--		VENDNAME 
			--	FROM [WISE_GP].[NEX}.dbo.PM00200 
			--	WHERE
			--		(VENDORID = @VENDORID);
			--END

			/*** PO HEADER  ***/
			/** Pull from GP and Create the PO row with it's respective PO Items. */
			INSERT INTO [dbo].[IE_PurchaseOrders] 
			(
				[VendorId]
				,[GPPONumber]
				--,[CloseDate]
				,[ModifiedBy]
				,[CreatedBy]
			) 
			/** TODO:  Change to point to a GP DB.*/
			SELECT VendorId, GPPONumber, ModifiedBy, CreatedBy FROM [dbo].[IE_PurchaseOrders] WHERE (PurchaseOrderID = -1);
			--SELECT
			--	VENDORID
			--	,PONUMBER
			--	--CLOSEDATE
			--	,@GPEmployeeID -- ModifiedBy - nvarchar(50)
			--	,@GPEmployeeID -- CreatedBy - nvarchar(50)
			--FROM
			--	[WISE_GP].[NEX}.dbo.POP10100
			--WHERE
			--	(PONUMBER = @GPPONUMBER);

			SET @PurchaseOrderID = SCOPE_IDENTITY();

			/*** PO LINES  ***/
			INSERT INTO [dbo].[IE_PurchaseOrderItems] (
			    PurchaseOrderId
			    ,ItemId
				,WarehouseSiteId
				,Quantity
				,ModifiedBy
				,CreatedBy
			)
			/** TODO:  Change to point to a GP DB.*/
			SELECT PurchaseOrderItemID, ItemId, WarehouseSiteId, Quantity, ModifiedBy, CreatedBy FROM [dbo].IE_PurchaseOrderItems WHERE (PurchaseOrderItemID = -1);
			--SELECT
			--	@PurchaseOrderID
			--	, ITM.ItemID
			--	, GPITM.LOCNCODE
			--	, GPITM.QTYORDER
			--	,@GPEmployeeID -- ModifiedBy - nvarchar(50)
			--	,@GPEmployeeID -- CreatedBy - nvarchar(50)
			--FROM
			--	[WISE_GP].[NEX}.dbo.POP10110 AS GPITM WITH (NOLOCK)
			--	INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			--	ON
			--		(ITM.ItemSKU = GPITM.ITEMNMBR)
			--WHERE
			--	(GPITM.PONUMBER = @GPPONumber);
		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[IE_PurchaseOrders] WHERE (GPPONumber = @GPPONumber);

END
GO

GRANT EXEC ON dbo.custIE_PurchaseOrdersGet TO PUBLIC
GO

/** EXEC dbo.custIE_PurchaseOrdersGet 'PO112388', 'SOSA001'; */