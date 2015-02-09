USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_PackingSlip')
	BEGIN
		PRINT 'Dropping VIEW vwIE_PackingSlip'
		DROP VIEW dbo.vwIE_PackingSlip
	END
GO

PRINT 'Creating VIEW vwIE_PackingSlip'
GO

/****** Object:  View [dbo].[vwIE_PackingSlip]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_PackingSlip.sql
**		Name: vwIE_PackingSlip
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
**		Date: 05/27/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/14/2014	reagan		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_PackingSlip]
AS
	-- Enter Query here
	SELECT
		IEPS.[PackingSlipID]
		,IEPS.[PurchaseOrderId]
		,IEPS.[ArrivalDate]
		,IEPS.[CloseDate]
		,IEPS.[PackingSlipNumber]
		,IEPS.[IsActive]
		,IEPS.[IsDeleted]
		,IEPO.[GPPONumber]
	FROM
	[dbo].[IE_PackingSlips] AS IEPS WITH (NOLOCK)
	INNER JOIN
	[dbo].[IE_PurchaseOrders] AS IEPO WITH (NOLOCK)
	ON
	IEPS.[PurchaseOrderId] = IEPO.[PurchaseOrderID]


GO
/* TEST */
-- SELECT * FROM vwIE_ProductBarcodeLocation where [LocationID] = 100154
