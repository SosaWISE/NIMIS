USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGS_AccountGeoFences')
	BEGIN
		PRINT 'Dropping VIEW vwGS_AccountGeoFences'
		DROP VIEW dbo.vwGS_AccountGeoFences
	END
GO

PRINT 'Creating VIEW vwGS_AccountGeoFences'
GO

/****** Object:  View [dbo].[vwGS_AccountGeoFences]    Script Date: 10/20/2012 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwGS_AccountGeoFences.sql
**		Name: vwGS_AccountGeoFences
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
**		Date: 10/20/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/20/2012	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwGS_AccountGeoFences]
AS

	/** Query */
	SELECT DISTINCT
		AGF.GeoFenceID
		, AGF.GeoFenceTypeId
		, [dbo].fxGetGeoFenceTypeUi(AGF.GeoFenceTypeId) AS GeoFenceTypeUi
		, AGF.ReportModeId
		, [dbo].fxGetReportModeUi(AGF.ReportModeId) AS [ReportModeUi]
		, AGF.AccountId
		, CUST.CustomerID
		, CUST.CustomerMasterFileId
		, AGF.GeoFenceName
		, [dbo].fxGetGeoFenceNameUi(AGF.GeoFenceTypeId, AGF.ReportModeId, AGF.GeoFenceName) AS GeoFenceNameUi
		, AGF.GeoFenceDescription
		, AGF.GeogCol2
		, AGF.MeanLattitude
		, AGF.MeanLongitude
		, AGF.Area
		, AGFT.GeoFenceType
		, AGFP.PlaceName
		, AGFP.PlaceDescription
		, AGFP.Lattitude AS PointLatitude
		, AGFP.Longitude AS PointLongitude
		, AGFC.CenterLattitude
		, AGFC.CenterLongitude
		, AGFC.Radius
		--, AGFG.Sequence AS PolySequence
		--, AGFG.Lattitude AS PolyLattitude
		--, AGFG.Longitude AS PolyLongitude
		, AGRG.MinLattitude
		, AGRG.MinLongitude
		, AGRG.MaxLattitude
		, AGRG.MaxLongitude
		, AGF.GoogleMapZoomLevel AS ZoomLevel
		, AGF.ModifiedOn
		, AGF.ModifiedBy
		, AGF.CreatedOn
		, AGF.CreatedBy
		, AGF.IsActive
		, AGF.IsDeleted
	FROM
		[dbo].GS_AccountGeoFences AS AGF WITH (NOLOCK)
		INNER JOIN [dbo].GS_AccountGeoFenceTypes AS AGFT WITH (NOLOCK)
		ON
			(AGF.GeoFenceTypeId = AGFT.GeoFenceTypeID)
		INNER JOIN [dbo].GS_AccountGeoFenceReportModes AS AGRM WITH (NOLOCK)
		ON
			(AGF.ReportModeId = AGRM.ReportModeID)
		LEFT OUTER JOIN [dbo].GS_AccountGeoFencePoints AS AGFP WITH (NOLOCK)
		ON
			(AGF.GeoFenceID = AGFP.GeoFenceId)
		LEFT OUTER JOIN [dbo].GS_AccountGeoFenceCircles AS AGFC WITH (NOLOCK)
		ON
			(AGF.GeoFenceID = AGFC.GeoFenceId)
		--LEFT OUTER JOIN [dbo].GS_AccountGeoFencePolygons AS AGFG WITH (NOLOCK)
		--ON
		--	(AGF.GeoFenceID = AGFG.GeoFenceId)
		LEFT OUTER JOIN [dbo].GS_AccountGeoFenceRectangles AS AGRG WITH (NOLOCK)
		ON
			(AGF.GeoFenceID = AGRG.GeoFenceID)
		INNER JOIN [WISE_CRM].[dbo].[MS_Accounts] AS MAS WITH (NOLOCK)
		ON
			(AGF.AccountId = MAS.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
		ON
			(MAS.AccountID = ACA.AccountId)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		ON
			(ACA.CustomerId = CUST.CustomerID);

GO
/* TEST */
 SELECT * FROM vwGS_AccountGeoFences
