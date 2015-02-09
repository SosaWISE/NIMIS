USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwQL_LeadProductOffers')
	BEGIN
		PRINT 'Dropping VIEW vwQL_LeadProductOffers'
		DROP VIEW dbo.vwQL_LeadProductOffers
	END
GO

PRINT 'Creating VIEW vwQL_LeadProductOffers'
GO

/****** Object:  View [dbo].[vwQL_LeadProductOffers]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwQL_LeadProductOffers.sql
**		Name: vwQL_LeadProductOffers
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
**		Date: 05/16/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/16/2012	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwQL_LeadProductOffers]
AS
	/** Enter Query here */
	SELECT 
		LPO.LeadProductOfferedId
		, LPO.LeadId
		, LPO.SalesRepId
		, DU.FullName AS SalesRepFullName
		, PRD.ProductSkwID
		, PRT.ProductTypeID
		, PRT.ProductTypeName
		, PRD.ProductName
		, PRD.ShortName
		, PRD.ProductImageName
		, LPO.OfferDate
	FROM
		dbo.QL_LeadProductOffers AS LPO WITH (NOLOCK)
		LEFT OUTER JOIN dbo.MC_DealerUsers AS DU WITH (NOLOCK)
		ON
			(LPO.SalesRepId = DU.UserName)
		INNER JOIN dbo.AE_Products AS PRD WITH (NOLOCK)
		ON
			(LPO.ProductSkwId = PRD.ProductSkwID)		
		INNER JOIN dbo.AE_ProductTypes AS PRT WITH (NOLOCK)
		ON
			(PRD.ProductTypeId = PRT.ProductTypeID)

GO
/* TEST */
SELECT * FROM vwQL_LeadProductOffers