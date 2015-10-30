USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_ProductBarcodeTracking')
	BEGIN
		PRINT 'Dropping VIEW vwIE_ProductBarcodeTracking'
		DROP VIEW dbo.vwIE_ProductBarcodeTracking
	END
GO

PRINT 'Creating VIEW vwIE_ProductBarcodeTracking'
GO

/****** Object:  View [dbo].[vwIE_ProductBarcodeTracking]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_ProductBarcodeTracking.sql
**		Name: vwIE_ProductBarcodeTracking
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/17/2014	Reagan	Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_ProductBarcodeTracking]
AS
	-- Enter Query here
	/*	SELECT
	     [ProductBarcodeTrackingID]
        ,[ProductBarcodeTrackingTypeId]
		,[ProductBarcodeId]
		,[TransferToWarehouseSiteId]
		,(SELECT [City] FROM  [RU_TeamLocations] WHERE [TeamLocationID] =  [IE_ProductBarcodeTracking].[TransferToWarehouseSiteId]) AS [Location]
		,[ReturnToVendorId]
		,[AssignedToCustomerId]
		,[AssignedToDealerId]
		,[RtmaNumberId]
		,[Comment]
	FROM [dbo].[IE_ProductBarcodeTracking]*/

		SELECT
	     IEPBT.[ProductBarcodeTrackingID]
        ,IEPBT.[ProductBarcodeTrackingTypeId]
		,IEPBT.[ProductBarcodeId]
		,IEPBT.[LocationTypeID]
		,IEPBT.[LocationID]
		/*,IEPBT.[TransferToWarehouseSiteId]
		,IEWS.WarehouseSiteName AS 'Location'
		,IEPBT.[ReturnToVendorId]
		,IEPBT.[AssignedToCustomerId]
		,IEPBT.[AssignedToDealerId]
		,IEPBT.[RtmaNumberId]*/
		,IEPBT.[Comment]
		FROM [dbo].[IE_ProductBarcodeTracking] IEPBT
	--FROM [dbo].[IE_ProductBarcodeTracking] IEPBT INNER JOIN IE_WarehouseSites IEWS
	--on
	--IEPBT.[TransferToWarehouseSiteId] = IEWS.[WarehouseSiteID]


GO

