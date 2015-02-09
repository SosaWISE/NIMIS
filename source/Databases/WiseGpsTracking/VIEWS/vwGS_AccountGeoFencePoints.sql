USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGS_AccountGeoFencePoints')
	BEGIN
		PRINT 'Dropping VIEW vwGS_AccountGeoFencePoints'
		DROP VIEW dbo.vwGS_AccountGeoFencePoints
	END
GO

PRINT 'Creating VIEW vwGS_AccountGeoFencePoints'
GO

/****** Object:  View [dbo].[vwGS_AccountGeoFencePoints]    Script Date: 10/20/2012 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwGS_AccountGeoFencePoints.sql
**		Name: vwGS_AccountGeoFencePoints
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
**		Date: 05/06/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	--------------	-------------------------------------------
**	05/06/2013	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwGS_AccountGeoFencePoints]
AS

	/** Query */
	SELECT DISTINCT
		AGF.GeoFenceID
		, AGF.AccountId
		, AGF.GeogCol2
		, AGFP.PlaceName
		, AGFP.PlaceDescription
		, AGFP.Lattitude AS PointLatitude
		, AGFP.Longitude AS PointLongitude
		, AGF.ModifiedOn
		, AGF.ModifiedBy
		, AGF.CreatedOn
		, AGF.CreatedBy
		, AGF.IsActive
		, AGF.IsDeleted
	FROM
		[dbo].GS_AccountGeoFences AS AGF WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].GS_AccountGeoFenceTypes AS AGFT WITH (NOLOCK)
		ON
			(AGF.GeoFenceTypeId = AGFT.GeoFenceTypeID)
		LEFT OUTER JOIN [dbo].GS_AccountGeoFencePoints AS AGFP WITH (NOLOCK)
		ON
			(AGF.GeoFenceID = AGFP.GeoFenceId)

GO
/* TEST */
 SELECT * FROM vwGS_AccountGeoFencePoints
