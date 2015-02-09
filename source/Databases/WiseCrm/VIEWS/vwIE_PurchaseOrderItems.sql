USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_PurchaseOrderItems')
	BEGIN
		PRINT 'Dropping VIEW vwIE_PurchaseOrderItems'
		DROP VIEW dbo.vwIE_PurchaseOrderItems
	END
GO

PRINT 'Creating VIEW vwIE_PurchaseOrderItems'
GO

/****** Object:  View [dbo].[vwIE_PurchaseOrderItems]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_PurchaseOrderItems.sql
**		Name: vwIE_PurchaseOrderItems
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 01/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/09/2014	Reagan	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_PurchaseOrderItems]
AS
	/** Enter Query here */
	-- to be continue
	SELECT IEPOIS.[PurchaseOrderItemID]
      ,IEPOIS.[PurchaseOrderId]
      --,IEPOIS.[ProductSkwId]
      ,IEPOIS.[ItemId]
	  ,AEI.[ItemDesc]
      ,IEPOIS.[Quantity]
	  --,[dbo].[fxGetProductBarcodeCount](IEPOIS.[PurchaseOrderItemID]) AS WithBarcodeCount
	  ,0 AS WithBarcodeCount
	  ,(IEPOIS.[Quantity]-[dbo].[fxGetProductBarcodeCount](IEPOIS.[PurchaseOrderItemID])) AS WithoutBarcodeCount
	 

	  , AEI.ItemSKU AS ProductSKU

	FROM [WISE_CRM].[dbo].[IE_PurchaseOrderItems] AS IEPOIS WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[AE_Items] AS AEI WITH (NOLOCK)
		ON
			(IEPOIS.ItemId =AEI.ItemID  )


GO
/* TEST */
-- SELECT * FROM vwIE_PurchaseOrderItems where PurchaseOrderId = 3300
