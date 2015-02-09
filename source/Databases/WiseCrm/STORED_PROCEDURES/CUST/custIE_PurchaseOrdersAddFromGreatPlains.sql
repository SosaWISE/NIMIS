USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_PurchaseOrdersAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custIE_PurchaseOrdersAddFromGreatPlains'
		DROP  Procedure  dbo.custIE_PurchaseOrdersAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custIE_PurchaseOrdersAddFromGreatPlains'
GO
/******************************************************************************
**		File: custIE_PurchaseOrdersAddFromGreatPlains.sql
**		Name: custIE_PurchaseOrdersAddFromGreatPlains
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
**		Auth: Bob McFadden
**		Date: 06/30/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/17/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_PurchaseOrdersAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @PurchaseOrderID INT;
	DECLARE @GPPONUMBER varchar(50);
	DECLARE @cntr int;
	DECLARE @maxrow int;

	BEGIN TRY
		-- DROP temp table if it exists and CREATE the temp table to hold the POs
		IF object_id('tempdb..#GPPOs') IS NOT NULL DROP TABLE #GPPOs
		CREATE TABLE #GPPOs
		(
		rownum int,
		GPPONUMBER varchar(50)
		)

		/*
		Pull the PO numbers from Great PLains into a temporary table that:
		--are new in GP
		--contain item skus
		--are not closed or cancelled in GP
		--do not exist in CRM
		*/
		INSERT #GPPOs (
			rownum,
			GPPONUMBER
			)
		SELECT 
			row_number() over(/*partition by POP10100.PONUMBER*/ order by POP10100.PONUMBER) as rownum,
			POP10100.PONUMBER
		FROM 
			-- PO HEADER
			DYSNEYDAD.NEX.dbo.POP10100
			JOIN
				(
				SELECT DISTINCT POP10100.PONUMBER AS PONUMBER
				FROM
					-- POHEADER
					DYSNEYDAD.NEX.dbo.POP10100

					 --PO LINES
					JOIN DYSNEYDAD.NEX.dbo.POP10110
						ON POP10100.PONUMBER = POP10110.PONUMBER

					-- INVENTORY MASTER
					JOIN DYSNEYDAD.NEX.dbo.IV00101
						ON POP10110.ITEMNMBR = IV00101.ITEMNMBR
				WHERE 
					POP10100.POSTATUS NOT IN (5,6)
					AND POP10100.PONUMBER NOT IN (SELECT GPPONumber FROM WISE_CRM.dbo.IE_PurchaseOrders)
				) AS PO_QRY
				ON POP10100.PONUMBER = PO_QRY.PONUMBER

		SET @maxrow = @@ROWCOUNT
		IF @maxrow > 0
		BEGIN
			BEGIN TRANSACTION
			SET @cntr = 1
	
			/*************
			***  LOOP  ***
			**************/
			WHILE @cntr <= @maxrow
			BEGIN
				SET @GPPONUMBER = (SELECT GPPONUMBER FROM #GPPOs WHERE rownum = @cntr)
			
				/******************
				***  PO HEADER  ***
				*******************/
				INSERT INTO [dbo].[IE_PurchaseOrders] 
				(
					[VendorId]
					,[GPPONumber]
					--,[CloseDate]
					,[ModifiedBy]
					,[CreatedBy]
				) 
				SELECT
					LTRIM(RTRIM(VENDORID))
					,LTRIM(RTRIM(PONUMBER))
					--CLOSEDATE
					,'SYSTEM' -- ModifiedBy - nvarchar(50)
					,'SYSTEM' -- CreatedBy - nvarchar(50)
				FROM
					[DYSNEYDAD].NEX.dbo.POP10100
				WHERE
					(PONUMBER = @GPPONUMBER);

				SET @PurchaseOrderID = SCOPE_IDENTITY();

				/*****************
				***  PO LINES  ***
				******************/
				/*** PO LINES  ***/
				INSERT INTO [dbo].[IE_PurchaseOrderItems] (
					PurchaseOrderId
					,ItemId
					,WarehouseSiteId
					,Quantity
					,ModifiedBy
					,CreatedBy
				)
				SELECT
					@PurchaseOrderID
					, LTRIM(RTRIM(ITM.ItemID))
					, LTRIM(RTRIM(GPITM.LOCNCODE))
					, GPITM.QTYORDER
					,'SYSTEM' -- ModifiedBy - nvarchar(50)
					,'SYSTEM' -- CreatedBy - nvarchar(50)
				FROM
					[DYSNEYDAD].NEX.dbo.POP10110 AS GPITM WITH (NOLOCK)
					INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
					ON
						(ITM.ItemSKU = GPITM.ITEMNMBR)
				WHERE
					(GPITM.PONUMBER = @GPPONumber);

				SET @cntr = @cntr + 1
			END -- END WHILE

		COMMIT TRANSACTION;
		END
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custIE_PurchaseOrdersAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custIE_PurchaseOrdersAddFromGreatPlains; */