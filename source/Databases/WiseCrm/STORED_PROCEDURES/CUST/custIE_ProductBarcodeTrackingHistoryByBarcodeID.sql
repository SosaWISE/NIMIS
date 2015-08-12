USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_ProductBarcodeTrackingHistoryByBarcodeID')
	BEGIN
		PRINT 'Dropping Procedure custIE_ProductBarcodeTrackingHistoryByBarcodeID'
		DROP  Procedure  dbo.custIE_ProductBarcodeTrackingHistoryByBarcodeID
	END
GO

PRINT 'Creating Procedure custIE_ProductBarcodeTrackingHistoryByBarcodeID'
GO
/******************************************************************************
**		File: custIE_ProductBarcodeTrackingHistoryByBarcodeID.sql
**		Name: custIE_ProductBarcodeTrackingHistoryByBarcodeID
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
**		Date: 08/11/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/11/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_ProductBarcodeTrackingHistoryByBarcodeID
(
	@ProductBarcodeId BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT 
			IEPBT.ProductBarcodeTrackingID AS id
			, IEPBT.ProductBarcodeTrackingID
			--, IEPBT.ProductBarcodeTrackingTypeId
			, IEPBT.ProductBarcodeId
			, PBITEM.ItemSKU
			, PBITEM.ModelNumber
			, PBITEM.[ItemDesc]
			, IEPBTT.ProductBarcodeTrackingTypeName
			--, IEPBT.LocationTypeId
			, IELT.LocationTypeName
			, IEPBT.LocationId
			, CASE
				WHEN IELT.LocationTypeID = 'Technician' THEN RU.FullName
				WHEN IELT.LocationTypeID = 'Transfer' OR IELT.LocationTypeID = 'Received' THEN IEWHS.WarehouseSiteName
				WHEN IELT.LocationTypeID = 'Sold' THEN 'C#: ' + CAST(AEC.CustomerMasterFileId AS VARCHAR) + ' | ' + AEC.FirstName + ' ' + AEC.LastName
				WHEN IELT.LocationTypeID = 'Returned' THEN IEV.VendorName
				ELSE NULL
			  END AS LocationName
			, IEPBT.AuditId
			, IEPBT.Comment
			--, IEPBT.IsDeleted
			, IEPBT.ModifiedOn
			, IEPBT.ModifiedBy
			--, IEPBT.CreatedOn
			--, IEPBT.CreatedBy
			--, IEPBT.DEX_ROW_TS
		FROM
			[dbo].[IE_ProductBarcodeTracking] AS IEPBT WITH (NOLOCK)
			INNER JOIN [dbo].[vwIE_ProductBarcodeItem] AS PBITEM
			ON
				(PBITEM.ProductBarcodeId = IEPBT.ProductBarcodeId)
			INNER JOIN [dbo].[IE_ProductBarcodeTrackingTypes] AS IEPBTT WITH (NOLOCK)
			ON
				(IEPBTT.ProductBarcodeTrackingTypeID = IEPBT.ProductBarcodeTrackingTypeId)
			LEFT OUTER JOIN [dbo].[IE_LocationTypes] AS IELT WITH (NOLOCK)
			ON
				(IELT.LocationTypeID = IEPBT.LocationTypeId)
			LEFT OUTER JOIN (
				SELECT CAST(RU1.UserID AS VARCHAR) AS UserID, RU1.FullName FROM [WISE_HumanResource].[dbo].[RU_Users] AS RU1
			) AS RU
			ON
				(RU.UserID = IEPBT.LocationId)
			LEFT OUTER JOIN (
				SELECT 
					[WarehouseSiteID]
					, [WarehouseSiteName]
				FROM [dbo].[IE_WarehouseSites] WITH (NOLOCK)
			) AS IEWHS
			ON
				(IEWHS.WarehouseSiteID = IEPBT.LocationId)
			LEFT OUTER JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
			ON
				(CAST(AEC.CustomerID AS VARCHAR) = IEPBT.LocationId)
			LEFT OUTER JOIN [dbo].[IE_Vendors] AS IEV WITH (NOLOCK)
			ON
				(IEV.VendorID = IEPBT.LocationId)
		WHERE
			(IEPBT.IsDeleted = 'FALSE')
			AND (IEPBT.ProductBarcodeId  = @ProductBarcodeId)
		ORDER BY
			IEPBT.ProductBarcodeTrackingID DESC;
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custIE_ProductBarcodeTrackingHistoryByBarcodeID TO PUBLIC
GO

/** */
EXEC dbo.custIE_ProductBarcodeTrackingHistoryByBarcodeID 716515268;