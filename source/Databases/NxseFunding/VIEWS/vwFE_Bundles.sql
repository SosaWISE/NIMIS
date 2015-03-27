USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_Bundles')
	BEGIN
		PRINT 'Dropping VIEW vwFE_Bundles'
		DROP VIEW dbo.vwFE_Bundles
	END
GO

PRINT 'Creating VIEW vwFE_Bundles'
GO

/****** Object:  View [dbo].[vwFE_Bundles]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_Bundles.sql
**		Name: vwFE_Bundles
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
**		Date: 03/27/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/27/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_Bundles]
AS
	-- Enter Query here
	SELECT
		BND.BundleID
		, FEP.PurchaserID
		, FEP.PurchaserName
		, FET.TrackingNumberID
		, FET.TrackingNumber
		, FET.DeliveryDate
		, BND.SubmittedOn
		, BND.SubmittedBy
		, BND.CreatedOn
		, BND.CreatedBy
	FROM
		[dbo].[FE_Bundles] AS BND WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Purchasers] AS FEP WITH (NOLOCK)
		ON
			(FEP.PurchaserID = BND.PurchaserId)
		LEFT OUTER JOIN [dbo].[FE_TrackingNumber] AS FET WITH (NOLOCK)
		ON
			(FET.BundleId = BND.BundleID)
GO
/* TEST */
-- SELECT * FROM vwFE_Bundles
